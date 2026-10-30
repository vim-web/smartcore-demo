Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.Data

Public Class BranchDetailsUpdation
    Inherits System.Web.UI.Page
    Shared SchemeDt As DataTable
    Shared AcHeadDt As DataTable
    Shared Dt_AcDtls As DataTable
    'Dim FormID As Integer = 2122
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Session("SearchType") = "Branch"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            'Load_DefaultValues()
            GetBranchCode()
        End If
        If Session("SearchResult") <> "" Then
            txt_BrCode.Text = ""
            txt_BrCode.Text = Session("SearchResult")
            txt_BrCode_TextChanged(Me.txt_BrCode, System.EventArgs.Empty)
            txt_BrCode.Focus()
            Session("SearchResult") = ""

        End If


    End Sub

    'Private Sub Load_DefaultValues()
    '    Dim M_CommonBL As New CommonBL
    '    ' M_CommonBL.Fill_Pickup(drp_Dist, 28, 454)
    '    'M_CommonBL.Fill_Pickup(drp_Country, 47, 0)
    '    ''M_CommonBL.Fill_Pickup(drp_State, 48, 0)

    '    'M_CommonBL.Fill_State(drp_State)
    '    'M_CommonBL.Fill_District(drp_Dist, drp_State.SelectedValue.ToString())

    '    drp_Country.Items.Insert(0, "--Select--")
    '    drp_State.Items.Insert(0, "--Select--")
    '    drp_Dist.Items.Insert(0, "--Select--")

    '    If drp_Country.SelectedIndex = 0 Then
    '        drp_State.SelectedIndex = 0
    '        drp_Dist.SelectedIndex = 0
    '    End If
    'End Sub
   
    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Try
            Dim S_CommanBL As New CommonBL
            Dim dtBranch As New DataTable()
            'Dim Country As String = drp_Country.SelectedValue.ToString()
            'Dim State As String = drp_State.SelectedValue.ToString()
            'Dim District As String = drp_Dist.SelectedItem.Text
            'Dim DistrictCode As String = drp_Dist.SelectedValue.ToString()
            Dim BrCode As String = txt_BrCode.Text
            Dim BrName As String = txt_Brname.Text
            Dim BrAddress As String = txt_Address.Text
            'Dim Area As String = drp_Area.SelectedValue.ToString()
            Dim Phone1 As String = txt_phone1.Text
            Dim Phone2 As String = txt_phone2.Text
            Dim EmailId As String = txt_EmailID.Text
            'Dim FaxNo As String = txt_FaxNo.Text
            'Dim WeeklyOff As String = drp_WeeklyOff.SelectedValue
            'Dim AccCode As String = txt_AccCode.Text
            'Dim BlockName As String = txt_BlockName.Text
            dtBranch = S_CommanBL.SaveBranchDetails(BrCode, BrName, BrAddress, Phone1, Phone2, EmailId)
            ShowAlertMessage("Saved Sucessfully")

            GetBranchCode()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click

        Try

            ' txt_DistCode.Text = ""
            txt_BrCode.Text = ""
            txt_Brname.Text = ""
            txt_Address.Text = ""
            'drp_Area.ClearSelection()
            txt_phone1.Text = ""
            txt_phone2.Text = ""
            txt_EmailID.Text = ""
            'txt_FaxNo.Text = ""
            'drp_WeeklyOff.ClearSelection()
            'drp_Country.ClearSelection()
            'drp_State.ClearSelection()
            'drp_Dist.ClearSelection()
            'txt_AccCode.Text = ""
            'txt_BlockName.Text = ""
            'Load_DefaultValues()
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click

    End Sub

    Protected Sub txt_BrCode_TextChanged(sender As Object, e As EventArgs) Handles txt_BrCode.TextChanged

        Try
            Dim dtFillBranch As New DataTable()
            Dim S_CommanBL As New CommonBL
            dtFillBranch = S_CommanBL.Fill_BranchDetails(txt_BrCode.Text)
            If dtFillBranch.Rows.Count > 0 Then
                txt_BrCode.Text = dtFillBranch.Rows(0)("Br_Code").ToString()
                txt_Brname.Text = dtFillBranch.Rows(0)("Br_Name").ToString().Trim()
                txt_Address.Text = dtFillBranch.Rows(0)("Br_Address").ToString().Trim()
                txt_phone1.Text = dtFillBranch.Rows(0)("Br_Phone1").ToString()
                txt_phone2.Text = dtFillBranch.Rows(0)("Br_Phone2").ToString()
                txt_EmailID.Text = dtFillBranch.Rows(0)("Br_Email").ToString()
                'txt_FaxNo.Text = dtFillBranch.Rows(0)("Br_FaxNo").ToString()
                'drp_WeeklyOff.SelectedValue = dtFillBranch.Rows(0)("Br_WeeklyOff").ToString().Trim()
                'txt_AccCode.Text = dtFillBranch.Rows(0)("Br_AccCode").ToString()
                'txt_BlockName.Text = dtFillBranch.Rows(0)("Block_Name").ToString()

                'txt_DistCode.Text = dtFillBranch.Rows(0)("District_Code").ToString()
                'drp_Area.SelectedValue = dtFillBranch.Rows(0)("Br_Area").ToString()

                'drp_Country.SelectedValue = dtFillBranch.Rows(0)("Br_DefCountry").ToString()
                'drp_State.SelectedValue = dtFillBranch.Rows(0)("Br_DefState").ToString()
                'Call drp_State_SelectedIndexChanged(sender, e)
                'drp_Dist.SelectedValue = dtFillBranch.Rows(0)("District_Code").ToString()


            End If
        Catch ex As Exception

        End Try


    End Sub
    Private Sub GetBranchCode()
        Try
            Dim DtBrCode As New DataTable()
            Dim S_CommanBL As New CommonBL
            DtBrCode = S_CommanBL.GetBranchCode()
            If DtBrCode.Rows.Count > 0 Then
                txt_BrCode.Text = DtBrCode.Rows(0)("BranchCode").ToString
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

End Class