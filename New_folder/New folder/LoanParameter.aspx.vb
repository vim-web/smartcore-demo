Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Partial Public Class LoanParameter
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
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Load_DefaultValues()
        Try



            Dim M_CommonBL As New CommonBL
            M_CommonBL.GetSchemes(CmbSchemeFrom, Session("TransDate"), "LOAN")


            CmbMortoriumRequired.Items.Add("Yes")
            CmbMortoriumRequired.Items.Add("No")

            CmbStandingInstruction.Items.Add("Yes")
            CmbStandingInstruction.Items.Add("No")

            CmbAgentCollection.Items.Add("Yes")
            CmbAgentCollection.Items.Add("No")

            CmbDispursalType.Items.Add("Partial")
            CmbDispursalType.Items.Add("LumbSum")

            CmbIntCal.Items.Add("EMI")
            CmbIntCal.Items.Add("AS And When Credit")


            CmbShareRequired.Items.Add("Yes")
            CmbShareRequired.Items.Add("No")

            BindGrid()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub BindGrid()
        Try
            Dim dt As New DataTable("Table1")

            Dim Param As New LoanParameterBL
            dt = Param.Show_Data(CmbSchemeFrom.SelectedValue)
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
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            TxtDate.Text = ""
            TxtDepLoanExtraIntRate.Text = ""
            TxtDepLoanMaxElgibility.Text = ""
            TxtMarginPer.Text = ""
            TxtMaxAge.Text = ""
            TxtMaxLoanAmt.Text = ""
            TxtMaxLoanPeriod.Text = ""
            TxtMinAge.Text = ""
            TxtMinIntDays.Text = ""
            TxtMinLoanAmount.Text = ""
            TxtMinLoanPeriod.Text = ""
            TxtMinNetIncome.Text = ""
            TxtMinNoOfShare.Text = ""
            TxtSharePerc.Text = ""


            CmbAgentCollection.SelectedIndex = 0
            CmbDispursalType.SelectedIndex = 0
            CmbIntCal.SelectedIndex = 0
            CmbMortoriumRequired.SelectedIndex = 0
            CmbSchemeFrom.SelectedIndex = 0
            CmbShareRequired.SelectedIndex = 0
            CmbStandingInstruction.SelectedIndex = 0

            BindGrid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Try

            Clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub CmbSchemeFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSchemeFrom.SelectedIndexChanged
        Try
            BindGrid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try

            Dim M_LoanParameter As New SMARTCORE_BO.LoanParameterBO
            With M_LoanParameter
                .LnpEffectiveDate = TxtDate.Text
                .LnpMinLimit = TxtMinLoanAmount.Text
                .LnpMaxLimit = TxtMaxLoanAmt.Text
                .LnpMinPeriod = TxtMinLoanPeriod.Text
                .LnpMaxPeriod = TxtMaxLoanPeriod.Text
                .LnpMinAge = TxtMinAge.Text
                .LnpMaxAge = TxtMaxAge.Text
                .MinNetIncome = TxtMinNetIncome.Text
                .MarginPercentage = TxtMarginPer.Text
                .MortoriumRequired = CmbMortoriumRequired.SelectedItem.Text
                .LnpDisbursalType = CmbDispursalType.SelectedItem.Text
                .LnpShareRequired = CmbShareRequired.SelectedItem.Text
                .LnpSharePercentage = TxtSharePerc.Text
                .LnpStandingInstr = CmbStandingInstruction.SelectedItem.Text
                .LnpMinIntDays = TxtMinIntDays.Text
                .LnpMinNoSureties = TxtMinNoOfShare.Text
                .IntCalculation = CmbIntCal.SelectedItem.Text
                .DepLoanMaxEligibility = TxtDepLoanMaxElgibility.Text
                .DepLoanExtraIntRate = TxtDepLoanExtraIntRate.Text
                .LnpAgentCollection = CmbAgentCollection.SelectedItem.Text
                .MakerID = Session("LoginUserID").ToString()
                .BrCode = Session("DefaultBrCode").ToString()
                .SchCode = CmbSchemeFrom.SelectedValue
            End With
            Try                Dim M_LoanParameterBL As New LoanParameterBL                Dim M_SchCode As Integer                M_SchCode = M_LoanParameterBL.Save_Data(M_LoanParameter, "NEW")                If M_SchCode > 0 Then                    ShowAlertMessage("Saved Sucessfully")
                    Clear()
                Else
                    ShowAlertMessage("Not Saved Sucessfully")
                End If                'MsgBox("SchCode : " & M_SchCode)                M_LoanParameterBL = Nothing

            Catch ex As Exception
                ShowAlertMessage(ex.Message)

            End Try

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class