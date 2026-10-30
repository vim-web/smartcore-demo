Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class NEFT_IntPayment
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared CommCls As New CommonClass()
    Dim FormID As Integer = 1218
    Dim Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
                Ddlsection.Items.Add("DEPOSIT")
                M_dt = M_CommonBL.Get_Scheme(Ddlsection.SelectedValue)
                DdlScheme.DataTextField = "Sch_Name"
                DdlScheme.DataValueField = "Sch_Code"
                DdlScheme.DataSource = M_dt
                DdlScheme.DataBind()
                DdlScheme.Items.Add("ALL")
                ''Region Wise
                M_CommonBL.Fill_UserWiseRegion(Drpregn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), Drpregn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), Drpregn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DtTo.Text = Session("TransDate")
                DrpRptType.Items.Add("Report")
                DrpRptType.Items.Add("Bank Updation")
                Call DrpRptType_SelectedIndexChanged(Me.DrpRptType, System.EventArgs.Empty)
                ''----Bank Details
                M_CommonBL.Fill_Branch(DrpBankBranch, False, True)
                DrpBankBranch.SelectedValue = Session("DefaultBrCode")
                M_CommonBL.GetBranchBankDetails(DrpBank, DrpBankBranch.SelectedValue)
                M_CommonBL.Fill_BankAccountNo(DrpBankAccNo, DrpBank.SelectedValue.ToString())
                Call DrpBankAccNo_SelectedIndexChanged(Me.DrpBankAccNo, System.EventArgs.Empty)
                ''----Bank Details
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Dim ClsComm As New CommonClass()
        Dim M_Sql As String
        Dim M_Dt As New DataTable()

        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String
        Dim StrSlf As String
        Dim SchemeCode As String
        Dim BranchCode As String
        Dim RegnCode As String
        Dim AreaCode As String
        BranchCode = DdlBranch.SelectedValue
        SchemeCode = DdlScheme.SelectedValue
        RegnCode = Drpregn.SelectedValue
        AreaCode = DrpArea.SelectedValue
        M_DBConn = New Connection()

        Company_Name = M_CommonDa.GetCompanyDetails()
        Dim Terminal = System.Environment.MachineName


        lblProgress.Text = ""
        Dim CommmonCls As New CommonClass
        M_DBConn = New Connection()

        M_Sql = "Update Temp_DCBValueR_Tbl set Regn_Code='" & RegnCode & "',Module='PROCESS_NEFT_RPT',Area_Code = '" & AreaCode & "',Br_Code='" & BranchCode & "',Sch_Code='" & SchemeCode & "',To_Date=" & CommmonCls.DbDate(DtTo.Text) & ",Section='NCD-BOND',Terminal='" & Session("HostID") & "',UserID='" & Session("LoginID") & "'"
        M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
        M_Sql = "exec [StartJOB_Sp] 'RK_PROCESS_NEFT'"
        M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
        Timer1.Enabled = True
        lblProgress.Text = "  Started......"

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub Drpregn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Drpregn.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), Drpregn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DrpArea_SelectedIndexChanged(Me.DrpArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub ShowPrintDialog(ByVal url As String, ByVal target As String, ByVal windowFeatures As String)
        Try
            Dim context As HttpContext = HttpContext.Current

            If ([String].IsNullOrEmpty(target) OrElse target.Equals("_self", StringComparison.OrdinalIgnoreCase)) AndAlso [String].IsNullOrEmpty(windowFeatures) Then

                context.Response.Redirect(url)
            Else
                Dim page As Page = DirectCast(context.Handler, Page)
                If page Is Nothing Then
                    Throw New InvalidOperationException("Cannot redirect to new window outside Page context.")
                End If
                url = page.ResolveClientUrl(url)

                Dim script As String
                windowFeatures = windowFeatures & ",scrollbars=yes"
                If Not [String].IsNullOrEmpty(windowFeatures) Then
                    script = "window.open(""{0}"", ""{1}"", ""{2}"", ""{1}"");"
                Else
                    script = "window.open(""{0}"", ""{1}"");"
                End If

                script = [String].Format(script, url, target, windowFeatures)
                ScriptManager.RegisterStartupScript(page, GetType(Page), "Redirect", script, True)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub
    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), Drpregn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        Try
            Dim M_DataTable As New DataTable

            Dim SqlStr As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()
            M_DBConn = New Connection()

            SqlStr = "SELECT sj.name AS JOBNAME FROM msdb.dbo.sysjobactivity AS sja INNER JOIN msdb.dbo.sysjobs AS sj ON sja.job_id = sj.job_id"
            SqlStr = SqlStr & " WHERE sja.start_execution_date IS NOT NULL    AND sja.stop_execution_date IS NULL AND sj.name = 'PROCESS_NEFT_RPT'"

            M_DBConn.ExecuteDataTable(M_DataTable, SqlStr, False)
            If M_DataTable.Rows.Count > 0 Then
                lblProgress.Text = "  Progress......." 'DateTime.Now.ToString()
            Else
                lblProgress.Text = "  Completed." 'DateTime.Now.ToString()
                Timer1.Enabled = False
            End If
            M_DBConn = Nothing

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try


    End Sub
    Protected Sub Get_PostingData()
        Try
            Dim M_DtTbl As New DataTable
            Dim SqlStr As String
            M_DBConn = New Connection()
            SqlStr = " Exec Get_DepositBankMarking_Sp '" & Drpregn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Session("LoginID") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & IIf(ChkPost.Checked = True, "Y", "N") & "','" & Session("HostID") & "'"

            M_DBConn.ExecuteDataTable(M_DtTbl, SqlStr, False)
            If M_DtTbl.Rows.Count > 0 Then
                Gv_PostInt.DataSource = M_DtTbl
                Gv_PostInt.DataBind()
            End If
            M_DBConn = Nothing
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim SqlStr As String = ""
            Dim clscomm As New CommonClass
            M_DBConn = New Connection()

            If DrpRptType.SelectedItem.Text = "Bank Updation" And ChkPost.Checked = False Then
                Dim M_DataTable As DataTable
                SqlStr = " Exec Get_DepositBankMarking_Sp '" & Drpregn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Session("LoginID") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & IIf(ChkPost.Checked = True, "Y", "N") & "','" & Session("HostID") & "'"

                M_DBConn.ExecuteDataTable(M_DataTable, SqlStr, False)
                If M_DataTable.Rows.Count > 0 Then
                    GvDeposit.DataSource = M_DataTable
                    GvDeposit.DataBind()
                End If
                M_DBConn = Nothing
            ElseIf DrpRptType.SelectedItem.Text = "Report" And ChkPost.Checked = True Then
                Get_PostingData()
            Else
                'Flag,Br_Name ,Sch_Name,Cust_Id ,Cust_Name  ,Acc_No ,Accrued ,Tds ,PayMode,BankName,IFSC
                Dim HeaderArr(14, 4) As String
                Dim HeaderWidth(14, 1) As String
                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "Br_Name"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "12"
                HeaderArr(1, 3) = "12"

                HeaderArr(2, 0) = "Scheme"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "12"
                HeaderArr(2, 3) = "12"

                HeaderArr(3, 0) = "Cust_Id"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "5"
                HeaderArr(3, 3) = "5"

                HeaderArr(4, 0) = "Cust_Name"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "20"
                HeaderArr(4, 3) = "20"

                HeaderArr(5, 0) = "Acc_No"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "Period"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "8"
                HeaderArr(6, 3) = "8"

                HeaderArr(7, 0) = "Eff.Date"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "10"
                HeaderArr(7, 3) = "10"

                HeaderArr(8, 0) = "DueDate"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "10"
                HeaderArr(8, 3) = "10"

                HeaderArr(9, 0) = "Outstanding"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "8"
                HeaderArr(9, 3) = "8"

                HeaderArr(10, 0) = "Accrued"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "8"
                HeaderArr(10, 3) = "8"

                HeaderArr(11, 0) = "Tds"
                HeaderArr(11, 1) = "R"
                HeaderArr(11, 2) = "8"
                HeaderArr(11, 3) = "8"

                HeaderArr(12, 0) = "PayMode"
                HeaderArr(12, 1) = "L"
                HeaderArr(12, 2) = "12"
                HeaderArr(12, 3) = "12"


                HeaderArr(13, 0) = "BankName"
                HeaderArr(13, 1) = "L"
                HeaderArr(13, 2) = "12"
                HeaderArr(13, 3) = "12"

                HeaderArr(14, 0) = "IFSC"
                HeaderArr(14, 1) = "L"
                HeaderArr(14, 2) = "12"
                HeaderArr(14, 3) = "12"

                Session("RptHeader") = HeaderArr

                If DrpRptType.SelectedItem.Text = "Report" Then
                    M_Sql = " EXEC Get_NEFT_IntPaymentDetails_Sp '" & Drpregn.SelectedValue & "','" & DrpArea.SelectedValue & "','" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Session("LoginID") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("HostID") & "' "
                    Session("RptQry") = M_Sql
                    ''Response.Redirect("~/PrnView.aspx?PageType=80", False)
                    Dim PrEMI As New Print
                    PrEMI.ShowPrintDialog("PrnView.aspx?PageType=80", "_blank", "menubar=0,width=1200,height=700,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes")

                    Exit Sub
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpRptType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpRptType.SelectedIndexChanged
        Try
            If DrpRptType.SelectedIndex = 0 Then
                BtnPro.Visible = True
                CmdSave.Visible = False
                ChkPost.Visible = True
            Else
                BtnPro.Visible = False
                CmdSave.Visible = True
                ChkPost.Checked = False
                ChkPost.Visible = False
            End If
            Call ChkPost_CheckedChanged(Me.ChkPost, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try

            If ChkPost.Checked = False Then
                Dim SelectionDT As New DataTable
                SelectionDT.TableName = "Table"
                SelectionDT.Columns.Add("AccId", GetType(String))
                SelectionDT.Columns.Add("Status", GetType(String))

                For i As Integer = 0 To GvDeposit.Rows.Count - 1
                    Dim tb As CheckBox = DirectCast(GvDeposit.Rows(i).FindControl("ChkSelect"), CheckBox)
                    If tb.Checked Then
                        SelectionDT.Rows.Add(GvDeposit.DataKeys(i)("Acc_Id").ToString(), "Y")
                    Else
                        SelectionDT.Rows.Add(GvDeposit.DataKeys(i)("Acc_Id").ToString(), "N")
                    End If
                Next

                Dim M_DataTable As New DataTable
                Dim MComBL As New CommonBL
                Dim SqlStr As String = ""
                Dim M_SelectionSw As New IO.StringWriter
                SelectionDT.WriteXml(M_SelectionSw)
                M_DataTable = MComBL.NEFTBankUpdation(Session("LoginID"), M_SelectionSw.ToString)
                If M_DataTable.Rows.Count > 0 Then
                    GvDeposit.DataSource = Nothing
                    GvDeposit.DataBind()
                    ShowAlertMessage("UPDATION COMPLETED....")
                Else
                    ShowAlertMessage("UPDATION FAILED....")
                End If
            Else
                Dim PostDT As New DataTable
                PostDT.TableName = "Table"
                PostDT.Columns.Add("Acc_Id", GetType(String))
                PostDT.Columns.Add("Br_Code", GetType(String))
                PostDT.Columns.Add("Cust_Id", GetType(String))
                PostDT.Columns.Add("Sch_Code", GetType(String))
                PostDT.Columns.Add("Acc_No", GetType(String))
                PostDT.Columns.Add("Accrued", GetType(String))
                PostDT.Columns.Add("Tds", GetType(String))
                'Acc_Id,Br_Code,Cust_Id,Sch_Code,Acc_No,Accrued,Tds
                For i As Integer = 0 To Gv_PostInt.Rows.Count - 1
                    Dim tb As CheckBox = DirectCast(Gv_PostInt.Rows(i).FindControl("ChkSelect"), CheckBox)
                    If tb.Checked Then
                        Dim AccId As String = Gv_PostInt.DataKeys(i)("Acc_Id").ToString()
                        Dim Br_Code As String = Gv_PostInt.DataKeys(i)("Br_Code").ToString()
                        Dim Cust_Id As String = Gv_PostInt.DataKeys(i)("Cust_Id").ToString()
                        Dim Sch_Code As String = Gv_PostInt.DataKeys(i)("Sch_Code").ToString()
                        Dim Acc_No As String = Gv_PostInt.DataKeys(i)("Acc_No").ToString()
                        Dim Accrued As String = Gv_PostInt.DataKeys(i)("Accrued").ToString()
                        Dim Tds As String = Gv_PostInt.DataKeys(i)("Tds").ToString()
                        PostDT.Rows.Add(AccId, Br_Code, Cust_Id, Sch_Code, Acc_No, Accrued, Tds)

                    End If
                Next

                Dim IntPostDT As New DataTable
                Dim SqlStr As String = ""
                Dim M_ComBL As New CommonBL
                Dim M_PostDTSw As New IO.StringWriter
                PostDT.WriteXml(M_PostDTSw)
                IntPostDT = M_ComBL.Post_NEFT_DepositInt(Session("DefaultBrCode"), DtTo.Text, Session("LoginID"), M_PostDTSw.ToString, Session("HostID"), HdnBankAccId.Value, DrpBankAccNo.SelectedValue, TxtRemarks.Text)
                If IntPostDT.Rows.Count > 0 Then
                    ChkAll.Checked = False
                    Gv_PostInt.DataSource = Nothing
                    Gv_PostInt.DataBind()
                    ShowAlertMessage("SAVED SUCCESSFULLY....")
                Else
                    ShowAlertMessage("UPDATION FAILED....")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChkPost_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkPost.CheckedChanged
        Try
            If ChkPost.Checked = True Then
                BtnPro.Visible = True
                'BtnPost.Visible = True
                CmdSave.Visible = True
                Gv_PostInt.DataSource = Nothing
                Gv_PostInt.DataBind()
                GvDeposit.DataSource = Nothing
                GvDeposit.DataBind()
                PostDiv.Visible = True
                DivBank.Visible = False
                'DivbankAccount.Visible = True
                DtTo.Text = Session("TransDate")
                DtTo.Enabled = False
            Else
                ' BtnPro.Visible = False
                DivBank.Visible = False
                BtnPost.Visible = False
                PostDiv.Visible = False
                DivBank.Visible = True
                DivbankAccount.Visible = False
                Gv_PostInt.DataSource = Nothing
                Gv_PostInt.DataBind()
                GvDeposit.DataSource = Nothing
                GvDeposit.DataBind()
                DtTo.Enabled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChkAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAll.CheckedChanged
        Try
            If ChkAll.Checked = True Then

                Dim i As Integer
                For i = 0 To Gv_PostInt.Rows.Count - 1
                    Dim row As GridViewRow = Gv_PostInt.Rows(i)
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = True
                Next
            ElseIf ChkAll.Checked = False Then
                For i = 0 To Gv_PostInt.Rows.Count - 1
                    Dim row As GridViewRow = Gv_PostInt.Rows(i)
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = False
                Next

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub drpBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrpBank.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_BankAccountNo(DrpBankAccNo, DrpBank.SelectedValue.ToString())
    End Sub
    Protected Sub DrpBankBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpBankBranch.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.GetBranchBankDetails(DrpBank, DrpBankBranch.SelectedValue)
        M_CommonBL.Fill_BankAccountNo(DrpBankAccNo, DrpBank.SelectedValue.ToString())
        Call DrpBankAccNo_SelectedIndexChanged(Me.DrpBankAccNo, System.EventArgs.Empty)
    End Sub

    Protected Sub DrpBankAccNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpBankAccNo.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.GetCustDetailsByAccNo(DrpBankAccNo.SelectedItem.Text)
            If M_Dt.Rows.Count > 0 Then
                HdnBankAccId.Value = M_Dt.Rows(0).Item("ACC_ID").ToString()
            Else
                HdnBankAccId.Value = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class