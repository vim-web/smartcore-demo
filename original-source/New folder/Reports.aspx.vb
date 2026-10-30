Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Partial Public Class Reports
    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim FormID As Integer = 1122
    Shared Obj_UserRights As Object




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


                M_DBConn = New Connection
                Call FillRptSection()

                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1

                LblDt3.Visible = False
                LblDt4.Visible = False
                LblTxt1.Visible = False
                LblTxt2.Visible = False
                Dt3.Visible = False
                Dt4.Visible = False
                Txt1.Visible = False
                Txt2.Visible = False
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillRptType(ByVal Rpt_Section As String)
        Try
            Dim M_DataTable As DataTable = New DataTable()

            Dim M_Sql As String
            M_Sql = "Select Rpt_ID,Rpt_Name from  Report_Master_M_Tbl Where Rpt_Section='" & Rpt_Section & "' "
            M_Sql = M_Sql & " order by Rpt_Name"
            M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)

            DdlRptType.DataTextField = "Rpt_Name"
            DdlRptType.DataValueField = "Rpt_ID"
            DdlRptType.DataSource = M_DataTable
            DdlRptType.DataBind()
            DdlRptType.Items.Add(" <--- SELECT --->")
            If DdlRptType.Items.Count > 0 Then DdlRptType.SelectedIndex = DdlRptType.Items.Count - 1
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillRptSection()
        Try
            Dim M_DataTable As DataTable = New DataTable()

            Dim M_Sql As String
            M_Sql = "Select distinct Upper(Rpt_Section) As Rpt_Section from  Report_Master_M_Tbl "
            M_Sql = M_Sql & " order by Rpt_Section"
            M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)

            DdlSection.DataTextField = "Rpt_Section"
            DdlSection.DataValueField = "Rpt_Section"
            DdlSection.DataSource = M_DataTable
            DdlSection.DataBind()
            DdlSection.Items.Add(" <--- SELECT --->")
            If DdlSection.Items.Count > 0 Then DdlSection.SelectedIndex = DdlSection.Items.Count - 1
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    
    Protected Sub DdlRptType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlRptType.SelectedIndexChanged
        Try
            Dim M_Dt As DataTable = New DataTable()

            Dim M_Sql As String

            M_DBConn = New Connection()
            If DdlRptType.SelectedIndex <> DdlRptType.Items.Count - 1 Then
                M_Sql = "Select * from  Report_Master_M_Tbl "
                M_Sql = M_Sql & " Where Rpt_ID=" & DdlRptType.SelectedValue
                M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

                If M_Dt.Rows.Count > 0 Then
                    Rpt_Qry = M_Dt.Rows(0)("Rpt_Qry").ToString()
                    Rpt_Path = M_Dt.Rows(0)("Rpt_Path").ToString()
                    Rpt_Heading = M_Dt.Rows(0)("Rpt_Heading").ToString()
                    Rpt_Formula = M_Dt.Rows(0)("Rpt_Formula").ToString()
                    Rpt_ProcessQry = M_Dt.Rows(0)("Rpt_ProcessQry").ToString()

                    ' Locking Unusable Controls
                    If InStr(Rpt_ProcessQry + Rpt_Qry + Rpt_Formula, "@TXT1", CompareMethod.Text) <= 0 Then
                        Txt1.Visible = False
                        LblTxt1.Visible = False
                    Else
                        Txt1.Visible = True
                        LblTxt1.Visible = True
                    End If
                    If InStr(Rpt_ProcessQry + Rpt_Qry + Rpt_Formula, "@TXT2", CompareMethod.Text) <= 0 Then
                        Txt2.Visible = False
                        LblTxt2.Visible = False
                    Else
                        Txt2.Visible = True
                        LblTxt2.Visible = True
                    End If
                    If InStr(Rpt_ProcessQry + Rpt_Qry + Rpt_Formula, "@DT1", CompareMethod.Text) <= 0 Then
                        Dt1.Visible = False
                        LblDt1.Visible = False
                    Else
                        Dt1.Visible = True
                        LblDt1.Visible = True
                    End If
                    If InStr(Rpt_ProcessQry + Rpt_Qry + Rpt_Formula, "@DT2", CompareMethod.Text) <= 0 Then
                        Dt2.Visible = False
                        LblDt2.Visible = False
                    Else
                        Dt2.Visible = True
                        LblDt2.Visible = True
                    End If
                    If InStr(Rpt_ProcessQry + Rpt_Qry + Rpt_Formula, "@DT3", CompareMethod.Text) <= 0 Then
                        Dt3.Visible = False
                        LblDt3.Visible = False
                    Else
                        Dt3.Visible = True
                        LblDt3.Visible = True
                    End If
                    If InStr(Rpt_ProcessQry + Rpt_Qry + Rpt_Formula, "@DT4", CompareMethod.Text) <= 0 Then
                        Dt4.Visible = False
                        LblDt4.Visible = False
                    Else
                        Dt4.Visible = True
                        LblDt4.Visible = True
                    End If

                End If

                BtnProcess.Visible = IIf(Rpt_ProcessQry.Length = 0, False, True)

                Call FillRptControls(DdlRptType.SelectedValue)

            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub BtnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnShow.Click
        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()
            Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")

            M_Sql = Rpt_Qry '.ToUpper()
            If Ddl1.Visible = True Then M_Sql = Replace(M_Sql, "@DDL1", "'" & Ddl1.SelectedValue & "'", , , CompareMethod.Text)
            If Ddl2.Visible = True Then M_Sql = Replace(M_Sql, "@DDL2", "'" & Ddl2.SelectedValue & "'", , , CompareMethod.Text)

            If Txt1.Visible = True Then M_Sql = Replace(M_Sql, "@TXT1", "'" & Txt1.Text & "'", , , CompareMethod.Text)
            If Txt2.Visible = True Then M_Sql = Replace(M_Sql, "@TXT2", "'" & Txt2.Text & "'", , , CompareMethod.Text)

            If Dt1.Visible = True Then M_Sql = Replace(M_Sql, "@DT1", ClsComm.DbDate(Dt1.Text), , , CompareMethod.Text)
            If Dt2.Visible = True Then M_Sql = Replace(M_Sql, "@DT2", ClsComm.DbDate(Dt2.Text), , , CompareMethod.Text)
            If Dt3.Visible = True Then M_Sql = Replace(M_Sql, "@DT3", ClsComm.DbDate(Dt3.Text), , , CompareMethod.Text)
            If Dt4.Visible = True Then M_Sql = Replace(M_Sql, "@DT4", ClsComm.DbDate(Dt4.Text), , , CompareMethod.Text)
            M_Sql = Replace(M_Sql, "@USERID", "'" & Session("LoginID") & "'", , , CompareMethod.Text)
            M_Sql = Replace(M_Sql, "@TERMINAL", "'" & Session("HostID") & "'", , , CompareMethod.Text)

            ' FORMULAS
            StrSlf = Rpt_Formula
            If Ddl1.Visible = True Then StrSlf = Replace(StrSlf, "@DDL1", Ddl1.SelectedItem.Text, , , CompareMethod.Text)
            If Ddl2.Visible = True Then StrSlf = Replace(StrSlf, "@DDL2", Ddl2.SelectedItem.Text, , , CompareMethod.Text)

            If Txt1.Visible = True Then StrSlf = Replace(StrSlf, "@TXT1", Txt1.Text, , , CompareMethod.Text)
            If Txt2.Visible = True Then StrSlf = Replace(StrSlf, "@TXT2", Txt2.Text, , , CompareMethod.Text)

            If Dt1.Visible = True Then StrSlf = Replace(StrSlf, "@DT1", Dt1.Text, , , CompareMethod.Text)
            If Dt2.Visible = True Then StrSlf = Replace(StrSlf, "@DT2", Dt2.Text, , , CompareMethod.Text)
            If Dt3.Visible = True Then StrSlf = Replace(StrSlf, "@DT3", Dt3.Text, , , CompareMethod.Text)
            If Dt4.Visible = True Then StrSlf = Replace(StrSlf, "@DT4", Dt4.Text, , , CompareMethod.Text)

            StrSlf = Replace(StrSlf, "@USERID", Session("LoginID"), , , CompareMethod.Text)
            StrSlf = Replace(StrSlf, "@TERMINAL", Session("HostID"), , , CompareMethod.Text)
            StrSlf = Replace(StrSlf, "@Company", Company_Name, , , CompareMethod.Text)

            ' Formula From Sql Query Output 
            If InStr(StrSlf, "@Sql") > 0 Then
                Dim M_Qry As String = ""
                Dim M_QryFormula As String = ""
                Dim QryVar As String = ""
                Dim QryNo1 As Integer = "0"
                QryVar = Mid(StrSlf, InStr(StrSlf, "@Sql"), IIf(Len(StrSlf) - InStr(StrSlf, "@Sql") < 6, Len(StrSlf), 6))
                QryVar = Trim(Replace(QryVar, "|", ""))
                If IsNumeric(Replace(QryVar, "@Sql", "")) = True Then
                    QryNo1 = Replace(QryVar, "@Sql", "")

                    If QryNo1 > 0 Then
                        M_Qry = "Select Formula_Qry From Report_Formula_Tbl Where Rpt_ID=" & DdlRptType.SelectedValue & " And Formula_Order=" & QryNo1
                        M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)

                        If M_Dt.Rows.Count > 0 Then

                            M_QryFormula = M_Dt.Rows(0)(0).ToString()
                            'M_QryFormula = M_QryFormula.ToUpper()

                            If Ddl1.Visible = True Then M_QryFormula = Replace(M_QryFormula, "@DDL1", "'" & Ddl1.SelectedValue & "'", , , CompareMethod.Text)
                            If Ddl2.Visible = True Then M_QryFormula = Replace(M_QryFormula, "@DDL2", "'" & Ddl2.SelectedValue & "'", , , CompareMethod.Text)

                            If Txt1.Visible = True Then M_QryFormula = Replace(M_QryFormula, "@TXT1", "'" & Txt1.Text & "'", , , CompareMethod.Text)
                            If Txt2.Visible = True Then M_QryFormula = Replace(M_QryFormula, "@TXT2", "'" & Txt2.Text & "'", , , CompareMethod.Text)

                            If Dt1.Visible = True Then M_QryFormula = Replace(M_QryFormula, "@DT1", ClsComm.DbDate(Dt1.Text), , , CompareMethod.Text)
                            If Dt2.Visible = True Then M_QryFormula = Replace(M_QryFormula, "@DT2", ClsComm.DbDate(Dt2.Text), , , CompareMethod.Text)
                            If Dt3.Visible = True Then M_QryFormula = Replace(M_QryFormula, "@DT3", ClsComm.DbDate(Dt3.Text), , , CompareMethod.Text)
                            If Dt4.Visible = True Then M_QryFormula = Replace(M_QryFormula, "@DT4", ClsComm.DbDate(Dt4.Text), , , CompareMethod.Text)
                            M_QryFormula = Replace(M_QryFormula, "@USERID", "'" & Session("LoginID") & "'", , , CompareMethod.Text)
                            M_QryFormula = Replace(M_QryFormula, "@TERMINAL", "'" & Session("HostID") & "'", , , CompareMethod.Text)

                            M_DBConn.ExecuteDataTable(M_Dt, M_QryFormula, False)

                            If M_Dt.Rows.Count > 0 Then
                                StrSlf = Replace(StrSlf, QryVar, M_Dt.Rows(0)(0).ToString())
                            Else
                                StrSlf = Replace(StrSlf, QryVar, "0")
                            End If
                        End If
                    End If

                End If
            End If

            Call GetSubReports(DdlRptType.SelectedValue)

            Session.Add("RptQry", M_Sql)
            Session.Add("RptPath", Server.MapPath("~/Reports/" & Rpt_Path))
            Session.Add("RptFormula", StrSlf)

            If CmbOutput.SelectedIndex = 0 Then
                Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
            Else
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetSubReports(ByVal RptID As Integer)
        Try
            Dim SubRptName As String = ""
            Dim SubRptQry(50) As String
            Dim M_Sql As String = ""
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()
            Dim SqlStr As String = ""
            Dim SubSql As String = ""
            Dim I As Integer = 0

            SqlStr = "SELECT Rpt_Name, Rpt_Qry, Rpt_ProcessQry FROM Report_SubRpt_M_Tbl Where Rpt_ID=" & RptID
            M_DBConn.ExecuteDataTable(M_Dt, SqlStr, False)

            For I = 0 To M_Dt.Rows.Count - 1
                If SubRptName.Length > 0 Then SubRptName = SubRptName & "|"
                SubRptName = SubRptName & M_Dt.Rows(I)("Rpt_Name").ToString
                SubRptQry(I) = M_Dt.Rows(I)("Rpt_Qry").ToString

                SubRptQry(I) = SubRptQry(I)

                If Ddl1.Visible = True Then SubRptQry(I) = Replace(SubRptQry(I), "@DDL1", "'" & Ddl1.SelectedValue & "'", , , CompareMethod.Text)
                If Ddl2.Visible = True Then SubRptQry(I) = Replace(SubRptQry(I), "@DDL2", "'" & Ddl2.SelectedValue & "'", , , CompareMethod.Text)

                If Txt1.Visible = True Then SubRptQry(I) = Replace(SubRptQry(I), "@TXT1", "'" & Txt1.Text & "'", , , CompareMethod.Text)
                If Txt2.Visible = True Then SubRptQry(I) = Replace(SubRptQry(I), "@TXT2", "'" & Txt2.Text & "'", , , CompareMethod.Text)

                If Dt1.Visible = True Then SubRptQry(I) = Replace(SubRptQry(I), "@DT1", ClsComm.DbDate(Dt1.Text), , , CompareMethod.Text)
                If Dt2.Visible = True Then SubRptQry(I) = Replace(SubRptQry(I), "@DT2", ClsComm.DbDate(Dt2.Text), , , CompareMethod.Text)
                If Dt3.Visible = True Then SubRptQry(I) = Replace(SubRptQry(I), "@DT3", ClsComm.DbDate(Dt3.Text), , , CompareMethod.Text)
                If Dt4.Visible = True Then SubRptQry(I) = Replace(SubRptQry(I), "@DT4", ClsComm.DbDate(Dt4.Text), , , CompareMethod.Text)
                M_Sql = Replace(SubRptQry(I), "@USERID", "'" & Session("LoginID") & "'", , , CompareMethod.Text)
                M_Sql = Replace(SubRptQry(I), "@TERMINAL", "'" & Session("HostID") & "'", , , CompareMethod.Text)
            Next

            Session.Add("SubRptNames", SubRptName)
            Session.Add("SubRptQry", SubRptQry)
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

            M_Sql = "Select * from  Report_Sub_Tbl "
            M_Sql = M_Sql & " Where Rpt_ID=" & DdlRptType.SelectedValue
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            Ddl1.Visible = False
            Ddl2.Visible = False
            Ddl1.Items.Clear()
            Ddl2.Items.Clear()

            LblDdl1.Text = ""
            LblDdl2.Text = ""
            LblDt1.Text = ""
            LblDt2.Text = ""
            LblDt3.Text = ""
            LblDt4.Text = ""
            LblTxt1.Text = ""
            LblTxt2.Text = ""

            While M_Dt.Rows.Count >= I
                If M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "DDL" Then ' DropdownList
                    If M_Dt.Rows(I - 1)("Ctrl_Order").ToString() = 1 Then
                        Ddl1.DataTextField = M_Dt.Rows(I - 1)("Ctrl_Display").ToString()
                        Ddl1.DataValueField = M_Dt.Rows(I - 1)("Ctrl_Value").ToString()
                        M_Sql = M_Dt.Rows(I - 1)("Ctr_Qry").ToString()
                        M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)
                        Ddl1.DataSource = M_DataTable
                        Ddl1.DataBind()
                        If M_Sql = M_Dt.Rows(I - 1)("Ctr_AllOption").ToString() Then
                            Ddl1.Items.Add(" <--- SELECT --->")
                        Else
                            If Ddl1.Items.Count > 0 Then Ddl1.SelectedIndex = 0
                        End If
                        Ddl1.Visible = True

                        LblDdl1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()

                    ElseIf M_Dt.Rows(I - 1)("Ctrl_Order").ToString() = 2 Then
                        Ddl2.DataTextField = M_Dt.Rows(I - 1)("Ctrl_Display").ToString()
                        Ddl2.DataValueField = M_Dt.Rows(I - 1)("Ctrl_Value").ToString()
                        M_Sql = M_Dt.Rows(I - 1)("Ctr_Qry").ToString()
                        M_DBConn.ExecuteDataTable(M_DataTable, M_Sql, False)
                        Ddl2.DataSource = M_DataTable
                        Ddl2.DataBind()
                        If M_Sql = M_Dt.Rows(I - 1)("Ctr_AllOption").ToString() Then
                            Ddl2.Items.Add(" <--- SELECT --->")
                        Else
                            If Ddl2.Items.Count > 0 Then Ddl1.SelectedIndex = 0
                        End If
                        Ddl2.Visible = True

                        LblDdl2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End If
                ElseIf M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "DT" Then ' DropdownList
                    Select Case M_Dt.Rows(I - 1)("Ctrl_Order").ToString()
                        Case 1
                            LblDt1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 2
                            LblDt2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 3
                            LblDt3.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 4
                            LblDt4.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End Select
                ElseIf M_Dt.Rows(I - 1)("Ctrl_Type").ToString().ToUpper = "TXT" Then ' DropdownList
                    Select Case M_Dt.Rows(I - 1)("Ctrl_Order").ToString()
                        Case 1
                            LblTxt1.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                        Case 2
                            LblTxt2.Text = M_Dt.Rows(I - 1)("Ctr_Caption").ToString()
                    End Select
                End If
                I = I + 1
            End While
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub DdlSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlSection.SelectedIndexChanged
        Call FillRptType(DdlSection.SelectedValue)
    End Sub


    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub


    Protected Sub BtnProcess_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnProcess.Click
        Try
            Dim M_Dt As DataTable = New DataTable()

            Dim M_Sql As String
            Dim ClsComm As New CommonClass()

            M_DBConn = New Connection()

            M_Sql = Rpt_ProcessQry.ToUpper()
            If Ddl1.Visible = True Then M_Sql = Replace(M_Sql, "@DDL1", "'" & Ddl1.SelectedValue & "'")
            If Ddl2.Visible = True Then M_Sql = Replace(M_Sql, "@DDL2", "'" & Ddl2.SelectedValue & "'")

            If Txt1.Visible = True Then M_Sql = Replace(M_Sql, "@TXT1", "'" & Txt1.Text & "'")
            If Txt2.Visible = True Then M_Sql = Replace(M_Sql, "@TXT2", "'" & Txt2.Text & "'")

            If Dt1.Visible = True Then M_Sql = Replace(M_Sql, "@DT1", ClsComm.DbDate(Dt1.Text))
            If Dt2.Visible = True Then M_Sql = Replace(M_Sql, "@DT2", ClsComm.DbDate(Dt2.Text))
            If Dt3.Visible = True Then M_Sql = Replace(M_Sql, "@DT3", ClsComm.DbDate(Dt3.Text))
            If Dt4.Visible = True Then M_Sql = Replace(M_Sql, "@DT4", ClsComm.DbDate(Dt4.Text))

            M_Sql = Replace(M_Sql, "@USERID", "'" & Session("LoginID") & "'")
            M_Sql = Replace(M_Sql, "@TERMINAL", "'" & Session("HostID") & "'")


            'M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
            M_DBConn.ExecuteScalar(M_Sql, , False)

            ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub BtnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub Dt4_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Dt4.TextChanged

    End Sub
End Class