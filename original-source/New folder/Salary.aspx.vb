Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class Salary
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1054
    Shared Obj_UserRights As Object
    Public Salary As New DataTable("Table")
    Public Bonus As New DataTable("Table")
    Public Advance As New DataTable("Table")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            TxtDate.Text = Session("TransDate")
            Salary_Grid()
            Bonus_Grid()

        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_EmpId As Integer
            Dim M_Emp_Leavesurrender As New SMARTCORE_BO.Emp_LeavesurrenderBO
            Dim M_LeaveSurrend As New Emp_LeavesurrenderBL
            With M_Emp_Leavesurrender

                .MakerId = Session("UserId")
                .MakingTime = Session("TransDate")
                .TrDate = TxtDate.Text
            End With
            Salary_Grid()
            Dim Leavedt As DataTable = DirectCast(Session("salary"), DataTable)
            Dim M_LeaveSurrender As New IO.StringWriter
            Leavedt.WriteXml(M_LeaveSurrender)
            Dim M_CommonBL As New CommonBL
            Session("M_EntryMode") = "NEW"
            M_EmpId = M_LeaveSurrend.Save_Data(M_Emp_Leavesurrender, M_LeaveSurrender.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))
            ShowAlertMessage("Saved Sucessfully")
            M_LeaveSurrend = Nothing
            DgvSalary.DataSource = Nothing
            DgvSalary.DataBind()
        Catch ex As Exception
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub
   
    Private Sub Salary_Grid()
        Try
            DgvSalary.DataSource = Nothing
            Salary.Columns.Add("Empid", GetType(String))
            Salary.Columns.Add("EmpName", GetType(String))
            Salary.Columns.Add("GrossPay", GetType(String))
            Salary.Columns.Add("LeaveBal", GetType(String))
            Salary.Columns.Add("SurrenderLeave", GetType(String))
            Salary.Columns.Add("SurrenderAmt", GetType(String))
            For gvrow As Integer = 0 To DgvSalary.Rows.Count - 1
                Dim Bal As TextBox = DirectCast(DgvSalary.Rows(gvrow).Cells(4).FindControl("TxtBalance"), TextBox)
                Dim LeaveAmt As Integer = DgvSalary.Rows(gvrow).Cells(2).Text / 30 * Val(Bal.Text)
                Salary.Rows.Add(DgvSalary.Rows(gvrow).Cells(0).Text, DgvSalary.Rows(gvrow).Cells(1).Text, DgvSalary.Rows(gvrow).Cells(2).Text, DgvSalary.Rows(gvrow).Cells(3).Text, Bal.Text, LeaveAmt)
            Next

            Session("salary") = Salary
            DgvSalary.DataSource = Salary
            DgvSalary.DataBind()
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

   
    Protected Sub DgvSalary_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvSalary.RowDataBound
        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            e.Row.Cells(0).Visible = False
        End If
    End Sub

    Protected Sub DgvSalary_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvSalary.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("salary"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvSalary.DataSource = dt
            DgvSalary.DataBind()
            Session("salary") = dt
            If dt.Rows.Count <= 0 Then Call Salary_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub TxtBalance_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Salary_Grid()
    End Sub
   

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try

            Dim Dt As New DataTable
            Dim M_Emp_Leavesurrender As New Emp_LeavesurrenderBL
            Dt = M_Emp_Leavesurrender.Locate(TxtDate.Text)
            DgvSalary.DataSource = Dt
            DgvSalary.DataBind()
            Salary_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/main.aspx")
    End Sub

    Protected Sub Bonus_Save_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Bonus_Save.Click
        Try
            Dim M_EmpId As Integer
            Dim M_Emp_Leavesurrender As New SMARTCORE_BO.Emp_LeavesurrenderBO            Dim M_LeaveSurrend As New Emp_LeavesurrenderBL            With M_Emp_Leavesurrender

                .MakerId = Session("UserId")
                .MakingTime = Session("TransDate")
                .TrDate = TextBonus.Text
            End With
            Bonus_Grid()            Dim Leavedt As DataTable = DirectCast(Session("Bonus"), DataTable)
            Dim M_LeaveBonus As New IO.StringWriter
            Leavedt.WriteXml(M_LeaveBonus)
            Dim M_CommonBL As New CommonBL            Session("M_EntryMode") = "NEW"            M_EmpId = M_LeaveSurrend.Save_Bonus(M_Emp_Leavesurrender, M_LeaveBonus.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))            ShowAlertMessage("Saved Sucessfully")            M_LeaveSurrend = Nothing
            DgvBonus.DataSource = Nothing
            DgvBonus.DataBind()
        Catch ex As Exception
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub
    Private Sub Bonus_Grid()
        Try
            DgvBonus.DataSource = Nothing
            Bonus.Columns.Add("EmpId", GetType(String))
            Bonus.Columns.Add("EmpName", GetType(String))
            Bonus.Columns.Add("GrossPay", GetType(String))
            Bonus.Columns.Add("MaxBonus", GetType(String))
            Bonus.Columns.Add("Bonus", GetType(String))
            For gvrow As Integer = 0 To DgvBonus.Rows.Count - 1
                Dim Bal As TextBox = DirectCast(DgvBonus.Rows(gvrow).Cells(4).FindControl("TxtBonus"), TextBox)
                Bonus.Rows.Add(DgvBonus.Rows(gvrow).Cells(0).Text, DgvBonus.Rows(gvrow).Cells(1).Text, DgvBonus.Rows(gvrow).Cells(2).Text, DgvBonus.Rows(gvrow).Cells(3).Text, Bal.Text)
                Dim MaxBonus As Integer
                MaxBonus = DgvBonus.Rows(gvrow).Cells(3).Text
                If MaxBonus < Bal.Text Then
                    ShowAlertMessage("MAXIMUM BONUS IS LESS THAN BONUS AMOUNT....")
                    Exit Sub
                End If
            Next
            Session("Bonus") = Bonus
            DgvBonus.DataSource = Bonus
            DgvBonus.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DgvBonus_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvBonus.RowDataBound
        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            e.Row.Cells(0).Visible = False
        End If
    End Sub

    Protected Sub DgvBonus_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvBonus.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("Bonus"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvBonus.DataSource = dt
            DgvBonus.DataBind()
            Session("Bonus") = dt
            If dt.Rows.Count <= 0 Then Call Bonus_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtBonus_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Bonus_Grid()
    End Sub

    Protected Sub Bonus_Close_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Bonus_Close.Click
        Response.Redirect("~/main.aspx")
    End Sub
    Protected Sub ShowBonus_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ShowBonus.Click
        Try

            Dim Dt As New DataTable
            Dim M_Emp_Leavesurrender As New Emp_LeavesurrenderBL
            Dt = M_Emp_Leavesurrender.Bonus(TextBonus.Text)
            DgvBonus.DataSource = Dt
            DgvBonus.DataBind()
            Bonus_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ShowAdvance_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ShowAdvance.Click
        Try

            Dim Dt As New DataTable
            Dim M_Emp_Leavesurrender As New Emp_LeavesurrenderBL
            Dt = M_Emp_Leavesurrender.Advance(TxtAdvancedt.Text)
            DgvAdvance.DataSource = Dt
            DgvAdvance.DataBind()
            Advance_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub AdvanceSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AdvanceSave.Click
        Try
            Dim M_EmpId As Integer
            Dim M_Emp_Leavesurrender As New SMARTCORE_BO.Emp_LeavesurrenderBO            Dim M_LeaveSurrend As New Emp_LeavesurrenderBL            With M_Emp_Leavesurrender

                .MakerId = Session("UserId")
                .MakingTime = Session("TransDate")
                .TrDate = TxtAdvancedt.Text
            End With
            Advance_Grid()            Dim Leavedt As DataTable = DirectCast(Session("Advance"), DataTable)
            Dim M_LeaveAdvance As New IO.StringWriter
            Leavedt.WriteXml(M_LeaveAdvance)
            Dim M_CommonBL As New CommonBL            Session("M_EntryMode") = "NEW"            M_EmpId = M_LeaveSurrend.Save_Advance(M_Emp_Leavesurrender, M_LeaveAdvance.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))            ShowAlertMessage("Saved Sucessfully")            M_LeaveSurrend = Nothing
            DgvAdvance.DataSource = Nothing
            DgvAdvance.DataBind()
        Catch ex As Exception
            ShowAlertMessage("Saving Failed")
        End Try
    End Sub

    Protected Sub AdvanceClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AdvanceClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
    Private Sub Advance_Grid()
        Try
            DgvAdvance.DataSource = Nothing
            Advance.Columns.Add("EmpId", GetType(String))
            Advance.Columns.Add("EmpName", GetType(String))
            Advance.Columns.Add("GrossPay", GetType(String))
            Advance.Columns.Add("MaxAdvance", GetType(String))
            Advance.Columns.Add("Advance", GetType(String))
            For gvrow As Integer = 0 To DgvAdvance.Rows.Count - 1
                Dim Bal As TextBox = DirectCast(DgvAdvance.Rows(gvrow).Cells(4).FindControl("TxtAdvance"), TextBox)
                Advance.Rows.Add(DgvAdvance.Rows(gvrow).Cells(0).Text, DgvAdvance.Rows(gvrow).Cells(1).Text, DgvAdvance.Rows(gvrow).Cells(2).Text, DgvAdvance.Rows(gvrow).Cells(3).Text, Bal.Text)
                Dim MaxAd As Integer
                MaxAd = DgvAdvance.Rows(gvrow).Cells(3).Text
                If MaxAd < Bal.Text Then
                    ShowAlertMessage("MAXIMUM ADVANCE IS LESS THAN ADVANCE AMOUNT....")
                    Exit Sub
                End If
            Next
            Session("Advance") = Advance
            DgvAdvance.DataSource = Advance
            DgvAdvance.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DgvAdvance_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvAdvance.RowDataBound
        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
        End If
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            e.Row.Cells(0).Visible = False
        End If
    End Sub

    Protected Sub DgvAdvance_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvAdvance.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("Advance"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvAdvance.DataSource = dt
            DgvAdvance.DataBind()
            Session("Advance") = dt
            If dt.Rows.Count <= 0 Then Call Advance_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub TxtAdvance_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Advance_Grid()
    End Sub
End Class