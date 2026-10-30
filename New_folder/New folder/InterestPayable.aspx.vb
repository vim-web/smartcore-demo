Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class InterestPayable
    Inherits System.Web.UI.Page
    Private M_DBConn As Connection
    Private Rpt_Qry As String
    Private Rpt_Path As String
    Private Rpt_Heading As String
    Private Rpt_Formula As String
    Private Rpt_ProcessQry As String
    Private M_CommonBL As New CommonBL
    Private M_CommonDa As New CommonDA
    Dim FormID As Integer = 1073
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If


                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                DdlBranch.Items.Add("-- ALL --")
                DtFrom.Text = Session("TransDate")
                If Request.QueryString("RptType") = "Payable" Then
                    DdlRptType.SelectedIndex = 0
                Else
                    DdlRptType.SelectedIndex = 1
                End If


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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click

        Try

        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        Dim Br_Code As String
        Dim Br As Integer
        Dim Str_Flf, Sql_String As String
        M_DBConn = New Connection()


        If DdlRptType.SelectedItem.Text = "Payable" Then
            Sql_String = "select (O.Acc_No) As AccNo,(Accrued) As Accrued ,S.Sch_Name,S.Sch_Section_Name from  Deposit_Outstand_R_Tbl O  " & _
                " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code  Where Terminal='" & Session("HostID") & "' " & _
                "  -- Group By O.Sch_Code,S.Sch_Name,S.Sch_Section_Name"
        Else
            Sql_String = "select (O.Acc_No) As AccNo,c.Cust_Name,(NOD_IntAmt+OD_IntAmt) As Accrued ,S.Sch_Name,S.Sch_Section_Name from Loan_Outstand_R_Tbl O " & _
                " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code join Cust_Master_M_Tbl c on O.Cust_Id=c.Cust_Id Where (NOD_IntAmt+OD_IntAmt) > 0 and Terminal='" & Session("HostID") & "' " & _
                " --Group By O.Sch_Code,S.Sch_Name,S.Sch_Section_Name " & _
                " /*Union All " & _
                " Select (O.Acc_No) As AccNo,(Accrued) As Accrued ,S.Sch_Name,S.Sch_Section_Name from  Investment_Outstand_R_Tbl O " & _
                " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code " & _
                " Where Accrued > 0 And  Terminal='" & Session("HostID") & "' " & _
                " --Group By O.Sch_Code,S.Sch_Name,S.Sch_Section_Name */" & _
                " Union All " & _
                " Select (O.SecNo) As AccNo,(Accrued_Int) As Accrued ,S.Sch_Name,S.Sch_Section_Name from  GovtSec_Outstand_R_Tbl O " & _
                " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code " & _
                " Where Accrued_Int > 0 And  Terminal='" & Session("HostID") & "' " & _
                " --Group By O.Sch_Code,S.Sch_Name,S.Sch_Section_Name"
        End If
        M_DBConn = Nothing
        Session.Add("RptQry", Sql_String)
        StrSlf = "Company|" & Company_Name & "|Branch|" & IIf(DdlBranch.SelectedItem.Text <> "-- ALL --", DdlBranch.SelectedItem.Text, "Consolidation") & "|Heading|Interest : " & IIf(DdlRptType.SelectedItem.Text = "Payable", "Payable", "Receivable") & " AS ON  " & ClsComm.DbDate(DtFrom.Text) & " |User_ID|" & Session("LoginID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
        Session.Add("RptPath", Server.MapPath("~/Reports/IntReceivable_Payable.rpt" & Rpt_Path))
        Session.Add("RptFormula", StrSlf)
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub cmbprocess_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmbprocess.Click
        Try

            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()
            Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
            Dim Br_Code As String
            Dim Br As Integer
            Dim Str_Flf, Sql_String As String
            M_DBConn = New Connection()

            Started()
            '    If Scmb_Branch.ArrayValue1(Scmb_Branch.ListIndex) <> "" Then Br_Code = Scmb_Branch.ArrayValue1(Scmb_Branch.ListIndex)
            M_DBConn.ExecuteDataTable(M_Dt, "Delete From Deposit_Outstand_R_Tbl Where Terminal='" & Session("HostID") & "'", False)
            M_DBConn.ExecuteDataTable(M_Dt, "Delete From Loan_Outstand_R_Tbl Where Terminal='" & Session("HostID") & "'", False)
            M_DBConn.ExecuteDataTable(M_Dt, "Delete From Loan_Outstand_R_Tbl Where Terminal='" & Session("HostID") & "'", False)
            ' M_DBConn.ExecuteDataTable(M_Dt, "Delete From  Investment_Outstand_R_Tbl Where Terminal='" & Session("HostID") & "'", False)
            M_DBConn.ExecuteDataTable(M_Dt, "Delete From GovtSec_Outstand_R_Tbl Where Terminal='" & Session("HostID") & "'", False)

            For Br = 0 To DdlBranch.Items.Count - 1

                If DdlBranch.SelectedItem.Text = "-- ALL --" Then
                    Br_Code = DdlBranch.Items(Br).Value
                Else
                    Br_Code = DdlBranch.SelectedValue.ToString()
                End If



                Sql_String = "Select Sch_Code,Sch_Name,Sch_Section_Name From Gen_SchemeMaster_P_Tbl Where Sch_Section_Name In ("
                If DdlRptType.SelectedItem.Text = "Payable" Then
                    Sql_String = Sql_String & "'DEPOSIT') And Sch_Type1='TERM1' "
                Else
                    Sql_String = Sql_String & "'LOAN','INVESTMENT','GOVT SECURITY')"
                End If
                M_DBConn.ExecuteDataTable(M_Dt, Sql_String, False)


                For i = 0 To M_Dt.Rows.Count - 1



                    Lbl_Status.Text = M_Dt.Rows(i)(1).ToString()


                    If M_Dt.Rows(i)(2).ToString() = "LOAN" Then

                        Sql_String = "Exec [dbo].[Process_LoanAccrued_Sp] '" & Br_Code & "', 'S','" & M_Dt.Rows(i)(0).ToString() & "' ,"
                        Sql_String = Sql_String + "''," + ClsComm.DbDate(DtFrom.Text) & "," + ClsComm.DbDate(DtFrom.Text) & ",'N','Y','Y','Y','" & Session("HostID") & "','" & Session("LoginID") & "'"
                        M_DBConn.ExecuteScalar(Sql_String, Connection.SQLSource.STOREDPROCEDURE, False)

                    ElseIf M_Dt.Rows(i)(2).ToString() = "INVESTMENT" Then

                        '            MainConnection.Execute "Exec Investment_Outstand_Sp  'N', 'S','" & Rst!Sch_Code & "','" & Br_Code & "','" + Format(Dtp_DemDate, "MM/dd/yyyy") & "','" & User_Id & "','N'"
                        '            Call Cls_Out.Update_InvestInterest(Rst!Sch_Code, Dtp_DemDate.Value, Lbl_StatusNo)
                        ' Sql_String = "Exec Investment_Outstand_Sp  'N', 'S','" & M_Dt.Rows(i)(0).ToString() & "','" & Br_Code & "'," + ClsComm.DbDate(DtFrom.Text) & ",'" & Session("LoginID") & "', 'N','',"
                        ' Sql_String = Sql_String & "'Y','" & Session("HostID") & "','Y'"
                        ' M_DBConn.ExecuteScalar(Sql_String, Connection.SQLSource.STOREDPROCEDURE, False)

                    ElseIf M_Dt.Rows(i)(2).ToString() = "DEPOSIT" Then

                        Sql_String = "Exec [Deposit_Outstand_Sp] 'N','" & Br_Code & "', 'S','" & M_Dt.Rows(i)(0).ToString() & "',''," & ClsComm.DbDate(DtFrom.Text) & ",'Y','" & Session("LoginID") & "','" & Session("HostID") & "','N' "
                        M_DBConn.ExecuteScalar(Sql_String, Connection.SQLSource.STOREDPROCEDURE, False)
                        'Call Cls_Out.Update_DepInterest(Rst!Sch_Code, Dtp_DemDate.Value, Lbl_StatusNo)

                    ElseIf M_Dt.Rows(i)(2).ToString() = "GOVT SECURITY" Then
                        Sql_String = "Exec GovtSec_Outstand_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & _
                            Br_Code & "','" & _
                            M_Dt.Rows(i)(0).ToString() & "','" & Session("HostID") & "','" & _
                            Session("LoginID") & "','Y','','N','N'"
                        M_DBConn.ExecuteScalar(Sql_String, Connection.SQLSource.STOREDPROCEDURE, False)
                    End If



                Next
                If DdlBranch.SelectedItem.Text <> "-- ALL --" Then
                    Exit For
                End If

            Next Br
            If M_Dt.Rows.Count > 0 Then
                Completed()
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Completed()

        Lbl_Status.Text = "Process Completed......"
        CmdShow.Visible = True

    End Sub
    Public Sub Started()
        Lbl_Status.Text = "  Process Started......"
        CmdShow.Visible = False
    End Sub
End Class