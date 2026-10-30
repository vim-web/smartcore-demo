
Imports SMARTCORE_BL
Imports SMARTCORE_Common.CommonClass
Partial Public Class memberornamentlist
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1086
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If


                Dim M_CommonBL As New CommonBL
                M_CommonBL.Fill_Branch(DdlBranch)
                M_CommonBL.GetSchemes(DdlScheme, Session("TransDate"), "share")
                TxtDate.Attributes.Add("readonly", "readonly")
                TxtCustName.Attributes.Add("readonly", "readonly")
                TxtAddress.Attributes.Add("readonly", "readonly")
                TxtDate.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim Dt_AcDtls As DataTable
        Dt_AcDtls = M_CommonBL.GetAccountTranDtls(DdlBranch.SelectedValue, DdlScheme.SelectedValue, TxtAccNo.Text, Session("TransDate"))
        If Dt_AcDtls.Rows.Count > 0 Then
            TxtCustName.Text = Dt_AcDtls.Rows(0)("Cust_Name").ToString()
            TxtAddress.Text = Dt_AcDtls.Rows(0)("Cust_Address").ToString()
            HdnAccid.Value = Dt_AcDtls.Rows(0)("Acc_id").ToString()
        End If
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim HeaderArr(9, 4) As String


        HeaderArr(0, 0) = "AccNo."
        HeaderArr(0, 1) = "R"
        HeaderArr(0, 2) = "10"
        HeaderArr(0, 3) = "13"

        HeaderArr(1, 0) = "Br.Name"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "10"
        HeaderArr(1, 3) = "13"

        HeaderArr(2, 0) = "LoanDate"
        HeaderArr(2, 1) = "R"
        HeaderArr(2, 2) = "12"
        HeaderArr(2, 3) = "13"

        HeaderArr(3, 0) = "LoanAmount"
        HeaderArr(3, 1) = "R"
        HeaderArr(3, 2) = "12"
        HeaderArr(3, 3) = "13"

        HeaderArr(4, 0) = "ClosedDate"
        HeaderArr(4, 1) = "R"
        HeaderArr(4, 2) = "12"
        HeaderArr(4, 3) = "13"


        HeaderArr(5, 0) = "OrnamentList"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "12"
        HeaderArr(5, 3) = "13"

        HeaderArr(6, 0) = "Remarks"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "12"
        HeaderArr(6, 3) = "13"

        HeaderArr(7, 0) = "NetWt"
        HeaderArr(7, 1) = "R"
        HeaderArr(7, 2) = "13"
        HeaderArr(7, 3) = "13"

        HeaderArr(8, 0) = "Grosswt"
        HeaderArr(8, 1) = "R"
        HeaderArr(8, 2) = "13"
        HeaderArr(8, 3) = "13"

        HeaderArr(9, 0) = "status"
        HeaderArr(9, 1) = "L"
        HeaderArr(9, 2) = "8"
        HeaderArr(9, 3) = "13"

        Session("RptHeader") = HeaderArr
        Dim Dt As Date
        Dim M_Sql As String
        Dt = TxtDate.Text
        M_Sql = "Exec [MemberWiseGoldList_Sp] '" & TxtAccNo.Text & "','" & Format(Dt, "MM/dd/yyyy") & "','','" & Session("HostID") & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class