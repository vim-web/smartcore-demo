Imports SMARTCORE_BL
Public Class RDPendingInst

    Inherits System.Web.UI.Page
    Dim M_CommonBl As New CommonBL
    Dim FormID As Integer = 1120
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
            TxtAsonDt.Attributes.Add("readonly", "readonly")
            TxtAsonDt.Text = Session("TransDate")
            M_CommonBl.Fill_Branch(CmbBranch)
            M_CommonBl.GetSchemes(CmbScheme, Session("TransDate"), "DEPOSIT", "RD")
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

            Dim HeaderArr(8, 4) As String
            Dim HeaderWidth(8, 1) As String

            HeaderArr(0, 0) = "SLNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "ACCNO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "OPEN DATE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "10"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "DUE DATE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "NAME"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "40"
            HeaderArr(4, 3) = "40"

            HeaderArr(5, 0) = "INST AMT"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "BALANCE"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"

            HeaderArr(7, 0) = "PENDING"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "10"


            Dim Dt As Date
            Dt = TxtAsonDt.Text
            Session("RptHeader") = HeaderArr
            M_Sql = "Exec RdPendingInst_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "','" & Format(Dt, "MM/dd/yyyy") & "','" & Session("HostID").ToString() & "'"
            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class