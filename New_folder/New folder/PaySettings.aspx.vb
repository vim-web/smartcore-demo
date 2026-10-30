Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class PaySettings
    Inherits System.Web.UI.Page
    Public PaySettingTbl As New DataTable("Table")
    Dim FormID As Integer = 1113
    Shared Obj_UserRights As Object


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'USer Rights
            Dim M_Comm As New CommonBL
            Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
            M_Comm = Nothing
            If Obj_UserRights.View_Right = False Then
                Response.Redirect("~/UnauthorisedAcces.ASPX")
            End If


            ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            Session("SearchType") = "EMPLOYEE"
            Dim M_PayrollBL As New PayrollBL
            M_PayrollBL.Fill_PayHead(CmbPayHead)
            M_PayrollBL = Nothing
            Set_Grid()
        End If
        If Session("SearchResult") <> "" Then
            TxtID.Text = Session("SearchResult")
            TxtID_TextChanged(Me.TxtID, System.EventArgs.Empty)
            TxtID.Focus()
            Session("SearchResult") = ""

        End If
        Session("SearchResult") = ""
    End Sub

    Private Sub Set_Grid()
        PaySettingTbl.Columns.Add("PayCode", GetType(integer))
        PaySettingTbl.Columns.Add("PayHead", GetType(String))
        PaySettingTbl.Columns.Add("EmpAmt", GetType(Double))
        PaySettingTbl.Columns.Add("EmpPer", GetType(Double))
        PaySettingTbl.Columns.Add("BankAmt", GetType(Double))
        PaySettingTbl.Columns.Add("BankPer", GetType(Double))
        PaySettingTbl.Columns.Add("AdnlAmt", GetType(Double))
        PaySettingTbl.Columns.Add("AccNo", GetType(String))
        PaySettingTbl.Columns.Add("AccID", GetType(Integer))
        PaySettingTbl.Columns.Add("Remarks", GetType(String))

        PaySettingTbl.Rows.Add(0, "", 0, 0, 0, 0, 0, "", 0, "")
        'PaySettingTbl.Rows.Add(2, "DA", 0, 0, 0, 0, 0, "", 0, "")
        Dim columns(1) As DataColumn
        columns(0) = PaySettingTbl.Columns("PayCode")
        PaySettingTbl.PrimaryKey = columns
        Session("PayTbl") = PaySettingTbl
        DgvPaySetting.DataSource = PaySettingTbl
        DgvPaySetting.DataBind()
    End Sub

    Protected Sub TxtID_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtID.TextChanged
        If TxtID.Text.Length = 0 Then

            Session("M_EntryMode") = "NEW"
        Else
            Try
                Dim M_Employee As New SMARTCORE_BO.EmployeeBO
                Dim M_PayrollBL As New PayrollBL
                M_Employee = M_PayrollBL.Locate_Data(TxtID.Text)
                Dim M_Datatable As New DataTable
                M_Datatable = M_PayrollBL.Locate_PaySettings(TxtID.Text)
                Session("PayTbl") = M_Datatable
                DgvPaySetting.DataSource = M_Datatable
                DgvPaySetting.DataBind()
                If Locate_Data(M_Employee) = False Then
                    ShowAlertMessage("Invalid Employee ID")
                End If
                M_PayrollBL = Nothing
            Catch ex As Exception

            End Try
        End If

    End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Private Function Locate_Data(ByVal M_Employee As SMARTCORE_BO.EmployeeBO) As Boolean
        If M_Employee.EmpID = 0 Then
            Return False
        End If
        With M_Employee
            TxtName.Text = .EmpName
        End With
        Return True
    End Function

    Protected Sub CmdClear_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear.Click
        ClearControls
    End Sub

    Protected Sub CmdAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdAdd.Click

        If Val(TxtID.Text) <= 0 Then
            ShowAlertMessage("INVALID EMPLOYEE ID ENTERED")
            TxtID.Focus()
            Exit Sub
        End If


        Dim dt As DataTable = DirectCast(Session("PayTbl"), DataTable)
        Dim Row As DataRow
        Row = dt.NewRow
        Row("PayCode") = CmbPayHead.SelectedValue
        Row("PayHead") = CmbPayHead.SelectedItem.Text
        Row("EmpAmt") = Val(TxtEmpAmt.Text)
        Row("EmpPer") = Val(TxtEmpPer.Text)
        Row("BankAmt") = Val(TxtBankAmt.Text)
        Row("BankPer") = Val(TxtBankPer.Text)
        Row("AdnlAmt") = Val(TxtAdnlAmt.Text)
        Row("AccNo") = TxtAccNo.Text
        Row("AccID") = Val(HdnAccid.Value)
        Row("Remarks") = TxtRemarks.Text
        Try
            dt.Rows.Add(Row)
        Catch ex As Exception

        End Try
        Try
            Dim Expression As String = "PAYHEAD=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try
        DgvPaySetting.DataSource = dt
        DgvPaySetting.DataBind()
        Session("PayTbl") = dt
        ClearControls()
    End Sub

    Private Sub ClearControls()
        TxtEmpAmt.Text = ""
        TxtEmpPer.Text = ""
        TxtBankAmt.Text = ""
        TxtBankPer.Text = ""
        TxtAdnlAmt.Text = ""
        TxtAccNo.Text = ""
        TxtAccName.Text = ""
        HdnAccid.Value = 0
        TxtRemarks.Text = ""
    End Sub

    Private Sub DeletePayHead(ByVal PayCode As String)
        Dim i As Integer
        Dim dt As DataTable = DirectCast(Session("PayTbl"), DataTable)
        For i = 0 To DgvPaySetting.Rows.Count - 1
            If dt.Rows.Item(i).Item("PayCode").ToString = PayCode Then
                dt.Rows.Item(i).Delete()
                GoTo otherow
            End If
        Next i
otherow:
        DgvPaySetting.DataSource = dt
        DgvPaySetting.DataBind()
        Session("PayTbl") = dt

        If dt.Rows.Count <= 0 Then Call Set_Grid()

    End Sub

    Private Sub DgvPaySetting_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvPaySetting.RowDeleting
        Try
            Dim PayCode As String = CInt(DgvPaySetting.DataKeys(e.RowIndex).Value)
            DeletePayHead(PayCode)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub TxtAccNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtAccNo.TextChanged
        Dim M_CommonBL As New CommonBL
        Dim M_Accid As Integer
        TxtAccName.Text = M_CommonBL.GetAccountCustName(TxtAccNo.Text, M_Accid)
        HdnAccid.Value = M_Accid
        M_CommonBL = Nothing
    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click
            Dim Paydt As DataTable = DirectCast(Session("PayTbl"), DataTable)
            Dim M_PaySw As New IO.StringWriter
            Paydt.WriteXml(M_PaySw)

            Dim M_PayrollBL As New PayrollBL
            Dim M_Dt As New DataTable
            If  M_PayrollBL.Save_PaySettings(Txtid.Text, M_PaySw.ToString.Replace("DocumentElement", "NewDataSet"))=True then
            ShowAlertMessage("EMPLOYEE SETTINGS UPDATED")
            TxtID.Text = ""
            TxtName.Text = ""
            ClearControls()
            Set_Grid()
            TxtID.Focus()
        Else
            ShowAlertMessage("EMPLOYEE SETTINGS NOT UPDATED")
        End If


    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub
End Class