Imports SMARTCORE_BL
Partial Public Class DocList
    Inherits System.Web.UI.Page
    Dim M_Dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim M_DocCategoryBL As New DocCategoryBL
            M_DocCategoryBL.Fill_DOCCATG(CmbCategory)
            M_DocCategoryBL = Nothing



            TxtDate.Attributes.Add("readonly", "readonly")
            TxtDate.Text = Session("TransDate")
        End If
        Load_DocList()

    End Sub

    Private Sub Load_DocList()

        Dim TempDt As DataTable
        Dim M_Qry As String
        Dim M_ReportsBL As New ReportsBL
        Try
            'M_Qry = "SELECT DOC_ID,DOCCATG_NAME,DOC_TITLE,DOC_DESCRIPTION,DOC_USER,DOC_DATE FROM DOCUMENTMASTER,DOCCATEGORY WHERE DOC_CATG = DOCCATG_CODE order by doc_id,doc_date"

            M_Qry = "Exec Load_UserDocuments '" & Session("UserID") & "'"
            If ChkFilterCategory.Checked = True Then
                M_Qry = M_Qry + ",1," + CmbCategory.SelectedValue
            Else
                M_Qry = M_Qry + ",0,0"
            End If
            If ChkFilterSection.Checked = True Then
                M_Qry = M_Qry + ",1," + CmbSection.SelectedValue
            Else
                M_Qry = M_Qry + ",0,0"
            End If

            If ChkFilterDate.Checked = True Then
                M_Qry = M_Qry + ",1,'" + Convert.ToDateTime(TxtDate.Text).ToString("MM/dd/yyyy") + "'"
            Else
                M_Qry = M_Qry + ",0,'" + Convert.ToDateTime(TxtDate.Text).ToString("MM/dd/yyyy") + "'"
            End If



            M_Dt = New DataTable
            M_Dt = M_ReportsBL.ExecutQuery(M_Qry)
            'GvDoc.DataSource = M_Dt
            'GvDoc.DataBind()

            GridView1.DataSource = M_Dt
            GridView1.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub GvDoc_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GvDoc.RowDeleting

    '    Dim M_DocNo As String = CInt(GvDoc.DataKeys(e.RowIndex).Value)
    '    Dim Dt As New DataTable
    '    Try
    '        Dim M_DocumentMasterBL As New DocumentMasterBL
    '        Dt = M_DocumentMasterBL.Locate_file(M_DocNo)
    '        If Dt.Rows.Count > 0 Then
    '            Dim PageURl As String = "~/DocView.aspx?DocID=" + M_DocNo
    '            Response.Redirect(PageURl)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub CmbCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbCategory.SelectedIndexChanged, CmbSection.SelectedIndexChanged

        Load_DocList()
    End Sub

    Private Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim GrRow As GridViewRow = DirectCast(DirectCast(e.CommandSource, Control).Parent.Parent, GridViewRow)

        Dim M_DocNo As String = GrRow.Cells(0).Text
        Dim Dt As New DataTable
        Try
            Dim M_DocumentMasterBL As New DocumentMasterBL
            Dt = M_DocumentMasterBL.Locate_file(M_DocNo)
            If Dt.Rows.Count > 0 Then
                Dim PageURl As String = "~/DocView.aspx?DocID=" + M_DocNo
                Response.Redirect(PageURl)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.Header Then

            e.Row.Cells(0).Text = "ID"
            e.Row.Cells(1).Text = "CATEGORY"
            e.Row.Cells(2).Text = "Title"
            e.Row.Cells(3).Text = "DESCRIPTION"
            e.Row.Cells(4).Text = "User ID"
            e.Row.Cells(5).Text = "User Name"
            e.Row.Cells(6).Text = "Date"

            e.Row.Cells(0).Width = 70
            e.Row.Cells(1).Width = 150
            e.Row.Cells(2).Width = 150
            e.Row.Cells(3).Width = 150
            e.Row.Cells(4).Width = 220
            e.Row.Cells(5).Width = 300
            e.Row.Cells(6).Width = 150

            e.Row.BackColor = Drawing.Color.CornflowerBlue
            e.Row.ForeColor = Drawing.Color.AntiqueWhite

        ElseIf e.Row.RowType = DataControlRowType.DataRow Then

            Dim LnkSave As New LinkButton
            Dim MyCell As New TableCell
            LnkSave.Text = "Save"
            MyCell.Controls.Add(LnkSave)
            e.Row.Cells.AddAt(7, MyCell)

        End If

    End Sub

    Protected Sub TxtDate_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtDate.TextChanged
        Load_DocList()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class