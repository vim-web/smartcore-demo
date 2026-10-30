
Imports SMARTCORE_Common
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class LockerInventory
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1034
    Shared Obj_UserRights As Object
    Dim M_CommonBl As New LockerInventoryBL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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
                LoadDefaultValues()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadDefaultValues()
        Try
            Dim M_commonBl As New CommonBL
            M_commonBl.Fill_Branch(CmbBranch)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnSave.Click
        Dim M_LockerInventory As New SMARTCORE_BO.LockerInventoryBO        Dim M_LockerInventoryBL As New LockerInventoryBL        Dim M_BrCode As Integer        Dim dt As New DataTable        Dim LkrNo As String        Dim KeyNo As String
        With M_LockerInventory

            .LriCode = CmbLricode.SelectedValue
            .LriRowNo = TxtRowNo.Text
            .LriColNo = TxtColNo.Text
            .LogID = "0"
            .MakingTime = Session("TransDate")
            .CheckingTime = "1900-01-01"
            .BrCode = CmbBranch.SelectedValue
            .LriLockerNo = TxtLockerNo.Text
            .LriLockerKeyNo = TxtkeyNo.Text
            .MakerID = Session("LoginId")
            .CheckerID = "0"
            .LriStatus = "O"

        End With


        dt = M_CommonBl.CheckLockerNo(TxtLockerNo.Text, CmbBranch.SelectedValue, CmbLricode.SelectedValue)
        If dt.Rows.Count > 0 Then
            LkrNo = dt.Rows(0)(0).ToString()
            ShowAlertMessage("LockerNo '" & LkrNo & "' Already added. Please Check!")
            Exit Sub
        Else
            dt = M_CommonBl.CheckLockerKeyNo(TxtkeyNo.Text, CmbBranch.SelectedValue, CmbLricode.SelectedValue)
            If dt.Rows.Count > 0 Then
                KeyNo = dt.Rows(0)(0).ToString()
                ShowAlertMessage("LockerKeyNo '" & KeyNo & "' Already added. Please Check!")
                Exit Sub
            Else                Try                    M_BrCode = M_LockerInventoryBL.Save_Data(M_LockerInventory, "NEW")                    ShowAlertMessage("Saved Sucessfully")                    Clear()                    M_LockerInventoryBL = Nothing                Catch ex As Exception                    ShowAlertMessage("Saving Failed")                End Try            End If        End If    End Sub    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Clear()
    End Sub
    Private Sub Clear()
        TxtColNo.Text = ""
        TxtkeyNo.Text = ""
        TxtLockerNo.Text = ""
        TxtRowNo.Text = ""
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Dim M_Locker As New LockerInventoryBL
        M_Locker.GetLocker(CmbLricode, CmbBranch.SelectedValue)
    End Sub
    'Private Sub CheckLockerNo()
    '    Dim dt As New DataTable
    '    Dim LkrNo As String
    '    Try
    '        dt = M_CommonBl.CheckLockerNo(TxtLockerNo.Text, CmbBranch.SelectedValue, CmbLricode.SelectedValue)
    '        LkrNo = dt.Rows(0)(0).ToString()
    '        ShowAlertMessage("LockerNo '" & LkrNo & "' already added! Please Check")
    '        Exit Sub
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub CheckLockerkeyNo()
    '    Dim dt As New DataTable
    '    Dim KeyNo As String
    '    Try
    '        dt = M_CommonBl.CheckLockerKeyNo(TxtkeyNo.Text, CmbBranch.SelectedValue, CmbLricode.SelectedValue)
    '        KeyNo = dt.Rows(0)(0).ToString()
    '        ShowAlertMessage("LockerKeyNo '" & KeyNo & "' already added! Please Check")
    '        Exit Sub
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class