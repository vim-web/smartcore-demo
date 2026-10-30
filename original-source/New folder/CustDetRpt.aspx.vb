Imports SMARTCORE_BL
Imports SMARTCORE_DA
Public Class CustDetRpt
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Load_DefaultValues()
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
            Dim M_CommonBL As New CommonBL

            M_CommonBL.Fill_Branch(DrpBranch, True)
            M_CommonBL.Fill_Pickup(CmbCustType, 6, 0)
            M_CommonBL.Fill_Pickup(CmbLocn, 14, 0)
            M_CommonBL.Fill_Pickup(CmbOccupn, 4, 0)
            M_CommonBL.Fill_Pickup(CmbEducn, 13, 0)
            M_CommonBL.Fill_Pickup(CmbCaste, 2, 0)
            M_CommonBL.Fill_Pickup(CmbMaritalStatus, 10, 0)
            M_CommonBL.Fill_Pickup(CmbGender, 8, 0)
            M_CommonBL.Fill_Pickup(CmbReligion, 1, 0)
            M_CommonBL.Fill_Pickup(CmbCategory, 7, CmbCustType.SelectedValue)
            M_CommonBL = Nothing

            DrpBranch.SelectedValue = Session("DefaultBrCode")
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Protected Sub cmdshow_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cmdshow.Click
        Dim M_Sql As String
        Dim M_Dt As New DataTable()
        Dim Branch As String

        If DrpBranch.SelectedItem.Text = "<-- ALL -->" Then
            Branch = "0"
        Else
            Branch = DrpBranch.SelectedValue
        End If

     
        Dim HeaderArr(6, 4) As String
        Dim HeaderWidth(6, 1) As String
        HeaderArr(0, 0) = "CUST ID"
        HeaderArr(0, 1) = "L"
        HeaderArr(0, 2) = "10"
        HeaderArr(0, 3) = "15"

        HeaderArr(1, 0) = "NAME"
        HeaderArr(1, 1) = "L"
        HeaderArr(1, 2) = "10"
        HeaderArr(1, 3) = "15"

        HeaderArr(2, 0) = "MOTHER NAME"
        HeaderArr(2, 1) = "L"
        HeaderArr(2, 2) = "10"
        HeaderArr(2, 3) = "15"

        HeaderArr(3, 0) = "WARD"
        HeaderArr(3, 1) = "L"
        HeaderArr(3, 2) = "10"
        HeaderArr(3, 3) = "15"

        HeaderArr(4, 0) = "PANCHAYAT"
        HeaderArr(4, 1) = "L"
        HeaderArr(4, 2) = "10"
        HeaderArr(4, 3) = "15"

        HeaderArr(5, 0) = "ADDRESS"
        HeaderArr(5, 1) = "L"
        HeaderArr(5, 2) = "10"
        HeaderArr(5, 3) = "10"

        HeaderArr(6, 0) = "BRANCH"
        HeaderArr(6, 1) = "L"
        HeaderArr(6, 2) = "10"
        HeaderArr(6, 3) = "10"


        Session("RptHeader") = HeaderArr

        M_Sql = "EXEC Cust_Category_Rpt'" & Branch & "','" & IIf(ChkType.Checked, CmbCustType.SelectedValue, "0") & "','" & IIf(ChkCategory.Checked, CmbCategory.SelectedValue, "0") & "','" & IIf(ChkGender.Checked, CmbGender.SelectedValue, "0") & "','" & IIf(ChkReligion.Checked, CmbReligion.SelectedValue, "0") & "','" & IIf(ChkCaste.Checked, CmbCaste.SelectedValue, "0") & "','" & IIf(ChkLocn.Checked, CmbLocn.SelectedValue, "0") & "','" & IIf(ChkOccupn.Checked, CmbOccupn.SelectedValue, "0") & "','" & IIf(ChkEducn.Checked, CmbEducn.SelectedValue, "0") & "','" & IIf(ChkMarStatus.Checked, CmbMaritalStatus.SelectedValue, "0") & "','" & IIf(ChkPanchyt.Checked, TxtPanchayt.Text, "0") & "','" & IIf(Chkward.Checked, TxtCustWard.Text, "0") & "'"
        Session("RptQry") = M_Sql
        Response.Redirect("~/PrnView.aspx?PageType=80", False)
    End Sub

    Protected Sub ChkType_CheckedChanged(sender As Object, e As EventArgs) Handles ChkType.CheckedChanged
        If ChkType.Checked = True Then
            CmbCustType.Enabled = True
        Else
            CmbCustType.Enabled = False
        End If

    End Sub

    Protected Sub ChkCategory_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCategory.CheckedChanged
        If ChkCategory.Checked = True Then
            CmbCategory.Enabled = True
        Else
            CmbCategory.Enabled = False
        End If
    End Sub

    Protected Sub ChkGender_CheckedChanged(sender As Object, e As EventArgs) Handles ChkGender.CheckedChanged
        If ChkGender.Checked = True Then
            CmbGender.Enabled = True
        Else
            CmbGender.Enabled = False
        End If
    End Sub

    Protected Sub ChkReligion_CheckedChanged(sender As Object, e As EventArgs) Handles ChkReligion.CheckedChanged
        If ChkReligion.Checked = True Then
            CmbReligion.Enabled = True
        Else
            CmbReligion.Enabled = False
        End If
    End Sub

    Protected Sub ChkCaste_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCaste.CheckedChanged
        If ChkCaste.Checked = True Then
            CmbCaste.Enabled = True
        Else
            CmbCaste.Enabled = False
        End If
    End Sub

    Protected Sub ChkLocn_CheckedChanged(sender As Object, e As EventArgs) Handles ChkLocn.CheckedChanged
        If ChkLocn.Checked = True Then
            CmbLocn.Enabled = True
        Else
            CmbLocn.Enabled = False
        End If
    End Sub

    Protected Sub ChkOccupn_CheckedChanged(sender As Object, e As EventArgs) Handles ChkOccupn.CheckedChanged
        If ChkOccupn.Checked = True Then
            CmbOccupn.Enabled = True
        Else
            CmbOccupn.Enabled = False
        End If
    End Sub

    Protected Sub ChkEducn_CheckedChanged(sender As Object, e As EventArgs) Handles ChkEducn.CheckedChanged
        If ChkEducn.Checked = True Then
            CmbEducn.Enabled = True
        Else
            CmbEducn.Enabled = False
        End If
    End Sub

    Protected Sub ChkMarStatus_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMarStatus.CheckedChanged
        If ChkMarStatus.Checked = True Then
            CmbMaritalStatus.Enabled = True
        Else
            CmbMaritalStatus.Enabled = False
        End If
    End Sub

    Protected Sub CmdClose_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/main.aspx")
    End Sub
    Protected Sub ChkPanchyt_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChkPanchyt.CheckedChanged
        If ChkPanchyt.Checked = True Then
            TxtPanchayt.Enabled = True
        Else
            TxtPanchayt.Enabled = False
        End If
    End Sub

    Protected Sub Chkward_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Chkward.CheckedChanged
        If Chkward.Checked = True Then
            TxtCustWard.Enabled = True
        Else
            TxtCustWard.Enabled = False
        End If
    End Sub
End Class