Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Imports System.Drawing
Imports System.IO
Imports System.Web.UI


Partial Public Class BalanceSheetDrillingDown
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1010
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                If IsPostBack = False Then

                    Dim M_Comm As New CommonBL
                    Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                    M_Comm = Nothing
                    If Obj_UserRights.View_Right = False Then
                        Response.Redirect("~/UnauthorisedAcces.ASPX")
                    End If

                    M_DBConn = New Connection
                    Dim M_CommonBL As New CommonBL
                    ''**********************************************************************************
                    If System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH") <> "%" Then
                        M_CommonBL.Fill_Branch(DrpBranch, False)
                    Else
                        M_CommonBL.Fill_Branch(DrpBranch, True)
                    End If
                    ''**********************************************************************************
                    DrpBranch.SelectedValue = Session("DefaultBrCode")
                    TxtAsOn.Text = Session("TransDate")
                    TxtToDt.Text = Session("TransDate")
                    DrpType.Items.Add("Balance Sheet")
                    DrpType.Items.Add("Profit & Loss")
                    DrpType.SelectedIndex = 0

                    Dim M_Dt As New DataTable()
                    M_Dt = M_CommonBL.Get_CashCode()
                    If M_Dt.Rows.Count > 0 Then
                        Session("CASH_CODE") = M_Dt.Rows(0).Item("Acc_Code")
                        Session("BANK_CODE") = M_Dt.Rows(1).Item("Acc_Code")
                    End If
                    'RdoOption.Items(0).Selected = True
                    '  BindGrid()
                End If
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub GvLiab_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GvLiab.SelectedIndexChanged
        Try
            Dim AccCode As String
            Dim AccHead As String
            Dim M_CommonBl As New CommonBL
            Dim M_dt As New DataTable
            Dim Branch As String
            Dim RptType As String

            If DrpType.SelectedIndex = 0 Then
                RptType = "B"
            ElseIf DrpType.SelectedIndex = 1 Then
                RptType = "P"
            End If

            If DrpBranch.SelectedValue = "<-- ALL -->" Then
                Branch = "0"
            Else
                Branch = DrpBranch.SelectedValue
            End If
            ' Dim index As Integer = Convert.ToInt32(e)
            For Each row1 As GridViewRow In GvLiab.Rows
                If row1.RowIndex = GvLiab.SelectedIndex Then
                    row1.BackColor = ColorTranslator.FromHtml("#A1DCF2")
                Else
                    row1.BackColor = ColorTranslator.FromHtml("#FFFFFF")
                End If
            Next
            Dim row As GridViewRow = GvLiab.SelectedRow
            AccCode = row.Cells(1).Text
            AccHead = row.Cells(2).Text
          



            If AccCode = Session("BANK_CODE") Or AccCode = Session("CASH_CODE") Then

                HdnCashCode.Value = "Y"
                M_dt = M_CommonBl.Get_GLyearBalance(AccCode, TxtAsOn.Text, TxtToDt.Text, Branch, RptType)
                DivBal.Visible = False
                DivSubTotal.Visible = True
                If M_dt.Rows.Count > 0 Then
                    GVSubTotal.DataSource = M_dt
                    GVSubTotal.DataBind()
                End If

            Else
                HdnCashCode.Value = "N"
                M_dt = M_CommonBl.Get_GLyearBalance(AccCode, TxtAsOn.Text, TxtToDt.Text, Branch, RptType)
                DivBal.Visible = False
                DivYear.Visible = True

                GvYear.DataSource = M_dt
                GvYear.DataBind()

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try


    End Sub

    Protected Sub GvAsset_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GvAsset.SelectedIndexChanged
        Try
            Dim AccCode As String
            Dim AccHead As String
            Dim M_CommonBl As New CommonBL
            Dim M_dt As New DataTable
            Dim Branch As String
            Dim RptType As String

            If DrpType.SelectedIndex = 0 Then
                RptType = "B"
            ElseIf DrpType.SelectedIndex = 1 Then
                RptType = "P"
            End If
          

            If DrpBranch.SelectedValue = "<-- ALL -->" Then
                Branch = "0"
            Else
                Branch = DrpBranch.SelectedValue
            End If
            ' Dim index As Integer = Convert.ToInt32(e)
            For Each row1 As GridViewRow In GvAsset.Rows
                If row1.RowIndex = GvAsset.SelectedIndex Then
                    row1.BackColor = ColorTranslator.FromHtml("#A1DCF2")
                Else
                    row1.BackColor = ColorTranslator.FromHtml("#FFFFFF")
                End If
            Next
            Dim row As GridViewRow = GvAsset.SelectedRow
            AccCode = row.Cells(1).Text
            AccHead = row.Cells(2).Text

            If AccCode = Session("BANK_CODE") Or AccCode = Session("CASH_CODE") Then
                HdnCashCode.Value = "Y"

                M_dt = M_CommonBl.Get_GLyearBalance(AccCode, TxtAsOn.Text, TxtToDt.Text, Branch, RptType)
                DivBal.Visible = False
                DivSubTotal.Visible = True
                If M_dt.Rows.Count > 0 Then
                    GVSubTotal.DataSource = M_dt
                    GVSubTotal.DataBind()
                End If

            Else
                HdnCashCode.Value = "N"
                M_dt = M_CommonBl.Get_GLyearBalance(AccCode, TxtAsOn.Text, TxtToDt.Text, Branch, RptType)
                DivBal.Visible = False
                DivYear.Visible = True
                DivSubTotal.Visible = False
                If M_dt.Rows.Count > 0 Then
                    GvYear.DataSource = M_dt
                    GvYear.DataBind()
                End If
            End If





        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub


    Protected Sub GvYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GvYear.SelectedIndexChanged
        Try
            Dim AccCode As String
            Dim AccHead As String
            Dim FinYear As String
            Dim M_CommonBl As New CommonBL
            Dim M_dt As New DataTable
            Dim Branch As String
            Dim RptType As String

            If DrpType.SelectedIndex = 0 Then
                RptType = "B"
            ElseIf DrpType.SelectedIndex = 1 Then
                RptType = "P"
            End If
            If DrpBranch.SelectedValue = "<-- ALL -->" Then
                Branch = "0"
            Else
                Branch = DrpBranch.SelectedValue
            End If
            ' Dim index As Integer = Convert.ToInt32(e)
            Dim row As GridViewRow = GvYear.SelectedRow
            AccCode = row.Cells(0).Text
            AccHead = row.Cells(1).Text
            FinYear = row.Cells(2).Text
            M_dt = M_CommonBl.Get_GLMonthBalance(AccCode, FinYear, Branch, TxtAsOn.Text, TxtToDt.Text, RptType)
            DivYear.Visible = False
            DivMonth.Visible = True
            DivSubTotal.Visible = False
            If M_dt.Rows.Count > 0 Then
                GvMonth.DataSource = M_dt
                GvMonth.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub GvMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GvMonth.SelectedIndexChanged
        Try


            For Each row1 As GridViewRow In GvMonth.Rows
                If row1.RowIndex = GvMonth.SelectedIndex Then
                    row1.BackColor = System.Drawing.Color.Aquamarine
                Else
                    row1.BackColor = ColorTranslator.FromHtml("#FFFFFF")
                End If
            Next
            Dim AccCode As String
            Dim AccHead As String
            Dim Month As String
            Dim M_CommonBl As New CommonBL
            Dim M_dt As New DataTable
            Dim Branch As String
            Dim RptType As String

            If DrpType.SelectedIndex = 0 Then
                RptType = "B"
            ElseIf DrpType.SelectedIndex = 1 Then
                RptType = "P"
            End If
            If DrpBranch.SelectedValue = "<-- ALL -->" Then
                Branch = "0"
            Else
                Branch = DrpBranch.SelectedValue
            End If
            ' Dim index As Integer = Convert.ToInt32(e)
            Dim row As GridViewRow = GvMonth.SelectedRow
            AccCode = row.Cells(0).Text
            AccHead = row.Cells(1).Text
            Month = row.Cells(2).Text
            M_dt = M_CommonBl.Get_GLDailyBalance(AccCode, Month, Branch, TxtAsOn.Text, RptType)
            DivDaily.Visible = True
            DivMonth.Visible = False
            If M_dt.Rows.Count > 0 Then
                GvDaily.DataSource = M_dt
                GvDaily.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try


    End Sub

    Protected Sub BtnBack1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnBack1.Click
    
        If HdnCashCode.Value = "Y" Then
            DivYear.Visible = False
            DivSubTotal.Visible = True
        Else
            DivBal.Visible = True
            DivYear.Visible = False
        End If


    End Sub

    Protected Sub BtnBack2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnBack2.Click
        DivMonth.Visible = False
        DivYear.Visible = True
    End Sub

    Protected Sub BtnBack3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnBack3.Click
        DivMonth.Visible = True
        DivDaily.Visible = False
    End Sub

    Protected Sub GvDaily_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GvDaily.SelectedIndexChanged
        Try
            Dim AccCode As String
            Dim AccHead As String
            Dim Tr_Date As String
            Dim M_CommonBl As New CommonBL
            Dim M_dt As New DataTable
            Dim Branch As String

            If DrpBranch.SelectedValue = "<-- ALL -->" Then
                Branch = "0"
            Else
                Branch = DrpBranch.SelectedValue
            End If
            ' Dim index As Integer = Convert.ToInt32(e)
            Dim row As GridViewRow = GvDaily.SelectedRow
            AccCode = row.Cells(0).Text
            AccHead = row.Cells(1).Text
            Tr_Date = row.Cells(2).Text
            M_dt = M_CommonBl.Get_GLDayBookBalance(Branch, AccCode, Tr_Date)
            DivDayBook.Visible = True
            DivDaily.Visible = False
            If M_dt.Rows.Count > 0 Then
                GvDayBook.DataSource = M_dt
                GvDayBook.DataBind()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub BtnBack4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnBack4.Click
        DivDayBook.Visible = False
        DivDaily.Visible = True
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try


            Dim M_commonBl As New CommonBL
            Dim M_Ds As New DataSet()
            Dim Dt1 As New DataTable
            Dim Dt2 As New DataTable
            Dim Branch As String

            If DrpBranch.SelectedValue = "<-- ALL -->" Then
                Branch = "All"
            Else
                Branch = DrpBranch.SelectedValue
            End If
            If DrpType.SelectedIndex = 0 Then
                M_Ds = M_commonBl.Get_BalanceSheet(Branch, TxtAsOn.Text, Session("LoginUserID"))
                Session("locLiabTbl") = M_Ds.Tables(0)
                Session("locAssetTbl") = M_Ds.Tables(1)

            End If
            If DrpType.SelectedIndex = 1 Then
                M_Ds = M_commonBl.Get_ProfitLoss(Branch, TxtAsOn.Text, TxtToDt.Text, Session("LoginUserID"))
                Session("locLiabTbl") = M_Ds.Tables(0)
                Session("locAssetTbl") = M_Ds.Tables(1)

            End If

          

            BindGrid()
            
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Protected Sub BindGrid()

        Dim M_Dt As New DataTable()
        Dim M_Dt1 As New DataTable()
        M_Dt = DirectCast(Session("locLiabTbl"), DataTable)
        M_Dt1 = DirectCast(Session("locAssetTbl"), DataTable)
        GvLiab.DataSource = M_Dt
        GvLiab.DataBind()
        GvAsset.DataSource = M_Dt1
        GvAsset.DataBind()
        DivBal.Visible = True
        DivDaily.Visible = False
        DivDayBook.Visible = False
        DivMonth.Visible = False
        DivYear.Visible = False

    End Sub

    Protected Sub GvLiab_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvLiab.RowDataBound
        Try
            'If e.Row.Cells(3).Text = "Y" Then
            '    e.Row.BackColor = System.Drawing.Color.Aquamarine
            'End If
            'If e.Row.Cells(3).Text = "G" Then
            '    e.Row.BackColor = System.Drawing.Color.Cyan
            'End If
            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim pretype As String = e.Row.Cells(0).Text
                    Dim Amnt As String = e.Row.Cells(3).Text
                    Dim AccCode As String = e.Row.Cells(1).Text

                    For Each cell As TableCell In e.Row.Cells
                        If pretype = "G" Then
                            e.Row.ForeColor = System.Drawing.Color.Black
                            e.Row.Font.Bold = True
                            e.Row.Cells(4).Controls(0).Visible = False
                        End If
                        If pretype = "Y" Then
                            e.Row.ForeColor = System.Drawing.Color.Black
                            e.Row.Font.Bold = True
                            e.Row.Cells(4).Controls(0).Visible = False
                        End If
                        If Amnt = "0.00" And AccCode = "&nbsp;" Then
                            e.Row.Cells(3).Text = ""
                            'e.Row.Cells(4).Controls(0).EnableTheming = False
                            e.Row.Cells(4).Enabled = False
                        End If
                        e.Row.Cells(0).Visible = False
                    Next

                End If
            End If
            GvLiab.HeaderRow.Cells(0).Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GvAsset_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvAsset.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                'If e.Row.Cells(3).Text = "Y" Then

                '    e.Row.BackColor = System.Drawing.Color.Aquamarine
                '    e.Row.BackColor = Drawing.Color.FromName("#EEEFFE")

                'End If
                'If e.Row.Cells(3).Text = "G" Then
                '    e.Row.BackColor = System.Drawing.Color.Cyan
                'End If
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim pretype As String = e.Row.Cells(0).Text
                    Dim Amnt As String = e.Row.Cells(3).Text
                    Dim AccCode As String = e.Row.Cells(1).Text
                    For Each cell As TableCell In e.Row.Cells
                        If pretype = "G" Then
                            e.Row.BackColor = Color.Black
                            e.Row.Font.Bold = True
                            e.Row.Cells(4).Controls(0).Visible = False

                        End If
                        If pretype = "Y" Then
                            e.Row.ForeColor = System.Drawing.Color.Black
                            e.Row.Font.Bold = True
                            e.Row.Cells(4).Controls(0).Visible = False
                        End If
                        If Amnt = "0.00" And AccCode = "&nbsp;" Then
                            e.Row.Cells(3).Text = ""
                            e.Row.BackColor = Color.Black
                            e.Row.Font.Bold = True
                            e.Row.Cells(4).Enabled = False
                        End If
                        e.Row.Cells(0).Visible = False
                    Next
                End If
            End If
            GvAsset.HeaderRow.Cells(0).Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnExcel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExcel.Click
        Try
            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "Customers.xls"))
            Response.ContentType = "application/ms-excel"
            Dim sw As New StringWriter()
            Dim htw As New HtmlTextWriter(sw)
            GvDayBook.AllowPaging = False

            'Change the Header Row back to white color
            GvDayBook.HeaderRow.Style.Add("background-color", "#FFFFFF")
            'Applying stlye to gridview header cells

            GvDayBook.RenderControl(htw)
            Response.Write(sw.ToString())
            Response.[End]()
        Catch ex As Exception

        End Try


       
    End Sub
    
    Protected Sub GVSubTotal_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GVSubTotal.SelectedIndexChanged
        Try
            Dim AccCode As String
            Dim AccHead As String
            Dim M_CommonBl As New CommonBL
            Dim M_dt As New DataTable
            Dim Branch As String
            Dim RptType As String
            If DrpType.SelectedIndex = 0 Then
                RptType = "B"
            ElseIf DrpType.SelectedIndex = 1 Then
                RptType = "P"
            End If

            If DrpBranch.SelectedValue = "<-- ALL -->" Then
                Branch = "0"
            Else
                Branch = DrpBranch.SelectedValue
            End If
            ' Dim index As Integer = Convert.ToInt32(e)
            For Each row1 As GridViewRow In GVSubTotal.Rows
                If row1.RowIndex = GVSubTotal.SelectedIndex Then
                    row1.BackColor = ColorTranslator.FromHtml("#A1DCF2")
                Else
                    row1.BackColor = ColorTranslator.FromHtml("#FFFFFF")
                End If
            Next
            Dim row As GridViewRow = GVSubTotal.SelectedRow
            AccCode = row.Cells(0).Text
            AccHead = row.Cells(1).Text


            M_dt = M_CommonBl.Get_GLyearBalance(AccCode, TxtAsOn.Text, TxtToDt.Text, Branch, RptType)
            DivBal.Visible = False
            DivYear.Visible = True
            DivSubTotal.Visible = False
            If M_dt.Rows.Count > 0 Then
                GvYear.DataSource = M_dt
                GvYear.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnBack5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnBack5.Click
        DivBal.Visible = True
        DivSubTotal.Visible = False
    End Sub

    Protected Sub GvYear_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvYear.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim pretype As String = e.Row.Cells(1).Text
                    Dim AccCode As String = e.Row.Cells(0).Text

                    For Each cell As TableCell In e.Row.Cells
                        If pretype = "Total" And AccCode = "&nbsp;" Then
                            e.Row.ForeColor = System.Drawing.Color.Black
                            e.Row.Font.Bold = True
                            e.Row.Cells(7).Controls(0).Visible = False
                        End If
                      
                       
                    Next

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GvMonth_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvMonth.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim pretype As String = e.Row.Cells(1).Text
                    Dim AccCode As String = e.Row.Cells(0).Text

                    For Each cell As TableCell In e.Row.Cells
                        If pretype = "Total" And AccCode = "&nbsp;" Then
                            e.Row.ForeColor = System.Drawing.Color.Black
                            e.Row.Font.Bold = True
                            e.Row.Cells(7).Controls(0).Visible = False
                        End If


                    Next

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GvDaily_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvDaily.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim pretype As String = e.Row.Cells(1).Text
                    Dim AccCode As String = e.Row.Cells(0).Text

                    For Each cell As TableCell In e.Row.Cells
                        If pretype = "Total" And AccCode = "&nbsp;" Then
                            e.Row.ForeColor = System.Drawing.Color.Black
                            e.Row.Font.Bold = True
                            e.Row.Cells(7).Controls(0).Visible = False
                        End If


                    Next

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpType.SelectedIndexChanged
        If DrpType.SelectedIndex = 1 Then
            LblLiab.Text = "Expenditure"
            LblAsset.Text = "Income"
            LblFrom.Text = "From Date"
            LblToDt.Visible = True
            TxtToDt.Visible = True
        End If
        If DrpType.SelectedIndex = 0 Then
            LblLiab.Text = "Liability"
            LblAsset.Text = "Asset"
            LblFrom.Text = "As On Date"
            LblToDt.Visible = False
            TxtToDt.Visible = False
        End If
    End Sub
End Class