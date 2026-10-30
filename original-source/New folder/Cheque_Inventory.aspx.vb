
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Imports SMARTCORE_BL

Partial Public Class Cheque_Inventory
    Inherits System.Web.UI.Page
    Dim ch_inv As New Cheque_inventorybl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            load_values()
            txt_date.Text = Session("TransDate")
            load_type()
            setgrid()
            CmbBranch.SelectedValue = Session("DefaultBrCode")
        End If
    End Sub
    Private Sub load_values()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        M_CommonBL.Fill_Pickup(CmbGrpName, 51, 0)
    End Sub
    Private Sub setgrid()
        Dim dt As New DataTable
        dt.Columns.Add("SLNO", GetType(String))
        dt.Columns.Add("STATINGNO", GetType(String))
        dt.Columns.Add("ENDINGNO", GetType(String))
        dt.Rows.Add("", "", "")
        GvRpt.DataSource = dt
        GvRpt.DataBind()

    End Sub

    Private Sub load_type()
        Try
            Dim dt As New DataTable
            dt = ch_inv.code(CmbGrpName.SelectedValue)
            Dim str As String
            CmbBookType.Items.Clear()
            str = dt.Rows(0)(0).ToString()
            Dim words As String() = str.Split(New Char() {","c})
            Dim word As String
            For Each word In words
                CmbBookType.Items.Add(word)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbGrpName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbGrpName.SelectedIndexChanged
        load_type()
        If (txt_startcheq.Text <> "") Then
            value()

        End If

    End Sub

    Protected Sub txt_startcheq_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_startcheq.TextChanged
        If (txt_startcheq.Text <> "") Then
            Dim str As String
            str = txt_startcheq.Text
            Dim i, j As Integer
            i = (str).Length
            j = Integer.Parse(str.Substring(i - 1, 1))
            If j <> 1 And j <> 6 Then
                ShowAlertMessage("Cheque book no starts from 1 or 6")

            Else
                value()
            End If



        End If

    End Sub

    Protected Sub txt_noofbooks_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_noofbooks.TextChanged
        If (txt_startcheq.Text <> "") Then
            value()
            

        End If
    End Sub

    Protected Sub CmbBookType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBookType.SelectedIndexChanged
        If (txt_startcheq.Text <> "") Then
            value()
        End If
    End Sub

    Private Sub value()
        Try
            Dim i As Integer = 0
            i = Val(txt_noofbooks.Text) * Val(CmbBookType.SelectedItem.Text)
            i = i + Val(txt_startcheq.Text)
            i = i - 1
            txt_endcheq.Text = i.ToString()
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
    Protected Sub CmdAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdAdd.Click
        Dim dt As New DataTable
        Try
            dt = ch_inv.save_data(CmbBranch.SelectedValue, CmbGrpName.SelectedValue, Val(txt_noofbooks.Text), CmbBookType.SelectedValue, Val(txt_startcheq.Text), Date.Parse(txt_date.Text), Session("LoginID"), "N")
            GvRpt.DataSource = dt
            GvRpt.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_close.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_save.Click
        Try
            ch_inv.save_data(CmbBranch.SelectedValue, CmbGrpName.SelectedValue, Val(txt_noofbooks.Text), CmbBookType.SelectedValue, Val(txt_startcheq.Text), Date.Parse(txt_date.Text), Session("LoginID"), "Y")
            clear()
            setgrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_clear.Click
        clear()
    End Sub
    Private Sub clear()
        txt_startcheq.Text = ""
        txt_noofbooks.Text = ""
        txt_endcheq.Text = ""
    End Sub
End Class