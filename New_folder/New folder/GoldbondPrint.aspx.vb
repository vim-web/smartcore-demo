Public Class GoldbondPrint
    Inherits System.Web.UI.Page
    Public TowordsFn As New SMARTCORE_Common.CommonClass
    Dim FormID As Integer = 1065

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PrintBond_FD("0080820000004")
    End Sub
    Public Sub PrintBond_FD(ByVal Tran_No As String)
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA

        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

        M_Sql = "Select * FROM LoanBond_T_Tbl Where Acc_No ='" & Tran_No & "'"
        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        LblAddress.Text = M_Dt.Rows(0)("Address").ToString
        LblAmount.Text = M_Dt.Rows(0)("Lm_LoanAmount").ToString
        Lblamt2.Text = M_Dt.Rows(0)("Lm_LoanAmount").ToString
        LblGlNo.Text = M_Dt.Rows(0)("Acc_No").ToString
        LblMemberNo.Text = M_Dt.Rows(0)("Lm_MemNo").ToString
        LblName.Text = M_Dt.Rows(0)("Cust_Name").ToString
        LblAddress.Text = M_Dt.Rows(0)("Address").ToString
        LblDebtAmt.Text = M_Dt.Rows(0)("Lm_LoanAmount").ToString
        LblDescription.Text = M_Dt.Rows(0)("Ornaments").ToString
        LblCount.Text = M_Dt.Rows(0)("Orn_Count").ToString
        LblTotWeigt.Text = M_Dt.Rows(0)("GrossWeight").ToString
        LblCount.Text = M_Dt.Rows(0)("Orn_Count").ToString
        LblTotWeigt.Text = M_Dt.Rows(0)("GrossWeight").ToString
        LblGoldWeight.Text = M_Dt.Rows(0)("NetWeight").ToString
        LblCost.Text = M_Dt.Rows(0)("Lm_BankRate").ToString * M_Dt.Rows(0)("NetWeight").ToString
        Lblweight.Text = M_Dt.Rows(0)("NetWeight").ToString
        LbllCost.Text = M_Dt.Rows(0)("Lm_BankRate").ToString * M_Dt.Rows(0)("NetWeight").ToString
        Lblamt2.Text = M_Dt.Rows(0)("Lm_LoanAmount").ToString
        LblAmt4.Text = M_Dt.Rows(0)("Lm_BankRate").ToString
        LblAmt3.Text = M_Dt.Rows(0)("Lm_BankRate").ToString * M_Dt.Rows(0)("NetWeight").ToString
        LblTotAmt.Text = M_Dt.Rows(0)("Lm_LoanAmount").ToString
        LblDate.Text = Date.Parse(M_Dt.Rows(0)("Lm_LoanDate").ToString)
        Lbl_LoanDate.Text = Date.Parse(M_Dt.Rows(0)("Lm_LoanDate").ToString)
        LblVoucherNo.Text = M_Dt.Rows(0)("VoucherNo").ToString
        LblGLNo2.Text = M_Dt.Rows(0)("Acc_No").ToString
        LblGlAmt.Text = M_Dt.Rows(0)("Lm_LoanAmount").ToString
        LblAmtinWords.Text = TowordsFn.Towords(M_Dt.Rows(0)("Lm_LoanAmount").ToString)
        LblMno.Text = M_Dt.Rows(0)("Lm_MemNo").ToString
        LblName2.Text = M_Dt.Rows(0)("Cust_Name").ToString
        LblWeight2.Text = M_Dt.Rows(0)("GrossWeight").ToString
        LblGlNo4.Text = M_Dt.Rows(0)("Acc_No").ToString
        LblWeight3.Text = M_Dt.Rows(0)("GrossWeight").ToString
        LblGlNo3.Text = M_Dt.Rows(0)("Acc_No").ToString
        LblName4.Text = M_Dt.Rows(0)("Cust_Name").ToString & Space(5) & M_Dt.Rows(0)("Lm_MemNo").ToString
        LblName3.Text = M_Dt.Rows(0)("Cust_Name").ToString & Space(5) & M_Dt.Rows(0)("Lm_MemNo").ToString
        LblRs.Text = M_Dt.Rows(0)("Lm_LoanAmount").ToString
        LblDate2.Text = Date.Parse(M_Dt.Rows(0)("Lm_LoanDate").ToString)
        LblRs2.Text = M_Dt.Rows(0)("Lm_LoanAmount").ToString
        LblDate4.Text = Date.Parse(M_Dt.Rows(0)("Lm_LoanDate").ToString)
        LblOrnament.Text = M_Dt.Rows(0)("Ornaments").ToString
        LblOrnament2.Text = M_Dt.Rows(0)("Ornaments").ToString
        LblDuedate.Text = Date.Parse(M_Dt.Rows(0)("Lm_DueDate").ToString)

    End Sub
End Class