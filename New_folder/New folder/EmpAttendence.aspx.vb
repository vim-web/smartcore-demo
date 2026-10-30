
Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Partial Public Class EmpAttendence
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private M_DBConn As New Connection
    Dim ClsComm As New CommonClass()
    Dim dt As New DataTable
    Dim FormID As Integer = 1093
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
                Load_DefaultValues()
                Call CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

      
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.Fill_Pickup(CmbLeaveType, 65, 0)
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            CmbBranch.Enabled = False
            M_CommonBL = Nothing

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


    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub



    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        ClearData()
    End Sub

    Private Sub ClearData()
        TxtReason.Text = ""
        DivAbsent.Visible = False
    End Sub

    Protected Sub CmdSave_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_AttendenceBO As New AttendenceMasterBO
        Dim M_AttendenceBL As New AttendenceMasterBL

        If M_AttendenceBL.Validate_Attendence(CmbEmployeeName.SelectedValue, Session("TransDate")) Then
            M_AttendenceBL = Nothing
            ShowAlertMessage("Attendence  :" & CmbEmployeeName.SelectedItem.Text & " Is Already Marked.Please Select Another Employee")
            Exit Sub
        End If

        Try
            With M_AttendenceBO

                .LeaveType = IIf(CmbEmpStatus.SelectedValue = "PRESENT", 0, CmbLeaveType.SelectedValue)
                .EmpID = CmbEmployeeName.SelectedValue
                .Status = IIf(CmbEmpStatus.SelectedValue = "PRESENT", "Y", "N")
                .TrDate = Session("TransDate")
                .Description = IIf(TxtReason.Text.Length = 0, "", TxtReason.Text)

            End With
            M_AttendenceBL.Save_Data(M_AttendenceBO, "NEW")
            ShowAlertMessage("Saved Successfully")
            EmpStatus()
            ClearData()
            CmbEmployeeName.Items.Remove(CmbEmployeeName.SelectedItem)
        Catch ex As Exception
            ShowAlertMessage(ex.ToString())
        End Try
    End Sub

    Protected Sub CmbUserStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbEmpStatus.SelectedIndexChanged
        If (CmbEmpStatus.SelectedItem.Text = "PRESENT") Then
            DivAbsent.Visible = False
        Else
            DivAbsent.Visible = True
        End If
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            Dim M_Attendance As New AttendenceMasterBL

            M_Attendance.Fill_UserEmployee(CmbEmployeeName, Session("TransDate"), CmbBranch.SelectedValue)
            EmpStatus()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub EmpStatus()
        Dim sql As String
        sql = "Select E.Emp_ID ,U.Emp_Name,case When E.Status = 'Y' then 'Present' Else P.Pkc_Description + ' - ' + E.Description End as Status from Emp_Attendence_M_Tbl E Join Employee_M_Tbl U on E.Emp_ID=U.Emp_ID left outer join PickupCode_P_Tbl P on P.Pkc_Code = E.LeaveType where  Tr_Date=" & ClsComm.DbDate(Session("TransDate")) & " and Emp_Branch='" & CmbBranch.SelectedValue & "'"
        M_DBConn.ExecuteDataTable(dt, sql, False)
        Session("Emp") = dt
        Dgvemp.DataSource = dt
        Dgvemp.DataBind()
    End Sub
End Class