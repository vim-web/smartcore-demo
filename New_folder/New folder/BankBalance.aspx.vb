Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class BankBalance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                Dim M_CommonBl As New CommonBL
                Dim M_Dt As New DataTable
                M_CommonBl.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("ALL")
                M_Dt = M_CommonBl.Get_Bank(DdlBranch.SelectedValue)
                DdlBank.DataTextField = "Bank_Name"
                DdlBank.DataValueField = "Acc_Code"
                DdlBank.DataSource = M_Dt
                DdlBank.DataBind()
                DdlBank.Items.Add("ALL")
                DtFrom.Text = Session("TransDate")


                If Session("BB_RPTS_BRANCH") IsNot Nothing Then

                    If Session("BB_RPTS_OPTION").ToString() = "1" Then
                        ChkWithCashBalance.Checked = True
                    Else
                        ChkWithCashBalance.Checked = False
                    End If
                    ChkWithCashBalance_CheckedChanged(Me.ChkWithCashBalance, System.EventArgs.Empty)

                    DtFrom.Text = Session("BB_RPTS_DATE")

                    DdlBranch.SelectedValue = Session("BB_RPTS_BRANCH")
                    DdlBranch.SelectedItem.Text = DdlBranch.SelectedItem.Text
                    DdlBranch_SelectedIndexChanged(Me.DdlBranch, System.EventArgs.Empty)

                    DdlBank.SelectedValue = Session("BB_RPTS_BANK")
                    DdlBank.SelectedItem.Text = DdlBank.SelectedItem.Text

                End If
               

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
            Dim AsOnDate As String = DtFrom.Text
            ''Dim ToDate As String = DtTo.Text


            Session("BB_RPTS_DATE") = DtFrom.Text
            Session("BB_RPTS_BRANCH")=DdlBranch.SelectedValue.ToString()
            Session("BB_RPTS_BANK") = DdlBank.SelectedValue.ToString()
            Session("BB_RPTS_OPTION") = IIf(ChkWithCashBalance.Checked = True, "1", "0")



            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

            If ChkWithCashBalance.Checked = True Then
                M_Sql = "exec GetDailyCashBankBalancRpt_SP'" & DdlBranch.SelectedValue & "','" & Convert.ToDateTime(AsOnDate).ToString("MM/dd/yyyy") & "','" & Convert.ToDateTime(Session("TransDate")).ToString("MM/dd/yyyy") & "'"
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|Bank Balance As On " & DtFrom.Text & " |User_ID|" & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                Session.Add("RptQry", M_Sql)
                Session.Add("RptFormula", StrSlf)
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/BankAndCashBalanceRpt.rpt"))
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

            Else

                M_Sql = "exec Bank_Balance_Sp'" & DdlBranch.SelectedValue & "','" & DdlBank.SelectedValue & "','" & Convert.ToDateTime(AsOnDate).ToString("MM/dd/yyyy") & "'"
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & DdlBranch.SelectedItem.ToString & "|Heading|Bank Balance As On " & DtFrom.Text & " |User_ID|" & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                Session.Add("RptQry", M_Sql)
                Session.Add("RptFormula", StrSlf)
                Session.Add("RptPath", Server.MapPath("~/Reports/Loans/BankBalanceRpt.rpt"))
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            End If
           
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DdlBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlBranch.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.Get_Bank(DdlBranch.SelectedValue)
            DdlBank.DataTextField = "Bank_Name"
            DdlBank.DataValueField = "Acc_Code"
            DdlBank.DataSource = M_Dt
            DdlBank.DataBind()
            DdlBank.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub ChkWithCashBalance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkWithCashBalance.CheckedChanged
        If ChkWithCashBalance.Checked = True Then
            Label3.Visible = False
            DdlBank.Visible = False
        Else
            Label3.Visible = True
            DdlBank.Visible = True
        End If
    End Sub
End Class