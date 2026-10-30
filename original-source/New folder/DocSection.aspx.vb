Imports SMARTCORE_BL

Partial Public Class DocSection
    Inherits System.Web.UI.Page
    Dim M_DOCSECTIONCODE As Integer
    Dim M_EntryMode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("LoginID") = "" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session("Access_DMS") <> "Y" Then
            Response.Redirect("~/AccessDenied.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Session("M_EntryMode") = "NEW"
            ImgSrch.Attributes.Add("OnClick", "window.open('Search.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If
        If Session("SearchResult") <> "" Then
            TxtID.Text = Session("SearchResult")
            Locate_Data()
            TxtID.Focus()
            Session("SearchResult") = ""
        Else
            Session("SearchResult") = ""
            Session("SearchQuery") = "Select DOCSECTION_NAME as Name,DOCSECTION_CODE as ID from DOCSECTION ORDER BY DOCSECTION_NAME"
        End If

    End Sub
    Private Function Validate_Data() As Boolean
        If Len(TxtName.Text.Trim) = 0 Then
            PnlMsg.Visible = True
            LblMsg.Text = "Invalid Section Name Entered"
            Validate_Data = False
            TxtName.Focus()
            Exit Function
        End If
        Validate_Data = True
    End Function
    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdSave.Click
        If Validate_Data() = False Then Exit Sub
        CmdSave.Enabled = False
        PnlMsg.Visible = False
        Dim M_DOCSECTION As New SMARTCORE_BO.DocSectionBO
        With M_DOCSECTION
            .MAKERDATE = Now
            .CANCELDATE = "01/01/1900"
            .DOCSECTIONCODE = Val(TxtID.Text)
            .MAKERID = Session("UserID")
            .CANCELUSER = 0
            .DOCSECTIONNAME = TxtName.Text
            .STATUS = "O"
        End With
        Try
            Dim M_DocSectionBL As New DocSectionBL
            M_DOCSECTIONCODE = M_DocSectionBL.Save_Data(M_DOCSECTION, Session("M_EntryMode"))
            M_DocSectionBL = Nothing
            ClearAll()
            TxtID.Focus()
            CmdSave.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ClearAll()
        TxtID.Text = ""
        TxtName.Text = ""
        Session("M_EntryMode") = "NEW"
        PnlMsg.Visible = False
        LblMsg.Text = ""
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdClose.Click
        ClearAll()
        TxtID.Enabled = True
        TxtID.Focus()
    End Sub

    Protected Sub ImgSrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSrch.Click
        Session("SearchCtrl") = TxtID
        Session("SearchRtnCol") = 2
        TxtID.Text = Session("SearchResult")
    End Sub

    Protected Sub TxtID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtID.TextChanged
        Locate_Data()
    End Sub
    Private Sub Locate_Data()
        If TxtID.Text.Length = 0 Then
            Call ClearAll()
            TxtName.Text = ""
            TxtID.Text = "NEW"
            TxtID.Enabled = False
        Else
            Try
                Dim M_DocCategory As New SMARTCORE_BO.DocSectionBO
                Dim M_DocCategoryBL As New DocSectionBL
                M_DocCategory = M_DocCategoryBL.Locate_Data(TxtID.Text)
                If M_DocCategory.DOCSECTIONCODE = 0 Then
                    Session("M_EntryMode") = "NEW"
                    TxtName.Text = ""
                    TxtID.Text = "NEW"
                    TxtID.Enabled = False
                    Exit Sub
                End If
                With M_DocCategory
                    TxtID.Text = .DOCSECTIONCODE
                    TxtName.Text = .DOCSECTIONNAME
                End With
                Session("M_EntryMode") = "VIEW"
                M_DocCategoryBL = Nothing
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub Show_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Show.Click
        Locate_Data()
    End Sub
End Class