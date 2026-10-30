Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_DAL
Imports SMARTCORE_Common.CommonClass
Imports System.Windows
Imports System.IO
Public Class Sch_branch_settings
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Load_DefaultValues()
    End Sub
    Private Sub Load_DefaultValues()
        Try
            If Not IsPostBack Then
                Dim M_CmmonBl As New CommonBL
                Dim dt As New DataTable
                dt = M_CmmonBl.Get_BranchDetails()
                If (dt.Rows.Count > 0) Then
                    GVBranch.DataSource = dt
                    'GVBranch.DataTextField = "Br_Name"
                    'GVBranch.DataValueField = "Br_Code"
                    GVBranch.DataBind()
                End If

                Dim dtt As New DataTable
                dtt = M_CmmonBl.GetSchemesloan()
                DrpScheme.DataSource = dtt
                DrpScheme.DataTextField = "Sch_Name"
                DrpScheme.DataValueField = "Sch_Code"
                DrpScheme.DataBind()

                DrpScheme.Items.Add("Select")

                Dim Sch_code As String = DrpScheme.SelectedValue

                Dim M_CommanBL As New CommonBL
                Dim M_Datatable As New DataTable()


            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpScheme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpScheme.SelectedIndexChanged

    End Sub


    Protected Sub chkHeader_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim i As Integer
            Dim ChkAll As CheckBox = DirectCast(sender, CheckBox)
            If GVBranch.Rows.Count > 0 Then
                For i = 0 To GVBranch.Rows.Count - 1
                    Dim Chk As CheckBox = DirectCast(GVBranch.Rows(i).Cells(1).FindControl("chkChild"), CheckBox)
                    If ChkAll.Checked = True Then
                        Chk.Checked = True
                    Else

                        Chk.Checked = False
                    End If
                Next
            End If
        Catch Ex As Exception
            Ex.Message.ToString()
        End Try

    End Sub

    Protected Sub btn_save_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_save.Click

        Try
            Dim dt As New DataTable("Table")
            Dim dtcol As DataColumn
            Dim DtRow As DataRow = dt.NewRow()
            Dim M_CommonBL As New CommonBL
            dtcol = New DataColumn("BrCode", System.Type.[GetType]("System.String"))
            dt.Columns.Add(dtcol)

            If GVBranch.Rows.Count > 0 Then

                For i As Integer = 0 To GVBranch.Rows.Count - 1
                    Dim chkChild As CheckBox = DirectCast(GVBranch.Rows(i).Cells(0).FindControl("chkChild"), CheckBox)
                    If chkChild.Checked = True Then
                        DtRow = dt.NewRow()

                        DtRow(0) = GVBranch.Rows(i).Cells(1).Text
                        dt.Rows.Add(DtRow)
                    End If
                Next
            End If

            Dim M_TranSw As New StringWriter()
            dt.WriteXml(M_TranSw)
            M_CommonBL.Br_Sch_set(DrpScheme.SelectedValue, M_TranSw.ToString().Replace("DocumentElement", "NewDataSet"), Session("TransDate"), Session("UserID").ToString())
            ShowAlertMessage("Saved Successfully")


            GVBranch.DataSource = Nothing
            GVBranch.DataBind()
        Catch Ex As Exception
            Ex.Message.ToString()
        End Try
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub clear()
        DrpScheme.SelectedValue = "Select"

    End Sub

    Protected Sub btn_clear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btn_clear.Click
        clear()
    End Sub
End Class