Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL



Partial Public Class Loan_BDR_Report
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

                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "LOAN")
                DdlScheme.Items.Add("<--ALL-->")

                Dim M_CommonDa As New CommonDA
                Company_Name = M_CommonDa.GetCompanyDetails()
                M_CommonDa = Nothing
                DtFrom.Text = Session("TransDate")
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
            Dim SqlCmdQuery As String
            Dim DtScheme As New DataTable

            'If DdlBranch.SelectedValue <> "<--ALL-->" Then
            '    Brcode = DdlBranch.SelectedValue
            'Else
            '    Brcode = "0"
            'End If
            'If DdlScheme.SelectedValue <> "<--ALL-->" Then
            '    SchemeCode = DdlScheme.SelectedValue
            'Else
            '    SchemeCode = "0"
            'End If



            '-------------------------------------------


            If DtFrom.Text = "" Then
                ShowAlertMessage("Please enter As on Date")
                DtFrom.Focus()
                Exit Sub
            End If

            ImgProcess.Enabled = False

            M_DBConn.ExecuteScalar("DELETE from Loan_BDR_R_Tbl where Terminal = '" & Session("HostID") & "'", Connection.SQLSource.STOREDPROCEDURE, False)

            Dim M_CommonDA As New CommonDA

            ' DtBranch = GetBranches(Brcode, DdlBranch.Text)
            DtScheme = M_CommonDA.Fill_Section("LOAN")

            Dim Br_Code As String
            Dim Sch_Code As String
            Dim M_Ds As New DataSet
            Dim J As Integer
            Dim I As Integer

            'Dim Lbl As Label = DirectCast(UpdateProgress1.FindControl("Lbl_Status"), Label)
            'Lbl.Text = ""





            For J = 0 To DdlBranch.Items.Count - 1
                If DdlBranch.SelectedValue <> "<--ALL-->" Then
                    Br_Code = DdlBranch.SelectedValue
                Else
                    'If DdlBranch.Items(J).Text <> "<--ALL-->" Then J = J + 1
                    Br_Code = DdlBranch.Items(J).Value
                End If

                For I = 0 To DdlScheme.Items.Count - 1

                    If DdlScheme.SelectedValue <> "<--ALL-->" Then
                        Sch_Code = DdlScheme.SelectedValue
                        'Lbl.Text = "Pocessing  " & DdlScheme.Text & " - " & Br_Code
                    Else
                        Sch_Code = DdlScheme.Items(I).Value
                        'Lbl.Text = "Pocessing  " & DdlScheme.Items(J).Text & " - " & Br_Code
                    End If

                    If Sch_Code <> "<--ALL-->" And Br_Code <> "<--ALL-->" Then
                        SqlCmdQuery = "Exec Loan_BDR_Sp '" & Sch_Code & "','" & Br_Code & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & _
                            Session("LoginID") & "','" & Session("HostID") & "','N','N'," & IIf(rbtnSummary.Checked = True, "'Y'", "'N'")
                        M_DBConn.ExecuteDataSet(M_Ds, SqlCmdQuery, False)
                    End If

                    If DdlScheme.SelectedValue <> "<--ALL-->" Then Exit For
                Next I
                If DdlBranch.SelectedValue <> "<--ALL-->" Then Exit For
            Next J
            'Lbl.Text = ""

            Dim HeaderArr(20, 4) As String

            If M_Ds.Tables.Count >= 2 Then
                For I = 0 To M_Ds.Tables(1).Rows.Count - 1
                    HeaderArr(I, 0) = M_Ds.Tables(1).Rows(I)("HeadStr").ToString
                    HeaderArr(I, 1) = M_Ds.Tables(1).Rows(I)("HeadAlign").ToString
                    HeaderArr(I, 2) = M_Ds.Tables(1).Rows(I)("HeadGrWdith").ToString
                    HeaderArr(I, 3) = M_Ds.Tables(1).Rows(I)("HeadPrWdith").ToString
                Next
                Session("RptQry") = M_Ds.Tables(0).Rows(0)("RptQry").ToString
                Session("RptHeader") = HeaderArr
            End If


            ImgProcess.Enabled = True
            If DdlReportType.SelectedItem.Text <> "ALL" Then
                M_DBConn.ExecuteScalar("DELETE from Loan_BDR_R_Tbl where Terminal = '" & Session("HostID") & "' and   Display_Order not in ('" & DdlReportType.SelectedValue & "')", Connection.SQLSource.STOREDPROCEDURE, False)
            End If



            ShowAlertMessage("Process Completed.")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Response.Redirect("~/PrnView.aspx?PageType=80", False)
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