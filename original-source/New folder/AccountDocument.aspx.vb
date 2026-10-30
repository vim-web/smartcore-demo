Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.IO
Partial Public Class AccountDocument
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Dim M_commCl As New CommonClass
    Dim M_datatable As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadDefaultValue()
        End If

    End Sub

    Public Sub LoadDefaultValue()
        M_CommonBL.Fill_Branch(ddl_branch)
        M_CommonBL.GetSchemes(ddl_Scheme, Session("TransDate"))
        M_CommonBL.Fill_Pickup(ddl_DocType, 92, 0)
        Griddatabind()
        txt_DocDate.Text = Session("TransDate")
    End Sub

    Protected Sub txt_AccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_AccNo.TextChanged
        Try
            Dim AccID
            GetAccountTranDtls()
            'Dim M_datatable As New DataTable
           
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetAccountTranDtls()
        Try
            'TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
            Dim Dt_AcDtls As New DataTable
            Dim M_dtable As New DataTable
            Dim M_AccNo As String = txt_AccNo.Text.Trim()
            If M_AccNo.Length = 4 Then
                M_AccNo = Right("000" + ddl_branch.SelectedValue, 3) + Right("000" + ddl_Scheme.SelectedValue, 3) + Right("0000000" + txt_AccNo.Text, 7)
            End If
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(ddl_branch.SelectedValue.Trim(), ddl_Scheme.SelectedValue.Trim(), M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                hid_AccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                txt_name.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                txt_address.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                'If M_AccNo.Length >= 8 Then
                '    CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
                'End If
                txt_AccNo.Text = M_AccNo
                M_dtable = M_CommonBL.GetAccDocument(hid_AccID.Value.Trim())
                If M_dtable.Rows.Count > 0 Then
                    hid_status.Value = "NEWOLD"
                    Session("grd_docData") = M_dtable
                    Grid_DcumentDetails.DataSource = M_dtable
                    Grid_DcumentDetails.DataBind()
                Else
                    Griddatabind()
                End If

            Else
                ShowAlertMessage("INVALID ACCOUNT NO")
                txt_AccNo.Text = ""
            End If


        Catch ex As Exception

        End Try
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

    Protected Sub but_add_Click(ByVal sender As Object, ByVal e As EventArgs) Handles but_add.Click
        Try

            Dim docDate As String = txt_DocDate.Text.Trim()
            Dim DocDetails As String = txt_DocDetails.Text.Trim()
            Dim M_datatable As New DataTable
            M_datatable = Session("grd_docData")
            Dim status As String = hid_status.Value.Trim()
            If status = "NEW" Then
                M_datatable.Rows(0).Delete()
            End If
            Dim dr As DataRow
            dr = M_datatable.NewRow()
            dr(0) = ddl_DocType.SelectedValue.Trim()
            dr(1) = docDate
            dr(2) = DocDetails
            dr(3) = "Active"
            dr(4) = "-"
            dr(5) = "-"
            M_datatable.Rows.Add(dr)
            hid_status.Value = "OLD"
            Session("grd_docData") = M_datatable
            Grid_DcumentDetails.DataSource = M_datatable
            Grid_DcumentDetails.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Public Function ConvertDatatableToXML(ByVal dt As DataTable) As String
        Try
            Dim str As New MemoryStream()
            dt.WriteXml(str, True)
            str.Seek(0, SeekOrigin.Begin)
            Dim sr As New StreamReader(str)
            Dim xmlstr As String
            xmlstr = sr.ReadToEnd()
            Return (xmlstr)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Function

    Private Sub GetAccDocmentDetails()

    End Sub

    Private Sub Griddatabind()
        Try

        
            M_datatable = New DataTable

            M_datatable.Columns.Add("Doc_TypeID")
            M_datatable.Columns.Add("Tr_Date")
            M_datatable.Columns.Add("Doc_Dtls")
            M_datatable.Columns.Add("Doc_Status")
            M_datatable.Columns.Add("Doc_RtnDate")
            M_datatable.Columns.Add("Doc_RtnDtls")
            Dim dr As DataRow
            dr = M_datatable.NewRow()
            dr(0) = "-"
            dr(1) = "-"
            dr(2) = "-"
            dr(3) = "-"
            dr(4) = "-"
            dr(5) = "-"
            M_datatable.Rows.Add(dr)
            Session("grd_docData") = M_datatable
            hid_status.Value = "NEW"
            Grid_DcumentDetails.DataSource = M_datatable
            Grid_DcumentDetails.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Grid_DcumentDetails_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Grid_DcumentDetails.RowDeleting
        Try
            M_datatable = New DataTable
            M_datatable.Columns.Add("Doc_TypeID")
            M_datatable.Columns.Add("Tr_Date")
            M_datatable.Columns.Add("Doc_Dtls")
            M_datatable.Columns.Add("Doc_Status")
            M_datatable.Columns.Add("Doc_RtnDate")
            M_datatable.Columns.Add("Doc_RtnDtls")
            Dim rowIndex As Integer = e.RowIndex
            Dim dr_ As DataRow
            For index As Integer = 0 To Grid_DcumentDetails.Rows.Count - 1
                If rowIndex <> index Then
                    dr_ = M_datatable.NewRow()
                    dr_(0) = Grid_DcumentDetails.Rows(index).Cells(0).Text
                    dr_(1) = Grid_DcumentDetails.Rows(index).Cells(1).Text
                    dr_(2) = Grid_DcumentDetails.Rows(index).Cells(2).Text
                    dr_(3) = Grid_DcumentDetails.Rows(index).Cells(3).Text
                    dr_(4) = Grid_DcumentDetails.Rows(index).Cells(4).Text
                    dr_(5) = Grid_DcumentDetails.Rows(index).Cells(5).Text

                    M_datatable.Rows.Add(dr_)
                End If
            Next
            If M_datatable.Rows.Count > 0 Then
                Session("grd_docData") = M_datatable
                hid_status.Value = "OLD"
                Grid_DcumentDetails.DataSource = M_datatable
                Grid_DcumentDetails.DataBind()
            Else
                Griddatabind()
            End If


        Catch ex As Exception

        End Try


    End Sub

    Protected Sub Grid_DcumentDetails_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Grid_DcumentDetails.SelectedIndexChanged

    End Sub

    Protected Sub but_save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles but_save.Click
        Try
            Dim M_da As New DataTable

            M_da.Columns.Add("Doc_TypeID")
            M_da.Columns.Add("Tr_Date")
            M_da.Columns.Add("Doc_Dtls")
            M_da.Columns.Add("Doc_Status")
            M_da.Columns.Add("Doc_RtnDate")
            M_da.Columns.Add("Doc_RtnDtls")
            Dim dr As DataRow
            For index As Integer = 0 To Grid_DcumentDetails.Rows.Count - 1
                dr = M_da.NewRow()
                Dim StrDate As String = Grid_DcumentDetails.Rows(index).Cells(4).Text.Trim()
                Dim StrStatus As String = Grid_DcumentDetails.Rows(index).Cells(3).Text.Trim()
                If StrDate = "-" Then
                    StrDate = "01/01/1900"
                Else
                    StrDate = M_commCl.DbDate(StrDate, True)
                End If

                If StrStatus = "Active" Then
                    StrStatus = "A"
                End If

                If StrStatus = "Return" Then
                    StrStatus = "R"
                End If

                dr(0) = Grid_DcumentDetails.Rows(index).Cells(0).Text.Trim()
                dr(1) = M_commCl.DbDate(Grid_DcumentDetails.Rows(index).Cells(1).Text.Trim(), True)
                dr(2) = Grid_DcumentDetails.Rows(index).Cells(2).Text()
                dr(3) = StrStatus
                dr(4) = StrDate
                dr(5) = Grid_DcumentDetails.Rows(index).Cells(5).Text()
                M_da.Rows.Add(dr)
            Next

            ' M_datatable = DirectCast(Session("grd_docData"), DataTable)
            M_da.TableName = "grd_docData"
            Dim XmlStr As String = ConvertDatatableToXML(M_da)
            Dim M_valid As Boolean = M_CommonBL.InsertAccDocument(hid_AccID.Value.Trim(), XmlStr)

            If M_valid = True Then
                ShowAlertMessage("Successful Saved")
            Else
                ShowAlertMessage("Server Error. Try Again.")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Grid_DcumentDetails_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles Grid_DcumentDetails.DataBound

    End Sub

    Protected Sub Grid_DcumentDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid_DcumentDetails.RowDataBound
        e.Row.Cells(0).Visible = False

        If e.Row.Cells(4).Text = "01/01/1900" Then e.Row.Cells(4).Text = ""
        If e.Row.Cells(3).Text = "A" Then
            e.Row.Cells(3).Text = "Active"

        End If

        If e.Row.Cells(3).Text = "R" Then e.Row.Cells(3).Text = "Return"
    End Sub
End Class