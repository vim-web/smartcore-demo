
Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Partial Public Class InvestmenrIntRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1136
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
                TxtEffDate.Text = Session("TransDate")
                HdnDate.Value = Session("TransDate")

              


            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try

            Dim HeaderArr(12, 4) As String
            Dim HeaderWidth(12, 1) As String


            HeaderArr(0, 0) = "SLNO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "ACCNO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "20"
            HeaderArr(1, 3) = "15"

            HeaderArr(2, 0) = "INVESTAMOUNT"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "25"
            HeaderArr(2, 3) = "20"

            HeaderArr(3, 0) = "INTEREST RECEIVED"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "25"
            HeaderArr(3, 3) = "20"

            HeaderArr(4, 0) = "BALANCE INTEREST"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "25"
            HeaderArr(4, 3) = "20"

            Session("RptHeader") = HeaderArr

            Dim M_Sql
            M_Sql = "Exec [Investment_Int_Sp] '" & Format(CDate(TxtEffDate.Text), "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        Catch ex As Exception

        End Try
    End Sub
End Class