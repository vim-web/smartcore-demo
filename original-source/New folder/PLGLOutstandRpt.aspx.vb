Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class PLGLOutstandRpt
    Inherits System.Web.UI.Page
    Shared Obj_UserRights As Object
    Dim FormID As Integer = 2123

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



                'M_DBConn = New Connection
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click

        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim clscomm As New CommonClass

            Dim Br_code As String = DdlBranch.SelectedValue
            Dim From_Date As String = DtFrom.Text
            Dim To_Date As String = DtTo.Text
            Dim User_Id As String = Session("UserID")
           


            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            Dim HeaderArr(11, 4) As String
            Dim HeaderWidth(11, 1) As String

            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "R"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "Branch Name"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "Cust ID"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "Customer Name"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "13"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "Acc No"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "GL Net Wt"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "15"

            HeaderArr(6, 0) = "Bank Rate"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"

            HeaderArr(7, 0) = "GL Loan Amount"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "5"

            HeaderArr(8, 0) = "GL Outstand"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "12"
            HeaderArr(8, 3) = "12"

            HeaderArr(9, 0) = "PL Acc No"
            HeaderArr(9, 1) = "R"
            HeaderArr(9, 2) = "12"
            HeaderArr(9, 3) = "12"

            HeaderArr(10, 0) = "PL Loan Amount"
            HeaderArr(10, 1) = "R"
            HeaderArr(10, 2) = "12"
            HeaderArr(10, 3) = "12"

            HeaderArr(11, 0) = "PL Outstand"
            HeaderArr(11, 1) = "R"
            HeaderArr(11, 2) = "12"
            HeaderArr(11, 3) = "12"

           

            M_Sql = "EXEC Get_PLGLOutstand_Rpt '" & DdlBranch.SelectedValue & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("UserID") & "','" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click

        Response.Redirect("~/Main.aspx")

    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
End Class