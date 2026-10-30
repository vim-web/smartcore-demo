Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class GoldRedemptionReport
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1068
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
                DdlBranch.Items.Add("<--ALL-->")
                DdlBranch.SelectedValue = Session("DefaultBrCode")



                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")



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
        



        Dim Dt As Date
        Dt = DtFrom.Text
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        M_Sql = "Exec GoldRedemptionRpt_Sp " & ClsComm.DbDate(DtFrom.Text) & "," & ClsComm.DbDate(DtTo.Text) & ",'" & Session("UserID") & "','" & Session("HostID") & "','" & DdlBranch.SelectedValue & "'"
        StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|Gold Stock Statement from " & DtFrom.Text & " to " & DtTo.Text & "|User_ID|" & Session("UserID")
        StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
        Session.Add("RptQry", M_Sql)
        Session.Add("RptFormula", StrSlf)
        Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/GoldStkStmt.rpt"))
        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class