Imports SMARTCORE_Common.CommonClass
Imports System.Configuration.ConfigurationManager
Imports System.IO
Imports System.Data.OleDb
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Drawing

'****************PROCEDURE***********
'InsertUpdate_LSA_FORBulkTransaction
'uSP_XLTRANSACTIONS
'uSP_GETLASFORAUTH
'****************TABLE***************
'TBL_LASTRANBULKDETAILS


Public Class TransFrmXL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then

            Btn_Save.Enabled = False
            ViewState("IsAuth") = "No"
            BlankGrid()
        End If

        If Len(Request.QueryString("TRANID")) > 0 Then
            GetAuthoriseDtls(Request.QueryString("TRANID"))
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

    Private Sub BlankGrid()
        Dim DtTrans As New DataTable("Table")
        Dim DtCol = New DataColumn("SLNO", System.Type.GetType("System.Int32"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Account No", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Cust Id", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Customer Name", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Branch", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Scheme Name", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Amount", System.Type.GetType("System.Double"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("Balance", System.Type.GetType("System.Double"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("ACCID", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("SCHCODE", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("BRCODE", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("ACCCODE", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("ACCNAME", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("SEQNO", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("BANKACCOUNTCODE", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("ID", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)
        DtCol = New DataColumn("NARRATION", System.Type.GetType("System.String"))
        DtTrans.Columns.Add(DtCol)

        DtTrans.Rows.Add()
        GVTransDetails.DataSource = DtTrans
        GVTransDetails.DataBind()
    End Sub
    Private Sub GetAuthoriseDtls(ByVal TransID As String)
        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataTable
        Dim M_TranTbl As DataTable
        Dim M_SecTbl As DataTable

        ' M_Dataset = M_CommonBL.GetAuthoriseDtls("TRAN", TransID)

        Dim L_LASUploadScreenBL As New LASUploadScreenBL
        M_Dataset = L_LASUploadScreenBL.AUTH_TransactionBULKLAS(TransID)
        GVTransDetails.DataSource = M_Dataset
        GVTransDetails.DataBind()
        CmdUpload.Visible = False
        ViewState("IsAuth") = "Yes"
        Btn_Save.Enabled = True
        FleFile.Visible = False
        LblFile.Visible = False
    End Sub

    Protected Sub CmdUpload_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdUpload.Click
        Dim UpLoadPath As String
        UpLoadPath = AppSettings("UPLOAD_PATH")
        If FleFile.HasFile Then

            Dim FileName As String = Path.GetFileName(FleFile.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FleFile.PostedFile.FileName)
            If System.IO.File.Exists(UpLoadPath + FileName) Then
                System.IO.File.Delete(UpLoadPath + FileName)
            End If
            FleFile.PostedFile.SaveAs(UpLoadPath + FileName)
            Dim FilePath As String = Path.Combine(UpLoadPath, FleFile.FileName)


            ' Dim XlStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & UpLoadPath & FleFile.FileName & ";  Extended Properties=""Excel 12.0 Xml;HDR=YES"";"
            Dim XlStr As String = [String].Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;IMEX=1;""", UpLoadPath & FleFile.FileName)
            Dim XLCon As New OleDb.OleDbConnection()
            XLCon.ConnectionString = XlStr
            Dim XLCmnd As New OleDbDataAdapter("select * from [Sheet1$]", XLCon)
            Dim DTXLRead As New DataTable("XLDATA")
            XLCmnd.Fill(DTXLRead)
            XLCon.Close()

            If DTXLRead.Columns.Count <> 4 Then
                ShowAlertMessage("Cannot upload the selected file,Invalid Columns")
                Exit Sub
            Else
                If DTXLRead.Columns(0).ColumnName <> "ACCOUNTNO" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 1st Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(1).ColumnName <> "PRINCIPLEAMOUNT" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 2nd Columns")
                    Exit Sub
                End If
                If DTXLRead.Columns(2).ColumnName <> "BANKACCOUNTCODE" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 3rd Columns")
                    Exit Sub
                End If

                If DTXLRead.Columns(3).ColumnName <> "NARRATION" Then
                    ShowAlertMessage("Cannot upload the selected file,Invalid 4rd Columns")
                    Exit Sub
                End If


            End If


            If DTXLRead.Rows.Count > 0 Then

                Dim SW As New IO.StringWriter
                DTXLRead.WriteXml(SW)
                Dim DtResult As New DataTable("TRANSACTION")
                Dim TrDate As String
                TrDate = Convert.ToDateTime(Session("TransDate")).ToString("MM/dd/yyyy")
                Dim L_LASUploadScreenBL As New LASUploadScreenBL
                DtResult = L_LASUploadScreenBL.View_TransactionBULKLAS(SW.ToString(), TrDate)
                GVTransDetails.DataSource = DtResult
                GVTransDetails.DataBind()
                Dim drROW() As DataRow
                drROW = DtResult.Select("CUSTID IS NULL")
                If drROW.Length > 0 Then
                    ShowAlertMessage("Invalid Upload details,Please correct the file before upload")
                    Btn_Save.Enabled = False
                    Exit Sub
                Else
                    If GVTransDetails.Rows.Count > 0 Then
                        ShowAlertMessage("Uploaded Sucessfully,Please save to make transaction")
                        Btn_Save.Enabled = True
                        Exit Sub
                    End If
                End If

            Else

            End If


        Else



        End If
    End Sub

    Protected Sub Btn_Save_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Btn_Save.Click
        Try
            If ViewState("IsAuth") = "No" Then


                Dim DtTrans As New DataTable("Table")
                Dim DtCol = New DataColumn("AccNo", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("AccCode", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("AccHead", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("RctCash", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("RctTransfer", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("PayCash", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("PayTransfer", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("Seq_No", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("Narration", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("SCHCODE", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("BANKACCOUNTCODE", System.Type.GetType("System.String"))
                DtTrans.Columns.Add(DtCol)
                DtCol = New DataColumn("ID", System.Type.GetType("System.Int32"))
                DtTrans.Columns.Add(DtCol)


                Dim SCHCODE As String
                If GVTransDetails.Rows.Count > 0 Then
                    SCHCODE = GVTransDetails.Rows(0).Cells(9).Text
                End If

                Dim i As Integer
                For i = 0 To GVTransDetails.Rows.Count - 1
                    DtTrans.Rows.Add(GVTransDetails.Rows(i).Cells(1).Text, GVTransDetails.Rows(i).Cells(11).Text, GVTransDetails.Rows(i).Cells(12).Text, "0", GVTransDetails.Rows(i).Cells(6).Text, "0", "0", GVTransDetails.Rows(i).Cells(13).Text, GVTransDetails.Rows(i).Cells(15).Text, GVTransDetails.Rows(i).Cells(9).Text, GVTransDetails.Rows(i).Cells(14).Text, GVTransDetails.Rows(i).Cells(16).Text)
                Next


                Dim M_TranSw As New IO.StringWriter
                DtTrans.WriteXml(M_TranSw)
                Dim M_Dt As DataTable
                Dim L_CommanBL As New CommonBL
                M_Dt = L_CommanBL.INSERTLSABulkTran(Session("DefaultBrCode"), Session("TransDate"), Session("DefaultBrCode"), DtTrans.Rows(0)("Narration").ToString(), Session("LoginID"), M_TranSw.ToString, "", , "", SCHCODE)

                If M_Dt.Rows.Count > 0 Then
                    ShowAlertMessage("Transaction Imported,Pending for Authorisation")
                    Btn_Save.Enabled = False
                    BlankGrid()
                    Exit Sub

                End If
            Else
                Dim M_CommonBL As New CommonBL
                If M_CommonBL.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                    ShowAlertMessage("Authotisation Failed")
                    Response.Redirect("~/ManagerPassing.aspx")
                    Exit Sub
                End If
                M_CommonBL = Nothing
                Response.Redirect("~/ManagerPassing.aspx?Passing=y")
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GVTransDetails_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GVTransDetails.RowDataBound
        e.Row.Cells(8).CssClass = "HideCol"
        e.Row.Cells(9).CssClass = "HideCol"
        e.Row.Cells(10).CssClass = "HideCol"
        e.Row.Cells(11).CssClass = "HideCol"
        e.Row.Cells(12).CssClass = "HideCol"
        e.Row.Cells(13).CssClass = "HideCol"
        e.Row.Cells(14).CssClass = "HideCol"
        e.Row.Cells(15).CssClass = "HideCol"
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(2).Text = "" Or e.Row.Cells(2).Text = "&nbsp;" Then
                e.Row.BackColor = Drawing.Color.Red
                e.Row.ForeColor = Drawing.Color.White
                Btn_Save.Enabled = False

                'For Each cell As TableCell In e.Row.Cells
                '    cell.BackColor = Color.Green
                'Next

                If e.Row.Cells(1).Text <> "&nbsp;" Then
                    e.Row.Cells(3).Text = "INVALID LOAN NUMBER"
                End If

            End If
        End If
    End Sub

 

    Protected Sub But_Clear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles But_Clear.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class