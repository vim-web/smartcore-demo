Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.IO
Public Class DebRpt_Series
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim M_commCl As New CommonClass
    Dim M_datatable As New DataTable
    Shared Obj_UserRights As Object
    Dim FormID As Integer = 1154
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            LoadDefaultValue()
            ddl_Scheme.Items.Insert(0, "Select")
        End If

    End Sub
    Public Sub LoadDefaultValue()

        M_CommonBL.GetSchemes(ddl_Scheme, Session("TransDate"), "DEPOSIT")
        Griddatabind()

    End Sub

    Private Sub Griddatabind()
        Try


            M_datatable = New DataTable

            M_datatable.Columns.Add("SchCode")
            M_datatable.Columns.Add("SeriesCode")
            M_datatable.Columns.Add("Series")
            M_datatable.Columns.Add("SubSeries")
            M_datatable.Columns.Add("FromMonth")
            M_datatable.Columns.Add("ToMonth")
            M_datatable.Columns.Add("FromDays")
            M_datatable.Columns.Add("ToDays")
            M_datatable.Columns.Add("Redemption")           
            Dim dr As DataRow
            dr = M_datatable.NewRow()
            dr(0) = "-"
            dr(1) = "-"
            dr(2) = "-"
            dr(3) = "-"
            dr(4) = "-"
            dr(5) = "-"
            dr(6) = "-"
            dr(7) = "-"
            dr(8) = "-"           
            M_datatable.Rows.Add(dr)
            Session("grd_Data") = M_datatable
            GV_RptDetails.DataSource = M_datatable
            GV_RptDetails.DataBind()
            hid_status.Value = "NEW"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_Scheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Scheme.SelectedIndexChanged
        Try
            Dim dttab As New DataTable
            dttab = M_CommonBL.Get_DebSeries_Rpt(ddl_Series, ddl_Scheme.SelectedValue)
            ddl_Series.Items.Insert(0, "Select")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
        ddl_Scheme.SelectedIndex = 0
        TxtSubSeries.Text = ""
        TxtFrmMonth.Text = ""
        TxtToMonth.Text = ""
        TxtFrmDays.Text = ""
        TxtToDays.Text = ""
        TxtRedemption.Text = ""
        TxtRemarks.Text = ""
        Griddatabind()
    End Sub

    Protected Sub but_add_Click(sender As Object, e As EventArgs) Handles but_add.Click
        Try
            Dim M_datatable As New DataTable
            Dim SeriesChar As String()
            M_datatable = Session("grd_Data")
            Dim status As String = hid_status.Value.Trim()
            If status = "NEW" Then
                M_datatable.Rows(0).Delete()
            End If
            Dim dr As DataRow
            dr = M_datatable.NewRow()
            dr(0) = ddl_Scheme.SelectedValue
            dr(1) = ddl_Series.SelectedValue
            dr(2) = ddl_Series.SelectedItem.Text
            dr(3) = TxtSubSeries.Text
            dr(4) = Val(TxtFrmMonth.Text)
            dr(5) = Val(TxtToMonth.Text)
            dr(6) = Val(TxtFrmDays.Text)
            dr(7) = Val(TxtToDays.Text)
            dr(8) = TxtRedemption.Text
            'SeriesChar = ddl_Series.SelectedItem.Text.Split("-")
            ' dr(8) = SeriesChar(1)
            hid_status.Value = "OLD"
            M_datatable.Rows.Add(dr)
            GV_RptDetails.DataSource = M_datatable
            GV_RptDetails.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GV_RptDetails_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GV_RptDetails.RowDeleting
        Try
            M_datatable = New DataTable
            M_datatable.Columns.Add("SchCode")
            M_datatable.Columns.Add("SeriesCode")
            M_datatable.Columns.Add("Series")
            M_datatable.Columns.Add("SubSeries")
            M_datatable.Columns.Add("FromMonth")
            M_datatable.Columns.Add("ToMonth")
            M_datatable.Columns.Add("FromDays")
            M_datatable.Columns.Add("ToDays")
            M_datatable.Columns.Add("Redemption")

            Dim rowIndex As Integer = e.RowIndex
            Dim dr_ As DataRow
            For index As Integer = 0 To GV_RptDetails.Rows.Count - 1
                If rowIndex <> index Then
                    dr_ = M_datatable.NewRow()
                    dr_(0) = GV_RptDetails.Rows(index).Cells(0).Text
                    dr_(1) = GV_RptDetails.Rows(index).Cells(1).Text
                    dr_(2) = GV_RptDetails.Rows(index).Cells(2).Text
                    dr_(3) = GV_RptDetails.Rows(index).Cells(3).Text
                    dr_(4) = GV_RptDetails.Rows(index).Cells(4).Text
                    dr_(5) = GV_RptDetails.Rows(index).Cells(5).Text
                    dr_(6) = GV_RptDetails.Rows(index).Cells(6).Text
                    dr_(7) = GV_RptDetails.Rows(index).Cells(7).Text
                    dr_(8) = GV_RptDetails.Rows(index).Cells(8).Text

                    M_datatable.Rows.Add(dr_)
                End If
            Next
            If M_datatable.Rows.Count > 0 Then
                Session("grd_Data") = M_datatable
                hid_status.Value = "OLD"
                GV_RptDetails.DataSource = M_datatable
                GV_RptDetails.DataBind()
            Else
                Griddatabind()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Try
            Dim M_da As New DataTable
            M_da.Columns.Add("SchCode")
            M_da.Columns.Add("SeriesCode")
            M_da.Columns.Add("Series")
            M_da.Columns.Add("SubSeries")
            M_da.Columns.Add("FromMonth")
            M_da.Columns.Add("ToMonth")
            M_da.Columns.Add("FromDays")
            M_da.Columns.Add("ToDays")
            M_da.Columns.Add("Redemption")           
            Dim dr As DataRow

            For index As Integer = 0 To GV_RptDetails.Rows.Count - 1
                dr = M_da.NewRow()

                dr(0) = GV_RptDetails.Rows(index).Cells(0).Text.Trim()
                dr(1) = GV_RptDetails.Rows(index).Cells(1).Text.Trim()
                dr(2) = GV_RptDetails.Rows(index).Cells(2).Text.Trim()
                dr(3) = GV_RptDetails.Rows(index).Cells(3).Text.Trim()
                dr(4) = GV_RptDetails.Rows(index).Cells(4).Text.Trim()
                dr(5) = GV_RptDetails.Rows(index).Cells(5).Text.Trim()
                dr(6) = GV_RptDetails.Rows(index).Cells(6).Text.Trim()
                dr(7) = GV_RptDetails.Rows(index).Cells(7).Text.Trim()
                dr(8) = GV_RptDetails.Rows(index).Cells(8).Text.Trim()              
                M_da.Rows.Add(dr)
            Next


            M_da.TableName = "DEBSERIES"
            Dim XmlStr As String = ConvertDatatableToXML(M_da)
            Dim M_valid As Boolean = M_CommonBL.SaveDebSeries_Rpt(XmlStr, Session("LoginID"), TxtRemarks.Text)

            If M_valid = True Then
                ShowAlertMessage("Successful Saved")
            Else
                ShowAlertMessage("Error. Try Again.")
            End If
            ClearAll()
        Catch ex As Exception

        End Try


    End Sub

    Public Function ConvertDatatableToXML(ByVal dt As DataTable) As String
        Try
            Dim str As New MemoryStream()
            dt.WriteXml(str, True)
            str.Seek(0, SeekOrigin.Begin)
            Dim sr As New StreamReader(str)
            Dim xmlstr As String
            xmlstr = sr.ReadToEnd()
            Return (xmlstr)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Function

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "`")
                Dim M_CommonCls As New CommonClass
                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub CmdClear0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        ClearAll()
    End Sub

    Protected Sub ddl_Series_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Series.SelectedIndexChanged
        Dim dttab As New DataTable
        dttab = M_CommonBL.Get_AllDebSeries_Scheme(ddl_Scheme.SelectedValue, ddl_Series.SelectedValue)
        If dttab.Rows.Count > 0 Then
            GV_RptDetails.DataSource = dttab
            GV_RptDetails.DataBind()
            hid_status.Value = "OLD"
            Session("grd_Data") = dttab

        End If
    End Sub
End Class