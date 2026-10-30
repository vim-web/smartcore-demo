Imports SMARTCORE_BL
Imports System.Data
Imports System.Web.Services
Public Class RegionMaster
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session("M_EntryMode") = "NEW"
            Session("SearchType") = "USER"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            hid_RegCode.Value = TxtRegionCode.Text
        End If
        If Session("SearchResult") <> "" Then
            TxtRegionManager.Text = Session("SearchResult")
            TxtRegionManager_TextChanged(Me.TxtRegionManager, System.EventArgs.Empty)
            Session("SearchResult") = ""

        End If
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_Dt As New DataTable
            Dim M_CommonBl As New CommonBL

            If Len(Trim(TxtRegionCode.Text)) = 0 Then
                ShowAlertMessage("Please Enter Region Code")
                Exit Sub

            End If

            If Len(Trim(TxtRegionName.Text)) = 0 Then
                ShowAlertMessage("Please Enter Region Name")
                Exit Sub
            End If

            If Len(Trim(TxtRegionManager.Text)) = 0 Then
                ShowAlertMessage("Please select Region Manager")
                Exit Sub
            End If


            If Len(Trim(TxtRemarks.Text)) = 0 Then
                ShowAlertMessage("Please Enter Remarks")
                Exit Sub
            End If

            M_Dt = M_CommonBl.Insert_RegionMaster(Session("M_EntryMode"), TxtRegionCode.Text, TxtRegionName.Text, TxtRemarks.Text, Session("LoginUserID").ToString(), TxtRegionManager.Text)
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

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.ASPX")
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs) Handles CmdClear0.Click
        Try
            clear()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)

        End Try
    End Sub
    Protected Sub clear()
        TxtRegionCode.Text = ""
        TxtRegionName.Text = ""
        TxtRemarks.Text = ""
        TxtRegionManager.Text = ""
        TxtName.Text = ""
    End Sub
    Protected Sub TxtRegionCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtRegionCode.TextChanged


        Dim M_CommonBL As New CommonBL
        Dim M_Dt As New DataTable
        M_Dt = M_CommonBL.Get_RegionMaster(TxtRegionCode.Text)
        If M_Dt.Rows.Count > 0 Then
            Session("M_EntryMode") = "UPDATE"
            TxtRegionName.Text = M_Dt.Rows(0).Item("Region_Name").ToString()
            TxtRemarks.Text = M_Dt.Rows(0).Item("Remarks").ToString()
            TxtRegionManager.Text = M_Dt.Rows(0).Item("Region_Manager").ToString()
            TxtName.Text = M_Dt.Rows(0).Item("EMP_NAME").ToString()
        Else

            Session("M_EntryMode") = "NEW"
            TxtRegionName.Text = ""
            TxtRemarks.Text = ""
            TxtRegionManager.Text = ""
            TxtName.Text = ""

        End If
    End Sub

    Protected Sub TxtRegionManager_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtRegionManager.TextChanged
        Try
            Dim M_dt As New DataTable
            Dim M_CommonBl As New CommonBL
            M_dt = M_CommonBl.Get_UserName(TxtRegionManager.Text)
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
