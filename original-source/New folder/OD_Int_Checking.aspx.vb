Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class OD_Int_Checking
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("ALL")
                DtTo.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                M_dt = M_CommonBL.Get_Scheme("LOAN", "13105")
                DdlScheme.DataTextField = "Sch_Name"
                DdlScheme.DataValueField = "Sch_Code"
                DdlScheme.DataSource = M_dt
                DdlScheme.DataBind()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim clscomm As New CommonClass

            Dim HeaderArr(6, 4) As String
            Dim HeaderWidth(6, 1) As String
      

            HeaderArr(0, 0) = "InstNo"
            HeaderArr(0, 1) = "R"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "Inst.Date"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "10"

            HeaderArr(2, 0) = "InstAmnt"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "8"
            HeaderArr(2, 3) = "8"

            HeaderArr(3, 0) = "Delay Days"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "5"
            HeaderArr(3, 3) = "5"

            HeaderArr(4, 0) = "OD Int"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "Receipt Amnt"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"


            Session("RptHeader") = HeaderArr
            M_Sql = "EXEC [Get_EDI_OdInterest_FromBegining_Check] '" & Val(AccId.Value) & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            If TxtAccNo.Text.Length < 7 Then
                TxtAccNo.Text = Right("000" + DdlBranch.SelectedValue, 3) + Right("000" + DdlScheme.SelectedValue, 3) + Right("0000000" + TxtAccNo.Text, 7)
            End If
            Dim Dt_AcDtls As New DataTable
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(DdlBranch.SelectedValue, DdlScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString() + "-" + Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                AccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                DdlBranch.SelectedValue = Dt_AcDtls.Rows(0)("Br_Code").ToString()
                DdlScheme.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
            Else
                AccId.Value = "0"
                TxtName.Text = ""
                ShowAlertMessage("Invalid Account Details")
            End If
     

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
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
    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class