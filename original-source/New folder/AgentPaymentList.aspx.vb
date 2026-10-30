Imports SMARTCORE_BL
Public Class AgentPaymentList
    Inherits System.Web.UI.Page
    Dim M_CommonBl As New CommonBL
    Dim FormID As Integer = 1008
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            Load_DefaultValues()
        End If
    End Sub

    Private Function Load_DefaultValues()
        Try
            M_CommonBl.Fill_Branch(CmbBranch)
            M_CommonBl.GetSchemes(CmbScheme, Session("TransDate"), "DEPOSIT", "DD")
            M_CommonBl.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, CmbBranch.SelectedValue)
            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
            TxtFrDate.Text = Session("TransDate")
            TxtToDate.Text = Session("TransDate")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Function

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
        CmbScheme.SelectedValue = TxtScheme.Text
    End Sub

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim M_Dt As DataTable = New DataTable()


            Dim HeaderArr(8, 4) As String
            Dim HeaderWidth(8, 1) As String

            If CmbAgent.Text = "All" Then
                HeaderArr(0, 0) = "AGENT NAME"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "50"
                HeaderArr(0, 3) = "50"

                HeaderArr(1, 0) = "PAYMENT"
                HeaderArr(1, 1) = "R"
                HeaderArr(1, 2) = "15"
                HeaderArr(1, 3) = "15"
            Else
                HeaderArr(0, 0) = "SLNO"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "5"
                HeaderArr(0, 3) = "5"

                HeaderArr(1, 0) = "DATE"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "15"
                HeaderArr(1, 3) = "15"

                HeaderArr(2, 0) = "ACCNO"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "15"
                HeaderArr(2, 3) = "15"

                HeaderArr(3, 0) = "PAYMENT"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "15"
                HeaderArr(3, 3) = "15"

            End If

            Dim DtFrom As Date
            Dim DtTo As Date
            DtFrom = TxtFrDate.Text
            DtTo = TxtToDate.Text

            Session("RptHeader") = HeaderArr
            M_Sql = "Exec AgentAccPayment_Sp '" & CmbBranch.SelectedValue & "','" & CmbScheme.SelectedValue & "','" & Format(DtFrom, "MM/dd/yyyy") & "','" & Format(DtTo, "MM/dd/yyyy") & "'," & IIf(CmbAgent.Text = "All", 0, CmbAgent.SelectedValue)
            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBranch.SelectedIndexChanged
        M_CommonBl.Fill_Agent(CmbAgent, CommonBL.AgentType.DDAgent, CmbBranch.SelectedValue)
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class