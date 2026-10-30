Imports System.Drawing
Imports SMARTCORE_BL
Partial Public Class UserRights
    Inherits System.Web.UI.Page
    Dim M_UserBL As New UserBL()
    Dim AddStatus As String = ""
    Dim ViewStatus As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("SearchType") = "USERRIGHT"
                Defaultvalue()
                Session("SearchType") = "USER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_GroupName(CmbFormType)
                CmbFormType.Items.Add("ALL")
            End If
            If Session("SearchResult") <> "" Then
                TxtUser.Text = Session("SearchResult")
                TxtUser_TextChanged(Me.TxtUser, System.EventArgs.Empty)
                Session("SearchResult") = ""

            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Defaultvalue()
        Try
            Dim DtTab As New DataTable()
            DtTab.Columns.Add("Mnu_ID")
            DtTab.Columns.Add("Mnu_Caption")
            DtTab.Columns.Add("GViewRights")
            DtTab.Columns.Add("GAddRights")
            DtTab.Columns.Add("ViewRights")
            DtTab.Columns.Add("AddRights")
            DtTab.Rows.Add("", "", "", "", "", "")
            GvUser.DataSource = DtTab
            GvUser.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub ButShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ButShow.Click

    '    Try
    '        ViewState("index") = "0"

    '        Dim DtTab As New DataTable()
    '        DtTab = M_UserBL.GetUserRights(TxtUser.Text)

    '        ' DtTab.Columns("Mnu_ID").SetOrdinal(0)
    '        'DtTab.Columns("Mnu_Caption").SetOrdinal(1)
    '        'DtTab.Columns("GViewRights").SetOrdinal(2)
    '        'DtTab.Columns("GAddRights").SetOrdinal(3)
    '        'DtTab.Columns("ViewRights").SetOrdinal(4)
    '        'DtTab.Columns("AddRights").SetOrdinal(5)
    '        ViewState("USER") = DtTab
    '        GvUser.DataSource = DtTab
    '        GvUser.DataBind()
    '        If DtTab.Rows.Count > 0 Then
    '            TxtUserName.Text = DtTab.Rows(0)("EmpName").ToString()
    '            TxtGroupId.Text = DtTab.Rows(0)("GroupID").ToString()
    '            TxtGrpName.Text = DtTab.Rows(0)("GroupName").ToString()
    '        Else
    '            ShowAlertMessage("Invalid User")
    '            TxtUser.Text = ""
    '        End If
    '    Catch ex As Exception
    '    End Try

    'End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub



    Protected Sub GvGroup_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvUser.RowDataBound
        Try
            ' e.Row.Cells(0).CssClass = "hideGridColumn"
            e.Row.Cells(0).Visible = False
            If e.Row.RowType = DataControlRowType.Header Then

                Dim DtMainMenu As New DataTable()
                DtMainMenu = M_UserBL.GetMainMenu()
                Dim DrpSerch As DropDownList = DirectCast(e.Row.FindControl("DropSearch"), DropDownList)
                DrpSerch.DataTextField = "Mnu_Caption"
                DrpSerch.DataValueField = "Mnu_ID"
                DrpSerch.DataSource = DtMainMenu
                DrpSerch.DataBind()
                DrpSerch.Items.Insert(0, "<< Select >>")
                '  DrpSerch.SelectedIndexChanged += New EventHandler(DrpSearch_SelectedIndexChanged)
                DrpSerch.SelectedIndex = Convert.ToInt16(ViewState("index").ToString())
            End If

            Dim Dt As New DataTable()
            Dt = DirectCast(ViewState("USER"), DataTable)
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim Expression As String = "Mnu_ID = '" + e.Row.Cells(0).Text & "'"
                Dim SortingOrder As String = "Mnu_ID ASC"
                Dim drFiltered As DataRow() = Dt.[Select](Expression, SortingOrder)

                ' foreach (DataRow Dr in Dt.Rows)
                ' {

                Dim ChkView As CheckBox = DirectCast(e.Row.Cells(2).FindControl("ChkView"), CheckBox)
                Dim ChkAdd As CheckBox = DirectCast(e.Row.Cells(3).FindControl("ChkAdd"), CheckBox)
                Dim TxtExView As TextBox = DirectCast(e.Row.Cells(4).FindControl("TxtExistingView"), TextBox)
                Dim TxtExAdd As TextBox = DirectCast(e.Row.Cells(5).FindControl("TxtExistingAdd"), TextBox)
                Dim DrpView As DropDownList = DirectCast(e.Row.Cells(6).FindControl("DrpViewRight"), DropDownList)
                Dim DrpAdd As DropDownList = DirectCast(e.Row.Cells(7).FindControl("DrpAddRight"), DropDownList)


                TxtExView.Enabled = False
                TxtExAdd.Enabled = False
                ChkAdd.Enabled = False
                ChkView.Enabled = False
                For Each row As DataRow In drFiltered
                    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Center

                    e.Row.Cells(2).Width = New Unit("100px")
                    e.Row.Cells(3).Width = New Unit("100px")
                    e.Row.Cells(4).Width = New Unit("100px")
                    e.Row.Cells(5).Width = New Unit("100px")
                    e.Row.Cells(6).Width = New Unit("100px")
                    e.Row.Cells(7).Width = New Unit("100px")

                    If row(2).ToString() = "Y" Then
                        ChkView.Checked = True
                        e.Row.Cells(2).BackColor = Color.LightGreen
                        ChkView.BackColor = Color.LightGreen
                    ElseIf row(2).ToString() = "N" Then
                        ChkView.Checked = False
                        e.Row.Cells(2).BackColor = Color.LightSalmon
                    End If



                    If row(3).ToString() = "Y" Then
                        ChkAdd.Checked = True
                        e.Row.Cells(3).BackColor = Color.LightGreen
                        ChkAdd.BorderColor = Color.LightGreen
                    ElseIf row(3).ToString() = "N" Then
                        ChkAdd.Checked = False
                        e.Row.Cells(3).BackColor = Color.LightSalmon
                    End If


                    If row(4).ToString() = "D" Then
                        TxtExView.Text = "Inherited"
                        DrpView.Text = "Inherited"

                        e.Row.Cells(4).BackColor = Color.LightGray
                        e.Row.Cells(6).BackColor = Color.LightGray

                        TxtExView.BackColor = Color.LightGray
                        
                        DrpView.BackColor = Color.LightGray
                    ElseIf row(4).ToString() = "Y" Then
                        TxtExView.Text = "Allow"
                        DrpView.Text = "Allow"

                        e.Row.Cells(4).BackColor = Color.LightGreen
                        e.Row.Cells(6).BackColor = Color.LightGreen

                        DrpView.BackColor = Color.LightGreen
                       
                        TxtExView.BackColor = Color.LightGreen
                    Else
                        TxtExView.Text = "Deny"
                        DrpView.Text = "Deny"

                        e.Row.Cells(4).BackColor = Color.LightSalmon
                        e.Row.Cells(6).BackColor = Color.LightSalmon

                        DrpView.BackColor = Color.LightSalmon
                       
                        TxtExView.BackColor = Color.LightSalmon
                    End If

                    If row(5).ToString() = "D" Then

                        TxtExAdd.Text = "Inherited"
                        DrpAdd.Text = "Inherited"

                        e.Row.Cells(5).BackColor = Color.LightGray
                        e.Row.Cells(7).BackColor = Color.LightGray

                        DrpAdd.BackColor = Color.LightGray
                       
                        TxtExAdd.BackColor = Color.LightGray
                    ElseIf row(5).ToString() = "Y" Then
                        TxtExAdd.Text = "Allow"
                        DrpAdd.Text = "Allow"

                        e.Row.Cells(5).BackColor = Color.LightGreen
                        e.Row.Cells(7).BackColor = Color.LightGreen

                        DrpAdd.BackColor = Color.LightGreen


                        TxtExAdd.BackColor = Color.LightGreen
                    Else

                        TxtExAdd.Text = "Deny"
                        DrpAdd.Text = "Deny"

                        e.Row.Cells(5).BackColor = Color.LightSalmon
                        e.Row.Cells(7).BackColor = Color.LightSalmon

                        DrpAdd.BackColor = Color.LightSalmon

                       
                        TxtExAdd.BackColor = Color.LightSalmon
                    End If
                Next
            End If
           

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub GvUser_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GvUser.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim HeaderGrid As GridView = DirectCast(sender, GridView)
            Dim HeaderGridRow As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)

            Dim HeaderCell As New TableCell()
            '  HeaderCell.Text = "Group Rights";
            HeaderCell.ColumnSpan = 1
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Group Rights"
            ' HeaderCell.ColumnSpan = 2
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Existing Rights"
            'HeaderCell.ColumnSpan = 2
            HeaderGridRow.Cells.Add(HeaderCell)

            HeaderCell = New TableCell()
            HeaderCell.Text = "Add New Rights"
            'HeaderCell.ColumnSpan = 2
            HeaderGridRow.Cells.Add(HeaderCell)


            GvUser.Controls(0).Controls.AddAt(0, HeaderGridRow)
        End If

    End Sub
    Private Sub DropSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim DrpSerch As DropDownList = DirectCast(sender, DropDownList)
            ViewState("index") = DrpSerch.SelectedIndex
            'int i = DrpSerch.SelectedIndex;
            Dim DtSearch As DataTable = DirectCast(ViewState("USER"), DataTable)
            If DrpSerch.SelectedIndex > 0 Then
                If DtSearch.Rows.Count > 0 Then
                    DrpSerch.SelectedIndex = Convert.ToInt16(ViewState("index").ToString())
                    Dim DtFilter As DataTable = DtSearch.[Select]("Mnu_ParentID='" + DrpSerch.SelectedValue & "'").CopyToDataTable()
                    GvUser.DataSource = DtFilter

                    GvUser.DataBind()


                End If
            Else
                GvUser.DataSource = DtSearch

                GvUser.DataBind()
            End If
        Catch ex As Exception

            Dim DtSearch As DataTable = DirectCast(ViewState("USER"), DataTable)
            Dim DtClone As DataTable = DtSearch.Clone()
            DtClone.Rows.Add()
            GvUser.DataSource = DtClone


            GvUser.DataBind()
        End Try

    End Sub
    Protected Sub DrpAddRight_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
        Dim ddl As DropDownList = DirectCast(sender, DropDownList)
        Dim row As GridViewRow = DirectCast(ddl.NamingContainer, GridViewRow)
        Dim rowIndex As Integer = row.RowIndex
        ddl.BackColor = Color.Blue
        GvUser.Rows(rowIndex).Cells(7).BackColor = Color.Blue
        If ddl.SelectedItem.Text = "Allow" Then
            ddl.BackColor = Color.LightGreen
            ' ddl.ForeColor =  Color.LightGray;
            GvUser.Rows(rowIndex).Cells(7).BackColor = Color.LightGreen
        ElseIf ddl.SelectedItem.Text = "Deny" Then
            ddl.BackColor = Color.LightSalmon
            'ddl.ForeColor =  Color.White;
            GvUser.Rows(rowIndex).Cells(7).BackColor = Color.LightSalmon
        ElseIf ddl.SelectedItem.Text = "Inherited" Then
            ddl.BackColor = Color.LightGray
            ' ddl.ForeColor =  Color.LightGreen;
            GvUser.Rows(rowIndex).Cells(7).BackColor = Color.LightGray
        End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpViewRight_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddl As DropDownList = DirectCast(sender, DropDownList)
        Dim row As GridViewRow = DirectCast(ddl.NamingContainer, GridViewRow)
        Dim rowIndex As Integer = row.RowIndex
        ddl.BackColor = Color.Blue
        GvUser.Rows(rowIndex).Cells(6).BackColor = Color.Blue
        If ddl.SelectedItem.Text = "Allow" Then
            ddl.BackColor = Color.LightGreen

            GvUser.Rows(rowIndex).Cells(6).BackColor = Color.LightGreen
        ElseIf ddl.SelectedItem.Text = "Deny" Then
            ddl.BackColor = Color.LightSalmon

            GvUser.Rows(rowIndex).Cells(6).BackColor = Color.LightSalmon
        ElseIf ddl.SelectedItem.Text = "Inherited" Then
            ddl.BackColor = Color.LightGray

            GvUser.Rows(rowIndex).Cells(6).BackColor = Color.LightGray
        End If

    End Sub

    Protected Sub TxtUser_TextChanged(sender As Object, e As EventArgs) Handles TxtUser.TextChanged

        Try
            If TxtUser.Text.Length > 0 Then
                ViewState("index") = "0"
                Dim DtTab As New DataTable()
                DtTab = M_UserBL.GetUserRights(TxtUser.Text, "N", "", CmbFormType.SelectedValue)
                ' DtTab.Columns("Mnu_ID").SetOrdinal(0)
                'DtTab.Columns("Mnu_Caption").SetOrdinal(1)
                'DtTab.Columns("GViewRights").SetOrdinal(2)
                'DtTab.Columns("GAddRights").SetOrdinal(3)
                'DtTab.Columns("ViewRights").SetOrdinal(4)
                'DtTab.Columns("AddRights").SetOrdinal(5)
                ViewState("USER") = DtTab
                GvUser.DataSource = DtTab
                GvUser.DataBind()
                If DtTab.Rows.Count > 0 Then
                    TxtUserName.Text = DtTab.Rows(0)("EmpName").ToString()
                    TxtGroupId.Text = DtTab.Rows(0)("GroupID").ToString()
                    TxtGrpName.Text = DtTab.Rows(0)("GroupName").ToString()

                Else
                    ShowAlertMessage("Invalid User")
                    TxtUser.Text = ""
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Bt_Save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bt_Save.Click
        Dim DtRights As New DataTable()
        DtRights.TableName = "UsrRight"
        DtRights.Columns.Add("InterfaceID")
        DtRights.Columns.Add("ViewRight")
        DtRights.Columns.Add("AddRight")
        Dim DtRow As DataRow
        Try

            For i As Integer = 0 To GvUser.Rows.Count - 1

                Dim UpdateFlag As Boolean
                Dim DrpViewRight As DropDownList = DirectCast(GvUser.Rows(i).Cells(6).FindControl("DrpViewRight"), DropDownList)
                Dim DrpAddRight As DropDownList = DirectCast(GvUser.Rows(i).Cells(7).FindControl("DrpAddRight"), DropDownList)
                Dim TxtExViewRight As TextBox = DirectCast(GvUser.Rows(i).Cells(6).FindControl("TxtExistingView"), TextBox)
                Dim TxtExAddRight As TextBox = DirectCast(GvUser.Rows(i).Cells(7).FindControl("TxtExistingAdd"), TextBox)
                AddStatus = ""
                ViewStatus = ""
                UpdateFlag = False

                If TxtExViewRight.Text <> DrpViewRight.SelectedItem.Text OrElse TxtExAddRight.Text <> DrpAddRight.SelectedItem.Text Then
                    UpdateFlag = True
                    If DrpViewRight.SelectedIndex = 0 Then
                        ViewStatus = ""
                    ElseIf DrpViewRight.SelectedIndex = 1 Then

                        ViewStatus = "Y"
                    ElseIf DrpViewRight.SelectedIndex = 2 Then
                        ViewStatus = "N"
                    End If
                End If

                If TxtExViewRight.Text <> DrpViewRight.SelectedItem.Text OrElse TxtExAddRight.Text <> DrpAddRight.SelectedItem.Text Then
                    UpdateFlag = True
                    If DrpAddRight.SelectedIndex = 0 Then
                        AddStatus = ""
                    ElseIf DrpAddRight.SelectedIndex = 1 Then
                        AddStatus = "Y"
                    ElseIf DrpAddRight.SelectedIndex = 2 Then
                        AddStatus = "N"
                    End If
                End If

                If UpdateFlag = True Then
                    DtRow = DtRights.NewRow()
                    DtRow(0) = GvUser.Rows(i).Cells(0).Text
                    DtRow(1) = ViewStatus
                    DtRow(2) = AddStatus
                    DtRights.Rows.Add(DtRow)

                End If
            Next
            Dim M_RightsSw As New System.IO.StringWriter()
            DtRights.WriteXml(M_RightsSw)

            M_UserBL.SaveUserRights(TxtUser.Text, M_RightsSw.ToString())

            ShowAlertMessage("User Rights Saved Successfully")

            GvUser.DataSource = Nothing
            GvUser.DataBind()
            Clear()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Bt_Reset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bt_Reset.Click

        Try
            'Dim Gen As New ClsGen()
            'Gen.ResetFormControlValues(Me)
            GvUser.DataSource = Nothing
            GvUser.DataBind()
            ViewState("index") = "0"

        Catch ex As Exception

        End Try

    End Sub
    Private Sub Clear()
        Try
            TxtUser.Text = ""
            TxtUserName.Text = ""
            TxtGroupId.Text = ""
            TxtGrpName.Text = ""

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    
    Protected Sub CmbFormType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFormType.SelectedIndexChanged
        Try
            Dim DtTab As New DataTable()
            Dim FormType As String = IIf(CmbFormType.SelectedValue = "ALL", "%", CmbFormType.SelectedValue)
            DtTab = M_UserBL.GetUserRights(TxtUser.Text, "N", "", FormType)
            If DtTab.Rows.Count > 0 Then


                ViewState("USER") = DtTab
                GvUser.DataSource = DtTab
                GvUser.DataBind()
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class