Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class ChittyPaymentList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If

            If Not IsPostBack Then
                Load_DefaultValues()
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Load_DefaultValues()
        Try
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            ' CmbBranch.Items.Add(" -- ALL --")
            M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), ("GROUP DEPOSIT"))

            HdnTrDate.Value = Session("TransDate")

            TxtFromDate.Text = HdnTrDate.Value

            CmbBranch.SelectedValue = Session("DefaultBrCode")

            CmbBranch_SelectedIndexChanged(Me.CmbBranch, System.EventArgs.Empty)


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

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        Try
            Dim M_ChittyGroupBL As New ChittyGroupBL
            M_ChittyGroupBL.Get_ChittyGrp(CmbChittyGrp, CmbBranch.SelectedValue, CmbScheme.SelectedValue)
            ' CmbChittyGrp.Items.Add(" -- ALL --")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        Try
            CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdReport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdReport.Click
        Try
            Dim CommonCls As New CommonClass

            Dim StrResult As String = ""
            Dim ChittyGrp As String = "%"
            Dim Br As String = "%"
            Dim Sch As String = "%"

            If CmbChittyGrp.SelectedItem.Text <> " -- ALL --" Then
                ChittyGrp = CmbChittyGrp.SelectedValue
            End If

            If CmbBranch.SelectedItem.Text <> " -- ALL --" Then
                Br = CmbBranch.SelectedValue
            End If

            If CmbScheme.SelectedItem.Text <> " -- ALL --" Then
                Sch = CmbScheme.SelectedValue
            End If
            '--Exec [Chitty_Payment_List_Sp] '7','37','98','08/22/2013','sa'
            StrResult = "Exec Chitty_Payment_List_Sp '" & Br & "','" & Sch & "','" & ChittyGrp & _
                "'," & CommonCls.DbDate(TxtFromDate.Text) & ",'" & Session("UserID") & "'"

            Dim HeaderArr(9, 4) As String
            Dim HeaderWidth(9, 1) As String
            HeaderArr(0, 0) = "SL No "
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "10"
            HeaderArr(0, 3) = "3"

            HeaderArr(1, 0) = "Name "
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "30"
            HeaderArr(1, 3) = "25"

            HeaderArr(2, 0) = "Account No      "
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "20"
            HeaderArr(2, 3) = "13"

            HeaderArr(3, 0) = "Date      "
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "20"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "AMOUNT "
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "20"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "Narration  "
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "20"
            HeaderArr(5, 3) = "15"

        
            Session("RptHeader") = HeaderArr

            Session("RptQry") = StrResult
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

            CommonCls = Nothing

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class