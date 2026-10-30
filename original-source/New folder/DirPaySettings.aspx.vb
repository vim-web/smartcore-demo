Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class DirPaySettings
    Inherits System.Web.UI.Page
    Public DirFee_T_Tbl As New DataTable("Table")
    Dim FormID As Integer = 1113
    Shared Obj_UserRights As Object
    Dim str As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Set_Grid()
            Load_Defaultvalues()
        End If
        Dim M_Comm As New CommonBL
        Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
        M_Comm = Nothing
        If Obj_UserRights.View_Right = False Then
            Response.Redirect("~/UnauthorisedAcces.ASPX")
           
        End If
        If Len(Request.QueryString("TRANID")) > 0 Then
            GetAuthoriseDtls("DIR", Request.QueryString("TRANID"))
        End If
    End Sub

    Private Sub GetAuthoriseDtls(ByVal FromNm As String, ByVal TransID As String)

        Dim M_CommonBL As New CommonBL
        Dim M_Dataset As New DataSet
        M_Dataset = M_CommonBL.GetAuthoriseDtls("DIR", TransID)
        M_CommonBL = Nothing
        Dim M_TranTbl As New DataTable
        M_TranTbl = M_Dataset.Tables(1)

        Session("TransTbl") = M_Dataset.Tables(0)


        M_CommonBL = Nothing
        GetAccountDtls(M_TranTbl)
        DgvTransaction.DataSource = M_Dataset.Tables(0)
        DgvTransaction.DataBind()
    End Sub
    Private Sub GetAccountDtls(ByVal Dtbl As DataTable)

        CmbDirector.SelectedValue = Dtbl.Rows(0)(0).ToString()
        CmbDirector.Enabled = False
        If (Dtbl.Rows(0).Item("Cash_Amt").ToString() = "0.00") Then
            CmbCashTransfer.Text = "Transfer"
            TxtAcHeadTransAmt.Text = Dtbl.Rows(0)(3).ToString()
            CmbCashTransfer.Enabled = False
            TxtAcHeadTransAmt.Enabled = False
        ElseIf (Dtbl.Rows(0).Item("Transfer_Amt").ToString() = "0.00") Then
            CmbCashTransfer.Text = "Cash"
            TxtAcHeadTransAmt.Text = Dtbl.Rows(0)(2).ToString()
            CmbCashTransfer.Enabled = False
            TxtAcHeadTransAmt.Enabled = False
        End If

    End Sub


    Private Sub Load_Defaultvalues()
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Directors(CmbDirector)
        CmbCashTransfer.Items.Add("Cash")
        CmbCashTransfer.Items.Add("Transfer")
        CmbCashTransfer.SelectedIndex = 0
    End Sub
    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Set_Grid()
        Try
            DgvTransaction.DataSource = Nothing
            DirFee_T_Tbl.Columns.Add("Dirname", GetType(String))
            DirFee_T_Tbl.Columns.Add("Tranno", GetType(String))
            DirFee_T_Tbl.Columns.Add("AccCode", GetType(String))
            DirFee_T_Tbl.Columns.Add("AccHead", GetType(String))
            DirFee_T_Tbl.Columns.Add("RctCash", GetType(String))
            DirFee_T_Tbl.Columns.Add("RctTransfer", GetType(String))
            DirFee_T_Tbl.Columns.Add("PayCash", GetType(String))
            DirFee_T_Tbl.Columns.Add("PayTransfer", GetType(String))

            DirFee_T_Tbl.Rows.Add(" ", " ", " ", "")
            Session("DirFee_T_Tbl") = DirFee_T_Tbl
            DgvTransaction.DataSource = DirFee_T_Tbl
            DgvTransaction.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    '   
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub



    Private Sub clear()

        TxtAcHeadTransAmt.Text = " "
        LblPayCashTotal.Text = " "
        LblPayTransferTotal.Text = " "
        LblRctCashTotal.Text = " "
        LblRctTransferTotal.Text = " "
        Set_Grid()
    End Sub


    Protected Sub DgvTransaction_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvTransaction.RowDeleting
        Try
            Dim dt As DataTable = DirectCast(Session("DirFee_T_Tbl"), DataTable)
            dt.Rows(e.RowIndex).Delete()
            DgvTransaction.DataSource = dt
            DgvTransaction.DataBind()
            Calc_TranTotal()
            Session("DirFee_T_Tbl") = dt
            If dt.Rows.Count <= 0 Then Call Set_Grid()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
        
    End Sub
    Protected Sub CmdSave_Click(sender As Object, e As ImageClickEventArgs) Handles CmdSave.Click
        Try            If Len(Request.QueryString("TRANID")) > 0 Then
                Dim M_CommonBL1 As New CommonBL
                If M_CommonBL1.Authorise_Tran(Request.QueryString("TRANID"), Session("TransDate").ToString(), Session("UserID"), Session("DefaultBrCode").ToString()) = False Then
                    ShowAlertMessage("Authotisation Failed")
                    Response.Redirect("~/ManagerPassing.aspx.vb")
                    Exit Sub
                End If
                M_CommonBL1 = Nothing
                Response.Redirect("~/ManagerPassing.aspx?Passing=y")
                Exit Sub
            End If            Dim M_DirPaySettings As New SMARTCORE_BO.DirPaySettingsBO            With M_DirPaySettings
                .TrDate = Session("TransDate")
                .MakingTime = Session("TransDate")
                .CheckingTime = Session("TransDate")
                .BrCode = Session("DefaultBrCode")
                .MakerID = Session("LoginId")
                .CheckerID = Session("UserId")

            End With            Dim M_BrCode As String            Dim dt9 As New DataTable            Dim Dptable As DataTable = DirectCast(Session("DirFee_T_Tbl"), DataTable)
            Dim M_Dirpay As New IO.StringWriter
            Dptable.WriteXml(M_Dirpay)
            Dim M_DirPaySettingsBL As New DirPaySettingsBL            Session("M_EntryMode") = "NEW"            M_BrCode = M_DirPaySettingsBL.Save_Data(M_DirPaySettings, M_Dirpay.ToString.Replace("DocumentElement", "NewDataSet"), Session("M_EntryMode"))            'dt9 = M_DirPaySettingsBL.Update_AccHeadTran(Session("DefaultBrCode"), Session("TransDate"), Session("LoginID"), Session("TransDate"), Session("LoginID"), Session("TransDate"), M_Dirpay.ToString.Replace("DocumentElement", "NewDataSet"))
            Response.Redirect("~/NewAccount.aspx?AccNo=" & "DIRECTOR FEE" & "&TransId=" & M_BrCode & "&PrintBond=N" & "&SECTION=" & "ModuleCatg" & "&SCHEME=" & "VOU" & "&SEC=" & "GENERAL" & "&DRCR=C" & "&TRANCATG=" & "TRAN")            clear()        Catch ex As Exception        End Try
    End Sub

    Private Sub Calc_TranTotal()
        Try
            LblRctCashTotal.Text = "0.00"
            LblRctTransferTotal.Text = "0.00"
            LblPayCashTotal.Text = "0.00"
            LblPayTransferTotal.Text = "0.00"
            Dim j As Double = 0
            Dim s As Double = 0
            Dim s1 As Double = 0
            For i As Double = j To DgvTransaction.Rows.Count - 1
                s += Val(DgvTransaction.Rows(i).Cells(5).Text)
                s1 += Val(DgvTransaction.Rows(i).Cells(6).Text)
                j = i + 1
            Next

            LblPayCashTotal.Text = s.ToString()
            LblPayTransferTotal.Text = s1.ToString()
        Catch ex As Exception
            LoggError(ex)
        End Try

    End Sub


    Protected Sub CmdClose_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    Protected Sub CmdClear_Click(sender As Object, e As ImageClickEventArgs) Handles CmdClear.Click
        clear()
    End Sub
    Private Sub Acc_head()
        Dim dt5 As New DataTable


        Try
            Dim M_Acchead As New SMARTCORE_BL.DirPaySettingsBL
            dt5 = M_Acchead.Acc_head("136")
            str = dt5.Rows(0)(0).ToString()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdAdd_Click(sender As Object, e As ImageClickEventArgs) Handles CmdAdd.Click
        Dim dt As DataTable = DirectCast(Session("DirFee_T_Tbl"), DataTable)
        Dim dt5 As New DataTable
        Dim M_Acchead As New SMARTCORE_BL.DirPaySettingsBL
        dt5 = M_Acchead.Acc_head("136")
        Dim Row As DataRow
        Row = dt.NewRow
        Row("Dirname") = CmbDirector.SelectedValue
        Row("Tranno") = " "
        Row("AccCode") = dt5.Rows(0)(1).ToString()
        Row("AccHead") = dt5.Rows(0)(0).ToString()
        If CmbCashTransfer.SelectedIndex = 0 Then
            Row("RctCash") = 0
            Row("RctTransfer") = 0
            Row("PayCash") = Val(TxtAcHeadTransAmt.Text)
            Row("PayTransfer") = 0
        Else
            Row("RctCash") = 0
            Row("RctTransfer") = 0
            Row("PayCash") = 0
            Row("PayTransfer") = Val(TxtAcHeadTransAmt.Text)
        End If

        Try
            dt.Rows.Add(Row)
            Dim Expression As String = "AccHead=''"
            If dt.[Select](Expression).Count > 0 Then dt.Rows.RemoveAt(dt.Rows.IndexOf(dt.[Select](Expression)(0)))
        Catch ex As Exception

        End Try

        DgvTransaction.DataSource = dt
        DgvTransaction.DataBind()
        Session("DirFee_T_Tbl") = dt
        Calc_TranTotal()

    End Sub


    Protected Sub DgvTransaction_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DgvTransaction.RowDataBound
       
    End Sub
End Class