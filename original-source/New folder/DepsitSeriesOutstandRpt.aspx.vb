Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class DepsitSeriesOutstandRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
                M_dt = M_CommonBL.Get_SchemeSection
            
                ' M_dt = M_CommonBL.Get_Scheme(Ddlsection.SelectedValue)
             
                M_CommonBL.Fill_Branch(DdlBranch)
                M_CommonBL.Fill_DebSeries(Ddlseries)
                M_CommonBL.GetSchemes(DrpSch, Session("TransDate"), "DEPOSIT")
                DdlBranch.Items.Add("ALL")
                Ddlseries.Items.Add("ALL")
                DrpSch.Items.Add("ALL")
                DtTo.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                Call Ddlseries_SelectedIndexChanged(Me.Ddlseries, System.EventArgs.Empty)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""
            Dim clscomm As New CommonClass
            Dim Scheme As String = ""
            Dim Series As String = ""
            Dim SubSeries As String = ""
            Dim Branch As String = ""


            If DrpSch.SelectedValue = "ALL" Then
                Scheme = "0"
            Else
                Scheme = DrpSch.SelectedValue
            End If
            If DdlBranch.SelectedValue = "ALL" Then
                Branch = "0"
            Else
                Branch = DdlBranch.SelectedValue
            End If
            If Ddlseries.SelectedValue = "ALL" Then
                Series = "0"
            Else
                Series = Ddlseries.SelectedValue
            End If
            If DdlSubSer.SelectedValue = "ALL" Then
                SubSeries = "0"
            Else
                SubSeries = DdlSubSer.SelectedValue
            End If


            Dim HeaderArr(14, 4) As String
            Dim HeaderWidth(14, 1) As String
            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "5"
            HeaderArr(0, 3) = "5"

            HeaderArr(1, 0) = "Cust Name"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "22"
            HeaderArr(1, 3) = "22"


            HeaderArr(2, 0) = "Scheme"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "22"
            HeaderArr(2, 3) = "22"


            HeaderArr(3, 0) = "Acc No"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "13"
            HeaderArr(3, 3) = "13"

            HeaderArr(4, 0) = "Dep Date"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "10"
            HeaderArr(4, 3) = "10"


            HeaderArr(5, 0) = "No Of Deb"
            HeaderArr(5, 1) = "R"
            HeaderArr(5, 2) = "10"
            HeaderArr(5, 3) = "5"


            HeaderArr(6, 0) = "Month"
            HeaderArr(6, 1) = "R"
            HeaderArr(6, 2) = "10"
            HeaderArr(6, 3) = "5"

            HeaderArr(7, 0) = "Days"
            HeaderArr(7, 1) = "R"
            HeaderArr(7, 2) = "10"
            HeaderArr(7, 3) = "5"

            HeaderArr(8, 0) = "Int Rate"
            HeaderArr(8, 1) = "R"
            HeaderArr(8, 2) = "10"
            HeaderArr(8, 3) = "5"

            HeaderArr(9, 0) = "Period Type"
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "13"
            HeaderArr(9, 3) = "13"

            HeaderArr(10, 0) = "Due Date"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "15"
            HeaderArr(10, 3) = "15"

            HeaderArr(11, 0) = "Outstand"
            HeaderArr(11, 1) = "R"
            HeaderArr(11, 2) = "12"
            HeaderArr(11, 3) = "12"

            HeaderArr(12, 0) = "Canvasser"
            HeaderArr(12, 1) = "L"
            HeaderArr(12, 2) = "12"
            HeaderArr(12, 3) = "12"

            HeaderArr(13, 0) = "App Date"
            HeaderArr(13, 1) = "L"
            HeaderArr(13, 2) = "12"
            HeaderArr(13, 3) = "12"

            HeaderArr(14, 0) = "Distinctive No"
            HeaderArr(14, 1) = "L"
            HeaderArr(14, 2) = "20"
            HeaderArr(14, 3) = "25"

        
      

            Session("RptHeader") = HeaderArr

            M_Sql = "EXEC [Get_DepositOutstandSeriesWise_Sp] '" & Branch & "','" & Scheme & "','" & Series & "','" & SubSeries & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "','" & Session("UserID") & "','" & Session("HostID") & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=130", False)

        Catch ex As Exception

        End Try
   
    End Sub

    Protected Sub Ddlseries_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Ddlseries.SelectedIndexChanged
        Dim M_CommonBL As New CommonBL
        Dim SeriesCode As String = ""
        SeriesCode = IIf(Ddlseries.SelectedValue = "ALL", "0", Ddlseries.SelectedValue)
        M_CommonBL.Fill_DebSubSeries(DdlSubSer, SeriesCode)
        DdlSubSer.Items.Add("ALL")
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class