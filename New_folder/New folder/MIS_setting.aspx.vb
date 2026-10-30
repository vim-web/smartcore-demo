Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports System.Data
Imports System.IO


Partial Public Class MIS_setting
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If

        If Not IsPostBack Then
            Load_DefaultValues()


        End If
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_commanBL As New CommonBL
        M_commanBL.Fill_AccHead(ddl_accounthead)
        M_commanBL.Fill_MIS_Setting(ddl_report)
        bindgrid()

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
    Public Function bindgrid()
        dt = New DataTable()
        dt.Columns.Add("Rpt_Code")
        dt.Columns.Add("Rpt_Name")
        dt.Columns.Add("Acc_Code")
        dt.Columns.Add("Acc_Name")
        dt.Columns.Add("Acc_Type")
        'dt.Rows.Add("","")
        Dim dr As DataRow
        dr = dt.NewRow()
        dr(0) = ""
        dr(1) = ""
        dr(2) = ""
        dr(3) = ""
        dr(4) = ""
        dt.Rows.Add(dr)
        Grid_reportdata.DataSource = dt
        Grid_reportdata.DataBind()
        hid_gird_row.Value = "NEW"
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try

            Dim M_commanBL As New CommonBL
            Dim dt As DataTable = New DataTable
            dt = New DataTable()
            dt.Columns.Add("Rpt_Code")
            dt.Columns.Add("Acc_Code")
            dt.Columns.Add("Acc_Type")
            Dim dr_ As DataRow
            For index As Integer = 0 To Grid_reportdata.Rows.Count - 1
                dr_ = dt.NewRow()
                dr_(0) = Grid_reportdata.Rows(index).Cells(0).Text
                dr_(1) = Grid_reportdata.Rows(index).Cells(2).Text
                dr_(2) = Grid_reportdata.Rows(index).Cells(4).Text
                dt.Rows.Add(dr_)
            Next

            dt.TableName = "MIS_Setting"
            Dim XmlString = ConvertDatatableToXML(dt)
            Dim i As Integer = M_commanBL.Insert_MIS_Setting(XmlString)

            If i > 0 Then
                ShowAlertMessage("Successful Saved!")
                bindgrid()
            Else
                ShowAlertMessage("Sorry ! . Server Error. Try Again")
                bindgrid()
            End If

            


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub but_add1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles but_add1.Click
        Try

      
            Dim dt As DataTable = New DataTable
            dt = New DataTable()
            dt.Columns.Add("Rpt_Code")
            dt.Columns.Add("Rpt_Name")
            dt.Columns.Add("Acc_Code")
            dt.Columns.Add("Acc_Name")
            dt.Columns.Add("Acc_Type")
            Dim dr_ As DataRow
            If hid_gird_row.Value = "OLD" Then
                For index As Integer = 0 To Grid_reportdata.Rows.Count - 1
                    dr_ = dt.NewRow()
                    dr_(0) = Grid_reportdata.Rows(index).Cells(0).Text
                    dr_(1) = Grid_reportdata.Rows(index).Cells(1).Text
                    dr_(2) = Grid_reportdata.Rows(index).Cells(2).Text
                    dr_(3) = Grid_reportdata.Rows(index).Cells(3).Text
                    dr_(4) = Grid_reportdata.Rows(index).Cells(4).Text
                    dt.Rows.Add(dr_)
                Next
            End If

            dr_ = dt.NewRow()
            dr_(0) = ddl_report.SelectedValue.Trim()
            dr_(1) = ddl_report.SelectedItem.Text.Trim()
            dr_(2) = ddl_accounthead.SelectedValue.Trim()
            dr_(3) = ddl_accounthead.SelectedItem.Text()
            dr_(4) = Radio_report.SelectedValue.Trim()
            dt.Rows.Add(dr_)
            hid_gird_row.Value = "OLD"
            Grid_reportdata.DataSource = dt
            Grid_reportdata.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Grid_reportdata_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid_reportdata.RowCommand
        Try

        
            If e.CommandName = "Delete" Then
                Dim indexin As Integer = e.CommandArgument
                dt = New DataTable()
                dt.Columns.Add("Rpt_Code")
                dt.Columns.Add("Rpt_Name")
                dt.Columns.Add("Acc_Code")
                dt.Columns.Add("Acc_Name")
                dt.Columns.Add("Acc_Type")
                Dim dr_ As DataRow

                For index As Integer = 0 To Grid_reportdata.Rows.Count - 1

                    If indexin <> index Then
                        dr_ = dt.NewRow()
                        dr_(0) = Grid_reportdata.Rows(index).Cells(0).Text
                        dr_(1) = Grid_reportdata.Rows(index).Cells(1).Text
                        dr_(2) = Grid_reportdata.Rows(index).Cells(2).Text
                        dr_(3) = Grid_reportdata.Rows(index).Cells(3).Text
                        dr_(4) = Grid_reportdata.Rows(index).Cells(4).Text
                        dt.Rows.Add(dr_)
                    End If
                Next

                If dt.Rows.Count > 0 Then
                    Grid_reportdata.DataSource = dt
                    Grid_reportdata.DataBind()
                Else
                    bindgrid()
                    hid_gird_row.Value = "OLD"
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Grid_reportdata_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Grid_reportdata.RowDeleting

    End Sub

    Protected Sub ddl_report_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_report.SelectedIndexChanged

        Try
            Dim Rpt_Name As String = ddl_report.SelectedValue.Trim()
            Dim M_commanBL As New CommonBL()
            Dim M_DataTable As New DataTable
            M_DataTable = M_commanBL.Get_ACC_Rpt_Name(Rpt_Name)
            If M_DataTable.Rows.Count > 0 Then
                hid_gird_row.Value = "OLD"
                Grid_reportdata.DataSource = M_DataTable
                Grid_reportdata.DataBind()
            Else

                bindgrid()
            End If
           
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Grid_reportdata_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid_reportdata.RowDataBound
        e.Row.Cells(0).Visible = False
        e.Row.Cells(2).Visible = False
    End Sub
End Class