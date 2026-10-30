Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Public Class UserChangeGroup
    
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

      
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            'USer Rights
            'Dim M_Comm As New CommonBL
            'Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            'M_Comm = Nothing
            'If Obj_UserRights.View_Right = False Then
            '    Response.Redirect("~/UnauthorisedAcces.ASPX")
            'End If
            Load_DefaultValues()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Load_DefaultValues()
        Try

     
        Dim M_CommonBL As New CommonBL

        M_CommonBL.Fill_Pickup(CmbUserType, 69, 0)
        M_CommonBL.Fill_UserEmployee(CmbEmployeeName)
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
        
        TxtMaxAmt.Text = "0.0"
    End Sub
    Protected Sub CmdSave_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try

            Dim M_UserBL As New UserBL
            Dim DtTab As New DataTable()
            Dim msg As String
            DtTab = M_UserBL.UpdateEmpUserDetails(CmbEmployeeName.SelectedValue, CmbUserType.SelectedValue, TxtMaxAmt.Text)
            msg = DtTab.Rows(0)("Result").ToString()
            ShowAlertMessage(msg)

            TxtCustId.Text = ""
            TxtMaxAmt.Text = ""
            CmbUserType.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmbEmployeeName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbEmployeeName.SelectedIndexChanged
        Try
            Dim M_UsrRegBO As New UserBO
            Dim M_UserBL As New UserBL
                Dim DtTab As New DataTable()
            DtTab = M_UserBL.GetEmployeCustDetails(CmbEmployeeName.SelectedValue)
               
                ViewState("USER") = DtTab
               
                If DtTab.Rows.Count > 0 Then
                TxtCustId.Text = DtTab.Rows(0)("Cust_Id").ToString()
                TxtMaxAmt.Text = DtTab.Rows(0)("MaxTranAmt").ToString()

                CmbUserType.SelectedValue = DtTab.Rows(0)("User_Type").ToString()

                Else
                ShowAlertMessage("Invalid User")
                TxtCustId.Text = ""
                TxtMaxAmt.Text = ""

                End If

        Catch ex As Exception

        End Try
    End Sub
End Class