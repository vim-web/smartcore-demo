Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Partial Public Class AccountMaster
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1134
    Shared Obj_UserRights As Object
    Shared AcHeadDt As DataTable

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
                Session("M_EntryMode") = "NEW"
                TxtAccCode.Text = "NEW"
                Session("SearchType") = "ACCOUNTHEAD"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")


                Load_DefaultValues()
            End If
            If Session("SearchResult") <> "" Then
                TxtAccCode.Text = Session("SearchResult")
                TxtAccCode_TextChanged(Me.TxtAccCode, System.EventArgs.Empty)
                TxtAccCode.Focus()
                Session("SearchResult") = ""
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_Common As New CommonBL
            M_Common.Fill_AccountGroup(CmbGroup)
            M_Common.Fill_Pickup(CmbCurrency, 23, 0)
            M_Common.Fill_Branch(DrpBranch, False, False)

            CmbALIE.Items.Add("Asset")
            CmbALIE.Items.Add("Liability")
            CmbALIE.Items.Add("Income")
            CmbALIE.Items.Add("Expenditure")


            CmbActive.Items.Add("No")
            CmbActive.Items.Add("Yes")

            CmbBalType.Items.Add("Debit")
            CmbBalType.Items.Add("Credit")

            CmbHOAcc.Items.Add("No")
            CmbHOAcc.Items.Add("Yes")
            CmbHOAcc.Items.Add("Branch")

            CmbServiceTax.Items.Add("No")
            CmbServiceTax.Items.Add("Yes")

            CmbStatementType.Items.Add("Detailed")
            CmbStatementType.Items.Add("Summary")

            CmbPosition.Items.Add("Profit&Loss")
            CmbPosition.Items.Add("BalanceSheet")
            CmbPosition.Items.Add("Trading & BalanceSheet")
            CmbPosition.Items.Add("Profit&Loss and BalanceSheet")
            Dim m_datatbl As New DataTable()
            m_datatbl = M_Common.Get_AccCode()
            TxtAccCode.Text = m_datatbl.Rows(0).Item("Acc_code")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Try
            Dim M_AccountMaster As New SMARTCORE_BO.AccountmasterBO

            With M_AccountMaster
                .AccActivatedate = TxtDate.Text
                .AccCode = TxtAccCode.Text
                .AccName = TxtAccName.Text
                .AccShortName = TxtAccShortName.Text
                .AccGrpCode = CmbGroup.SelectedValue
                .AccSeqNo = TxtSeqNO.Text
                .AccBalType = CmbBalType.SelectedItem.Text
                .AccALIE = CmbALIE.SelectedItem.Text
                .AccPosition = CmbPosition.SelectedItem.Text
                .AccStatementtype = CmbStatementType.SelectedItem.Text
                .AccCurrency = CmbCurrency.SelectedValue
                .AccEnabled = CmbActive.SelectedIndex.ToString()
                .AccHoAc = CmbHOAcc.SelectedItem.Text
                .AccServTax = CmbServiceTax.SelectedIndex.ToString()
                .Remarks = TxtRemarks.Text
                .Acc_Branch = IIf(CmbHOAcc.SelectedItem.Text = "Branch", DrpBranch.SelectedValue, "0")
                .AccVisible = CmbAccVisible.SelectedValue
            End With
            Try
                Dim M_AccCode As String
                Dim M_AccountMasterBL As New AccountmasterBL
                M_AccCode = M_AccountMasterBL.Save_Data(M_AccountMaster, Session("M_EntryMode"))
                If M_AccCode > 0 Then
                    If Session("M_EntryMode") = "NEW" Then
                        ShowAlertMessage("Saved Successfully")
                    Else
                        ShowAlertMessage("Updated Successfully")
                    End If

                    clear()
                Else
                    ShowAlertMessage("Not Saved Successfully")

                End If
                M_AccountMasterBL = Nothing
            Catch ex As Exception

            End Try



        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try
            clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Private Sub clear()
        Try
            TxtAccCode.Text = ""
            TxtAccName.Text = ""
            TxtAccShortName.Text = ""
            TxtSeqNO.Text = ""
            TxtRemarks.Text = ""
            CmbActive.SelectedIndex = 0
            CmbALIE.SelectedIndex = 0
            CmbBalType.SelectedIndex = 0
            CmbCurrency.SelectedIndex = 0
            CmbGroup.SelectedIndex = 0
            CmbHOAcc.SelectedIndex = 0
            CmbPosition.SelectedIndex = 0
            CmbServiceTax.SelectedIndex = 0
            CmbStatementType.SelectedIndex = 0

        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub TxtAccCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccCode.TextChanged
        Try
            Dim M_AccountMaster As New SMARTCORE_BO.AccountmasterBO
            Dim M_AccountMasterBL As New AccountmasterBL
            M_AccountMaster = M_AccountMasterBL.Locate_Data(TxtAccCode.Text)
            If M_AccountMaster.AccCode = 0 Then
                Dim str As String
                str = TxtAccCode.Text
                clear()
                TxtAccCode.Text = str
                TxtAccCode.Focus()
                Session("M_EntryMode") = "NEW"

                Exit Sub
            End If
            Session("M_EntryMode") = "UPDATE"

            With M_AccountMaster
                TxtDate.Text = Format(.AccActivatedate, "dd/MM/yyyy")
                TxtAccCode.Text = .AccCode
                TxtAccName.Text = .AccName
                TxtAccShortName.Text = .AccShortName
                CmbGroup.SelectedValue = .AccGrpCode
                TxtSeqNO.Text = .AccSeqNo
                If .AccBalType = "D" Then
                    CmbBalType.SelectedIndex = 0
                Else
                    CmbBalType.SelectedIndex = 1
                End If
                If .AccALIE = "A" Then
                    CmbALIE.SelectedIndex = 0
                ElseIf .AccALIE = "L" Then
                    CmbALIE.SelectedIndex = 1
                ElseIf .AccALIE = "I" Then
                    CmbALIE.SelectedIndex = 2
                Else
                    CmbALIE.SelectedIndex = 3
                End If
                If .AccPosition = "P" Then
                    CmbPosition.SelectedIndex = 0
                ElseIf .AccPosition = "B" Then
                    CmbPosition.SelectedIndex = 1
                ElseIf .AccPosition = "T" Then
                    CmbPosition.SelectedIndex = 2
                Else
                    CmbPosition.SelectedIndex = 3

                End If
                CmbCurrency.SelectedValue = .AccCurrency
                CmbActive.SelectedIndex = .AccEnabled.ToString()
                If .AccHoAc = "Y" Then
                    CmbHOAcc.SelectedIndex = 1
                ElseIf .AccHoAc = "B" Then
                    CmbHOAcc.SelectedIndex = 2
                    DivBranch.Visible = True
                    DrpBranch.SelectedValue = .Acc_Branch.ToString()
                Else
                    CmbHOAcc.SelectedIndex = 0
                End If
                CmbServiceTax.SelectedIndex = .AccServTax.ToString()
                TxtRemarks.Text = .Remarks
                CmbAccVisible.SelectedValue = .AccVisible
            End With
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub CmbHOAcc_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbHOAcc.SelectedIndexChanged
        If CmbHOAcc.SelectedItem.Text = "Branch" Then
            DivBranch.Visible = True
        Else
            DivBranch.Visible = False
        End If
    End Sub
End Class

