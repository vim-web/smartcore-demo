Imports SMARTCORE_BL
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Partial Public Class PrnView
    Inherits System.Web.UI.Page
    Dim DtRpt As New DataTable
    Dim DtTotal As New DataTable
    Dim FormID As Integer = 1118
    Dim HrCnt As Integer
    Dim FrCnt As Integer
    Dim GrdCnt As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not IsPostBack Then
                GC.Collect()
                Load_Report()
                GenerateText()
                Session("PreUrl") = Request.UrlReferrer.ToString()
            End If
            Page.ClientScript.RegisterStartupScript(Me.[GetType](), "ListPrinters", "findPrinters();", True)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Load_Report()
        Dim M_Ds As DataSet
        Dim M_Qry As String
        Dim M_Heading As String = ""
        Dim M_HeadTitle As String = ""
        Dim M_CommonBL As New CommonBL
        Try
            Dim M_HeaderDt As DataTable
            Dim M_FooterDt As DataTable
            M_Qry = Session("RptQry")
            M_Ds = M_CommonBL.Fill_RptData(M_Qry)
            DtRpt = M_Ds.Tables(1)
            GvRpt.DataSource = DtRpt
            GvRpt.DataBind()
            Session("ReportTbl") = DtRpt

            M_HeaderDt = M_Ds.Tables(0)
            M_FooterDt = M_Ds.Tables(2)

            HrCnt = M_Ds.Tables(0).Rows.Count
            FrCnt = M_Ds.Tables(2).Rows.Count
            GrdCnt = DtRpt.Rows.Count

            Dim Header As String
            Dim Footer As String
            Dim HeaderAlign As String

            For i = 0 To M_HeaderDt.Rows.Count - 1
                HeaderAlign = "C"

                Try
                    HeaderAlign = M_HeaderDt.Rows(i)(1).ToString()
                Catch ex As Exception

                End Try

                Header = M_HeaderDt.Rows(i)(0).ToString
                If Header.Length > 0 Then
                    Select Case i
                        Case 0
                            lblHeading1.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading1.ID = HeaderAlign
                            DivHeader1.Visible = True
                        Case 1
                            lblHeading2.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading2.ID = HeaderAlign
                            DivHeader2.Visible = True
                        Case 2
                            lblHeading3.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading3.ID = HeaderAlign
                            DivHeader3.Visible = True
                        Case 3
                            lblHeading4.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading4.ID = HeaderAlign
                            DivHeader4.Visible = True
                        Case 4
                            lblHeading5.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading5.ID = HeaderAlign
                            DivHeader5.Visible = True
                        Case 5
                            lblHeading6.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading6.ID = HeaderAlign
                            DivHeader6.Visible = True
                        Case 6
                            lblHeading7.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading7.ID = HeaderAlign
                            DivHeader7.Visible = True
                        Case 7
                            lblHeading8.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading8.ID = HeaderAlign
                            DivHeader8.Visible = True
                        Case 8
                            lblHeading9.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading9.ID = HeaderAlign
                            DivHeader9.Visible = True
                        Case 9
                            lblHeading10.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading10.ID = HeaderAlign
                            DivHeader10.Visible = True
                        Case 10
                            lblHeading11.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading11.ID = HeaderAlign
                            DivHeader11.Visible = True
                        Case 11
                            lblHeading12.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading12.ID = HeaderAlign
                            DivHeader12.Visible = True
                        Case 12
                            lblHeading13.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading13.ID = HeaderAlign
                            DivHeader13.Visible = True
                        Case 13
                            lblHeading14.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading14.ID = HeaderAlign
                            DivHeader14.Visible = True
                        Case 14
                            lblHeading15.Text = M_HeaderDt.Rows(i)(0).ToString()
                            lblHeading15.ID = HeaderAlign
                            DivHeader15.Visible = True
                    End Select
                End If

            Next

            For i = 0 To M_FooterDt.Rows.Count - 1
                Select Case i
                    Case 0
                        lblFooter1.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter1.Visible = True
                    Case 1
                        lblFooter2.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter2.Visible = True
                    Case 2
                        lblFooter3.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter3.Visible = True
                    Case 3
                        lblFooter4.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter4.Visible = True
                    Case 4
                        lblFooter5.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter5.Visible = True
                    Case 5
                        lblFooter6.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter6.Visible = True
                    Case 6
                        lblFooter7.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter7.Visible = True
                    Case 7
                        lblFooter8.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter8.Visible = True
                    Case 8
                        lblFooter9.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter9.Visible = True
                    Case 9
                        lblFooter10.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter10.Visible = True
                    Case 10
                        lblFooter11.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter11.Visible = True
                    Case 11
                        lblFooter12.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter12.Visible = True
                    Case 12
                        lblFooter13.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter13.Visible = True
                    Case 13
                        lblFooter14.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter14.Visible = True
                    Case 14
                        lblFooter15.Text = M_FooterDt.Rows(i)(0).ToString()
                        DivFooter15.Visible = True
                End Select

            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GvRpt_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvRpt.RowDataBound
        Try
            Dim HeaderArr(25, 4) As String
            HeaderArr = Session("RptHeader")
            Dim j As Integer
            Dim HeadStr As String
            Dim Colwidth As Integer
            Dim ColAlign As String

            If e.Row.RowType = DataControlRowType.Header Then
                For j = 0 To 25
                    If j = 0 Then
                        e.Row.Cells(j).Width = 0
                        e.Row.Cells(j).Visible = False
                    Else
                        If Val(HeaderArr(j - 1, 2)) > 0 Then
                            HeadStr = HeaderArr(j - 1, 0)
                            Colwidth = HeaderArr(j - 1, 2)
                            ColAlign = HeaderArr(j - 1, 1)

                            e.Row.Cells(j).Text = HeadStr
                            e.Row.Cells(j).Width = Colwidth

                            If ColAlign = "L" Then
                                e.Row.Cells(j).HorizontalAlign = HorizontalAlign.Left
                            Else
                                e.Row.Cells(j).HorizontalAlign = HorizontalAlign.Right
                            End If
                        End If
                    End If
                Next
            ElseIf e.Row.RowType = DataControlRowType.DataRow Then
                For j = 0 To 25

                    If e.Row.Cells(0).Text = "*" Or e.Row.Cells(0).Text = "+" Or e.Row.Cells(0).Text = "-" Or e.Row.Cells(0).Text = "$" Then
                        e.Row.Cells(j).BackColor = Drawing.Color.FromName("#EEEFFE")
                        'e.Row.Cells(j).ForeColor = Drawing.Color.Black
                        e.Row.Cells(j).Font.Bold = True
                    End If
                    If e.Row.Cells(0).Text = "R" Then
                        e.Row.Cells(j).BackColor = Drawing.Color.FromName("#5f9be8")
                        'e.Row.Cells(j).ForeColor = Drawing.Color.Black
                        e.Row.Cells(j).Font.Bold = True
                    End If
                    If j = 0 Then
                        e.Row.Cells(j).Width = 0
                        e.Row.Cells(j).Visible = False
                    Else
                        If Val(HeaderArr(j - 1, 2)) > 0 Then
                            HeadStr = HeaderArr(j - 1, 0)
                            Colwidth = HeaderArr(j - 1, 2)
                            ColAlign = HeaderArr(j - 1, 1)
                            If e.Row.Cells(j).Text = "01/01/1900" Then e.Row.Cells(j).Text = ""
                            If e.Row.Cells(j).Text = "01-01-1900" Then e.Row.Cells(j).Text = ""
                            If ColAlign = "L" Then
                                e.Row.Cells(j).HorizontalAlign = HorizontalAlign.Left
                                If e.Row.Cells(j).Text.Trim = "0" Then e.Row.Cells(j).Text = ""
                            Else
                                If Val(e.Row.Cells(j).Text) = 0 Then e.Row.Cells(j).Text = ""
                                e.Row.Cells(j).HorizontalAlign = HorizontalAlign.Right
                            End If
                        End If
                    End If
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function PrintHeading(ByRef LineNo As Integer, ByRef HeadLines As Integer, Optional ByVal HeaderSpace As Boolean = True) As String

        Dim PageWidth As Integer
        Dim MidLngth As Integer
        Dim StrHead As String
        PageWidth = Request.QueryString("PageType")
        StrHead = ""
        HeadLines = 0

        If Len(lblHeading1.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading1.Text)) / 2
            If lblHeading1.ID = "L" Then
                MidLngth = 1
                DivHeader1.Style.Add("text-align", "left")
            End If

            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading1.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading2.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading2.Text)) / 2
            If lblHeading2.ID = "L" Then
                MidLngth = 1
                DivHeader2.Style.Add("text-align", "left")
            End If

            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading2.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading3.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading3.Text)) / 2
            If lblHeading3.ID = "L" Then
                MidLngth = 1
                DivHeader3.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading3.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading4.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading4.Text)) / 2
            If lblHeading4.ID = "L" Then
                MidLngth = 1
                DivHeader4.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading4.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading5.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading5.Text)) / 2
            If lblHeading5.ID = "L" Then
                MidLngth = 1
                DivHeader5.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading5.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading6.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading6.Text)) / 2
            If lblHeading6.ID = "L" Then
                MidLngth = 1
                DivHeader6.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading6.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading7.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading7.Text)) / 2
            If lblHeading7.ID = "L" Then
                MidLngth = 1
                DivHeader7.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading7.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading8.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading8.Text)) / 2
            If lblHeading8.ID = "L" Then
                MidLngth = 1
                DivHeader8.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading8.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading9.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading9.Text)) / 2
            If lblHeading9.ID = "L" Then
                MidLngth = 1
                DivHeader9.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading9.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading10.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading10.Text)) / 2
            If lblHeading10.ID = "L" Then
                MidLngth = 1
                DivHeader10.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading10.Text) + "@"
            HeadLines = HeadLines + 1
        End If



        If Len(lblHeading11.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading11.Text)) / 2
            If lblHeading11.ID = "L" Then
                MidLngth = 1
                DivHeader11.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading11.Text) + "@"
            HeadLines = HeadLines + 1
        End If


        If Len(lblHeading12.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading12.Text)) / 2
            If lblHeading12.ID = "L" Then
                MidLngth = 1
                DivHeader12.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading12.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading13.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading13.Text)) / 2
            If lblHeading13.ID = "L" Then
                MidLngth = 1
                DivHeader13.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading13.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading14.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading14.Text)) / 2
            If lblHeading14.ID = "L" Then
                MidLngth = 1
                DivHeader14.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading14.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        If Len(lblHeading15.Text) > 0 Then
            MidLngth = Int(PageWidth - Len(lblHeading15.Text)) / 2
            If lblHeading15.ID = "L" Then
                MidLngth = 1
                DivHeader15.Style.Add("text-align", "left")
            End If
            If MidLngth < 0 Then MidLngth = 2
            StrHead = StrHead + Space(MidLngth) + Trim(lblHeading15.Text) + "@"
            HeadLines = HeadLines + 1
        End If

        Dim j As Int16
        Dim PrStr As String
        Dim ColWidth As Int16
        Dim HeaderArr(10, 4) As String
        HeaderArr = Session("RptHeader")
        For j = 1 To DtRpt.Columns.Count - 1
            ColWidth = Val(HeaderArr(j - 1, 3)) + 1
            PrStr = StrDup(ColWidth, "=")
            StrHead = StrHead + PrStr
        Next
        StrHead = StrHead + "@"
        HeadLines = HeadLines + 1
        For j = 1 To DtRpt.Columns.Count - 1
            If HeaderArr(j - 1, 1) = "L" Then
                PrStr = Left(HeaderArr(j - 1, 0) + Space(HeaderArr(j - 1, 3)), HeaderArr(j - 1, 3)) + " "
            Else
                PrStr = Right(Space(HeaderArr(j - 1, 3)) + HeaderArr(j - 1, 0), HeaderArr(j - 1, 3)) + " "
            End If
            StrHead = StrHead + PrStr
        Next
        StrHead = StrHead + "@"
        HeadLines = HeadLines + 1
        For j = 1 To DtRpt.Columns.Count - 1
            ColWidth = Val(HeaderArr(j - 1, 3)) + 1
            PrStr = StrDup(ColWidth, "-")
            StrHead = StrHead + PrStr
        Next
        StrHead = StrHead + "@"
        HeadLines = HeadLines + 1
        LineNo = LineNo + HeadLines
        Return StrHead

    End Function

    Private Sub GenerateText()
        Dim PrntStr As New StringBuilder
        Dim PrintStr As String = ""
        Dim LineNo As Long
        Dim i As Long
        Dim k As Long
        Dim Prtex As String
        Dim StartLnNo As Integer = 0
        Dim LnToBePrint As Integer
        Dim LnPerPage As Integer
        Dim HeadLnNo As Integer
        Dim ColWidth As Int16
        Dim PageNo As Int16 = 1
        LnPerPage = 72
        Dim PagePrintFrom As Integer = Val(TxtPageNo.Text)
        Dim PagePrintEnd As Integer = Val(TxtPageNoTo.Text)


        LnToBePrint = 66


        Dim SkipLine As Integer
        If Val(TxtPageNo.Text) > 0 Then
            PageNo = Val(TxtPageNo.Text)
        End If

        PrntStr.Append(PrintHeading(LineNo, HeadLnNo, False))
        Dim HeaderArr(10, 4) As String
        HeaderArr = Session("RptHeader")
        Dim PrStr As String
        If Val(TxtPageNo.Text) > 0 Then
            StartLnNo = (Val(TxtPageNo.Text) - 1) * (LnToBePrint - HeadLnNo)
            LineNo = HeadLnNo
        End If

        If PagePrintFrom = 0 Then
            PagePrintFrom = 0
        End If


        If PagePrintEnd = 0 Then
            PagePrintEnd = 999999999
        End If

        If StartLnNo > 0 Then
            Dim p As Integer = 0
            For p = StartLnNo To 1 Step -1
                If DtRpt.Rows(p)(0).ToString = "-" Or DtRpt.Rows(p)(0).ToString = "+" Then
                    SkipLine = SkipLine + 1
                End If
                If DtRpt.Rows(p)(0).ToString = "$" Then
                    SkipLine = SkipLine + 2
                End If
            Next
            StartLnNo = StartLnNo - SkipLine + 2
        End If


        Dim PageToEnd As Integer


        For i = StartLnNo To GvRpt.Rows.Count - 1

            PageToEnd = Math.Round((GvRpt.Rows.Count / (72 - HrCnt - FrCnt)), 0)

            If Val(TxtPageNo.Text) > 0 Then
                If DtRpt.Rows(StartLnNo - 1)(0).ToString = "+" Or DtRpt.Rows(StartLnNo - 1)(0).ToString = "$" Then
                    For k = 1 To DtRpt.Columns.Count - 1
                        ColWidth = Val(HeaderArr(k - 1, 3)) + 1
                        PrStr = StrDup(ColWidth, "=")
                        PrntStr.Append(PrStr)
                    Next
                    PrntStr.Append("@")
                    LineNo = LineNo + 1
                End If
            End If


            Prtex = ""

            If LineNo > 63 Then
                LineNo = LineNo
            End If
            If DtRpt.Rows(i)(0).ToString = "-" Or DtRpt.Rows(i)(0).ToString = "$" Then
                If (LineNo) Mod (LnToBePrint) = 0 Then
                    PrntStr.Append("@")
                    PrntStr.Append("Page No." & +PageNo)
                    PrntStr.Append("@@@@@")
                    LineNo = 0
                    PageNo = PageNo + 1

                    If Val(PageNo) > PagePrintEnd Then
                        Exit For
                    End If
                    PrntStr.Append(PrintHeading(LineNo, HeadLnNo))
                End If

                For k = 1 To DtRpt.Columns.Count - 1
                    ColWidth = Val(HeaderArr(k - 1, 3)) + 1
                    PrStr = StrDup(ColWidth, "-")
                    PrntStr.Append(PrStr)
                Next

                PrntStr.Append("@")

                LineNo = LineNo + 1    ''' SKIPING LINE
            End If

            If (LineNo) Mod (LnToBePrint) = 0 Then
                PrntStr.Append("@")
                PrntStr.Append("Page No." & +PageNo)
                PrntStr.Append("@@@@@")
                LineNo = 0
                PageNo = PageNo + 1

                If Val(PageNo) > PagePrintEnd Then
                    Exit For
                End If
                PrntStr.Append(PrintHeading(LineNo, HeadLnNo))
            End If

            For J = 1 To DtRpt.Columns.Count - 1
                If HeaderArr(J - 1, 1) = "L" Then
                    If DtRpt.Rows(i)(J).ToString = "01/01/1900" Then
                        PrStr = Left(Space(10) + Space(HeaderArr(J - 1, 3)), HeaderArr(J - 1, 3))
                    Else
                        PrStr = Left(DtRpt.Rows(i)(J).ToString + Space(HeaderArr(J - 1, 3)), HeaderArr(J - 1, 3))
                    End If

                    Prtex = Prtex + PrStr + " "
                Else
                    PrStr = FormatAmt(Val(DtRpt.Rows(i)(J).ToString), HeaderArr(J - 1, 3))
                    Prtex = Prtex + PrStr + " "
                End If
            Next J

            LineNo = LineNo + 1
            PrntStr.Append(Prtex + "@")
            If DtRpt.Rows(i)(0).ToString = "+" Or DtRpt.Rows(i)(0).ToString = "$" Then
                If (LineNo) Mod (LnToBePrint) = 0 Then
                    PrntStr.Append("@")
                    PrntStr.Append("Page No." & +PageNo)
                    PrntStr.Append("@@@@@")
                    LineNo = 0
                    PageNo = PageNo + 1

                    If Val(PageNo) > PagePrintEnd Then
                        Exit For
                    End If
                    PrntStr.Append(PrintHeading(LineNo, HeadLnNo))
                End If

                For k = 1 To DtRpt.Columns.Count - 1
                    ColWidth = Val(HeaderArr(k - 1, 3)) + 1
                    PrStr = StrDup(ColWidth, "=")
                    PrntStr.Append(PrStr)
                Next
                PrntStr.Append("@")
                LineNo = LineNo + 1


                If (LineNo) Mod (LnToBePrint) = 0 Then
                    PrntStr.Append("@")
                    PrntStr.Append("Page No." & +PageNo)
                    PrntStr.Append("@@@@@")
                    LineNo = 0
                    PageNo = PageNo + 1

                    If Val(PageNo) > PagePrintEnd Then
                        Exit For
                    End If
                    PrntStr.Append(PrintHeading(LineNo, HeadLnNo))
                End If
            End If

        Next i


        For j = 1 To DtRpt.Columns.Count - 1
            ColWidth = Val(HeaderArr(j - 1, 3)) + 1
            PrStr = StrDup(ColWidth, "-")
            PrntStr.Append(PrStr)
        Next
        PrntStr.Append("@")
        LineNo = LineNo + 1
        If DivFooter1.Visible = True Then
            PrntStr.Append(lblFooter1.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter2.Visible = True Then
            PrntStr.Append(lblFooter2.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter3.Visible = True Then
            PrntStr.Append(lblFooter3.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter4.Visible = True Then
            PrntStr.Append(lblFooter4.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter5.Visible = True Then
            PrntStr.Append(lblFooter5.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter6.Visible = True Then
            PrntStr.Append(lblFooter6.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter7.Visible = True Then
            PrntStr.Append(lblFooter7.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter8.Visible = True Then
            PrntStr.Append(lblFooter8.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter9.Visible = True Then
            PrntStr.Append(lblFooter9.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter10.Visible = True Then
            PrntStr.Append(lblFooter10.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter11.Visible = True Then
            PrntStr.Append(lblFooter11.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter12.Visible = True Then
            PrntStr.Append(lblFooter12.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter13.Visible = True Then
            PrntStr.Append(lblFooter13.Text + "@")
            LineNo = LineNo + 1
        End If

        If DivFooter14.Visible = True Then
            PrntStr.Append(lblFooter14.Text + "@")
            LineNo = LineNo + 1
        End If
        Dim SkipLn As Integer
        For SkipLn = LineNo To 72
            PrntStr.Append("@")
        Next
        HdnText.Value = PrntStr.ToString
        PrintStr = ""

        LblPageno1.Text = "PageNo 1 -" & PageNo

    End Sub

    'Private Sub GenerateText()
    '    Dim PrintStr As String = ""
    '    Dim LineNo As Long
    '    Dim i As Long
    '    Dim k As Long
    '    Dim Prtex As String
    '    Dim StartLnNo As Integer = 0
    '    Dim LnToBePrint As Integer
    '    Dim LnPerPage As Integer
    '    Dim HeadLnNo As Integer
    '    Dim ColWidth As Int16
    '    Dim PageNo As Int16 = 1
    '    LnPerPage = 72
    '    LnToBePrint = 68
    '    If Val(TxtPageNo.Text) > 0 Then
    '        PageNo = Val(TxtPageNo.Text)
    '    End If
    '    PrintStr = PrintStr + PrintHeading(LineNo, HeadLnNo, False)
    '    Dim HeaderArr(10, 4) As String
    '    HeaderArr = Session("RptHeader")
    '    Dim PrStr As String
    '    If Val(TxtPageNo.Text) > 0 Then
    '        StartLnNo = (Val(TxtPageNo.Text) - 1) * (68 - HeadLnNo)
    '        LineNo = HeadLnNo
    '    End If

    '    For i = StartLnNo To GvRpt.Rows.Count - 1

    '        Prtex = ""
    '        If DtRpt.Rows(i)(0).ToString = "-" Or DtRpt.Rows(i)(0).ToString = "$" Then
    '            For k = 1 To DtRpt.Columns.Count - 1
    '                ColWidth = Val(HeaderArr(k - 1, 3)) + 1
    '                PrStr = StrDup(ColWidth, "-")
    '                PrintStr = PrintStr + PrStr
    '            Next
    '            PrintStr = PrintStr + "@"

    '            LineNo = LineNo + 1
    '        End If

    '        If (LineNo) Mod (LnToBePrint) = 0 Then
    '            PrintStr = PrintStr + "@"
    '            PrintStr = PrintStr + "Page No." & +PageNo
    '            PrintStr = PrintStr + "@@@"
    '            LineNo = 0
    '            PageNo = PageNo + 1
    '            PrintStr = PrintStr + PrintHeading(LineNo, HeadLnNo)
    '        End If

    '        For J = 1 To DtRpt.Columns.Count - 1
    '            If HeaderArr(J - 1, 1) = "L" Then
    '                PrStr = Left(DtRpt.Rows(i)(J).ToString + Space(HeaderArr(J - 1, 3)), HeaderArr(J - 1, 3))
    '                Prtex = Prtex + PrStr + " "
    '            Else
    '                PrStr = FormatAmt(DtRpt.Rows(i)(J).ToString, HeaderArr(J - 1, 3))
    '                Prtex = Prtex + PrStr + " "
    '            End If
    '        Next J

    '        LineNo = LineNo + 1
    '        PrintStr = PrintStr + Prtex + "@"
    '        If DtRpt.Rows(i)(0).ToString = "+" Or DtRpt.Rows(i)(0).ToString = "$" Then

    '            If (LineNo - StartLnNo) Mod (LnToBePrint) = 0 Then
    '                PrintStr = PrintStr + "@@@@"
    '                LineNo = 0
    '                PrintStr = PrintStr + PrintHeading(LineNo, HeadLnNo)
    '            End If

    '            For k = 1 To DtRpt.Columns.Count - 1
    '                ColWidth = Val(HeaderArr(k - 1, 3)) + 1
    '                PrStr = StrDup(ColWidth, "=")
    '                PrintStr = PrintStr + PrStr
    '            Next
    '            PrintStr = PrintStr + "@"
    '            LineNo = LineNo + 1
    '        End If

    '    Next i


    '    For j = 1 To DtRpt.Columns.Count - 1
    '        ColWidth = Val(HeaderArr(j - 1, 3)) + 1
    '        PrStr = StrDup(ColWidth, "-")
    '        PrintStr = PrintStr + PrStr
    '    Next
    '    PrintStr = PrintStr + "@"

    '    If DivFooter1.Visible = True Then
    '        PrintStr = PrintStr + lblFooter1.Text + "@"
    '    End If

    '    If DivFooter2.Visible = True Then
    '        PrintStr = PrintStr + lblFooter2.Text + "@"
    '    End If

    '    If DivFooter3.Visible = True Then
    '        PrintStr = PrintStr + lblFooter3.Text + "@"
    '    End If

    '    If DivFooter4.Visible = True Then
    '        PrintStr = PrintStr + lblFooter4.Text + "@"
    '    End If

    '    HdnText.Value = PrintStr
    '    PrintStr = ""
    'End Sub
    Public Function FormatAmt(ByVal FormatVal As Double, ByVal FormatLength As String) As String
        Dim M_FormatAmt As String
        If FormatVal = 0 Then
            M_FormatAmt = Space(FormatLength)
        Else
            M_FormatAmt = Right(Space(FormatLength) & Format(FormatVal, "#0.00"), FormatLength)
        End If
        Return M_FormatAmt
    End Function


    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdClose.Click
        Response.Redirect(Session("PreUrl"))
    End Sub

    Protected Sub TxtPageNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtPageNo.TextChanged
        DtRpt = DirectCast(Session("ReportTbl"), DataTable)

        GenerateText()

    End Sub

    Protected Sub BtnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnWord.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=SafeReport.doc")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-word "
        Dim sw As New System.IO.StringWriter()
        Dim hw As New HtmlTextWriter(sw)
        ExportDiv.Visible = False
        divPrint.RenderControl(hw)
        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.[End]()
        ExportDiv.Visible = True
    End Sub



    Protected Sub GvRpt_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GvRpt.SelectedIndexChanged

    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click


        Dim attachment As String = "attachment; filename=Report.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sWriter As New System.IO.StringWriter()
        Dim htwWriter As New HtmlTextWriter(sWriter)
        divPrint.RenderControl(htwWriter)
        Response.Write(sWriter.ToString())
        Response.[End]()
        'ANSHAD
        'Dim attachment As String = "attachment; filename=Report.xls"
        'Response.ClearContent()
        'Response.AddHeader("content-disposition", attachment)
        'Response.ContentType = "application/ms-excel"
        'Response.ContentType.ToString()
        'Dim sWriter As StringWriter = New StringWriter()
        'Dim htwWriter As HtmlTextWriter = New HtmlTextWriter(sWriter)

        'For i As Integer = 0 To GvRpt.Rows.Count - 1
        '    Dim row As GridViewRow = GvRpt.Rows(i)
        '    row.Attributes.Add("class", "textmode")
        'Next

        'divPrint.RenderControl(htwWriter)
        'Dim style As String = "<style> .textmode { mso-number-format:\@; } </style>"
        'Response.Write(style)
        'Response.ContentType = "application/text"
        'Response.Write(sWriter.ToString())
        'Response.[End]()
        'ANSHAD

        ''Response.ContentType = "application/pdf"
        ''Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf")
        ''Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ''Dim sw As New StringWriter()
        ''Dim hw As New HtmlTextWriter(sw)
        ''ExportDiv.Visible = False
        ''divPrint.RenderControl(hw)
        ''Dim sr As New StringReader(sw.ToString())
        ''Dim pdfDoc As New Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
        ''Dim htmlparser As New HTMLWorker(pdfDoc)
        ''PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
        ''pdfDoc.Open()
        ''htmlparser.Parse(sr)
        ''pdfDoc.Close()
        ''Response.Write(pdfDoc)
        ''Response.End()
        '' ''Response.ContentType = "application/pdf"
        '' ''Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf")
        '' ''Response.Cache.SetCacheability(HttpCacheability.NoCache)
        '' ''Dim sw As New StringWriter()
        '' ''Dim hw As New HtmlTextWriter(sw)

        '' ''divPrint.RenderControl(hw)
        '' ''Dim sr As New StringReader(sw.ToString())
        '' ''Dim pdfDoc As New Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
        '' ''Dim htmlparser As New HTMLWorker(pdfDoc)
        '' ''PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
        '' ''pdfDoc.Open()
        ' '' ''htmlparser.Parse(sr)
        '' ''pdfDoc.Close()
        '' ''Response.Write(pdfDoc)
        '' ''Response.End()

        'Response.ContentType = "application/pdf"
        'Response.AddHeader("content-disposition", "attachment;filename=UserDetails.pdf")
        'Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)
        ''Form.RenderControl(hw);
        'ExportDiv.Visible = False
        'divPrint.Style.Add("background-color", "#C2D69B")
        'divPrint.RenderControl(hw)

        'Dim sr As New StringReader(sw.ToString())
        'Dim pdfDoc As New Document(PageSize.A2, 7.0F, 7.0F, 7.0F, 0.0F)
        'Dim htmlparser As New HTMLWorker(pdfDoc)
        'PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
        'pdfDoc.Open()
        ''pdfDoc.NewPage();
        'pdfDoc.Add(Chunk.NEWLINE)

        'htmlparser.Parse(sr)
        'pdfDoc.Close()
        'Response.Write(pdfDoc)
        'Response.End()

        'Response.Clear() ' excel
        'Response.Buffer = True
        'Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
        'Response.Charset = ""
        'Response.ContentType = "application/vnd.ms-excel"
        'Dim sw As New StringWriter()
        'Dim hw As New HtmlTextWriter(sw)
        'ExportDiv.Visible = False
        'divPrint.RenderControl(hw)
        'Response.Output.Write(sw.ToString())
        'Response.Flush()
        'Response.End()


    End Sub

    Protected Sub GvRpt_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles GvRpt.DataBound

        If Session("RptMergeHeader") = Nothing Then Exit Sub

        If Session("RptMergeHeader").ToString().Length = 0 Then Exit Sub

        Dim HeaderMergeArr(,) As String
        HeaderMergeArr = Session("RptMergeHeader")
        Dim j As Integer
        Dim i As Integer = 0
        Dim row As GridViewRow '= New GridViewRow(0, 6, DataControlRowType.Header, DataControlRowState.Normal)
        Dim cell As New TableHeaderCell()
        For j = 0 To UBound(HeaderMergeArr) - 1
            If HeaderMergeArr(j, 0) <> Nothing Then

                If i = 0 Then
                    row = New GridViewRow(0, CInt(HeaderMergeArr(0, 3)), DataControlRowType.Header, DataControlRowState.Normal)
                    cell = New TableHeaderCell()
                    cell.Text = ""
                    cell.ColumnSpan = CInt(HeaderMergeArr(0, 2))
                    row.Controls.Add(cell)
                    HidMultiHeader.Value = 1
                End If
                ' row = New GridViewRow(0, j, DataControlRowType.Header, DataControlRowState.Normal)
                cell = New TableHeaderCell()
                cell.Text = HeaderMergeArr(j, 0)
                cell.ColumnSpan = CInt(HeaderMergeArr(0, 1))
                row.Controls.Add(cell)
                i = 1
            End If
        Next
        If i > 0 Then
            GvRpt.HeaderRow.Parent.Controls.AddAt(0, row)
        End If
        'Dim row As New GridViewRow(0, 6, DataControlRowType.Header, DataControlRowState.Normal)

        'Dim cell As New TableHeaderCell()
        'cell.Text = ""
        'cell.ColumnSpan = 4
        'row.Controls.Add(cell)

        'cell = New TableHeaderCell()
        'cell.Text = "RECEIPTS"
        'cell.ColumnSpan = 2
        'row.Controls.Add(cell)

        'cell = New TableHeaderCell()
        'cell.ColumnSpan = 2
        'cell.Text = "PAYMENTS"
        'row.Controls.Add(cell)

        ''row.BackColor = ColorTranslator.FromHtml("#3AC0F2")
        'GvRpt.HeaderRow.Parent.Controls.AddAt(0, row)
    End Sub




    Protected Sub TxtPageNoTo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtPageNoTo.TextChanged
        DtRpt = DirectCast(Session("ReportTbl"), DataTable)
        GenerateText()

    End Sub
End Class