Imports SMARTCORE_BL

Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common

Public Class LoanLegalNotice
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1114
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
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")

                Session("SearchType") = "ACCOUNTNO"
                Load_DefaultValues()
             

            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "ACCOUNTNO" Then
                    Txt_AccNo.Text = Session("SearchResult")
                    Txt_AccNo_TextChanged(Me.Txt_AccNo, System.EventArgs.Empty)
                End If
                Session("SearchResult") = ""
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            '' CmbSection.SelectedValue = "LOAN"

            M_CommonBL.Fill_Branch(Ddl_Branch)

            ''M_CommonBL.Fill_Section(CmbSection)
            CmbSection.Items.Add("LOAN")
            M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), "LOAN")
            Txt_FromDate.Text = Session("TransDate")

            Ddl_Branch.SelectedValue = Session("DefaultBrCode")
            Session("SearchBranch") = Ddl_Branch.SelectedValue.ToString()

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub Txt_AccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_AccNo.TextChanged
        Try
            Dim M_CommonBL As New CommonBL
            Dim Dt_AcDtls As DataTable

            If Txt_AccNo.Text.Trim.Length <= 7 Then
                Txt_AccNo.Text = Right("000" + Ddl_Branch.SelectedValue, 3) + Right("000" + Ddl_Scheme.SelectedValue, 3) + Right("0000000" + Txt_AccNo.Text, 7)
            End If

            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(Ddl_Branch.SelectedValue, Ddl_Scheme.SelectedValue, Txt_AccNo.Text, Session("TransDate"))

            If Dt_AcDtls.Rows.Count > 0 Then
                If Dt_AcDtls.Rows(0)("section").ToString() = "LOAN" Then
                    Txt_CustId.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                    Txt_CustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                    Txt_CustAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString() & Space(5) & Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()

                    CmbSection.SelectedValue = CmbSection.Items.FindByText(Dt_AcDtls.Rows(0)("section")).Value
                    'CmbSection.Items.FindByText("LOAN").Selected = True
                    CmbSection_SelectedIndexChanged(Me.CmbSection, System.EventArgs.Empty)
                    Ddl_Scheme.SelectedValue = Dt_AcDtls.Rows(0)("Sch_Code").ToString()
                    HdnAccId.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                Else
                    ShowAlertMessage("Invalid Account Number!!!")
                    Txt_AccNo.Text = ""
                    Txt_CustAddress.Text = ""
                    Txt_CustName.Text = ""
                    Txt_CustId.Text = ""

                End If

            Else
                ShowAlertMessage("Invalid Account Number!!!")
                Txt_AccNo.Text = ""
                Txt_AccNo.Text = ""
                Txt_CustAddress.Text = ""
                Txt_CustName.Text = ""
                Txt_CustId.Text = ""

            End If
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim ClsComm As New CommonClass
            If ChkExcl.Checked = True Then

                Dim HeaderArr(15, 4) As String
                Dim HeaderWidth(15, 1) As String
                HeaderArr(0, 0) = "Inst_No"
                HeaderArr(0, 1) = "R"
                HeaderArr(0, 2) = "3"
                HeaderArr(0, 3) = "3"

                HeaderArr(1, 0) = "Inst_Date"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "10"
                HeaderArr(1, 3) = "10"

                HeaderArr(2, 0) = "Remarks"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "15"

                HeaderArr(3, 0) = "Inst_Amount"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "8"
                HeaderArr(3, 3) = "8"

                HeaderArr(4, 0) = "TranMode"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "10"
                HeaderArr(4, 3) = "10"

                HeaderArr(5, 0) = "Inst_Prn"
                HeaderArr(5, 1) = "R"
                HeaderArr(5, 2) = "8"
                HeaderArr(5, 3) = "8"

                HeaderArr(6, 0) = "Inst_Int"
                HeaderArr(6, 1) = "R"
                HeaderArr(6, 2) = "8"
                HeaderArr(6, 3) = "8"

                HeaderArr(7, 0) = "PrnRcvd"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "8"
                HeaderArr(7, 3) = "8"

                HeaderArr(8, 0) = "IntRcvd"
                HeaderArr(8, 1) = "R"
                HeaderArr(8, 2) = "8"
                HeaderArr(8, 3) = "8"

                HeaderArr(9, 0) = "OD_IntRcvd"
                HeaderArr(9, 1) = "R"
                HeaderArr(9, 2) = "8"
                HeaderArr(9, 3) = "8"

                HeaderArr(10, 0) = "ChrgRcvd"
                HeaderArr(10, 1) = "R"
                HeaderArr(10, 2) = "8"
                HeaderArr(10, 3) = "8"

                HeaderArr(11, 0) = "Prn_Balance"
                HeaderArr(11, 1) = "R"
                HeaderArr(11, 2) = "8"
                HeaderArr(11, 3) = "8"

                HeaderArr(12, 0) = "Int_Balance"
                HeaderArr(12, 1) = "R"
                HeaderArr(12, 2) = "8"
                HeaderArr(12, 3) = "8"

                HeaderArr(13, 0) = "DelayDays"
                HeaderArr(13, 1) = "R"
                HeaderArr(13, 2) = "8"
                HeaderArr(13, 3) = "8"

                HeaderArr(14, 0) = "Od_Interest"
                HeaderArr(14, 1) = "R"
                HeaderArr(14, 2) = "8"
                HeaderArr(14, 3) = "8"


                Session("RptHeader") = HeaderArr
                Dim M_Sql As String
                M_Sql = "EXEC [Get_LegalNotice_Sp_Excl] " & HdnAccId.Value & ", " & ClsComm.DbDate(Txt_FromDate.Text) & ""
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
            Else
                Dim M_ReportBl As New ReportsBL
                Dim M_Sql As String

                Dim M_Dt As New DataTable()
                Dim M_CommonDa As New SMARTCORE_DA.CommonDA
                Dim StrSlf As String = ""
                Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

                'Dim AsOnDt As Date = Txt_FromDate.Text

                M_Sql = " Exec [dbo].[Get_LegalNotice_Sp] " & HdnAccId.Value & ", " & ClsComm.DbDate(Txt_FromDate.Text) & ""
                'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : All |Heading|Accounts Statement As On : " & Txt_FromDate.Text & " |User_ID|" & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                Session.Add("RptQry", M_Sql)
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/LegalAccountStatement.rpt"))
                Session.Add("RptFormula", StrSlf)
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbSection.SelectedIndexChanged
       
        'Try
        '    M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), CmbSection.SelectedItem.ToString)
        '    Ddl_Scheme.SelectedIndex = 0
        '    Session("SearchSection") = CmbSection.SelectedItem.Text()
        '    Session("SearchScheme") = Ddl_Scheme.SelectedValue.ToString()
        'Catch ex As Exception
        '    ShowAlertMessage(ex.Message)
        'End Try



       

    End Sub

    Protected Sub Ddl_Branch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Branch.SelectedIndexChanged
        Try
            Session("SearchBranch") = Ddl_Branch.SelectedValue.ToString()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Ddl_Scheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddl_Scheme.SelectedIndexChanged
        Try
            Session("SearchScheme") = Ddl_Scheme.SelectedValue.ToString()
        Catch ex As Exception

        End Try

    End Sub

End Class