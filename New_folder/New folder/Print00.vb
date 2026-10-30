Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports System.Text
Imports System.Web.SessionState
Imports SMARTCORE_DAL

Imports SMARTCORE_BL


'---------------------------------------------------------
'      * Procedure         :PrintClass
'      * DateTime          :9-Apr-2012
'      * Author            :Prasanth
'      * Purpose           :for Printing
'      * Functional Outline:
'     *---------------------------------------------------------- 




Public Class Print
    Private LastRow As Integer
    Private NoOfSkippingHeader As Integer
    Private M_DBConn As New Connection

    'public static void Print(Control ctrl)
    '{

    '}

    'public void PrintSet(Control ctrl, string Script)
    '{
    '    StringWriter stringWrite = new StringWriter();
    '    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
    '    if (ctrl is WebControl)
    '    {
    '        Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
    '    }
    '    Page pg = new Page();
    '    pg.EnableEventValidation = false;
    '    if (Script != string.Empty)
    '    {
    '        pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
    '    }
    '    HtmlForm frm = new HtmlForm();
    '    pg.Controls.Add(frm);
    '    frm.Attributes.Add("runat", "server");
    '    frm.Controls.Add(ctrl);
    '    pg.DesignerInitialize();
    '    pg.RenderControl(htmlWrite);
    '    string strHTML = stringWrite.ToString();
    '    HttpContext.Current.Response.Clear();
    '    HttpContext.Current.Response.Write(strHTML);
    '    HttpContext.Current.Response.Write("<script>window.print();</script>");
    '    HttpContext.Current.Response.End();
    '}


    Private Structure Printing_Setup
        Public Print_Needed As [Boolean]
        Public Print_TranType As [String]
        Public Print_Dialog As [Boolean]
        Public Print_Beforepassing As [Boolean]
        Public Print_Preview As [Boolean]
        Public Print_Terminal As [String]
        Public Print_NofCopeis_Rec As Integer
        Public Print_NofCopeis_Pay As Integer
        Public Print_Copiestoremarks As [String]
        Public Print_Heading As [String]
        Public Print_TransferVoucher As [Boolean]
        Public Print_ContraReceipt As [Boolean]
        Public Print_ContraPayment As [Boolean]
    End Structure



    '---------------------------------------------------------
    '         * Procedure         : SetUPPrint
    '         * DateTime          :9-Apr-2012
    '         * Author            :Prasanth
    '         * Purpose           :for Printer Setting
    '         * Functional Outline:
    '        *---------------------------------------------------------- 

    Public Function SetUPPrint(ByVal BranchCode As [String], ByVal Section As [String], ByVal SchCode As [String], ByVal TransID As [String], ByVal VrNo As [String], ByVal TranType As [String], _
     ByVal Copies As Integer, ByVal Preview As [Boolean], ByVal TranDate As DateTime, ByVal User As [String], ByVal TranData As DataTable, Optional ByVal TranFn As String = "") As [String]

        Dim Status As Boolean = False
        Dim isTranTypeboth As [String] = ""
        Dim isDebitTran As [Boolean] = False
        Dim isCreditTran As [Boolean] = False
        Dim NoCopies As Integer
        Dim iSTransferOnly As [Boolean]
        Dim PrintStr As [String] = ""

        HttpContext.Current.Session("PrintStr") = ""
        Dim DtPrint As New DataTable()
        Dim Qry As String
        Qry = "Select * From Print_Setup_P_Tbl Where Print_Sch_Code = '" & SchCode & "' And Br_Code = '" & BranchCode & "'"
        M_DBConn.ExecuteDataTable(DtPrint, Qry, False)


        If DtPrint.Rows.Count = 0 Then
            Return "false"
        End If

        Dim PSetup As New Printing_Setup()




        PSetup.Print_Needed = If(DtPrint.Rows(0)("Print_Needed").ToString() = "Y", True, False)
        PSetup.Print_TranType = DtPrint.Rows(0)("Print_TranType").ToString()
        PSetup.Print_Dialog = If(DtPrint.Rows(0)("Print_Dialog").ToString() = "Y", True, False)
        PSetup.Print_Beforepassing = If(DtPrint.Rows(0)("Print_Beforepassing").ToString() = "Y", True, False)
        PSetup.Print_Preview = If(DtPrint.Rows(0)("Print_Preview").ToString() = "Y", True, False)
        PSetup.Print_Terminal = DtPrint.Rows(0)("Print_Terminal").ToString()
        PSetup.Print_NofCopeis_Rec = Integer.Parse(DtPrint.Rows(0)("Print_NOC_RecCash").ToString())
        PSetup.Print_NofCopeis_Pay = Integer.Parse(DtPrint.Rows(0)("Print_NOC_PayCash").ToString())
        PSetup.Print_Copiestoremarks = DtPrint.Rows(0)("Print_Copiestoremarks").ToString()

        Dim M_CommonBL As New CommonBL



        Dim DtScheme As New DataTable
        Qry = "select * from Gen_SchemeMaster_P_Tbl where sch_code='" & SchCode & "'"
        M_DBConn.ExecuteDataTable(DtScheme, Qry, False)


        If DtScheme.Rows.Count = 0 Then
            PSetup.Print_Heading = "GENERAL "
        Else

            PSetup.Print_Heading = DtScheme.Rows(0)("Sch_Name").ToString()
        End If
        PSetup.Print_TransferVoucher = If(DtPrint.Rows(0)("Print_TransferVoucher").ToString() = "Y", True, False)
        PSetup.Print_ContraReceipt = If(DtPrint.Rows(0)("Print_ContraReceipt").ToString() = "Y", True, False)
        PSetup.Print_ContraPayment = If(DtPrint.Rows(0)("Print_ContraPayment").ToString() = "Y", True, False)

        If PSetup.Print_Needed = False Then
            Return "false"
        End If

        Dim DtVoucherType As New DataTable
        Qry = "Exec PrintVoucher_TranType_Sp '" & Section & "', '" & TransID & "', '" & BranchCode & "', '" & User & "','" & Format(TranDate, "MM/dd/yyyy") & "',''"
        M_DBConn.ExecuteDataTable(DtVoucherType, Qry, False)

        If DtVoucherType.Rows.Count = 0 Then
            Return "false"
        End If
        For i As Integer = 0 To DtVoucherType.Rows.Count - 1
            If Section = "LOAN" Then
                If DtVoucherType.Rows(i)("Tran_type").ToString() = TranType Then
                    isTranTypeboth = TranType
                End If
            ElseIf DtVoucherType.Rows(i)("Tran_type").ToString() = "C" Then
                isCreditTran = True
            ElseIf DtVoucherType.Rows(i)("Tran_type").ToString() = "D" Then
                isDebitTran = True
            End If
            'If isCreditTran = True AndAlso isDebitTran = True Then
            '    isTranTypeboth = "B"

            'End If
        Next
        For j As Integer = 0 To DtVoucherType.Rows.Count - 1
            'If DtVoucherType.Rows(j)("Tran_type").ToString() <> TranType Then
            '    GoTo Skip
            'End If
            If PSetup.Print_TranType <> "B" AndAlso PSetup.Print_TranType <> DtVoucherType.Rows(j)("Tran_type").ToString() Then


                GoTo Skip
            End If
            If PSetup.Print_TransferVoucher = False AndAlso DtVoucherType.Rows(j)("Is_TransferOnly").ToString() = "Y" Then
                GoTo Skip
            End If
            If PSetup.Print_ContraPayment = False AndAlso TranType = "C" AndAlso DtVoucherType.Rows(j)("Tran_type").ToString() = "D" Then
                GoTo Skip
            End If
            If PSetup.Print_ContraReceipt = False AndAlso TranType = "D" AndAlso DtVoucherType.Rows(j)("Tran_type").ToString() = "C" Then
                GoTo Skip
            End If

            If DtVoucherType.Rows(j)("Tran_type").ToString() = "C" Then
                NoCopies = PSetup.Print_NofCopeis_Rec
            Else
                NoCopies = PSetup.Print_NofCopeis_Pay
            End If
            If Section = "LOAN" Then
                NoCopies = Copies

            ElseIf Section = "GENERAL" Then
                NoCopies = Copies
            End If
            If DtVoucherType.Rows(j)("Is_TransferOnly").ToString() = "Y" Then
                iSTransferOnly = True
            End If




            'for (int p = 0; p <= Copies; p++) {
            HttpContext.Current.Session("PrintStr") = HttpContext.Current.Session("PrintStr") + PrintNew(BranchCode, Section, TransID, If(isTranTypeboth = "B", "B", DtVoucherType.Rows(j)("Tran_type").ToString()), PSetup.Print_Heading, Copies, _
             DtVoucherType.Rows(j)("Vr_No").ToString(), TranData, User, TranDate, IIf(SchCode = "LAF", "LAF", TranFn))
            Dim url As String = "PrintViewApplet.aspx"

            ShowPrintDialog(url, "_blank", "menubar=0,width=750,height=400,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes")
Skip:

            '}
            If isTranTypeboth = "B" Then
                Exit For

            End If
        Next
        Return PrintStr

    End Function



    Public Function SetUPPrintWODialogue(ByVal BranchCode As [String], ByVal Section As [String], ByVal SchCode As [String], ByVal TransID As [String], ByVal VrNo As [String], ByVal TranType As [String], _
 ByVal Copies As Integer, ByVal Preview As [Boolean], ByVal TranDate As DateTime, ByVal User As [String], ByVal TranData As DataTable, Optional ByVal TranFn As String = "") As [String]

        Dim Status As Boolean = False
        Dim isTranTypeboth As [String] = ""
        Dim isDebitTran As [Boolean] = False
        Dim isCreditTran As [Boolean] = False
        Dim NoCopies As Integer
        Dim iSTransferOnly As [Boolean]
        Dim PrintStr As [String] = ""

        HttpContext.Current.Session("PrintStr") = ""
        Dim DtPrint As New DataTable()
        Dim Qry As String
        Qry = "Select * From Print_Setup_P_Tbl Where Print_Sch_Code = '" & SchCode & "' And Br_Code = '" & BranchCode & "'"
        M_DBConn.ExecuteDataTable(DtPrint, Qry, False)


        If DtPrint.Rows.Count = 0 Then
            Return "false"
        End If

        Dim PSetup As New Printing_Setup()




        PSetup.Print_Needed = If(DtPrint.Rows(0)("Print_Needed").ToString() = "Y", True, False)
        PSetup.Print_TranType = DtPrint.Rows(0)("Print_TranType").ToString()
        PSetup.Print_Dialog = If(DtPrint.Rows(0)("Print_Dialog").ToString() = "Y", True, False)
        PSetup.Print_Beforepassing = If(DtPrint.Rows(0)("Print_Beforepassing").ToString() = "Y", True, False)
        PSetup.Print_Preview = If(DtPrint.Rows(0)("Print_Preview").ToString() = "Y", True, False)
        PSetup.Print_Terminal = DtPrint.Rows(0)("Print_Terminal").ToString()
        PSetup.Print_NofCopeis_Rec = Integer.Parse(DtPrint.Rows(0)("Print_NOC_RecCash").ToString())
        PSetup.Print_NofCopeis_Pay = Integer.Parse(DtPrint.Rows(0)("Print_NOC_PayCash").ToString())
        PSetup.Print_Copiestoremarks = DtPrint.Rows(0)("Print_Copiestoremarks").ToString()

        Dim M_CommonBL As New CommonBL



        Dim DtScheme As New DataTable
        Qry = "select * from Gen_SchemeMaster_P_Tbl where sch_code='" & SchCode & "'"
        M_DBConn.ExecuteDataTable(DtScheme, Qry, False)


        If DtScheme.Rows.Count = 0 Then
            PSetup.Print_Heading = "GENERAL "
        Else

            PSetup.Print_Heading = DtScheme.Rows(0)("Sch_Name").ToString()
        End If
        PSetup.Print_TransferVoucher = If(DtPrint.Rows(0)("Print_TransferVoucher").ToString() = "Y", True, False)
        PSetup.Print_ContraReceipt = If(DtPrint.Rows(0)("Print_ContraReceipt").ToString() = "Y", True, False)
        PSetup.Print_ContraPayment = If(DtPrint.Rows(0)("Print_ContraPayment").ToString() = "Y", True, False)

        If PSetup.Print_Needed = False Then
            Return "false"
        End If

        Dim DtVoucherType As New DataTable
        Qry = "Exec PrintVoucher_TranType_Sp '" & Section & "', '" & TransID & "', '" & BranchCode & "', '" & User & "','" & Format(TranDate, "MM/dd/yyyy") & "',''"
        M_DBConn.ExecuteDataTable(DtVoucherType, Qry, False)

        If DtVoucherType.Rows.Count = 0 Then
            Return "false"
        End If
        For i As Integer = 0 To DtVoucherType.Rows.Count - 1
            If Section = "LOAN" Then
                If DtVoucherType.Rows(i)("Tran_type").ToString() = TranType Then
                    isTranTypeboth = TranType
                End If
            ElseIf DtVoucherType.Rows(i)("Tran_type").ToString() = "C" Then
                isCreditTran = True
            ElseIf DtVoucherType.Rows(i)("Tran_type").ToString() = "D" Then
                isDebitTran = True
            End If
            If isCreditTran = True AndAlso isDebitTran = True Then
                isTranTypeboth = "B"

            End If
        Next
        For j As Integer = 0 To DtVoucherType.Rows.Count - 1
            'If DtVoucherType.Rows(j)("Tran_type").ToString() <> TranType Then
            '    GoTo Skip
            'End If
            If PSetup.Print_TranType <> "B" AndAlso PSetup.Print_TranType <> DtVoucherType.Rows(j)("Tran_type").ToString() Then


                GoTo Skip
            End If
            If PSetup.Print_TransferVoucher = False AndAlso DtVoucherType.Rows(j)("Is_TransferOnly").ToString() = "Y" Then
                GoTo Skip
            End If
            If PSetup.Print_ContraPayment = False AndAlso TranType = "C" AndAlso DtVoucherType.Rows(j)("Tran_type").ToString() = "D" Then
                GoTo Skip
            End If
            If PSetup.Print_ContraReceipt = False AndAlso TranType = "D" AndAlso DtVoucherType.Rows(j)("Tran_type").ToString() = "C" Then
                GoTo Skip
            End If

            If DtVoucherType.Rows(j)("Tran_type").ToString() = "C" Then
                NoCopies = PSetup.Print_NofCopeis_Rec
            Else
                NoCopies = PSetup.Print_NofCopeis_Pay
            End If
            If Section = "LOAN" Then
                NoCopies = Copies

            ElseIf Section = "GENERAL" Then
                NoCopies = Copies
            End If
            If DtVoucherType.Rows(j)("Is_TransferOnly").ToString() = "Y" Then
                iSTransferOnly = True
            End If




            'for (int p = 0; p <= Copies; p++) {
            HttpContext.Current.Session("PrintStr") = HttpContext.Current.Session("PrintStr") + PrintNew(BranchCode, Section, TransID, If(isTranTypeboth = "B", "B", DtVoucherType.Rows(j)("Tran_type").ToString()), PSetup.Print_Heading, Copies, _
             DtVoucherType.Rows(j)("Vr_No").ToString(), TranData, User, TranDate, IIf(SchCode = "LIF", "LIF", TranFn))
            '  Dim url As String = "PrintViewApplet.aspx"

            '   ShowPrintDialog(url, "_blank", "menubar=0,width=750,height=400,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes")
Skip:

            '}
            If isTranTypeboth = "B" Then
                Exit For

            End If
        Next
        Return PrintStr

    End Function



    '---------------------------------------------------------
    '        * Procedure         : Print
    '        * DateTime          :9-Apr-2012
    '        * Author            :Prasanth
    '        * Purpose           :for Printing Voucher
    '        * Functional Outline:
    '       *---------------------------------------------------------- 




    Private Function Print(ByVal BranchCode As [String], ByVal Section As [String], ByVal TransID As [String], ByVal TransType As [String], ByVal Heading As [String], ByVal NoOfCopies As Integer, _
     ByVal VoucherNo As [String], ByVal DtVoucher As DataTable, ByVal User As [String]) As [String]

        Dim PrintFormat As [String] = ""
        Dim PrintFooter As [String] = ""
        Dim StrPrint As [String] = ""
        Dim BuildPrintString As [String] = ""
        Dim Jump_Header As Integer = 0
        Dim Jump_Footer As Integer = 0
        Dim strVoucherName As [String] = ""


        'String PrintText="";
        Dim PrintRowNo As Integer = 0
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim LeftPos As Long = 0
        Dim PrintSpace As Long = 0
        Dim VoucherLength As Integer = 0
        Dim CashTotal As [Double] = 0
        Dim TransferTotal As [Double] = 0
        Dim Tr_Date As [String] = ""
        Dim WordsPrintLine As Integer = 0
        Dim WordsPrint As [Boolean] = False



        Dim Type As [String] = ""
        Dim CustomerDtls As [String] = ""
        Dim AccountDtls As [String] = ""
        Dim BankName As [String] = ""
        Dim BranchName As [String] = ""
        Dim PhoneNo As [String] = ""
        Dim Terminal As [String] = ""
        Dim Maker As [String] = ""
        Dim Checker As [String] = ""
        Dim Slno As Integer = 0
        Dim Narration As [String] = ""
        Dim Narration2 As [String] = ""
        Dim Prn_Bal As [Double] = 0
        Dim OD_Bal As [Double] = 0
        Dim Int_Bal As [Double] = 0
        Dim Remarks As [String] = ""
        Dim modcatg As [String] = ""



        Dim SlnoLeft As Integer = 0
        Dim SlnoWidth As Integer = 0
        Dim AcHeadLeft As Integer = 0
        Dim AcHeadWidth As Integer = 0
        Dim GLCodeLeft As Integer = 0
        Dim GLCodeWidth As Integer = 0
        Dim CashLeft As Integer = 0
        Dim CashWidth As Integer = 0
        Dim TransferLeft As Integer = 0
        Dim TransferWidth As Integer = 0
        Dim NarrationLeft As Integer = 0
        Dim NarrationWidth As Integer = 0
        Dim PrintDtls As [Boolean] = False


        Dim Str As [String] = If(TransType = "B", "Y", "N")

        Dim Qry As String
        Dim DtGetVouSetup As New DataTable
        Qry = "select * from VoucherFormat_P_Tbl order by RowNo,LeftPos,VouMode"
        M_DBConn.ExecuteDataTable(DtGetVouSetup, Qry, False)

        For k As Integer = 0 To DtGetVouSetup.Rows.Count - 1

            If DtGetVouSetup.Rows(k)("Details") Is "HEADER" Then
                Jump_Header = Integer.Parse(DtGetVouSetup.Rows(k)("Width").ToString())
            ElseIf DtGetVouSetup.Rows(k)("Details") Is "FOOTER" Then
                Jump_Footer = Integer.Parse(DtGetVouSetup.Rows(k)("Width").ToString())
            End If
        Next
        For p As Integer = 0 To Jump_Header
            PrintFormat = "<BR>"
        Next



        Qry = "Exec PrintVoucher_Sp '" & Section & "','" & TransID & "','" & TransType & "','', '" & VoucherNo & "','" & BranchCode & "','" & User & "'"
        M_DBConn.ExecuteDataTable(DtVoucher, Qry, False)



        If DtVoucher.Rows.Count > 0 Then
            TransType = DtVoucher.Rows(0)("TransType").ToString()
            VoucherNo = If(DtVoucher.Rows(0)("VoucherNo").ToString() <> "", DtVoucher.Rows(0)("VoucherNo").ToString(), "0")
            Tr_Date = DtVoucher.Rows(0)("Tr_Date").ToString()
            CustomerDtls = If(DtVoucher.Rows(0)("CustomerDtls").ToString() <> "", DtVoucher.Rows(0)("CustomerDtls").ToString(), "")
            AccountDtls = DtVoucher.Rows(0)("AccountDtls").ToString()
            BankName = DtVoucher.Rows(0)("BankName").ToString()
            BranchName = DtVoucher.Rows(0)("BranchName").ToString()
            PhoneNo = DtVoucher.Rows(0)("PhoneNo").ToString()
            Prn_Bal = [Double].Parse(DtVoucher.Rows(0)("Balance").ToString())
            OD_Bal = [Double].Parse(DtVoucher.Rows(0)("OD_Balance").ToString())
            Int_Bal = [Double].Parse(DtVoucher.Rows(0)("Int_Balance").ToString())
            Terminal = DtVoucher.Rows(0)("Terminal").ToString()
            Maker = DtVoucher.Rows(0)("Maker").ToString()
            Checker = DtVoucher.Rows(0)("Checker").ToString()
            Narration = DtVoucher.Rows(0)("Narration").ToString()
            modcatg = DtVoucher.Rows(0)("modulecatg").ToString()
            Remarks = DtVoucher.Rows(0)("Remarks").ToString()
            strVoucherName = DtVoucher.Rows(0)("VoucherName").ToString()
            Narration2 = If(DtVoucher.Rows(0)("Narration2").ToString() <> "", DtVoucher.Rows(0)("Narration2").ToString(), "")
        End If

        For m As Integer = 0 To DtGetVouSetup.Rows.Count - 1
            If DtGetVouSetup.Rows(m)("VouMode").ToString() = "2" AndAlso PrintDtls = False Then
                BuildPrintString = ""
                For t As Integer = 0 To DtVoucher.Rows.Count - 1
                    Slno = Slno + 1
                    If SlnoLeft > 0 Then
                        PrintFormat = AddSpace(SlnoLeft)
                    End If
                    If SlnoWidth > 0 Then
                        PrintFormat = PrintFormat & Left(Slno & AddSpace(SlnoWidth).ToString(), SlnoWidth)
                    End If
                    If AcHeadLeft > (SlnoLeft + SlnoWidth) Then
                        PrintFormat = PrintFormat & AddSpace(AcHeadLeft - (SlnoLeft + SlnoWidth))
                    End If
                    If AcHeadWidth > 0 Then
                        PrintFormat = PrintFormat & Left(DtVoucher.Rows(t)("AccHead").ToString() & AddSpace(AcHeadWidth), AcHeadWidth)
                    End If
                    If GLCodeLeft > (AcHeadLeft + AcHeadWidth) Then
                        PrintFormat = PrintFormat & AddSpace(GLCodeLeft - (AcHeadLeft + AcHeadWidth))
                    End If
                    If GLCodeWidth > 0 Then
                        PrintFormat = PrintFormat & Left(DtVoucher.Rows(t)("Gl_Code").ToString() & AddSpace(GLCodeWidth), GLCodeWidth)
                    End If
                    If CashLeft < TransferLeft Then
                        If CashLeft > (SlnoLeft + SlnoWidth + AcHeadLeft + AcHeadWidth) Then
                            PrintFormat = PrintFormat & AddSpace(CashLeft - (SlnoLeft + SlnoWidth + AcHeadLeft + AcHeadWidth))
                        End If
                        PrintFormat = PrintFormat & Right(AddSpace(CashWidth) & FormatSkipZero([Double].Parse(DtVoucher.Rows(t)("Cash").ToString()), "#0.00"), CashWidth)

                        If TransferLeft > (SlnoLeft + SlnoWidth + AcHeadLeft + AcHeadWidth + CashLeft + CashWidth) Then
                            PrintFormat = PrintFormat & AddSpace(CashLeft - (SlnoLeft + SlnoWidth + AcHeadLeft + AcHeadWidth + CashLeft + CashWidth))
                        End If
                        PrintFormat = PrintFormat & Right(AddSpace(TransferWidth) & FormatSkipZero([Double].Parse(DtVoucher.Rows(t)("Transfer").ToString()), "#0.00"), TransferWidth)
                        CashTotal = CashTotal + [Double].Parse(DtVoucher.Rows(t)("Cash").ToString())

                        TransferTotal = TransferTotal + [Double].Parse(DtVoucher.Rows(t)("Transfer").ToString())
                    Else
                        If TransferLeft > (SlnoLeft + SlnoWidth + AcHeadLeft + AcHeadWidth) Then
                            PrintFormat = PrintFormat & AddSpace(TransferLeft - (SlnoLeft + SlnoWidth + AcHeadLeft + AcHeadWidth))
                        End If
                        PrintFormat = PrintFormat & Right(AddSpace(TransferWidth) & FormatSkipZero([Double].Parse(DtVoucher.Rows(t)("Transfer").ToString()), "#0.00"), TransferWidth)
                        If CashLeft > (SlnoLeft + SlnoWidth + AcHeadLeft + AcHeadWidth + TransferLeft + TransferWidth) Then
                            PrintFormat = PrintFormat & AddSpace(CashLeft - (SlnoLeft + SlnoWidth + AcHeadLeft + AcHeadWidth + TransferLeft + TransferWidth))
                        End If

                        PrintFormat = PrintFormat & Right(AddSpace(CashWidth) & FormatSkipZero([Double].Parse(DtVoucher.Rows(t)("Cash").ToString()), "#0.00"), CashWidth)
                        CashTotal = CashTotal + [Double].Parse(DtVoucher.Rows(t)("Cash").ToString())


                        TransferTotal = TransferTotal + [Double].Parse(DtVoucher.Rows(t)("Transfer").ToString())
                    End If
                    BuildPrintString = BuildPrintString & PrintFormat
                    PrintFormat = ""
                    PrintRowNo = PrintRowNo + 1
                Next

                PrintDtls = True
            End If
            If Integer.Parse(DtGetVouSetup.Rows(m)("RowNo").ToString()) > PrintRowNo Then
                If WordsPrint = True Then
                    WordsPrint = False
                    If PrintFooter.Length > VoucherLength Then
                        PrintFooter = PrintFooter.ToString().Substring(1, VoucherLength)
                        PrintFooter = PrintFooter & AddSpace(Integer.Parse(PrintSpace.ToString())) & PrintFooter.ToString().Substring(1, VoucherLength + 1)
                        WordsPrintLine = 2
                        BuildPrintString = PrintFooter
                        PrintFooter = ""
                    Else
                        PrintFooter = BuildPrintString & PrintFooter
                        BuildPrintString = PrintFooter
                        PrintFooter = ""

                    End If
                Else
                    PrintFooter = BuildPrintString & PrintFooter
                    PrintFooter = ""
                End If
                PrintFooter = ""
                Dim y As Integer = PrintRowNo
                While y + 1 < Integer.Parse(DtGetVouSetup.Rows(m)("RowNo").ToString())
                    PrintFooter = PrintFooter & "@"
                    y += 1
                End While
                BuildPrintString = BuildPrintString & PrintFooter
                PrintFooter = ""
                PrintRowNo = Integer.Parse(DtGetVouSetup.Rows(m)("RowNo").ToString())
            End If
            If PrintFooter.Length = 0 Then
                PrintSpace = Long.Parse(DtGetVouSetup.Rows(m)("LeftPos").ToString())
                BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString()))
                LeftPos = PrintSpace
            Else
                PrintSpace = Long.Parse(DtGetVouSetup.Rows(m)("LeftPos").ToString()) - LeftPos
                BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString()))
            End If
            If Long.Parse(DtGetVouSetup.Rows(m)("VouMode").ToString()) = 0 OrElse Long.Parse(DtGetVouSetup.Rows(m)("VouMode").ToString()) = 2 Then
                'PrintFooter = DtGetVouSetup.Rows[m]["Details"].ToString();

                Select Case DtGetVouSetup.Rows(m)("Details").ToString().ToUpper()
                    Case "VOUCHER NO"
                        PrintFooter = VoucherNo
                        Exit Select
                    Case "CUSTOMER DTLS"
                        PrintFooter = CustomerDtls
                        Exit Select
                    Case "TRANS DATE"
                        PrintFooter = "aaaaa"
                        Exit Select
                    Case "ACCOUNT DTLS"
                        PrintFooter = AccountDtls
                        Exit Select
                    Case "MAKER"
                        PrintFooter = Maker & "            Time:- " & DateTime.Now.TimeOfDay.ToString()
                        Exit Select
                    Case "TERMINAL"
                        PrintFooter = ""
                        Exit Select
                    Case "TOTAL CASH"
                        PrintFooter = FormatSkipZero(CashTotal, "#0.00")
                        Exit Select
                    Case "TOTAL TRANSFER"
                        PrintFooter = FormatSkipZero(TransferTotal, "#0.00")
                        Exit Select
                    Case "GRAND TOTAL"
                        PrintFooter = FormatSkipZero(CashTotal + TransferTotal, "#0.00")
                        Exit Select
                    Case "TOTAL IN WORDS"
                        If TransType = "B" Then
                            PrintFooter = NumberToText(Integer.Parse(TransferTotal.ToString()), True, False)
                        Else
                            PrintFooter = NumberToText(Integer.Parse(CashTotal.ToString()) + Integer.Parse(TransferTotal.ToString()), True, False)
                        End If
                        WordsPrint = True
                        Exit Select
                    Case "BRANCH"
                        PrintFooter = BranchName
                        Exit Select
                    Case "SCHEME"
                        If TransType = "C" Then
                            PrintFooter = Heading.ToString().ToUpper() & " RECEIPT"
                        Else
                            PrintFooter = Heading.ToString().ToUpper() & " PAYMENT"
                        End If
                        Exit Select
                    Case "BALANCE"
                        If Prn_Bal > 0 Then
                            PrintFooter = "Bal: " & Prn_Bal
                        End If
                        If OD_Bal > 0 Then
                            PrintFooter = PrintFooter & "OD Bal: " & OD_Bal
                        End If
                        If Int_Bal > 0 Then
                            PrintFooter = PrintFooter & "    Int Bal: " & Int_Bal
                        End If
                        Exit Select
                    Case "NARRATION"
                        PrintFooter = Narration
                        Exit Select
                    Case "NARRATION2"
                        PrintFooter = Narration2
                        Exit Select
                    Case "REMARKS"
                        PrintFooter = Remarks
                        Exit Select
                    Case "VOUCHERNAME"
                        PrintFooter = strVoucherName
                        Exit Select
                    Case "JOURNAL"

                        Exit Select
                End Select
                PrintFooter = BuildPrintString & PrintFooter
                BuildPrintString = PrintFooter
                'PrintFooter = "";
                If PrintSpace < 0 Then
                    PrintSpace = 0
                End If
                If DtGetVouSetup.Rows(m)("Alignment").ToString() = "L" Then
                    PrintFooter = PrintFooter & AddSpace(Integer.Parse(PrintSpace.ToString()))
                    If DtGetVouSetup.Rows(m)("PrintType").ToString() = "0" Then
                        PrintFooter = PrintFooter & Left(BuildPrintString & AddSpace(Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())), Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString()))
                    ElseIf DtGetVouSetup.Rows(m)("PrintType").ToString() = "1" Then
                        PrintFooter = PrintFooter & Left(BuildPrintString & AddSpace(Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())), Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString()))
                    End If
                    Dim TempStr As [String] = BuildPrintString
                    BuildPrintString = PrintFooter
                    PrintFooter = ""
                    TempStr = ""
                Else

                    PrintFooter = PrintFooter & AddSpace(Integer.Parse(PrintSpace.ToString()))
                    If DtGetVouSetup.Rows(m)("PrintType").ToString() = "0" Then
                        PrintFooter = PrintFooter & Right(AddSpace(Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())) & PrintFooter, Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString()))
                    ElseIf DtGetVouSetup.Rows(m)("PrintType").ToString() = "1" Then
                        PrintFooter = PrintFooter & Right(AddSpace(Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())) & PrintFooter, Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString()))
                    End If
                    Dim TempStr As [String] = BuildPrintString
                    BuildPrintString = PrintFooter
                    '+ TempStr;
                    PrintFooter = ""
                    TempStr = ""
                End If
            Else
                Select Case DtGetVouSetup.Rows(m)("Details").ToString()
                    Case "SLNO"
                        SlnoLeft = Integer.Parse(DtGetVouSetup.Rows(m)("LeftPos").ToString())
                        SlnoWidth = Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())
                        Exit Select
                    Case "ACCOUNT HEAD"
                        AcHeadLeft = Integer.Parse(DtGetVouSetup.Rows(m)("LeftPos").ToString())
                        AcHeadWidth = Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())
                        Exit Select
                    Case "CASH"
                        CashLeft = Integer.Parse(DtGetVouSetup.Rows(m)("LeftPos").ToString())
                        CashWidth = Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())
                        Exit Select
                    Case "TRANSFER"
                        TransferLeft = Integer.Parse(DtGetVouSetup.Rows(m)("LeftPos").ToString())
                        TransferWidth = Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())
                        Exit Select
                    Case "NARRATION"
                        NarrationLeft = Integer.Parse(DtGetVouSetup.Rows(m)("LeftPos").ToString())
                        NarrationWidth = Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())
                        Exit Select
                    Case "GLCODE"
                        GLCodeLeft = Integer.Parse(DtGetVouSetup.Rows(m)("LeftPos").ToString())
                        GLCodeWidth = Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())
                        Exit Select


                End Select
            End If

            LeftPos = LeftPos + Integer.Parse(DtGetVouSetup.Rows(m)("Width").ToString())

            StrPrint = StrPrint & BuildPrintString
            BuildPrintString = ""
        Next


        Return StrPrint
        PrintFooter = ""

    End Function


    Private Function PrintNew(ByVal BranchCode As [String], ByVal Section As [String], ByVal TransID As [String], ByVal TransType As [String], ByVal Heading As [String], ByVal NoOfCopies As Integer, _
     ByVal VoucherNo As [String], ByVal DtVoucher As DataTable, ByVal User As [String], ByVal TranDate As DateTime, ByVal TranFunction As [String]) As [String]
        Dim HeaderSpace As [String] = ""
        Dim HeaderDetails As [String] = ""
        Dim VoucherDetails As [String] = ""
        Dim FooterDetails As [String] = ""
        Dim FooterSpace As [String] = ""
        Dim PrintFormat As [String] = ""
        Dim Str As [String] = If(TransType = "B", "Y", "N")
        Dim Qry As String
        Dim DtGetVouSetup As New DataTable
        Qry = "select * from VoucherFormat_P_Tbl order by RowNo,slno,LeftPos,VouMode"
        M_DBConn.ExecuteDataTable(DtGetVouSetup, Qry, False)


        Qry = "Exec PrintVoucher_Sp '" & Section & "','" & TransID & "','" & TransType & "','', '" & VoucherNo & "','" & BranchCode & "','" & User & "', '" & TranDate.ToString("MM/dd/yyyy") & "', '" & TranFunction & "'"
        'M_DBConn.ExecuteReader(Qry, Connection.SQLSource.STOREDPROCEDURE)
        Dim DtTmp As New DataTable
        M_DBConn.ExecuteDataTable(DtTmp, Qry, False)

        Qry = "select * from VoucherData where Terminal='" & User & "'"

        'Qry = "select * from VoucherData where Terminal='" & User & "' and VoucherNo='" & VoucherNo & "'"
        'Qry = "select * from VoucherData where Terminal='" & User & "' and Tran_No ='" & TransID & "'"
        M_DBConn.ExecuteDataTable(DtVoucher, Qry, False)


        Dim PSetup As New Printing_Setup()
        HeaderSpace = SkipHeader(DtGetVouSetup)

        HttpContext.Current.Session("PrintHeading") = Heading
        HeaderDetails = BuildHeader(DtGetVouSetup, DtVoucher, 0, Heading)
        VoucherDetails = BuildDetails(DtGetVouSetup, DtVoucher, 1, LastRow)
        FooterDetails = BuildFooter(DtGetVouSetup, DtVoucher, 2, LastRow)
        FooterSpace = SkipFooter(DtGetVouSetup)

        PrintFormat = HeaderSpace & HeaderDetails & VoucherDetails & FooterDetails & FooterSpace
        HttpContext.Current.Session("VoucherDet") = DtVoucher

        Return PrintFormat

    End Function


    '---------------------------------------------------------
    '      * Procedure         : AddSpace
    '      * DateTime          :9-Apr-2012
    '      * Author            :Prasanth
    '      * Purpose           :for Adding space
    '      * Functional Outline:
    '     *---------------------------------------------------------- 




    Private Function AddSpace(ByVal Howmuch As Integer) As [String]
        Dim Spce As [String] = ""
        For i As Integer = 0 To Howmuch - 1
            'Spce =Spce + "&nbsp;";
            Spce = Spce & "~"
        Next

        Return Spce
    End Function



    '---------------------------------------------------------
    '         * Procedure         : AddSpace
    '         * DateTime          :9-Apr-2012
    '         * Author            :Prasanth
    '         * Purpose           :for taking left substring
    '         * Functional Outline:
    '        *---------------------------------------------------------- 




    Public Function Left(ByVal text As String, ByVal length As Integer) As String
        Dim RetStr As [String] = ""
        If length < 0 Then
            RetStr = ""
        ElseIf length = 0 OrElse text.Length = 0 Then
            RetStr = ""
        ElseIf text.Length <= length Then
            RetStr = text
        Else
            RetStr = text.Substring(0, length)
        End If

        If RetStr.ToString().Contains("&nbsp;") = True Then
            If RetStr.Length > 6 Then
                If Right(RetStr, 6) <> "&nbsp;" Then


                    Dim StrLast As Integer = RetStr.LastIndexOf("&")


                    'RetStr = Right(RetStr, StrLast);
                    'RetStr = Left(RetStr, RetStr.Length);
                    'String BuildSpace = "";
                    'for (int o = StrLast; StrLast <= 6; o++) {
                    '    BuildSpace =  RetStr.Substring(StrLast, RetStr.Length);
                    '} 

                    'RetStr = Left(RetStr, int.Parse(RetStr.ToString().Length.ToString()) - StrLast);
                    RetStr = RetStr.Remove(StrLast, RetStr.Length - StrLast)
                    'else {
                    '  RetStr = "&nbsp;";
                    '}
                End If
            End If
        End If


        Return RetStr

    End Function


    '---------------------------------------------------------
    '          * Procedure         : Right
    '          * DateTime          :9-Apr-2012
    '          * Author            :Prasanth
    '          * Purpose           :for taking Right substring
    '          * Functional Outline:
    '         *---------------------------------------------------------- 




    Public Function Right(ByVal value As String, ByVal length As Integer) As String
        Dim Str As [String] = ""
        Str = value.Substring(value.Length - length)
        Return Str
    End Function



    '---------------------------------------------------------
    '          * Procedure         : FormatSkipZero
    '          * DateTime          :9-Apr-2012
    '          * Author            :Prasanth
    '          * Purpose           :
    '          * Functional Outline:
    '         *---------------------------------------------------------- 




    Public Function FormatSkipZero(ByVal Value As [Double], ByVal FormatType As [String]) As String
        Dim t As [String] = ""
        If Value = 0 Then
            t = AddSpace(FormatType.Length)
        Else
            t = Value.ToString(FormatType)
        End If
        Return t
    End Function




    '---------------------------------------------------------
    '          * Procedure         : NumberToText
    '          * DateTime          :9-Apr-2012
    '          * Author            :Prasanth
    '          * Purpose           :
    '          * Functional Outline:
    '         *---------------------------------------------------------- 




    Public Function NumberToText(ByVal number As Integer, ByVal useAnd As Boolean, ByVal useArab As Boolean) As String
        If number = 0 Then
            Return "Zero"
        End If
        Dim [and] As String = If(useAnd, "and ", "")
        ' deals with UK or US numbering
        If number = -2147483648 Then
            Return "Minus Two Billion One Hundred " & [and] & "Forty Seven Million Four Hundred " & [and] & "Eighty Three Thousand " & "Six Hundred " & [and] & "Forty Eight"
        End If
        Dim num As Integer() = New Integer(3) {}
        Dim first As Integer = 0
        Dim u As Integer, h As Integer, t As Integer
        Dim sb As New System.Text.StringBuilder()
        If number < 0 Then
            sb.Append("Minus ")
            number = -number
        End If
        Dim words0 As String() = {"", "One ", "Two ", "Three ", "Four ", "Five ", _
         "Six ", "Seven ", "Eight ", "Nine "}
        Dim words1 As String() = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", _
         "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen "}
        Dim words2 As String() = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", _
         "Eighty ", "Ninety "}
        Dim words3 As String() = {"Thousand ", "Million ", "Billion "}
        num(0) = number Mod 1000
        ' units
        num(1) = number \ 1000
        num(2) = number \ 1000000
        num(1) = num(1) - 1000 * num(2)
        ' thousands
        num(3) = number \ 1000000000
        ' billions
        num(2) = num(2) - 1000 * num(3)
        ' millions
        For i As Integer = 3 To 1 Step -1
            If num(i) <> 0 Then
                first = i
                Exit For
            End If
        Next
        For i As Integer = first To 0 Step -1
            If num(i) = 0 Then
                Continue For
            End If
            u = num(i) Mod 10
            ' ones
            t = num(i) \ 10
            h = num(i) \ 100
            ' hundreds
            t = t - 10 * h
            ' tens
            If h > 0 Then
                sb.Append(words0(h) & "Hundred ")
            End If
            If u > 0 OrElse t > 0 Then
                If h > 0 OrElse i < first Then
                    sb.Append([and])
                End If
                If t = 0 Then
                    sb.Append(words0(u))
                ElseIf t = 1 Then
                    sb.Append(words1(u))
                Else
                    sb.Append(words2(t - 2) & words0(u))
                End If
            End If
            If i <> 0 Then
                sb.Append(words3(i - 1))
            End If
        Next
        Return "Rupees " & sb.ToString().TrimEnd() & " Only"
    End Function



    '---------------------------------------------------------
    '          * Procedure         : SkipHeader
    '          * DateTime          :9-Apr-2012
    '          * Author            :Prasanth
    '          * Purpose           :to Skip Header
    '          * Functional Outline:
    '         *---------------------------------------------------------- 





    Public Function SkipHeader(ByVal DtHeaderSpace As DataTable) As [String]
        Dim SkipTop As [String] = ""
        For i As Integer = 0 To DtHeaderSpace.Rows.Count - 2
            If DtHeaderSpace.Rows(i)("Details").ToString() = "HEADER" Then
                For j As Integer = 0 To Integer.Parse(DtHeaderSpace.Rows(i)("Width").ToString())
                    SkipTop = SkipTop & "@"
                Next
            End If
        Next
        Return SkipTop
    End Function


    '---------------------------------------------------------
    '              * Procedure         : SkipFooter
    '              * DateTime          :9-Apr-2012
    '              * Author            :Prasanth
    '              * Purpose           :to Skip Footer
    '              * Functional Outline:
    '             *---------------------------------------------------------- 




    Public Function SkipFooter(ByVal DtHeaderSpace As DataTable) As [String]
        Dim SkipBottom As [String] = ""
        For i As Integer = 0 To DtHeaderSpace.Rows.Count - 2
            If DtHeaderSpace.Rows(i)("Details").ToString() = "FOOTER" Then
                For j As Integer = 0 To Integer.Parse(DtHeaderSpace.Rows(i)("Width").ToString())
                    SkipBottom = SkipBottom & "@"
                Next
            End If
        Next
        Return SkipBottom
    End Function


    '---------------------------------------------------------
    '             * Procedure         : BuildHeader
    '             * DateTime          :9-Apr-2012
    '             * Author            :Prasanth
    '             * Purpose           :to Build Header
    '             * Functional Outline:
    '            *---------------------------------------------------------- 



    Public Function BuildHeader(ByVal DtHeader As DataTable, ByVal DtVoucher As DataTable, ByVal VouMode As Integer, ByVal Header As [String]) As [String]
        Dim StrHeader As [String] = ""

        Dim PrintRowNo As Integer = 0
        Dim LeftPos As Long = 0
        Dim PrintSpace As Long = 0
        Dim VoucherLength As Integer = 0
        Dim CashTotal As [Double] = 0
        Dim TransferTotal As [Double] = 0
        Dim WordsPrintLine As Integer = 0
        Dim WordsPrint As [Boolean] = False
        Dim GrTotal As Double


        If DtVoucher.Rows.Count = 0 Then
            Return "false"
        End If

        'Printing_Setup PSetup = new Printing_Setup();

        Dim TransType As [String] = DtVoucher.Rows(0)("TransType").ToString()
        Dim VoucherNo As [String] = If(DtVoucher.Rows(0)("VoucherNo").ToString() <> "", DtVoucher.Rows(0)("VoucherNo").ToString(), "0")
        Dim Tr_Date As [String] = DtVoucher.Rows(0)("Tr_Date").ToString()
        Dim CustomerDtls As [String] = If(DtVoucher.Rows(0)("CustomerDtls").ToString() <> "", DtVoucher.Rows(0)("CustomerDtls").ToString(), "")
        Dim AccountDtls As [String] = DtVoucher.Rows(0)("AccountDtls").ToString()
        Dim BankName As [String] = DtVoucher.Rows(0)("BankName").ToString()
        Dim BranchName As [String] = DtVoucher.Rows(0)("BranchName").ToString()
        Dim PhoneNo As [String] = DtVoucher.Rows(0)("PhoneNo").ToString()
        Dim Prn_Bal As [Double] = [Double].Parse(DtVoucher.Rows(0)("Balance").ToString())
        Dim OD_Bal As [Double] = [Double].Parse(DtVoucher.Rows(0)("OD_Balance").ToString())
        Dim Int_Bal As [Double] = [Double].Parse(DtVoucher.Rows(0)("Int_Balance").ToString())
        Dim Terminal As [String] = DtVoucher.Rows(0)("Terminal").ToString()
        Dim Maker As [String] = DtVoucher.Rows(0)("Maker").ToString()
        Dim Checker As [String] = DtVoucher.Rows(0)("Checker").ToString()
        Dim Narration As [String] = DtVoucher.Rows(0)("Narration").ToString()
        Dim modcatg As [String] = DtVoucher.Rows(0)("modulecatg").ToString()
        Dim Remarks As [String] = DtVoucher.Rows(0)("Remarks").ToString()
        Dim TranDate As DateTime = DateTime.Parse(Tr_Date.ToString())
        Dim Heading As [String] = Header
        Dim BuildPrintString As [String] = ""
        Dim PrevRowNo As Integer = 0
        Dim SkipLine As [String] = ""

        For i As Integer = 0 To DtHeader.Rows.Count - 1
            If Double.Parse(DtHeader.Rows(i)("VouMode").ToString()) = VouMode Then
                PrintSpace = Integer.Parse(DtHeader.Rows(i)("LeftPos").ToString())
                PrintRowNo = Integer.Parse(DtHeader.Rows(i)("RowNo").ToString())

                For k As Integer = PrevRowNo To PrintRowNo - 1
                    If PrevRowNo <> PrintRowNo Then
                        SkipLine = SkipLine & "@"
                    End If
                Next


                Select Case DtHeader.Rows(i)("Details").ToString().ToUpper()
                    Case "VOUCHER NO"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & VoucherNo
                        Exit Select
                    Case "CUSTOMER DTLS"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & AccountDtls & " - " & CustomerDtls
                        Exit Select
                    Case "TRANS DATE"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & TranDate.ToString("dd/MM/yyyy")
                        Exit Select
                    Case "ACCOUNT DTLS"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & AccountDtls
                        Exit Select
                    Case "MAKER"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & Maker & "            Time:- " & Convert.ToString(DateTime.Now.TimeOfDay)
                        Exit Select
                    Case "TERMINAL"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & Terminal
                        Exit Select
                    Case "TOTAL CASH"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & FormatSkipZero([Double].Parse(CashTotal.ToString()), "#0.00")
                        Exit Select
                    Case "TOTAL TRANSFER"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & FormatSkipZero([Double].Parse(TransferTotal.ToString()), "#0.00")
                        Exit Select
                    Case "GRAND TOTAL"
                        GrTotal = CashTotal + TransferTotal
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & FormatSkipZero([Double].Parse(GrTotal.ToString()), "#0.00")
                        Exit Select
                    Case "TOTAL IN WORDS"
                        GrTotal = CashTotal + TransferTotal
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & NumberToText(Integer.Parse(GrTotal.ToString()), True, False)
                        WordsPrint = True
                        Exit Select
                    Case "BRANCH"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & Left(BranchName & AddSpace(39), 39) & Trim(PhoneNo)
                        Exit Select
                    Case "BRANCH_NAMEONLY"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & BranchName
                        Exit Select
                    Case "BRANCH_PHONE"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & ""
                        Exit Select
                    Case "SCHEME"
                        If TransType = "C" Then
                            BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & Heading.ToUpper() & " RECEIPT"
                        Else
                            BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & Heading.ToUpper() & " PAYMENT"
                        End If
                        Exit Select
                    Case "BALANCE"
                        If Prn_Bal > 0 Then
                            BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & "Bal: " & Prn_Bal
                        End If
                        If OD_Bal > 0 Then
                            BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & "OD Bal: " & OD_Bal
                        End If
                        If Int_Bal > 0 Then
                            BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & "    Int Bal: " & Int_Bal
                        End If
                        Exit Select
                    Case "NARRATION"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & Narration
                        Exit Select
                    Case "PRINTNARRATION"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & Narration
                        Exit Select
                    Case "REMARKS"
                        BuildPrintString = AddSpace(Integer.Parse(PrintSpace.ToString())) & Remarks
                        Exit Select
                End Select

                If BuildPrintString.Length > 0 Then
                    'BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(DtHeader.Rows(i)("Width").ToString()))
                    BuildPrintString = Left(BuildPrintString + AddSpace(Integer.Parse(DtHeader.Rows(i)("Width").ToString())), Len(AddSpace(Integer.Parse(DtHeader.Rows(i)("Width").ToString()))))

                    'for (int j = 0; j <= int.Parse(DtHeader.Rows[i]["RowNo"].ToString()); j++)
                    '{
                    '    if (j <= PrevRowNo) {
                    '        BuildPrintString = BuildPrintString + "@";
                    '    }

                    '}

                End If
            End If

            StrHeader = StrHeader & SkipLine & BuildPrintString
            BuildPrintString = ""
            PrevRowNo = PrintRowNo
            SkipLine = ""
        Next
        LastRow = PrintRowNo
        Return StrHeader
    End Function


    '---------------------------------------------------------
    '            * Procedure         : BuildDetails
    '            * DateTime          :9-Apr-2012
    '            * Author            :Prasanth
    '            * Purpose           :to Build Print Details
    '            * Functional Outline:
    '           *---------------------------------------------------------- 



    Public Function BuildDetails(ByVal DtDetails As DataTable, ByVal DtVoucher As DataTable, ByVal VouMode As Integer, ByVal LastRow As Integer) As [String]
        Dim StrDetails As [String] = ""
        Dim BuildPrintString As [String] = ""
        Dim PrintSpace As Integer = 0




        Dim SlnoLeft As Integer = 0
        Dim SlnoWidth As Integer = 0
        Dim AcHeadLeft As Integer = 0
        Dim AcHeadWidth As Integer = 0
        Dim GLCodeLeft As Integer = 0
        Dim GLCodeWidth As Integer = 0
        Dim CashLeft As Integer = 0
        Dim CashWidth As Integer = 0
        Dim TransferLeft As Integer = 0
        Dim TransferWidth As Integer = 0
        Dim NarrationLeft As Integer = 0
        Dim NarrationWidth As Integer = 0
        Dim PrintDtls As [Boolean] = False
        Dim SkipLine As [String] = ""
        Dim PrintRowNo As Integer = 0


        For i As Integer = 0 To DtDetails.Rows.Count - 2
            If Double.Parse(DtDetails.Rows(i)("VouMode").ToString()) = VouMode Then
                PrintRowNo = Integer.Parse(DtDetails.Rows(i)("RowNo").ToString())
                If PrintDtls = False Then
                    For p As Integer = LastRow To PrintRowNo - 2
                        SkipLine = SkipLine & "@"
                    Next
                    PrintDtls = True
                End If
                Select Case DtDetails.Rows(i)("Details").ToString().ToUpper()
                    Case "SLNO"
                        SlnoLeft = Integer.Parse(DtDetails.Rows(i)("LeftPos").ToString())
                        SlnoWidth = Integer.Parse(DtDetails.Rows(i)("Width").ToString())
                        Exit Select
                    Case "ACCOUNT HEAD"
                        AcHeadLeft = Integer.Parse(DtDetails.Rows(i)("LeftPos").ToString())
                        AcHeadWidth = Integer.Parse(DtDetails.Rows(i)("Width").ToString())
                        Exit Select
                    Case "CASH"
                        CashLeft = Integer.Parse(DtDetails.Rows(i)("LeftPos").ToString())
                        CashWidth = Integer.Parse(DtDetails.Rows(i)("Width").ToString())
                        Exit Select
                    Case "TRANSFER"
                        TransferLeft = Integer.Parse(DtDetails.Rows(i)("LeftPos").ToString())
                        TransferWidth = Integer.Parse(DtDetails.Rows(i)("Width").ToString())
                        Exit Select
                    Case "NARRATION"
                        NarrationLeft = Integer.Parse(DtDetails.Rows(i)("LeftPos").ToString())
                        NarrationWidth = Integer.Parse(DtDetails.Rows(i)("Width").ToString())
                        Exit Select
                End Select
            End If
        Next


        BuildPrintString = SkipLine

        Dim Narration As [String] = ""
        Dim Prn_Bal As [Double] = 0
        Dim Terminal As [String] = ""
        Dim Maker As [String] = ""
        Dim Checker As [String] = ""
        Dim Slno As Integer = 0
        Dim Narration2 As [String] = ""
        Dim OD_Bal As [Double] = 0
        Dim Int_Bal As [Double] = 0
        Dim Remarks As [String] = ""
        Dim modcatg As [String] = ""


        LastRow = PrintRowNo

        If DtVoucher.Rows.Count > 0 Then
            Narration = DtVoucher.Rows(0)("Narration").ToString()
            Prn_Bal = [Double].Parse(DtVoucher.Rows(0)("Balance").ToString())
            Int_Bal = [Double].Parse(DtVoucher.Rows(0)("Int_Balance").ToString())
            Terminal = DtVoucher.Rows(0)("Terminal").ToString()
            Maker = DtVoucher.Rows(0)("Maker").ToString()
            Checker = DtVoucher.Rows(0)("Checker").ToString()
            Narration = DtVoucher.Rows(0)("Narration").ToString()
            modcatg = DtVoucher.Rows(0)("modulecatg").ToString()
            Remarks = DtVoucher.Rows(0)("Remarks").ToString()
        End If
        NoOfSkippingHeader = 0

        For j As Integer = 0 To DtVoucher.Rows.Count - 1
            BuildPrintString = BuildPrintString & AddSpace(SlnoLeft) & Left(j + 1 & AddSpace(SlnoLeft), SlnoWidth)
            BuildPrintString = BuildPrintString & AddSpace(AcHeadLeft) & Left(DtVoucher.Rows(j)("Acchead").ToString() & AddSpace(AcHeadWidth), AcHeadWidth)
            BuildPrintString = BuildPrintString & AddSpace(CashLeft) & Left(FormatSkipZero([Double].Parse(DtVoucher.Rows(j)("Cash").ToString()), "#0.00") & AddSpace(CashLeft), CashWidth)
            BuildPrintString = BuildPrintString & AddSpace(TransferLeft) & Left(FormatSkipZero([Double].Parse(DtVoucher.Rows(j)("TRANSFER").ToString()), "#0.00") & AddSpace(TransferLeft), TransferWidth)
            BuildPrintString = BuildPrintString & "@"
            LastRow = LastRow + j
            NoOfSkippingHeader = NoOfSkippingHeader + 1
        Next




        StrDetails = BuildPrintString
        Return StrDetails
    End Function

    '---------------------------------------------------------
    '        * Procedure         : BuildDetails
    '        * DateTime          :9-Apr-2012
    '        * Author            :Prasanth
    '        * Purpose           :to Build Print Footer Details
    '        * Functional Outline:
    '       *---------------------------------------------------------- 



    Public Function BuildFooter(ByVal DtDetails As DataTable, ByVal DtVoucher As DataTable, ByVal VouMode As Integer, ByVal LastRow As Integer) As [String]
        Dim StrFooter As [String] = ""

        Dim SkipLine As [String] = ""

        Dim Narration As [String] = ""
        Dim Prn_Bal As [Double] = 0
        Dim Terminal As [String] = ""
        Dim Maker As [String] = ""
        Dim Checker As [String] = ""
        Dim Slno As Integer = 0
        Dim Narration2 As [String] = ""
        Dim OD_Bal As [Double] = 0
        Dim Int_Bal As [Double] = 0
        Dim Remarks As [String] = ""
        Dim modcatg As [String] = ""
        Dim CashTotal As [Double] = [Double].Parse(DtVoucher.Compute("Sum(Cash)", "").ToString())
        Dim TransferTotal As [Double] = [Double].Parse(DtVoucher.Compute("Sum(Transfer)", "").ToString())

        Dim PrintSpace As Integer = 0

        Dim PrintRowNo As Integer = 0
        Dim BuildPrintString As [String] = ""


        If DtVoucher.Rows.Count > 0 Then
            Narration = DtVoucher.Rows(0)("Narration").ToString()
            Prn_Bal = [Double].Parse(DtVoucher.Rows(0)("Balance").ToString())
            Int_Bal = [Double].Parse(DtVoucher.Rows(0)("Int_Balance").ToString())
            Terminal = DtVoucher.Rows(0)("Terminal").ToString()
            Maker = DtVoucher.Rows(0)("Maker").ToString()
            Checker = DtVoucher.Rows(0)("Checker").ToString()
            Narration = DtVoucher.Rows(0)("Narration").ToString()
            modcatg = DtVoucher.Rows(0)("modulecatg").ToString()
            Remarks = DtVoucher.Rows(0)("Remarks").ToString()
        End If

        LastRow = LastRow + DtDetails.Rows.Count
        For l As Integer = 0 To DtDetails.Rows.Count - 1
            If Double.Parse(DtDetails.Rows(l)("VouMode").ToString()) = VouMode Then
                LastRow = Integer.Parse(DtDetails.Rows(l - 1)("RowNo").ToString())
                If LastRow = 13 And NoOfSkippingHeader = 2 Then
                    PrintRowNo = Integer.Parse(DtDetails.Rows(l)("RowNo").ToString()) - (2)
                ElseIf LastRow = 13 And NoOfSkippingHeader = 3 Then
                    PrintRowNo = Integer.Parse(DtDetails.Rows(l)("RowNo").ToString()) - (4)
                ElseIf LastRow = 13 And NoOfSkippingHeader = 4 Then
                    PrintRowNo = Integer.Parse(DtDetails.Rows(l)("RowNo").ToString()) - (6)
                ElseIf LastRow = 13 And NoOfSkippingHeader = 5 Then
                    PrintRowNo = Integer.Parse(DtDetails.Rows(l)("RowNo").ToString()) - (8)
                Else

                    PrintRowNo = Integer.Parse(DtDetails.Rows(l)("RowNo").ToString())
                End If

                For p As Integer = LastRow To PrintRowNo - 1
                    BuildPrintString = BuildPrintString & "@"
                Next
                PrintSpace = Integer.Parse(DtDetails.Rows(l)("LeftPos").ToString())
                Select Case DtDetails.Rows(l)("Details").ToString().ToUpper()
                    Case "BALANCE"
                        If Prn_Bal > 0 Then
                            BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & "Bal: " & Prn_Bal
                        End If
                        If OD_Bal > 0 Then
                            BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & "OD Bal: " & OD_Bal
                        End If
                        If Int_Bal > 0 Then
                            BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & "    Int Bal: " & Int_Bal
                        End If
                        Exit Select
                    Case "NARRATION"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & Narration
                        Exit Select
                    Case "PRINTNARRATION"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & Narration
                        Exit Select
                    Case "REMARKS"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & Remarks
                        Exit Select
                    Case "MAKER"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & Maker & "            Time:- " & Convert.ToString(Format(DateTime.Now, "HH:mm:ss"))
                        Exit Select
                    Case "TERMINAL"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & Terminal
                        Exit Select
                    Case "TOTAL CASH"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & FormatSkipZero([Double].Parse(CashTotal.ToString()), "#0.00")
                        Exit Select
                    Case "TOTAL TRANSFER"
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & FormatSkipZero([Double].Parse(TransferTotal.ToString()), "#0.00")
                        Exit Select
                    Case "GRAND TOTAL"
                        Dim GrTotal As [Double] = CashTotal + TransferTotal
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & FormatSkipZero([Double].Parse(GrTotal.ToString()), "#0.00")
                        Exit Select
                    Case "TOTAL IN WORDS"
                        Dim GrTotal As Double = CashTotal + TransferTotal
                        BuildPrintString = BuildPrintString & AddSpace(Integer.Parse(PrintSpace.ToString())) & NumberToText(Double.Parse(GrTotal.ToString()), True, False)
                        Exit Select

                End Select

                LastRow = PrintRowNo

                SkipLine = ""
            End If
        Next
        StrFooter = BuildPrintString
        Return StrFooter
    End Function


    '---------------------------------------------------------
    '        * Procedure         : BuildDetails
    '        * DateTime          :9-Apr-2012
    '        * Author            :Prasanth
    '        * Purpose           :to show print dialog
    '        * Functional Outline:
    '       *---------------------------------------------------------- 

    Public Sub ShowPrintDialog(ByVal url As String, ByVal target As String, ByVal windowFeatures As String)
        Dim context As HttpContext = HttpContext.Current

        If ([String].IsNullOrEmpty(target) OrElse target.Equals("_self", StringComparison.OrdinalIgnoreCase)) AndAlso [String].IsNullOrEmpty(windowFeatures) Then

            context.Response.Redirect(url)
        Else
            Dim page As Page = DirectCast(context.Handler, Page)
            If page Is Nothing Then
                Throw New InvalidOperationException("Cannot redirect to new window outside Page context.")
            End If
            url = page.ResolveClientUrl(url)

            Dim script As String
            windowFeatures = windowFeatures & ",scrollbars=yes"
            If Not [String].IsNullOrEmpty(windowFeatures) Then
                script = "window.open(""{0}"", ""{1}"", ""{2}"", ""{1}"");"
            Else
                script = "window.open(""{0}"", ""{1}"");"
            End If

            script = [String].Format(script, url, target, windowFeatures)
            ScriptManager.RegisterStartupScript(page, GetType(Page), "Redirect", script, True)
        End If
    End Sub

    Public Sub PrintFDBond(ByVal FdDetails As DataTable)



    End Sub
End Class
