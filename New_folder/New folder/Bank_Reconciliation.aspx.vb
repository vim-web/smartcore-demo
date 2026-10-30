Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports System.Drawing
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class Bank_Reconciliation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBl As New CommonBL
                Dim M_Dt As New DataTable
                M_CommonBl.Fill_Branch(DdlBranch)
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                'DdlBranch.Items.Add("ALL")
                M_Dt = M_CommonBl.Get_Bank(DdlBranch.SelectedValue)
                DdlBank.DataTextField = "Bank_Name"
                DdlBank.DataValueField = "Acc_Code"
                DdlBank.DataSource = M_Dt
                DdlBank.DataBind()
                'DdlBank.Items.Add("ALL")
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")
                Session("filestatus") = "Y"
                BlankGrid()
                RdoReconcile.SelectedIndex = "M"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable

            FileUpload()
            If Session("filestatus") = "N" Then
                Exit Sub
            End If

            M_Dt = M_CommonBl.Get_Reconciliation(DdlBranch.SelectedValue, DdlBank.SelectedValue, DrpReconStatus.SelectedValue, DtFrom.Text, DtTo.Text, Session("TransDate"), Session("UserID"))

            If M_Dt.Rows.Count > 0 Then
                GvReconcil.DataSource = M_Dt
                GvReconcil.DataBind()
            End If
            TranTotal()
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

    Protected Sub DdlBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlBank.SelectedIndexChanged

    End Sub
    'Protected Sub TxtReconcileDt_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    Try

    '        Dim RconDt As Date
    '        Dim Trandt As Date

    '        For Each row As GridViewRow In GvReconcil.Rows
    '            Dim ReconcileDt As TextBox = DirectCast(row.FindControl("TxtReconcileDt"), TextBox)
    '            RconDt = ReconcileDt.Text
    '            Trandt = row.Cells(11).Text
    '            If Trandt <> RconDt Then
    '                Dim Hex1 As String = "#84f991"
    '                row.BackColor = Drawing.ColorTranslator.FromHtml(Hex1)

    '            Else
    '                Dim hex As String = "#d9d9d9"
    '                row.BackColor = Drawing.ColorTranslator.FromHtml(hex)

    '            End If

    '        Next



    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub BlankGrid()
        Dim DtTrans As New DataTable("Table")
        Dim DtCol = New DataColumn("ID", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("BrCode", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Acc_Code", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Br_Name", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Acc_Name", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Credit", System.Type.GetType("System.Double"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Debit", System.Type.GetType("System.Double"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("InstrNo", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Tr_Date", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Recon_Status", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtTrans.Rows.Add()
        GvBankTran.DataSource = DtTrans
        GvBankTran.DataBind()

        Dim DtReconcile As New DataTable("Table")
        Dim DtRecCol = New DataColumn("Tran_ID", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Tran_No", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Br_Code", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Br_Name", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Acc_Code", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Acc_Name", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Vr_No", System.Type.GetType("System.Double"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Credit", System.Type.GetType("System.Double"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Debit", System.Type.GetType("System.Double"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Remarks", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Tr_Date", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtRecCol = New DataColumn("Recon_Status", System.Type.GetType("System.String"))
        DtReconcile.Columns.Add(DtRecCol)
        DtReconcile.Rows.Add()

        GvReconcil.DataSource = DtReconcile
        GvReconcil.DataBind()

    End Sub

    Protected Sub FileUpload()
        Dim UpLoadPath As String
        UpLoadPath = AppSettings("UPLOAD_PATH")
        Dim M_TranSw As New IO.StringWriter
        Dim M_commonBl As New CommonBL
        Dim M_dt As New DataTable
        Session("filestatus") = "Y"
        If FleBank.HasFile And DrpReconStatus.SelectedValue = "N" Then


            Try


                Dim FileName As String = Path.GetFileName(FleBank.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FleBank.PostedFile.FileName)
                'If System.IO.File.Exists(UpLoadPath + FileName) Then
                '    System.IO.File.Delete(UpLoadPath + FileName)
                'End If
                FleBank.PostedFile.SaveAs(UpLoadPath + FileName)
                Dim FilePath As String = Path.Combine(UpLoadPath, FleBank.FileName)


                ' Dim XlStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & UpLoadPath & FleFile.FileName & ";  Extended Properties=""Excel 12.0 Xml;HDR=YES"";"
                Dim XlStr As String = [String].Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;""", UpLoadPath & FleBank.FileName)
                Dim XLCon As New OleDb.OleDbConnection()
                XLCon.ConnectionString = XlStr
                Dim XLCmnd As New OleDbDataAdapter("select * from [Sheet1$]", XLCon)
                Dim DTXLRead As New DataTable("XLDATA")
                XLCmnd.Fill(DTXLRead)
                XLCon.Close()

                If DTXLRead.Columns.Count <> 6 Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid Columns")
                    Exit Sub
                Else
                    If DTXLRead.Columns(0).ColumnName <> "Br_Code" Then
                        ShowAlertMessage("Cannot upload the selected file,Invalid 1st Columns")
                        Exit Sub
                    End If
                    If DTXLRead.Columns(1).ColumnName <> "Acc_Code" Then
                        ShowAlertMessage("Cannot upload the selected file,Invalid 2nd Columns")
                        Exit Sub
                    End If
                    If DTXLRead.Columns(2).ColumnName <> "Credit" Then
                        ShowAlertMessage("Cannot upload the selected file,Invalid 3rd Columns")
                        Exit Sub
                    End If

                    If DTXLRead.Columns(3).ColumnName <> "Debit" Then
                        ShowAlertMessage("Cannot upload the selected file,Invalid 4th Columns")
                        Exit Sub
                    End If
                    If DTXLRead.Columns(4).ColumnName <> "InstrumentNo" Then
                        ShowAlertMessage("Cannot upload the selected file,Invalid 5th Columns")
                        Exit Sub
                    End If
                    If DTXLRead.Columns(5).ColumnName <> "Tr_Date" Then
                        ShowAlertMessage("Cannot upload the selected file,Invalid 6th Columns")
                        Exit Sub
                    End If
                  
                    DTXLRead.WriteXml(M_TranSw)

                    M_dt = M_commonBl.Get_BankEntry(M_TranSw.ToString(), DdlBranch.SelectedValue, DdlBank.SelectedValue, DrpReconStatus.SelectedValue, DtFrom.Text, DtTo.Text, Session("UserID"))

                    If M_dt.Rows.Count > 0 Then
                        GvBankTran.DataSource = M_dt
                        GvBankTran.DataBind()
                    End If

                End If


            Catch ex As Exception

            End Try
        ElseIf DrpReconStatus.SelectedValue = "Y" Then

            M_dt = M_commonBl.Get_BankEntry("", DdlBranch.SelectedValue, DdlBank.SelectedValue, DrpReconStatus.SelectedValue, DtFrom.Text, DtTo.Text, Session("UserID"))

            If M_dt.Rows.Count > 0 Then
                GvBankTran.DataSource = M_dt
                GvBankTran.DataBind()
            End If
        Else
            BlankGrid()
            ShowAlertMessage("INVALID FILE SELECTED / INVALID FORMAT")
            Session("filestatus") = "N"
            Exit Sub
        End If

        
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Try
            Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
            If page IsNot Nothing Then
                [error] = [error].Replace("'", "`")
                Dim M_CommonCls As New CommonClass

                ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
            End If

        Catch ex As Exception

        End Try
    End Sub

   

    Protected Sub RdoReconcile_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RdoReconcile.SelectedIndexChanged
        If RdoReconcile.SelectedValue = "A" Then
            GvBankTran.Columns(3).Visible = False
            GvReconcil.Columns(3).Visible = False

        Else
            GvBankTran.Columns(3).Visible = True
            GvReconcil.Columns(3).Visible = True
        End If
    End Sub

    Protected Sub CmdReconcile_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdReconcile.Click
        Try

            Dim DtTrans As New DataTable("Table")
            Dim DtCol = New DataColumn("ID", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
            DtCol = New DataColumn("Br_Code", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
            DtCol = New DataColumn("Acc_Code", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
            DtCol = New DataColumn("Br_Name", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
            DtCol = New DataColumn("Acc_Name", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
            DtCol = New DataColumn("Credit", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
            DtCol = New DataColumn("Debit", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
            DtCol = New DataColumn("InstrNo", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
            DtCol = New DataColumn("Tr_Date", System.Type.GetType("System.String"))
            DtTrans.Columns.Add(DtCol)
           
            'DtCol = New DataColumn("Recon_Status", System.Type.GetType("System.String"))
            'DtTrans.Columns.Add(DtCol)
            DtTrans.Rows.Add()
       

            Dim DtReconcile As New DataTable("Table")
            Dim DtRecCol = New DataColumn("Tran_ID", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Tran_No", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Br_Code", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Br_Name", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Acc_Code", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Acc_Name", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Vr_No", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Credit", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Debit", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Remarks", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            DtRecCol = New DataColumn("Tr_Date", System.Type.GetType("System.String"))
            DtReconcile.Columns.Add(DtRecCol)
            'DtRecCol = New DataColumn("Recon_Status", System.Type.GetType("System.String"))
            'DtReconcile.Columns.Add(DtRecCol)
            DtReconcile.Rows.Add()

            Dim M_Ds As DataSet
            Dim M_TranSw As New IO.StringWriter
            Dim Trandt As New DataTable
            Dim M_BankSw As New IO.StringWriter
            Dim Bankdt As New DataTable



            If RdoReconcile.SelectedValue = "M" Then

                For Each row As GridViewRow In GvBankTran.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(3).FindControl("chkSelect"), CheckBox)
                        If chkRow.Checked Then
                            Dim ID As String = row.Cells(0).Text
                            Dim Br_Code As String = row.Cells(1).Text
                            Dim Acc_Code As String = row.Cells(2).Text
                            Dim Br_Name As String = row.Cells(4).Text
                            Dim AccName As String = row.Cells(5).Text
                            Dim Credit As String = row.Cells(6).Text
                            Dim Debit As String = row.Cells(7).Text
                            Dim InstrNo As String = row.Cells(8).Text
                            Dim Tr_Date As String = row.Cells(9).Text

                            Try
                                Dim Expression As String = "ID=''"
                                If DtTrans.[Select](Expression).Count > 0 Then DtTrans.Rows.RemoveAt(DtTrans.Rows.IndexOf(DtTrans.[Select](Expression)(0)))
                            Catch ex As Exception
                                ShowAlertMessage(Err.Description)
                            End Try
                            

                            DtTrans.Rows.Add(ID, Br_Code, Acc_Code, Br_Name, AccName, Credit, Debit, InstrNo, Tr_Date)
                        End If
                    End If
                Next

                For Each row As GridViewRow In GvReconcil.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(3).FindControl("chkSelect"), CheckBox)
                        If chkRow.Checked Then
                            Dim Tran_ID As String = row.Cells(0).Text
                            Dim Tran_No As String = row.Cells(1).Text
                            Dim Br_Code As String = row.Cells(2).Text
                            Dim Br_Name As String = row.Cells(4).Text
                            Dim Acc_Code As String = row.Cells(5).Text
                            Dim Acc_Name As String = row.Cells(6).Text
                            Dim Vr_No As String = row.Cells(7).Text
                            Dim Credit As String = row.Cells(8).Text
                            Dim Debit As String = row.Cells(9).Text
                            Dim Remarks As String = row.Cells(10).Text
                            Dim Tr_Date As String = row.Cells(11).Text

                            Try
                                Dim Expression1 As String = "Tran_No=''"
                                If DtReconcile.[Select](Expression1).Count > 0 Then DtReconcile.Rows.RemoveAt(DtReconcile.Rows.IndexOf(DtReconcile.[Select](Expression1)(0)))
                            Catch ex As Exception
                                ShowAlertMessage(Err.Description)
                            End Try

                            DtReconcile.Rows.Add(Tran_ID, Tran_No, Br_Code, Br_Name, Acc_Code, Acc_Name, Vr_No, Credit, Debit, Remarks, Tr_Date)
                        End If
                    End If
                Next

                

               

            ElseIf RdoReconcile.SelectedValue = "A" Then

                For Each row As GridViewRow In GvBankTran.Rows


                    Dim ID As String = row.Cells(0).Text
                    Dim Br_Code As String = row.Cells(1).Text
                    Dim Acc_Code As String = row.Cells(2).Text
                    Dim Br_Name As String = row.Cells(4).Text
                    Dim AccName As String = row.Cells(5).Text
                    Dim Credit As String = row.Cells(6).Text
                    Dim Debit As String = row.Cells(7).Text
                    Dim InstrNo As String = row.Cells(8).Text
                    Dim Tr_Date As String = row.Cells(9).Text
                    DtTrans.Rows.Add(ID, Br_Code, Acc_Code, Br_Name, AccName, Credit, Debit, InstrNo, Tr_Date)
                    
                Next

                For Each row As GridViewRow In GvReconcil.Rows
                    
                    Dim Tran_ID As String = row.Cells(0).Text
                    Dim Tran_No As String = row.Cells(1).Text
                    Dim Br_Code As String = row.Cells(2).Text
                    Dim Br_Name As String = row.Cells(4).Text
                    Dim Acc_Code As String = row.Cells(5).Text
                    Dim Acc_Name As String = row.Cells(6).Text
                    Dim Vr_No As String = row.Cells(7).Text
                    Dim Credit As String = row.Cells(8).Text
                    Dim Debit As String = row.Cells(9).Text
                    Dim Remarks As String = row.Cells(10).Text
                    Dim Tr_Date As String = row.Cells(11).Text

                    DtReconcile.Rows.Add(Tran_ID, Tran_No, Br_Code, Br_Name, Acc_Code, Acc_Name, Vr_No, Credit, Debit, Remarks, Tr_Date)
                    
                Next

                Try
                    Dim Expression As String = "ID=''"
                    If DtTrans.[Select](Expression).Count > 0 Then DtTrans.Rows.RemoveAt(DtTrans.Rows.IndexOf(DtTrans.[Select](Expression)(0)))
                Catch ex As Exception
                    ShowAlertMessage(Err.Description)
                End Try

                Try
                    Dim Expression1 As String = "Tran_No=''"
                    If DtReconcile.[Select](Expression1).Count > 0 Then DtReconcile.Rows.RemoveAt(DtReconcile.Rows.IndexOf(DtReconcile.[Select](Expression1)(0)))
                Catch ex As Exception
                    ShowAlertMessage(Err.Description)
                End Try


                
            Else
                ShowAlertMessage("Invalid Reconcile Option...")
                RdoReconcile.Focus()
                Exit Sub
            End If
            Dim M_CommonBl As New CommonBL

            DtReconcile.WriteXml(M_TranSw)
            DtTrans.WriteXml(M_BankSw)
            M_Ds = M_CommonBl.Process_Reconciliation(M_BankSw.ToString(), M_TranSw.ToString(), DdlBranch.SelectedValue, DdlBank.SelectedValue, DrpReconStatus.SelectedValue, IIf(RdoReconcile.SelectedIndex = 0, "A", "M"), DtFrom.Text, DtTo.Text, Session("UserID"), Session("TransDate"))

            If M_Ds.Tables.Count > 0 Then
                Bankdt = M_Ds.Tables(0)
                Trandt = M_Ds.Tables(1)

                GvBankTran.DataSource = Bankdt
                GvBankTran.DataBind()

                GvReconcil.DataSource = Trandt
                GvReconcil.DataBind()
            End If
            TranTotal()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GvBankTran_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvBankTran.RowDataBound

        If e.Row.Cells(10).Text.Substring(0, 1) = "Y" Then
            'Dim Hex1 As String = "#84f991"
            Dim Hex1 As String = "#73cc7d"
            e.Row.BackColor = Drawing.ColorTranslator.FromHtml(Hex1)
            e.Row.Cells(3).Enabled = False
        End If
    End Sub

    Protected Sub GvReconcil_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvReconcil.RowDataBound
    
        If e.Row.Cells(12).Text.Substring(0, 1) = "Y" Then
            'Dim Hex1 As String = "#84f991" ''#73cc7d
            Dim Hex1 As String = "#73cc7d"
            e.Row.BackColor = Drawing.ColorTranslator.FromHtml(Hex1)
            e.Row.Cells(3).Enabled = False
        End If
    End Sub

    Protected Sub DrpReconStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpReconStatus.SelectedIndexChanged
       
        If DrpReconStatus.SelectedValue = "Y" Then
            FleBank.Visible = False
        Else
            FleBank.Visible = True
        End If
    End Sub
    Protected Sub TranTotal()
        Dim TotalCr As Double = 0
        Dim Recon_Cr As Double = 0
        Dim TotalDr As Double = 0
        Dim ReconDr As Double = 0

        Dim TotalCr1 As Double = 0
        Dim Recon_Cr1 As Double = 0
        Dim TotalDr1 As Double = 0
        Dim ReconDr1 As Double = 0


        For Each row As GridViewRow In GvBankTran.Rows
            TotalCr = TotalCr + Val(row.Cells(6).Text)
            TotalDr = TotalDr + Val(row.Cells(7).Text)

            If row.Cells(10).Text.Substring(0, 1) = "Y" Then
                Recon_Cr = Recon_Cr + Val(row.Cells(6).Text)
                ReconDr = ReconDr + Val(row.Cells(7).Text)
            End If
        Next

        For Each row As GridViewRow In GvReconcil.Rows
            TotalCr1 = TotalCr1 + Val(row.Cells(8).Text)
            TotalDr1 = TotalDr1 + Val(row.Cells(9).Text)

            If row.Cells(12).Text.Substring(0, 1) = "Y" Then
                Recon_Cr1 = Recon_Cr1 + Val(row.Cells(8).Text)
                ReconDr1 = ReconDr1 + Val(row.Cells(9).Text)
            End If
        Next

        LblBankCr1.Text = Format(Val(TotalCr - Recon_Cr), "#0.00")
        LblBankDr1.Text = Format(Val(TotalDr - ReconDr), "#0.00")

        LblTranCr1.Text = Format(Val(TotalCr1 - Recon_Cr1), "#0.00")
        LblTranDr1.Text = Format(Val(TotalDr1 - ReconDr1), "#0.00")
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class