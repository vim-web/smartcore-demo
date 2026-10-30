Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Imports CrystalDecisions.CrystalReports.Engine
Partial Public Class AssetDepreciation_rpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1043
    Shared Obj_UserRights As Object
    Private Shared M_DBConn As Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '  User(Rights)
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Txt_DateFrom.Text = Session("TransDate")
           
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim StrSlf As String
            M_DBConn = New Connection()
            Dim Company_Name As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable
            Company_Name = HttpContext.Current.Cache("CompanyName")


            M_Sql = "Exec Rpt_Depreciation_Sp " & ClsComm.DbDate(Txt_DateFrom.Text, False) & ""
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            StrSlf = "Company|" & Company_Name & "|Branch|" & "CONSOLIDTED" & "|Heading|Depreciation As On " & Txt_DateFrom.Text & " |User_ID|" & Session("UserID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            Session.Add("RptFormula", StrSlf)
            Session.Add("RptQry", M_Sql)
            Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/AssetDepreciationRpt.rpt"))
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
End Class