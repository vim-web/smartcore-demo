Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common

Partial Public Class AgentCollectionList
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1006
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            ' CmbOutput.SelectedIndex = 1
            Load_DefaultValues()


        End If
    End Sub




    Public Sub Load_DefaultValues()
        'Txt_FrDate.Attributes.Add("readonly", "readonly")
        'Txt_FrDate.Attributes.Add("readonly", "readonly")
        Opt_Detailed.Checked = True
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(Ddl_Branch)
        Ddl_Branch.SelectedValue = Session("DefaultBrCode")
        M_CommonBL.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, Ddl_Branch.SelectedValue)
        M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), "DEPOSIT", "DD")
        Ddl_Scheme.Items.Add("<--ALL-->")
        M_CommonBL = Nothing
        Dim RptType As String
        RptType = Request.QueryString("RptType")

        If RptType = "CANCELLATION REPORT" Then
            CmbOutput.SelectedValue = "CANCELLATION REPORT"
        ElseIf RptType = "COMMISSION POSTED LIST" Then
            CmbOutput.SelectedValue = "COMMISSION POSTED LIST"
        End If

    End Sub

    
    Private Function ValidateFields() As Boolean
        Dim Status As Boolean






        If Val(Txt_FrDate.Text.Length) > 0 Then
            Status = True
        Else
            ShowAlertMessage(" Invalid Date")
            Txt_FrDate.Focus()
            Return False
        End If

        If Val(Txt_ToDate.Text.Length) > 0 Then
            Status = True
        Else
            ShowAlertMessage(" Invalid Date")
            Txt_ToDate.Focus()
            Return False
        End If



        Return Status


    End Function

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    

    'Protected Sub BtnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnClose.Click
    '    Response.Redirect("~/Main.aspx")
    'End Sub

    'Protected Sub BtnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnShow.Click
    '    If ValidateFields() = False Then
    '        Exit Sub
    '    End If



    '    Dim M_ReportBl As New ReportsBL
    '    Dim M_Sql As String
    '    Dim ClsComm As New CommonClass()
    '    Dim M_Dt As New DataTable()
    '    Dim M_CommonDa As New CommonDA
    '    Dim Company_Name As String
    '    Dim StrSlf As String







    '    M_DBConn = New Connection()
    '    Company_Name = M_CommonDa.GetCompanyDetails()

    '    Dim Terminal = System.Environment.MachineName 'Request.ServerVariables("REMOTE_HOST")

    '    If Opt_Detailed.Checked = True Then

    '        M_Sql = "Exec Proc_Deposit_AgentCollectionList '" & Ddl_Branch.SelectedValue.ToString() & "','" & IIf(Ddl_Scheme.SelectedItem.Text = "<--ALL-->", "0", Ddl_Scheme.SelectedValue.ToString()) & "','" & CmbAgent.SelectedValue & "'," & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) '& ",  'N','" & Terminal & "'"

    '        M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

    '        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

    '        Session.Add("RptQry", M_Sql)
    '        Session.Add("RptPath", Session("File_Path") & "\" & "REPORTS\Deposit\Dep_AgentCollectionList.rpt")




    '        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Agent Collection List From : " & Txt_FrDate.Text & " To " & Txt_ToDate.Text & " |User_ID|" & Session("LoginID")
    '        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

    '        Session.Add("RptFormula", StrSlf)
    '    ElseIf Opt_Summary.Checked = True Then
    '        M_Sql = "Exec Proc_Deposit_AgentCollectionList '" & Ddl_Branch.SelectedValue.ToString() & "','" & IIf(Ddl_Scheme.SelectedItem.Text = "<--ALL-->", "0", Ddl_Scheme.SelectedValue.ToString()) & "','" & CmbAgent.SelectedValue & "'," & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) '& ",  'N','" & Terminal & "'"

    '        M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

    '        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

    '        Session.Add("RptQry", M_Sql)
    '        Session.Add("RptPath", Session("File_Path") & "\" & "REPORTS\Deposit\Dep_AgentCollectionList_Summary.rpt")




    '        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Agent Collection List From : " & Txt_FrDate.Text & " To " & Txt_ToDate.Text & " |User_ID|" & Session("LoginID")
    '        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

    '        Session.Add("RptFormula", StrSlf)

    '    End If

    '    If CmbOutput.SelectedItem.Text = "Screen" Then
    '        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
    '    Else
    '        Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
    '    End If



    '    ' Response.Redirect("~/RptView.aspx", False)

    'End Sub

    Protected Sub CmbAgent_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbAgent.SelectedIndexChanged

    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
    
        ' Response.Redirect("~/RptView.aspx", False)
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub Ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, Ddl_Branch.SelectedValue)
        M_CommonBL = Nothing
    End Sub

    Protected Sub ImageButton2_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click, ImageButton2.Click, ImageButton2.Click
        If ValidateFields() = False Then
            Exit Sub
        End If



        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New CommonDA
        Dim Company_Name As String
        Dim StrSlf As String


        If CmbOutput.SelectedItem.Text = "CANCELLATION REPORT" Then


            Dim HeaderArr(5, 4) As String
            Dim HeaderWidth(5, 1) As String
            HeaderArr(0, 0) = "DATE"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "8"

            HeaderArr(1, 0) = "NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "30"
            HeaderArr(1, 3) = "30"

            HeaderArr(2, 0) = "ACCOUNT NO"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "AMOUNT"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "8"

            HeaderArr(4, 0) = "AGENT NAME"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "13"



            Session("RptHeader") = HeaderArr



            M_Sql = "EXEC AgentCancelledListl_Sp '" & Ddl_Branch.SelectedValue & "','" & CmbAgent.SelectedValue & "'," & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Session("UserID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

            Exit Sub
        End If

        If CmbOutput.SelectedItem.Text = "COMMISSION POSTED LIST" Then


            Dim HeaderArr(5, 4) As String
            Dim HeaderWidth(5, 1) As String
            HeaderArr(0, 0) = "ACCOUNT DETAILS"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "45"
            HeaderArr(0, 3) = "45"

            HeaderArr(1, 0) = "AMOUNT CR"
            HeaderArr(1, 1) = "R"
            HeaderArr(1, 2) = "12"
            HeaderArr(1, 3) = "12"

            HeaderArr(2, 0) = "AMOUNT DR"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "12"
            HeaderArr(2, 3) = "12"


            Session("RptHeader") = HeaderArr


            M_Sql = "Exec [CommPostedStatement_SP] " & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & IIf(Ddl_Branch.SelectedItem.Text = "<--ALL-->", "0", Ddl_Branch.SelectedValue) & "','" & CmbAgent.SelectedValue & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

            Exit Sub
        End If



        M_DBConn = New Connection()
        Company_Name = M_CommonDa.GetCompanyDetails()

        Dim Terminal = System.Environment.MachineName 'Request.ServerVariables("REMOTE_HOST")

        If Opt_Detailed.Checked = True Then

            M_Sql = "Exec Proc_Deposit_AgentCollectionList '" & Ddl_Branch.SelectedValue.ToString() & "','" & IIf(Ddl_Scheme.SelectedItem.Text = "<--ALL-->", "0", Ddl_Scheme.SelectedValue.ToString()) & "','" & CmbAgent.SelectedValue & "'," & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) '& ",  'N','" & Terminal & "'"

            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Session.Add("RptQry", M_Sql)
            Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/Dep_AgentCollectionList.rpt"))




            StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Agent Collection List From : " & Txt_FrDate.Text & " To " & Txt_ToDate.Text & " |User_ID|" & Session("LoginID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

            Session.Add("RptFormula", StrSlf)
        ElseIf Opt_Summary.Checked = True Then
            M_Sql = "Exec Proc_Deposit_AgentCollectionList '" & Ddl_Branch.SelectedValue.ToString() & "','" & IIf(Ddl_Scheme.SelectedItem.Text = "<--ALL-->", "0", Ddl_Scheme.SelectedValue.ToString()) & "','" & CmbAgent.SelectedValue & "'," & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) '& ",  'N','" & Terminal & "'"

            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Session.Add("RptQry", M_Sql)
            Session.Add("RptPath", Server.MapPath("~/Reports/Deposit/Dep_AgentCollectionList_Summary.rpt"))




            StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Agent Collection List From : " & Txt_FrDate.Text & " To " & Txt_ToDate.Text & " |User_ID|" & Session("LoginID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

            Session.Add("RptFormula", StrSlf)

        End If
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
        'If CmbOutput.SelectedItem.Text = "Screen" Then

        'Else
        '    Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
        'End If



    End Sub
End Class