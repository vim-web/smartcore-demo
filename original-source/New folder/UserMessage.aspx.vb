Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Imports System.Data
Imports System.Data.SqlClient

Partial Public Class UserMessage
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim FormID As Integer = 1149
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                View()
                Txt_DateFrom.Text = Session("TransDate")
            End If



        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub View()
        Try

            Dim M_DBConn As New Connection
            Dim M_sql As String
            Dim M_DataTable As New DataTable()
            M_sql = "select Pkc_Code,Pkc_Description from PickupCode_P_Tbl where Pkc_Type=85"
            M_DBConn.ExecuteDataTable(M_DataTable, M_sql, False)
            Dim i As Integer = M_DataTable.Rows.Count

            grd_emp_list.DataSource = M_DataTable
            grd_emp_list.DataBind()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Private Sub View1(ByVal id)
        Try

        
            Dim M_DBConn As New Connection
            Dim M_sql As String
            Dim M_DataTable As New DataTable()
            M_sql = "select Emp_ID,Emp_Name from Employee_M_Tbl where Emp_Designation in(" + id + ")"
            M_DBConn.ExecuteDataTable(M_DataTable, M_sql, False)
            If M_DataTable.Rows.Count > 0 Then
                grd_user_list.DataSource = M_DataTable
                grd_user_list.DataBind()
            Else
                Dim dt As New DataTable
                dt.Rows.Add("", "")
                grd_user_list.DataSource = dt
                grd_user_list.DataBind()
            End If
           
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub grd_emp_list_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles grd_emp_list.SelectedIndexChanged

    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim id As String = ""
            For i As Integer = 0 To grd_emp_list.Rows.Count - 1
                Dim chkSelect As CheckBox = DirectCast(grd_emp_list.Rows(i).Cells(1).FindControl("chkSelect"), CheckBox)
                If chkSelect.Checked = True Then
                    id += grd_emp_list.Rows(i).Cells(2).Text + ","
                End If
            Next
            If Len(id) > 0 Then
                id = Mid(id, 1, Len(id) - 1)
                View1(id)
            Else
                grd_user_list.DataSource = Nothing

                Dim dt As New DataTable
                dt.Columns.Add("Emp_Name")
                dt.Columns.Add("Emp_ID")
                dt.Rows.Add("", "")
                grd_user_list.DataSource = dt
                grd_user_list.DataBind()
            End If



        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim dt As New DataTable("Table")
            dt.Columns.Add("EmployeeCode")
            Dim Bol As Boolean = False
            For i As Integer = 0 To grd_emp_list.Rows.Count - 1

                Dim chkSelect As CheckBox = DirectCast(grd_emp_list.Rows(i).Cells(1).FindControl("chkSelect"), CheckBox)
                If chkSelect.Checked = True Then
                    Bol = True
                End If
                If Bol = False Then
                    ShowAlertMessage("Please Select Users...")
                    Return
                End If
            Next
            For i As Integer = 0 To grd_user_list.Rows.Count - 1
                dt.Rows.Add(grd_user_list.Rows(i).Cells(1).Text)
            Next
            Dim M_CommonBL As New UserDiaryBL
            Dim ClsComm As New CommonClass()
            Dim M_EmpSw As New IO.StringWriter
            dt.WriteXml(M_EmpSw)

            Dim Code As Integer
            
            Code = M_CommonBL.Save_DataMsg(ClsComm.DbDate(Txt_DateFrom.Text, True), TxtMsg.Text, TxtSub.Text, M_EmpSw.ToString.Replace("DocumentElement", "NewDataSet"))
            If Code > 0 Then
                ShowAlertMessage("Saved Successfully...")
                Clear()

            Else
                ShowAlertMessage("Not Saved Successfully...")

            End If


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Try
            Clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Clear()
        Try
            Txt_DateFrom.Text = ""
            TxtMsg.Text = ""
            TxtSub.Text = ""
            For i As Integer = 0 To grd_emp_list.Rows.Count - 1

                Dim chkSelect As CheckBox = DirectCast(grd_emp_list.Rows(i).Cells(1).FindControl("chkSelect"), CheckBox)
                chkSelect.Checked = False
            Next
            grd_user_list.DataSource = Nothing
            grd_user_list.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub grd_emp_list_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd_emp_list.RowDataBound
        Try


            If (e.Row.RowType = DataControlRowType.Header) Then
                DirectCast(e.Row.FindControl("cbSelectAll"), CheckBox).Attributes.Add("onclick", "javascript:SelectAll('" & DirectCast(e.Row.FindControl("cbSelectAll"), CheckBox).ClientID & "')")
              e.Row.Cells(2).Visible = False
            End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then
                e.Row.Cells(2).Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grd_user_list_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grd_user_list.RowDataBound
        Try
            If (e.Row.RowType = DataControlRowType.Header) Then
                e.Row.Cells(1).Visible = False


            End If
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                e.Row.Cells(1).Visible = False

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class