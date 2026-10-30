Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Imports System.Data

Partial Public Class DayEndReports
    Inherits System.Web.UI.Page

    Private Shared Common As New CommonBL
    Private Shared ClsCommon As New CommonClass
    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 1045
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then

            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            Call FillCombo()
        End If
    End Sub

    Protected Sub BtnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnProcess.Click
        Dim SqlStr As String

        ' Clear Report Table
        SqlStr = "DELETE FROM De_Details_R_Tbl WHERE Terminal = '" & Session("HostID") & "'"

        M_DBConn = New Connection()

        M_DBConn.ExecuteScalar(SqlStr, , False)

        Call Process_DayEnd_Detailed()

    End Sub

    Public Sub FillCombo()
        Common.Fill_Branch(DdlBranch)
        DdlBranch.Items.Add(" <--- ALL ---> ")
        DdlBranch.SelectedIndex = 0
    End Sub


    Private Sub Process_DayEnd_Detailed()
        On Error GoTo Err_handler_Process
        Dim I As Integer
        Dim SqlStr As String
        Dim I_SchCount As Integer
        Dim IsFirstSch As String
        Dim Br As Integer
        Dim M_Dt As DataTable = New DataTable()

        SqlStr = "Delete From Tmp_RandD_P_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Temp_Subsidiary_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Deposit_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Loan_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Share_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Borrowing_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Investment_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From GovtSec_Outstand_R_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)
        SqlStr = "Delete From Suspense_Outstanding_Tbl Where Terminal ='" & Session("HostID") & "'"
        M_DBConn.ExecuteScalar(SqlStr, , False)

        SqlStr = "select Sch_Code,Sch_Name,Sch_Section_Name from Gen_SchemeMaster_P_Tbl Where Sch_Section_Name In ('BORROWINGS','DEPOSIT','GROUP DEPOSIT','INVESTMENT','LOAN','SHARE','SUSPENSE')"
        M_DBConn.ExecuteDataTable(M_Dt, SqlStr, False)

        For Br = 1 To DdlBranch.Items.Count - 1
            If DdlBranch.SelectedValue <> "" Then Br = DdlBranch.SelectedIndex

            ' RandD between Month Start Date and AsonDate
            LblProgress.Text = DdlBranch.Items(Br).Value.ToString() & " - " & "RandD"

            SqlStr = "Exec Acc_Get_RandD '" & CDate(TxtAsonDate.Text).Month & "/01/" & CDate(TxtAsonDate.Text).Year & "'," & ClsCommon.DbDate(TxtAsonDate.Text) & _
            ",'" & Session("LoginID") & "','" & DdlBranch.Items(Br).Value.ToString() & "','N','%','N'"
            M_DBConn.ExecuteScalar(SqlStr, , False)
            LblProgress.Text = DdlBranch.Items(Br).Value.ToString() & " - " & "Daybook"

            ' Daybook / Subsidiary On AsonDate
            SqlStr = "Exec Gen_Subsidiary_Sp '','" & DdlBranch.Items(Br).Value.ToString() & "'," & _
                ClsCommon.DbDate(TxtAsonDate.Text) & "," & ClsCommon.DbDate(TxtAsonDate.Text) & ",'','" & Session("LoginID") & "','" & Session("HostID") & "'"
            M_DBConn.ExecuteScalar(SqlStr, , False)
            LblProgress.Text = ""

            For I = 0 To M_Dt.Rows.Count - 1

                LblProgress.Text = DdlBranch.Items(Br).Value.ToString() & " - " & M_Dt.Rows(I)("Sch_Name").ToString


                Select Case M_Dt.Rows(I)("Sch_Section_Name").ToString
                    Case "DEPOSIT"
                        SqlStr = "Exec Deposit_Outstand_Sp 'N','" & DdlBranch.Items(Br).Value.ToString() & _
                            "','S','" & M_Dt.Rows(I)("Sch_Code").ToString & "',''," & ClsCommon.DbDate(TxtAsonDate.Text) & ",'N','" & Session("LoginID") & "','" & Session("HostID") & "','N'"
                    Case "LOAN"
                        SqlStr = "Exec [dbo].[Process_LoanAccrued_Sp] '" & DdlBranch.Items(Br).Value.ToString() & "', 'S',"
                        SqlStr = SqlStr + "'" & M_Dt.Rows(I)("Sch_Code").ToString & "' ,"
                        SqlStr = SqlStr + "''," + ClsCommon.DbDate(TxtAsonDate.Text) & "," + ClsCommon.DbDate(TxtAsonDate.Text) & ",'N','Y','Y','Y','" & Session("HostID") & "','" & Session("LoginID") & "'"
                    Case "SHARE"
                        SqlStr = "Exec Share_Outstand_Sp 'N','" & M_Dt.Rows(I)("Sch_Code").ToString & "','" & _
                            DdlBranch.Items(Br).Value.ToString() & "'," + ClsCommon.DbDate(TxtAsonDate.Text) & ",'" & Session("LoginID") & "'"
                    Case "INVESTMENT"
                        SqlStr = "Exec Investment_Outstand_Sp  'N', 'S','" & M_Dt.Rows(I)("Sch_Code").ToString & "','" & DdlBranch.Items(Br).Value.ToString() & "'," + ClsCommon.DbDate(TxtAsonDate.Text) & ",'" & Session("LoginID") & "', 'N'"
                    Case "BORROWINGS"
                        SqlStr = "Exec Borrowing_Outstand_Sp 'N', 'S','" & M_Dt.Rows(I)("Sch_Code").ToString & "','" & DdlBranch.Items(Br).Value.ToString() & "'," + ClsCommon.DbDate(TxtAsonDate.Text) & ",'" & Session("LoginID") & "','N','N'"
                    Case "SUSPENSE"
                        SqlStr = "EXEC Suspense_Outstanding_Pro '" & DdlBranch.Items(Br).Value.ToString() & "','" & M_Dt.Rows(I)("Sch_Code").ToString & "'," + ClsCommon.DbDate(TxtAsonDate.Text) & ",'" & Session("HostID") & "','N'"
                    Case "GOVT SECURITY"
                        SqlStr = "Exec GovtSec_Outstand_Sp " & ClsCommon.DbDate(TxtAsonDate.Text) & ",'" & _
                            DdlBranch.Items(Br).Value.ToString() & "','" & _
                            M_Dt.Rows(I)("Sch_Code").ToString & "','" & Session("HostID") & "','" & _
                            Session("LoginID") & "','N','','N','N'"
                    Case "GROUP DEPOSIT" ' To Do
                        SqlStr = ""
                    Case "LOCKER" ' To Do
                        SqlStr = ""
                    Case Else
                        SqlStr = ""
                End Select

                If Len(SqlStr) > 0 Then M_DBConn.ExecuteScalar(SqlStr, , False)
            Next I

            If DdlBranch.SelectedValue <> "" Then Exit For
        Next Br
        LblProgress.Text = ""

        Exit Sub

Err_handler_Process:

    End Sub

    Private Sub Show_DayEndRpt_Detailed()
        Try
            Dim Rpt_Name, SubRpt_Name, Str_Flf As String
            Dim Str_FormatedFlf As String
            Dim DayOpCash As Double
            Dim MonthOpCash As Double
            Dim YearOpCash As Double
            Dim ClCash As Double
            Dim YearOpDate As Date
            Dim SubRptQry(50) As String
            Dim SqlStr As String
            Dim M_Dt As DataTable = New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA


            Dim Company_Name As String = M_CommonDa.GetCompanyDetails()

            ' Cash Balance
            SqlStr = "Select Dt_OpCashBalance,Dt_CashBalance From Gen_TranDateMaster_M_Tbl Where Dt_Transaction_Date=" & _
                ClsCommon.DbDate(TxtAsonDate.Text) & " "
            If DdlBranch.SelectedValue <> "" Then
                SqlStr = SqlStr & " And Br_Code='" & DdlBranch.SelectedValue & "'"
            End If

            M_DBConn.ExecuteDataTable(M_Dt, SqlStr, False)
            If M_Dt.Rows.Count > 0 Then
                DayOpCash = M_Dt.Rows(0)("Dt_OpCashBalance")
                ClCash = M_Dt.Rows(0)("Dt_CashBalance")
            End If

            SqlStr = "Select Top 1 Dt_OpCashBalance,Dt_CashBalance From Gen_TranDateMaster_M_Tbl Where Dt_Transaction_Date<" & _
                "'" & CDate(TxtAsonDate.Text).Month & "/01/" & CDate(TxtAsonDate.Text).Year & "' "
            If DdlBranch.SelectedValue <> "" Then
                SqlStr = SqlStr & " And Br_Code='" & DdlBranch.SelectedValue & "'"
            End If
            SqlStr = SqlStr & " Order By Dt_Transaction_Date Desc"

            M_DBConn.ExecuteDataTable(M_Dt, SqlStr, False)
            If M_Dt.Rows.Count > 0 Then
                MonthOpCash = M_Dt.Rows(0)("Dt_CashBalance")
            End If

            If CDate(TxtAsonDate.Text).Month >= 4 Then
                YearOpDate = "01/04/" & CDate(TxtAsonDate.Text).Year
            Else
                YearOpDate = "01/04/" & CDate(TxtAsonDate.Text).Year - 1
            End If

            SqlStr = "Select Top 1 Dt_OpCashBalance,Dt_CashBalance From Gen_TranDateMaster_M_Tbl Where Dt_Transaction_Date<" & _
                ClsCommon.DbDate(TxtAsonDate.Text)
            If DdlBranch.SelectedValue <> "" Then
                SqlStr = SqlStr & " And Br_Code='" & DdlBranch.SelectedValue & "'"
            End If
            SqlStr = SqlStr & " Order By Dt_Transaction_Date Desc"

            M_DBConn.ExecuteDataTable(M_Dt, SqlStr, False)
            If M_Dt.Rows.Count > 0 Then
                YearOpCash = M_Dt.Rows(0)("Dt_CashBalance")
            End If

            ' Share
            SqlStr = "Select A.Acc_Code As Sch_Code,A.Acc_Name As Sch_Name,COUNT(O.Mem_No) As OutNo,Isnull(Sum(O.Share_Capital),0) As OutAmt,Isnull(Min(R.ClCr-R.ClDr),0) As GlBal" & Chr(13) & _
                " From Share_Outstand_R_Tbl O " & Chr(13) & _
                " Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code " & Chr(13) & _
                " Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=S.Acc_Code " & Chr(13) & _
                " Left Outer Join Tmp_RandD_P_Tbl R On R.Acc_Code=O.Acc_Code And R.Terminal=O.Terminal " & Chr(13) & _
                " Where O.Terminal='" & Session("HostID") & "' " & Chr(13) & _
                " Group By A.Acc_Code,A.Acc_Name Order By A.Acc_Code "

            ' Deposit
            SubRptQry(1) = "Select A.Acc_Code As Sch_Code,A.Acc_Name As Sch_Name,COUNT(O.Acc_No) As OutNo, " & Chr(13) & _
                "    Isnull(Sum(O.Outstanding*Case When O.Bal_Type='C' Then 1 Else -1 End),0) As OutAmt, " & Chr(13) & _
                "    Isnull(Min(R.ClCr-R.ClDr),0) As GlBal, " & Chr(13) & _
                "    Sum(Case When M.Dm_DueDate<" & ClsCommon.DbDate(TxtAsonDate.Text) & " And Isnull(S.Sch_Type1,'')='TERM1' Then 1 Else 0 End) As MatNo, " & Chr(13) & _
                "    Sum(Case When M.Dm_DueDate<" & ClsCommon.DbDate(TxtAsonDate.Text) & " AND M.Dm_WithEffDate<M.Dm_DueDate And Isnull(S.Sch_Type1,'')='TERM1' Then O.Outstanding Else 0 End) As MatAmt, " & Chr(13) & _
                "    Max(Case When Isnull(S.Sch_Type1,'')='' Then 'TERM1' Else S.Sch_Type1 End) As TERMType " & Chr(13) & _
                "From Deposit_Outstand_R_Tbl O " & Chr(13) & _
                "Join Dep_Master_M_Tbl M On M.Br_Code=O.Br_Code And M.Acc_No=O.Acc_No And M.Sch_Code=O.Sch_Code " & Chr(13) & _
                "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code " & Chr(13) & _
                " Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=S.Acc_Code " & Chr(13) & _
                "Left Outer Join Tmp_RandD_P_Tbl R On R.Acc_Code=O.Acc_Code And R.Terminal=O.Terminal " & Chr(13) & _
                "Where O.Terminal='" & Session("HostID") & "' " & Chr(13) & _
                "Group By A.Acc_Code,A.Acc_Name Order By A.Acc_Code"

            'Loan
            SubRptQry(0) = "Select A.Acc_Code As Sch_Code,A.Acc_Name As Sch_Name,COUNT(O.Acc_No) As OutNo," & Chr(13) & _
                "    Isnull(Sum(O.Outstanding*Case When O.BalType='D' Then 1 Else -1 End),0) As OutAmt," & Chr(13) & _
                "    Isnull(Min(R.ClDr-R.ClCr),0) As GlBal," & Chr(13) & _
                "    Sum(Case When O.OD_Amt>0 Then 1 Else 0 End) As OD_No," & Chr(13) & _
                "    Isnull(Sum(O.OD_Amt), 0) As OD_Amt " & Chr(13) & _
                "From Loan_Outstand_R_Tbl O " & Chr(13) & _
                "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code " & Chr(13) & _
                " Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=S.Acc_Code " & Chr(13) & _
                "Left Outer Join Tmp_RandD_P_Tbl R On R.Acc_Code=O.Acc_Code And R.Terminal=O.Terminal " & Chr(13) & _
                "Where O.Terminal='" & Session("HostID") & "' " & Chr(13) & _
                "Group By A.Acc_Code,A.Acc_Name Order By A.Acc_Code"

            'Suspense
            SubRptQry(2) = "Select A.Acc_Code As Sch_Code,A.Acc_Name As Sch_Name,COUNT(O.Acc_No) As OutNo," & Chr(13) & _
                "    Isnull(Sum(O.Balance*Case When O.BalType='Dr' Then -1 Else 1 End),0) As OutAmt," & Chr(13) & _
                "    Isnull(Min(R.ClCr-R.ClDr), 0) As GlBal " & Chr(13) & _
                "From Suspense_Outstanding_Tbl O " & Chr(13) & _
                "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code" & Chr(13) & _
                " Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=S.Acc_Code " & Chr(13) & _
                "Left Outer Join Tmp_RandD_P_Tbl R On R.Acc_Code=S.Acc_Code And R.Terminal=O.Terminal " & Chr(13) & _
                "Where O.Terminal='" & Session("HostID") & "' " & Chr(13) & _
                "Group By A.Acc_Code,A.Acc_Name Order By A.Acc_Code"

            'Investment
            SubRptQry(3) = "Select A.Acc_Code As Sch_Code,A.Acc_Name As Sch_Name,COUNT(O.Acc_No) As OutNo," & Chr(13) & _
                "    Isnull(Sum(O.Outstanding*Case When O.Bal_Type='D' Then 1 Else -1 End),0) As OutAmt," & Chr(13) & _
                "    Isnull(Min(R.ClDr - R.ClCr), 0) As GlBal " & Chr(13) & _
                "From Investment_Outstand_R_Tbl O " & Chr(13) & _
                "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code" & Chr(13) & _
                " Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=S.Acc_Code " & Chr(13) & _
                "Left Outer Join Tmp_RandD_P_Tbl R On R.Acc_Code=O.Acc_Code And R.Terminal=O.Terminal " & Chr(13) & _
                "Where O.Terminal='" & Session("HostID") & "' " & Chr(13) & _
                "Group By A.Acc_Code,A.Acc_Name Order By A.Acc_Code"

            'Borrowings
            SubRptQry(4) = "Select A.Acc_Code As Sch_Code,A.Acc_Name As Sch_Name,COUNT(O.Acc_No) As OutNo," & Chr(13) & _
                "    Isnull(Sum(O.Outstanding*Case When O.BalType='C' Then 1 Else -1 End),0) As OutAmt," & Chr(13) & _
                "    Isnull(Min(R.ClCr - R.ClDr), 0) As GlBal " & Chr(13) & _
                "From Borrowing_Outstand_R_Tbl O " & Chr(13) & _
                "Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=O.Sch_Code " & Chr(13) & _
                " Join Gen_Accountmaster_P_Tbl A On A.Acc_Code=S.Acc_Code " & Chr(13) & _
                "Left Outer Join Tmp_RandD_P_Tbl R On R.Acc_Code=O.Acc_Code And R.Terminal=O.Terminal " & Chr(13) & _
                "Where O.Terminal='" & Session("HostID") & "' " & Chr(13) & _
                "Group By A.Acc_Code,A.Acc_Name Order By A.Acc_Code"

            'Daybook
            SubRptQry(5) = "Select Isnull(Sum(Case When Tran_Type='C' Then Cash Else 0 End),0) As Cash_Rec," & Chr(13) & _
                "        Isnull(Sum(Case When Tran_Type='C' Then Transfer Else 0 End),0) As Adj_Rec," & Chr(13) & _
                "        Isnull(Sum(Case When Tran_Type='D' Then Cash Else 0 End),0) As Cash_Pay," & Chr(13) & _
                "        Isnull(Sum(Case When Tran_Type='D' Then Transfer Else 0 End),0) As Adj_Pay " & Chr(13) & _
                "From Temp_Subsidiary_R_Tbl Where Terminal='" & Session("HostID") & "'"

            'RandD
            SubRptQry(6) = "Select Isnull(Sum(CurRec),0) As CurRec," & Chr(13) & _
                "        Isnull(Sum(CurPay),0) As CurPay," & Chr(13) & _
                "        Isnull(Sum(R.OpUptoRec+CurRec),0) As Prog_Rec," & Chr(13) & _
                "        Isnull(Sum(R.OpUptoPay + CurPay), 0) As Prog_Pay " & Chr(13) & _
                "From Tmp_RandD_P_Tbl R Where Terminal='" & Session("HostID") & "'"

            Str_Flf = "Company|" & Company_Name & "|Branch|" & DdlBranch.SelectedItem.Text & "|Heading|Day End Report For " & TxtAsonDate.Text
            Str_FormatedFlf = "DayOpCash|" & DayOpCash & "|ClCash|" & ClCash & "|MonthOpCash|" & MonthOpCash & "|YearOpCash|" & YearOpCash

            SubRpt_Name = "Loan|Deposit|Suspense|Investment|Borrowings|Daybook|RandD"

            Str_Flf = Str_Flf + "|Footer1|Clerk|Footer2|Accountant|Footer3|Manager|User_ID|" & Session("LoginID")

            Session.Add("RptFormula", Str_Flf)
            Session.Add("RptQry", SqlStr)
            Session.Add("SubRptNames", SubRpt_Name)
            Session.Add("SubRptQry", SubRptQry)
            Session.Add("RptPath", Server.MapPath("~/Reports/DayEndReport/DayEndReport_Detail.rpt"))
            Response.Redirect("~/RptView.aspx", False)

            M_CommonDa = Nothing

            Exit Sub

ErrHandler:
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



    Protected Sub BtnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnShow.Click
        Call Show_DayEndRpt_Detailed()
    End Sub
End Class