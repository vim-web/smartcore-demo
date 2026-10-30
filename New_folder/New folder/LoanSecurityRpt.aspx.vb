Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class LoanSecurityRpt
    Inherits System.Web.UI.Page
    Shared Obj_UserRights As Object
    Dim FormID As Integer = 1123
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

       
            If Session("UserID") = "" And Session("LoginUserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If

            If Not IsPostBack Then
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                LoadDefaultValues()
                TxtDate.Text = Session("TransDate")
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub LoadDefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch, False)
        M_CommonBL.Fill_SchemeByModule(CmbScheme, "LOAN", "GEN.LOAN")
        CmbScheme.Items.Remove("<-- ALL -->")
        FillLoanSecurities()
        M_CommonBL.Fill_Pickup(DdlAreaType, 89, 0)
        '-- DdlAreaType.Items.Add("<-- ALL -->")
        '--DdlAreaType.SelectedItem.Value = ("<-- ALL -->")
        CmbSecurity.Enabled = False
        TxtAccNo.Enabled = False
        ChkAreaType.Checked = False
        DdlAreaType.Enabled = False
        TxtAreaFrom.Enabled = False

        TxtAreaTo.Enabled = False
    End Sub

    Private Sub FillLoanSecurities()
        Try
            Dim M_CommonBL As New CommonBL
            Dim M_Datatable As New DataTable
            M_Datatable = M_CommonBL.LoadLoanSecurities()
            CmbSecurity.DataSource = M_Datatable
            CmbSecurity.DataTextField = "Pkc_Description"
            CmbSecurity.DataValueField = "Pkc_Code"
            CmbSecurity.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ChkSecurity_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkSecurity.CheckedChanged
        If ChkSecurity.Checked Then
            CmbSecurity.Enabled = True
        Else
            CmbSecurity.Enabled = False
        End If


    End Sub

    Protected Sub ChkAccNo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkAccNo.CheckedChanged
        If ChkAccNo.Checked Then
            TxtAccNo.Enabled = True
        Else
            TxtAccNo.Enabled = False
        End If
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtAccNo.Text, 7)

            Dim Dt_AccDetls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"))
            If Dt_AccDetls.Rows.Count > 0 Then
            Else
                ShowAlertMessage("INVALID ACCOUNT NO")
            End If
        Catch ex As Exception

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

    Protected Sub CmdShow_Click(sender As Object, e As ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim StrSlf As String = ""
        Dim ClsComm As New CommonClass()
        Dim M_Dt As DataTable = New DataTable()
        Dim SecurityType As String
        Dim AccNo As String

        If ChkSecurity.Checked Then
            SecurityType = CmbSecurity.SelectedItem.ToString()
        Else
            SecurityType = "N"
        End If


        If ChkAccNo.Checked And TxtAccNo.Text.Trim() <> "" Then
            AccNo = TxtAccNo.Text
        Else
            AccNo = "0"
        End If

        Dim HeaderArr(7, 4) As String
        Dim HeaderWidth(7, 1) As String

        HeaderArr(0, 0) = "Sl No"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "10"
        HeaderArr(0, 3) = "10"

        HeaderArr(1, 0) = "ACCNO"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "13"
        HeaderArr(1, 3) = "13"

        HeaderArr(2, 0) = "CUST-NAME"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "15"
        HeaderArr(2, 3) = "15"

        HeaderArr(3, 0) = "SECURITY-TYPE"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "15"
        HeaderArr(3, 3) = "15"



        HeaderArr(4, 0) = "DETAILS"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "20"
        HeaderArr(4, 3) = "20"

        HeaderArr(5, 0) = "DETAILS1"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "20"
        HeaderArr(5, 3) = "20"

        HeaderArr(6, 0) = "DETAILS2"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "20"
        HeaderArr(6, 3) = "20"


        Session("RptHeader") = HeaderArr
        If ChkAreaType.Checked = True Then
            M_Sql = "Exec Loan_Security_Report '" & CmbBranch.SelectedValue.ToString() & "','" & CmbScheme.SelectedValue.ToString() & "'," & ClsComm.DbDate(TxtDate.Text) & ",'" & SecurityType.ToString() & "','" & AccNo.ToString() & "','" & Session("UserID") & "','" & DdlAreaType.SelectedValue & "'," & Val(TxtAreaFrom.Text) & "," & Val(TxtAreaTo.Text) & ""
        Else
            M_Sql = "Exec Loan_Security_Report '" & CmbBranch.SelectedValue.ToString() & "','" & CmbScheme.SelectedValue.ToString() & "'," & ClsComm.DbDate(TxtDate.Text) & ",'" & SecurityType.ToString() & "','" & AccNo.ToString() & "','" & Session("UserID") & "'"

        End If
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx", False)
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtAccNo.Text = ""
    End Sub

    Protected Sub ChkAreaType_CheckedChanged(sender As Object, e As EventArgs) Handles ChkAreaType.CheckedChanged
        If ChkAreaType.Checked = True Then
            DdlAreaType.Enabled = True
            TxtAreaFrom.Enabled = True
            TxtAreaTo.Enabled = True
        Else
            DdlAreaType.Enabled = False
            TxtAreaFrom.Enabled = False

            TxtAreaTo.Enabled = False
        End If
    End Sub

    Protected Sub CmbSecurity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbSecurity.SelectedIndexChanged

        ChkAreaType.Checked = False
        DdlAreaType.Enabled = False
    End Sub
End Class