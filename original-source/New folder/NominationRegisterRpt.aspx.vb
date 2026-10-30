Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class NominationRegisterRpt
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
            M_CommonBL.Fill_Section(DrpSection)
            M_CommonBL.GetSchemes(DrpScheme, Session("TransDate"), DrpSection.SelectedItem.ToString)
            Call DrpSection_SelectedIndexChanged(Me.DrpSection, System.EventArgs.Empty)
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

            Dim HeaderArr(7, 4) As String
            Dim HeaderWidth(7, 1) As String
            HeaderArr(0, 0) = "ACC NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "13"
            HeaderArr(0, 3) = "13"

            HeaderArr(1, 0) = "NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "SCHEME"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "DATE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "NOM NAME"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "15"
            HeaderArr(4, 3) = "15"

            HeaderArr(5, 0) = "DESCRIPTION"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "15"

            HeaderArr(6, 0) = "ADDRESS"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "20"
            HeaderArr(6, 3) = "20"

            Dim SchCode As String
            If DrpScheme.SelectedValue = "ALL" Then
                SchCode = "0"
            Else
                SchCode = DrpScheme.SelectedValue
            End If

            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC NominationRegister_Sp'" & CmbBranch.SelectedValue & "','" & DrpSection.SelectedItem.Text & "','" & SchCode & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtToDate.Text) & ""
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub DrpSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpSection.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        M_CommonBL.GetSchemes(DrpScheme, Session("TransDate"), DrpSection.SelectedItem.ToString)
        DrpScheme.Items.Add("ALL")
    End Sub
End Class