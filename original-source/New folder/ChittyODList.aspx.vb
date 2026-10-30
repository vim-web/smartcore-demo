Imports SMARTCORE_BL
Public Class ChittyODList
    Inherits System.Web.UI.Page
    Dim M_CommonBl As New CommonBL
    Dim FormID As Integer = 1030
    Shared Obj_UserRights As Object



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If

            Load_DefaultValues()
            TxtFrDate.Text = Session("TransDate")
            CmbBranch.SelectedValue = Session("DefaultBrCode")
            CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
        End If
    End Sub
    Private Function Load_DefaultValues()
        Try
            M_CommonBl.Fill_Branch(CmbBranch)
            M_CommonBl.GetSchemes(CmbScheme, Session("TransDate"), ("GROUP DEPOSIT"))

            Call CmbScheme_SelectedIndexChanged(Me.CmbScheme, System.EventArgs.Empty)
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

    Protected Sub TxtScheme_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtScheme.TextChanged
        CmbScheme.SelectedValue = TxtScheme.Text
        TxtScheme.Text = CmbScheme.SelectedValue
    End Sub

    Protected Sub CmbScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbScheme.SelectedIndexChanged
        TxtScheme.Text = CmbScheme.SelectedValue
        CmbScheme.SelectedValue = TxtScheme.Text
        Try
            Dim M_ChittyGroupBL As New ChittyGroupBL
            M_ChittyGroupBL.Get_ChittyGrp(CmbChitty, CmbBranch.SelectedValue, CmbScheme.SelectedValue)
            CmbChitty.Items.Add("All")

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim M_Dt As DataTable = New DataTable()


            Dim HeaderArr(8, 4) As String
            Dim HeaderWidth(8, 1) As String

            HeaderArr(0, 0) = "ACC NO"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "13"
            HeaderArr(0, 3) = "13"


            HeaderArr(1, 0) = "MEM NO"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "13"
            HeaderArr(1, 3) = "13"

            HeaderArr(2, 0) = "CUST NAME"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "40"
            HeaderArr(2, 3) = "40"


            HeaderArr(3, 0) = "PHONE"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "14"
            HeaderArr(3, 3) = "14"

            HeaderArr(4, 0) = "PAID INST"
            HeaderArr(4, 1) = "R"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"

            HeaderArr(5, 0) = "OD INST"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "10"


            HeaderArr(6, 0) = "BALANCE"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "11"
            HeaderArr(6, 3) = "11"

            HeaderArr(7, 0) = "  "
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "2"
            HeaderArr(7, 3) = "2"

            Dim DtFrom As Date
            DtFrom = TxtFrDate.Text

            Session("RptHeader") = HeaderArr
            M_Sql = "Exec ChittyODList_Sp '" & CmbBranch.SelectedValue & "','" & CmbChitty.SelectedValue & "','" & Format(DtFrom, "MM/dd/yyyy") & "'"
            Session("RptQry") = M_Sql

            Response.Redirect("~/PrnView.aspx?PageType=80", False)
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

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class