Imports SMARTCORE_BL
Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing
Imports System.Drawing.ColorTranslator
Imports SMARTCORE_Common

Partial Public Class Main
    Inherits System.Web.UI.Page
    Dim FormID As Integer = 1104

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = "" Then
                Response.Redirect("~/SmartLogin.aspx")
            End If
            '    If Not IsPostBack Then

            '        'If Session("UserID") <> Request.QueryString("UsrID") Then
            '        '    Response.Redirect("~/SmartLogin.aspx")
            '        'End If

            '        If Request.QueryString("UsrID") = Nothing Then
            '            Accordion2.SelectedIndex = 1
            '        Else
            '            Accordion2.SelectedIndex = 0
            '        End If

            If Session("ASSIGNEDBRANCH").ToString.ToLower() = "%" Then
                AdminMenu.Visible = True
                SysAdminMenu.Visible = False
                PnlMenu.Visible = False
                HoReports.Visible = True
            Else
                AdminMenu.Visible = False
                PnlMenu.Visible = True
                SysAdminMenu.Visible = False
                HoReports.Visible = False
            End If

            SessionDate()

            Dim barColors As Color() = New Color(7) {Color.Red, Color.YellowGreen, Color.Yellow, Color.Pink, Color.Navy, Color.Green, Color.Blue, Color.Red}
            Dim chartcolor As Color() = New Color(7) {Color.Violet, Color.Pink, Color.Gray, Color.Aqua, Color.Brown, Color.DarkGreen, Color.Gold, Color.DeepSkyBlue}
            Dim chartcolor1 As Color() = New Color(7) {Color.Red, Color.DarkBlue, Color.YellowGreen, Color.DarkGray, Color.DarkGreen, Color.CadetBlue, Color.Red, Color.Yellow}
            Dim chartcolor2 As Color() = New Color(7) {Color.Gray, Color.Green, Color.DeepPink, Color.Yellow, Color.Chartreuse, Color.Coral, Color.Red, Color.RoyalBlue}
            Dim chartcolor3 As Color() = New Color(7) {Color.Green, Color.Yellow, Color.DarkBlue, Color.DarkSlateGray, Color.Cornsilk, Color.Gainsboro, Color.DarkKhaki, Color.DarkCyan}
            Dim Colorcode As String() = New String(7) {"#b21069", "#5d0890", "#0fb512", "#dd6b0a", "#cedd0a", "#de234b", "#414682", "#0ba71c"}
            Dim Ds As New DataSet
            Dim M_CommonBL As New CommonBL
            Ds = M_CommonBL.Get_DashBoard(Session("TransDate"), Session("DefaultBrCode"), Session("UserID"))
            Dim Dt1 As New DataTable
            Dim Dt2 As New DataTable
            Dim Dt3 As New DataTable
            Dim Dt4 As New DataTable
            Dim Dt5 As New DataTable
            Dim Dt6 As New DataTable
            Dim Dt7 As New DataTable
            Dim Dt8 As New DataTable
            Dim Dt9 As New DataTable



            Dt1 = Ds.Tables(0)
            Dt2 = Ds.Tables(1)
            Dt3 = Ds.Tables(2)
            Dt4 = Ds.Tables(3)
            Dt5 = Ds.Tables(4)
            Dt6 = Ds.Tables(5)
            Dt7 = Ds.Tables(6)
            'Dt8 = Ds.Tables(7)
            'Dt9 = Ds.Tables(8)
            Dim dr As DataRow
            If Dt6.Rows.Count = 0 Then
                'Dt6.Columns.Add("Cust_Name")
                'Dt6.Columns.Add("acc_no")
                'Dt6.Columns.Add("loanamt")
                'Dt6.Columns.Add("balance")
                'Dt6.Columns.Add("Cust_Mobile")
                Dt6.Rows.Add("-", "0", "0", "0", "0")


            End If



            DgvSummary.DataSource = Dt4
            DgvSummary.DataBind()
            'DgvDeposit.DataSource = Dt8
            'DgvDeposit.DataBind()
            GridLoan.DataSource = Dt6
            GridLoan.DataBind()

            Reminder.DataSource = Dt7
            Reminder.DataBind()
            'ChartShare_Ac.Series("Default")("PieLabelStyle") = "Disabled"
            grid_loan.DataSource = Dt3
            grid_loan.DataBind()
            Dim x As String() = New String(Dt1.Rows.Count - 1) {}
            Dim y As Integer() = New Integer(Dt1.Rows.Count - 1) {}
            For i As Integer = 0 To Dt1.Rows.Count - 1
                x(i) = Dt3.Rows(i)(0)
                y(i) = Dt3.Rows(i)(1)
            Next
            'ChartShare_Ac.Series("Default").Points.DataBindXY(x, y)
            'For i As Integer = 0 To ChartShare_Ac.Series("Default").Points.Count - 1
            '    ChartShare_Ac.Series("Default").Points(i).Color = barColors(i)
            'Next

            Dim cx As String() = New String(Dt2.Rows.Count - 1) {}
            Dim cy As Integer() = New Integer(Dt2.Rows.Count - 1) {}
            For i As Integer = 0 To Dt2.Rows.Count - 1
                cx(i) = Dt2.Rows(i)(0)
                cy(i) = Dt2.Rows(i)(1)
            Next
            'ChartShare_outSt.Series("Default").Points.DataBindXY(cx, cy)
            'For i As Integer = 0 To ChartShare_outSt.Series("Default").Points.Count - 1
            '    ChartShare_outSt.Series("Default").Points(i).Color = FromHtml(Colorcode(i))
            'Next

            Dim mdx As String() = New String(Dt3.Rows.Count - 1) {}
            Dim mdy As Integer() = New Integer(Dt3.Rows.Count - 1) {}

            For i As Integer = 0 To Dt3.Rows.Count - 1
                mdx(i) = Dt3.Rows(i)(0)
                mdy(i) = Dt3.Rows(i)(1)
            Next
            'ChartDeposit_Ac.Series("Default").Points.DataBindXY(mdx, mdy)

            'For i As Integer = 0 To ChartDeposit_Ac.Series("Default").Points.Count - 1
            '    ChartDeposit_Ac.Series("Default").Points(i).Color = chartcolor1(i)
            '    'ChartDeposit_Ac.Series("Default").Points(i).Color = FromHtml(Colorcode(i))
            'Next


            Dim Lx As String() = New String(Dt4.Rows.Count - 1) {}
            Dim Ly As Integer() = New Integer(Dt4.Rows.Count - 1) {}

            For i As Integer = 0 To Dt4.Rows.Count - 1
                Lx(i) = Dt4.Rows(i)(0)
                Ly(i) = Dt4.Rows(i)(1)
            Next
            'ChartDeposit_OutSt.Series("Default").Points.DataBindXY(Lx, Ly)

            'For i As Integer = 0 To ChartDeposit_OutSt.Series("Default").Points.Count - 1
            '    ChartDeposit_OutSt.Series("Default").Points(i).Color = chartcolor2(i)
            'Next


            ChartLoan_Ac.Series("Default")("PieLabelStyle") = "Disabled"
            ChartLoan_Ac.Series("Default").XValueMember = "Particulars"
            ChartLoan_Ac.Series("Default").YValueMembers = "NoOfAccounts"
            ChartLoan_Ac.Series("Default").YValuesPerPoint = Dt3.Rows.Count

            ChartLoan_Ac.Series("Default")("DoughnutRadius") = "60"
            ChartLoan_Ac.Series("Default")("Exploded") = "true"

            ChartLoan_Ac.DataSource = Dt3
            ChartLoan_Ac.DataBind()

            For i As Integer = 0 To ChartLoan_Ac.Series("Default").Points.Count - 1
                ChartLoan_Ac.Series("Default").Points(i).Color = barColors(i)
            Next

            ChartLoan_OutSt.Series("Default")("PieLabelStyle") = "Disabled"
            ChartLoan_OutSt.Series("Default").XValueMember = "Particulars"
            ChartLoan_OutSt.Series("Default").YValueMembers = "Outstanding"
            ChartLoan_OutSt.Series("Default").YValuesPerPoint = Dt3.Rows.Count
            ChartLoan_OutSt.Series("Default")("DoughnutRadius") = "60"
            ChartLoan_OutSt.Series("Default")("Exploded") = "true"

            ChartLoan_OutSt.DataSource = Dt3
            ChartLoan_OutSt.DataBind()

            For i As Integer = 0 To ChartLoan_OutSt.Series("Default").Points.Count - 1
                ChartLoan_OutSt.Series("Default").Points(i).Color = chartcolor3(i)
            Next

            '        Rights()

            '        Dim Ds1 As New DataSet
            '        Dim dt12 As New DataTable
            '        Ds1 = M_CommonBL.Get_UserDiary(Session("TransDate"), Session("DefaultBrCode"), Session("UserID").ToString())
            '        Dim dt11 As New DataTable
            '        If Ds1.Tables.Count > 0 Then

            '            dt11 = Ds1.Tables(0)
            '            dt12 = Ds1.Tables(1)
            '            If dt11.Rows.Count > 0 Then
            '                DGV_UserDiary.DataSource = dt11
            '                DGV_UserDiary.DataBind()

            '            End If
            '            If dt12.Rows.Count > 0 Then
            '                GrdUserMessage.DataSource = dt12
            '                GrdUserMessage.DataBind()
            '            End If



            '            ' Response.Write("<span style='position:absolute;top:55px;width:100%;'> <marquee> <H3> <font  color=blue> " & Session("GeneralMessage").ToString & " </font></H3> </marquee>  </span>")
            '            'HdnNews.Value = 
            '            'TextBox1.Text = Session("GeneralMessage").ToString
            '        End If
            '    End If

        Catch ex As Exception
            LoggError(ex)
        End Try
    End Sub
    Protected Sub SessionDate()
        Session("PrintFromDt") = Session("TransDate")
    End Sub
    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub
    'Private Sub Rights()
    '    Try
    '        Dim Dt As New DataTable
    '        Dim M_CommonBL As New CommonBL
    '        Dt = M_CommonBL.Get_Rights(Session("UserID"))
    '        For k As Integer = 0 To Dt.Rows.Count - 1
    '            If Dt.Rows(k)(0).ToString() = "CustomerRegister" Then
    '                Panel4.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "AccountOpen" Then
    '                Panel2.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "AccountTransaction" Then
    '                Panel3.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "Authorisation" Then
    '                Panel5.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "Cancellation" Then
    '                Panel6.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "DuplicatePrinting" Then
    '                Panel7.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "DailyStatements" Then
    '                Panel9.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "Outstanding" Then
    '                Panel11.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "PersonalLedger" Then
    '                Panel1.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "GoldStock" Then
    '                Panel8.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "ChequeIssue" Then
    '                Panel4.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "ChequeReturn" Then
    '                Panel15.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "LAForm" Then
    '                Panel12.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "LoanApplication" Then
    '                Panel17.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "LoanSecurity" Then
    '                Panel13.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "PropertyInspection" Then
    '                Panel18.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "RecordInspection" Then
    '                Panel19.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "LoanSanction" Then
    '                Panel20.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "CashBalance Chart" Then
    '                Chart1.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "Dep Outstanding Chart" Then
    '                Chart2.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "Matured Deposit Chart" Then
    '                Chart3.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "Loan OutStanding Chart" Then
    '                Chart4.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "Loan Od Amt chart" Then
    '                Chart5.Visible = False
    '            ElseIf Dt.Rows(k)(0).ToString() = "Loan Od No chart" Then
    '                Chart6.Visible = False

    '            End If


    '        Next
    '        Dt = Nothing
    '    Catch ex As Exception

    '    End Try
    'End Sub

End Class