Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data
Imports System.IO

Partial Public Class Emp_PayScale
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim M_CommCL As New CommonClass
    Dim M_dataTable As New DataTable
    Dim flg As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LoadDefaultValue()
        End If


    End Sub
    Private Function sortDataTable(ByVal dTable As DataTable) As DataTable
        Dim dView As New DataView(dTable)
        dView.Sort = "Emp_Salary ASC"
        dTable = dView.ToTable()
        Return dTable
    End Function
    Protected Sub but_add_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles but_add.Click
        Try

            Dim Status As String = hid_Grd_status.Value.Trim()

            If Status = "OLD" Then
                M_dataTable = Session("Emp_payScale")
            End If

            If Status = "NEW" Then
                M_dataTable = Session("Emp_payScale")
                'Session("Emp_payScale") = Nothing
                M_dataTable.Rows(0).Delete()
            End If
            Dim dr As DataRow
            dr = M_dataTable.NewRow()
            dr(1) = txt_Salary.Text.Trim()
            dr(2) = txt_Increment.Text.Trim()
            M_dataTable.Rows.Add(dr)
            M_dataTable = sortDataTable(M_dataTable)
            'M_dataTable.Select("Emp_Salary", "Emp_Salary asc")
            Session("Emp_payScale") = M_dataTable
            hid_Grd_status.Value = "OLD"
            grd_emp_payScale.DataSource = M_dataTable
            grd_emp_payScale.DataBind()

        Catch ex As Exception
            ShowAlertMessage("Sorry !. Server Error. Try Again.")
        End Try
    End Sub


    Private Sub LoadDefaultValue()
        M_CommonBL.Fill_Pickup(ddl_Designation, 85, 0)
        M_dataTable = M_CommonBL.Emp_PayScaleList("10791")

        If M_dataTable.Rows.Count > 0 Then
            txt_Date.Text = M_dataTable.Rows(0)(0)
            M_dataTable = sortDataTable(M_dataTable)
            Session("Emp_payScale") = M_dataTable
            grd_emp_payScale.DataSource = M_dataTable
            grd_emp_payScale.DataBind()
            hid_Grd_status.Value = "OLD"
        Else
            bindgrid()
        End If


    End Sub

    Private Sub bindgrid()
        'M_dataTable.Columns.Add("Emp_Salary", GetType(Integer))
        'M_dataTable.Columns.Add("Emp_Increment", GetType(Integer))
        'M_dataTable.Columns.Add("Effective_Date")
        'M_dataTable.Columns.Add("Emp_Salary", GetType(Integer))
        'M_dataTable.Columns.Add("Emp_Increment", GetType(Integer))
        Dim dr As DataRow
        dr = M_dataTable.NewRow()
        dr(1) = 0
        dr(2) = 0
        M_dataTable.Rows.Add(dr)
        Session("Emp_payScale") = M_dataTable
        grd_emp_payScale.DataSource = M_dataTable
        grd_emp_payScale.DataBind()
        hid_Grd_status.Value = "NEW"
    End Sub

    Protected Sub but_Save_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles but_Save.Click
        Try

            If flg = 0 Then
                flg = 1
                Dim Designation As String = ddl_Designation.SelectedValue.Trim()
                Dim StrDate As String = M_CommCL.DbDate(txt_Date.Text.Trim())
                M_dataTable = Session("Emp_payScale")
                M_dataTable.TableName = "Emp_payScale"
                Dim XMLString As String = ConvertDatatableToXML(M_dataTable)
                Dim M_valid As Boolean = M_CommonBL.Emp_PayScale(XMLString, Designation, StrDate)

                If M_valid = True Then
                    ShowAlertMessage("Successful Saved")
                Else
                    ShowAlertMessage("Sorry !. Server Error. Try Again.")
                End If
            Else
                ShowAlertMessage("wait............")
            End If
            flg = 0
        Catch ex As Exception
            ShowAlertMessage("Sorry !. Server Error. Try Again.")
        End Try

    End Sub
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

    

    Protected Sub ddl_Designation_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddl_Designation.SelectedIndexChanged
        Try
            ' Dim M_dataTable As New DataTable
            Dim Designation As String = ddl_Designation.SelectedValue.Trim()
            M_dataTable = M_CommonBL.Emp_PayScaleList(Designation)

            If M_dataTable.Rows.Count > 0 Then
                txt_Date.Text = M_dataTable.Rows(0)(0)
                M_dataTable = sortDataTable(M_dataTable)
                Session("Emp_payScale") = M_dataTable
                hid_Grd_status.Value = "OLD"
                grd_emp_payScale.DataSource = M_dataTable
                grd_emp_payScale.DataBind()
              
            Else
                bindgrid()
            End If

           
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub but_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles but_Close.Click
          Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub grd_emp_payScale_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grd_emp_payScale.RowCommand

        Try
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            M_dataTable.Columns.Add("Effective_Date")
            M_dataTable.Columns.Add("Emp_Salary", GetType(Integer))
            M_dataTable.Columns.Add("Emp_Increment", GetType(Integer))

            If e.CommandName = "Delete" Then
                Dim dr_ As DataRow
                For index As Integer = 0 To grd_emp_payScale.Rows.Count - 1

                    If rowIndex <> index Then
                        dr_ = M_dataTable.NewRow()
                        dr_(1) = grd_emp_payScale.Rows(index).Cells(0).Text
                        dr_(2) = grd_emp_payScale.Rows(index).Cells(1).Text

                        M_dataTable.Rows.Add(dr_)
                    End If
                Next
                'M_dataTable = Session("Emp_payScale")
                'M_dataTable.Rows(rowIndex).Delete()
                M_dataTable = sortDataTable(M_dataTable)
                Session("Emp_payScale") = M_dataTable
                If M_dataTable.Rows.Count > 0 Then
                    hid_Grd_status.Value = "OLD"
                    grd_emp_payScale.DataSource = M_dataTable
                    grd_emp_payScale.DataBind()
                Else
                    bindgrid()
                    ' hid_Grd_status.Value = "OLD"
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage("Sorry !. Server Error. Try Again.")
        End Try
    End Sub
  
    Protected Sub grd_emp_payScale_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grd_emp_payScale.RowDeleting

    End Sub

    Protected Sub txt_Date_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_Date.TextChanged
        Try
            Dim Designation As String = ddl_Designation.SelectedValue.Trim()
            Dim StrDate = M_CommCL.DbDate(txt_Date.Text)
            M_dataTable = M_CommonBL.Emp_Payscale_DateList(Designation, StrDate)

            If M_dataTable.Rows.Count > 0 Then
                M_dataTable = sortDataTable(M_dataTable)
                Session("Emp_payScale") = M_dataTable
                grd_emp_payScale.DataSource = M_dataTable
                grd_emp_payScale.DataBind()
                hid_Grd_status.Value = "OLD"
            Else
                bindgrid()
            End If

        Catch ex As Exception
            ShowAlertMessage("Sorry !. Server Error. Try Again.")
        End Try
    End Sub
End Class