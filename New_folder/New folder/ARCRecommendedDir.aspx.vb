Imports SMARTCORE_BL
Imports SMARTCORE_DA
Imports SMARTCORE_Common
Public Class ARCRecommendedDir
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Load_DefaultValues()
        End If
    End Sub

    Private Sub Load_DefaultValues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Branch(Cmb_Branch)
        M_CommonBL.Fill_Directors(CmbDirCode)
        M_CommonBL.GetSchemes(Cmb_Scheme, Session("TransDate"), "LOAN")
        Txt_FrDate.Text = Session("TransDate")
        Txt_ToDate.Text = Session("TransDate")
        Cmb_Branch.Items.Add("ALL")
        Cmb_Scheme.Items.Add("ALL")
        M_CommonBL = Nothing
    End Sub

    Protected Sub CmdShow_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Dim M_Sql As String
        'Dim ClsComm As New CommonClass()
        Dim M_Dt As New DataTable()
        Dim StrSlf As String
        Dim M_CommonDa As New CommonDA

        Dim Brcode As String
        Dim SchemeCode As String
        If Cmb_Branch.SelectedValue <> "ALL" Then
            Brcode = Cmb_Branch.SelectedValue
        Else
            Brcode = "0"
        End If
        If Cmb_Scheme.SelectedValue <> "ALL" Then
            SchemeCode = Cmb_Scheme.SelectedValue
        Else
            SchemeCode = "0"
        End If


        Dim HeaderArr(7, 4) As String
        Dim HeaderWidth(7, 1) As String
        HeaderArr(0, 0) = "Branch"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "20"
        HeaderArr(0, 3) = "15"

        HeaderArr(1, 0) = "AccNo"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "40"
        HeaderArr(1, 3) = "40"

        HeaderArr(2, 0) = "Scheme"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "20"
        HeaderArr(2, 3) = "13"

        HeaderArr(3, 0) = "MemNo"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "20"
        HeaderArr(3, 3) = "13"

        HeaderArr(4, 0) = "LoanAmt"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "20"
        HeaderArr(4, 3) = "13"

        HeaderArr(5, 0) = "OpeningDate"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "20"
        HeaderArr(5, 3) = "13"

        HeaderArr(6, 0) = "ClosedDate"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "20"
        HeaderArr(6, 3) = "13"

        Session("RptHeader") = HeaderArr


        M_Sql = "EXEC ArcRecommandedDir_Sp'" & Brcode & "','" & SchemeCode & "','" & CmbDirCode.SelectedValue & "'," & CommonClass.DbDate(Txt_FrDate.Text) & "," & CommonClass.DbDate(Txt_ToDate.Text) & ""
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class