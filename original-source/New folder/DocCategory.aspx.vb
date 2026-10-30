Imports SMARTCORE_BL

Partial Public Class DocCategory
    Inherits System.Web.UI.Page
    Dim M_DOCCATGCODE As Integer
    Dim M_EntryMode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


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

            Dim SrchTbl As New DataTable("Table")
            SrchTbl.Columns.Add("COLNM", GetType(String))
            SrchTbl.Columns.Add("HEADING", GetType(String))
            SrchTbl.Rows.Add("DOCCATG_NAME", "NAME")
            SrchTbl.Rows.Add("DOCCATG_CODE", "ID")
            Session("SearchColDtls") = SrchTbl

            Session("SearchReturn") = 2
            Session("SearchResult") = ""
            Session("SearchQuery") = "Select DOCCATG_NAME as Name,DOCCATG_CODE as ID from DOCCATEGORY"
        End If

    End Sub
    Private Function Validate_Data() As Boolean
        If Len(TxtName.Text.Trim) = 0 Then
            PnlMsg.Visible = True
            LblMsg.Text = "Invalid Category Name Entered"
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
        Dim M_DocCatg As New SMARTCORE_BO.DocCategoryBO
        With M_DocCatg
            .MAKERDATE = Now
            .CANCELDATE = "01/01/1900"
            .DOCCATGCODE = Val(TxtID.Text)
            .MAKERID = Session("UserID")
            .CANCELUSER = 0
            .DOCCATGNAME = TxtName.Text
            .STATUS = "O"
        End With
        Try
            Dim M_DocCategoryBL As New DocCategoryBL
            M_DOCCATGCODE = M_DocCategoryBL.Save_Data(M_DocCatg, Session("M_EntryMode"))
            M_DocCategoryBL = Nothing
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
            Session("M_EntryMode") = "NEW"
            TxtID.Text = "NEW"
            TxtID.Enabled = False
        Else
            Try
                Dim M_DocCategory As New SMARTCORE_BO.DocCategoryBO
                Dim M_DocCategoryBL As New DocCategoryBL
                M_DocCategory = M_DocCategoryBL.Locate_Data(TxtID.Text)
                If M_DocCategory.DOCCATGCODE = 0 Then
                    Session("M_EntryMode") = "NEW"
                    TxtName.Text = ""
                    TxtID.Text = "NEW"
                    TxtID.Enabled = False
                    Exit Sub
                End If
                With M_DocCategory
                    TxtID.Text = .DOCCATGCODE
                    TxtName.Text = .DOCCATGNAME
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
