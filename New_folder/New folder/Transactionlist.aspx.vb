Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL

Public Class Transactionlist
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Rpt_Qry As String
    Private Rpt_Path As String
    Shared AcHeadDt As DataTable
    Private Rpt_Heading As String
    Private Rpt_Formula As String
    Private Rpt_ProcessQry As String
    Private M_CommonBL As New CommonBL
    Private M_CommonDa As New CommonDA
    Dim FormID As Integer = 1145
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                TxtFromDate.Text = Session("TransDate")
                TxtToDate.Text = Session("TransDate")
                M_DBConn = New Connection
                M_CommonBL.Fill_Branch(CmbBranch)
                CmbBranch.Items.Add("<--ALL-->")
                CmbBranch.SelectedValue = Session("DefaultBrCode")
                CmbOutput.Items.Add("Printer")
                CmbOutput.Items.Add("Screen")
                CmbOutput.SelectedIndex = 1
                Session("SearchType") = "ACCOUNTNO"
                AcHeadDt = DirectCast(Cache("Achead"), DataTable)
            End If

           
          
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "'")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
   
    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            Dim StrSlf As String = ""
            Dim ClsComm As New CommonClass()
            Dim M_Dt As DataTable = New DataTable()
            Dim Company_Name As String = HttpContext.Current.Cache("CompanyName")
            Dim Brcode As String
            Dim SchemeCode As String
            If CmbBranch.SelectedValue <> "<--ALL-->" Then
                Brcode = CmbBranch.SelectedValue
            Else
                Brcode = "0"
            End If

            M_Sql = Rpt_Qry '.ToUpper()

            ' Formula From Sql Query Output 

            Dim M_Qry As String = ""
            Dim M_QryFormula As String = ""
            Dim QryVar As String = ""
            Dim QryNo1 As Integer = "0"





            If ChkToExcl.Checked = False Then
                Dim Dt As Date
                Dt = TxtFromDate.Text
                M_Sql = "Exec TransactionListRpt_Sp '" & Brcode & "','','" & TxtAccCode.Text & "','','" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "'"
                StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|Branch : " & CmbBranch.SelectedItem.ToString & "|Heading|Transaction List of " & TxtAccName.Text & " from " & TxtFromDate.Text & " to " & TxtToDate.Text & "|User_ID|" & Session("UserID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
                Session.Add("RptQry", M_Sql)
                Session.Add("RptFormula", StrSlf)
                Session.Add("RptPath", Server.MapPath("~/Reports/RKFinance/TranList.rpt"))
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
                Exit Sub
            Else
                Dim HeaderArr(5, 4) As String
                Dim HeaderWidth(5, 1) As String
                HeaderArr(0, 0) = "Date"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "12"
                HeaderArr(0, 3) = "12"

                HeaderArr(1, 0) = "Narration"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "75"
                HeaderArr(1, 3) = "75"

                HeaderArr(2, 0) = "Credit(Rs)"
                HeaderArr(2, 1) = "R"
                HeaderArr(2, 2) = "13"
                HeaderArr(2, 3) = "13"

                HeaderArr(3, 0) = "Debit(Rs)"
                HeaderArr(3, 1) = "R"
                HeaderArr(3, 2) = "13"
                HeaderArr(3, 3) = "13"


                HeaderArr(4, 0) = "Balance"
                HeaderArr(4, 1) = "R"
                HeaderArr(4, 2) = "13"
                HeaderArr(4, 3) = "13"

                HeaderArr(5, 0) = "Bal Type"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "10"
                HeaderArr(5, 3) = "10"

                Session("RptHeader") = HeaderArr

                M_Sql = "Exec TransactionListRptToExcl_Sp '" & Brcode & "','','" & TxtAccCode.Text & "','','" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "'"
                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=80", False)
            End If
            'Session.Add("RptQry", M_Qry)
            'StrSlf = "Company|" & Company_Name & "|Branch|" & CmbBranch.SelectedItem.ToString & " - Branch|Heading|Transaction List Between : " & TxtFromDate.Text & " And " & TxtToDate.Text & " |User_ID|" & Session("LoginID")


            'StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"

            'Rpt_Path = "Transaction_List.rpt"

            'M_Qry = "Exec Proc_Transaction_List_Sp '" & Brcode & "','','" & TxtAccCode.Text  & "','','" & Format(CDate(TxtFromDate.Text), "MM/dd/yyyy") & "','" & Format(CDate(TxtToDate.Text), "MM/dd/yyyy") & "','" & Session("LoginID") & "','" & Session("HostID") & "'"

            'M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)

            'If M_Dt.Rows.Count > 0 Then
            '    'M_DBConn.ExecuteDataTable(M_Dt, M_QryFormula, False)
            '    M_DBConn.ExecuteDataTable(M_Dt, M_Qry, False)

            '    If M_Dt.Rows.Count > 0 Then
            '        StrSlf = Replace(StrSlf, QryVar, M_Dt.Rows(0)(0).ToString())
            '    Else
            '        StrSlf = Replace(StrSlf, QryVar, "0")
            '    End If
            'End If

            'Session.Add("RptQry", M_Qry)

            'Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/" & Rpt_Path))
            'Session.Add("RptFormula", StrSlf)

            'If CmbOutput.SelectedIndex = 0 Then
            '    Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
            'Else
            '    Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            'End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub TxtAccCode_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccCode.TextChanged
        Try
            Dim Expression As String = "Acc_Code='" & TxtAccCode.Text & "'"
            Dim drFiltered As DataRow() = AcHeadDt.[Select](Expression)

            If drFiltered.Count > 0 Then
                TxtAccName.Text = drFiltered(0)("Acc_Name")
            Else
                TxtAccName.Text = ""
                ShowAlertMessage("Invalid A/c Head")
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()> _
  Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Try
            If True Then
                Dim Expression As String = "Acc_Name LIKE '" & prefixText & "%'"
                Dim SortingOrder As String = "Acc_Name ASC"
                Dim drFiltered As DataRow() = AcHeadDt.[Select](Expression, SortingOrder)
                Dim CompletionSet = New List(Of String)()
                For Each row As DataRow In drFiltered
                    CompletionSet.Add(row("Acc_Name").ToString().PadRight(40, Convert.ToChar(".")) & "|" & "Code:" & row("Acc_Code").ToString())
                Next
                Return CompletionSet.ToArray()
            End If
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Function

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class