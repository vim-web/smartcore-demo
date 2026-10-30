Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Public Class LoanStanding
    Inherits System.Web.UI.Page
    Shared Dt_AcDtls As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbFrmBranch)
            M_CommonBL.Fill_Branch(CmbToBranch)
            CmbFrmBranch.SelectedValue = Session("DefaultBrCode")
            CmbToBranch.SelectedValue = Session("DefaultBrCode")

            M_CommonBL.GetSchemes(CmbLoanScheme, Session("TransDate"), "LOAN")
            M_CommonBL.GetSchemes(CmbDepScheme, Session("TransDate"), "DEPOSIT")

            CmbAmountType.Items.Add("Balance")
            CmbAmountType.Items.Add("Fixed")

            CmbType.Items.Add("Total")
            CmbType.Items.Add("Principle")
            CmbType.Items.Add("Whole Amount")

            Call CmbDepScheme_SelectedIndexChanged(Me.CmbDepScheme, System.EventArgs.Empty)
            Call CmbLoanScheme_SelectedIndexChanged(Me.CmbLoanScheme, System.EventArgs.Empty)
            M_CommonBL = Nothing
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
    Protected Sub CmbDepScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbDepScheme.SelectedIndexChanged
        TxtDepScheme.Text = CmbDepScheme.SelectedValue
        CmbDepScheme.SelectedValue = TxtDepScheme.Text
    End Sub

    Protected Sub CmbLoanScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbLoanScheme.SelectedIndexChanged
        TxtLoanScheme.Text = CmbLoanScheme.SelectedValue
        CmbLoanScheme.SelectedValue = TxtLoanScheme.Text
    End Sub

    Protected Sub TxtLoanScheme_TextChanged(sender As Object, e As EventArgs) Handles TxtLoanScheme.TextChanged
        CmbLoanScheme.SelectedValue = TxtLoanScheme.Text
        TxtLoanScheme.Text = CmbLoanScheme.SelectedValue
    End Sub

    Protected Sub TxtDepScheme_TextChanged(sender As Object, e As EventArgs) Handles TxtDepScheme.TextChanged
        CmbDepScheme.SelectedValue = TxtDepScheme.Text
        TxtDepScheme.Text = CmbDepScheme.SelectedValue
    End Sub

    Protected Sub TxtFrmShCode_TextChanged(sender As Object, e As EventArgs) Handles TxtFrmShCode.TextChanged
        TxtFrmAccNo.Text = Right("000" + CmbFrmBranch.SelectedValue, 3) + Right("000" + CmbDepScheme.SelectedValue, 3) + Right("0000000" + TxtFrmShCode.Text, 7)
        Call TxtFrmAccNo_TextChanged(Me.TxtFrmAccNo, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtLoanShCode_TextChanged(sender As Object, e As EventArgs) Handles TxtLoanShCode.TextChanged
        TxtLoanAccNo.Text = Right("000" + CmbToBranch.SelectedValue, 3) + Right("000" + CmbLoanScheme.SelectedValue, 3) + Right("0000000" + TxtLoanShCode.Text, 7)
        Call TxtLoanAccNo_TextChanged(Me.TxtLoanAccNo, System.EventArgs.Empty)
    End Sub

    Protected Sub TxtLoanAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtLoanAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtLoanAccNo.Text
            Dim M_Sql As String
            Dim M_DbConn As New Connection
            Dim M_Dt As New DataTable

            M_Sql = "select day(Lm_LoanDate) from Loan_Master_M_Tbl where Acc_No='" & TxtLoanAccNo.Text & "'"
            M_DbConn.ExecuteDataTable(M_Dt, M_Sql, False)
            If M_Dt.Rows.Count > 0 Then
                TxtExeDay.Text = M_Dt(0)(0).ToString
            End If
            'Call CmbSchemeTo_SelectedIndexChanged(Me.CmbSchemeFrom, System.EventArgs.Empty)

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbToBranch.SelectedValue, CmbLoanScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtToCustNo.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Session("CustomerId") = TxtToCustNo.Text
                TxtToName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                'TxtMblNo.Text = Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                TxtToAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                AccIdTo.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()

            Else
                ShowAlertMessage("Invalid A/c No")
                TxtLoanAccNo.Focus()
            End If

            'TxtAccNoTo.Text = M_AccNo
            M_DbConn = Nothing
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_LoanStandingBO As New SMARTCORE_BO.LoanStandingBO
            With M_LoanStandingBO
                .AccID = AccIdTo.Value
                .FromAccID = AccIDFrom.Value
                .ExecutionDay = Val(TxtExeDay.Text)
                .LogID = "0"
                .InstrDate = Session("TransDate")
                '.MakingTime = M_Dt.Rows(0).Item("Making_Time")
                '.CheckingTime = M_Dt.Rows(0).Item("Checking_Time")
                .ExecutionAmt = TxtAmount.Text
                '.StandingID = M_Dt.Rows(0).Item("Standing_ID")
                .BrCode = CmbToBranch.SelectedValue
                .SchCode = CmbLoanScheme.SelectedValue
                .AccNo = TxtLoanAccNo.Text
                .ExecutionType = CmbType.SelectedValue
                .MakerID = Session("LoginUserID")
                .CheckerID = ""
                .ExecutionPeriodicity = CmbStatus.SelectedValue
                .ExecutionAmtType = CmbAmountType.SelectedValue
                .Authorised = "Y"
                .PostUptoDate = "N"
            End With

            Dim M_LoanStdBL As New LoanStandingBL
            Dim M_StId As Integer
            Session("EntryMode") = "NEW"
            M_StId = M_LoanStdBL.Save_Data(M_LoanStandingBO, Session("EntryMode"))
            If M_StId > 0 Then
                ShowAlertMessage("Successfully Saved")
            End If
        Catch
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub

    Protected Sub TxtFrmAccNo_TextChanged(sender As Object, e As EventArgs) Handles TxtFrmAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_AccNo As String = TxtLoanAccNo.Text
            Dim DT_Assignedtails As New DataTable
            Dim M_Date As New DataTable


            ''Call CmbSchemeFrom_SelectedIndexChanged(Me.CmbSchemeFrom, System.EventArgs.Empty)

            DT_Assignedtails = M_CommonBL.CheckLoanStanding(CmbFrmBranch.SelectedValue, CmbLoanScheme.SelectedValue, M_AccNo, Session("TransDate"))

            If DT_Assignedtails.Rows.Count > 0 Then
                ShowAlertMessage("This Account is Already Standing with  " & DT_Assignedtails.Rows(0)(2) & "  A/c No")
                TxtFrmAccNo.Focus()
                Exit Sub
            End If

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbFrmBranch.SelectedValue, CmbDepScheme.SelectedValue, M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtFrmCustNo.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Session("CustomerId") = TxtFrmCustNo.Text
                TxtFrmName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                AccIDFrom.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                TxtFrmAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()


            Else
                AccIDFrom.Value = ""
                ShowAlertMessage("Invalid A/c No")
                TxtFrmAccNo.Focus()
            End If

            'TxtAccNoFrom.Text = M_AccNo

            M_CommonBL = Nothing

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class