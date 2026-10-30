Imports SMARTCORE_DAL
Imports SMARTCORE_BL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Imports System.IO
Imports System.Web.Services
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Configuration
Imports System.Text.RegularExpressions
Partial Public Class CustVenuedetails
    Inherits System.Web.UI.Page
    Dim FormID As Integer
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = "" Then
            Response.Redirect("~/Smartlogin.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            'If Obj_UserRights.View_Right = False Then
            '    Response.Redirect("~/UnauthorisedAcces.ASPX")
            'End If
            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=300,width=500,scrollbars=yes');return false;")
            Session("SearchType") = "VENUE"
            Txtid.Focus()
            Session("SearchResult") = ""

        End If
        If Txtid.Text <> "" Then
            Clear()
            Txtid.Text = Session("SearchResult")
            Txtid_TextChanged(Me.Txtid, System.EventArgs.Empty)
        Else
            Txtid.Text = Session("SearchResult")
        End If

    End Sub
    Protected Sub CmdSave0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave0.Click
        Dim M_Venue As New SMARTCORE_BO.VenueMasterBO
        Dim M_VenueBL As New VenueMasterBL
        With M_Venue
            '.VID = Txtid.Text
            .VName = Txtname.Text
            .VContactPName = Txtcontact.Text
            .VAddress = Txtaddress.Text
            .VLandmark = Txtland.Text
            .VPhoneNo = Txtph.Text
            .VMobileNo = Txtmobile.Text
        End With
        M_VenueBL.Save_Data(M_Venue, Session("M_EntryMode"))
        Clear()
        M_VenueBL = Nothing


    End Sub
    Private Sub Clear()
        Txtid.Text = ""
        Txtname.Text = ""
        Txtaddress.Text = ""
        Txtland.Text = ""
        Txtph.Text = ""
        Txtcontact.Text = ""
        Txtmobile.Text = ""
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Clear()
    End Sub

    Protected Sub Txtid_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txtid.TextChanged

    End Sub
End Class