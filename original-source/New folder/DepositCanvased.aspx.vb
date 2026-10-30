Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports SMARTCORE_DA
Imports CrystalDecisions.Shared
Imports System.IO
Imports SMARTCORE_Common
Public Class DepositCanvased
    Inherits System.Web.UI.Page
    Dim M_CommonBL As New CommonBL
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                LoadValues()
                Session("SearchType") = "CUSTOMER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            End If
            If Session("SearchResult") <> "" Then
                If Session("SearchType") = "CUSTOMER" Then
                    Txt_Cust_Id.Text = Session("SearchResult")
                    Txt_Cust_Id_TextChanged(Me.Txt_Cust_Id, System.EventArgs.Empty)
                    Txt_Cust_Id.Focus()
                    Session("SearchResult") = ""
                End If
                Session("SearchResult") = ""
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try



    End Sub
    Public Sub LoadValues()
        M_CommonBL.Fill_Branch(Drp_Branch)
        Drp_Branch.Items.Add("--Select--")
        Drp_Branch.SelectedValue = Session("DefaultBrCode")
    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub CmdSave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
        Try
            Dim Brcode As String
            Brcode = Drp_Branch.SelectedValue
            Dim Customerid As String
            Customerid = Txt_Cust_Id.Text
            Dim CustomerName As String
            CustomerName = Txt_Cust_Name.Text
            Dim Trdate As Date
            Trdate = Session("TransDate")
            Dim M_CommonBL As New CommonBL
            Dim InsQry As String
            InsQry = M_CommonBL.DepositCanvas(Brcode, Customerid, CustomerName, Trdate)
            ShowAlertMessage("Save Deposit Canvas Successfully")
            Txt_Cust_Id.Text = ""
            Txt_Cust_Name.Text = ""

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
       
    End Sub

    Protected Sub Txt_Cust_Id_TextChanged(sender As Object, e As EventArgs) Handles Txt_Cust_Id.TextChanged
        Load_CustomerData()
    End Sub
    Private Sub Load_CustomerData()
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PhotoID As New SMARTCORE_BO.CustIdentityBO
            Dim M_ADDID As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL

            M_CustMaster = M_CustMasterBL.Locate_Data(Val(Txt_Cust_Id.Text), M_PresAddress, M_PermAddress, M_PhotoID, M_ADDID)
            Locate_Customer(M_CustMaster, M_PresAddress, M_PermAddress)
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
    Private Sub Locate_Customer(ByVal M_CustMaster As SMARTCORE_BO.CustMasterBO, ByVal M_PresAddress As SMARTCORE_BO.CustAddressBO, ByVal M_PermAddress As SMARTCORE_BO.CustAddressBO)
        On Error Resume Next
       
        If M_CustMaster.CustStatus = "I" Or M_CustMaster.CustStatus = "D" Then
            ShowAlertMessage("CUSTOMER IS NOT ACTIVE")
            Exit Sub
        End If

        With M_CustMaster
            Txt_Cust_Name.Text = .CustName
            Txt_Cust_Id.Enabled = False
        End With
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        Txt_Cust_Id.Text = ""
        Txt_Cust_Name.Text = ""
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class