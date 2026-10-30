Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_Common


Public Class IncentiveCanvasserDetails_Rpt
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1011
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If

            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If
            If Not IsPostBack Then
                Dim M_CommonBl As New CommonBL
                Session("SearchType") = "CANVASEDBY"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
                M_CommonBl.Fill_CanvassedBy(ID)
                TxtFromDate.Text = Session("TransDate")
                TxtTodate.Text = Session("TransDate")
            End If
            If Session("SearchResult") <> "" Then
                Txt_Canvasserid.Text = Session("SearchResult")
                Txt_Canvasserid_TextChanged(Me.Txt_Canvasserid, System.EventArgs.Empty)
                Session("SearchResult") = ""
                ''cust_grid()             


            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        Dim ClsComm As New CommonClass()

        Dim HeaderArr(14, 4) As String
        Dim HeaderWidth(14, 1) As String
        HeaderArr(0, 0) = "SLN0"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "4"
        HeaderArr(0, 3) = "4"


        HeaderArr(1, 0) = "Name of the cavaser"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "10"
        HeaderArr(1, 3) = "10"

        HeaderArr(2, 0) = "Canvaser ID"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "13"
        HeaderArr(2, 3) = "13"



        HeaderArr(3, 0) = "Month"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "15"
        HeaderArr(3, 3) = "15"

        HeaderArr(4, 0) = "Deposit Number"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "15"
        HeaderArr(4, 3) = "15"


        HeaderArr(5, 0) = "Deposit Holder Name"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "10"

        HeaderArr(6, 0) = "Type of Deposit"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "10"
        HeaderArr(6, 3) = "10"

        HeaderArr(7, 0) = "Deposit Amount"
        HeaderArr(7, 1) = "L"
        HeaderArr(7, 2) = "15"
        HeaderArr(7, 3) = "15"

        HeaderArr(8, 0) = "Closed Amount"
        HeaderArr(8, 1) = "L"
        HeaderArr(8, 2) = "15"
        HeaderArr(8, 3) = "15"

        HeaderArr(9, 0) = "Current Deposit"
        HeaderArr(9, 1) = "L"
        HeaderArr(9, 2) = "15"
        HeaderArr(9, 3) = "15"

        HeaderArr(10, 0) = "Deposit Start Date"
        HeaderArr(10, 1) = "L"
        HeaderArr(10, 2) = "15"
        HeaderArr(10, 3) = "15"


        HeaderArr(11, 0) = "Deposit End Date"
        HeaderArr(11, 1) = "L"
        HeaderArr(11, 2) = "10"
        HeaderArr(11, 3) = "10"

        HeaderArr(12, 0) = "Total Days"
        HeaderArr(12, 1) = "L"
        HeaderArr(12, 2) = "10"
        HeaderArr(12, 3) = "10"


        HeaderArr(13, 0) = "Interest %"
        HeaderArr(13, 1) = "L"
        HeaderArr(13, 2) = "15"
        HeaderArr(13, 3) = "15"

        HeaderArr(14, 0) = "Incentive for the Month"
        HeaderArr(14, 1) = "L"
        HeaderArr(14, 2) = "15"
        HeaderArr(14, 3) = "15"


        Session("RptHeader") = HeaderArr

        M_Sql = "EXEC Get_Incentive_Sp'" & Txt_Canvasserid.Text & "'," & CommonClass.DbDate(TxtFromDate.Text) & "," & CommonClass.DbDate(TxtTodate.Text) & ""
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
        Exit Sub
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub Txt_Canvasserid_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_Canvasserid.TextChanged
        Try

            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL
            M_CustMaster = M_CustMasterBL.Locate_Data(Txt_Canvasserid.Text, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
            With M_CustMaster

                'CmbAdmBranch.SelectedValue = .BrCode
                Txt_CanvasserName.Text = .CustName
                'Txt_.Text = M_PermAddress.AddAddress1 + "," + M_PermAddress.AddAddress2 + "," + M_PermAddress.AddAddress3 + "," + M_PermAddress.AddPostoffice + "," + M_PermAddress.AddPinCode
                'TxtPermLand.Text = 
                'TxtPermPin.Text = 
                'TxtPermHouseNo.Text = M_PresAddress.AddHouseNo
                'TxtPermDist.Text = M_PresAddress.AddDist
                'TxtPermCountry.Text = M_PresAddress.AddCountry
                'cust_show.cust_grid()
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