Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Public Class BranchDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights       

            Dim M_Comm As New CommonBL
            'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            'M_Comm = Nothing
            'If Obj_UserRights.View_Right = False Then
            '    Response.Redirect("~/UnauthorisedAcces.ASPX")
            'End If

            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(DrpBranch)
            DrpBranch.Items.Insert(0, "ALL")

        End If

    End Sub
    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String

            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String
            HeaderArr(0, 0) = "BR_CODE"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "BR_NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "20"

            HeaderArr(2, 0) = "ADDRESS"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "20"

            HeaderArr(3, 0) = "CONTACT.NO:"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "12"
            HeaderArr(3, 3) = "12"

            HeaderArr(4, 0) = "EMAIL"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "20"

            HeaderArr(5, 0) = "REGION NAME"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "20"


            HeaderArr(6, 0) = "AREA NAME"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "20"

            HeaderArr(7, 0) = "AREA MANAGER"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "15"
            HeaderArr(7, 3) = "15"

            HeaderArr(8, 0) = "REGION MANAGER"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "15"
            HeaderArr(8, 3) = "15"

            Session("RptHeader") = HeaderArr
            Dim Branch As String
            If DrpBranch.SelectedItem.Text = "ALL" Then
                Branch = "ALL"
            Else
                Branch = DrpBranch.SelectedValue
            End If
            M_Sql = "Exec Branch_Details_Sp '" & Branch & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

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
End Class