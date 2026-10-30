Imports SMARTCORE_DAL
Imports System.Configuration.ConfigurationManager
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportSource
Imports System.IO
Imports System.Net

Imports System.Drawing.Printing




Partial Public Class RptView
    Inherits System.Web.UI.Page
    Shared ReportDoc As New ReportDocument
    Dim FormID As Integer = 1125
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Label1.Text = ""
                CRV.BestFitPage = False
                CRV.BestFitPage = False
                CRV.Width = New Unit("1200px")
                CRV.Height = New Unit("1200px")
                hdnOutput.Value = Request.QueryString("Outputto")

                Ddl_Export.Items.Add("Rich Text (RTF)")
                Ddl_Export.Items.Add("Portable Document (PDF)")
                Ddl_Export.Items.Add("MS Word (DOC)")
                Ddl_Export.Items.Add("MS Excel (XLS)")
                Ddl_Export.SelectedIndex = 0


                For Each strLocalPrinter As String In PrinterSettings.InstalledPrinters
                    'List Printers on a Combobox 

                    Ddl_Printer.Items.Add(strLocalPrinter)
                Next

                'Ddl_Printer.SelectedIndex = 1

              



               

            End If
        Catch ex As Exception
            Label1.Text = Label1.Text + "|" + ex.Message
        End Try



    End Sub
    Private Sub Load_Report()
        Dim M_Dt As DataTable = New DataTable()
        Dim M_Qry As String
        Dim M_Heading As String = ""
        Dim M_HeadTitle As String = ""
        Dim M_RptPath As String = ""
        Dim I As Integer
        Dim J As Integer
        Dim RptFormula As String
        Dim SubRptQry(6) As String
        Dim SubRptNames As String
        Dim StrFlf(100) As String
        Dim ArrSub(100) As String
        Dim M_ReportsBL As New SMARTCORE_DAL.Connection
        Try
            Try
                Dim fname As String = DateTime.Now.ToString("yyyyMMddHHmmss").ToString() & ".pdf"


                Dim SubRpttDoc As New ReportDocument
                Dim exprtopt As ExportOptions = Nothing
                Dim destiopt As New DiskFileDestinationOptions()
                destiopt.DiskFileName = Server.MapPath(fname)


                
                M_RptPath = Session("RptPath")
                ReportDoc.Load(M_RptPath)

                M_Heading = "" 'Session("RptHeading")
                M_Qry = Session("RptQry")

                M_ReportsBL.ExecuteDataTable(M_Dt, M_Qry, False)

                ReportDoc.SetDataSource(M_Dt)

                '------------------- SUB REPORT ---------------------
                If Session("SubRptNames") <> Nothing Then
                    SubRptNames = Session("SubRptNames")
                Else
                    SubRptNames = ""
                End If
                If SubRptNames <> "" Then
                    SubRptQry = Session("SubRptQry")
                    ArrSub = Split(SubRptNames, "|")

                    For I = 0 To UBound(ArrSub)
                        SubRpttDoc = ReportDoc.OpenSubreport(ArrSub(I))

                        M_ReportsBL.ExecuteDataTable(M_Dt, SubRptQry(I), False)
                        SubRpttDoc.SetDataSource(M_Dt)
                    Next
                End If

                '------------------- SUB REPORT ---------------------

                RptFormula = Session("RptFormula")
                StrFlf = Split(RptFormula, "|")

                With ReportDoc.DataDefinition.FormulaFields
                    For I = 0 To StrFlf.Length - 1 Step 2
                        For J = 0 To .Count - 1
                            If .Item(J).Name = StrFlf(I) Then
                                ReportDoc.DataDefinition.FormulaFields(StrFlf(I)).Text = "'" + Replace(StrFlf(I + 1), "'", "") + "'"
                                Exit For
                            End If
                        Next
                    Next
                End With

                CRV.EnableDatabaseLogonPrompt = True

         
                Dim a As String = AppSettings("ServerName")



                If Val(hdnOutput.Value) = 2 Then
                    'ReportDoc.SetDatabaseLogon("sanoj", "Safe123.", "SANOJ\SQL2008", "WEBMASTER")

                    CRV.ReportSource = ReportDoc
                    CRV.Zoom(100)
                Else

                    ' ReportDoc.PrintOptions.PrinterName = Ddl_Printer.SelectedItem.Text
                    'ReportDoc.PrintOptions.PaperSize = PaperSize.DefaultPaperSize
                    ReportDoc.PrintToPrinter(1, True, 1, 3)
                End If



            Catch ex As Exception
                Label1.Text = M_RptPath + "|" + ex.Message
            End Try

            Exit Sub

        Catch ex As Exception
            Label1.Text = M_RptPath + "|" + ex.Message
        End Try
    End Sub

   

    Protected Sub Btn_Export_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Btn_Export.Click


        Load_Report()


        'Toexport

        Dim oStream As New MemoryStream ' // using System.IO

        'this contains the value of the selected export format.
        Select Case Ddl_Export.SelectedItem.Text

            Case "Rich Text (RTF)"
                '-----------------------------------------------------------

                oStream = ReportDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows)
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/rtf"
                '------------------------------------------------------------

                '------------------------------------------------------------
            Case "Portable Document (PDF)"

                oStream = ReportDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/pdf"
                '--------------------------------------------------------------

                '--------------------------------------------------------------
            Case "MS Word (DOC)"

                oStream = ReportDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows)
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/doc"
                '---------------------------------------------------------------

                '---------------------------------------------------------------
            Case "MS Excel (XLS)"

                oStream = ReportDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel)
                Response.Clear()
                Response.Buffer = True
                Response.ContentType = "application/vnd.ms-excel"
                '---------------------------------------------------------------
        End Select 'export format
        Try
            Response.BinaryWrite(oStream.ToArray())
            Response.End()
        Catch err As Exception
            Response.Write("< BR >")
            Response.Write(err.Message.ToString)
        End Try


    End Sub

End Class