


Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common

Partial Public Class DailyCashRpt
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
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            ' CmbOutput.SelectedIndex = 1
            Load_DefaultValues()

            If Session("DC_RPTS_BRANCH") IsNot Nothing Then

                Txt_FrDate.Text = Session("DC_RPTS_FROM_DATE")
                Txt_ToDate.Text = Session("DC_RPTS_TO_DATE")
                Ddl_Branch.SelectedValue = Session("DC_RPTS_BRANCH")
                Ddl_Branch.SelectedItem.Text = Ddl_Branch.SelectedItem.Text

                If (Session("DC_RPTS_OPTION").ToString() = "1") Then
                    ChkTotal.Checked = True
                Else
                    ChkTotal.Checked = False
                End If


            End If



        End If
    End Sub




    Public Sub Load_DefaultValues()

        Try

    
        'Txt_FrDate.Attributes.Add("readonly", "readonly")
        'Txt_FrDate.Attributes.Add("readonly", "readonly")
        Txt_FrDate.Text = Session("TransDate")
        Txt_ToDate.Text = Session("TransDate")
        Opt_Detailed.Checked = True
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(Ddl_Branch)
        'UAT
        Ddl_Branch.Items.Add("ALL")
        Ddl_Branch.SelectedValue = Session("DefaultBrCode")
        M_CommonBL.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, Ddl_Branch.SelectedValue)

        M_CommonBL = Nothing
        Dim RptType As String
        RptType = Request.QueryString("RptType")

        Catch ex As Exception

        End Try

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




    Protected Sub CmbAgent_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbAgent.SelectedIndexChanged

    End Sub



    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub Ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged

    End Sub

    Protected Sub ImageButton2_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click, ImageButton2.Click, ImageButton2.Click


        Try

       
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


        Session("DC_RPTS_FROM_DATE") = Txt_FrDate.Text
        Session("DC_RPTS_TO_DATE") = Txt_ToDate.Text
        Session("DC_RPTS_BRANCH") = Ddl_Branch.SelectedValue.ToString()
        Session("DC_RPTS_OPTION") = IIf(ChkTotal.Checked = True, "1", "0")


        M_Sql = "Exec DailyCash_SP '" & Ddl_Branch.SelectedValue.ToString() & "'," & ClsComm.DbDate(Txt_FrDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'" & Session("LoginID") & "','" & IIf(ChkTotal.Checked, "Y", "N") & "'"



        Session.Add("RptQry", M_Sql)

        If ChkTotal.Checked Then
            Session.Add("RptPath", Server.MapPath("~/Reports/CASHIER_MODULE/CashTotal.rpt"))
        Else
            Session.Add("RptPath", Server.MapPath("~/Reports/CASHIER_MODULE/CASH.rpt"))
        End If


        StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Cash Scroll From : " & Txt_FrDate.Text & " To " & Txt_ToDate.Text & " |User_ID|" & Session("LoginID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

        Session.Add("RptFormula", StrSlf)


        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)



        'M_Sql = "EXEC  Process_AccLedgerAll_Sp " & ClsComm.DbDate("01/01/2014") & "," & ClsComm.DbDate("03/03/2014") & ",'" & Ddl_Branch.SelectedValue & "','" & Session("UserID") & "','N','N'"
        'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
        'StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.Text & "|Heading| ALL GL  REPORT FROM "
        'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
        'Session.Add("RptQry", M_Sql)
        'Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/RandDAllHeads.rpt"))
        'Session.Add("RptFormula", StrSlf)

        'Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

        'Exit Sub

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Txt_ToDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_ToDate.TextChanged
        ChkTotal.Text = "Cash  On: " + Txt_ToDate.Text
    End Sub
End Class