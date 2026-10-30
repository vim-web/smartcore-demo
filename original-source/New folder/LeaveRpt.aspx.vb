Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class LeaveRpt
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 1093
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                M_DBConn = New Connection
                TxtAsonDate.Text = Session("TransDate")
                LoadDefaultValues()
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
    Private Sub LoadDefaultValues()
        Dim M_CommonBL As New CommonBL
        Dim M_Sql As String
        Dim M_DBConn As New Connection
        M_CommonBL.Fill_Pickup(CmbLeavetype, 65, 0)
        CmbType.Items.Add("Employeewise")
        CmbType.Items.Add("LeaveTypeWise")
        Dim M_DataTable1 As New DataTable
        Dim M_DisplayMem1 As String = "Emp_name"
        Dim M_ValueMem1 As String = "Emp_ID"
        M_Sql = "select Emp_ID,Emp_name from Employee_M_Tbl "
        M_Sql = M_Sql & " order by Emp_ID "
        M_DBConn.ExecuteDataTable(M_DataTable1, M_Sql, False)
        CommonClass.Fill_Combo(CmbEmpName, M_DataTable1, M_DisplayMem1, M_ValueMem1)
        CmbLeavetype.Visible = False
        CmbEmpName.Visible = False
        LblEmp.Visible = False
        LblLeaveType.Visible = False
    End Sub
    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmbType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbType.SelectedIndexChanged
        If CmbType.SelectedIndex = 0 Then
            CmbEmpName.Visible = True
            LblEmp.Visible = True
            CmbLeavetype.Visible = False
            LblLeaveType.Visible = False
        Else
            CmbLeavetype.Visible = True
            LblLeaveType.Visible = True
            CmbEmpName.Visible = False
            LblEmp.Visible = False
        End If
            
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        ' Formula From Sql Query Output 
        If CmbType.SelectedItem.Text = "Employeewise" Then
            Dim M_Qry As String = ""
            Dim HeaderArr(4, 5) As String
            Dim HeaderWidth(3, 1) As String
            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "4"

            HeaderArr(1, 0) = "LEAVE TYPE"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "4"
            HeaderArr(1, 3) = "4"

            HeaderArr(2, 0) = "BALANCE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "13"
            HeaderArr(2, 3) = "13"


            Session("RptHeader") = HeaderArr
            M_Qry = "EXEC [LeaveBalanceRpt_Sp]   '" & CmbEmpName.SelectedValue & "','" & Format(CDate(TxtAsonDate.Text), "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Qry
        Else
            Dim M_Qry As String = ""
            Dim HeaderArr(4, 5) As String
            Dim HeaderWidth(3, 1) As String
            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "4"
            HeaderArr(0, 3) = "4"

            HeaderArr(1, 0) = "EMP NAME"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "4"
            HeaderArr(1, 3) = "4"

            HeaderArr(2, 0) = "BALANCE"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "13"
            HeaderArr(2, 3) = "13"
            M_Qry = "EXEC [LeaveTypeRpt_Sp]   '" & CmbLeavetype.SelectedValue & "','" & Format(CDate(TxtAsonDate.Text), "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Qry

        End If
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Exit Sub

    End Sub
End Class