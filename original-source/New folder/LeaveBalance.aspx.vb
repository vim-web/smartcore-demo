Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class LeaveBalance
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1054
    Shared Obj_UserRights As Object
    Public LeaveBal As New DataTable("Table")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Session("SearchType") = "EMPLOYEE"
        End If
        If Session("SearchResult") <> "" Then
            Txt_EmpId.Text = Session("SearchResult")
            Txt_EmpId_TextChanged(Me.Txt_EmpId, System.EventArgs.Empty)
            Txt_EmpId.Focus()
            Session("SearchResult") = ""
        End If
        Session("SearchResult") = ""

    End Sub

    Protected Sub Txt_EmpId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_EmpId.TextChanged
        Try
            Dim M_LeavBalance As New SMARTCORE_BL.EmpLeaveBL
            Dim Dt As New DataSet
            Dt = M_LeavBalance.EmpLocate_Data(Txt_EmpId.Text)
            Dim M_Table As New DataTable
            M_Table = Dt.Tables(0)
            Locate_Data(M_Table)
            DgvLeavebalance.DataSource = Nothing

            DgvLeavebalance.DataSource = Dt.Tables(1)
            DgvLeavebalance.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Locate_Data(ByVal Dtbl As DataTable)
        Txt_EmpName.Text = Dtbl.Rows(0).Item("Emp_Name").ToString()
        Txt_Date.Text = Dtbl.Rows(0).Item("Emp_EffectFrom").ToString()
        Txt_EmpName.Enabled = False
    End Sub
    Private Sub Grid()
        Try


            LeaveBal.Columns.Add("pkc_code")
            LeaveBal.Columns.Add("Pkc_Description")
            LeaveBal.Columns.Add("Balance")
            For gvrow As Integer = 0 To DgvLeavebalance.Rows.Count - 1
                Dim Bal As TextBox = DirectCast(DgvLeavebalance.Rows(gvrow).Cells(2).FindControl("TxtBalance"), TextBox)
                LeaveBal.Rows.Add(DgvLeavebalance.Rows(gvrow).Cells(0).Text, DgvLeavebalance.Rows(gvrow).Cells(1).Text, Bal.Text)
            Next
            Session("LeaveTran") = LeaveBal

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
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
   

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_LeaveBalance As New SMARTCORE_BO.LeaveBalanceBO

        With M_LeaveBalance


            .Trdate = Session("Transdate")
            .EmpId = Txt_EmpId.Text
            .LeaveId = 0
            .LeaveOpBal = 0
            .MakerId = Session("UserId")
            .MakingTime = Session("Transdate")

        End With

        Try
            Dim M_LeaveBalanceBL As New LeaveBalanceBL
            Dim dt As New DataTable
            Dim M_EmpId As Integer
            Grid()
            Dim Leavedt As DataTable = DirectCast(Session("LeaveTran"), DataTable)
            Dim M_Leave As New IO.StringWriter
            Leavedt.WriteXml(M_Leave)
            Dim M_CommonBL As New CommonBL
            Session("M_EntryMode") = "NEW"
            M_EmpId = M_LeaveBalanceBL.Save_Data(M_LeaveBalance, M_Leave.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))
            ShowAlertMessage("Saved Sucessfully")
            M_LeaveBalanceBL = Nothing
        Catch ex As Exception
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub

    Protected Sub DgvLeavebalance_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvLeavebalance.RowDataBound
        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            e.Row.Cells(0).Visible = False
        End If
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class