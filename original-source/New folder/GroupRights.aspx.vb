Imports SMARTCORE_BL
Imports System.Drawing.Drawing2D
Imports System.Drawing
Partial Public Class GroupRights
    Inherits System.Web.UI.Page
    Dim M_UserBL As New UserBL()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                DefaultValue()
            End If

        Catch ex As Exception

        End Try
    End Sub



    Public Sub DefaultValue()
        Try
            Dim M_CommanBL As New CommonBL()
            M_CommanBL.Fill_Pickup(Drop_Group, 69, 0)
            Drop_Group.SelectedIndex = 0
            ViewState("index") = "0"
            Dim DtTab As New DataTable()
            DtTab = M_UserBL.GroupShow(Drop_Group.SelectedValue.ToString())
            ViewState("Group") = DtTab
            GvGroup.DataSource = DtTab
            GvGroup.DataBind()
            ViewState("GroupData") = DtTab
        Catch ex As Exception

        End Try
    End Sub
    'Protected Sub ButShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButShow.Click
    '    Try
    '        ViewState("index") = "0"
    '        Dim DtTab As New DataTable()
    '        DtTab = M_UserBL.GroupShow(TxtGroup.Text)
    '        ViewState("Group") = DtTab
    '        GvGroup.DataSource = DtTab
    '        GvGroup.DataBind()
    '        ViewState("GroupData") = DtTab
    '    Catch Ex As Exception
    '        Ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub ButSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButSave.Click
        Try
            Dim ViewStatus As String
            Dim AddStatus As String
            Dim M_DtRights As New DataTable()
            M_DtRights.TableName = "UsrRight"
            M_DtRights.Columns.Add("InterfaceID")
            M_DtRights.Columns.Add("ViewRight")
            M_DtRights.Columns.Add("AddRight")
            Dim DtRow As DataRow

            For i As Integer = 0 To GvGroup.Rows.Count - 1
                Dim ChkView As CheckBox = DirectCast(GvGroup.Rows(i).Cells(4).FindControl("ChkViewNew"), CheckBox)
                If ChkView.Checked Then
                    ViewStatus = "Y"
                Else
                    ViewStatus = "N"
                End If
                Dim ChkAdd As CheckBox = DirectCast(GvGroup.Rows(i).Cells(5).FindControl("ChkAddNew"), CheckBox)
                If ChkAdd.Checked Then

                    AddStatus = "Y"

                Else

                    AddStatus = "N"
                End If


                Dim int_id As String = GvGroup.Rows(i).Cells(0).Text

                DtRow = M_DtRights.NewRow()
                DtRow(0) = int_id
                DtRow(1) = ViewStatus
                DtRow(2) = AddStatus
                M_DtRights.Rows.Add(DtRow)
            Next
            Dim M_RightsSw As System.IO.StringWriter = New System.IO.StringWriter()
            M_DtRights.WriteXml(M_RightsSw)

            M_UserBL.GroupSave(Drop_Group.SelectedValue.ToString(), M_RightsSw.ToString())


            ShowAlertMessage("Group Rights Saved Successfully")
            ChkAll.Checked = False
            GvGroup.DataSource = Nothing
            GvGroup.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub GvGroup_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvGroup.RowDataBound
        Try
            ' e.Row.Cells(0).CssClass = "hideGridColumn"
            e.Row.Cells(0).Visible = False
            If e.Row.RowType = DataControlRowType.Header Then

                Dim DtMainMenu As New DataTable()
                DtMainMenu = M_UserBL.GetMainMenu()
                Dim DrpSerch As DropDownList = DirectCast(e.Row.FindControl("DrpSearch"), DropDownList)
                DrpSerch.DataTextField = "Mnu_Caption"
                DrpSerch.DataValueField = "Mnu_ID"
                DrpSerch.DataSource = DtMainMenu
                DrpSerch.DataBind()
                DrpSerch.Items.Insert(0, "<< Select >>")
                'DrpSerch.SelectedIndexChanged += New EventHandler(DrpSearch_SelectedIndexChanged)
                DrpSerch.SelectedIndex = Convert.ToInt16(ViewState("index").ToString())
            End If

            Dim Dt As New DataTable()
            Dt = DirectCast(ViewState("Group"), DataTable)

            If e.Row.RowType = DataControlRowType.DataRow Then
                For Each Dr As DataRow In Dt.Rows
                    Dim ChkView As CheckBox = DirectCast(e.Row.Cells(2).FindControl("ChkView"), CheckBox)
                    Dim ChkAdd As CheckBox = DirectCast(e.Row.Cells(3).FindControl("ChkAdd"), CheckBox)
                    Dim ChkViewNew As CheckBox = DirectCast(e.Row.Cells(4).FindControl("ChkViewNew"), CheckBox)
                    Dim ChkAddNew As CheckBox = DirectCast(e.Row.Cells(5).FindControl("ChkAddNew"), CheckBox)


                    If Dr(0).ToString() = e.Row.Cells(0).Text Then
                        If Dr(2).ToString() = "Y" Then

                            ChkView.Checked = True
                            e.Row.Cells(2).BackColor = Color.LightGreen
                            ChkViewNew.Checked = True

                            e.Row.Cells(4).BackColor = Color.LightGreen
                        Else
                            ChkView.Checked = False
                            e.Row.Cells(2).BackColor = Color.LightSalmon
                            ChkViewNew.Checked = False

                            e.Row.Cells(4).BackColor = Color.LightSalmon
                        End If
                        If Dr(3).ToString() = "Y" Then
                            ChkAdd.Checked = True
                            e.Row.Cells(3).BackColor = Color.LightGreen
                            ChkAddNew.Checked = True

                            e.Row.Cells(5).BackColor = Color.LightGreen
                        Else
                            ChkAdd.Checked = False
                            e.Row.Cells(3).BackColor = Color.LightSalmon
                            ChkAddNew.Checked = False

                            e.Row.Cells(5).BackColor = Color.LightSalmon
                        End If
                    End If
                Next
            End If

        Catch ex As Exception
        End Try

             
    End Sub

    Protected Sub ButReset_Click(sender As Object, e As EventArgs) Handles ButReset.Click
        Try
            ChkAll.Checked = False
            GvGroup.DataSource = Nothing
            GvGroup.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub DrpSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim DrpSerch As DropDownList = DirectCast(sender, DropDownList)
            ViewState("index") = DrpSerch.SelectedIndex
            Dim i As Integer = DrpSerch.SelectedIndex
            Dim DtSearch As DataTable = DirectCast(ViewState("GroupData"), DataTable)
            If DrpSerch.SelectedIndex > 0 Then

                If DtSearch.Rows.Count > 0 Then
                    DrpSerch.SelectedIndex = Convert.ToInt16(ViewState("index").ToString())
                    Dim DtFilter As DataTable = DtSearch.[Select]("ParentID='" + DrpSerch.SelectedValue & "'").CopyToDataTable()
                    GvGroup.DataSource = DtFilter
                    '   DrpSerch.SelectedIndex = i;
                    GvGroup.DataBind()
                End If
            Else
                GvGroup.DataSource = DtSearch
                GvGroup.DataBind()
            End If
        Catch ex As Exception

            Dim DtSearch As DataTable = DirectCast(ViewState("GroupData"), DataTable)

            DtSearch = DirectCast(ViewState("Group"), DataTable)
            Dim DtClone As DataTable = DtSearch.Clone()
            DtClone.Rows.Add()
            GvGroup.DataSource = DtClone

            GvGroup.DataBind()
        End Try

    End Sub

    Protected Sub GvGroup_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GvGroup.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim HeaderGrid As GridView = DirectCast(sender, GridView)
                Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

                Dim HeaderCell As New TableCell()
                '  HeaderCell.Text = "Group Rights";
                HeaderCell.ColumnSpan = 1
                HeaderGridRow.Cells.Add(HeaderCell)



                HeaderCell = New TableCell()
                HeaderCell.Text = "Existing Rights"
                ' HeaderCell.ColumnSpan = 1
                HeaderGridRow.Cells.Add(HeaderCell)


                HeaderCell = New TableCell()
                HeaderCell.Text = "Add New Rights"
                ' HeaderCell.ColumnSpan = 1
                HeaderGridRow.Cells.Add(HeaderCell)


                GvGroup.Controls(0).Controls.AddAt(0, HeaderGridRow)
            End If
        Catch Ex As Exception
            Ex.Message.ToString()
        End Try

    End Sub

    Protected Sub ChkAll_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAll.CheckedChanged
        Try
            If ChkAll.Checked = True Then

                Dim i As Integer
                For i = 0 To GvGroup.Rows.Count - 1
                    Dim row As GridViewRow = GvGroup.Rows(i)
                    Dim ChkView As CheckBox = DirectCast(row.FindControl("ChkViewNew"), CheckBox)
                    Dim ChkAdd As CheckBox = DirectCast(row.FindControl("ChkAddNew"), CheckBox)
                    GvGroup.Rows(i).Cells(5).BackColor = Color.LightGreen
                    GvGroup.Rows(i).Cells(4).BackColor = Color.LightGreen
                    ChkView.Checked = True
                    ChkAdd.Checked = True
                Next
            ElseIf ChkAll.Checked = False Then
                Dim i As Integer
                For i = 0 To GvGroup.Rows.Count - 1
                    Dim row As GridViewRow = GvGroup.Rows(i)
                    Dim ChkView As CheckBox = DirectCast(row.FindControl("ChkViewNew"), CheckBox)
                    Dim ChkAdd As CheckBox = DirectCast(row.FindControl("ChkAddNew"), CheckBox)
                    GvGroup.Rows(i).Cells(5).BackColor = Color.LightSalmon
                    GvGroup.Rows(i).Cells(4).BackColor = Color.LightSalmon
                    ChkView.Checked = False
                    ChkAdd.Checked = False
                Next

            End If
        Catch Ex As Exception
            Ex.Message.ToString()
        End Try

    End Sub

   

   

    Protected Sub Drop_Group_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Drop_Group.SelectedIndexChanged
        Try
            ViewState("index") = "0"
            Dim DtTab As New DataTable()
            DtTab = M_UserBL.GroupShow(Drop_Group.SelectedValue.ToString())
            ViewState("Group") = DtTab
            GvGroup.DataSource = DtTab
            GvGroup.DataBind()
            ViewState("GroupData") = DtTab
            ChkAll.Checked = False
        Catch Ex As Exception
            Ex.Message.ToString()
        End Try
    End Sub

    
End Class