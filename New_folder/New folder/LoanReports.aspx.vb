Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class LoanReports
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1095
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
                M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))

                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<--ALL-->")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                DdlBranch.Enabled = True
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")
                DdlScheme.Items.Add("<--ALL-->")

                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1

                DtFrom.Text = Session("TransDate")

                DdlReportOptions.Items.Add("---SELECT---")
                DdlReportOptions.Items.Add("Gold Ornament List")
                DdlReportOptions.Items.Add("Loan OverDue List")
                DdlReportOptions.Items.Add("Loan Risk Fund Contribution")
                DdlReportOptions.Items.Add("Loan Form Register")
                DdlReportOptions.Items.Add("LoanAppRegister")
                DdlReportOptions.Items.Add("Ornament Release Register")
                DdlReportOptions.Items.Add("Gold Auction List")


                If Request.QueryString("RptType") = "AppReg" Then
                    DdlReportOptions.SelectedIndex = 5
                    LblDt2.Visible = True
                    DtTo.Visible = True
                ElseIf Request.QueryString("RptType") = "AppForm" Then
                    DdlReportOptions.SelectedIndex = 4
                ElseIf Request.QueryString("RptType") = "GoldRelease" Then
                    DdlReportOptions.SelectedIndex = 6
                    DtTo.Text = Session("TransDate")
                    LblDt2.Visible = True
                    DtTo.Visible = True
                End If

                DdlReportType.Items.Add("Ason Date")
                DdlReportType.Items.Add("Upto Date")
                DdlReportType1.Items.Add("Full Over Due")
                DdlReportType1.Items.Add("Partial Over Due")
                DdlReportType1.Items.Add("All")
                DdlReportType.SelectedIndex = 0
                DdlReportType1.SelectedIndex = 0
                DdlReportType.Visible = False
                DdlReportType1.Visible = False
                DdlReportType.Visible = False
                DdlReportType1.Visible = False
                LblOpt1.Visible = False
                LblOpt2.Visible = False
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

            Dim Brcode As String
            Dim SchemeCode As String
            If DdlBranch.SelectedValue <> "<--ALL-->" Then
                Brcode = DdlBranch.SelectedValue
            Else
                Brcode = "0"
            End If
            If DdlScheme.SelectedValue <> "<--ALL-->" Then
                SchemeCode = DdlScheme.SelectedValue
            Else
                SchemeCode = "0"
            End If

            M_Sql = Rpt_Qry '.ToUpper()


            ' Formula From Sql Query Output 

            Dim M_Qry As String = ""
            Dim M_QryFormula As String = ""
            Dim QryVar As String = ""
            Dim QryNo1 As Integer = "0"

            If DdlReportOptions.Text = "Loan OverDue List" Then




                Rpt_Path = "Loan_ODList.rpt"

                M_Qry = "SELECT L.*,S.Sch_Name FROM Loan_Outstand_R_Tbl as L INNER JOIN Gen_SchemeMaster_P_Tbl AS S ON  L.Sch_Code = S.Sch_Code WHERE "
                M_Qry = M_Qry + "L.Demand_Date='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' And Terminal = '" & Session("HostID") & "'  And UserId='" & Session("LoginID") & "'"
                M_Qry = M_Qry + " And L.ODPrnAmt>0 "
                If Brcode <> "0" Then
                    M_Qry = M_Qry + " And Br_Code='" & Brcode & "'        "
                End If
                If SchemeCode <> "0" Then
                    M_Qry = M_Qry + " And L.Sch_Code ='" & SchemeCode & "'        "
                End If
                M_Qry = M_Qry + "  order by L.Lm_LoanDate,Right(Space(18)+L.Acc_No,18)"


                'ElseIf DdlReportOptions.Text = "Gold Ornament List" Then '''Gowtham For Excel


                '    Rpt_Path = "Loan_Ornament_List.rpt"

                '    M_Qry = "Select L.*,O.Orn_Type As Orn_Type_Code, P.Pkc_Description As Orn_Type,Cast(Orn_NetWt As Decimal(15,2)) As Orn_NetWt, "
                '    M_Qry = M_Qry + "Cast(Orn_GrossWt As Decimal(15,2)) As Orn_GrossWt,Orn_Count,Orn_Remarks, "
                '    M_Qry = M_Qry + "A.Outstanding,Lm_LoanAmount, Lm_ApprValue ,P.Pkc_Description Ornament_details From Loan_Detail_Vw L "
                '    M_Qry = M_Qry + "Join Loan_Outstand_R_Tbl A On A.Br_Code=L.BrCode And A.Sch_Code=L.Sch_Code And A.Acc_No=L.AcNo And "
                '    M_Qry = M_Qry + " A.Terminal='" & Session("HostID") & "' And A.UserId='" & Session("LoginID") & "' And Demand_Date='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' "
                '    M_Qry = M_Qry + " Join Gold_Ornaments_M_Tbl O On O.Br_Code=L.BrCode And O.Sch_Code=L.Sch_Code And O.Acc_No=L.AcNo "
                '    M_Qry = M_Qry + "Join PickupCode_P_Tbl P On P.Pkc_Code=O.Orn_Type Where L.Authorised='Y' "
                '    M_Qry = M_Qry + "And L.Sch_Code='" & SchemeCode & "'and L.BrCode='" & Brcode & "' "
                '    M_Qry = M_Qry + "And L.Lm_LoanDate<='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' Order By Right(Space(18)+L.AcNo,18)"

            ElseIf DdlReportOptions.Text = "Loan Risk Fund Contribution" Then



                Rpt_Path = "Loan_Risk_Fund_Contribution.rpt"

                M_Qry = " SELECT     L.Sch_Code,C.Cust_Name, C.Cust_Dob_Inc,  case when C.Cust_Dob_Inc ='01/01/1900' then 0 else Dbo.SfDateDiff_Fn('Y',C.Cust_Dob_Inc,'" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "') end  As Age , "
                M_Qry = M_Qry + " L.Lm_MemNo, L.Acc_No,S.Sch_Name, L.Lm_Period, L.Lm_PeriodType, L.Lm_HolidayPeriod,   L.Lm_HolidayPeriodType,"
                M_Qry = M_Qry + " Cast(L.Lm_Period+L.Lm_HolidayPeriod As Varchar(5)) + Case When L.Lm_PeriodType='M' Then ' Months' Else ' Days' End As Loan_Period, "
                M_Qry = M_Qry + " L.Lm_LoanAmount,L.Lm_LoanDate,L.Lm_DueDate,(T.Cash + T.Transfer) AS Risk_Amount,L.Lm_Remarks, C.cust_id,L.Lm_Shr_Sch_Code  FROM     "
                M_Qry = M_Qry + " Loan_Master_M_Tbl L INNER JOIN Transaction_T_Tbl  T ON L.Acc_ID =T.Acc_ID "
                M_Qry = M_Qry + " INNER JOIN  Gold_ProcessingFee_M_Tbl G ON T.Sch_Code = G.Sch_Code AND "
                M_Qry = M_Qry + " T.Gl_Code = G.Acc_Code INNER JOIN Cust_Master_M_Tbl  C ON L.Cust_Id = C.Cust_Id INNER JOIN"
                M_Qry = M_Qry + " Gen_SchemeMaster_P_Tbl S ON L.Sch_Code = S.Sch_Code "
                M_Qry = M_Qry + " WHERE     (T.Cash + T.Transfer > 0) AND "
                M_Qry = M_Qry + " (G.EffectiveDate =(Select Top 1 EffectiveDate From Gold_ProcessingFee_M_Tbl Where Sch_Code=L.Sch_Code And EffectiveDate <='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "'"
                M_Qry = M_Qry + " AND  G.Acc_Code = '307'  Order By EffectiveDate Desc) )"
                M_Qry = M_Qry + " AND (L.Authorised = 'Y') AND (T.Authorised = 'Y') AND (T.Acc_Code <> T.Gl_Code) AND (T.Tran_Type = 'C')"
                If Brcode <> "0" Then
                    M_Qry = M_Qry + " And L.Br_Code='" & Brcode & "'        "
                End If
                M_Qry = M_Qry + " And T.Tr_Date Between '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' And '" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "' "
                M_Qry = M_Qry + " And T.Gl_Code = '307'"
                M_Qry = M_Qry + " Order By  L.Br_Code,S.Sch_Name,Right('000000000000000000'+L.Acc_No,18) "


            ElseIf DdlReportOptions.Text = "Gold Auction List" Then

                Dim HeaderArr(11, 4) As String
                Dim HeaderWidth(11, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "LoanNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "11"
                HeaderArr(1, 3) = "11"

                HeaderArr(2, 0) = "MemNo"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "11"
                HeaderArr(2, 3) = "11"

                HeaderArr(3, 0) = "Name"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "25"
                HeaderArr(3, 3) = "25"


                HeaderArr(4, 0) = "Address"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "50"
                HeaderArr(4, 3) = "50"

                HeaderArr(5, 0) = "LoanDate"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "DueDate"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "10"

                HeaderArr(7, 0) = "LoanAmount"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "13"
                HeaderArr(7, 3) = "13"

                HeaderArr(8, 0) = "Balance"
                HeaderArr(8, 1) = "R"
                HeaderArr(8, 2) = "13"
                HeaderArr(8, 3) = "13"

                HeaderArr(9, 0) = "IntAmount"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "13"
                HeaderArr(9, 3) = "13"

                HeaderArr(10, 0) = "NetWt"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "13"
                HeaderArr(10, 3) = "13"

                HeaderArr(11, 0) = "GrossWt"
                HeaderArr(11, 1) = "R"
                HeaderArr(11, 2) = "13"
                HeaderArr(11, 3) = "13"

                Session("RptHeader") = HeaderArr

                M_Sql = "Exec [Proc_GoldAuctionListRpt] '" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "'," & ClsComm.DbDate(DtFrom.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=180", False)

                Exit Sub

            ElseIf DdlReportOptions.Text = "Gold Ornament List" Then
                '''''''
                ''ElseIf DdlReportOptions.Text = "Gold Ornament List" Then
                Dim Scheme As String

                If DdlScheme.SelectedValue = "<--ALL-->" Then
                    Scheme = "0"
                Else
                    Scheme = DdlScheme.SelectedValue
                End If

                Dim HeaderArr(12, 4) As String
                Dim HeaderWidth(12, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "LoanNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Scheme"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "25"
                HeaderArr(2, 3) = "25"

                HeaderArr(3, 0) = "MemNo"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"

                HeaderArr(4, 0) = "Name"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "25"
                HeaderArr(4, 3) = "25"


                HeaderArr(5, 0) = "LoanDate"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "OrnDetail"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "25"
                HeaderArr(6, 3) = "25"

                HeaderArr(7, 0) = "OrnCount"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "8"
                HeaderArr(7, 3) = "8"

                HeaderArr(8, 0) = "NetWt"
                HeaderArr(8, 1) = "R"
                HeaderArr(8, 2) = "7"
                HeaderArr(8, 3) = "7"

                HeaderArr(9, 0) = "GrossWt"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "7"
                HeaderArr(9, 3) = "7"

                HeaderArr(10, 0) = "LoanAmt"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "11"
                HeaderArr(10, 3) = "11"

                HeaderArr(11, 0) = "AppAmt"
                HeaderArr(11, 1) = "R"
                HeaderArr(11, 2) = "11"
                HeaderArr(11, 3) = "11"

                HeaderArr(12, 0) = "Outstand"
                HeaderArr(12, 1) = "R"
                HeaderArr(12, 2) = "11"
                HeaderArr(12, 3) = "11"

                Session("RptHeader") = HeaderArr

                M_Sql = "Exec [Proc_GoldOrnamentListRpt] '" & DdlBranch.SelectedValue & "','" & Scheme & "'," & ClsComm.DbDate(DtFrom.Text) & "," & ClsComm.DbDate(DtTo.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "','Y'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub


                End If



                ' ---------------------


                M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)

                If M_Dt.Rows.Count > 0 Then

                    M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)

                    If M_Dt.Rows.Count > 0 Then
                        StrSlf = Replace(StrSlf, QryVar, M_Dt.Rows(0)(0).ToString())
                    Else
                        StrSlf = Replace(StrSlf, QryVar, "0")
                    End If
                End If

                Session.Add("RptQry", M_Qry)

                ' -----------------------------------------------
                If DdlReportOptions.Text = "Loan OverDue List" Then

                If ChkExcl.Checked = False Then
                    'M_Sql = "Exec LoanOverdueRpt_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "','" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "'"

                    M_Sql = "Exec LoanOverdueRpt_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Session("LoginId") & "' ,'" & Session("HostID") & "'"



                    Dim Dt As Date
                    Dt = DtFrom.Text
                    StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|Loan Overdue Statement as on  " & DtFrom.Text & "|User_ID|" & Session("UserID")
                    StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                    Session.Add("RptQry", M_Sql)
                    Session.Add("RptFormula", StrSlf)
                    Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/LoanODRpts.rpt"))
                    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

                    Exit Sub

                End If


                'Dim HeaderArr(12, 4) As String
                'Dim HeaderWidth(12, 1) As String
                'HeaderArr(0, 0) = "SlNo"
                'HeaderArr(0, 1) = "L"
                'HeaderArr(0, 2) = "4"
                'HeaderArr(0, 3) = "4"

                'HeaderArr(1, 0) = "Member No"
                'HeaderArr(1, 1) = "L"
                'HeaderArr(1, 2) = "13"
                'HeaderArr(1, 3) = "13"

                'HeaderArr(2, 0) = "Name"
                'HeaderArr(2, 1) = "L"
                'HeaderArr(2, 2) = "50"
                'HeaderArr(2, 3) = "13"

                'HeaderArr(3, 0) = "Address"
                'HeaderArr(3, 1) = "L"
                'HeaderArr(3, 2) = "50"
                'HeaderArr(3, 3) = "10"


                'HeaderArr(4, 0) = "Loan No"
                'HeaderArr(4, 1) = "L"
                'HeaderArr(4, 2) = "13"
                'HeaderArr(4, 3) = "13"

                'HeaderArr(5, 0) = "Loan Date"
                'HeaderArr(5, 1) = "L"
                'HeaderArr(5, 2) = "10"
                'HeaderArr(5, 3) = "10"

                'HeaderArr(6, 0) = "Due Date"
                'HeaderArr(6, 1) = "L"
                'HeaderArr(6, 2) = "10"
                'HeaderArr(6, 3) = "10"

                'HeaderArr(7, 0) = "Loan Amt"
                'HeaderArr(7, 1) = "R"
                'HeaderArr(7, 2) = "13"
                'HeaderArr(7, 3) = "13"

                'HeaderArr(8, 0) = "Balance"
                'HeaderArr(8, 1) = "R"
                'HeaderArr(8, 2) = "13"
                'HeaderArr(8, 3) = "13"

                'HeaderArr(9, 0) = "OD Amount"
                'HeaderArr(9, 1) = "R"
                'HeaderArr(9, 2) = "13"
                'HeaderArr(9, 3) = "13"

                'HeaderArr(10, 0) = "Interest"
                'HeaderArr(10, 1) = "R"
                'HeaderArr(10, 2) = "13"
                'HeaderArr(10, 3) = "13"

                'HeaderArr(11, 0) = "OD Int"
                'HeaderArr(11, 1) = "R"
                'HeaderArr(11, 2) = "13"
                'HeaderArr(11, 3) = "13"

                'Session("RptHeader") = HeaderArr

                'M_Sql = "Exec LoanOverdueRpt_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "','" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "'"
                'Session("RptQry") = M_Sql
                'Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Dim HeaderArr(12, 4) As String
                Dim HeaderWidth(12, 1) As String
               
                HeaderArr(0, 0) = "SL No"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "13"
                HeaderArr(0, 3) = "13"

                HeaderArr(1, 0) = "Branch"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Scheme_Name"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "50"
                HeaderArr(2, 3) = "10"


                HeaderArr(3, 0) = "Account_No"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"

                HeaderArr(4, 0) = "Cust_Name"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "15"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "Address"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "Loan_Date"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "13"
                HeaderArr(6, 3) = "13"

                HeaderArr(7, 0) = "Loan_Amount"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "13"
                HeaderArr(7, 3) = "13"

                HeaderArr(8, 0) = "Balance"
                HeaderArr(8, 1) = "R"
                HeaderArr(8, 2) = "13"
                HeaderArr(8, 3) = "13"

                HeaderArr(9, 0) = "OD_Amount"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "13"
                HeaderArr(9, 3) = "13"


                HeaderArr(10, 0) = "Interest"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "13"
                HeaderArr(10, 3) = "13"

                HeaderArr(11, 0) = "OD Interest"
                HeaderArr(11, 1) = "R"
                HeaderArr(11, 2) = "13"
                HeaderArr(11, 3) = "13"

                'HeaderArr(12, 0) = "OD_Days"
                'HeaderArr(12, 1) = "R"
                'HeaderArr(12, 2) = "13"
                'HeaderArr(12, 3) = "13"

                'HeaderArr(13, 0) = "Last_Interest_Date"
                'HeaderArr(13, 1) = "R"
                'HeaderArr(13, 2) = "13"
                'HeaderArr(13, 3) = "13"

                Session("RptHeader") = HeaderArr

                ''M_Sql = "Exec LoanOverdueRpt_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "','" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "'"

                M_Sql = "Exec LoanOverdueRpt_Sp_Exl_new " & ClsComm.DbDate(DtFrom.Text) & ",'" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Session("LoginId") & "' ,'" & Session("HostID") & "'"

                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=180", False)


                Exit Sub




            ElseIf DdlReportOptions.Text = "Gold Ornament List" Then

                M_Qry = "Select L.*,O.Orn_Type As Orn_Type_Code, P.Pkc_Description As Orn_Type,Cast(Orn_NetWt As Decimal(15,2)) As Orn_NetWt, "
                M_Qry = M_Qry + "Cast(Orn_GrossWt As Decimal(15,2)) As Orn_GrossWt,Orn_Count,Orn_Remarks, "
                M_Qry = M_Qry + "A.Outstanding,Lm_LoanAmount, Lm_ApprValue ,P.Pkc_Description Ornament_details From Loan_Detail_Vw L "
                M_Qry = M_Qry + "Join Loan_Outstand_R_Tbl A On A.Br_Code=L.BrCode And A.Sch_Code=L.Sch_Code And A.Acc_No=L.AcNo And "
                M_Qry = M_Qry + " A.Terminal='" & Session("HostID") & "' And A.UserId='" & Session("LoginID") & "' And Demand_Date='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' "
                M_Qry = M_Qry + " Join Gold_Ornaments_M_Tbl O On O.Br_Code=L.BrCode And O.Sch_Code=L.Sch_Code And O.Acc_No=L.AcNo "
                M_Qry = M_Qry + "Join PickupCode_P_Tbl P On P.Pkc_Code=O.Orn_Type Where L.Authorised='Y' "
                M_Qry = M_Qry + "And L.Lm_LoanDate<='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "'"
                If Brcode <> "0" Then
                    M_Qry = M_Qry + "and L.BrCode ='" & Brcode & "' "
                End If
                If SchemeCode <> "0" Then
                    M_Qry = M_Qry + "And L.Sch_Code ='" & SchemeCode & "'"
                End If
                M_Qry = M_Qry + " Order By Right(Space(18)+L.AcNo,18)"
                StrSlf = "Company|" & Company_Name & "|Branch|" & DdlBranch.SelectedItem.ToString & " - Branch|Heading|Loan OverDue  As On : " & DtFrom.Text & "  |User_ID|" & Session("UserID")



            ElseIf DdlReportOptions.Text = "Loan Risk Fund Contribution" Then
                Rpt_Path = "Loan_Risk_Fund_Contribution.rpt"

                M_Qry = " SELECT     L.Sch_Code,C.Cust_Name, C.Cust_Dob_Inc,  case when C.Cust_Dob_Inc ='01/01/1900' then 0 else Dbo.SfDateDiff_Fn('Y',C.Cust_Dob_Inc,L.Lm_LoanDate) end  As Age , "
                M_Qry = M_Qry + " L.Lm_MemNo, L.Acc_No,S.Sch_Name, L.Lm_Period, L.Lm_PeriodType, L.Lm_HolidayPeriod,   L.Lm_HolidayPeriodType,"
                M_Qry = M_Qry + " Cast(L.Lm_Period+L.Lm_HolidayPeriod As Varchar(5)) + Case When L.Lm_PeriodType='M' Then ' Months' Else ' Days' End As Loan_Period, "
                M_Qry = M_Qry + " L.Lm_LoanAmount,L.Lm_LoanDate,L.Lm_DueDate,(T.Cash + T.Transfer) AS Risk_Amount,L.Lm_Remarks, C.cust_id,L.Lm_Shr_Sch_Code  FROM     "
                M_Qry = M_Qry + " Loan_Master_M_Tbl L INNER JOIN Transaction_T_Tbl  T ON L.Acc_ID =T.Acc_ID "
                M_Qry = M_Qry + " INNER JOIN  Gold_ProcessingFee_M_Tbl G ON T.Sch_Code = G.Sch_Code AND "
                M_Qry = M_Qry + " T.Gl_Code = G.Acc_Code INNER JOIN Cust_Master_M_Tbl  C ON L.Cust_Id = C.Cust_Id INNER JOIN"
                M_Qry = M_Qry + " Gen_SchemeMaster_P_Tbl S ON L.Sch_Code = S.Sch_Code "
                M_Qry = M_Qry + " WHERE     (T.Cash + T.Transfer > 0) AND "
                M_Qry = M_Qry + " (G.EffectiveDate =(Select Top 1 EffectiveDate From Gold_ProcessingFee_M_Tbl Where Sch_Code=L.Sch_Code And EffectiveDate <='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "'"
                M_Qry = M_Qry + " AND  G.Acc_Code = '307'  Order By EffectiveDate Desc) )"
                M_Qry = M_Qry + " AND (L.Authorised = 'Y') AND (T.Authorised = 'Y') AND (T.Acc_Code <> T.Gl_Code) AND (T.Tran_Type = 'C') "

                If Brcode <> "0" Then
                    M_Qry = M_Qry + " And L.Br_Code='" & Brcode & "' "
                End If

                If SchemeCode <> "0" Then
                    M_Qry = M_Qry + " And L.Sch_Code='" & SchemeCode & "' "
                End If

                M_Qry = M_Qry + " And T.Tr_Date Between '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' And '" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "' "
                M_Qry = M_Qry + " And T.Gl_Code = '307'"
                M_Qry = M_Qry + " Order By L.Br_Code,S.Sch_Name,Right('000000000000000000'+L.Acc_No,18) "

                StrSlf = "Company|" & Company_Name & "|Branch|" & DdlBranch.SelectedItem.ToString & " - Branch|Heading|Loan Risk Fund Contribution Statement (Form No. A) Between " & DtFrom.Text & " and " & DtTo.Text & " |User_ID|" & Session("UserID")

            End If


            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"


            M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)

            If M_Dt.Rows.Count > 0 Then
                'M_DBConn.ExecuteDataTable(M_Dt, M_QryFormula, False)
                M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)

                If M_Dt.Rows.Count > 0 Then
                    StrSlf = Replace(StrSlf, QryVar, M_Dt.Rows(0)(0).ToString())
                Else
                    StrSlf = Replace(StrSlf, QryVar, "0")
                End If
            End If

            Session.Add("RptQry", M_Qry)

            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/" & Rpt_Path))
            Session.Add("RptFormula", StrSlf)

            If CmbOutput.SelectedIndex = 0 Then
                Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
            Else
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Dim SqlCmdQuery As String

            Dim Brcode As String
            Dim SchemeCode As String
            If DdlBranch.SelectedValue <> "<--ALL-->" Then
                Brcode = DdlBranch.SelectedValue
            Else
                Brcode = "0"
            End If
            If DdlScheme.SelectedValue <> "<--ALL-->" Then
                SchemeCode = DdlScheme.SelectedValue
            Else
                SchemeCode = "0"
            End If

            If DdlReportOptions.Text = "Loan OverDue List" Then

                SqlCmdQuery = "Exec [dbo].[Process_LoanAccrued_Sp] '" & Brcode & "', 'S',"
                SqlCmdQuery = SqlCmdQuery + "'" & SchemeCode & "' ,"
                SqlCmdQuery = SqlCmdQuery + "'','" + Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" + Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','N','Y','Y','Y','" & Session("HostID") & "','" & Session("LoginID") & "'"
                SqlCmdQuery = SqlCmdQuery + ",'N','N','N'"
                If DdlReportType.SelectedIndex = 0 Then
                    SqlCmdQuery = SqlCmdQuery + ",'N','N','%','A'"
                ElseIf DdlReportType.SelectedIndex = 1 Then
                    SqlCmdQuery = SqlCmdQuery + ",'N','N','%','U'"            
                End If
                If DdlReportType1.SelectedIndex = 0 Then
                    SqlCmdQuery = SqlCmdQuery + ",'F'"
                ElseIf DdlReportType1.SelectedIndex = 1 Then
                    SqlCmdQuery = SqlCmdQuery + ",'P'"
                Else
                    SqlCmdQuery = SqlCmdQuery + ",'A'"
                End If

                SqlCmdQuery = "Exec [dbo].[ProcessLoanOD_Sp] '" & Brcode & "','" & SchemeCode & "','" + Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "'"


            ElseIf DdlReportOptions.Text = "LoanAppRegister" Then

                'M_Qry = "SELECT L.*,S.Sch_Name FROM Loan_Outstand_R_Tbl as L INNER JOIN Gen_SchemeMaster_P_Tbl AS S ON  L.Sch_Code = S.Sch_Code WHERE L.Sch_Code='" & SchemeCode & "' "
                'M_Qry = M_Qry + " And L.Demand_Date='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' And Terminal = '" & Session("HostID") & "'  And UserId='" & Session("LoginID") & "' AND Br_Code='" & Brcode & "' "
                'M_Qry = M_Qry + " And L.ODPrnAmt>0 "
                'M_Qry = M_Qry + "  order by L.Lm_LoanDate,Right(Space(18)+L.Acc_No,18)"

                'StrSlf = "Company|" & Company_Name & "|Branch|" & DdlBranch.SelectedItem.ToString & " - Branch|Heading|Loan OverDue  As On : " & DtFrom.Text & "  |User_ID|" & Session("UserID")

                Dim HeaderArr(12, 4) As String
                Dim HeaderWidth(12, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "AccNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "5"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "MemNo"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "5"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "CustName"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "17"

                HeaderArr(4, 0) = "LOANAMT"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "5"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "LoanDate"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "PIDATE"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "10"


                HeaderArr(7, 0) = "PI DONE BY"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "10"
                HeaderArr(7, 3) = "10"

                HeaderArr(8, 0) = "RIDATE"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "5"
                HeaderArr(8, 3) = "10"


                HeaderArr(9, 0) = "RI DONE BY"
                HeaderArr(9, 1) = "L"
                HeaderArr(9, 2) = "10"
                HeaderArr(9, 3) = "10"

                HeaderArr(10, 0) = "Status"

                HeaderArr(10, 1) = "L"
                HeaderArr(10, 2) = "10"
                HeaderArr(10, 3) = "10"

                HeaderArr(11, 0) = "RES.NO"
                HeaderArr(11, 1) = "L"
                HeaderArr(11, 2) = "5"
                HeaderArr(11, 3) = "10"

                HeaderArr(12, 0) = "RES.DATE"
                HeaderArr(12, 1) = "L"
                HeaderArr(12, 2) = "5"
                HeaderArr(12, 3) = "10"




                Session("RptHeader") = HeaderArr
                Dim M_Sql
                M_Sql = "Exec [LoanAppRegisterRpt] '" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginId") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub
            ElseIf DdlReportOptions.Text = "Loan Form Register" Then


                Dim HeaderArr(8, 4) As String
                Dim HeaderWidth(8, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "FormNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "MemNo"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "13"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "CustName"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "30"
                HeaderArr(3, 3) = "30"

                HeaderArr(4, 0) = "Form Amount"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "13"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "Form Date"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "Remarks"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "45"
                HeaderArr(6, 3) = "45"

                Session("RptHeader") = HeaderArr
                Dim M_Sql
                M_Sql = "Exec [LoanAppRegisterFormRpt] '" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate("01/01/1900"), "MM/dd/yyyy") & "','" & Session("LoginId") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub

            ElseIf DdlReportOptions.Text = "Ornament Release Register" Then


                Dim HeaderArr(8, 4) As String
                Dim HeaderWidth(8, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "ACC NO"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "Member No"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "13"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "Name"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "30"
                HeaderArr(3, 3) = "30"

                HeaderArr(4, 0) = "Loan Amount"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "13"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "Release Date"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                HeaderArr(6, 0) = "Remarks"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "25"
                HeaderArr(6, 3) = "25"

                Session("RptHeader") = HeaderArr
                Dim M_Sql
                M_Sql = "Exec [LoanGoldReleaseRegister] '" & Brcode & "','" & SchemeCode & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginId") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub

            ElseIf DdlReportOptions.Text = "Loan Risk Fund Contribution" Then

                SqlCmdQuery = "Exec Oustand_LoanDeposit 'SMARTCORE' , '" & Session("HostID") & "' ,'" & Session("LoginID") & "' ,'" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','''LOAN''','" & IIf(Brcode = "0", "", Brcode) & "','Y'"

            End If

            M_DBConn.ExecuteScalar(SqlCmdQuery, Connection.SQLSource.STOREDPROCEDURE, False)

            ShowAlertMessage("Process Completed...")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DdlReportOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlReportOptions.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            ImageButton1.Visible = True
            DdlReportType.Visible = False
            DdlReportType1.Visible = False
            LblOpt1.Visible = False
            LblOpt2.Visible = False
            If DdlReportOptions.Text = "Loan OverDue List" Then
                LblDt2.Visible = False
                DtTo.Visible = False
                DtFrom.Text = Session("TransDate")
                'DdlReportType.Visible = True
                'DdlReportType1.Visible = True
                'LblOpt1.Visible = True
                'LblOpt2.Visible = True
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")
                DdlScheme.Items.Add("<--ALL-->")
            ElseIf DdlReportOptions.Text = "Gold Ornament List" Then
                LblDt2.Visible = True
                DtTo.Visible = True
                DtTo.Text = Session("TransDate")
                DtFrom.Text = Session("TransDate")
                ImageButton1.Visible = False
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN", "GOLDLOAN")
                DdlScheme.Items.Add("<--ALL-->")
            ElseIf DdlReportOptions.Text = "Loan Risk Fund Contribution" Then
                LblDt2.Visible = True
                DtFrom.Text = Session("TransDate")
                DtTo.Visible = True
                DtTo.Text = Session("TransDate")
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")
                DdlScheme.Items.Add("<--ALL-->")
            ElseIf DdlReportOptions.Text = "LoanAppRegister" Then
                LblDt2.Visible = True
                DtTo.Visible = True
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")
                DdlScheme.Items.Add("<--ALL-->")
            ElseIf DdlReportOptions.Text = "Ornament Release Register" Then
                LblDt2.Visible = True
                DtTo.Visible = True
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN", "GOLDLOAN")
                DdlScheme.Items.Add("<--ALL-->")
            ElseIf DdlReportOptions.Text = "Gold Auction List" Then
                LblDt2.Visible = False
                DtTo.Visible = False
                ImageButton1.Visible = False
                DtFrom.Text = Session("TransDate")
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN", "GOLDLOAN")
                DdlScheme.Items.Add("<--ALL-->")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
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
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
            DdlBranch.Items.Add("<-- ALL -->")
        Catch ex As Exception

        End Try
    End Sub
End Class