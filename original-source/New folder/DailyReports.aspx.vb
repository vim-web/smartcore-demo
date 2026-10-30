Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common


Partial Public Class DailyReports
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1043
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/SmartLogin.aspx")
            End If
            If Not IsPostBack Then
                '  User(Rights)
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1
                Load_DefaultValues()
                Chk_BrWise.Visible = False
                Chk_RecPay.Visible = False
                Chk_Pay.Visible = False
                Chk_Rec.Visible = False
                Chk_Grp.Visible = False
                Chk_BalHeads.Visible = False

                If Opt_CashScroll.Checked = True Then
                    Chck_Excel.Visible = True
                Else
                    Chck_Excel.Visible = False
                End If

                If Session("DAILY_RPTS_OPTION_BTN") IsNot Nothing Then

                    Opt_CashScroll.Checked = False

                    Select Case Session("DAILY_RPTS_OPTION_BTN")
                        Case "DB"
                            Opt_DayBook.Checked = True
                        Case "CS"
                            Opt_CashScroll.Checked = True
                        Case "IB"
                            Opt_IB.Checked = True
                        Case "GL"
                            Opt_GL.Checked = True
                        Case "TL"
                            Opt_TransferList.Checked = True
                        Case "RB"
                            Opt_RctPay.Checked = True
                        Case "DE"
                            Opt_DayEnd.Checked = True
                        Case "IBV"
                            Opt_IBVOU.Checked = True
                    End Select

                    Set_Controls()
                    Txt_DateFrom.Text = Session("DAILY_RPTS_FROM_DATE")
                    Txt_DateTo.Text = Session("DAILY_RPTS_TO_DATE")
                    Ddl_Branch.SelectedValue = Session("DAILY_RPTS_BRANCH").ToString()
                    Ddl_Branch.SelectedItem.Text = Ddl_Branch.SelectedItem.Text
                    'Ddl_Branch.Text = Ddl_Branch.SelectedItem.Text


                End If


            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            M_CommonBL.Fill_UserWiseBranch(Ddl_Branch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            ''Region Wise
            Ddl_Branch.Items.Remove(Ddl_Branch.Items.FindByText("ALL"))
            Ddl_Branch.Items.Add("ALL")
            Txt_DateFrom.Text = Session("TransDate")
            Txt_DateTo.Text = Session("TransDate")

            M_CommonBL.Fill_AccHead(CmbGL)
            CmbGL.Items.Add("ALL")
            Ddl_Branch.SelectedValue = Session("DefaultBrCode")

            CmbIB.Items.Add("ALL")
            CmbIB.Items.Add("DEBIT")
            CmbIB.Items.Add("CREDIT")

            CmbIB.SelectedValue = 0
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub




    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA
            Dim Company_Name As String
            Dim StrSlf As String
            Dim OpCash As Double
            Dim ClCash As Double
            Dim Dt As DataTable

            M_DBConn = New Connection()

            Company_Name = HttpContext.Current.Cache("CompanyName")

            Dt = M_CommonDa.GetOpeningBalance(ClsComm.DbDate(Txt_DateFrom.Text, True), Ddl_Branch.SelectedValue)
            If Dt.Rows.Count > 0 Then
                OpCash = Convert.ToDouble(Dt.Rows(0)("Dt_OpCashBalance").ToString)
                ClCash = Convert.ToDouble(Dt.Rows(0)("Dt_CashBalance").ToString)
            Else
                OpCash = 0
                ClCash = 0
            End If
            Dim Terminal = System.Environment.MachineName 'Request.ServerVariables("REMOTE_HOST")


            Session("DAILY_RPTS_FROM_DATE") = Txt_DateFrom.Text
            Session("DAILY_RPTS_TO_DATE") = Txt_DateTo.Text
            Session("DAILY_RPTS_BRANCH") = Ddl_Branch.SelectedValue.ToString()



            If Opt_DayBook.Checked = True Then

                Session("DAILY_RPTS_OPTION_BTN") = "DB"

                M_Sql = "Exec DayBookRpt_Sp'" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text) & ",'" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                Session("RptQry") = M_Sql
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & Ddl_Branch.SelectedItem.ToString & "|Heading|Day Book As on " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                Session.Add("RptFormula", StrSlf)
                Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/DayBook.rpt"))
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

                Exit Sub


                Dim HeaderArr(9, 4) As String
                Dim HeaderWidth(9, 1) As String
                HeaderArr(0, 0) = "VR NO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "6"

                HeaderArr(1, 0) = "PARTICULARS"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "50"
                HeaderArr(1, 3) = "31"

                HeaderArr(2, 0) = "TRANSFER"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "20"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "CASH"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "20"
                HeaderArr(3, 3) = "13"

                HeaderArr(4, 0) = "TOTAL"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "20"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "VR NO"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "6"

                HeaderArr(6, 0) = "PARTICULARS"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "50"
                HeaderArr(6, 3) = "31"

                HeaderArr(7, 0) = "TRANSFER"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "20"
                HeaderArr(7, 3) = "13"

                HeaderArr(8, 0) = "CASH"
                HeaderArr(8, 1) = "R"
                HeaderArr(8, 2) = "20"
                HeaderArr(8, 3) = "13"

                HeaderArr(9, 0) = "TOTAL"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "20"
                HeaderArr(9, 3) = "13"

                Session("RptHeader") = HeaderArr



                M_Sql = "EXEC DayBookDtls_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text) & "," & ClsComm.DbDate(Txt_DateFrom.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','N'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub

                M_Sql = "EXEC DayBookDetail_sub_sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text) & "," & ClsComm.DbDate(Txt_DateFrom.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','N'"

                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

                M_Sql = ""

                M_Sql = "SELECT A.Acc_Name, D.*  FROM         DayBookDetail_R_Tbl AS D "
                M_Sql = M_Sql & " INNER JOIN  Gen_Accountmaster_P_Tbl AS A ON A.Acc_Code = D.Acc_Code INNER JOIN  Gen_AccountGrp_P_Tbl AS G"
                M_Sql = M_Sql & " ON A.Acc_Grp_Code = G.Grp_Code  Where UserID = '" & Session("UserID") & "' And Terminal='" & Session("HostID") & "'  ORDER BY G.Grp_SeqNo, "
                M_Sql = M_Sql & " A.Acc_SeqNo,Cast(D.RVr_No As Float), Cast(D.PVr_No As Float)"

                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

                Session.Add("RptQry", M_Sql)
                'Session.Add("RptPath", Session("File_Path") & "\" & "Reports\Accounts\DBDetailed_Sub.rpt")
                Session("RptPath") = Server.MapPath("~/Reports/Accounts/DBDetailed_Sub.rpt")



                StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|D  A  Y     B  O  O  K     Dated : " & Txt_DateFrom.Text & " |User_ID| " & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
                StrSlf = StrSlf + "|OpeningCash|" & Format(OpCash, ".00") & ""
                StrSlf = StrSlf + "|ClosingCash|" & Format(ClCash, ".00") & ""



            ElseIf Opt_IB.Checked = True Then

                Session("DAILY_RPTS_OPTION_BTN") = "IB"
               
                Dim HeaderArr(4, 4) As String
                Dim HeaderWidth(4, 1) As String
                HeaderArr(0, 0) = "BRANCH"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "20"
                HeaderArr(0, 3) = "15"

                HeaderArr(1, 0) = "PARTICULARS"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "40"
                HeaderArr(1, 3) = "40"

                HeaderArr(2, 0) = "AMOUNT"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "20"
                HeaderArr(2, 3) = "13"

                Session("RptHeader") = HeaderArr

                M_Sql = "Exec IBTransactionList_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text) & ",'" & Mid(CmbIB.SelectedItem.Text, 1, 1) & "','" & Session("UserID") & "','" & Session("HostID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
                Exit Sub
            ElseIf Opt_CashScroll.Checked = True Then

                Session("DAILY_RPTS_OPTION_BTN") = "CS"

                If Chck_Excel.Checked = True Then
                    Dim HeaderArr(6, 4) As String
                    Dim HeaderWidth(6, 1) As String

                    HeaderArr(0, 0) = "VR NO"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "10"
                    HeaderArr(0, 3) = "6"

                    HeaderArr(1, 0) = "DATE"
                    HeaderArr(1, 1) = "L"
                    HeaderArr(1, 2) = "20"
                    HeaderArr(1, 3) = "10"

                    HeaderArr(2, 0) = "PARTICULARS"
                    HeaderArr(2, 1) = "L"
                    HeaderArr(2, 2) = "50"
                    HeaderArr(2, 3) = "30"

                    HeaderArr(3, 0) = "RECEIPT"
                    HeaderArr(3, 1) = "L"
                    HeaderArr(3, 2) = "20"
                    HeaderArr(3, 3) = "15"

                    HeaderArr(4, 0) = "PAYMENT"
                    HeaderArr(4, 1) = "L"
                    HeaderArr(4, 2) = "20"
                    HeaderArr(4, 3) = "13"





                    Session("RptHeader") = HeaderArr



                    M_Sql = "EXEC CashBook_Sp_Excel " & ClsComm.DbDate(Txt_DateFrom.Text) & "," & ClsComm.DbDate(Txt_DateTo.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                    Session("RptQry") = M_Sql
                    Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Else


                    M_Sql = "Exec CashBook_Sp " & ClsComm.DbDate(Txt_DateFrom.Text) & "," & ClsComm.DbDate(Txt_DateTo.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                    Session("RptQry") = M_Sql
                    StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & Ddl_Branch.SelectedItem.ToString & "|Heading|Cash Book As on " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")
                    StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                    Session.Add("RptFormula", StrSlf)
                    Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/CashBook.rpt"))
                    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
                End If
                Exit Sub

                M_Sql = "EXEC Gen_Subsidiary_Sp '','" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text) & "," & ClsComm.DbDate(Txt_DateFrom.Text) & ",'','" & Session("UserID") & "','" & Session("HostID") & "'"

                M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)


                M_Sql = ""


                M_Sql = "SELECT MAX(S.Acc_Code) AS Acc_Code, MAX(A.Acc_Name) AS Acc_Name, TR_DATE,Vr_No, Case when Tran_Function In ('P','AG') Then Short_Name + ' ' + Max(Narration)"
                M_Sql = M_Sql & " Else Short_Name + ' ' + Max(Particulars) End As Particulars ,Tran_Type,Sum(Cash) Cash ,"
                M_Sql = M_Sql & " Max(Dt_OpCashBalance) As Dt_OpCashBalance,dbo.Gen_GetUserName_Fn (Max(Maker_ID)) As Maker_ID, dbo.Gen_GetUserName_Fn (Max(Checker_ID)) As Checker_ID "
                M_Sql = M_Sql & " FROM   Temp_Subsidiary_R_Tbl S  Left Outer Join Gen_TranDateMaster_M_Tbl T On T.Dt_Transaction_Date=S.TR_DATE  and T.br_code='" & Ddl_Branch.SelectedValue & "' "
                M_Sql = M_Sql & " Inner Join Gen_Accountmaster_P_Tbl A ON S.Acc_Code = A.Acc_Code Inner Join Gen_AccountGrp_P_Tbl G ON A.Acc_Grp_Code = G.Grp_Code"
                M_Sql = M_Sql & "   WHERE  Cash > 0 And TR_DATE = " & ClsComm.DbDate(Txt_DateFrom.Text) & " AND UserId ='" & Session("UserID") & "' AND Terminal ='" & Session("HostID") & "'  Group By  TR_DATE,Vr_No,Tran_Type,"
                M_Sql = M_Sql & " Tran_Function, Short_Name, /*S.Acc_No,*/(case when Tran_Function IN('P','AG') "
                M_Sql = M_Sql & " THEN '' ELSE Acc_No END) ORDER BY Right('0000000000000000000'+Vr_No,18) "

                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

                Session.Add("RptQry", M_Sql)
                Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/Cashier_Scroll.rpt"))



                StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Cash Scroll As on: " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
                StrSlf = StrSlf + "|OP_CashBal|" & OpCash & ""
                StrSlf = StrSlf + "|OP_CashBal|" & OpCash & ""
            ElseIf Opt_GL.Checked = True Then

                Dim AccCode As String = CmbGL.SelectedValue
                If CmbGL.SelectedValue = "ALL" Then AccCode = "0"

                Session("DAILY_RPTS_OPTION_BTN") = "GL"

                'If Ddl_Branch.SelectedValue = "ALL" Then
                '    Dim HeaderArr(8, 4) As String
                '    Dim HeaderWidth(8, 1) As String

                '    HeaderArr(0, 0) = "BRANCH"
                '    HeaderArr(0, 1) = "L"
                '    HeaderArr(0, 2) = "8"
                '    HeaderArr(0, 3) = "8"

                '    HeaderArr(1, 0) = "PARTICULARS"
                '    HeaderArr(1, 1) = "L"
                '    HeaderArr(1, 2) = "40"
                '    HeaderArr(1, 3) = "40"

                '    HeaderArr(2, 0) = "DATE"
                '    HeaderArr(2, 1) = "L"
                '    HeaderArr(2, 2) = "10"
                '    HeaderArr(2, 3) = "10"

                '    HeaderArr(3, 0) = "RECEIPT"
                '    HeaderArr(3, 1) = "R"
                '    HeaderArr(3, 2) = "15"
                '    HeaderArr(3, 3) = "13"

                '    HeaderArr(4, 0) = "PROG RECEIPT"
                '    HeaderArr(4, 1) = "R"
                '    HeaderArr(4, 2) = "19"
                '    HeaderArr(4, 3) = "13"

                '    HeaderArr(5, 0) = "PAYMENT"
                '    HeaderArr(5, 1) = "R"
                '    HeaderArr(5, 2) = "15"
                '    HeaderArr(5, 3) = "13"

                '    HeaderArr(6, 0) = "PROG PAYMENT"
                '    HeaderArr(6, 1) = "R"
                '    HeaderArr(6, 2) = "19"
                '    HeaderArr(6, 3) = "13"

                '    HeaderArr(7, 0) = "BALANCE"
                '    HeaderArr(7, 1) = "R"
                '    HeaderArr(7, 2) = "20"
                '    HeaderArr(7, 3) = "13"

                '    HeaderArr(8, 0) = " "
                '    HeaderArr(8, 1) = "L"
                '    HeaderArr(8, 2) = "2"
                '    HeaderArr(8, 3) = "2"

                '    Session("RptHeader") = HeaderArr
                'Else
                Dim HeaderArr(7, 4) As String
                Dim HeaderWidth(7, 1) As String

                HeaderArr(0, 0) = "PARTICULARS"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "40"
                HeaderArr(0, 3) = "40"

                HeaderArr(1, 0) = "DATE"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "10"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "RECEIPT"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "PROG RECEIPT"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "19"
                HeaderArr(3, 3) = "13"

                HeaderArr(4, 0) = "PAYMENT"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "15"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "PROG PAYMENT"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "19"
                HeaderArr(5, 3) = "13"

                HeaderArr(6, 0) = "BALANCE"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "20"
                HeaderArr(6, 3) = "13"

                HeaderArr(7, 0) = " "
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "2"
                HeaderArr(7, 3) = "2"

                Session("RptHeader") = HeaderArr
                'End If

                M_Sql = "EXEC  Acc_Get_GLedgerNew_Sp " & ClsComm.DbDate(Txt_DateFrom.Text) & "," & ClsComm.DbDate(Txt_DateTo.Text) & ",'" & Ddl_Branch.SelectedValue & "','" & AccCode & "','" & Session("UserID") & "','N','N'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
                Exit Sub

                ''Crystal
                'M_Sql = "EXEC  Acc_Get_GLedgerNew_Sp " & ClsComm.DbDate(Txt_DateFrom.Text) & "," & ClsComm.DbDate(Txt_DateTo.Text) & ",'" & Ddl_Branch.SelectedValue & "','" & AccCode & "','" & Session("UserID") & "','N','N'"
                'Session("RptQry") = M_Sql
                'StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & Ddl_Branch.SelectedItem.ToString & "|Heading|General Ledger of " & CmbGL.SelectedItem.Text & " from " & Txt_DateFrom.Text & " to " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                'Session.Add("RptFormula", StrSlf)
                'If Ddl_Branch.SelectedValue = "ALL" Then
                '    Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/GenLedgerALL.rpt"))
                'Else
                '    Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/GenLedger.rpt"))
                'End If
                'Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
                'Exit Sub

            ElseIf Opt_TransferList.Checked = True Then

                Session("DAILY_RPTS_OPTION_BTN") = "TL"

                Dim HeaderArr(4, 4) As String
                Dim HeaderWidth(4, 1) As String
                HeaderArr(0, 0) = "VR NO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "8"

                HeaderArr(1, 0) = "PARTICULARS"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "40"
                HeaderArr(1, 3) = "40"

                HeaderArr(2, 0) = "RECEIPT"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "20"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "PAYMENT"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "20"
                HeaderArr(3, 3) = "13"

                Session("RptHeader") = HeaderArr
                Dim TransferType As Integer
                If OptOwnBranch.Checked = True Then
                    TransferType = 1
                Else
                    TransferType = 1
                End If
                M_Sql = "Exec AdjustmentScroll_Sp " & ClsComm.DbDate(Txt_DateFrom.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & Ddl_Branch.SelectedValue & "'," & TransferType & ",'" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
                Exit Sub
                'M_Sql = "EXEC AdjustScroll_Sp " & ClsComm.DbDate(Txt_DateFrom.Text) & ",'" & Session("UserID") & "','" & Ddl_Branch.SelectedValue & "','%','' "

                'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

                'Session.Add("RptQry", M_Sql)
                'Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/Adj_List.rpt"))

                'StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Transfer List As on: " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")
                'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            ElseIf Opt_RctPay.Checked = True Then

                Session("DAILY_RPTS_OPTION_BTN") = "RB"

                If Chk_RecPay.Checked = True Then

                    If Ddl_Branch.Text = "ALL" Then
                        M_Sql = "RctDisb_Sp 'All'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & "CONSOLIDTED" & "|Heading|R & P FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndRecPay.rpt"))


                    Else
                        M_Sql = "RctDisb_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & "|Heading|R & P REPORT FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndRecPay.rpt"))

                    End If


                ElseIf Chk_Rec.Checked = True Then

                    If Ddl_Branch.Text = "ALL" Then
                        M_Sql = "RctDisb_Sp 'All'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & "CONSOLIDTED" & "|Heading|Rceipt FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndReconly.rpt"))


                    Else
                        M_Sql = "RctDisb_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & "|Heading|Receipt REPORT FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndReconly.rpt"))

                    End If

                ElseIf Chk_Pay.Checked = True Then

                    If Ddl_Branch.Text = "ALL" Then
                        M_Sql = "RctDisb_Sp 'All'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & "CONSOLIDTED" & "|Heading|Rceipt FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndPayonly.rpt"))


                    Else
                        M_Sql = "RctDisb_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & "|Heading|Receipt REPORT FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndPayonly.rpt"))

                    End If
                ElseIf Chk_Grp.Checked = True Then

                    If Ddl_Branch.Text = "ALL" Then
                        M_Sql = "RctDisb_Sp 'All'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & "CONSOLIDTED" & "|Heading|RECEIPT & DISBURSEMENT REPORT FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndGrp.rpt"))


                    Else
                        M_Sql = "RctDisb_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & "|Heading|RECEIPT & DISBURSEMENT REPORT FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndGrp.rpt"))

                    End If
                ElseIf Chk_BalHeads.Checked = True Then
                    If Ddl_Branch.Text = "ALL" Then
                        M_Sql = "RctDisbBalHeads_Sp 'All'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & "CONSOLIDTED" & "|Heading|R & D Balance Heads FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndBalHeads.rpt"))
                    Else
                        M_Sql = "RctDisbBalHeads_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & "|Heading|R & D Balance Heads FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RndBalHeads.rpt"))
                    End If
                Else

                    If Ddl_Branch.Text = "ALL" Then
                        M_Sql = "RctDisb_Sp 'All'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & "CONSOLIDTED" & "|Heading|RECEIPT & DISBURSEMENT REPORT FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/Rnd.rpt"))


                    Else
                        M_Sql = "RctDisb_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & "|Heading|RECEIPT & DISBURSEMENT REPORT FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
                        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                        Session.Add("RptQry", M_Sql)
                        Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/Rnd.rpt"))
                    End If
                End If
                If Chk_BrWise.Checked = True Then
                    Dim HeaderArr(22, 4) As String
                    Dim HeaderWidth(22, 1) As String
                    HeaderArr(0, 0) = "Particulars"
                    HeaderArr(0, 1) = "L"
                    HeaderArr(0, 2) = "25"
                    HeaderArr(0, 3) = "13"

                    HeaderArr(1, 0) = "001 Receipt"
                    HeaderArr(1, 1) = "R"
                    HeaderArr(1, 2) = "15"
                    HeaderArr(1, 3) = "13"

                    HeaderArr(2, 0) = "001 Payment"
                    HeaderArr(2, 1) = "R"
                    HeaderArr(2, 2) = "15"
                    HeaderArr(2, 3) = "13"

                    HeaderArr(3, 0) = "002 Receipt"
                    HeaderArr(3, 1) = "R"
                    HeaderArr(3, 2) = "15"
                    HeaderArr(3, 3) = "13"

                    HeaderArr(4, 0) = "002 Payment"
                    HeaderArr(4, 1) = "R"
                    HeaderArr(4, 2) = "15"
                    HeaderArr(4, 3) = "13"

                    HeaderArr(5, 0) = "003 Receipt"
                    HeaderArr(5, 1) = "R"
                    HeaderArr(5, 2) = "15"
                    HeaderArr(5, 3) = "13"

                    HeaderArr(6, 0) = "003 Payment"
                    HeaderArr(6, 1) = "R"
                    HeaderArr(6, 2) = "15"
                    HeaderArr(6, 3) = "13"

                    HeaderArr(7, 0) = "004 Receipt"
                    HeaderArr(7, 1) = "R"
                    HeaderArr(7, 2) = "15"
                    HeaderArr(7, 3) = "13"

                    HeaderArr(8, 0) = "004 Payment"
                    HeaderArr(8, 1) = "R"
                    HeaderArr(8, 2) = "15"
                    HeaderArr(8, 3) = "13"

                    HeaderArr(9, 0) = "005 Receipt"
                    HeaderArr(9, 1) = "R"
                    HeaderArr(9, 2) = "15"
                    HeaderArr(9, 3) = "13"

                    HeaderArr(10, 0) = "005 Payment"
                    HeaderArr(10, 1) = "R"
                    HeaderArr(10, 2) = "15"
                    HeaderArr(10, 3) = "13"

                    HeaderArr(11, 0) = "006 Receipt"
                    HeaderArr(11, 1) = "R"
                    HeaderArr(11, 2) = "15"
                    HeaderArr(11, 3) = "13"

                    HeaderArr(12, 0) = "006 Payment"
                    HeaderArr(12, 1) = "R"
                    HeaderArr(12, 2) = "15"
                    HeaderArr(12, 3) = "13"

                    HeaderArr(13, 0) = "007 Receipt"
                    HeaderArr(13, 1) = "R"
                    HeaderArr(13, 2) = "15"
                    HeaderArr(13, 3) = "13"

                    HeaderArr(14, 0) = "007 Payment"
                    HeaderArr(14, 1) = "R"
                    HeaderArr(14, 2) = "15"
                    HeaderArr(14, 3) = "13"

                    HeaderArr(15, 0) = "008 Receipt"
                    HeaderArr(15, 1) = "R"
                    HeaderArr(15, 2) = "15"
                    HeaderArr(15, 3) = "13"

                    HeaderArr(16, 0) = "008 Payment"
                    HeaderArr(16, 1) = "R"
                    HeaderArr(16, 2) = "15"
                    HeaderArr(16, 3) = "13"

                    HeaderArr(17, 0) = "009 Receipt"
                    HeaderArr(17, 1) = "R"
                    HeaderArr(17, 2) = "15"
                    HeaderArr(17, 3) = "13"

                    HeaderArr(18, 0) = "009 Payment"
                    HeaderArr(18, 1) = "R"
                    HeaderArr(18, 2) = "15"
                    HeaderArr(18, 3) = "13"

                    HeaderArr(19, 0) = "Total Receipt"
                    HeaderArr(19, 1) = "R"
                    HeaderArr(19, 2) = "15"
                    HeaderArr(19, 3) = "13"

                    HeaderArr(20, 0) = "Total Payment"
                    HeaderArr(20, 1) = "R"
                    HeaderArr(20, 2) = "15"
                    HeaderArr(20, 3) = "13"

                    Session("RptHeader") = HeaderArr

                    M_Sql = "Exec RctPayBrWise_Sp " & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
                    Session("RptQry") = M_Sql
                    Response.Redirect("~/PrnView.aspx?PageType=130", False)
                    Exit Sub
                End If
            ElseIf Opt_DayEnd.Checked = True Then

                Session("DAILY_RPTS_OPTION_BTN") = "DE"

                Dim Ds As New DataSet
                M_Sql = "DayEndSummary_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & ""
                M_DBConn.ExecuteDataSet(Ds, M_Sql, False)
                M_Dt = Ds.Tables(0)
                StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & "|Heading|DAY END REPORT AS ON " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")
                Session.Add("RptQry", M_Sql)
                Session.Add("RptPath", Server.MapPath("~/Reports/DayEndReport/DayEnd.rpt"))
                Session("RptHeading") = "DAY END REPORT AS ON " & Txt_DateFrom.Text
                If Ddl_Branch.SelectedItem.ToString() = "All" Then
                    Session("RptHeading1") = HttpContext.Current.Cache("CompanyName") + " - CONSOLIDATED"
                Else
                    Session("RptHeading1") = HttpContext.Current.Cache("CompanyName") + " - " + Ddl_Branch.SelectedItem.ToString()
                End If

                Session.Add("RptFormula", StrSlf)
                Response.Redirect("~/DayEndRep.aspx?PageType=130", False)
                Exit Sub





                'Dim Ds As New DataSet
                'M_Sql = "DayEndSummary_Sp '" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & ""
                'M_Dt = Ds.Tables(0)
                'M_DBConn.ExecuteDataSet(M_Dt, M_Sql, False)
                'StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & "|Heading|DAY END REPORT AS ON " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")


                '' StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"


                'Session.Add("RptQry", M_Sql)
                'Session.Add("RptPath", Server.MapPath("~/DayEnd.rpt"))

                'If Ddl_Branch.Text = "ALL" Then
                '    M_Sql = "Day_end_Report 'All'," & ClsComm.DbDate(Txt_DateFrom.Text, False)
                '    M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                '    StrSlf = "Company|" & Company_Name & "|Branch|" & "DAYEND REPORT" & "|Heading|DATEEND " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")
                '    StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                '    Session.Add("RptQry", M_Sql)
                '    Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/Dayendreport.rpt"))


                'Else
                '    M_Sql = "Day_end_Report '" & Ddl_Branch.SelectedValue & "', " & ClsComm.DbDate(Txt_DateFrom.Text, False)
                '    M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                '    StrSlf = "Company|" & Company_Name & "|Branch|" & "DAYEND REPORT" & "|Heading|DATEEND " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")
                '    StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                '    Session.Add("RptQry", M_Sql)
                '    Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/Dayendreport.rpt"))

                'End If

            ElseIf Opt_IBVOU.Checked = True Then

                Session("DAILY_RPTS_OPTION_BTN") = "IBV"

                Dim HeaderArr(4, 4) As String
                Dim HeaderWidth(4, 1) As String
                HeaderArr(0, 0) = "ACCHOLDER"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "40"
                HeaderArr(0, 3) = "34"

                HeaderArr(1, 0) = "ACCHEAD"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "30"
                HeaderArr(1, 3) = "20"

                HeaderArr(2, 0) = "DEBIT"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "CREDIT"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "15"
                HeaderArr(3, 3) = "13"

                Session("RptHeader") = HeaderArr
                Dim str As String

                If CmbIB.SelectedItem.Text = "DEBIT" Then
                    str = "D"
                ElseIf CmbIB.SelectedItem.Text = "CREDIT" Then
                    str = "C"
                ElseIf CmbIB.SelectedItem.Text = "ALL" Then
                    str = "%"
                End If
                M_Sql = "Exec IBVoucher_Sp'" & Ddl_Branch.SelectedValue & "'," & ClsComm.DbDate(Txt_DateFrom.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & str & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
                Exit Sub
            End If




            Session.Add("RptFormula", StrSlf)
            If CmbOutput.SelectedIndex = 0 Then
                Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
            Else
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            End If
        Catch ex As Exception
            LoggError(ex)
            ShowAlertMessage(ex.Message)
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
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception
            LoggError(ex)
        End Try



    End Sub

    Private Sub Set_Controls()

        RdoGlType.Visible = False
        CmbIB.Visible = False
        Lbl_IBTran.Visible = False
        Lbl_DateTo.Visible = True
        Txt_DateTo.Visible = True
        Chk_RecPay.Visible = False
        Chk_Pay.Visible = False
        Chk_Rec.Visible = False
        Chk_Grp.Visible = False
        Chk_BalHeads.Visible = False
        TransferPnl.Visible = False
        Chck_Excel.Visible = False

        If Opt_RctPay.Checked = True Then
            Lbl_DateTo.Visible = True
            Txt_DateTo.Visible = True
            CmbGL.Visible = False
            Lbl_AccHead.Visible = False
            Chk_RecPay.Visible = True
            Chk_Pay.Visible = True
            Chk_Rec.Visible = True
            Chk_Grp.Visible = True
            Chk_BrWise.Visible = True
            Chk_BalHeads.Visible = True
            Chck_Excel.Visible = False
        ElseIf Opt_GL.Checked = True Then
            Lbl_DateTo.Visible = True
            Txt_DateTo.Visible = True
            CmbGL.Visible = True
            Lbl_AccHead.Visible = True
            Chck_Excel.Visible = False
            'RdoGlType.Visible = True

        ElseIf Opt_IB.Checked = True Then
            CmbIB.Visible = True
            Lbl_IBTran.Visible = True
            CmbGL.Visible = False
            Lbl_AccHead.Visible = False
            Chck_Excel.Visible = False
        ElseIf Opt_IBVOU.Checked = True Then
            CmbIB.Visible = True
            Lbl_IBTran.Visible = True
            CmbGL.Visible = False
            Lbl_AccHead.Visible = False
            Chck_Excel.Visible = False
        ElseIf Opt_CashScroll.Checked = True Then
            RdoGlType.Visible = False
            CmbIB.Visible = False
            Lbl_IBTran.Visible = False
            Lbl_DateTo.Visible = True
            Txt_DateTo.Visible = True
            Chk_RecPay.Visible = False
            Chk_Pay.Visible = False
            Chk_Rec.Visible = False
            Chk_Grp.Visible = False
            Chk_BalHeads.Visible = False
            TransferPnl.Visible = False
            Chck_Excel.Visible = True
        Else
            Lbl_DateTo.Visible = False
            Txt_DateTo.Visible = False
            CmbGL.Visible = False
            Lbl_AccHead.Visible = False
            OptAllTrans.Visible = True
            OptOwnBranch.Visible = True
            Chck_Excel.Visible = False

        End If
        If Opt_TransferList.Checked = True Then
            TransferPnl.Visible = True
            Chck_Excel.Visible = False
        End If
        If Opt_DayEnd.Checked = True Then
            Txt_DateTo.Visible = False
            CmbGL.Visible = False
            CmbIB.Visible = False
            Chck_Excel.Visible = False

        End If

    End Sub

    Protected Sub Opt_CashScroll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_CashScroll.CheckedChanged
        Set_Controls()
    End Sub

    Protected Sub Opt_RctPay_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_RctPay.CheckedChanged
        Set_Controls()
    End Sub

    Protected Sub Opt_TransferList_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_TransferList.CheckedChanged
        Set_Controls()
    End Sub

    Protected Sub Opt_DayBook_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_DayBook.CheckedChanged
        Set_Controls()
    End Sub

    Protected Sub Opt_GL_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_GL.CheckedChanged
        Set_Controls()
    End Sub

    Protected Sub CmbGL_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbGL.SelectedIndexChanged

    End Sub

    Protected Sub RdoGlType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdoGlType.SelectedIndexChanged

    End Sub

    Protected Sub Opt_IBVOU_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_IBVOU.CheckedChanged
        Set_Controls()
    End Sub

    Protected Sub Opt_IB_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_IB.CheckedChanged
        Set_Controls()
    End Sub

    Protected Sub Opt_DayEnd_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Opt_DayEnd.CheckedChanged
        Set_Controls()
    End Sub
    Protected Sub DrpRegn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpRegn.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DrpArea_SelectedIndexChanged(Me.DrpArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(Ddl_Branch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Ddl_Branch.Items.Remove(Ddl_Branch.Items.FindByText("ALL"))
            Ddl_Branch.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub
End Class
