Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Partial Public Class CmpFinalAcc_Frm
  
    Inherits System.Web.UI.Page
    Private Shared M_DBConn As Connection
    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Private Shared Company_Name As String
    Dim FormID As Integer = 1010
    Shared Obj_UserRights As Object
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                M_DBConn = New Connection
                Dim M_CommonBL As New CommonBL
                ''**********************************************************************************
                If System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH") <> "%" Then
                    M_CommonBL.Fill_Branch(DdlBranch, False)
                Else
                    M_CommonBL.Fill_Branch(DdlBranch, True)
                End If
                ''**********************************************************************************
                DdlBranch.SelectedValue = Session("DefaultBrCode")
                DrpRpyType.Items.Add("Balance Sheet")
                DrpRpyType.Items.Add("Profit & Loss")
                DrpRpyType.SelectedIndex = 0

                DtFrom.Text = Session("TransDate")
                TxtDt2.Text = Session("TransDate")
                TxtDt3.Text = Session("TransDate")
                'RdoOption.Items(0).Selected = True
            End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
        Try
            Dim M_Sql As String = ""
            Dim M_Dt As New DataTable
            Dim dt1 As Date
            Dim dt2 As Date
            Dim dt3 As Date
            dt1 = DtFrom.Text
            dt2 = TxtDt2.Text
            dt3 = TxtDt3.Text


            M_Sql = " Exec[GetComparativeAccBal_Sp] '" & DdlBranch.SelectedValue & "','','" & Format(dt1, "MM/dd/yyyy") & "','" & Format(dt2, "MM/dd/yyyy") & "','" & Format(dt3, "MM/dd/yyyy") & "','" & Session("HostID") & "'"


            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
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

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_ReportBl As New ReportsBL
            Dim M_Sql As String
            Dim ClsComm As New CommonClass
            Dim M_Dt As New DataTable()
            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Dim StrSlf As String = ""



            Dim rssub(2) As String
            Dim Str_Flf As String
            Dim StrSub_sql As String
            Dim SubFld As String
            Dim SubRptName As String
            Dim ReportType As String
            ReportType = DrpRpyType.SelectedItem.Text

            If ReportType = "Balance Sheet" Then



                M_Sql = " SELECT distinct Grp_Name, Acc_Name, Grp_SeqNo, Acc_SeqNo " & _
                          " , Date_1," & _
                          " ABS( (SELECT Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                          "         AND (SELECT Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )>0 )) As Date_1Bal," & _
                          " (SELECT Case WHEN  Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_1BalType" & _
                          " , Date_2," & _
                          " ABS( (SELECT Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                          "         AND (SELECT Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )>0)) As Date_2Bal," & _
                          " (SELECT Case WHEN  Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_2BalType" & _
                          "  , Date_3," & _
                          " ABS( (SELECT Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                          "         AND (SELECT Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )>0)) As Date_3Bal," & _
                          " (SELECT Case WHEN  Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_3BalType" & _
                          " FROM Account_Bal_Comp_R_Tbl ABC" & _
                          " WHERE (Acc_Position IN ( 'B', 'PB' )) " & _
                          " And (Terminal = '" & Session("HostID") & "')  " & _
                          " AND 	((Date_3BalType = 'C') OR (Date_2BalType = 'C') OR (Date_1BalType = 'C'))" & _
                          " UNION ALL" & _
                          " SELECT   'P & L', 'NET PROFIT', '500', '500',  '', " & _
                          "  Case  When Isnull(Sum(Case When Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0)>0 " & _
                          "  Then  Abs(Isnull(Sum(Case When Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0)) Else 0 End as Date_1Bal,'' , '', " & _
                          "  Case  When Isnull(Sum(Case When Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0)>0 " & _
                          "  Then  Abs(Isnull(Sum(Case When Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0)) Else 0 End as Date_2Bal,'' , '', " & _
                          "  Case  When Isnull(Sum(Case When Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0)>0 Then  " & _
                          "  Abs(Isnull(Sum(Case When Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0)) Else 0 End as Date_3Bal,'' " & _
                          "  FROM Account_Bal_Comp_R_Tbl WHERE  	(Acc_Position = 'P') " & _
                          " And Terminal = '" & Session("HostID") & "'" & _
                          "  ORDER BY Grp_SeqNo , Acc_SeqNo "




                StrSub_sql = " SELECT distinct Grp_Name, Acc_Name, Grp_SeqNo, Acc_SeqNo " & _
                              " , Date_1," & _
                              " ABS( (SELECT Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                              "         AND (SELECT Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )<0 )) As Date_1Bal," & _
                              " (SELECT Case WHEN  Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_1BalType" & _
                              " , Date_2," & _
                              " ABS( (SELECT Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                              "         AND (SELECT Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )<0)) As Date_2Bal," & _
                              " (SELECT Case WHEN  Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_2BalType" & _
                              "  , Date_3," & _
                              " ABS( (SELECT Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                              "         AND (SELECT Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )<0)) As Date_3Bal," & _
                              " (SELECT Case WHEN  Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_3BalType" & _
                              " FROM Account_Bal_Comp_R_Tbl ABC" & _
                              " WHERE (Acc_Position IN ( 'B', 'PB' )) " & _
                              " And (Terminal = '" & Session("HostID") & "')  " & _
                              " AND 	((Date_3BalType = 'D') OR (Date_2BalType = 'D') OR (Date_1BalType = 'D'))" & _
                              " UNION ALL" & _
                              " SELECT   'P & L', 'NET PROFIT', '500', '500',  '', " & _
                              "  Case  When Isnull(Sum(Case When Date_1BalType='C'  Then 1 Else -1 End * Date_1Bal),0)<0 " & _
                              "  Then  Abs(Isnull(Sum(Case When Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0)) Else 0 End as Date_1Bal,'' , '', " & _
                              "  Case  When Isnull(Sum(Case When Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0)<0 " & _
                              "  Then  Abs(Isnull(Sum(Case When Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0)) Else 0 End as Date_2Bal,'' , '', " & _
                              "  Case  When Isnull(Sum(Case When Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0)<0 Then  " & _
                              "  Abs(Isnull(Sum(Case When Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0)) Else 0 End as Date_3Bal,'' " & _
                              "  FROM Account_Bal_Comp_R_Tbl WHERE  	(Acc_Position = 'P') " & _
                              " And Terminal = '" & Session("HostID") & "'" & _
                              "  ORDER BY Grp_SeqNo , Acc_SeqNo "

                rssub(0) = StrSub_sql
            End If
            If ReportType = "Profit & Loss" Then
                '-- Expenditure
                M_Sql = " SELECT distinct Grp_Name, Acc_Name, Grp_SeqNo, Acc_SeqNo " & _
                              " , Date_1," & _
                              " ABS( (SELECT Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                              "         AND (SELECT Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )<0 )) As Date_1Bal," & _
                              " (SELECT Case WHEN  Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_1BalType" & _
                              " , Date_2," & _
                              " ABS( (SELECT Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                              "         AND (SELECT Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )<0)) As Date_2Bal," & _
                              " (SELECT Case WHEN  Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_2BalType" & _
                              "  , Date_3," & _
                              " ABS( (SELECT Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                              "         AND (SELECT Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )<0)) As Date_3Bal," & _
                              " (SELECT Case WHEN  Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_3BalType" & _
                              " FROM Account_Bal_Comp_R_Tbl ABC" & _
                              " WHERE (Acc_Position IN ( 'P' )) " & _
                              " And (Terminal = '" & Session("HostID") & "')  " & _
                              " AND 	((Date_3BalType = 'D') OR (Date_2BalType = 'D') OR (Date_1BalType = 'D'))" & _
                              " UNION ALL" & _
                              " SELECT   'P & L', 'NET PROFIT', '500', '500',  '', " & _
                              "  Case  When Isnull(Sum(Case When Date_1BalType='C'  Then 1 Else -1 End * Date_1Bal),0)>0 " & _
                              "  Then  Abs(Isnull(Sum(Case When Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0)) Else 0 End as Date_1Bal,'' , '', " & _
                              "  Case  When Isnull(Sum(Case When Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0)>0 " & _
                              "  Then  Abs(Isnull(Sum(Case When Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0)) Else 0 End as Date_2Bal,'' , '', " & _
                              "  Case  When Isnull(Sum(Case When Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0)>0 Then  " & _
                              "  Abs(Isnull(Sum(Case When Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0)) Else 0 End as Date_3Bal,'' " & _
                              "  FROM Account_Bal_Comp_R_Tbl WHERE (Acc_Position = 'P') " & _
                              " And Terminal = '" & Session("HostID") & "'" & _
                              "  ORDER BY Grp_SeqNo , Acc_SeqNo "

                'Dr_Obj = Set_DataReader(Str_Sql)



                '-- Income
                StrSub_sql = " SELECT distinct Grp_Name, Acc_Name, Grp_SeqNo, Acc_SeqNo " & _
                          " , Date_1," & _
                          " ABS( (SELECT Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                          "         AND (SELECT Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )>0 )) As Date_1Bal," & _
                          " (SELECT Case WHEN  Isnull(SUM(case when Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_1BalType" & _
                          " , Date_2," & _
                          " ABS( (SELECT Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                          "         AND (SELECT Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )>0)) As Date_2Bal," & _
                          " (SELECT Case WHEN  Isnull(SUM(case when Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_2BalType" & _
                          "  , Date_3," & _
                          " ABS( (SELECT Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code " & _
                          "         AND (SELECT Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code )>0)) As Date_3Bal," & _
                          " (SELECT Case WHEN  Isnull(SUM(case when Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0 ) > 0 Then 'C' Else 'D' End From  Account_Bal_Comp_R_Tbl AAA Where AAA.Acc_Code = ABC.Acc_Code) As Date_3BalType" & _
                          " FROM Account_Bal_Comp_R_Tbl ABC" & _
                          " WHERE (Acc_Position IN ( 'P' )) " & _
                          " And (Terminal = '" & Session("HostID") & "')  " & _
                          " AND 	((Date_3BalType = 'C') OR (Date_2BalType = 'C') OR (Date_1BalType = 'C'))" & _
                          " UNION ALL" & _
                          " SELECT   'P & L', 'NET PROFIT', '500', '500',  '', " & _
                          "  Case  When Isnull(Sum(Case When Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0)<0 " & _
                          "  Then  Abs(Isnull(Sum(Case When Date_1BalType='C' Then 1 Else -1 End * Date_1Bal),0)) Else 0 End as Date_1Bal,'' , '', " & _
                          "  Case  When Isnull(Sum(Case When Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0)<0 " & _
                          "  Then  Abs(Isnull(Sum(Case When Date_2BalType='C' Then 1 Else -1 End * Date_2Bal),0)) Else 0 End as Date_2Bal,'' , '', " & _
                          "  Case  When Isnull(Sum(Case When Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0)<0 Then  " & _
                          "  Abs(Isnull(Sum(Case When Date_3BalType='C' Then 1 Else -1 End * Date_3Bal),0)) Else 0 End as Date_3Bal,'' " & _
                          "  FROM Account_Bal_Comp_R_Tbl WHERE  	(Acc_Position = 'P') " & _
                          " And Terminal = '" & Session("HostID") & "'" & _
                          "  ORDER BY Grp_SeqNo , Acc_SeqNo "

                rssub(0) = StrSub_sql

            End If
            Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()
            M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)

            'Str_Flf = "Company|" & Company_Name & "|Heading|Comparitive Balance Sheet Between " & Format(Convert.ToDateTime(DtFrom.Text), "dd/MM/yyyy") & " And " & Format(Convert.ToDateTime(TxtDt2.Text), "dd/MM/yyyy") & " And " & Format(Convert.ToDateTime(TxtDt3.Text), "dd/MM/yyyy") & " |User_Id|" & System_Name & "   "
            StrSlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|ReportType|" & ReportType & "|Branch|Branch : All |Heading|Comparitive '" & ReportType & "' Between " & Format(Convert.ToDateTime(DtFrom.Text), "dd/MM/yyyy") & " And " & Format(Convert.ToDateTime(TxtDt2.Text), "dd/MM/yyyy") & " And " & Format(Convert.ToDateTime(TxtDt3.Text), "dd/MM/yyyy") & " |User_ID|" & Session("UserID")
            StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Manager"
            SubFld = "Asset_Expenditure"
            Session.Add("RptQry", M_Sql)
            SubRptName = "Asset_Expenditure"
            Session.Add("SubRptNames", SubRptName)
            Session.Add("SubRptQry", rssub)
            Session.Add("RptPath", Server.MapPath("~/Reports/Accounts/BalanceSheet_CompareNew.rpt"))
            Session.Add("RptFormula", StrSlf)
            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click

        Response.Redirect("~/Main.aspx")
    End Sub
End Class

