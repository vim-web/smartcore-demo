Imports SMARTCORE_DAL
Partial Public Class PrintViewApplet
    Inherits System.Web.UI.Page

    Private M_DBConn As New Connection
    Dim FormID As Integer = 1117

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), "ListPrinters", "findPrinters();", True)

            LblDate.Text = "Date " & Session("TransDate").ToString() '("dd/MM/yyyy").ToString()
            'BuildReceipt()
            Dim Str As [String] = Session("PrintStr").ToString()            
            HiddenField1.Value = Str
        End If
    End Sub
    Private Sub BuildReceipt()
        Dim DtRec As New DataTable()
        Dim DtCompany As New DataTable()
        'DtCompany = DirectCast(Cache("Company"), DataTable)

        Dim Qry As String
        Qry = "select Cmp_Name,Cmp_Address from Gen_CompanyProfile_M_Tbl"
        'M_DBConn.ExecuteReader(Qry, Connection.SQLSource.STOREDPROCEDURE)
        Dim DtTmp As New DataTable
        M_DBConn.ExecuteDataTable(DtCompany, Qry, False)




        If DtCompany.Rows.Count > 0 Then
            LblHeader.Text = DtCompany.Rows(0)("Cmp_Name").ToString()
            LblAdd.Text = DtCompany.Rows(0)("Cmp_Address").ToString()
        End If
        Dim DtVoucher As DataTable = DirectCast(Session("VoucherDet"), DataTable)
        Dim Prnt As New Print
        If DtVoucher.Rows.Count > 0 Then
            LblVrNo.Text = "No.  " & DtVoucher.Rows(0)("VoucherNo").ToString()
            Dim TrDate As DateTime = DateTime.Parse(DtVoucher.Rows(0)("Tr_Date").ToString())
            LblDate.Text = "Date " & TrDate.ToString("dd/MM/yyyy").ToString()
            LblVrType.Text = Session("PrintHeading").ToString()
            LblVrHeaderDet.Text = If(DtVoucher.Rows(0)("CustomerDtls").ToString() <> "", DtVoucher.Rows(0)("CustomerDtls").ToString(), "")
            Dim CashTot As [Double] = [Double].Parse(DtVoucher.Compute("sum(cash)", "").ToString())
            Dim TransTot As [Double] = [Double].Parse(DtVoucher.Compute("sum(Transfer)", "").ToString())
            LblWords.Text = Prnt.NumberToText([Double].Parse(CashTot.ToString()) + [Double].Parse(TransTot.ToString()), True, False).ToString()

            LblUser.Text = DtVoucher.Rows(0)("Maker").ToString()
        End If
        Dim DtColRec As New DataColumn("Sl No.", GetType(System.Int16))
        DtColRec.AutoIncrement = True
        DtColRec.AutoIncrementStep = 1
        DtColRec.AutoIncrementSeed = 1
        DtRec.Columns.Add(DtColRec)

        DtColRec = New DataColumn("Particulars", GetType(System.String))
        DtRec.Columns.Add(DtColRec)

        DtColRec = New DataColumn("Cash", GetType(System.Double))
        DtRec.Columns.Add(DtColRec)

        DtColRec = New DataColumn("Transfers", GetType(System.Double))
        DtRec.Columns.Add(DtColRec)

        Dim DtRow As DataRow

        For i As Integer = 0 To DtVoucher.Rows.Count - 1
            DtRow = DtRec.NewRow()
            DtRow(1) = DtVoucher.Rows(i)("Acchead").ToString()
            If [Double].Parse(DtVoucher.Rows(i)("Cash").ToString()) > 0 Then
                DtRow(2) = Prnt.FormatSkipZero([Double].Parse(DtVoucher.Rows(i)("Cash").ToString()), "#0.00").ToString()
            Else
                DtRow(2) = 0
            End If
            If [Double].Parse(DtVoucher.Rows(i)("TRANSFER").ToString()) > 0 Then
                DtRow(3) = Prnt.FormatSkipZero([Double].Parse(DtVoucher.Rows(i)("TRANSFER").ToString()), "#0.00").ToString()
            Else
                DtRow(3) = 0
            End If
            DtRec.Rows.Add(DtRow)
        Next
        GVRecDetails.DataSource = DtRec
        GVRecDetails.DataBind()

        GVRecDetails.FooterRow.Cells(2).Text = Prnt.FormatSkipZero([Double].Parse(DtRec.Compute("sum(cash)", "").ToString()), "#0.00").ToString()
        GVRecDetails.FooterRow.Cells(3).Text = Prnt.FormatSkipZero([Double].Parse(DtRec.Compute("sum(Transfers)", "").ToString()), "#0.00").ToString()
    End Sub

    Protected Sub GVRecDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

End Class