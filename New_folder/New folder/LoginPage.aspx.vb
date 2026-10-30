Imports SMARTCORE_BL
Imports System.Configuration
Imports System.Configuration.ConfigurationManager
Partial Public Class LoginPage
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1102

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CmdClose.Attributes.Add("OnClick", "window.close();")
            DtpLoginDate.Attributes.Add("readonly", "readonly")
            DtpLoginDate.Text = Date.Now.ToString("dd/MM/yyyy")
            Session.Add("LoginStatus", "")
            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            M_CommonBL = Nothing
        End If
    End Sub

    Protected Sub CmbLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmbLogin.Click
        Dim M_CommonBL As New CommonBL
        Dim M_Dt As New DataTable
        Dim I As Integer
        Dim Message As String
        '   Dim M_DtMessage As New DataTable
        M_Dt = M_CommonBL.Fill_BranchDtls(CmbBranch.SelectedValue)
        Session("DefDist") = M_Dt.Rows(0).Item("Br_DefDist")
        Session("DefState") = M_Dt.Rows(0).Item("Br_DefState")
        Session("DefCountry") = M_Dt.Rows(0).Item("Br_DefCountry")
        M_CommonBL = Nothing
        Message = ""

        Dim M_RemainderBl As New RemainderBL
        Session("RemainderTbl") = M_RemainderBl.Set_GenRemainderGrid
        Dim M_DtMessage As DataTable = DirectCast(Session("RemainderTbl"), DataTable)
        If M_DtMessage.Rows.Count > 0 Then
            For I = 0 To M_DtMessage.Rows.Count - 1
                If I = 0 Then
                    Message = Message + (M_DtMessage.Rows(I)(2).ToString & " By[" & M_DtMessage.Rows(I)(3).ToString & "] ")
                Else
                    Message = Message + (" :::: " & M_DtMessage.Rows(I)(2).ToString & " By[" & M_DtMessage.Rows(I)(3).ToString & "] ")
                End If



            Next



            Session.Add("GeneralMessage", Message)
        Else
            Session.Add("GeneralMessage", "HAVE A NICE DAY")
        End If


        Session.Add("LoginID", TxtLogin.Text)
        Session("UserID") = TxtLogin.Text
        Session.Add("LoginPswd", TxtPswd.Text)
        Session.Add("TransDate", DtpLoginDate.Text)
        Session.Add("DefaultBrCode", CmbBranch.SelectedValue)
        Session.Add("DefaultBrName", CmbBranch.Text)
        Session.Add("File_Path", AppSettings("REPORT_FOLDER"))
        Response.Redirect("~/Main.aspx")
    End Sub

End Class