Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Partial Public Class InterestDemandRpt

    Inherits System.Web.UI.Page

    Private Shared M_DBConn As Connection
    Dim FormID As Integer = 1089
    Shared Obj_UserRights As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If IsPostBack = False Then

                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If



                Dim M_CommonBL As New CommonBL

                M_CommonBL.Fill_Branch(CmbBranch)
                M_CommonBL.GetSchemes(CmbScheme, Session("TransDate"), "LOAN")

                TxtFrDate.Text = Session("TransDate")
                TxtToDate.Text = Session("TransDate")

                M_CommonBL = Nothing
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim M_Sql As String
            M_Sql = "SELECT L.*,S.Sch_Name FROM Loan_DCB_R_Tbl L Join Gen_SchemeMaster_P_Tbl S On S.Sch_Code=L.Sch_Code  WHERE Terminal = '" & _
            Session("HostID") & "' Order By Acc_No"

            Dim StrFlf As String
            StrFlf = "Company|" & HttpContext.Current.Cache("CompanyName") & "|Branch|" & CmbBranch.SelectedItem.Text & _
                "|Heading|Demand,Collection & Balance and Loan Outstanding Statement Between " & TxtFrDate.Text & " And " & TxtToDate.Text

            Session.Add("RptPath", Server.MapPath("~/Reports/Loans/Loan_DCB.rpt"))
            Session.Add("RptQry", M_Sql)
            Session.Add("RptFormula", StrFlf)

            ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")

            Response.Redirect("~/RptPreview.aspx?Outputto=2", False)

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
    Protected Sub ImgProcess_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcess.Click
        Try
            Dim CommmonCls As New CommonClass
            Dim M_Sql As String
            M_Sql = "Exec Loan_Dcb '" & CmbScheme.SelectedValue & "','" & CmbBranch.SelectedValue & "'," & CommmonCls.DbDate(TxtFrDate.Text) & _
                "," & CommmonCls.DbDate(TxtToDate.Text) & ",'" & Session("HostID") & "','" & Session("LoginID") & "','Y','N',''"

            M_DBConn = New Connection()
            M_DBConn.ExecuteScalar(M_Sql, Connection.SQLSource.STOREDPROCEDURE, False)

            ShowAlertMessage("Process Completed.")

            CommmonCls = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub
End Class