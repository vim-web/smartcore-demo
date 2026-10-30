Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.IO
Partial Public Class AccDocReturn
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
        Griddatabind()
        txt_DocDate.Text = Session("TransDate")
    End Sub

    Private Sub GetAccountTranDtls()
        Try
            'TxtAccNo.Text = Right("000" + CmbBranch.SelectedValue, 3) + Right("000" + CmbScheme.SelectedValue, 3) + Right("0000000" + TxtShortCode.Text, 7)
            Dim Dt_AcDtls As New DataTable
            Dim M_AccNo As String = txt_AccNo.Text.Trim()

            If M_AccNo.Length = 4 Then
                M_AccNo = Right("000" + ddl_branch.SelectedValue, 3) + Right("000" + ddl_Scheme.SelectedValue, 3) + Right("0000000" + txt_AccNo.Text, 7)
            End If
            Dt_AcDtls = M_CommonBL.GetAccountTranDtls(ddl_branch.SelectedValue.Trim(), ddl_Scheme.SelectedValue.Trim(), M_AccNo, Session("TransDate"))
            If Dt_AcDtls.Rows.Count > 0 Then
                hid_AccID.Value = Dt_AcDtls.Rows(0)("Acc_ID").ToString()
                txt_name.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
                txt_address.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
                M_datatable = M_CommonBL.GetAccDoumentDelt(hid_AccID.Value.Trim())
                If M_datatable.Rows.Count > 0 Then
                    Session("grd_docData") = M_datatable

                    Grid_DcumentDetails.DataSource = M_datatable
                    Grid_DcumentDetails.DataBind()
                Else
                    Griddatabind()
                End If
                txt_AccNo.Text = M_AccNo
            Else
                ShowAlertMessage("INVALID ACCOUNT NO")
                txt_AccNo.Text = ""
            End If


        Catch ex As Exception
            ShowAlertMessage("Sorry !. Server Error.Try Again.")
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

    Private Sub Griddatabind()
        Try
            M_datatable = New DataTable
            M_datatable.Columns.Add("Doc_ID")
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
            ShowAlertMessage("Sorry !. Server Error.Try Again.")
        End Try
    End Sub


    Protected Sub txt_AccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txt_AccNo.TextChanged
        Try

            GetAccountTranDtls()
            'Dim M_datatable As New DataTable
          

        Catch ex As Exception
            ShowAlertMessage("Sorry !. Server Error.Try Again.")
        End Try
    End Sub

    Protected Sub but_close_Click(ByVal sender As Object, ByVal e As EventArgs) Handles but_close.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub but_save_Click(ByVal sender As Object, ByVal e As EventArgs) Handles but_save.Click
        Try
            Dim DocID As String = ""
            For index As Integer = 0 To Grid_DcumentDetails.Rows.Count - 1
                Dim StrCheckValid = Grid_DcumentDetails.Rows(index).Cells(4).Text.Trim()
                If StrCheckValid = "Active" Then
                    Dim M_chech As CheckBox = DirectCast(Grid_DcumentDetails.Rows(index).Cells(0).FindControl("Che_Status"), CheckBox)
                    If M_chech.Checked = True Then
                        DocID = DocID + Grid_DcumentDetails.Rows(index).Cells(1).Text + ","
                    End If
                End If
            Next
            DocID = Mid(DocID, 1, DocID.Length - 1)
            Dim StrDate As String = txt_DocDate.Text.Trim()
            Dim Details As String = txt_DocDetails.Text.Trim()
            Dim M_valid As Boolean = M_CommonBL.UpdateAccDocDelt(DocID, StrDate, Details)
            If M_valid = True Then
                ShowAlertMessage("Successful Updated")
            Else
                ShowAlertMessage("Sorry !. Server Error.Try Again.")
            End If

        Catch ex As Exception
            ShowAlertMessage("Sorry !. Server Error.Try Again.")
        End Try
    End Sub

    Protected Sub Grid_DcumentDetails_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid_DcumentDetails.RowDataBound
        Try
            e.Row.Cells(1).Visible = False
            
            If e.Row.Cells(5).Text = "01/01/1900" Then e.Row.Cells(5).Text = "-"
            If e.Row.Cells(4).Text = "A" Then
                e.Row.Cells(4).Text = "Active"
                hid_status.Value = "OLD"
            End If
            If e.Row.Cells(4).Text = "R" Then
                e.Row.Cells(4).Text = "Return"
                Dim Check As CheckBox = DirectCast(e.Row.Cells(0).FindControl("Che_Status"), CheckBox)
                Check.Checked = True
                Check.Enabled = False
            End If

        Catch ex As Exception
            ShowAlertMessage("Sorry !. Server Error.Try Again.")
        End Try
    End Sub
End Class