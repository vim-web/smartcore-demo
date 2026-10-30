Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports CrystalDecisions.Shared
Public Class Share
    Inherits System.Web.UI.Page
    Dim M_Sql As String
    Dim M_ReportBl As New ReportsBL
    Private Shared CommCls As New CommonClass()

    Dim M_CommonDa As New CommonDA
    Shared Company_Name As String
    Dim StrSlf As String
    Dim OpCash As Double
    Dim ClCash As Double
    Dim Dt As DataTable
    Private Shared M_DBConn As New Connection
    Dim FormID As Integer = 1126

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1
                Company_Name = M_CommonDa.GetCompanyDetails()
                Load_DefaultValues()
                Assign_Values()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        CmbSection.SelectedIndex = 0
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.Text)
        M_CommonBL = Nothing
       
    End Sub

    Private Sub Assign_Values()
        Dim M_CommonBL As New CommonBL

        CmbBranch.SelectedValue = Session("AcOpenBranch")
        CmbScheme.SelectedValue = Session("AcOpenScheme")

        M_CommonBL = Nothing

    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSection.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), CmbSection.SelectedItem.Text)
    End Sub

   
    Protected Sub CmdShow_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Try
                Dim RptName As String = ""
                Dim HeadingName As String = ""
                Dim ClsComm As New CommonClass()

                If CmbSection.SelectedItem.Text = "SHARE" Then
                    If Rbt_OpenReg.Checked = True Then

                        Dim HeaderArr(6, 4) As String
                        Dim HeaderWidth(6, 1) As String

                        HeaderArr(0, 0) = "SLNO"
                        HeaderArr(0, 1) = "L"
                        HeaderArr(0, 2) = "4"
                        HeaderArr(0, 3) = "4"

                        HeaderArr(1, 0) = "Member No"
                        HeaderArr(1, 1) = "L"
                        HeaderArr(1, 2) = "13"
                        HeaderArr(1, 3) = "13"

                        HeaderArr(2, 0) = "CustName"
                        HeaderArr(2, 1) = "L"
                        HeaderArr(2, 2) = "15"
                        HeaderArr(2, 3) = "15"

                        HeaderArr(3, 0) = "Admit Date"
                        HeaderArr(3, 1) = "L"
                        HeaderArr(3, 2) = "10"
                        HeaderArr(3, 3) = "10"


                        HeaderArr(4, 0) = "Res No"
                        HeaderArr(4, 1) = "L"
                        HeaderArr(4, 2) = "7"
                        HeaderArr(4, 3) = "7"

                        HeaderArr(5, 0) = "Res Date"
                        HeaderArr(5, 1) = "L"
                        HeaderArr(5, 2) = "10"
                        HeaderArr(5, 3) = "10"

                        HeaderArr(6, 0) = "Balance"
                        HeaderArr(6, 1) = "R"
                        HeaderArr(6, 2) = "11"
                        HeaderArr(6, 3) = "11"

                     



                        Session("RptHeader") = HeaderArr

                        M_Sql = "Exec ShareOpeningClosingRpt_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "', " & ClsComm.DbDate(TxtFromDate.Text) & "," & ClsComm.DbDate(TxtTodate.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "','O'"
                        Session("RptQry") = M_Sql
                        Response.Redirect("~/PrnView.aspx?PageType=80", False)

                        Exit Sub
                        'M_Sql = "Select S.Sch_Name,SM.ACC_ID ,C.Cust_Name,SM.Sm_AdmitDate,SM.Sm_ResNo,SM.Sm_ResDate," & _
                        '   " SM.Sm_NoShr, SM.Sm_Closed_Date," & _
                        '   " dbo.Share_OutstngMemNo_Fn ('" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "',SM.Sch_Code ,SM.Sm_MemNo ,SM.Br_Code ) AS Sm_Balance, " & _
                        '   " SM.Sch_Code,SM.Br_Code," & _
                        '   " SM.Sm_MemNo" & _
                        '   " From Share_Master_M_Tbl   SM" & _
                        '   " Left Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=SM.Sch_Code" & _
                        '   " Left Join Cust_Master_M_Tbl C On C.Cust_Id=SM.Cust_Id" & _
                        '   " Where SM.Authorised='Y' And SM.Sm_AdmitDate>='" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "' " & _
                        '   " And SM.Sm_AdmitDate<='" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "' " & _
                        '   " And SM.Sch_Code='" & CmbScheme.SelectedItem.Value & "'" & _
                        '   " And SM.Br_Code='" & CmbBranch.SelectedItem.Value & "'"
                        'HeadingName = "ACCOUNT OPENING REGISTER"
                        'RptName = "Share_OpeningRegister.rpt"

                    ElseIf Rbt_CloseReg.Checked = True Then
                        Dim HeaderArr(5, 4) As String
                        Dim HeaderWidth(5, 1) As String

                        HeaderArr(0, 0) = "SLNO"
                        HeaderArr(0, 1) = "L"
                        HeaderArr(0, 2) = "4"
                        HeaderArr(0, 3) = "4"

                        HeaderArr(1, 0) = "Member No"
                        HeaderArr(1, 1) = "L"
                        HeaderArr(1, 2) = "13"
                        HeaderArr(1, 3) = "13"

                        HeaderArr(2, 0) = "CustName"
                        HeaderArr(2, 1) = "L"
                        HeaderArr(2, 2) = "50"
                        HeaderArr(2, 3) = "13"


                        HeaderArr(3, 0) = "Res No"
                        HeaderArr(3, 1) = "L"
                        HeaderArr(3, 2) = "13"
                        HeaderArr(3, 3) = "13"

                        HeaderArr(4, 0) = "Res Date"
                        HeaderArr(4, 1) = "L"
                        HeaderArr(4, 2) = "10"
                        HeaderArr(4, 3) = "10"

                        HeaderArr(5, 0) = "Close Date"
                        HeaderArr(5, 1) = "L"
                        HeaderArr(5, 2) = "10"
                        HeaderArr(5, 3) = "10"






                        Session("RptHeader") = HeaderArr

                        M_Sql = "Exec ShareOpeningClosingRpt_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "', " & ClsComm.DbDate(TxtFromDate.Text) & "," & ClsComm.DbDate(TxtTodate.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "','C'"
                        Session("RptQry") = M_Sql
                        Response.Redirect("~/PrnView.aspx?PageType=80", False)

                        Exit Sub


                        'M_Sql = "Select S.Sch_Name,SM.ACC_ID ,C.Cust_Name,SM.Sm_AdmitDate,SM.Sm_ResNo,SM.Sm_ResDate," & _
                        '   " SM.Sm_NoShr, SM.Sm_Closed_Date," & _
                        '   " dbo.Share_OutstngMemNo_Fn ('" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "',SM.Sch_Code ,SM.Sm_MemNo ,SM.Br_Code ) AS Sm_Balance, " & _
                        '   " SM.Sch_Code,SM.Br_Code," & _
                        '   " SM.Sm_MemNo" & _
                        '   " From Share_Master_M_Tbl   SM" & _
                        '   " Left Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=SM.Sch_Code" & _
                        '   " Left Join Cust_Master_M_Tbl C On C.Cust_Id=SM.Cust_Id" & _
                        '   " Where SM.Authorised='Y' And SM.Sm_Closed_Date>SM.Sm_AdmitDate " & _
                        '   " And SM.Sm_Closed_Date>='" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "' " & _
                        '   " And SM.Sm_Closed_Date<='" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "' " & _
                        '   " And SM.Sch_Code='" & CmbScheme.SelectedItem.Value & "'" & _
                        '   " And SM.Br_Code='" & CmbBranch.SelectedItem.Value & "'"
                        'HeadingName = "ACCOUNT CLOSING REGISTER"
                        'RptName = "Share_Closure_Register.rpt"

                    ElseIf Rbt_DivPostedList.Checked = True Then
                        'M_Sql = "SELECT S.Sch_Name, D.Br_Code  , D.Sch_Code  , D.Tran_Type , D.Tr_Date, C .Cust_Id, " & _
                        '        " C.Cust_Name, D.FinYear, D.Cash + D.Transfer AS Div_Amt, 'Balance'" & _
                        '        " FROM  Transaction_T_Tbl  AS D INNER JOIN " & _
                        '        " Gen_SchemeMaster_P_Tbl AS S ON D.Sch_Code = S.Sch_Code INNER JOIN" & _
                        '        " Cust_Master_M_Tbl AS C ON C.Cust_Id  = D.Acc_ID " & _
                        '        " WHERE (D.Tran_Type = 'C')" & _
                        '        " And D.Tr_Date >='" & Format(Convert.ToDateTime(TxtFromDate.Text), "MM/dd/yyyy") & "' " & _
                        '        " And D.Tr_Date <='" & Format(Convert.ToDateTime(TxtToDate.Text), "MM/dd/yyyy") & "' " & _
                        '        " And SM.Sch_Code='" & CmbScheme.SelectedItem.Value & "'" & _
                        '        " And SM.Br_Code='" & CmbBranch.SelectedItem.Value & "'"
                        'HeadingName = "DIVIDEND POSTED LIST"
                        'RptName = "Dividend_PostedList.rpt"

                        Dim HeaderArr(7, 7) As String
                        Dim HeaderWidth(7, 1) As String

                        HeaderArr(0, 0) = "SLNO"
                        HeaderArr(0, 1) = "L"
                        HeaderArr(0, 2) = "4"
                        HeaderArr(0, 3) = "4"

                        HeaderArr(1, 0) = "Member No"
                        HeaderArr(1, 1) = "L"
                        HeaderArr(1, 2) = "13"
                        HeaderArr(1, 3) = "13"

                        HeaderArr(2, 0) = "Admit Date"
                        HeaderArr(2, 1) = "L"
                        HeaderArr(2, 2) = "10"
                        HeaderArr(2, 3) = "10"

                        HeaderArr(3, 0) = "CustName"
                        HeaderArr(3, 1) = "L"
                        HeaderArr(3, 2) = "25"
                        HeaderArr(3, 3) = "13"


                        HeaderArr(4, 0) = "Accrued Divident"
                        HeaderArr(4, 1) = "L"
                        HeaderArr(4, 2) = "6"
                        HeaderArr(4, 3) = "6"

                        HeaderArr(5, 0) = "Div Percentage"
                        HeaderArr(5, 1) = "L"
                        HeaderArr(5, 2) = "6"
                        HeaderArr(5, 3) = "6"


                        HeaderArr(6, 0) = "Balance"
                        HeaderArr(6, 1) = "R"
                        HeaderArr(6, 2) = "10"
                        HeaderArr(6, 3) = "10"


                        Session("RptHeader") = HeaderArr

                        M_Sql = "Exec ShareDivPostedList_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "', " & ClsComm.DbDate(TxtFromDate.Text) & "," & ClsComm.DbDate(TxtTodate.Text) & ",'" & Session("LoginId") & "'"
                        Session("RptQry") = M_Sql
                        Response.Redirect("~/PrnView.aspx?PageType=80", False)
                        Exit Sub


                    ElseIf Rbt_InoperShare.Checked = True Then

                        Dim HeaderArr(4, 4) As String
                        Dim HeaderWidth(4, 1) As String

                        HeaderArr(0, 0) = "SLNO"
                        HeaderArr(0, 1) = "L"
                        HeaderArr(0, 2) = "4"
                        HeaderArr(0, 3) = "4"

                        HeaderArr(1, 0) = "Member No"
                        HeaderArr(1, 1) = "L"
                        HeaderArr(1, 2) = "13"
                        HeaderArr(1, 3) = "13"

                        HeaderArr(2, 0) = "CustName"
                        HeaderArr(2, 1) = "L"
                        HeaderArr(2, 2) = "50"
                        HeaderArr(2, 3) = "13"


                        HeaderArr(3, 0) = "Admit Date"
                        HeaderArr(3, 1) = "L"
                        HeaderArr(3, 2) = "13"
                        HeaderArr(3, 3) = "13"

                        HeaderArr(4, 0) = "Balance"
                        HeaderArr(4, 1) = "R"
                        HeaderArr(4, 2) = "13"
                        HeaderArr(4, 3) = "13"


                        Session("RptHeader") = HeaderArr

                        M_Sql = "Exec ShareOpeningClosingRpt_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "', " & ClsComm.DbDate(TxtFromDate.Text) & "," & ClsComm.DbDate(TxtTodate.Text) & ",'" & Session("LoginId") & "','" & Session("HostID") & "','I'"
                        Session("RptQry") = M_Sql
                        Response.Redirect("~/PrnView.aspx?PageType=80", False)

                        Exit Sub






                        'M_Sql = "select	ACM.ACC_ID, ACM.ACC_CustID, CM.Cust_Name, SM.Sm_AdmitDate ,SM.Sm_ResNo, SM.Sm_ResDate, ACM.ACC_No," & _
                        '        " dbo.Share_OutstngMemNo_Fn ('" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "',SM.Sch_Code ,SM.Sm_MemNo ,SM.Br_Code ) AS Sm_Balance," & _
                        '        " SM.Sch_Code,SM.Br_Code," & _
                        '        " SM.Sm_MemNo" & _
                        '        " from AccMain_M_Tbl ACM" & _
                        '        " INNER JOIN Cust_Master_M_Tbl CM ON ACM.ACC_CustID =CM.Cust_Id " & _
                        '        " INNER JOIN Share_Master_M_Tbl SM ON SM.Acc_ID = ACM.ACC_ID " & _
                        '        " WHERE acm.ACC_SchCode  = '" & CmbScheme.SelectedItem.Value & "' " & _
                        '        " AND ACM.ACC_BrCode ='" & CmbBranch.SelectedItem.Value & "'" & _
                        '        " and NOT Exists  (Select 1 From Transaction_T_Tbl T Where T.Acc_ID=ACM.Acc_ID" & _
                        '        " and T.Tr_Date BETWEEN '" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "'" & _
                        '        " AND  '" & Format(CDate(TxtTodate.Text), "MM/dd/yyyy") & "') And SM.Sm_AdmitDate<'" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "' "
                        'HeadingName = "IN-OPEERATIIVE SHARE LIST"
                        'RptName = "Inoperativee_ShareList.rpt"


                    End If
                End If


                StrSlf = "Company|" & Company_Name & "|Branch|" & CmbBranch.SelectedItem.ToString & "|Heading|" & CmbSection.SelectedItem.Text & " " & HeadingName & " REPORT FROM " & TxtFromDate.Text & " TO " & TxtTodate.Text & " |User_ID|" & Session("LoginID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

                Session.Add("RptQry", M_Sql)
                If Rbt_DivPostedList.Checked = True Then
                    Session.Add("RptPath", Server.MapPath("~/Reports/Divident/" & RptName))

                Else
                    Session.Add("RptPath", Server.MapPath("~/Reports/Share/" & RptName))

                End If

                Session.Add("RptFormula", StrSlf)
                If CmbOutput.SelectedIndex = 0 Then
                    Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
                Else
                    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
                End If
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
                LoggError(ex)
            End Try
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


    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

   
End Class