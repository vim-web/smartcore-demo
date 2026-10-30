Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports System.Drawing
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data.SqlClient
Public Class BulkUploadPayrollProcess
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1126
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" And Session("LoginUserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
            End If
            btn_save.Enabled = DgvCsvFiledata.Rows.Count > 0
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub imgbtn_Show_Click(sender As Object, e As ImageClickEventArgs) Handles Btn_Show.Click
        Try
            Dim ObjCommonBL As New CommonBL
            Dim DtShowData As DataTable
            DtShowData = ObjCommonBL.Save_PayRoll(0, Session("UserID"))
            DgvCsvFiledata.DataSource = DtShowData
            DgvCsvFiledata.DataBind()
            btn_save.Enabled = DgvCsvFiledata.Rows.Count > 0
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub btn_save_Click(sender As Object, e As ImageClickEventArgs) Handles btn_save.Click
        Try
            If DgvCsvFiledata.Rows.Count = 0 Then Exit Sub
            Dim ObjCommonBL As New CommonBL
            Dim DtSave As DataTable
            DtSave = ObjCommonBL.Save_PayRoll(1, Session("UserID"))
            DgvCsvFiledata.DataSource = Nothing
            DgvCsvFiledata.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub BtnClose_Click(sender As Object, e As ImageClickEventArgs) Handles BtnClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub CmdUpload_Click(sender As Object, e As ImageClickEventArgs) Handles CmdUpload.Click

        FileUploadNew()
    End Sub
    Protected Sub FileUpload()
        Dim UpLoadPath As String
        UpLoadPath = AppSettings("UPLOAD_PATH")
        Dim M_TranSw As New IO.StringWriter
        Dim M_commonBl As New CommonBL
        Dim M_dt As New DataTable
        Try


            Dim FileName As String = Path.GetFileName(FleBank.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FleBank.PostedFile.FileName)
            'If System.IO.File.Exists(UpLoadPath + FileName) Then
            '    System.IO.File.Delete(UpLoadPath + FileName)
            'End If
            FleBank.PostedFile.SaveAs(UpLoadPath + FileName)
            Dim FilePath As String = Path.Combine(UpLoadPath, FleBank.FileName)


            'Dim XlStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & UpLoadPath & FleBank.FileName & ";  Extended Properties=""Excel 12.0 Xml;HDR=YES"";"
            Dim XlStr As String = [String].Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;""", UpLoadPath & FleBank.FileName)
            Dim M_ExcelType As String = ".xls"

            If M_ExcelType = Extension Then
                XlStr = [String].Format("Provider=Microsoft.ACE.OLEDB.4.0;Data Source={0};Extended Properties=""Excel 8.0 Xml;HDR=YES;IMEX=1;""", UpLoadPath & FleBank.FileName)
            Else
                XlStr = [String].Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;""", UpLoadPath & FleBank.FileName)
            End If

            Dim XLCon As New OleDb.OleDbConnection()
            XLCon.ConnectionString = XlStr
            Dim XLCmnd As New OleDbDataAdapter("select * from [Sheet1$]", XLCon)
            Dim DTXLRead As New DataTable("XLDATA")

            DTXLRead.Columns.Add("Slno")
            DTXLRead.Columns.Add("Date")
            DTXLRead.Columns.Add("Br_Code")
            DTXLRead.Columns.Add("Sch_Code")
            DTXLRead.Columns.Add("Gl_Code")
            DTXLRead.Columns.Add("AcNo")
            DTXLRead.Columns.Add("TransType")
            DTXLRead.Columns.Add("Amount")
            DTXLRead.Columns.Add("Remarks")

            XLCmnd.Fill(DTXLRead)
            XLCon.Close()

            If DTXLRead.Columns.Count <> 6 Then
                ShowAlertMessage("Cannot upload the selected file,Invalid Columns")
                Exit Sub
            Else
                If DTXLRead.Columns(0).ColumnName <> "Slno" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 1st Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(1).ColumnName <> "Date" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 2nd Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(2).ColumnName <> "Br_Code" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 3rd Columns")
                    Exit Sub
                End If

                If DTXLRead.Columns(3).ColumnName <> "Sch_Code" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 4th Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(4).ColumnName <> "Gl_Code" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 5th Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(5).ColumnName <> "AcNo" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 6th Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(6).ColumnName <> "TransType" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 7th Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(7).ColumnName <> "Amount" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 8th Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(8).ColumnName <> "Remarks" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 9th Columns")
                    Exit Sub
                End If

                DTXLRead.WriteXml(M_TranSw)

                'M_dt = M_commonBl.Get_BankEntry(M_TranSw.ToString(), DdlBranch.SelectedValue, DdlBank.SelectedValue, DrpReconStatus.SelectedValue, DtFrom.Text, DtTo.Text, Session("UserID"))




                BlankGrid()
                ShowAlertMessage("INVALID FILE SELECTED / INVALID FORMAT")
                Session("filestatus") = "N"
                Exit Sub
            End If
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub FileUploadNew()
        Dim M_commonBl As New CommonBL
        Dim dt As DataTable = New DataTable()

        Dim M_ExcelType As String = ".xls"

        If FleBank.FileName = "" Then
            ShowAlertMessage("File not selected...!!")
            Exit Sub
        End If

        Dim FolderPath As String
        Dim FileName As String
        Dim Extension As String

        FolderPath = AppSettings("UPLOAD_PATH")


        Extension = Path.GetExtension(FleBank.PostedFile.FileName).ToUpper
        Select Case Extension
            Case ".XLSX"
                M_ExcelType = ".xlsx"
                FileName = Path.GetFileName(FleBank.PostedFile.FileName)
            Case ".XLS"
                M_ExcelType = ".xls"
                FileName = Path.GetFileName(FleBank.PostedFile.FileName)
            Case Else
                ShowAlertMessage("Invalid File Format..!! (The file format should be .xls Or .xlsx)")
                Exit Sub
        End Select

        Dim SaveAsFilePath As String = Path.Combine(FolderPath, FileName)
        FleBank.SaveAs(SaveAsFilePath)


        If Len(SaveAsFilePath) > 0 Then

            Dim oleDbConnStr As String
            If M_ExcelType = ".xls" Then
                oleDbConnStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" & SaveAsFilePath & "';Extended Properties=Excel 8.0;"
            Else
                oleDbConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & SaveAsFilePath & "';Extended Properties='Excel 12.0;HDR=YES'"
            End If

            Dim M_TranSw As New IO.StringWriter
            Using oleDbConn As OleDbConnection = New OleDbConnection(oleDbConnStr)
                oleDbConn.Open()
                Dim dt2 As DataTable = New DataTable()
                Dim oleDbCmd As New OleDbCommand("SELECT * FROM [Sheet1$]", oleDbConn)
     
                Dim oleDbDa As New OleDbDataAdapter(oleDbCmd)
                oleDbDa.Fill(dt)
                
                dt.TableName = "Table"
                dt.WriteXml(M_TranSw)

                Dim ObjCommonBL As New CommonBL
                Dim DtSave As DataTable
                DtSave = ObjCommonBL.ShowInsertPayRoll(0, Session("UserID"), M_TranSw.ToString())
                DgvCsvFiledata.DataSource = Nothing
                DgvCsvFiledata.DataBind()
                oleDbConn.Close()
            End Using
        End If
    End Sub
    Private Sub BlankGrid()
        Dim DtTrans As New DataTable("Table")
        Dim DtCol = New DataColumn("SL NO", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Date", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("BrCode", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Scheme Code", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Acccode", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Accno", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("TransType", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Amount", System.Type.GetType("System.Double"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Narration", System.Type.GetType("System.String"))

        DtTrans.Rows.Add()
        DgvCsvFiledata.DataSource = DtTrans
        DgvCsvFiledata.DataBind()



    End Sub
End Class