Imports SMARTCORE_BO
Imports SMARTCORE_BL
Imports SMARTCORE_Common
Imports System.IO
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Partial Public Class AssetMaster
    Inherits System.Web.UI.Page
    Private M_MasterType As String
    Dim FormID As Integer = 1016
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Clear_Values()
            Load_Default()
            TxtAssetCode.Focus()

            Session("SearchType") = "ASSET"
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
        End If
        If Session("SearchResult") <> "" Then
            If Session("SearchType") = "ASSET" Then
                TxtAssetCode.Text = Session("SearchResult")
                TxtAssetCode_TextChanged(Me.TxtAssetCode, System.EventArgs.Empty)
            End If
            Session("SearchResult") = ""
        End If
    End Sub


    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear_Values()
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
    Private Sub Clear_Values()
        Dim M_Dt As New DataTable
        Dim M_FAItemBL As New FAItemBL

        Session("M_EntryMode") = "NEW"
        TxtAssetName.Text = ""
        TxtAssetDepPerc.Text = ""
        TxtAssetOpValue.Text = ""
        TxtAssOpQty.Text = ""
        TxtOpenDate.Text = Session("TransDate")
        M_Dt = M_FAItemBL.Get_AssetCode()
        ''TxtAssetCode.Text = M_Dt.Rows(0).Item("Next_AssetCode").ToString()
    End Sub
    Private Sub Load_Default()
        Try
            Dim M_FAItemBL As New FAItemBL
            Dim M_Dt As DataTable
            Dim M_CommonBl As New CommonBL
            With M_FAItemBL
                .Fill_Group(CmbAssetGroup)
                .Fill_AcHead(CmbAssetHead, "A")
                .Fill_AcHead(CmbAssetDepHead, "D")
           
            End With

            M_Dt = M_FAItemBL.Get_AssetCode()
            TxtAssetCode.Text = M_Dt.Rows(0).Item("Next_AssetCode").ToString()
            ''Val(Dt_AcDtls.Rows(0)("Pending_CrAmt").ToString()
            TxtOpenDate.Text = Session("TransDate")
            M_CommonBl.Fill_Branch(DrpBranch, False)

            DrpBranch.SelectedValue = Session("DefaultBrCode")
            Session("SearchBranch") = Session("DefaultBrCode")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        SaveAsset()
        Load_Default()
        Clear_Values()
        TxtAssetCode.Focus()
    End Sub
    Private Sub SaveAsset()

        Dim M_Items As New SMARTCORE_BO.FAItemBO
        Dim M_FAItemCode As String
        With M_Items
            .FAItemOpStockDate = TxtOpenDate.Text
            .FAItemDepPercentage = Val(TxtAssetDepPerc.Text)
            .FAItemOpStockQty = Val(TxtAssOpQty.Text)
            .FAItemOpStockVal = Val(TxtAssetOpValue.Text)
            .FAItemCode = TxtAssetCode.Text
            .FAItemName = TxtAssetName.Text
            .FAItemAcHead = CmbAssetHead.SelectedValue
            .FAItemDepHead = CmbAssetDepHead.SelectedValue
            .FAItemGroupCode = CmbAssetGroup.SelectedValue
            .FAItemBranchCode = DrpBranch.SelectedValue
        End With

        Try
            Dim M_FAItemBL As New FAItemBL
            M_FAItemCode = M_FAItemBL.Save_Data(Session("M_EntryMode"), M_Items)
            'MsgBox("Asset Code : " & M_FAItemCode)
            ShowAlertMessage("Saved Successfully")
            M_FAItemBL = Nothing
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

    Protected Sub TxtAssetCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtAssetCode.TextChanged
        If TxtAssetCode.Text.Length = 0 Then
            Call Clear_Values()
        Else
            Try
                Dim M_Item As New SMARTCORE_BO.FAItemBO
                Dim M_FAItemBL As New FAItemBL
                Dim M_ItmCode As String
                M_ItmCode = TxtAssetCode.Text
                Clear_Values()

                M_Item = M_FAItemBL.Locate_Data(M_ItmCode)
                Locate_AssetData(M_Item)
                M_FAItemBL = Nothing
            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Locate_AssetData(ByVal M_Item As SMARTCORE_BO.FAItemBO)

        If M_Item.FAItemCode = 0 Then

            Session("M_EntryMode") = "NEW"
            Exit Sub
        End If
        With M_Item
            TxtAssetDepPerc.Text = .FAItemDepPercentage
            TxtAssOpQty.Text = .FAItemOpStockQty
            TxtAssetOpValue.Text = .FAItemOpStockVal
            TxtAssetName.Text = .FAItemName
            CmbAssetHead.SelectedValue = .FAItemAcHead
            CmbAssetDepHead.SelectedValue = .FAItemDepHead
            CmbAssetGroup.SelectedValue = .FAItemGroupCode
            DrpBranch.SelectedValue = .FAItemBranchCode
            TxtOpenDate.Text = .FAItemOpStockDate
        End With
        Session("M_EntryMode") = "VIEW"

    End Sub

    Protected Sub DrpBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpBranch.SelectedIndexChanged
        Session("SearchBranch") = DrpBranch.SelectedValue
    End Sub
End Class

