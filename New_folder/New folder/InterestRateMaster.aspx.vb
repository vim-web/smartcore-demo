Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Partial Public Class InterestRateMaster
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1134
    Shared Obj_UserRights As Object
    Shared AcHeadDt As DataTable
    Shared SchemeDt As DataTable

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

                '    Dim LblMaster As Label = DirectCast(Master.FindControl("lbl_Heading"), Label)
                ' LblMaster.Text = "Interest Rate Master"
                Session("M_EntryMode") = "NEW"


                Load_DefaultValues()
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub
    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            SchemeDt = DirectCast(Cache("SchemeTbl"), DataTable)
            CommonClass.Fill_Combo(DrpScheme, SchemeDt, "Sch_Name", "Sch_Code")
            M_CommonBL.Fill_Pickup(CmbRenewIntPayMode, 44, 0)

            DrpPeriodType.Items.Add("Months")
            DrpPeriodType.Items.Add("Days")

            'DrpIntCalcMode.Items.Add("As Per Slab")
            'DrpIntCalcMode.Items.Add("For Completed Periods")

            DrpInterestFor.Items.Add("Amount")
            DrpInterestFor.Items.Add("Period")


            TxtEffectiveDate.Text = Session("TransDate").ToString()
            TxtCircularDate.Text = Session("TransDate").ToString()

            'TxtAmtUpto.Enabled = False

            'Schemes(DrpSchemeType.SelectedItem.Text)

            'TxtPeriodFrom.Enabled = False
            'TxtPeriodFrom.Text = "1"

            'BindGrid()
            'getvalue()
            ViewState("DenyDelete") = "N"
            grid()
            Call DrpScheme_SelectedIndexChanged(Me.DrpScheme, System.EventArgs.Empty)

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub grid()
        Try
            Dim dt As New DataTable("Table")
            dt.Columns.Add("PeriodStart")
            dt.Columns.Add("PeriodEnd")
            dt.Columns.Add("InterestRate")
            dt.Columns.Add("AmountUpto")
            dt.Columns.Add("Staff")
            dt.Columns.Add("ExStaff")
            dt.Columns.Add("SenCitizen")
            dt.Columns.Add("PayMode")
            dt.Columns.Add("PayModeId")
            dt.Rows.Add("", "", "", "", "", "", "", "", "")
            Session("M_Dgv") = dt
            GVGoldIntRate.DataSource = dt
            GVGoldIntRate.DataBind()
            Dim TxtPeriodFrom As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPeriodFrom"), TextBox)
            TxtPeriodFrom.Enabled = False
            TxtPeriodFrom.Text = 1
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

    Protected Sub ImgBtnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try


            Dim dt As DataTable = DirectCast(Session("M_Dgv"), DataTable)

            Dim dtrow As DataRow
            dtrow = dt.NewRow()
            Dim TxtPeriodFrom As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPeriodFrom"), TextBox)
            Dim TxtPeriodTo As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPeriodTo"), TextBox)
            Dim TxtIntRate As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtIntRate"), TextBox)
            Dim TxtAmtUpto As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtAmtUpto"), TextBox)
            Dim TxtStaff As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtStaff"), TextBox)
            Dim TxtExStaff As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtExStaff"), TextBox)
            Dim TxtSenCitizen As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtSenCitizen"), TextBox)
            Dim TxtPayMode As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPayMode"), TextBox)
            Dim TxtPayModeId As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPayModeId"), TextBox)
            dtrow(0) = TxtPeriodFrom.Text
            dtrow(1) = TxtPeriodTo.Text
            dtrow(2) = TxtIntRate.Text
            dtrow(3) = TxtAmtUpto.Text
            dtrow(4) = TxtStaff.Text
            dtrow(5) = TxtExStaff.Text
            dtrow(6) = TxtSenCitizen.Text
            dtrow(7) = TxtPayMode.Text
            dtrow(8) = TxtPayModeId.Text

            dt.Rows.Add(dtrow)
            Try
                Dim Expression As String = "PeriodStart=''"
                If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
            Catch ex As Exception

            End Try
            TxtPeriodFrom.Text = (TxtPeriodTo.Text) + 1
            TxtPeriodTo.Text = ""
            TxtIntRate.Text = ""
            TxtAmtUpto.Text = ""
            TxtStaff.Text = ""
            TxtExStaff.Text = ""
            TxtSenCitizen.Text = ""
            TxtPayMode.Text = ""
            TxtPayModeId.Text = ""
            
            GVGoldIntRate.DataSource = dt
            GVGoldIntRate.DataBind()

            Call DrpInterestFor_SelectedIndexChanged(Me.DrpInterestFor, System.EventArgs.Empty)
            Call DrpScheme_SelectedIndexChanged(Me.DrpScheme, System.EventArgs.Empty)
        

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_IntRateMaster As New SMARTCORE_BO.IntRateMasterBO            With M_IntRateMaster

                '.ImSlabId = 
                '.ImPeriodupto = 
                '.LogID = Session("LoginUserID").ToString()
                .ImEffectiveDate = TxtEffectiveDate.Text
                .ImIssueDate = "01/01/1990"
                .ImCirculardate = TxtCircularDate.Text
                '.MakingTime = 
                '.CheckingTime = 
                '.ImAmountupto = 
                '.ImIntRate = 
                '.ImStaff = 
                '.ImSenior = 
                '.ImExStaff = 
                '.ImPenal = 
                '.ImCrIntRate = 
                .SchCode = DrpScheme.SelectedValue
                .ImCircularNo = TxtCirNo.Text
                '.ImPeriodAmt = 
                .MakerID = Session("LoginUserID").ToString()
                .CheckerID = Session("LoginUserID").ToString()
                '.ImUponIssueDate = 
                '.ImIssueDateGtOrLt = 
                .ImPeriodType = DrpPeriodType.SelectedItem.Text
                '.Authorised = 

            End With            Try                Dim M_IntRateMasterBL As New IntRateMasterBL                Dim M_SchCode As Integer                Dim Trandt As DataTable = DirectCast(Session("M_Dgv"), DataTable)
                Dim M_TranSw As New IO.StringWriter

                Trandt.TableName = "Table"

                Trandt.WriteXml(M_TranSw)                M_SchCode = M_IntRateMasterBL.Save_Data(M_IntRateMaster, "NEW", M_TranSw.ToString())                If M_SchCode > 0 Then                    ShowAlertMessage("Saved Sucessfully")
                    Clear()
                Else
                    ShowAlertMessage("Not Saved Sucessfully")
                End If                'MsgBox("SchCode : " & M_SchCode)                M_IntRateMasterBL = Nothing

            Catch ex As Exception
                ShowAlertMessage(ex.Message)

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
            grid()
            DrpInterestFor.SelectedIndex = 0
            DrpPeriodType.SelectedIndex = 0
            DrpScheme.SelectedIndex = 0
            'TxtCircularDate.Text = ""
            TxtEffectiveDate.Text = Session("TransDate").ToString()
            TxtCircularDate.Text = Session("TransDate").ToString()
            TxtCirNo.Text = ""
            'TxtEffectiveDate.Text = ""
            Dim TxtPeriodFrom As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPeriodFrom"), TextBox)
            Dim TxtPeriodTo As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPeriodTo"), TextBox)
            Dim TxtIntRate As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtIntRate"), TextBox)
            Dim TxtAmtUpto As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtAmtUpto"), TextBox)
            Dim TxtStaff As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtStaff"), TextBox)
            Dim TxtExStaff As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtExStaff"), TextBox)
            Dim TxtSenCitizen As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtSenCitizen"), TextBox)
            Dim TxtPayMode As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPayMode"), TextBox)
            Dim TxtPayModeId As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPayModeId"), TextBox)
            TxtPeriodFrom.Text = 1
            TxtPeriodTo.Text = ""
            TxtIntRate.Text = ""
            TxtAmtUpto.Text = ""
            TxtStaff.Text = ""
            TxtExStaff.Text = ""
            TxtSenCitizen.Text = ""
            TxtPayMode.Text = ""
            TxtPayModeId.Text = ""

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub GVGoldIntRate_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GVGoldIntRate.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("M_Dgv"), DataTable)
            dt.Rows(e.RowIndex).Delete()

            GVGoldIntRate.DataSource = dt
            GVGoldIntRate.DataBind()
            Session("M_Dgv") = dt

            If dt.Rows.Count <= 0 Then Call grid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpInterestFor_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpInterestFor.SelectedIndexChanged
        Dim TxtAmtUpto As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtAmtUpto"), TextBox)
        If DrpInterestFor.SelectedIndex = 1 Then
            TxtAmtUpto.Enabled = False
            TxtAmtUpto.BackColor = Drawing.Color.Gray
            TxtAmtUpto.Text = "0"
        ElseIf DrpInterestFor.SelectedIndex = 0 Then
            TxtAmtUpto.Enabled = True
            TxtAmtUpto.BackColor = Drawing.Color.White
        End If

    End Sub

    Protected Sub DrpScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpScheme.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim dt As DataTable
            dt = M_CommonBL.Get_SchemeDetailsDeposit()
            If dt.Rows.Count > 0 Then

                For i As Integer = 1 To dt.Rows.Count - 1
                    If dt.Rows(i)("Sch_Code").ToString() = DrpScheme.SelectedValue Then

                        CmbRenewIntPayMode.Enabled = True
                        Call CmbRenewIntPayMode_SelectedIndexChanged(Me.CmbRenewIntPayMode, System.EventArgs.Empty)
                        Exit Sub

                    Else
                        CmbRenewIntPayMode.Enabled = False
                        Call CmbRenewIntPayMode_SelectedIndexChanged(Me.CmbRenewIntPayMode, System.EventArgs.Empty)
                    End If
                Next


            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbRenewIntPayMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbRenewIntPayMode.SelectedIndexChanged
        Try
            Dim TxtPayMode As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPayMode"), TextBox)
            Dim TxtPayModeId As TextBox = DirectCast(GVGoldIntRate.FooterRow.FindControl("TxtPayModeId"), TextBox)
            If CmbRenewIntPayMode.Enabled = True Then
                TxtPayMode.Text = CmbRenewIntPayMode.SelectedItem.Text
                TxtPayModeId.Text = CmbRenewIntPayMode.SelectedValue
            Else
                TxtPayMode.Text = "0"
                TxtPayModeId.Text = "0"
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class