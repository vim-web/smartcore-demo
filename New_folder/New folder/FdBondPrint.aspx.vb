Public Class FdBondPrint
    Inherits System.Web.UI.Page
    Public TowordsFn As New SMARTCORE_Common.CommonClass

    Dim FormID As Integer = 1057

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        PrintBond_FD("2/817/640/234634")



    End Sub
    Public Sub PrintBond_FD(ByVal Tran_No As String)
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim M_CommonDa As New SMARTCORE_DA.CommonDA

        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

        M_Sql = "Select Top 1 B.*,T.Vr_No,Dbo.Get_AccountOperatorsWithAddres_Fn(B.Cust_ID,B.Br_Code,B.Sch_Code,B.Acc_No,GETDATE(),'D') As Operators "
        M_Sql = M_Sql & "From DepBond_Vw B Left Outer Join Transaction_T_Tbl  T On T.Tran_No=B.Tran_No And T.Br_Code=B.Br_Code And T.Sch_Code=B.Sch_Code And "
        M_Sql = M_Sql & "T.Acc_No=B.Acc_No And T.Acc_Code=T.Gl_Code  where B.Tran_No='2/817/640/234634'"

        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

        LblBranch.Text = M_Dt.Rows(0)("Br_Name").ToString
        LblAddress.Text = M_Dt.Rows(0)("Add_Address1").ToString
        LblAddress2.Text = M_Dt.Rows(0)("Add_Address1").ToString
        LblBranch2.Text = M_Dt.Rows(0)("Br_Name").ToString
        LblCustName2.Text = M_Dt.Rows(0)("Cust_Name").ToString
        LblDepAmt.Text = M_Dt.Rows(0)("Dm_DepAmount").ToString & "/-"
        LblDepAmt2.Text = M_Dt.Rows(0)("Dm_DepAmount").ToString & "/-"
        LblDepDate.Text = Date.Parse(M_Dt.Rows(0)("Dm_DepDate").ToString)
        LblDepNo.Text = M_Dt.Rows(0)("Acc_No").ToString
        LblAccNo.Text = M_Dt.Rows(0)("Acc_No").ToString
        LblDmDate.Text = Date.Parse(M_Dt.Rows(0)("Dm_DepDate").ToString)
        LblDuedate.Text = Date.Parse(M_Dt.Rows(0)("Dm_DueDate").ToString)
        LblDueDate2.Text = Date.Parse(M_Dt.Rows(0)("Dm_DueDate").ToString)
        LblEffDate.Text = Date.Parse(M_Dt.Rows(0)("Dm_WithEffDate").ToString)
        LblIntRate2.Text = M_Dt.Rows(0)("Dm_IntRate").ToString
        LblIntrRate.Text = M_Dt.Rows(0)("Dm_IntRate").ToString
        LblName.Text = M_Dt.Rows(0)("Cust_Name").ToString
        LblNomName.Text = M_Dt.Rows(0)("Nm_Name").ToString
        LblNoOfmonth.Text = M_Dt.Rows(0)("Dm_Months").ToString & Space(1) & IIf(M_Dt.Rows(0)("Dm_Months").ToString > 0, "Months", "Days")
        LblNoOfmonth2.Text = M_Dt.Rows(0)("Dm_Months").ToString & Space(1) & IIf(M_Dt.Rows(0)("Dm_Months").ToString > 0, "Months", "Days")
        LblNoToWords.Text = TowordsFn.Towords(M_Dt.Rows(0)("Dm_DepAmount").ToString)
        LblNoToWords2.Text = TowordsFn.Towords(M_Dt.Rows(0)("Dm_DepAmount").ToString)
        LblOpMode.Text = M_Dt.Rows(0)("Dm_OperationMode").ToString
        LblPostofc.Text = M_Dt.Rows(0)("Add_Postoffice").ToString

        LblPrevilage.Text = M_Dt.Rows(0)("Previlege").ToString



    End Sub
End Class