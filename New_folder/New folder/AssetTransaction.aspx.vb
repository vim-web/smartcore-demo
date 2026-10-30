Imports SMARTCORE_BO
Imports SMARTCORE_BL
Imports SMARTCORE_DAL
Imports System.Data
Public Class AssetTransaction
    Inherits System.Web.UI.Page
    Shared AssetDt As DataTable
    Private M_MasterType As String
    Private M_EntryMode As String
    Private M_TranType As String
    Private M_PurchaseDept As String
    Dim DB_Con As New Connection
    Dim FormID As Integer = 1016
    Shared Obj_UserRights As Object

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        If Valid_Data() = False Then
            Exit Sub
        End If
        Dim M_TransBO As New SMARTCORE_BO.FATransBO
        Dim M_TransBL As New SMARTCORE_BL.FATransBL
        With M_TransBO
            .FATransID = TxtID.Text
            .FATransDate = DtpDate.Text
            .FATransBranchID = CmbBranch.SelectedValue
            .FATransTransferBranchID = CmbTransBranch.SelectedValue
            .FATransType = M_TranType
            .FATransDealer = TxtDealer.Text
            .FATransRemarks = TxtRemarks.Text
            .FACurrency = "0" 'CmbCurrency.SelectedValue
        End With
        M_TransBL.Save_Data(M_EntryMode, M_MasterType, M_TransBO, Session("M_Trans"))
        TxtID.Focus()
        TxtID.Text = Get_NextNO(M_TranType)
        Clear_Values()


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("UserID") = "" Then
                Response.Redirect("~/Smartlogin.aspx")
                Exit Sub
            End If
            If Not IsPostBack Then
                'USer Rights
                Dim M_Comm As New CommonBL
                Obj_UserRights = M_Comm.CheckUserRight(Session("LoginUserID").ToString(), FormID)
                M_Comm = Nothing
                If Obj_UserRights.View_Right = False Then
                    Response.Redirect("~/UnauthorisedAcces.ASPX")
                End If
                Set_Grid()
                Load_Default()
                DtpDate.Text = Session("TransDate")
            End If
            CmbTransBranch.Visible = False
            TxtDealer.Visible = False
            lbldealer.Visible = False
            lblToBranch.Visible = False
            
            CmbDept.Enabled = True
            'GetType Query String from menus
            M_MasterType = Request.QueryString("RptType")
            If M_MasterType = "PURCHASE" Then
                M_TranType = "PR"
                Head.Text = "PURCHASE"
                TxtDealer.Visible = True
                lbldealer.Visible = True

            ElseIf M_MasterType = "SALES" Then
                M_TranType = "SL"
                TxtDealer.Visible = True
                Head.Text = "SALES"
                lbldealer.Visible = True
                '   CmbDept.Enabled = False

            ElseIf M_MasterType = "TRANSFER" Then
                M_TranType = "TR"
                Head.Text = "TRANSFER"
                lblToBranch.Visible = True
                CmbTransBranch.Visible = True

            ElseIf M_MasterType = "AUCTION" Then
                M_TranType = "AU"
                Head.Text = "AUCTION"
                TxtDealer.Visible = True
                lbldealer.Visible = True
                ' CmbDept.Enabled = False

            ElseIf M_MasterType = "DAMAGE" Then
                M_TranType = "DM"
                Head.Text = "DAMAGE"
                TxtDealer.Visible = True
                lbldealer.Visible = True
                '   CmbDept.Enabled = False

            End If
            If Not IsPostBack Then
                TxtID.Text = Get_NextNO(M_TranType)
            End If

        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub Clear_Values()
        CmbRefNo.Text = ""
        TxtDealer.Text = ""
        TxtRemarks.Text = ""
        Clear_ItemDtls()
        Set_Grid()
        TxtID.Text = Get_NextNO(M_TranType)
    End Sub
    Private Sub Clear_ItemDtls()
        TxtItemCode.Text = ""
        TxtItemName.Text = ""
        TxtQty.Text = ""
        TxtRate.Text = ""
        TxtValue.Text = ""
        CmbRefNo.Text = ""
    End Sub
    Private Sub Load_Default()
        Dim M_FATransBL As New FATransBL
        With M_FATransBL
            .Fill_Branch(CmbBranch)
            .Fill_Branch(CmbTransBranch)
            .Fill_Dept(CmbDept)
        End With
        Dim M_CommonBL As New CommonBL
        M_CommonBL.Fill_Pickup(CmbCurrency, 88, 0)
        'CmbCurrency.SelectedValue = Session("LocalCurrCode")
        M_CommonBL = Nothing
        AssetDt = DirectCast(Cache("Asset"), DataTable)


    End Sub
    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Shared Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Try
            If True Then
                Dim Expression As String = "FAItem_Name LIKE '" & prefixText & "%'"
                Dim SortingOrder As String = "FAItem_Name ASC"
                Dim drFiltered As DataRow() = AssetDt.[Select](Expression, SortingOrder)
                Dim CompletionSet = New List(Of String)()
                For Each row As DataRow In drFiltered
                    CompletionSet.Add(row("FAItem_Name").ToString().PadRight(40, Convert.ToChar(".")) & "|" & "Code:" & row("FAItem_Code").ToString())
                Next
                Return CompletionSet.ToArray()
            End If
        Catch ex As Exception
            ShowAlertMessage(Err.Description)
        End Try
    End Function
    Private Sub Set_Grid()
        Set_PurchaseGrid()
    End Sub
    Private Sub Set_PurchaseGrid()

        Dim M_Dgv As New DataTable
        M_Dgv.Columns.Add("Item_Code", GetType(String))
        M_Dgv.Columns.Add("Item_Name", GetType(String))
        M_Dgv.Columns.Add("FATrans_RefNo", GetType(String))
        M_Dgv.Columns.Add("FATrans_DeptID", GetType(String))
        M_Dgv.Columns.Add("FATrans_DeptName", GetType(String))
        M_Dgv.Columns.Add("Item_Qty", GetType(String))
        M_Dgv.Columns.Add("Item_Rate", GetType(String))
        M_Dgv.Columns.Add("Item_Value", GetType(String))
        M_Dgv.Rows.Add("", "", "", "", "", "", "", "")


        Dgv_Trans.DataSource = M_Dgv
        Dgv_Trans.DataBind()
        Session("M_Trans") = M_Dgv

    End Sub
    Private Function Valid_Data() As Boolean
        If M_MasterType = "TRANSFER" Then
            If CmbBranch.SelectedValue = CmbTransBranch.SelectedValue Then
                ShowAlertMessage("Same Branch Transfer selected,Can't Save")
                Return False
                Exit Function
            End If
        End If
        Return True
    End Function
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub TxtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtQty.TextChanged
        TxtValue.Text = Val(TxtQty.Text) * Val(TxtRate.Text)
    End Sub

    Protected Sub TxtRate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRate.TextChanged
        TxtValue.Text = Val(TxtQty.Text) * Val(TxtRate.Text)
    End Sub

    Private Function Validate_ItemAdd() As Boolean
        If Val(TxtID.Text) = 0 Then
            ShowAlertMessage("Enter ID")
            Return False
            Exit Function
        End If
        If Request.QueryString("RptType") <> "TRANSFER" Then

            If Trim(TxtDealer.Text) = "" Then
                ShowAlertMessage("Dealer is not entered")
                TxtDealer.Focus()
                Return False
                Exit Function
            End If
        End If
       
        If Trim(TxtItemName.Text) = "" Then
            ShowAlertMessage("Item Code not entered")
            TxtItemName.Focus()
            Return False
            Exit Function
        End If

        If Val(TxtQty.Text) = 0 Then
            ShowAlertMessage("Quantity not entered")
            TxtQty.Focus()
            Return False
            Exit Function
        End If
        'If Val(TxtRate.Text) = 0 Then
        '    ShowAlertMessage("Rate not entered")
        'TxtRate.Focus()
        'Return False
        'Exit Function
        'End If
        Return True
    End Function

    Protected Sub TxtID_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtID.TextChanged
        Try
            Set_Grid()
            Dim M_S As New DataTable
            Dim M_FATransBL As New FATransBL
            M_S = M_FATransBL.Fill_PurchaseTrans(M_TranType, Val(TxtID.Text))
            If (M_S.Rows.Count = 0) Then
                M_S = DirectCast(Session("M_Trans"), DataTable)
                Dgv_Trans.DataSource = M_S
                Dgv_Trans.DataBind()
            Else
                Dgv_Trans.DataSource = M_S
                Dgv_Trans.DataBind()
                Session("M_Trans") = M_S
            End If


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub


    Protected Sub CmdAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdAdd.Click
        Try
            If Validate_ItemAdd() = False Then
                Exit Sub
            End If
            Dim DT As DataTable = DirectCast(Session("M_Trans"), DataTable)
            Try
                Dim Expression As String = "Item_Code=''"
                If DT.[Select](Expression).Count > 0 Then DT.Rows.RemoveAt(DT.Rows.IndexOf(DT.[Select](Expression)(0)))
            Catch ex As Exception
                ShowAlertMessage(Err.Description)
            End Try

            DT.Rows.Add(TxtItemCode.Text, TxtItemName.Text, CmbRefNo.Text, CmbDept.SelectedValue, CmbDept.SelectedItem.Text, Val(TxtQty.Text), Val(TxtRate.Text), Val(TxtValue.Text))
            Dgv_Trans.DataSource = DT
            Dgv_Trans.DataBind()
            Session("M_Trans") = DT
            Clear_ItemDtls()


        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub



    Protected Sub TxtItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtItemCode.TextChanged
        Try
            'Dim str As String
            'Dim dt As New DataTable
            'str = "select FAItem_Name from FAItem_M_Tbl where FAItem_Code='" & TxtItemCode.Text & "'"
            'DB_Con.ExecuteDataTable(dt, str, False)
            'TxtItemName.Text = dt.Rows(0)(0).ToString()
            Try
                Dim Expression As String = "FAItem_Code='" & TxtItemCode.Text & "'"
                Dim drFiltered As DataRow() = AssetDt.[Select](Expression)

                If drFiltered.Count > 0 Then
                    TxtItemName.Text = drFiltered(0)("FAItem_Name")
                Else
                    TxtItemName.Text = ""
                    ShowAlertMessage("Invalid A/c Head")
                End If

            Catch ex As Exception
                ShowAlertMessage(ex.Message)
            End Try
            'TxtItemName.Text = Get_ItemName(TxtItemCode.Text)
           
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

  
    Private Function Get_ItemName(ByVal Code As String) As String
        Try
            Dim M_FAItemBL As New FAItemBL
            Return M_FAItemBL.Get_ItemName(Val(TxtItemCode.Text))
            M_FAItemBL = Nothing
        Catch ex As Exception

        End Try
    End Function
   

    Private Function Get_NextNO(ByVal Tran_Type As String) As String
        Try
            Dim M_TRanBL As New FATransBL
            Return M_TRanBL.Fill_NextNumber(Tran_Type)
            M_TRanBL = Nothing
        Catch ex As Exception

        End Try
    End Function


    Protected Sub CmdClear1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClear1.Click
        Clear_Values()
    End Sub


    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
End Class
