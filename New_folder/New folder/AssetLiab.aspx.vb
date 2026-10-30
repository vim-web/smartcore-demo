Imports SMARTCORE_BL
Partial Public Class AssetLiab
    Inherits System.Web.UI.Page
    Shared BlankTbl As New DataTable
    Dim FormID As Integer = 1009

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hdnAccid.Value = Request.QueryString("CustID")
            hdnFilterOpt.Value = Request.QueryString("FilterOpt")
            If hdnFilterOpt.Value = "" Then hdnFilterOpt.Value = "ALL"
            Load_DefaultValues()
            Load_AssetLiab()
        End If
    End Sub

    Private Sub Load_DefaultValues()

        Try
            BlankTbl.Columns.Add("AL_Type", GetType(String))
            BlankTbl.Columns.Add("DirIndir", GetType(String))
            BlankTbl.Columns.Add("Br_Name", GetType(String))
            BlankTbl.Columns.Add("Acc_ID", GetType(String))
            BlankTbl.Columns.Add("Acc_Dtls", GetType(String))
            BlankTbl.Columns.Add("OP_Date", GetType(String))
            BlankTbl.Columns.Add("Due_Date", GetType(String))
            BlankTbl.Columns.Add("Bal_Amt", GetType(String))
            BlankTbl.Columns.Add("OD_Amt", GetType(String))
            BlankTbl.Columns.Add("Int_Amt", GetType(String))
            BlankTbl.Columns.Add("Section_Name", GetType(String))
            BlankTbl.Columns.Add("Module_Catg", GetType(String))
            BlankTbl.Rows.Add("", "", "", "", "", "", "", "", "")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Set_Grid(ByVal Dgv As Object)
        Dgv.DataSource = BlankTbl
        Dgv.DataBind()
    End Sub

    Private Sub Load_AssetLiab()

        Dim Dt As New DataTable
        Dim M_CommonBL As New CommonBL
        Dt = M_CommonBL.Get_AssetLiab(Val(hdnAccid.Value), Session("TransDate"), "xyz", hdnFilterOpt.Value)

        Dim dv_Asset As New DataView(Dt)
        dv_Asset.RowFilter = "AL_Type = 'A'"
        DgvAssets.DataSource = dv_Asset
        DgvAssets.DataBind()

        LblAssBal.Text = [String].Format("{0:n2}", Dt.Compute("Sum(Bal_Amt)", dv_Asset.RowFilter))

        If DgvAssets.Rows.Count = 0 Then
            Set_Grid(DgvAssets)
        End If

        Dim dv_OwnLiab As New DataView(Dt)
        dv_OwnLiab.RowFilter = "AL_Type = 'L' and DirIndir ='D'"
        DgvOwnLiab.DataSource = dv_OwnLiab
        DgvOwnLiab.DataBind()
        If DgvOwnLiab.Rows.Count = 0 Then
            Set_Grid(DgvOwnLiab)
        End If
        LblOwnLiab.Text = [String].Format("{0:n2}", Dt.Compute("Sum(Bal_Amt)", dv_OwnLiab.RowFilter))
        LblOwnOD.Text = [String].Format("{0:n2}", Dt.Compute("Sum(OD_Amt)", dv_OwnLiab.RowFilter))

        Dim dv_SurLiab As New DataView(Dt)
        dv_SurLiab.RowFilter = "AL_Type = 'L' and DirIndir ='I'"
        DgvSurLiab.DataSource = dv_SurLiab
        DgvSurLiab.DataBind()
        If DgvSurLiab.Rows.Count = 0 Then
            Set_Grid(DgvSurLiab)
        End If
        LblSurLiab.Text = [String].Format("{0:n2}", Dt.Compute("Sum(Bal_Amt)", dv_SurLiab.RowFilter))
        LblSurOD.Text = [String].Format("{0:n2}", Dt.Compute("Sum(OD_Amt)", dv_SurLiab.RowFilter))

        Dim RebateDt As New DataTable
        RebateDt = M_CommonBL.Get_CustomerRebate(Val(hdnAccid.Value))
        DgvRebate.DataSource = RebateDt
        DgvRebate.DataBind()

    End Sub

End Class