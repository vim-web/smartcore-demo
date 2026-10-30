Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class SchemeOutstandRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
                Ddlsection.Items.Add("LOAN")
                Ddlsection.Items.Add("DEPOSIT")
     
                M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
                DdlBranch.Items.Add("ALL")
                DtFrom.Text = Session("TransDate")
                DdlBranch.SelectedValue = Session("DefaultBrCode")
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

            Dim Br_code As String = DdlBranch.SelectedValue
            Dim From_Date As String = DtFrom.Text

            Dim User_Id As String = Session("UserID")

            Dim Section As String = Ddlsection.SelectedValue
            Dim ChkBranch As String = "N"

            If CkBranchWise.Checked = True Then
                ChkBranch = "Y"
            End If

            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

            If Ddlsection.SelectedItem.Text = "LOAN" Then


                Dim HeaderArr(4, 4) As String
                Dim HeaderWidth(4, 1) As String
                HeaderArr(0, 0) = "Scheme"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "25"

                HeaderArr(1, 0) = "Loan Amount"
                HeaderArr(1, 1) = "R"
                HeaderArr(1, 2) = "12"
                HeaderArr(1, 3) = "12"

                HeaderArr(2, 0) = "Received"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "13"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "Outstand"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "12"
                HeaderArr(3, 3) = "13"

                Session("RptHeader") = HeaderArr
            Else
                Dim HeaderArr(4, 4) As String
                Dim HeaderWidth(4, 1) As String
                HeaderArr(0, 0) = "Scheme"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "10"
                HeaderArr(0, 3) = "25"

                HeaderArr(1, 0) = "Deposit Amount"
                HeaderArr(1, 1) = "R"
                HeaderArr(1, 2) = "12"
                HeaderArr(1, 3) = "12"

                HeaderArr(2, 0) = "Paid"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "13"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "Outstand"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "12"
                HeaderArr(3, 3) = "13"

                Session("RptHeader") = HeaderArr
            End If


            M_Sql = "EXEC [Get_Scheme_Outstsand_Sp] '" & DdlBranch.SelectedValue & "','" & Ddlsection.SelectedItem.Text & "','" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & ChkBranch & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)


            Exit Sub

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub DdlBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DdlBranch.SelectedIndexChanged
        If DdlBranch.SelectedItem.Text = "ALL" Then
            CkBranchWise.Visible = True
        Else
            CkBranchWise.Checked = False
            CkBranchWise.Visible = False
        End If
    End Sub
    Protected Sub DrpRegn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpRegn.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            Call DrpArea_SelectedIndexChanged(Me.DrpArea, System.EventArgs.Empty)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpArea.SelectedIndexChanged
        Try
            Dim M_CommonBl As New CommonBL
            M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
            DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
            DdlBranch.Items.Add("ALL")
        Catch ex As Exception

        End Try
    End Sub
End Class