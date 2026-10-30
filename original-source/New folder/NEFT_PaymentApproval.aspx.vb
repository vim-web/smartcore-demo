Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class NEFT_PaymentApproval
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1217
    Dim Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                If Session("UserID") = "" Then
                    Response.Redirect("~/SmartLogin.aspx")
                End If
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                ''Region Wise
                M_CommonBL.Fill_UserWiseRegion(Drpregn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), Drpregn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), Drpregn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                TxtTrDate.Text = Session("TransDate")
                M_CommonBL.Get_NEFT_BankScheme(Drp_BankScheme)
                M_CommonBL.Get_NEFT_BankAccNo(DrpBankAccNo, Drp_BankScheme.SelectedValue)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Drpregn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Drpregn.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), Drpregn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DrpArea_SelectedIndexChanged(Me.DrpArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), Drpregn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Show_Approval()
    End Sub
    Protected Sub Show_Approval()
        Try
            Dim M_Dt As New DataTable
            Dim M_CommonBL As New CommonBL
            M_Dt = M_CommonBL.Get_NEFT_Approval(Drpregn.SelectedValue, DrpArea.SelectedValue, DdlBranch.SelectedValue, TxtTrDate.Text, Session("UserID"))

            If M_Dt.Rows.Count > 0 Then
                Gv_PostInt.DataSource = M_Dt
                Gv_PostInt.DataBind()
            Else
                Gv_PostInt.DataSource = Nothing
                Gv_PostInt.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ChkAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAll.CheckedChanged
        Try
            If ChkAll.Checked = True Then

                Dim i As Integer
                For i = 0 To Gv_PostInt.Rows.Count - 1
                    Dim row As GridViewRow = Gv_PostInt.Rows(i)
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = True
                Next
            ElseIf ChkAll.Checked = False Then
                For i = 0 To Gv_PostInt.Rows.Count - 1
                    Dim row As GridViewRow = Gv_PostInt.Rows(i)
                    Dim ChkSelect As CheckBox = DirectCast(row.FindControl("ChkSelect"), CheckBox)
                    ChkSelect.Checked = False
                Next

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

    Protected Sub Drp_BankScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Drp_BankScheme.SelectedIndexChanged
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Get_NEFT_BankAccNo(DrpBankAccNo, Drp_BankScheme.SelectedValue)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnApprove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnApprove.Click
       Try
            Dim SelectionDT As New DataTable
            SelectionDT.TableName = "Table"
            SelectionDT.Columns.Add("Tran_No", GetType(String))
            SelectionDT.Columns.Add("Acc_Id", GetType(String))


            For i As Integer = 0 To Gv_PostInt.Rows.Count - 1
                Dim tb As CheckBox = DirectCast(Gv_PostInt.Rows(i).FindControl("ChkSelect"), CheckBox)
                If tb.Checked Then
                    SelectionDT.Rows.Add(Gv_PostInt.DataKeys(i)("Tran_No").ToString(), Gv_PostInt.DataKeys(i)("Acc_Id").ToString())
                End If
            Next
            Dim M_SelectionSw As New IO.StringWriter
            SelectionDT.WriteXml(M_SelectionSw)
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.Approve_NEFT(M_SelectionSw.ToString(), TxtTrDate.Text, Session("UserID"), DrpBankAccNo.SelectedItem.Text, "Y")
            If M_Dt.Rows.Count > 0 Then
                ShowAlertMessage("Saved Successfully...")
                Show_Approval()
            Else
                ShowAlertMessage("Failed...")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
Try
            Dim SelectionDT As New DataTable
            SelectionDT.TableName = "Table"
            SelectionDT.Columns.Add("Tran_No", GetType(String))


            For i As Integer = 0 To Gv_PostInt.Rows.Count - 1
                Dim tb As CheckBox = DirectCast(Gv_PostInt.Rows(i).FindControl("ChkSelect"), CheckBox)
                If tb.Checked Then
                    SelectionDT.Rows.Add(Gv_PostInt.DataKeys(i)("Tran_No").ToString())
                End If
            Next
            Dim M_SelectionSw As New IO.StringWriter
            SelectionDT.WriteXml(M_SelectionSw)
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.Approve_NEFT(M_SelectionSw.ToString(), TxtTrDate.Text, Session("UserID"), DrpBankAccNo.SelectedItem.Text, "N")
            If M_Dt.Rows.Count > 0 Then
                ShowAlertMessage("Cancellation Completed...")
                Show_Approval()
            Else
                ShowAlertMessage("Failed...")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class