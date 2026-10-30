Imports SMARTCORE_BL
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports iTextSharp.text.html.simpleparser
Partial Public Class PrnViewNew
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
                            'Colwidth = HeaderArr(j - 1, 2)
                            ColAlign = HeaderArr(j - 1, 1)

                            e.Row.Cells(j).Text = HeadStr
                            '  e.Row.Cells(j).Width = Colwidth

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
                            'Colwidth = HeaderArr(j - 1, 2)
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
End Class