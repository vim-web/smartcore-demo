Public Class Sangam_Detls
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            TxtId.Text = "NEW"
            TxtDate.Text = Session("TransDate")
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Session("SearchType") = "SANGAM"
        End If
        If Session("SearchResult") <> "" Then
            TxtId.Text = Session("SearchResult")
            TxtId_TextChanged(Me.TxtId, System.EventArgs.Empty)
            TxtId.Focus()
            Session("SearchResult") = ""
        End If
        Session("SearchResult") = ""



    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_SangamDetlsBO As New SMARTCORE_BO.SangamDetlsBO
            With M_SangamDetlsBO
                .Id = Val(TxtId.Text)
                .FormationDate = TxtDate.Text
                .Name = TxtName.Text
                .Address1 = TxtAddress1.Text
                .Address2 = TxtAddress2.Text
                .Address3 = TxtAddress3.Text
                .BrCode = Session("DefaultBrCode")
            End With

            Dim M_SangamDetlsBL As New SMARTCORE_BL.SangamDetlsBL

            Session("M_EntryMode") = TxtId.Text

            M_SangamDetlsBL.Save_Data(M_SangamDetlsBO, Session("M_EntryMode"))

            ShowAlertMessage("Successfully Saved")

            ClearData()
            TxtId.Text = "NEW"

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

    Protected Sub TxtId_TextChanged(sender As Object, e As EventArgs) Handles TxtId.TextChanged
        If TxtId.Text.Length = 0 Then
            Call ClearData()
            Session("M_EntryMode") = "NEW"
        End If
        Dim M_SangamDetlsBl As New SMARTCORE_BL.SangamDetlsBL
        Dim M_SangamDetls As New SMARTCORE_BO.SangamDetlsBO
        Dim M_Id = TxtId.Text
        M_SangamDetls = M_SangamDetlsBl.Locate_Data(M_Id)
        Locate_Data(M_SangamDetls)
        With M_SangamDetls

        End With
    End Sub
    Private Sub Locate_Data(ByVal M_SangamDetls As SMARTCORE_BO.SangamDetlsBO)
        On Error Resume Next

        If M_SangamDetls.Id = 0 Then
            Session("M_EntryMode") = "NEW"
            TxtId.Text = "NEW"
            ClearData()
            Exit Sub
        End If
        With M_SangamDetls
            TxtName.Text = .Name
            TxtAddress1.Text = .Address1
            TxtAddress2.Text = .Address2
            TxtAddress3.Text = .Address3
            TxtDate.Text = .FormationDate
        End With
    End Sub
    Private Sub ClearData()
        TxtName.Text = ""
        TxtId.Text = "NEW"
        TxtAddress1.Text = ""
        TxtAddress2.Text = ""
        TxtAddress3.Text = ""
        TxtDate.Text = ""

    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        ClearData()
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

End Class