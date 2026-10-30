Imports SMARTCORE_BL
Imports SMARTCORE_Common

Public Class TranCancelRpt
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
            'Cmb_Branch.Items.Add("ALL")
            'Cmb_Branch.SelectedValue = Session("DefaultBrCode")



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

    Protected Sub CmdShow_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim M_Dt As New DataTable()

        Dim HeaderArr(11, 4) As String
        Dim HeaderWidth(11, 1) As String
        HeaderArr(0, 0) = "DATE"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "5"
        HeaderArr(0, 3) = "15"

        HeaderArr(1, 0) = "ACC NO"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "15"
        HeaderArr(1, 3) = "10"

        HeaderArr(2, 0) = "VR NO"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "5"
        HeaderArr(2, 3) = "10"

        HeaderArr(3, 0) = "TRAN TYPE"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "5"
        HeaderArr(3, 3) = "10"

        HeaderArr(4, 0) = "CASH"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "10"
        HeaderArr(4, 3) = "15"

        HeaderArr(5, 0) = "TRANSFER"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "15"

        HeaderArr(6, 0) = "MAKER ID"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "10"
        HeaderArr(6, 3) = "15"

        HeaderArr(7, 0) = "Making TIME"
        HeaderArr(7, 1) = "L"
        HeaderArr(7, 2) = "10"
        HeaderArr(7, 3) = "10"

        HeaderArr(8, 0) = "CHECKING TIME"
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "10"
        HeaderArr(8, 3) = "10"

        HeaderArr(9, 0) = "NARRATION"
        HeaderArr(9, 1) = "L"
        HeaderArr(9, 2) = "10"
        HeaderArr(9, 3) = "10"

        HeaderArr(10, 0) = "DELETED ID"
        HeaderArr(10, 1) = "L"
        HeaderArr(10, 2) = "5"
        HeaderArr(10, 3) = "10"
        Session("RptHeader") = HeaderArr

        M_Sql = "EXEC TranCancelationRpt_Sp'" & CmbBranch.SelectedValue & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class