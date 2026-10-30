Imports SMARTCORE_BL
Public Class SignaturePending
    Inherits System.Web.UI.Page
    Dim M_CommonBl As New CommonBL
    Dim FormID As Integer = 1130
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
    Private Function Load_DefaultValues()
        Try
            M_CommonBl.Fill_Branch(CmbBranch)
            M_CommonBl.GetSchemes(CmbScheme, Session("TransDate"))
            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
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

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim M_Dt As DataTable = New DataTable()

            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String

            HeaderArr(0, 0) = "SL.NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "7"
            HeaderArr(0, 3) = "7"


            HeaderArr(1, 0) = "ACC NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "14"
            HeaderArr(1, 3) = "14"


            HeaderArr(2, 0) = "OPEN DATE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "10"


            HeaderArr(3, 0) = "CUST ID"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "9"
            HeaderArr(3, 3) = "9"


            HeaderArr(4, 0) = "NAME"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "37"
            HeaderArr(4, 3) = "37"

            HeaderArr(5, 0) = "ACC STATUS"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "2"
            HeaderArr(5, 3) = "2"

            Session("RptHeader") = HeaderArr
            M_Sql = "Exec SignaturePendingAcc_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "'"
            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=130", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class