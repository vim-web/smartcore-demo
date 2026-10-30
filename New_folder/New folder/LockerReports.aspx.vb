Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class LockerReports
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim ClsCom As New CommonClass
    Dim FormID As Integer = 1099
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
                Dim RptType As String

                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<--ALL-->")
                DdlBranch.SelectedValue = Session("DefaultBrCode")

                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOCKER")
                DdlScheme.Items.Add("<--ALL-->")

                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1

                DtFrom.Text = Session("TransDate")

                RptType = Request.QueryString("RptType")


                DdlReportOptions.Items.Add("---SELECT---")
                '   DdlReportOptions.Items.Add("Gold Ornament List")
                DdlReportOptions.Items.Add("Locker Due List")
                '  DdlReportOptions.Items.Add("Loan Risk Fund Contribution")

                If RptType = "Locker Due List" Then
                    DdlReportOptions.SelectedIndex = 1
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

                M_Qry = "SELECT L.*,S.Sch_Name FROM Loan_Outstand_R_Tbl as L INNER JOIN Gen_SchemeMaster_P_Tbl AS S ON  L.Sch_Code = S.Sch_Code WHERE L.Sch_Code='" & SchemeCode & "' "
                M_Qry = M_Qry + " And L.Demand_Date='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' And Terminal = '" & Session("HostID") & "'  And UserId='" & Session("LoginID") & "' AND Br_Code='" & Brcode & "' "
                M_Qry = M_Qry + " And L.ODPrnAmt>0 "
                M_Qry = M_Qry + "  order by L.Lm_LoanDate,Right(Space(18)+L.Acc_No,18)"

            ElseIf DdlReportOptions.Text = "Gold Ornament List" Then


                Rpt_Path = "Loan_Ornament_List.rpt"

                M_Qry = "Select L.*,O.Orn_Type As Orn_Type_Code, P.Pkc_Description As Orn_Type,Cast(Orn_NetWt As Decimal(15,2)) As Orn_NetWt, "
                M_Qry = M_Qry + "Cast(Orn_GrossWt As Decimal(15,2)) As Orn_GrossWt,Orn_Count,Orn_Remarks, "
                M_Qry = M_Qry + "A.Outstanding,Lm_LoanAmount, Lm_ApprValue ,P.Pkc_Description Ornament_details From Loan_Detail_Vw L "
                M_Qry = M_Qry + "Join Loan_Outstand_R_Tbl A On A.Br_Code=L.BrCode And A.Sch_Code=L.Sch_Code And A.Acc_No=L.AcNo And "
                M_Qry = M_Qry + " A.Terminal='" & Session("HostID") & "' And A.UserId='" & Session("LoginID") & "' And Demand_Date='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' "
                M_Qry = M_Qry + " Join Gold_Ornaments_M_Tbl O On O.Br_Code=L.BrCode And O.Sch_Code=L.Sch_Code And O.Acc_No=L.AcNo "
                M_Qry = M_Qry + "Join PickupCode_P_Tbl P On P.Pkc_Code=O.Orn_Type Where L.Authorised='Y' "
                M_Qry = M_Qry + "And L.Sch_Code='" & SchemeCode & "'and L.BrCode='" & Brcode & "' "
                M_Qry = M_Qry + "And L.Lm_LoanDate<='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' Order By Right(Space(18)+L.AcNo,18)"

            ElseIf DdlReportOptions.Text = "Loan Risk Fund Contribution" Then



                Rpt_Path = "Loan_Risk_Fund_Contribution.rpt"

                M_Qry = " SELECT     L.Sch_Code,C.Cust_Name, C.Cust_Dob_Inc,Dbo.SfDateDiff_Fn('Y',C.Cust_Dob_Inc,'" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "') As Age , "
                M_Qry = M_Qry + " L.Lm_MemNo, L.Acc_No,S.Sch_Name, L.Lm_Period, L.Lm_PeriodType, L.Lm_HolidayPeriod,   L.Lm_HolidayPeriodType,"
                M_Qry = M_Qry + " Cast(L.Lm_Period+L.Lm_HolidayPeriod As Varchar(5)) + Case When L.Lm_PeriodType='M' Then ' Months' Else ' Days' End As Loan_Period, "
                M_Qry = M_Qry + " L.Lm_LoanAmount,L.Lm_LoanDate,L.Lm_DueDate,T.Cash + T.Transfer AS Risk_Amount,L.Lm_Remarks, C.cust_id,L.Lm_Shr_Sch_Code  FROM     "
                M_Qry = M_Qry + " Loan_Master_M_Tbl L INNER JOIN Transaction_T_Tbl  T ON L.Acc_ID =T.Acc_ID "
                M_Qry = M_Qry + " INNER JOIN  Gold_ProcessingFee_M_Tbl G ON T.Sch_Code = G.Sch_Code AND "
                M_Qry = M_Qry + " T.Gl_Code = G.Acc_Code INNER JOIN Cust_Master_M_Tbl  C ON L.Cust_Id = C.Cust_Id INNER JOIN"
                M_Qry = M_Qry + " Gen_SchemeMaster_P_Tbl S ON L.Sch_Code = S.Sch_Code "
                M_Qry = M_Qry + " WHERE     (T.Cash + T.Transfer > 0) AND "
                M_Qry = M_Qry + " (G.EffectiveDate =(Select Top 1 EffectiveDate From Gold_ProcessingFee_M_Tbl Where Sch_Code=L.Sch_Code And EffectiveDate <='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "'"
                M_Qry = M_Qry + " AND  G.Acc_Code = (Select Acc_Code  from Gen_PerCode_P_Tbl Where Pc_Type='RISK_FUND')  Order By EffectiveDate Desc) )"
                M_Qry = M_Qry + " AND (L.Authorised = 'Y') AND (T.Authorised = 'Y') AND (T.Acc_Code <> T.Gl_Code) AND (T.Tran_Type = 'C') And L.Br_Code='" & Brcode & "'        "
                M_Qry = M_Qry + " And T.Tr_Date Between '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' And '" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "' "
                M_Qry = M_Qry + " And T.Gl_Code = (Select Acc_Code  from Gen_PerCode_P_Tbl Where Pc_Type='RISK_FUND')"
                M_Qry = M_Qry + " Order By S.Sch_Name,Right('000000000000000000'+L.Acc_No,18) "



            End If



            ' -----------------------------------------------
            If DdlReportOptions.Text = "Locker Due List" Then


                Dim HeaderArr(7, 4) As String
                Dim HeaderWidth(7, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "AccNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "CustName"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "50"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "LoanDate"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"


                HeaderArr(4, 0) = "PrnOutstanding"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "13"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "PrnOdAmount"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "13"
                HeaderArr(5, 3) = "13"

                HeaderArr(6, 0) = "NodInterest"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "13"
                HeaderArr(6, 3) = "13"

                HeaderArr(7, 0) = "OdInterest"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "13"
                HeaderArr(7, 3) = "13"



                Session("RptHeader") = HeaderArr

                M_Sql = "Exec LoanOverdueRpt_Sp " & ClsComm.DbDate(DtFrom.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "','" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub

            ElseIf DdlReportOptions.Text = "Gold Ornament List" Then

                M_Qry = "Select L.*,O.Orn_Type As Orn_Type_Code, P.Pkc_Description As Orn_Type,Cast(Orn_NetWt As Decimal(15,2)) As Orn_NetWt, "
                M_Qry = M_Qry + "Cast(Orn_GrossWt As Decimal(15,2)) As Orn_GrossWt,Orn_Count,Orn_Remarks, "
                M_Qry = M_Qry + "A.Outstanding,Lm_LoanAmount, Lm_ApprValue ,P.Pkc_Description Ornament_details From Loan_Detail_Vw L "
                M_Qry = M_Qry + "Join Loan_Outstand_R_Tbl A On A.Br_Code=L.BrCode And A.Sch_Code=L.Sch_Code And A.Acc_No=L.AcNo And "
                M_Qry = M_Qry + " A.Terminal='" & Session("HostID") & "' And A.UserId='" & Session("LoginID") & "' And Demand_Date='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' "
                M_Qry = M_Qry + " Join Gold_Ornaments_M_Tbl O On O.Br_Code=L.BrCode And O.Sch_Code=L.Sch_Code And O.Acc_No=L.AcNo "
                M_Qry = M_Qry + "Join PickupCode_P_Tbl P On P.Pkc_Code=O.Orn_Type Where L.Authorised='Y' "
                M_Qry = M_Qry + "And L.Sch_Code='" & SchemeCode & "'and L.BrCode='" & Brcode & "' "
                M_Qry = M_Qry + "And L.Lm_LoanDate<='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' Order By Right(Space(18)+L.AcNo,18)"

                StrSlf = "Company|" & Company_Name & "|Branch|" & DdlBranch.SelectedItem.ToString & " - Branch|Heading|Loan OverDue  As On : " & DtFrom.Text & "  |User_ID|" & Session("UserID")

            ElseIf DdlReportOptions.Text = "Loan Risk Fund Contribution" Then
                Rpt_Path = "Loan_Risk_Fund_Contribution.rpt"

                M_Qry = " SELECT     L.Sch_Code,C.Cust_Name, C.Cust_Dob_Inc,Dbo.SfDateDiff_Fn('Y',C.Cust_Dob_Inc,'" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "') As Age , "
                M_Qry = M_Qry + " L.Lm_MemNo, L.Acc_No,S.Sch_Name, L.Lm_Period, L.Lm_PeriodType, L.Lm_HolidayPeriod,   L.Lm_HolidayPeriodType,"
                M_Qry = M_Qry + " Cast(L.Lm_Period+L.Lm_HolidayPeriod As Varchar(5)) + Case When L.Lm_PeriodType='M' Then ' Months' Else ' Days' End As Loan_Period, "
                M_Qry = M_Qry + " L.Lm_LoanAmount,L.Lm_LoanDate,L.Lm_DueDate,T.Cash + T.Transfer AS Risk_Amount,L.Lm_Remarks, C.cust_id,L.Lm_Shr_Sch_Code  FROM     "
                M_Qry = M_Qry + " Loan_Master_M_Tbl L INNER JOIN Transaction_T_Tbl  T ON L.Acc_ID =T.Acc_ID "
                M_Qry = M_Qry + " INNER JOIN  Gold_ProcessingFee_M_Tbl G ON T.Sch_Code = G.Sch_Code AND "
                M_Qry = M_Qry + " T.Gl_Code = G.Acc_Code INNER JOIN Cust_Master_M_Tbl  C ON L.Cust_Id = C.Cust_Id INNER JOIN"
                M_Qry = M_Qry + " Gen_SchemeMaster_P_Tbl S ON L.Sch_Code = S.Sch_Code "
                M_Qry = M_Qry + " WHERE     (T.Cash + T.Transfer > 0) AND "
                M_Qry = M_Qry + " (G.EffectiveDate =(Select Top 1 EffectiveDate From Gold_ProcessingFee_M_Tbl Where Sch_Code=L.Sch_Code And EffectiveDate <='" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "'"
                M_Qry = M_Qry + " AND  G.Acc_Code = (Select Acc_Code  from Gen_PerCode_P_Tbl Where Pc_Type='RISK_FUND')  Order By EffectiveDate Desc) )"
                M_Qry = M_Qry + " AND (L.Authorised = 'Y') AND (T.Authorised = 'Y') AND (T.Acc_Code <> T.Gl_Code) AND (T.Tran_Type = 'C') And L.Br_Code='" & Brcode & "'        "
                M_Qry = M_Qry + " And T.Tr_Date Between '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "' And '" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "' "
                M_Qry = M_Qry + " And T.Gl_Code = (Select Acc_Code  from Gen_PerCode_P_Tbl Where Pc_Type='RISK_FUND')"
                M_Qry = M_Qry + " Order By S.Sch_Name,Right('000000000000000000'+L.Acc_No,18) "

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

            If DdlReportOptions.Text = "Locker Due List" Then

                Dim HeaderArr(7, 4) As String
                Dim HeaderWidth(7, 1) As String
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "AccNo"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"

                HeaderArr(2, 0) = "CustName"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "30"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "OpeningDate"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"


                HeaderArr(4, 0) = "Due Date"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

            
                Dim M_Sql As String

                Session("RptHeader") = HeaderArr
                M_Sql = "EXEC LockerDueList_Sp '" & DdlBranch.SelectedValue & "','" & DdlScheme.SelectedValue & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub

            ElseIf DdlReportOptions.Text = "Loan Risk Fund Contribution" Then



            End If

            M_DBConn.ExecuteScalar(SqlCmdQuery, Connection.SQLSource.STOREDPROCEDURE, False)

            ShowAlertMessage("Process Completed...")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DdlReportOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlReportOptions.SelectedIndexChanged
        Try
            DdlReportType.Visible = False
            If DdlReportOptions.Text = "Locker Due List" Then
                LblDt2.Visible = False
                DtTo.Visible = False
                DtFrom.Text = "Ason Date"
            ElseIf DdlReportOptions.Text = "Gold Ornament List" Then
                LblDt2.Visible = False
                DtTo.Visible = False
                DtFrom.Text = "Ason Date"
            ElseIf DdlReportOptions.Text = "Loan Risk Fund Contribution" Then
                LblDt2.Visible = True
                DtFrom.Text = "From Date"
                DtTo.Visible = True
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class