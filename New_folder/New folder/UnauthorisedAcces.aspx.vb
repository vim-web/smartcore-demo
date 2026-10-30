Public Partial Class UnauthorisedAcces
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1147

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/smartlogin.ASPX")
            Exit Sub
        End If
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

        Response.Redirect("~/Main.ASPX")

    End Sub
End Class