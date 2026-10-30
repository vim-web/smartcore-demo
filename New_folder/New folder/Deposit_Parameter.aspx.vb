Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Partial Public Class Deposit_Parameter
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1134
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
            M_CommonBL.GetSchemes(CmbSchemeFrom, Session("TransDate"), "DEPOSIT")



            CmbStandingInstruction.Items.Add("Yes")
            CmbStandingInstruction.Items.Add("No")

            CmbPreClosureAllowed.Items.Add("Yes")
            CmbPreClosureAllowed.Items.Add("No")

            CmbRenewalAllowed.Items.Add("Yes")
            CmbRenewalAllowed.Items.Add("No")

            CmbAutoRenewal.Items.Add("Yes")
            CmbAutoRenewal.Items.Add("No")

            CmbRenewalwithRenewalNo.Items.Add("Yes")
            CmbRenewalwithRenewalNo.Items.Add("No")

            CmbLoanAllowed.Items.Add("Yes")
            CmbLoanAllowed.Items.Add("No")

            CmbExternalLein.Items.Add("Yes")
            CmbExternalLein.Items.Add("No")

            CmbChequeBookAllowed.Items.Add("Yes")
            CmbChequeBookAllowed.Items.Add("No")

            CmbPenaltyForPendingIns.Items.Add("Yes")
            CmbPenaltyForPendingIns.Items.Add("No")

            CmbTODAllowed.Items.Add("Yes")
            CmbTODAllowed.Items.Add("No")

            CmbAgentCollection.Items.Add("Yes")
            CmbAgentCollection.Items.Add("No")

            CmbHolidayInterestApplicable.Items.Add("Yes")
            CmbHolidayInterestApplicable.Items.Add("No")

            CmbIntPostingFrequency.Items.Add("YEARLY")
            CmbIntPostingFrequency.Items.Add("HALF YEARLY")


            M_CommonBL.Fill_Pickup(CmbChequeBookGroup, 51, 0)



            bindgrid()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub bindgrid()
        Try
            Dim ds As New DataSet
            Dim dt As New DataTable("Table1")
            Dim dt1 As New DataTable("Table2")
            Dim Param As New DepParameterBL
            ds = Param.Show_Data(CmbSchemeFrom.SelectedValue)
            dt = ds.Tables(0)
            dt1 = ds.Tables(1)
            Session("Module") = dt1

            If dt.Rows.Count > 0 Then
                Session("Param") = dt
                DgvParam.DataSource = dt
                DgvParam.DataBind()
            Else

                dt.Rows.Add("", "", "", "", "", "", "", "", "", "")
                Session("Param") = dt
                DgvParam.DataSource = dt
                DgvParam.DataBind()
            End If
            DivDD.Visible = False
            DivFD.Visible = False
            DivFDRD.Visible = False
            DivRD.Visible = False
            DivFDRDDD.Visible = False

            DivSBCA.Visible = False
            If dt1.Rows(0)(0).ToString = "FD" Then
                DivFD.Visible = True
                DivFDRD.Visible = True
                DivFDRDDD.Visible = True
            ElseIf dt1.Rows(0)(0).ToString = "RD" Then
                DivFDRD.Visible = True
                DivFDRDDD.Visible = True
                DivRD.Visible = True
            ElseIf dt1.Rows(0)(0).ToString = "DD" Then
                DivFDRDDD.Visible = True
                DivDD.Visible = True
            ElseIf dt1.Rows(0)(0).ToString = "SB" Then
                DivSBCA.Visible = True
            ElseIf dt1.Rows(0)(0).ToString = "CA" Then
                DivSBCA.Visible = True


            End If
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

    Protected Sub CmbSchemeFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSchemeFrom.SelectedIndexChanged
        Try
            bindgrid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            TxtDate.Text = ""
            TxtDormantPeriod.Text = ""
            TxtFreeChequeLeaf.Text = ""
            TxtInOperativePeriod.Text = ""
            TxtInstallmentMultiples.Text = ""
            TxtMaxOpeningAmt.Text = ""
            TxtMaxTransactionAmt.Text = ""
            TxtMinBalanceChqIss.Text = ""
            TxtMinOpeningAmt.Text = ""
            TxtMinTransactionAmt.Text = ""
            TxtPenaltyGradePeriod.Text = ""
            TxtTxtMinBalanceNonChqIss.Text = ""

            CmbStandingInstruction.SelectedIndex = 0
            CmbTODAllowed.SelectedIndex = 0
            CmbSchemeFrom.SelectedIndex = 0
            CmbRenewalwithRenewalNo.SelectedIndex = 0
            CmbRenewalAllowed.SelectedIndex = 0
            CmbAgentCollection.SelectedIndex = 0
            CmbAutoRenewal.SelectedIndex = 0
            CmbChequeBookAllowed.SelectedIndex = 0
            CmbChequeBookGroup.SelectedIndex = 0
            CmbExternalLein.SelectedIndex = 0
            CmbHolidayInterestApplicable.SelectedIndex = 0
            CmbIntPostingFrequency.SelectedIndex = 0
            CmbLoanAllowed.SelectedIndex = 0
            CmbPenaltyForPendingIns.SelectedIndex = 0
            CmbPreClosureAllowed.SelectedIndex = 0


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try


            Dim M_DepParameter As New SMARTCORE_BO.DepParameterBO

            Dim dt As DataTable = DirectCast(Session("Module"), DataTable)
            Dim str As String = dt.Rows(0)(0).ToString()

            If str = "SB" Or str = "CA" Then

                If TxtFreeChequeLeaf.Text = "" Then
                    ShowAlertMessage("Enter Free ChequeLeaf..")
                    TxtFreeChequeLeaf.Focus()
                    Return
                End If

                If TxtInOperativePeriod.Text = "" Then
                    ShowAlertMessage("Enter InOperative Period..")
                    TxtInOperativePeriod.Focus()
                    Return
                End If

                If TxtDormantPeriod.Text = "" Then
                    ShowAlertMessage("Enter Dormant Period..")
                    TxtDormantPeriod.Focus()
                    Return
                End If
                If TxtTxtMinBalanceNonChqIss.Text = "" Then
                    ShowAlertMessage("Enter Min Balance Non Cheque Issued..")
                    TxtTxtMinBalanceNonChqIss.Focus()
                    Return
                End If
                If TxtMinBalanceChqIss.Text = "" Then
                    ShowAlertMessage("Enter Min Balance  Cheque Issued..")
                    TxtMinBalanceChqIss.Focus()
                    Return
                End If

            End If

            If str = "RD" Then
                If TxtInstallmentMultiples.Text = "" Then
                    ShowAlertMessage("Enter Installment Multiples")
                    TxtInstallmentMultiples.Focus()
                    Return
                End If
                If TxtPenaltyGradePeriod.Text = "" Then
                    ShowAlertMessage("Enter Penalty grade period")
                    TxtPenaltyGradePeriod.Focus()
                    Return
                End If

            End If


            With M_DepParameter

                If str = "SB" Or str = "CA" Then
                    .DepFreeChequeLeafPerAnnum = TxtFreeChequeLeaf.Text
                    .DepDormantPeriod = TxtDormantPeriod.Text
                    .DepInopPeriod = TxtInOperativePeriod.Text
                    .DepChqGrpCode = CmbChequeBookGroup.SelectedValue
                    .DepMinBalance = TxtTxtMinBalanceNonChqIss.Text
                    .DepMinBalChqIssued = TxtMinBalanceChqIss.Text
                    .DepIntPostingFreq = CmbIntPostingFrequency.SelectedItem.Text
                    .DepChqIssueAllowed = CmbChequeBookAllowed.SelectedItem.Text
                    .DepTODAllowed = CmbTODAllowed.SelectedItem.Text
                Else
                    .DepFreeChequeLeafPerAnnum = 0
                    .DepDormantPeriod = 0
                    .DepInopPeriod = 0
                    .DepChqGrpCode = 0
                    .DepMinBalance = 0
                    .DepMinBalChqIssued = 0
                    .DepIntPostingFreq = "N"
                    .DepChqIssueAllowed = "N"
                    .DepTODAllowed = "N"
                End If
                If str = "FD" Then
                    .DepPreclosureAllowed = CmbPreClosureAllowed.SelectedItem.Text
                    .DepRenewalsAllowed = CmbRenewalAllowed.SelectedItem.Text
                    .DepAutoRenewal = CmbAutoRenewal.SelectedItem.Text
                    .DepRenewWithNewNo = CmbRenewalwithRenewalNo.SelectedItem.Text
                    .DepLoanAllowed = CmbLoanAllowed.SelectedItem.Text
                    .DepStandingInstr = CmbStandingInstruction.SelectedItem.Text
                    .DepLeinExternalAllowed = CmbExternalLein.SelectedItem.Text
                    .DepHolidayIntApplicable = CmbHolidayInterestApplicable.SelectedItem.Text
                ElseIf str = "RD" Then
                    .DepPreclosureAllowed = CmbPreClosureAllowed.SelectedItem.Text
                    .DepRenewalsAllowed = CmbRenewalAllowed.SelectedItem.Text
                    .DepAutoRenewal = CmbAutoRenewal.SelectedItem.Text
                    .DepRenewWithNewNo = CmbRenewalwithRenewalNo.SelectedItem.Text
                    .DepLoanAllowed = CmbLoanAllowed.SelectedItem.Text
                    .DepStandingInstr = CmbStandingInstruction.SelectedItem.Text
                    .DepInstallmentMultiples = TxtInstallmentMultiples.Text
                    .DepInopPenalty = CmbPenaltyForPendingIns.SelectedItem.Text
                    .DepInopPeriod = TxtPenaltyGradePeriod.Text
                    .DepHolidayIntApplicable = CmbHolidayInterestApplicable.SelectedItem.Text
                ElseIf str = "DD" Then
                    .DepPreclosureAllowed = CmbPreClosureAllowed.SelectedItem.Text
                    .DepRenewalsAllowed = CmbRenewalAllowed.SelectedItem.Text
                    .DepAutoRenewal = CmbAutoRenewal.SelectedItem.Text
                    .DepRenewWithNewNo = CmbRenewalwithRenewalNo.SelectedItem.Text
                    .DepLoanAllowed = CmbLoanAllowed.SelectedItem.Text
                    .DepAgentCollection = CmbAgentCollection.SelectedItem.Text
                    .DepHolidayIntApplicable = CmbHolidayInterestApplicable.SelectedItem.Text
                Else
                    .DepPreclosureAllowed = "N"
                    .DepRenewalsAllowed = "N"
                    .DepAutoRenewal = "N"
                    .DepRenewWithNewNo = "N"
                    .DepLoanAllowed = "N"
                    .DepStandingInstr = "N"
                    .DepLeinExternalAllowed = "N"
                    .DepInstallmentMultiples = 0
                    .DepInopPenalty = "N"
                    .DepInopPeriod = 0
                    .DepAgentCollection = "N"
                    .DepHolidayIntApplicable = "N"

                End If

                .DepMinTranAmount = TxtMinTransactionAmt.Text
                .DepMaxTranAmount = TxtMaxTransactionAmt.Text


                .SchCode = CmbSchemeFrom.SelectedValue
                .BrCode = Session("DefaultBrCode").ToString()

                .MakerID = Session("LoginUserID").ToString()
                .CheckerID = Session("LoginUserID").ToString()
                .DepEffectiveDate = TxtDate.Text

                .DepMinOpbal = TxtMinOpeningAmt.Text
                .DepMaxOpbal = TxtMaxOpeningAmt.Text

            End With

            Try
                Dim M_DepParameterBL As New DepParameterBL
                Dim M_SchCode As Integer
                M_SchCode = M_DepParameterBL.Save_Data(M_DepParameter, "NEW")
                If M_SchCode > 0 Then
                    ShowAlertMessage("Saved Sucessfully")
                    Clear()
                    bindgrid()
                Else
                    ShowAlertMessage("Not Saved Sucessfully")
                End If
                'MsgBox("SchCode : " & M_SchCode)

                M_DepParameterBL = Nothing
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try
            Clear()
            bindgrid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class