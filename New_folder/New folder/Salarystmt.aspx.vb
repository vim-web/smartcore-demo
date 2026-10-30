
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL

Partial Public Class Salarystmt
    Inherits System.Web.UI.Page

    Dim FormID As Integer = 1142
    Shared Obj_UserRights As Object
    Dim M_Sql As String
    Dim M_DBConn As New Connection
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
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try

           
            TxtFromDate.Text = Format(Date.Parse(Session("TransDate")), "MM/yyyy")

            Dim M_CommonBL As New CommonBL

            Dim M_DataTable1 As New DataTable
            Dim M_DisplayMem1 As String = "Emp_name"
            Dim M_ValueMem1 As String = "Emp_ID"
            M_Sql = "select Emp_ID,Emp_name from Employee_M_Tbl "
            M_Sql = M_Sql & " order by Emp_ID "
            M_DBConn.ExecuteDataTable(M_DataTable1, M_Sql, False)
            CommonClass.Fill_Combo(CmbEmployeeName, M_DataTable1, M_DisplayMem1, M_ValueMem1)
            


            

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click


        Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable
        Dim StrSlf As String

        M_Sql = "payslipstmt_sp '" + CmbEmployeeName.SelectedValue + "'," & ClsComm.DbDate(TxtFromDate.Text, False) & ""
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        StrSlf = "Company|" & Company_Name & "|Heading|Payslip for the Month of " + MonthName(Month(TxtFromDate.Text)) + " " + CStr(Year(TxtFromDate.Text))
        'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

        Session.Add("RptQry", M_Sql)
        Session.Add("RptPath", Server.MapPath("~/Reports/PaySlip.rpt"))
        Session.Add("RptFormula", StrSlf)

        Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
       
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class