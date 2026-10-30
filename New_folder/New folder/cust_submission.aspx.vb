Imports SMARTCORE_Common.CommonClass
Imports SMARTCORE_BL

Partial Public Class cust_submission

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'Load_DefaultValues()
                'Set_Grid()
                Session("SearchType") = "CUSTOMER"
                ImgSrch.Attributes.Add("OnClick", "window.open('Srch.aspx','MyWindow','height=500,width=750,scrollbars=yes');return false;")
            End If
            If Session("SearchResult") <> "" Then
                Txt_CustId.Text = Session("SearchResult")
                Txt_CustId_TextChanged(Me.Txt_CustId, System.EventArgs.Empty)
                Session("SearchResult") = ""
                cust_grid()
           
            End If
        Catch ex As Exception

        End Try



    End Sub

    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdSave.Click

        Try
            Dim cust_id As String = Txt_CustId.Text
            Dim cust_name As String = Txt_CustName.Text
            Dim cust_addres As String = Txt_CustAddress.Text
            Dim subdate As String = Txt_subdate.Text
            Dim M_CommanBL As New CommonBL
            Dim M_Datatable As New DataTable()
            Dim msg As String
            If Len(Trim(Txt_subdate.Text)) = 0 Then
                Txt_subdate.Focus()
                ErrorMessage("INVALID DATE OF SUB")
            End If
            Dim maker_id As String = Session("UserID")
            M_Datatable = M_CommanBL.insertprototype(cust_id, subdate, maker_id)
            'M_CommanBL.insertprototype(cust_id, subdate, "")
            msg = Convert.ToString(M_Datatable.Rows(0)(0))
            ShowAlertMessage(msg)
            Txt_CustId.Text = ""
            Txt_CustName.Text = ""
            Txt_CustAddress.Text = ""
            Txt_subdate.Text = ""

        Catch ex As Exception

        End Try


    End Sub

    Protected Sub ImgSrch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSrch.Click

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
            'Dim cust_id As String = Txt_CustId.Text
            'Dim cust_name As String = Txt_CustName.Text
            'Dim subdate As String = Txt_subdate.Text

            ''Dim Add_Address1 As String = Txt_CustAddress.Text
            ''Dim Add_HouseNo As String = Txt_CustAddress.Text
            ''Dim Add_Address2 As String = Txt_CustAddress.Text
            ''Dim Add_Taluk As String = Txt_CustAddress.Text
            ''Dim Add_Address3 As String = Txt_CustAddress.Text

            'Dim M_CommanBL As New CommonBL()
            'Dim M_Datatable As New DataTable()
            'M_Datatable = M_CommanBL.get_custgrid(cust_id, cust_name, subdate)
            'Grid_custsub.DataSource = M_Datatable
            'Grid_custsub.DataBind()
            cust_grid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Txt_CustId_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Txt_CustId.TextChanged
        Try
            Dim M_CustMaster As New SMARTCORE_BO.CustMasterBO
            Dim M_PresAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_PermAddress As New SMARTCORE_BO.CustAddressBO
            Dim M_AddProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_PhotoProof As New SMARTCORE_BO.CustIdentityBO
            Dim M_CustMasterBL As New CustMasterBL
            M_CustMaster = M_CustMasterBL.Locate_Data(Txt_CustId.Text, M_PresAddress, M_PermAddress, M_PhotoProof, M_AddProof)
            With M_CustMaster

                'CmbAdmBranch.SelectedValue = .BrCode
                Txt_CustName.Text = .CustName
                Txt_CustAddress.Text = M_PermAddress.AddAddress1 + "," + M_PermAddress.AddAddress2 + "," + M_PermAddress.AddAddress3 + "," + M_PermAddress.AddPostoffice + "," + M_PermAddress.AddPinCode
                'TxtPermLand.Text = 
                'TxtPermPin.Text = 
                'TxtPermHouseNo.Text = M_PresAddress.AddHouseNo
                'TxtPermDist.Text = M_PresAddress.AddDist
                'TxtPermCountry.Text = M_PresAddress.AddCountry
                'cust_show.cust_grid()
            End With
            'CmdShow_Click(Me.CmdShow, System.EventArgs.Empty)

            M_CustMaster = Nothing
            M_PresAddress = Nothing
            M_PermAddress = Nothing
            M_CustMasterBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try

    End Sub
    Public Shared Sub ErrorMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alertify.error('" & [error] & "');", True)
        End If
    End Sub
    Public Sub cust_grid()

        Try
            Dim cust_id As String = Txt_CustId.Text
            Dim cust_name As String = Txt_CustName.Text
            Dim subdate As String = Txt_subdate.Text

            'Dim Add_Address1 As String = Txt_CustAddress.Text
            'Dim Add_HouseNo As String = Txt_CustAddress.Text
            'Dim Add_Address2 As String = Txt_CustAddress.Text
            'Dim Add_Taluk As String = Txt_CustAddress.Text
            'Dim Add_Address3 As String = Txt_CustAddress.Text

            Dim M_CommanBL As New CommonBL()
            Dim M_Datatable As New DataTable()
            M_Datatable = M_CommanBL.get_custgrid(cust_id)
            M_CommanBL = Nothing
            Grid_D.DataSource = M_Datatable
            Grid_D.DataBind()
        Catch ex As Exception

        End Try
    End Sub

End Class