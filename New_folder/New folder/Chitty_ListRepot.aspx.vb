Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common

Partial Public Class Chitty_ListRepot
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1024
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    End Sub

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            DtFrom.Text = Session("TransDate")
            M_CommonBL.Fill_Branch(DdlBranch)
            DdlBranch.SelectedValue = Session("DefaultBrCode")
            DdlBranch_SelectedIndexChanged(Me.DdlBranch, System.EventArgs.Empty)
            M_CommonBL = Nothing
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String
            HeaderArr(0, 0) = "No"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "Member No"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "Name"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "20"

            HeaderArr(3, 0) = "Address"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "20"
            HeaderArr(3, 3) = "20"

            HeaderArr(4, 0) = "Phone"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "15"
            HeaderArr(4, 3) = "15"

            HeaderArr(5, 0) = "Auction Amt"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "15"

            HeaderArr(6, 0) = "Auction Date"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "15"
            HeaderArr(6, 3) = "15"

            HeaderArr(7, 0) = "Paid Date"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "15"
            HeaderArr(7, 3) = "15"

            HeaderArr(8, 0) = "Balance"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "20"
            HeaderArr(8, 3) = "13"

            HeaderArr(9, 0) = ""
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "5"
            HeaderArr(9, 3) = "6"

            Session("RptHeader") = HeaderArr

            Dim M_Sql As String
            M_Sql = "Exec Chitty_List_Sp " & DdlScheme.SelectedValue & ",'" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "'"
            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=130", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DdlBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlBranch.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_ChittyGroup(DdlScheme, , DdlBranch.SelectedValue)
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class