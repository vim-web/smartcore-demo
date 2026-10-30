Imports SMARTCORE_BL
Partial Public Class TranDtls
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1143

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hdnAccid.Value = Request.QueryString("AccID")
            Dim M_CommonBL As New CommonBL
            hdnSection.Value = M_CommonBL.Get_AccSection(hdnAccid.Value)
            Load_Transaction()
            If Not IsPostBack Then

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_Transaction()

        Try

            Dim M_Ds As New DataSet
            Dim M_CommonBL As New CommonBL
            Dim M_AccTbl As New DataTable
            Dim M_TranTbl As New DataTable

            M_Ds = M_CommonBL.Get_TransDtls(hdnAccid.Value)
            M_AccTbl = M_Ds.Tables(0)
            M_TranTbl = M_Ds.Tables(1)


            If hdnSection.Value <> "GROUP DEPOSIT" Then
                DivChitty.Visible = False
                DivAcTran.Visible = True
                DgvTransaction.DataSource = M_TranTbl
                DgvTransaction.DataBind()
                Session("AccTran") = M_TranTbl

                LblCrTotal.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(CREDIT_AMT)", ""))
                LblDrTotal.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(DEBIT_AMT)", ""))
                LblIntCr.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(INTEREST_CR)", ""))
                LblIntDr.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(INTEREST_DR)", ""))
                LblChrDr.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(CHRG_DR)", ""))
                LblChrCr.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(CHRG_CR)", ""))
            Else
                DivAcTran.Visible = False
                DivChitty.Visible = True
                DgvGrpDep.DataSource = M_TranTbl
                DgvGrpDep.DataBind()
                Session("AccTran") = M_TranTbl
                LblChittyCr.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(CREDIT_AMT)", ""))
                LblChittyDr.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(DEBIT_AMT)", ""))
                LblChittyDisc.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(DISC)", ""))
                LblChittyInt.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(INTAMT)", ""))
                LblChittyFor.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(FORFEIT)", ""))
                LblChittyBnkChrg.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(BANKCHRG)", ""))
                LblChittyChrCr.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(CHRG_DR)", ""))
                LblChittyChrDr.Text = [String].Format("{0:n2}", M_TranTbl.Compute("Sum(CHRG_CR)", ""))
            End If

            TxtBranch.Text = M_AccTbl.Rows(0)("ACC_BR").ToString()
            TxtScheme.Text = M_AccTbl.Rows(0)("ACC_SCH").ToString()
            TxtAccNo.Text = M_AccTbl.Rows(0)("ACC_NO").ToString()
            TxtCid.Text = M_AccTbl.Rows(0)("ACC_CUST").ToString()
            TxtName.Text = M_AccTbl.Rows(0)("ACC_NAME").ToString()
            TxtDueDate.Text = Format(M_AccTbl.Rows(0)("ACC_DUEDT"), "dd/MM/yyyy")
            TxtPeriod.Text = M_AccTbl.Rows(0)("ACC_PERIOD").ToString()
            TxtInstlmentAmt.Text = Format(M_AccTbl.Rows(0)("ACC_INSTAMT"), "#0.00")
            TxtAddress.Text = M_AccTbl.Rows(0)("ACC_ADDRESS").ToString()
            TxtInterestRate.Text = Format(M_AccTbl.Rows(0)("ACC_INTRATE"), "#0.00")
            TxtPenalInterest.Text = Format(M_AccTbl.Rows(0)("ACC_PINTRATE"), "#0.00")
            TxtBalance.Text = Format(M_AccTbl.Rows(0)("ACC_BALANCE"), "#0.00")
            TxtOverdue.Text = 0 'M_AccTbl.Rows(0)(11).ToString()
            TxtOpenDate.Text = Format(M_AccTbl.Rows(0)("ACC_OPDT"), "dd/MM/yyyy")
            TxtDetails.Text = M_AccTbl.Rows(0)("ACC_OTHERDTLS").ToString()
            TxtDays.Text = M_AccTbl.Rows(0)("INT_DAYS").ToString()
            Txt_SusBal.Text = M_AccTbl.Rows(0)("Susp_Balance").ToString()
            Txt_Canvased.Text = M_AccTbl.Rows(0)("CanvasedBy").ToString()
            TxtPendInst.Text = M_AccTbl.Rows(0)("PendingInstNo").ToString()
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

    'Private Sub DgvTransaction_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvTransaction.PageIndexChanging
    '    Dim M_Dt As DataTable = DirectCast(Session("AccTran"), DataTable)
    '    DgvTransaction.PageIndex = e.NewPageIndex
    '    DgvTransaction.DataSource = M_Dt
    '    DgvTransaction.DataBind()
    'End Sub
End Class