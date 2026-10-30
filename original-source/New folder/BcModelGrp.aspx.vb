Imports SMARTCORE_BL
Imports SMARTCORE_DA
Public Class BcModelGrp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Load_DefaultValues()
                TxtDate.Text = Session("TransDate")
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Public Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.GetSchemes(CmbLoanScheme, Session("TransDate"), "LOAN")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtLoanNo_TextChanged(sender As Object, e As EventArgs) Handles TxtLoanNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As DataTable
        Dim CustID As String = ""
        Dim M_AccNo As String = TxtLoanNo.Text
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(CmbBranch.SelectedValue, CmbLoanScheme.SelectedValue, M_AccNo, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            HdnAccid.Value = Dt_AcDtls.Rows(0)("Cust_ID").ToString()
            TxtName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
        Else
            ShowAlertMessage("INVALID ACCOUNT NO")
            TxtLoanNo.Text = ""
        End If
    End Sub

    Public Function Validate_Controls() As Boolean
        If Len(TxtGrpId.Text.Trim) = 0 Then
            ShowAlertMessage("INVALID GROUP ID")
            Return False
        End If

        If Len(TxtGrpName.Text.Trim) = 0 Then
            ShowAlertMessage("INVALID GROUP NAME")
            Return False
        End If

        If Len(TxtLoanNo.Text.Trim) = 0 Then
            ShowAlertMessage("INVALID LOAN NO")
            Return False
        End If

    End Function

    Private Sub Locate_Data(ByVal M_BCModelGrp As SMARTCORE_BO.BCModelGrpBO)

        If M_BCModelGrp.BCID = 0 Then
            Session("EntryMode") = "NEW"
            Exit Sub
        End If

        With M_BCModelGrp
            TxtDate.Text = .BCStartDate
            TxtGrpId.Text = .BCGrpID
            TxtLoanNo.Text = .BCLoanAccid
            CmbBranch.SelectedValue = .BrCode
            TxtGrpName.Text = .BCGrpName
            CmbLoanScheme.SelectedValue = .BCLoanSch
        End With
        Session("EntryMode") = "VIEW"

    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Dim M_BCID As Integer
        Dim M_BCModelGrp As New SMARTCORE_BO.BCModelGrpBO
        With M_BCModelGrp
            .BCStartDate = TxtDate.Text
            .BCID = 0
            .BCGrpID = TxtGrpId.Text
            .BCLoanAccid = HdnAccid.Value
            .BrCode = CmbBranch.SelectedValue
            .BCGrpName = TxtGrpName.Text
            .BCLoanSch = CmbLoanScheme.SelectedValue
        End With

        Try
            Dim M_BCModelGrpBL As New BCModelGrpBL
            M_BCID = M_BCModelGrpBL.Save_Data(M_BCModelGrp, "NEW")
            ShowAlertMessage("BC ID : " & M_BCID)
            M_BCModelGrpBL = Nothing
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

    Private Sub Clear_Controls()

        TxtGrpName.Text = ""
        TxtLoanNo.Text = ""
        TxtName.Text = ""
        TxtAddress.Text = ""

    End Sub

    Protected Sub TxtGrpId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtGrpId.TextChanged


        If TxtGrpId.Text.Length = 0 Then
            Call Clear_Controls()
        Else
            Try
                Dim M_BCModelGrp As New SMARTCORE_BO.BCModelGrpBO
                Dim M_BCModelGrpBL As New BCModelGrpBL
                M_BCModelGrp = M_BCModelGrpBL.Locate_Data(CmbBranch.SelectedValue, TxtGrpId.Text)
                Locate_Data(M_BCModelGrp)
                M_BCModelGrpBL = Nothing
            Catch ex As Exception

            End Try
        End If


    End Sub

End Class