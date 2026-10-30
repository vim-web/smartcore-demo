Imports SMARTCORE_BL
Public Class MailList
    Inherits System.Web.UI.Page
    Dim M_CommonBl As New CommonBL
    Dim FormID As Integer = 1103
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
            TxtFrDate.Attributes.Add("readonly", "readonly")
            TxtToDate.Attributes.Add("readonly", "readonly")
            TxtFrDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
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
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
        CmbScheme.SelectedValue = TxtScheme.Text
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim M_Dt As DataTable = New DataTable()

            Dim HeaderArr(4, 4) As String
            Dim HeaderWidth(4, 1) As String

            HeaderArr(0, 0) = "ADDRESS"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "60"
            HeaderArr(0, 3) = "60"


            HeaderArr(1, 0) = "ADDRESS"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "60"
            HeaderArr(1, 3) = "60"

            HeaderArr(2, 0) = "ADDRESS"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "60"
            HeaderArr(2, 3) = "60"

            HeaderArr(3, 0) = "ADDRESS"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "60"
            HeaderArr(3, 3) = "60"

            Dim DtFrom As Date
            Dim DtTo As Date
            DtFrom = TxtFrDate.Text
            DtTo = TxtToDate.Text

            Session("RptHeader") = HeaderArr
            M_Sql = "Exec MailList_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "','" & Format(DtFrom, "MM/dd/yyyy") & "','" & Format(DtTo, "MM/dd/yyyy") & "'"
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