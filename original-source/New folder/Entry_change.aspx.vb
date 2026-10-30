Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Partial Public Class Entry_change
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1111
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(DrpBr)
                DrpBr.Items.Add("ALL")
                DrpBr.SelectedValue = Session("DefaultBrCode")
                GvAccountDetails.DataSource = Set_AccountGrid()
                GvAccountDetails.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdAdd.Click
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Ds As New DataSet
            Dim Expression As String = ""
            M_Ds = M_CommonBl.TransactionDetails(DrpBr.SelectedValue, TxtVrno.Text, TxtTrdate.Text)
            Dim M_dt1 As DataTable
            M_dt1 = M_Ds.Tables(0)
            If M_dt1.Rows.Count > 0 Then
                GvAccountDetails.DataSource = M_dt1
                GvAccountDetails.DataBind()

            End If
        Catch ex As Exception
        End Try

    End Sub
   
    Protected Function Set_AccountGrid() As DataTable
        Try
            Dim M_dt As New DataTable("Table")
            M_dt.Columns.Add("Cust_Name", GetType(String))
            M_dt.Columns.Add("ACC_No", GetType(String))
            M_dt.Columns.Add("Tr_Date", GetType(String))
            M_dt.Columns.Add("Vr_No", GetType(String))
            M_dt.Columns.Add("Tran_Type", GetType(String))
            M_dt.Columns.Add("Cash", GetType(String))
            M_dt.Columns.Add("Transfer", GetType(String))
            M_dt.Columns.Add("Tran_id", GetType(String))
            M_dt.Rows.Add("", "", "", "", "", "", "", "")

            Return M_dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Protected Sub GvAccountDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvAccountDetails.RowDataBound
    'Try
    'If e.Row.RowType = DataControlRowType.DataRow Then
    ''Dim Tran_id As String = e.Row.Cells(5).Text
    'End If


    'Catch ex As Exception

    'End Try
    'End Sub
    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Try
            Dim Br_Name As String = DrpBr.SelectedValue
            Dim Vr_No As String = TxtVrno.Text
            Dim TrDate As String = TxtTrdate.Text
            Dim Chge_To As String = DrpMode.SelectedValue
            Dim maker_id As String = Session("UserID")
            Dim M_CommanBL As New CommonBL
            Dim M_Datatable As New DataTable()
            Dim Tran_No As String
            For Each row As GridViewRow In GvAccountDetails.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                    If chkRow.Checked Then
                        Dim storid As String = row.Cells(7).Text
                        Tran_No = Convert.ToString(storid)
                    End If
                End If
                M_Datatable = M_CommanBL.Entrychange(Br_Name, Vr_No, TrDate, Chge_To, maker_id, Tran_No)
            Next

            If M_Datatable.Rows.Count > 0 Then
                ShowAlertMessage("Saved Successfully")
                clear()
            End If

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
    Protected Sub clear()
        GvAccountDetails.DataSource = Set_AccountGrid()
        GvAccountDetails.DataBind()
        TxtVrno.Text = ""
        TxtTrdate.Text = ""
        DrpBr.SelectedValue = Session("DefaultBrCode")
    End Sub
    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        clear()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub

End Class