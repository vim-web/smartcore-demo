Imports SMARTCORE_DAL

Partial Public Class SMARTCORE
    Inherits System.Web.UI.MasterPage
    Private M_Conn As New Connection
    Dim M_Sql As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            
            LblUser.Text = "User ID    " & Session("TestLogin")
            LblTrDate.Text = "Trans Date " & Session("TransDate")
            'LblTrBranch.Text = "Branch " & Session("DefaultBrCode")
            LblTrBranch.Text = Session("Br_Name")
            'Response.Write("<span style='position:absolute;top:55px;width:100%;'> <marquee> <H3> <font  color=blue> " & Session("GeneralMessage").ToString & " </font></H3> </marquee>  </span>")
            'HdnNews.Value = 
            'TextBox1.Text = Session("GeneralMessage").ToString
        
        End If

        If Session("UserId") = "" Then
            Response.Redirect("~/SmartLogin.aspx")
        End If
        Message()
        UserLogout()
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If Session("UserId") = "" Then
            Response.Redirect("~/SmartLogin.aspx")
        Else

            If Session("IsDayOpened") = "N" Then
                Response.Redirect("~/DayOpenProcess.aspx")
                'ShowAlertMessage("...Day Open...")
                Exit Sub
            End If
            Response.Redirect("~/Main.aspx?UsrID=" & Session("UserID"))
        End If

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Dim M_Dt As New DataTable
        Dim Dt As Date
        Dt = Session("TransDate")
        M_Sql = "Update Login_Tbl set Status='N'  where  Tr_Date='" & Format(Dt, "MM/dd/yyyy") & "' and Username='" & Session("TestLogin") & "' And Br_Code='" & Session("DefaultBrCode") & "'"
        M_Conn.ExecuteDataTable(M_Dt, M_Sql, False)
        Session("LoginID") = ""
        Session("UserID") = ""
        Session.Clear()
        Session.Abandon()
        Response.Redirect("~/SmartLogin.aspx")
    End Sub
    Private Sub Message()
        Dim Dt As New DataTable
        Dim Da As Date
        Dim News As String = ""
        'Da = Session("TransDate")
        'M_Sql = "select Remainder_Content from Gen_Remainder_M_Tbl where RemainderDate='" & Format(Da, "MM/dd/yyyy") & "' and Priority='H' and Reminder_Id=(select MAX(Reminder_Id) from Gen_Remainder_M_Tbl where RemainderDate='" & Format(Da, "MM/dd/yyyy") & "' and Priority='H')"
        'M_Conn.ExecuteDataTable(Dt, M_Sql, False)
        Dim M_SqlStr = "select top 1 Ggr_LendingRate  from gold_gramrate_P_Tbl where Br_Code = '" & Session("DefaultBrCode") & "' order by Ggr_EffectDate desc,Ggr_LendingRate desc"
        Dim GoldDt As New DataTable
        M_Conn.ExecuteDataTable(GoldDt, M_SqlStr, False)
        If GoldDt.Rows.Count > 0 Then
            News = "Today's Maximum Lending Gram Rate  Rs.:" + GoldDt.Rows(0)(0).ToString() + " "
        Else
            News = ""
        End If

        'If Dt.Rows.Count > 0 Then
        '    News += Dt.Rows(0)(0).ToString()
        'Else
        '    News += " "
        'End If
        hid_News.Value = News

    End Sub
    Private Sub UserLogout()
        Dim Dt As New DataTable
        Dim dates As Date
        dates = Session("TransDate")
        M_Sql = "select * from Login_Tbl where Br_Code='" & Session("DefaultBrCode") & "' and Tr_Date='" & Format(dates, "MM/dd/yyyy") & "' and Username='" & Session("TestLogin") & "' and Status='N'"
        M_Conn.ExecuteDataTable(Dt, M_Sql, False)
        If Dt.Rows.Count > 0 Then
            Response.Redirect("~/SmartLogin.aspx?msg=You are Logout by Some User")
        End If
    End Sub
End Class