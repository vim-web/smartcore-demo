Imports SMARTCORE_BL
Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Public Class BranchAreaMapping
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
                DrpRegion.Items.Insert(0, "SELECT")

                BindBranchGrid()
            End If


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub BindBranchGrid()
        Try
            Dim M_Dt As New DataTable
            Dim M_CommonBl As New CommonBL
            M_Dt = M_CommonBl.BranchDetails()
            GvBranchDetails.DataSource = M_Dt
            GvBranchDetails.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub Defaultvalue()
        Try
            Dim DtTab As New DataTable()
            DtTab.Columns.Add("Br_Code")
            DtTab.Columns.Add("Br_Name")
            DtTab.Columns.Add("Br_Adderess")
            DtTab.Columns.Add("Br_Phone1")
            DtTab.Columns.Add("Br_Area")
            DtTab.Columns.Add("Br_Region")
            DtTab.Rows.Add("", "", "", "", "", "")
            GvUser.DataSource = DtTab
            GvUser.DataBind()
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

    Private Function GvUser() As Object
        Throw New NotImplementedException
    End Function



    Private Function sender() As Object
        Throw New NotImplementedException
    End Function

    Protected Sub DrpRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpRegion.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Get_Area(DrpArea, DrpRegion.SelectedValue)
            Return
        Catch ex As Exception
            Return
        End Try
    End Sub

    Private Function RegionCode() As String
        Throw New NotImplementedException
    End Function

    Protected Sub CmdSave_Click(sender As Object, e As ImageClickEventArgs) Handles CmdSave.Click

        Try
            Dim Dt_BranchArea As New DataTable
            Dt_BranchArea.TableName = "BranchArea"
            Dt_BranchArea.Columns.Add("BranchCode")
            Dim DtRow As DataRow
            For i As Integer = 0 To GvBranchDetails.Rows.Count - 1

                Dim ChkView As CheckBox = DirectCast(GvBranchDetails.Rows(i).Cells(0).FindControl("ChkView"), CheckBox)
                If ChkView.Checked Then
                    DtRow = Dt_BranchArea.NewRow()
                    DtRow(0) = GvBranchDetails.Rows(i).Cells(1).Text
                    Dt_BranchArea.Rows.Add(DtRow)
                End If
            Next
            Dim AreaMapping As New System.IO.StringWriter()
            Dt_BranchArea.WriteXml(AreaMapping)
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            M_Dt = M_CommonBl.BranchAreaMapping(DrpRegion.SelectedValue, DrpArea.Text, AreaMapping.ToString())

            If M_Dt.Rows.Count > 0 Then
                ShowAlertMessage("Saved Successfully")
                BindBranchGrid()
            Else
                ShowAlertMessage("Updation Failed...")
            End If



        Catch ex As Exception
        End Try
    End Sub

End Class