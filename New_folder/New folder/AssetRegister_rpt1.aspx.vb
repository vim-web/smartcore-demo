Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Imports CrystalDecisions.CrystalReports.Engine

Partial Public Class AssetRegister_RPT1
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1043
    Shared Obj_UserRights As Object
    Private Shared M_DBConn As Connection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                '  User(Rights)
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Txt_DateFrom.Text = Session("TransDate")
                Txt_DateTo.Text = Session("TransDate")

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Private Function ValidateControls() As Boolean
        Try
            If Date.Parse(Txt_DateFrom.Text) > Date.Parse(Txt_DateTo.Text) Then

                ShowAlertMessage("Incorrect Date entered Invalid Date ")
                Return False


            Else
                Return True
            End If


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Function
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            If ValidateControls() = False Then
                Exit Sub
            End If
            Dim M_Sql As String
            Dim StrSlf As String
            M_DBConn = New Connection()
            Dim Company_Name As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable
            Company_Name = HttpContext.Current.Cache("CompanyName")


            M_Sql = "Exec Rpt_Register_Sp 'PR'," & ClsComm.DbDate(Txt_DateFrom.Text, False) & "," & ClsComm.DbDate(Txt_DateTo.Text)
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            StrSlf = "Company|" & Company_Name & "|Branch|" & "CONSOLIDTED" & "|Heading|Purchase Register FROM " & Txt_DateFrom.Text & " TO " & Txt_DateTo.Text & " |User_ID|" & Session("UserID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            Session.Add("RptFormula", StrSlf)
            Session.Add("RptQry", M_Sql)
            Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/AssetRegisterRpt.rpt"))
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class