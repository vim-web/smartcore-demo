Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class LoanRisk
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Dim FormID As Integer = 1152
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If


                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Cmb_Branch)
            M_CommonBL.GetSchemes(Cmb_Scheme, Session("TransDate"), ("LOAN"), ("GOLDLOAN"))
            Txt_FrDate.Text = Session("TransDate")
            Cmb_Branch.SelectedValue = Session("DefaultBrCode")
            Cmb_Scheme.Items.Add("<--ALL-->")
            Cmb_Branch.Items.Add("<--ALL-->")
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

    Protected Sub Cmd_Show_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Show.Click
        Dim M_ReportBl As New ReportsBL
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim StrSlf As String
        Dim M_CommonDa As New CommonDA

        Dim Brcode As String
        Dim SchemeCode As String
        If Cmb_Branch.SelectedValue <> "<--ALL-->" Then
            Brcode = Cmb_Branch.SelectedValue
        Else
            Brcode = "0"
        End If
        If Cmb_Scheme.SelectedValue <> "<--ALL-->" Then
            SchemeCode = Cmb_Scheme.SelectedValue
        Else
            SchemeCode = "0"
        End If


        Dim Dt As Date
        Dt = Txt_FrDate.Text
        M_Sql = "Exec MIS_LoanRisk_Sp '" & Brcode & "','" & SchemeCode & "'," & ClsComm.DbDate(Txt_FrDate.Text) & "," & TxtGoldRate.Text
        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & Cmb_Branch.SelectedItem.ToString & "|Heading|Gold Loan Risk Analysis on Gold Rate  Rs." & TxtGoldRate.Text & " as on " & Txt_FrDate.Text & "|User_ID|" & Session("UserID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/MISLoanRisk.rpt"))
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

    End Sub

    Protected Sub Cmd_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Cmd_Close.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class