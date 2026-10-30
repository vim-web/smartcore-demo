Imports SMARTCORE_BL

Partial Public Class LoanODIntPosting
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1092
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


            Load_DefaultValues()
            CmbBranch.Attributes.Add("readonly", "readonly")
            TxtBrCode.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN", "OD")

            HdnTrDate.Value = Session("TransDate")

            TxtFromDate.Text = HdnTrDate.Value
            TxtToDate.Text = HdnTrDate.Value
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            TxtBrCode.Text = Session("DefaultBrCode")
            TxtSchCode.Text = CmbScheme.SelectedValue
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdReport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdReport.Click
        Try
            Dim M_LoanBL As New LoanMasterBL

            Dim StrResult As String = ""
            M_LoanBL.PostLoanInterest(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtFromDate.Text, TxtToDate.Text, HdnTrDate.Value, TxtAccNO.Text, False, True, Session("UserID"), StrResult)

            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String
            HeaderArr(0, 0) = "ACCOUNT NO    "
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "11"

            HeaderArr(1, 0) = "NAME       "
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "30"
            HeaderArr(1, 3) = "29"

            HeaderArr(2, 0) = "OUTSTANDING"
            HeaderArr(2, 1) = "R"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "DEBIT INTEREST"
            HeaderArr(3, 1) = "R"
            HeaderArr(3, 2) = "20"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "CREDIT INTEREST"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "13"

            Session("RptHeader") = HeaderArr

            Session("RptQry") = StrResult
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim M_LoanBL As New LoanMasterBL
            Dim M_Ds As New DataSet
            M_Ds = M_LoanBL.PostLoanInterest(CmbBranch.SelectedValue, CmbScheme.SelectedValue, TxtFromDate.Text, TxtToDate.Text, HdnTrDate.Value, TxtAccNO.Text, True, False, Session("UserID"))

            If M_Ds.Tables.Count > 0 Then
                If M_Ds.Tables(0).Rows.Count > 0 Then
                    Dim Tran_No As String = M_Ds.Tables(0).Rows(0)("Tran_No")
                    Response.Redirect("~/NewAccount.aspx?AccNo=" & CmbScheme.SelectedItem.Text & "&TransId=" & Tran_No & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & CmbScheme.SelectedValue & "&SEC=" & "LOAN" & "&DRCR=C" & "&TRANCATG=" & "TRAN")
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            TxtSchCode.Text = CmbScheme.SelectedValue
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtSchCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSchCode.TextChanged
        Try
            CmbScheme.SelectedValue = TxtSchCode.Text
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class