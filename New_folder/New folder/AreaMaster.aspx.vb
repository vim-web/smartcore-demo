Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Public Class AreaMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                M_Comm.Fill_Region(DrpRegion)
                Session("SearchType") = "USER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            End If
            If Session("SearchResult") <> "" Then
                TxtEmpId.Text = Session("SearchResult")
                TxtEmpId_TextChanged(Me.TxtEmpId, System.EventArgs.Empty)
                Session("SearchResult") = ""

            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Public Shared Sub ShowAlertMessag(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_Dt As New DataTable
            Dim M_CommonBl As New CommonBL

            If Len(Trim(TxtAreaCode.Text)) = 0 Then
                ShowAlertMessage("Please Enter Area Code")
                Exit Sub

            End If

            If Len(Trim(TxtAreaName.Text)) = 0 Then
                ShowAlertMessage("Please Enter Area Name")
                Exit Sub
            End If

            If Len(Trim(TxtRemarks.Text)) = 0 Then
                ShowAlertMessage("Please Enter Remarks")
                Exit Sub
            End If
            If Len(Trim(TxtEmpId.Text)) = 0 Then
                ShowAlertMessage("Please Select Area Manager")
                Exit Sub
            End If
            M_Dt = M_CommonBl.Insert_AreaMaster(Session("M_EntryMode"), TxtAreaCode.Text, DrpRegion.SelectedValue, TxtAreaName.Text, TxtRemarks.Text, Session("UserID"), TxtEmpId.Text)
            If M_Dt.Rows.Count > 0 Then
                If Session("M_EntryMode") = "UPDATE" Then
                    ShowAlertMessage("Updated Successfully...")
                Else
                    ShowAlertMessage("Saved Successfully...")
                End If
                clear()
            Else
                ShowAlertMessage("Failed")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.ASPX")
    End Sub
    Protected Sub CmdClear0_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClear0.Click
        Try
            clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub

    Protected Sub clear()
        TxtAreaName.Text = ""
        TxtRemarks.Text = ""
        TxtEmpId.Text = ""
        TxtName.Text = ""
    End Sub
    Protected Sub TxtAreaCode_TextChanged(sender As Object, e As EventArgs) Handles TxtAreaCode.TextChanged


        Dim M_CommonBL As New CommonBL
        Dim M_Dt As New DataTable

        M_Dt = M_CommonBL.Get_AreaMaster(TxtAreaCode.Text)
        If M_Dt.Rows.Count > 0 Then
            Session("M_EntryMode") = "UPDATE"
            TxtAreaName.Text = M_Dt.Rows(0).Item("Area_Name").ToString()
            TxtRemarks.Text = M_Dt.Rows(0).Item("Remarks").ToString()
            TxtEmpId.Text = M_Dt.Rows(0).Item("Area_Manager").ToString()
            TxtName.Text = M_Dt.Rows(0).Item("Emp_Name").ToString()
        Else
            Session("M_EntryMode") = "NEW"
            TxtAreaName.Text = ""
            TxtRemarks.Text = ""
            TxtEmpId.Text = ""
            TxtName.Text = ""
        End If



    End Sub

    Protected Sub TxtEmpId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtEmpId.TextChanged
        Try
            Dim M_dt As New DataTable
            Dim M_CommonBl As New CommonBL
            M_dt = M_CommonBl.Get_UserName(TxtEmpId.Text)
            If M_dt.Rows.Count > 0 Then
                TxtName.Text = M_dt.Rows(0).Item("Emp_Name")

                M_dt = Nothing
            Else
                TxtName.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class