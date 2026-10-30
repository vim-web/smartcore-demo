Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common

Partial Public Class closure
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1444
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
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(Ddl_Branch)
            'M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), "SHARE")
            M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"))


            Ddl_Branch.SelectedValue = Session("DefaultBrCode")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub Txt_AccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_AccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim Dt_AcDtls As DataTable
            Dim M_dt2 As New DataTable
            Dim M_DBConn As New Connection
            If Txt_AccNo.Text.Trim.Length <= 7 Then
                Txt_AccNo.Text = Right("000" + Ddl_Branch.SelectedValue, 3) + Right("000" + Ddl_Scheme.SelectedValue, 3) + Right("0000000" + Txt_AccNo.Text, 7)
            End If

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(Ddl_Branch.SelectedValue, Ddl_Scheme.SelectedValue, Txt_AccNo.Text, Session("TransDate"))

            If (Dt_AcDtls.Rows(0)("Acc_Status").ToString() = "C") Then
                ShowAlertMessage("Account is Closed!")
                Exit Sub
            Else

                If Dt_AcDtls.Rows.Count > 0 Then
                    Dim CurBal As Double = Dt_AcDtls.Rows(0)("Cur_Bal").ToString()
                    HiddenAccid.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                    Txt_CustId.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                    Txt_CustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                    Txt_CustAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString() & Space(5) & Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                    Txt_balance.Text = CurBal
                    Dim sql2 As String
                    sql2 = " select MAX(Tr_date)  from  Transaction_T_Tbl where  acc_no='" & Txt_AccNo.Text & "'"
                    M_DBConn.ExecuteDataTable(M_dt2, sql2, False)
                    Txt_LasttranDate.Text = Format(M_dt2.Rows(0)(0), "dd/MM/yyyy").ToString()
                Else
                    ShowAlertMessage("Invalid Account Number!!!")

                    Txt_AccNo.Text = ""
                End If
            End If

            'Dim M_dt2 As New DataTable



            'Txt_LasttranDate.Text = M_dt2.Rows(0)("Tr_Date").ToString()
            'Txt_LasttranDate.Text = Date.Parse(sql3.ToString())


            M_CommonBL = Nothing

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

    Protected Sub Ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged

    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim Sql As String
            Dim Dt As New DataTable
            If Txt_balance.Text = "0" Then
                Dt = M_CommonBL.Closure(HiddenAccid.Value, Ddl_Scheme.SelectedValue, Txt_LasttranDate.Text)
                ShowAlertMessage("Account is closed")
                clear()
            Else
                ShowAlertMessage("Account is not Zero Balance")
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub TxtShortCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtShortCode.TextChanged
        Try
            Txt_AccNo.Text = Right("000" + Ddl_Branch.SelectedValue, 3) + Right("000" + Ddl_Scheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
            Call Txt_AccNo_TextChanged(Me.TxtShortCode, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub Txt_balance_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_balance.TextChanged

    End Sub

    Protected Sub Ddl_Scheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Scheme.SelectedIndexChanged

    End Sub

    Protected Sub Txt_CustId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_CustId.TextChanged
        
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        clear()
    End Sub
    Private Sub clear()
        TxtShortCode.Text = ""
        Txt_AccNo.Text = ""
        Txt_CustId.Text = ""
        Txt_CustName.Text = ""
        Txt_CustAddress.Text = ""
        Txt_balance.Text = ""
        Txt_LasttranDate.Text = ""

    End Sub
End Class





