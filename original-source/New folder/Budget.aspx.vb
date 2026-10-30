Imports SMARTCORE_BL
Imports SMARTCORE_DA

Public Class Budget
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub TxtMar_TextChanged(sender As Object, e As EventArgs) Handles TxtMar.TextChanged, TxtApr.TextChanged, TxtMay.TextChanged, TxtJune.TextChanged, TxtJuly.TextChanged, TxtAug.TextChanged, TxtSep.TextChanged, TxtOct.TextChanged, TxtNov.TextChanged, TxtDec.TextChanged, TxtJan.TextChanged, TxtFeb.TextChanged
        TxtBudgAmt.Text = Val(TxtApr.Text) + Val(TxtMay.Text) + Val(TxtJune.Text) + Val(TxtJuly.Text) + Val(TxtAug.Text) + Val(TxtSep.Text) + Val(TxtOct.Text) + Val(TxtNov.Text) + Val(TxtDec.Text) + Val(TxtJan.Text) + Val(TxtFeb.Text) + Val(TxtMar.Text)
    End Sub

    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.Fill_AccountHead(CmbAcHead, "7")
            M_CommonBL.Fill_Pickup(CmbYear, 101, 0)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            'If ValidateFields() = False Then

            '    Exit Sub

            'End If
            Dim M_Budget As New SMARTCORE_BO.BudgetBO
            With M_Budget
                .BrCode = CmbBranch.SelectedValue
                .BudgetAmt = Val(TxtBudgAmt.Text)
                .April = Val(TxtApr.Text)
                .May = Val(TxtMay.Text)
                .June = Val(TxtJune.Text)
                .July = Val(TxtJuly.Text)
                .August = Val(TxtAug.Text)
                .September = Val(TxtSep.Text)
                .October = Val(TxtOct.Text)
                .November = Val(TxtNov.Text)
                .December = Val(TxtDec.Text)
                .January = Val(TxtJan.Text)
                .February = Val(TxtFeb.Text)
                .March = Val(TxtMar.Text)
                .AcHead = CmbAcHead.SelectedValue
                .Year = CmbYear.SelectedValue
            End With

            Dim M_BudgetBl As New BudgetBL
            Session("M_EntryMode") = "NEW"
            M_BudgetBl.Save_Data(M_Budget, Session("M_EntryMode"))
            ShowAlertMessage("BUDGET SUCCESSFULLY SAVED")
        Catch ex As Exception
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub
    'Private Function ValidateFields() As Boolean
    '    Try
    '        Dim Status As Boolean

    '        If Val(TxtApr.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage(" Please enter budget for April")
    '            TxtApr.Focus()
    '            Return False
    '        End If

    '        If Val(TxtMay.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage(" Please enter budget for May")
    '            TxtMay.Focus()
    '            Return False
    '        End If
    '        If Val(TxtJune.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage(" Please enter budget for June")
    '            TxtJune.Focus()
    '            Return False
    '        End If
    '        If Val(TxtJuly.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage(" Please enter budget for July")
    '            TxtJuly.Focus()
    '            Return False
    '        End If

    '        If Val(TxtAug.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage("Please enter budget for August")
    '            TxtAug.Focus()
    '            Return False
    '        End If
    '        If Val(TxtSep.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage(" Please enter budget for September")
    '            TxtSep.Focus()
    '            Return False
    '        End If
    '        If Val(TxtOct.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage("Please enter budget for October")
    '            TxtOct.Focus()
    '            Return False
    '        End If

    '        If Val(TxtNov.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage("Please enter budget for November")
    '            TxtNov.Focus()
    '            Return False
    '        End If
    '        If Val(TxtDec.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage("Please enter budget for December")
    '            TxtDec.Focus()
    '            Return False
    '        End If
    '        If Val(TxtJan.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage(" Please enter budget for January")
    '            TxtJan.Focus()
    '            Return False
    '        End If
    '        If Val(TxtFeb.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage(" Please enter budget for February")
    '            TxtFeb.Focus()
    '            Return False
    '        End If

    '        If Val(TxtMar.Text.Length) > 0 Then
    '            Status = True
    '        Else
    '            ShowAlertMessage(" Please enter budget for March")
    '            TxtMar.Focus()
    '            Return False
    '        End If
    '        Return Status
    '    Catch ex As Exception
    '        ShowAlertMessage(ex.Message)
    '    End Try

    'End Function
    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmbYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbYear.SelectedIndexChanged, CmbAcHead.SelectedIndexChanged, CmbBranch.SelectedIndexChanged
        Dim M_BudgetBl As New BudgetBL
        Dim M_Budget As New SMARTCORE_BO.BudgetBO
        M_Budget = M_BudgetBl.Locate_Data(CmbBranch.SelectedValue, CmbAcHead.SelectedValue, CmbYear.SelectedItem.Text)

        TxtBudgAmt.Text = ""
        TxtApr.Text = ""
        TxtMay.Text = ""
        TxtJune.Text = ""
        TxtJuly.Text = ""
        TxtAug.Text = ""
        TxtSep.Text = ""
        TxtOct.Text = ""
        TxtNov.Text = ""
        TxtDec.Text = ""
        TxtJan.Text = ""
        TxtFeb.Text = ""
        TxtMar.Text = ""


        With M_Budget
            TxtBudgAmt.Text = Format(Val(.BudgetAmt), "#.00")
            TxtApr.Text = Format(Val(.April), "#.00")
            TxtMay.Text = Format(Val(.May), "#.00")
            TxtJune.Text = Format(Val(.June), "#.00")
            TxtJuly.Text = Format(Val(.July), "#.00")
            TxtAug.Text = Format(Val(.August), "#.00")
            TxtSep.Text = Format(Val(.September), "#.00")
            TxtOct.Text = Format(Val(.October), "#.00")
            TxtNov.Text = Format(Val(.November), "#.00")
            TxtDec.Text = Format(Val(.December), "#.00")
            TxtJan.Text = Format(Val(.January), "#.00")
            TxtFeb.Text = Format(Val(.February), "#.00")
            TxtMar.Text = Format(Val(.March), "#.00")
        End With

    End Sub
End Class