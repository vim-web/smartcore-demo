Imports SMARTCORE_BL
Imports SMARTCORE_Common

Partial Public Class LoanIntSubventionRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1090
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If



                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch, True)
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN")
            TxtFrDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
            TxtLoanDtFr.Text = Session("TransDate")
            TxtLoanDtTo.Text = Session("TransDate")
            TxtOSDate.Text = Session("TransDate")
            'TxtRegnDate.Attributes.Add("readonly", "readonly")
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
            Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtBranch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBranch.TextChanged
        Try
            CmbBranch.SelectedValue = TxtBranch.Text
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        Try
            CmbScheme.SelectedValue = TxtScheme.Text
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            TxtBranch.Text = CmbBranch.SelectedValue
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtScheme.Text = CmbScheme.SelectedValue
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


    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim CommonCls As New CommonClass
            Dim StrSlf As String = ""
            Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

            If ChkClosed.Checked = True Then
                M_Sql = "Exec Loan_Int_SubVentionClosedAcs_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & _
                    "," & CommonCls.DbDate(TxtToDate.Text) & "," & CommonCls.DbDate(TxtLoanDtFr.Text) & ",'" & Session("UserID") & "','" & _
                    Session("HostID") & "','" & IIf(ChkLoanAmt.Checked = True, "Y", "N") & "'," & Val(TxtLnAmtFr.Text) & _
                    "," & Val(TxtLnAmtTo.Text) & "," & IIf(ChkOutstnd.Checked = True, CommonCls.DbDate(TxtOSDate.Text), "'01/01/1900'") & "," & CommonCls.DbDate(TxtLoanDtTo.Text)
            Else
                M_Sql = "Exec Loan_Int_SubVention_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & _
                "," & CommonCls.DbDate(TxtToDate.Text) & "," & CommonCls.DbDate(TxtLoanDtFr.Text) & "," & CommonCls.DbDate(TxtLoanDtTo.Text) & ",'" & Session("UserID") & "','" & _
                    Session("HostID") & "','" & IIf(ChkLoanAmt.Checked = True, "Y", "N") & "'," & Val(TxtLnAmtFr.Text) & _
                    "," & Val(TxtLnAmtTo.Text) & "," & IIf(ChkOutstnd.Checked = True, CommonCls.DbDate(TxtOSDate.Text), "'01/01/1900'")
            End If

            Session.Add("RptQry", M_Sql)
            Session("RptPath") = Server.MapPath("~/Reports/Loans/Loan_Int_Subvention.rpt")

            StrSlf = "Company|" & Company_Name & "|Branch|" & CmbBranch.SelectedItem.ToString & " - Branch|Heading|Loan Interest Subvention Statement Between " & _
                TxtFrDate.Text & " and " & TxtToDate.Text
            StrSlf = StrSlf + " , Loans Between " & TxtLoanDtFr.Text & " And " & TxtLoanDtTo.Text

            If ChkLoanAmt.Checked = True Then StrSlf = StrSlf + " , Loans Amount Between " & Val(TxtLnAmtFr.Text) & " And " & Val(TxtLnAmtTo.Text)
            If ChkClosed.Checked = True Then StrSlf = StrSlf + " , Claim For Closed Loans"
            If ChkOutstnd.Checked = True Then StrSlf = StrSlf + " , Outstanding As On " & TxtOSDate.Text

            StrSlf = StrSlf + " |User_ID| " & Session("UserID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            StrSlf = StrSlf & "|Percent1|" & Val(TxtPercent1.Text) & "|Percent2|" & Val(TxtPercent2.Text)

            Session.Add("RptFormula", StrSlf)

            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class