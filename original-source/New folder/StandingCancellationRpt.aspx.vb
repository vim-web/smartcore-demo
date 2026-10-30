Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class StandingCancellationRpt
    Inherits System.Web.UI.Page

    Dim FormID As Integer = 1142
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
                CmbBranch.SelectedValue = Session("DefaultBrCode")

                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)


            TxtFromDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")

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
            Dim M_Sql As String
            Dim M_Dt As New DataTable()

            Dim HeaderArr(5, 4) As String
            Dim HeaderWidth(5, 1) As String
            HeaderArr(0, 0) = "DEBITACC"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "20"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "CREDITACC"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "20"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "CANCELLEDBY"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "DATE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "20"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "TIME"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "20"


            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC StandingCancellationrpt_Sp'" & CmbBranch.SelectedValue & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class