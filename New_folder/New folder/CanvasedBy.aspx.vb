Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BL
Partial Public Class CanvasedBy
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'Load_DefaultValues()
                'Set_Grid()
                Session("SearchType") = "CUSTOMER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                Dim M_CommonBl As New CommonBL
                M_CommonBl.Fill_Branch(DrpBranch)
                DrpBranch.Items.Add("ALL")
                DrpStatus.Items.Add("Active")
                DrpStatus.Items.Add("DeActive")
            End If
            If Session("SearchResult") <> "" Then
                Txt_CustId.Text = Session("SearchResult")
                Txt_CustId_TextChanged(Me.Txt_CustId, System.EventArgs.Empty)
                Session("SearchResult") = ""
                ''cust_grid()
                


            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_CommonBl As New CommonBL
            Dim M_Dt As New DataTable
            Dim EntryMode As String

            M_Dt = M_CommonBl.GetCanvasedBy(Txt_CustId.Text)
            If M_Dt.Rows.Count > 0 Then
                EntryMode = "Update"
            Else
                EntryMode = "New"
            End If
            M_Dt = M_CommonBl.InsertUpdateCanvased(EntryMode, Txt_CustId.Text, DrpBranch.SelectedValue, Txt_subdate.Text, IIf(DrpStatus.SelectedValue = "Active", "A", "D"), Session("UserID"))
            ShowAlertMessage(M_Dt.Rows(0)(0))
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub Txt_CustId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_CustId.TextChanged
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL
            M_CustMaster = M_CustMasterBL.Locate_Data(Txt_CustId.Text, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
            With M_CustMaster


                Txt_CustName.Text = .CustName
                Txt_CustAddress.Text = M_PermAddress.AddAddress1 + "," + M_PermAddress.AddAddress2 + "," + M_PermAddress.AddAddress3 + "," + M_PermAddress.AddPostoffice + "," + M_PermAddress.AddPinCode
                DrpBranch.SelectedValue = M_CustMaster.BrCode
            End With
            'CmdShow_Click(Me.CmdShow, System.EventArgs.Empty)

            M_CustMaster = Nothing
            M_PresAddress = Nothing
            M_PermAddress = Nothing
            M_CustMasterBL = Nothing
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
End Class