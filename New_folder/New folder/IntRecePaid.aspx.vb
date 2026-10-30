Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common

Partial Public Class IntRecePaid
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1075


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1
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
            M_CommonBL.Fill_Section(CmbSection)
            'M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"))
            Txt_FromDate.Text = Session("TransDate")
            Txt_ToDate.Text = Session("TransDate")
            Ddl_Branch.SelectedValue = Session("DefaultBrCode")
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
                Txt_CustId.Text = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
                Txt_CustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                Txt_CustAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString() & Space(5) & Dt_AcDtls.Rows(0)("Cust_MobileNo").ToString()
                
                Dim count As Integer
                For count = 0 To LstCustId.Items.Count - 1
                    If LstCustId.Items(count).Text = Txt_CustId.Text Then
                        Exit Sub
                    End If

                Next
                LstCustId.Items.Add(Txt_CustId.Text)


            Else

                ShowAlertMessage("Invalid Account Number!!!")
                Txt_AccNo.Text = ""
            End If
            M_CommonBL = Nothing

            If Len(Txt_CustId.Text) > 0 Then CmbOutput.Focus()
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
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass()
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New CommonDA
            Dim Company_Name, Cust_ID As String
            Dim StrSlf As String

            If LstCustId.Items.Count >= 1 Then
                For I = 0 To LstCustId.Items.Count - 1
                    Cust_ID = Cust_ID + "''" + Trim(LstCustId.Items(I).ToString()) + "'',"
                Next
            End If

            
            If Len(Trim(Cust_ID)) <> 0 Then
                Cust_ID = "'" & Mid(Cust_ID, 1, Len(Cust_ID) - 1) & "'"
            End If


            M_DBConn = New Connection()
            Company_Name = M_CommonDa.GetCompanyDetails()

            Dim Terminal = System.Environment.MachineName 'Request.ServerVariables("REMOTE_HOST")
            Dim Sch_Section As String

            Sch_Section = M_CommonDa.GetSchemeSection(Ddl_Scheme.SelectedValue)


            If Sch_Section = "DEPOSIT" Then


                Dim HeaderArr(8, 4) As String
                Dim HeaderWidth(8, 1) As String
                HeaderArr(0, 0) = "DATE"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "8"

                HeaderArr(1, 0) = "CUST ID"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "50"
                HeaderArr(1, 3) = "50"

                HeaderArr(2, 0) = "BRANCH"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "20"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "ACC NO"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "20"
                HeaderArr(3, 3) = "13"


                HeaderArr(4, 0) = "SCHEME"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "20"
                HeaderArr(4, 3) = "13"


                HeaderArr(5, 0) = "NAME"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "20"
                HeaderArr(5, 3) = "13"


                HeaderArr(6, 0) = "AMOUNT"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "20"
                HeaderArr(6, 3) = "13"



                HeaderArr(7, 0) = "DEPOSIT DATE"
                HeaderArr(7, 1) = "L"
                HeaderArr(7, 2) = "20"
                HeaderArr(7, 3) = "13"


                Session("RptHeader") = HeaderArr

                M_Sql = "Exec Cust_IntPaidRecvd_List_Sp " & Ddl_Branch.SelectedValue & "," & Cust_ID & "," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ",'N','" & Session("UserID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)

                Exit Sub

            ElseIf Sch_Section = "SHARE" Then

            ElseIf Sch_Section = "LOAN" Then

                'M_Sql = "Exec PersonalLedger_Sp '" & Ddl_Branch.SelectedValue & "','LOAN' ,'" & Ddl_Scheme.SelectedValue & "','" & Txt_AccNo.Text & "'," & ClsComm.DbDate(Txt_FromDate.Text) & "," & ClsComm.DbDate(Txt_ToDate.Text) & ""
                'M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)
                'M_Sql = ""

                'M_Sql = "Select * From PersonalLedger_R_Tbl Where Sch_Code = '" & Ddl_Scheme.SelectedValue & "' And Acc_No = '" & Txt_AccNo.Text & "' Order By Tr_Date "
                'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)


                'Session.Add("RptQry", M_Sql)
                'Session.Add("RptPath", Server.MapPath("~/Reports/LOANS/Loan_PersonalLedger.rpt"))

            End If

            'StrSlf = "Company|" & Company_Name & "|Branch|" & Ddl_Branch.SelectedItem.ToString & " - Branch|Heading|Personal Ledger Between  " & Txt_FromDate.Text & " - " & Txt_ToDate.Text & " |User_ID|" & Session("UserID")
            'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

            'Session.Add("PersonalLedger", "~/PersonalLedger.aspx")
            'Session.Add("RptFormula", StrSlf)
            'If CmbOutput.SelectedIndex = 0 Then
            '    Response.Redirect("~/RptPreView.aspx?Outputto=1", False)
            'Else
            '    Response.Redirect("~/RptPreView.aspx?Outputto=2", False)
            'End If
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
        Try
            M_CommonBL.GetSchemes(Ddl_Scheme, Session("TransDate"), CmbSection.SelectedItem.ToString)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub AddMore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddMore.Click

        Dim count As Integer
        clear()
        For count = 0 To LstCustId.Items.Count - 1
            If LstCustId.Items(count).Text = Txt_CustId.Text Then
                Exit Sub
            End If

        Next
        LstCustId.Items.Add(Txt_CustId.Text)
    End Sub
    Private Sub clear()
        Txt_CustId.Text = ""
        Txt_AccNo.Text = ""
        Txt_CustAddress.Text = ""
        Txt_CustName.Text = ""

    End Sub

    Protected Sub btn_clear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_clear.Click
        clear()
        LstCustId.Items.Clear()
        Load_DefaultValues

    End Sub
End Class