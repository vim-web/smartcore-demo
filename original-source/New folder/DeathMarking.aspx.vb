Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports SMARTCORE_DAL

Partial Public Class DeathMarking
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1054
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            LoaddefaultValue()
            Session("M_EntryMode") = "NEW"
            Session("SearchType") = "CUSTOMER"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If
        If Session("SearchResult") <> "" Then
            Txt_custid.Text = Session("SearchResult")
            Txt_custid_TextChanged(Me.Txt_custid, System.EventArgs.Empty)
            Txt_custid.Focus()
            Session("SearchResult") = ""

        End If
    End Sub
    Private Sub LoaddefaultValue()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(CmbBranch)
        Cmbbranch.SelectedValue = Session("DefaultBrCode")
        Txt_Address.Enabled = False
        Txt_Name.Enabled = False
        Cmbbranch.Enabled = False
    End Sub


    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Dim M_DeathMarking As New SMARTCORE_BO.DeathMarkingBO        With M_DeathMarking

            .BrCode = CmbBranch.SelectedValue
            .Custid = Txt_custid.Text
            .deathdate = TxtDeathDate.Text
            .Intimationdate = Txt_Intimationdate.Text
            .Custname = Txt_Name.Text
            .Custaddress = Txt_Address.Text
            .Description = Txt_Description.Text

        End With        Try            Dim M_BrCode As String            Dim M_DeathMarkingBL As New DeathMarkingBL            M_BrCode = M_DeathMarkingBL.Save_Data(M_DeathMarking, Session("M_EntryMode"))            M_DeathMarkingBL = Nothing            ShowAlertMessage("Saved Successfully")        Catch ex As Exception            ShowAlertMessage("Saved Failed")        End Try    End Sub


    Protected Sub Txt_custid_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_custid.TextChanged
        Try            Dim M_CommonBL As New DeathMarkingBL
            Dim dt As New DataTable
            dt = M_CommonBL.Locate_Data(Txt_custid.Text)
            If dt.Rows.Count > 0 Then
                Cmbbranch.SelectedIndex = dt.Rows(0)("Br_Code").ToString()
                Txt_Name.Text = dt.Rows(0)("Cust_Name").ToString()
                Txt_Address.Text = dt.Rows(0)("Address").ToString()
            End If
            M_CommonBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Private Sub Locate()        Try
                Dim dt As New DataTable
            Dim M_commonBl As New DeathMarkingBL
            dt = M_commonBl.Locate_Data(Txt_custid.Text)
            CmbBranch.SelectedValue = dt.Rows(0)("Br_Code").ToString()
            Txt_Name.Text = dt.Rows(0)("Cust_Name").ToString()
            Txt_Address.Text = dt.Rows(0)("Address").ToString()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Clear_Controls()
        Txt_Name.Text = ""
        Txt_Address.Text = ""
        Txt_Description.Text = ""
        Txt_Intimationdate.Text = ""
        TxtDeathDate.Text = ""
        Txt_custid.Text = ""
    End Sub

    Protected Sub CmdClear0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear0.Click
        Clear_Controls()
    End Sub

    Private Sub ValidateControls()

    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class