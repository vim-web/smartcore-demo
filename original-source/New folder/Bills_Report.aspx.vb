Imports SMARTCORE_BL
Imports SMARTCORE_Common

Partial Public Class Bills_Report
    Inherits System.Web.UI.Page

    Private Shared Rpt_Qry As String
    Private Shared Rpt_Path As String
    Private Shared Rpt_Heading As String
    Private Shared Rpt_Formula As String
    Private Shared Rpt_ProcessQry As String
    Dim CommonCls As New CommonClass
    Dim FormID As Integer = 1012
    Shared Obj_UserRights As Object




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If

                Load_DefaultValues()
                TxtFrDate.Text = Session("TransDate")
                TxtToDate.Text = Session("TransDate")
                Call CmbRptName_SelectedIndexChanged(Me.CmbRptName, System.EventArgs.Empty)
                Call CmbBankName_SelectedIndexChanged(Me.CmbBankName, System.EventArgs.Empty)

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

    Private Sub Load_DefaultValues()
        Try
            CmbRptName.Items.Add("COLLECTION LETTER")
            CmbRptName.Items.Add("COLLECTION LETTER ACKNOWLEDGE")
            CmbRptName.Items.Add("BILLS REGISTER")
            CmbRptName.Items.Add("BILLS REALISED REPORT")
            CmbRptName.Items.Add("BILLS RETURNED REPORT")
            CmbRptName.Items.Add("PENDING BILLS")

            CmbRptName.SelectedIndex = 0

            Dim RptType As String
            RptType = Request.QueryString("RptType")

            If RptType = "BILLS REALISED REPORT" Then
                CmbRptName.SelectedIndex = 3

            ElseIf RptType = "BILLS RETURNED REPORT" Then
                CmbRptName.SelectedIndex = 4

            ElseIf RptType = "PENDING BILLS" Then
                CmbRptName.SelectedIndex = 5
            End If

            Dim M_CommonBL As New CommonBL
            M_CommonBL.Fill_Branch(CmbBranch)
            CmbBranch.SelectedValue = Session("DefaultBrCode")

            Dim M_BillBL As New BillBL
            M_BillBL.Fill_BillCollBranch("%", CmbBankName)

            CmbBillType.Items.Add("OUTWARD")
            CmbBillType.Items.Add("INWARD")
            CmbBillType.SelectedIndex = 0

            CmbOutput.Items.Add("Printer")
            CmbOutput.Items.Add("Screen")
            CmbOutput.SelectedIndex = 1
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmbRptName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbRptName.SelectedIndexChanged

    End Sub

    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try
            Dim Str As String 


        For Each gvRow As GridViewRow In DgvBills .Rows

            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
            If chkSel.Checked = True Then
                Dim BillId As TextBox = DirectCast(gvRow.FindControl("TxtBillId"), TextBox)
             Str = Str + IIf(Str = "", "", "-") + BillId.Text

            End If
        Next


            Dim M_Sql As String = ""

            Dim Company_Name As String
            Dim StrSlf As String = ""
            Dim Heading As String

            Dim M_CommonDa As New SMARTCORE_DA.CommonDA
            Company_Name = M_CommonDa.GetCompanyDetails()

            If CmbRptName.SelectedItem.Text = "COLLECTION LETTER" Then
                M_Sql = "Exec Bills_Report_Sp '" & CmbRptName.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & "," & _
                    CommonCls.DbDate(TxtToDate.Text) & "," & CmbBankName.SelectedValue & ",'" & Left(CmbBillType.SelectedItem.Text, 1) & "','" & CmbBranch.SelectedValue & "','" & Str &"'"

                Session.Add("RptPath", Server.MapPath("~/Reports/Bills/Bill_CollectionLetter.rpt"))
                StrSlf = "Company|" & Company_Name & "|Branch|" & CmbBranch.SelectedItem.ToString & " - Branch|Heading|Heading|User|" & Session("LoginID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            ElseIf CmbRptName.SelectedItem.Text = "COLLECTION LETTER ACKNOWLEDGE" Then
                M_Sql = "Exec Bills_Report_Sp '" & CmbRptName.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & "," & _
                    CommonCls.DbDate(TxtToDate.Text) & "," & CmbBankName.SelectedValue & ",'" & Left(CmbBillType.SelectedItem.Text, 1) & "','" & CmbBranch.SelectedValue & "','" & Str &"'"

                Session.Add("RptPath", Server.MapPath("~/Reports/Bills/Bill_CollectionLetterAcknowledge.rpt"))
                StrSlf = "Company|" & Company_Name & "|Branch|" & CmbBranch.SelectedItem.ToString & " - Branch|Heading|Heading|User|" & Session("LoginID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            ElseIf CmbRptName.SelectedItem.Text = "BILLS REGISTER" Then
                M_Sql = "Exec Bills_Report_Sp '" & CmbRptName.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & "," & _
                    CommonCls.DbDate(TxtToDate.Text) & "," & CmbBankName.SelectedValue & ",'" & Left(CmbBillType.SelectedItem.Text, 1) & "','" & CmbBranch.SelectedValue & "','" & Str &"'"

                Heading = CmbBillType.SelectedItem.Text & " Bills Register Between " & TxtFrDate.Text & " And " & TxtToDate.Text

                Session.Add("RptPath", Server.MapPath("~/Reports/Bills/BillsRegister.rpt"))
                StrSlf = "Company|" & Company_Name & "|Branch|" & CmbBranch.SelectedItem.ToString & " - Branch|Heading|" & Heading & "|User|" & Session("LoginID")
                StrSlf = StrSlf + "|Footer1|Clerk/Acct|Footer2|Accountant|Footer3|Secretary/Manager"
            ElseIf CmbRptName.SelectedItem.Text = "PENDING BILLS" Then

                Dim HeaderArr(10, 10) As String
                Dim HeaderWidth(10, 1) As String

                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "Bill No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"


                HeaderArr(2, 0) = "Bill Type"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "6"
                HeaderArr(2, 3) = "6"

                HeaderArr(3, 0) = "Presented Date"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "Acc NO"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "20"
                HeaderArr(4, 3) = "20"

                HeaderArr(5, 0) = "Name"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "17"
                HeaderArr(5, 3) = "17"


                HeaderArr(6, 0) = "Inst.No"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "10"

                HeaderArr(7, 0) = "Amount"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "10"
                HeaderArr(7, 3) = "10"


                HeaderArr(8, 0) = "Bank Name"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "14"
                HeaderArr(8, 3) = "14"

                HeaderArr(9, 0) = "Remarks"
                HeaderArr(9, 1) = "L"
                HeaderArr(9, 2) = "23"
                HeaderArr(9, 3) = "23"


                Session("RptHeader") = HeaderArr
                M_Sql = "Exec Bills_Report_Sp '" & CmbRptName.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & "," & _
                        CommonCls.DbDate(TxtToDate.Text) & "," & CmbBankName.SelectedValue & ",'" & Left(CmbBillType.SelectedItem.Text, 1) & "','" & CmbBranch.SelectedValue & "'"


                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub
            ElseIf CmbRptName.SelectedItem.Text = "BILLS REALISED REPORT" Then

                Dim HeaderArr(10, 10) As String
                Dim HeaderWidth(10, 1) As String

                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "Bill No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"


                HeaderArr(2, 0) = "Bill Type"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "6"
                HeaderArr(2, 3) = "6"

                HeaderArr(3, 0) = "Presented Date"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "Acc NO"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "20"
                HeaderArr(4, 3) = "20"

                HeaderArr(5, 0) = "Name"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "17"
                HeaderArr(5, 3) = "17"


                HeaderArr(6, 0) = "Inst.No"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "10"

                HeaderArr(7, 0) = "Amount"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "10"
                HeaderArr(7, 3) = "10"


                HeaderArr(8, 0) = "Bank Name"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "14"
                HeaderArr(8, 3) = "14"

                HeaderArr(9, 0) = "Remarks"
                HeaderArr(9, 1) = "L"
                HeaderArr(9, 2) = "23"
                HeaderArr(9, 3) = "23"


                Session("RptHeader") = HeaderArr
                M_Sql = "Exec Bills_Report_Sp '" & CmbRptName.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & "," & _
                        CommonCls.DbDate(TxtToDate.Text) & "," & CmbBankName.SelectedValue & ",'" & Left(CmbBillType.SelectedItem.Text, 1) & "','" & CmbBranch.SelectedValue & "'"


                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub
            ElseIf CmbRptName.SelectedItem.Text = "BILLS RETURNED REPORT" Then

                Dim HeaderArr(10, 10) As String
                Dim HeaderWidth(10, 1) As String

                HeaderArr(0, 0) = "SLN0"
                HeaderArr(0, 1) = "L"
                HeaderArr(0, 2) = "4"
                HeaderArr(0, 3) = "4"

                HeaderArr(1, 0) = "Bill No"
                HeaderArr(1, 1) = "L"
                HeaderArr(1, 2) = "13"
                HeaderArr(1, 3) = "13"


                HeaderArr(2, 0) = "Bill Type"
                HeaderArr(2, 1) = "L"
                HeaderArr(2, 2) = "6"
                HeaderArr(2, 3) = "6"

                HeaderArr(3, 0) = "Presented Date"
                HeaderArr(3, 1) = "L"
                HeaderArr(3, 2) = "10"
                HeaderArr(3, 3) = "10"

                HeaderArr(4, 0) = "Acc NO"
                HeaderArr(4, 1) = "L"
                HeaderArr(4, 2) = "20"
                HeaderArr(4, 3) = "20"

                HeaderArr(5, 0) = "Name"
                HeaderArr(5, 1) = "L"
                HeaderArr(5, 2) = "17"
                HeaderArr(5, 3) = "17"


                HeaderArr(6, 0) = "Inst.No"
                HeaderArr(6, 1) = "L"
                HeaderArr(6, 2) = "10"
                HeaderArr(6, 3) = "10"

                HeaderArr(7, 0) = "Amount"
                HeaderArr(7, 1) = "R"
                HeaderArr(7, 2) = "10"
                HeaderArr(7, 3) = "10"


                HeaderArr(8, 0) = "Bank Name"
                HeaderArr(8, 1) = "L"
                HeaderArr(8, 2) = "14"
                HeaderArr(8, 3) = "14"

                HeaderArr(9, 0) = "Remarks"
                HeaderArr(9, 1) = "L"
                HeaderArr(9, 2) = "23"
                HeaderArr(9, 3) = "23"


                Session("RptHeader") = HeaderArr
                M_Sql = "Exec Bills_Report_Sp '" & CmbRptName.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & "," & _
                        CommonCls.DbDate(TxtToDate.Text) & "," & CmbBankName.SelectedValue & ",'" & Left(CmbBillType.SelectedItem.Text, 1) & "','" & CmbBranch.SelectedValue & "'"


                Session("RptQry") = M_Sql
                Response.Redirect("~/PrnView.aspx?PageType=130", False)

                Exit Sub



            End If



            Session.Add("RptFormula", StrSlf)
            Session.Add("RptQry", M_Sql)
            If CmbOutput.SelectedIndex = 0 Then
                Response.Redirect("~/RptPreview.aspx?Outputto=1", False)
            Else
                Response.Redirect("~/RptPreview.aspx?Outputto=2", False)
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub




    Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim chkAll As CheckBox = DirectCast(DgvBills.HeaderRow.FindControl("chkSelectAll"), CheckBox)
        If chkAll.Checked = True Then
            For Each gvRow As GridViewRow In DgvBills.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = True
                Dim txtname As TextBox = DirectCast(gvRow.FindControl("TxtCust_Name"), TextBox)
                ' Dim txtlocation As TextBox = DirectCast(gvRow.FindControl("txtLocation"), TextBox)
                txtname.[ReadOnly] = False
                ' txtlocation.[ReadOnly] = False
                txtname.ForeColor = System.Drawing.Color.Black
                'txtlocation.ForeColor = System.Drawing.Color.Black
            Next
        Else
            For Each gvRow As GridViewRow In DgvBills.Rows
                Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
                chkSel.Checked = False
                Dim txtname As TextBox = DirectCast(gvRow.FindControl("TxtCust_Name"), TextBox)
                '  Dim txtlocation As TextBox = DirectCast(gvRow.FindControl("txtLocation"), TextBox)
                txtname.[ReadOnly] = True
                ' txtlocation.[ReadOnly] = True
                txtname.ForeColor = System.Drawing.Color.Blue
                ' txtlocation.ForeColor = System.Drawing.Color.Blue
            Next
        End If
    End Sub


    Protected Sub CmbBankName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbBankName.SelectedIndexChanged
        Dim Dt As DataTable
        Dim M_Sql As String

        Dim M_BillBL As New BillBL
        M_Sql = "Exec [GetBillIds] '" & CmbRptName.SelectedValue & "'," & CommonCls.DbDate(TxtFrDate.Text) & "," & _
                  CommonCls.DbDate(TxtToDate.Text) & "," & CmbBankName.SelectedValue & ",'" & Left(CmbBillType.SelectedItem.Text, 1) & "','" & CmbBranch.SelectedValue & "'"
        Dt = M_BillBL.Get_PendingBills(M_Sql)
        DgvBills.DataSource = Dt
        DgvBills.DataBind()


        M_BillBL = Nothing
    End Sub


    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
End Class