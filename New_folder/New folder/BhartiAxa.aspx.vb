Imports System.Data
Imports SMARTCORE_DAL
Imports SMARTCORE_Common
Imports SMARTCORE_DA
Imports SMARTCORE_BL



Public Class BhartiAxa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try
            If Not IsPostBack Then
                Dim M_CommonBL As New CommonBL
                Dim M_dt As New DataTable
              
                'M_CommonBL.Fill_UserWiseRegion(DrpRegn, Session("UserID"), System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                'M_CommonBL.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                'M_CommonBL.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
                ''Region Wise
                'DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
                'DdlBranch.Items.Add("ALL")
                DtFrom.Text = Session("TransDate")
                DtTo.Text = Session("TransDate")




            End If

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub CmdShow_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdShow.Click
        Try

            Dim M_Sql As String
            Dim M_Dt As New DataTable()

            Dim StrSlf As String = ""


            'Dim Br_code As String = DdlBranch.SelectedValue
            Dim From_Date As String = DtFrom.Text
            Dim To_date As String = DtTo.text

            Dim User_Id As String = Session("UserID")
            'Dim Sch_code As String = DdlScheme.SelectedValue
            'Dim Section As String = Ddlsection.SelectedValue



            Dim HeaderArr(26, 4) As String
            Dim HeaderWidth(26, 1) As String

            HeaderArr(0, 0) = "SLN0"
            HeaderArr(0, 1) = "L"
            HeaderArr(0, 2) = "15"
            HeaderArr(0, 3) = "15"

            HeaderArr(1, 0) = "Salutation"
            HeaderArr(1, 1) = "L"
            HeaderArr(1, 2) = "15"
            HeaderArr(1, 3) = "15"


            HeaderArr(2, 0) = "Cust_Name"
            HeaderArr(2, 1) = "L"
            HeaderArr(2, 2) = "15"
            HeaderArr(2, 3) = "15"

            HeaderArr(3, 0) = "Full_Address"
            HeaderArr(3, 1) = "L"
            HeaderArr(3, 2) = "15"
            HeaderArr(3, 3) = "15"

            HeaderArr(4, 0) = "Add_HouseNo"
            HeaderArr(4, 1) = "L"
            HeaderArr(4, 2) = "15"
            HeaderArr(4, 3) = "15"

            HeaderArr(5, 0) = "PostOffice"
            HeaderArr(5, 1) = "L"
            HeaderArr(5, 2) = "15"
            HeaderArr(5, 3) = "15"

            HeaderArr(6, 0) = "District"
            HeaderArr(6, 1) = "L"
            HeaderArr(6, 2) = "16"
            HeaderArr(6, 3) = "16"

            HeaderArr(7, 0) = "Add_PinCode"
            HeaderArr(7, 1) = "L"
            HeaderArr(7, 2) = "15"
            HeaderArr(7, 3) = "15"

            HeaderArr(8, 0) = "State"
            HeaderArr(8, 1) = "L"
            HeaderArr(8, 2) = "15"
            HeaderArr(8, 3) = "15"

            HeaderArr(9, 0) = "Gender"
            HeaderArr(9, 1) = "L"
            HeaderArr(9, 2) = "15"
            HeaderArr(9, 3) = "15"


         
            HeaderArr(10, 0) = "Status"
            HeaderArr(10, 1) = "L"
            HeaderArr(10, 2) = "15"
            HeaderArr(10, 3) = "15"

            HeaderArr(11, 0) = "Mobile"
            HeaderArr(11, 1) = "L"
            HeaderArr(11, 2) = "15"
            HeaderArr(11, 3) = "15"

            HeaderArr(12, 0) = "Email"
            HeaderArr(12, 1) = "L"
            HeaderArr(12, 2) = "15"
            HeaderArr(12, 3) = "15"


            HeaderArr(13, 0) = "DOB"
            HeaderArr(13, 1) = "L"
            HeaderArr(13, 2) = "15"
            HeaderArr(13, 3) = "15"

            HeaderArr(14, 0) = "PanCard"
            HeaderArr(14, 1) = "L"
            HeaderArr(14, 2) = "15"
            HeaderArr(14, 3) = "15"

            HeaderArr(15, 0) = "StateCode"
            HeaderArr(15, 1) = "L"
            HeaderArr(15, 2) = "15"
            HeaderArr(15, 3) = "15"

            HeaderArr(16, 0) = "CustId"
            HeaderArr(16, 1) = "L"
            HeaderArr(16, 2) = "16"
            HeaderArr(16, 3) = "16"

            HeaderArr(17, 0) = "Policy Start Date"
            HeaderArr(17, 1) = "L"
            HeaderArr(17, 2) = "15"
            HeaderArr(17, 3) = "15"

            HeaderArr(18, 0) = "Policy End Date"
            HeaderArr(18, 1) = "L"
            HeaderArr(18, 2) = "15"
            HeaderArr(18, 3) = "15"

            HeaderArr(19, 0) = "IMD Code"
            HeaderArr(19, 1) = "L"
            HeaderArr(19, 2) = "15"
            HeaderArr(19, 3) = "15"

            HeaderArr(20, 0) = "Staff Code"
            HeaderArr(20, 1) = "L"
            HeaderArr(20, 2) = "15"
            HeaderArr(20, 3) = "15"

            HeaderArr(21, 0) = "Ref Field 1 (WLI No)"
            HeaderArr(21, 1) = "L"
            HeaderArr(21, 2) = "25"
            HeaderArr(21, 3) = "15"

            HeaderArr(22, 0) = "Ref Field 2 (Receipt)"
            HeaderArr(22, 1) = "L"
            HeaderArr(22, 2) = "15"
            HeaderArr(22, 3) = "15"

            HeaderArr(23, 0) = "Master Policy No"
            HeaderArr(23, 1) = "L"
            HeaderArr(23, 2) = "15"
            HeaderArr(23, 3) = "15"

            HeaderArr(24, 0) = "SI"
            HeaderArr(24, 1) = "L"
            HeaderArr(24, 2) = "15"
            HeaderArr(24, 3) = "15"

            HeaderArr(25, 0) = "Premium(Excl GST)"
            HeaderArr(25, 1) = "L"
            HeaderArr(25, 2) = "15"
            HeaderArr(25, 3) = "15"



            Session("RptHeader") = HeaderArr


            M_Sql = "EXEC Get_BhartiAxaDetails '" & Format(CDate(DtFrom.Text), "MM/dd/yyyy") & "','" & Format(CDate(DtTo.Text), "MM/dd/yyyy") & "' "
            Session("RptQry") = M_Sql
            Response.Redirect("~/PrnView.aspx?PageType=80", False)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdClose_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles CmdClose.Click
        Response.Redirect("~/Main.aspx")
    End Sub

    'Protected Sub BtnPro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnPro.Click
    '    Try
    '        Dim M_Dt As New DataTable
    '        Dim M_Sql As String = ""
    '        Dim Br_code As String = DdlBranch.SelectedValue
    '        Dim From_Date As String = DtFrom.Text

    '        'Dim User_Id As String = Session("UserID")
    '        Dim Sch_code As String = DdlScheme.SelectedValue
    '        Dim Section As String = Ddlsection.SelectedValue
    '        Dim SchType As String = DrpType.SelectedValue


    '        Dim M_DBConn As SMARTCORE_DAL.Connection = New SMARTCORE_DAL.Connection()

    '        M_Sql = "Exec Get_DpositDemandList_Sp '" + Convert.ToDateTime(From_Date).ToString("MM/dd/yyyy") + "' ,'" + Br_code + "','" & Sch_code & "','" & Session("HostID") & "','" & Session("UserID") & "','" & DrpRegn.SelectedValue & "','" & DrpArea.SelectedValue & "'"



    '        M_DBConn.ExecuteDataTable(M_Dt, M_Sql, False)
    '        ShowAlertMessage("REPORT PROCESSED SUCESSFULLY")
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub
    'Protected Sub DrpRegn_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpRegn.SelectedIndexChanged
    '    Try
    '        Dim M_CommonBl As New CommonBL
    '        M_CommonBl.Fill_UserWiseArea(DrpArea, Session("UserID"), DrpRegn.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
    '        Call DrpArea_SelectedIndexChanged(Me.DrpArea, System.EventArgs.Empty)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub DrpArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DrpArea.SelectedIndexChanged
    '    Try
    '        Dim M_CommonBl As New CommonBL
    '        M_CommonBl.Fill_UserWiseBranch(DdlBranch, Session("UserID"), DrpRegn.SelectedValue, DrpArea.SelectedValue, System.Web.HttpContext.Current.Session("ASSIGNEDBRANCH"))
    '        DdlBranch.Items.Remove(DdlBranch.Items.FindByText("ALL"))
    '        DdlBranch.Items.Add("ALL")
    '    Catch ex As Exception

    '    End Try
    'End Sub

End Class