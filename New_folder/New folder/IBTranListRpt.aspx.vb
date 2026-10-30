


Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL



Partial Public Class IBTranListRpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1079
    Shared Obj_UserRights As Object
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String

    'Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
    '    'Dim Lbl As Label = DirectCast(UpdateProgress1.FindControl("Lbl_Status"), Label)
    '    'Lbl.Text = DateTime.Now.ToString()
    '    MyBase.Render(writer)
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then


                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If


                'Dim Lbl As Label = DirectCast(UpdateProgress1.FindControl("Lbl_Status"), Label)
                'Lbl.Text = ""
                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(DdlBranch)
                DdlBranch.Items.Add("<--ALL-->")

                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"))
                DdlScheme.Items.Add("<--ALL-->")

                Dim M_CommonDa As New CommonDA
                Company_Name = M_CommonDa.GetCompanyDetails()
                M_CommonDa = Nothing
                DtFrom.Text = Session("TransDate")

                DdlBranch.SelectedValue = Session("DefaultBrCode")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillRptControls(ByVal RptID As Integer)
        Try
            Dim M_DataTable As DataTable = New DataTable()
            Dim M_Dt As DataTable = New DataTable()
            Dim I As Integer = 1
            Dim M_Sql As String

            'M_Sql = "Select * from  Report_Sub_Tbl "
            'M_Sql = M_Sql & " Where Rpt_ID=" & DdlRptType.SelectedValue
            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            DdlBranch.Visible = False
            DdlScheme.Visible = False
            DdlBranch.Items.Clear()
            DdlScheme.Items.Clear()

            LblDdl1.Text = ""
            LblDdl2.Text = ""
            LblDt1.Text = ""


            While M_Dt.Rows.Count >= I
                If M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "DDL" Then ' DropdownList
                    If M_Dt.Rows(I - 1)("Ctrl_Order").ToString() = 1 Then
                        DdlBranch.DataTextField = M_Dt.Rows(I - 1)("Ctrl_Display").ToString()
                        DdlBranch.DataValueField = M_Dt.Rows(I - 1)("Ctrl_Value").ToString()
                        M_Sql = M_Dt.Rows(I - 1)("Ctr_Qry").ToString()
                        M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)
                        DdlBranch.DataSource = M_DataTable
                        DdlBranch.DataBind()
                        If M_Sql = M_Dt.Rows(I - 1)("Ctr_AllOption").ToString() Then
                            DdlBranch.Items.Add(" <--- SELECT --->")
                        Else
                            If DdlBranch.Items.Count > 0 Then DdlBranch.SelectedIndex = 0
                        End If
                        DdlBranch.Visible = True

                        LblDdl1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()

                    ElseIf M_Dt.Rows(I - 1)("Ctrl_Order").ToString() = 2 Then
                        DdlScheme.DataTextField = M_Dt.Rows(I - 1)("Ctrl_Display").ToString()
                        DdlScheme.DataValueField = M_Dt.Rows(I - 1)("Ctrl_Value").ToString()
                        M_Sql = M_Dt.Rows(I - 1)("Ctr_Qry").ToString()
                        M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)
                        DdlScheme.DataSource = M_DataTable
                        DdlScheme.DataBind()
                        If M_Sql = M_Dt.Rows(I - 1)("Ctr_AllOption").ToString() Then
                            DdlScheme.Items.Add(" <--- SELECT --->")
                        Else
                            If DdlScheme.Items.Count > 0 Then DdlBranch.SelectedIndex = 0
                        End If
                        DdlScheme.Visible = True

                        LblDdl2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End If
                ElseIf M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "DT" Then ' DropdownList
                    Select Case M_Dt.Rows(I - 1)("Ctrl_Order").ToString()
                        Case 1
                            LblDt1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 2
                            'LblDt2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 3
                            'LblTxt3.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 4
                            'LblTxt4.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End Select
                ElseIf M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "TXT" Then ' DropdownList
                    Select Case M_Dt.Rows(I - 1)("Ctrl_Order").ToString()
                        Case 1
                            '  LblTxt1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 2
                            ' LblTxt2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End Select
                End If
                I = I + 1
            End While
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Function GetBranches(ByVal SelectedBrCode As String, ByVal SelectedBrName As String) As DataTable
        Try
            Dim M_DataTable As New DataTable
            Dim M_CommonDA As New CommonDA
            If SelectedBrCode = "0" Then
                M_DataTable = M_CommonDA.Fill_Branch()
            Else
                M_DataTable.Rows.Add(SelectedBrCode, SelectedBrName)
            End If
            Return M_DataTable
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Function

    Protected Sub rbtnDetailed_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbtnDetailed.CheckedChanged
        Try
            If rbtnDetailed.Checked = True Then

                rbtnSummary.Checked = False
            ElseIf rbtnSummary.Checked = True Then
                rbtnDetailed.Checked = False
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub rbtnSummary_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbtnSummary.CheckedChanged
        Try
            If rbtnSummary.Checked = True Then

                rbtnDetailed.Checked = False
            ElseIf rbtnDetailed.Checked = True Then
                rbtnSummary.Checked = False
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub ImgProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcess.Click
        Try
            



            ShowAlertMessage("Process Completed.")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try

            Dim SchCode As String
            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String
            If DdlScheme.SelectedItem.Text = "<--ALL-->" Then
                SchCode = "%"
            Else
                SchCode = DdlScheme.SelectedItem.Value.ToString()
            End If


            HeaderArr(0, 0) = "Scheme"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "15"
            HeaderArr(0, 3) = "19"

            HeaderArr(1, 0) = "Acc No"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "10"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "Date"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "5"
            HeaderArr(2, 3) = "10"

            HeaderArr(3, 0) = "Account Head"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "10"
            HeaderArr(3, 3) = "10"

            HeaderArr(4, 0) = "Vr.No"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "5"
            HeaderArr(4, 3) = "6"

            HeaderArr(5, 0) = "Cash"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"

            HeaderArr(6, 0) = "Transfer"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "10"

            HeaderArr(7, 0) = "Tran Type"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "5"
            HeaderArr(7, 3) = "6"

            HeaderArr(8, 0) = "Narration"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "15"
            HeaderArr(8, 3) = "25"

            HeaderArr(9, 0) = "IB Branch Name"
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "15"
            HeaderArr(9, 3) = "20"

            Session("RptHeader") = HeaderArr

            Dim M_Sql As String
            M_Sql = "Exec IBTranListRpt_Sp '" & DdlBranch.SelectedValue & "','" & SchCode & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "'"
            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=130", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Try
            Response.Redirect("~/Main.aspx")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class