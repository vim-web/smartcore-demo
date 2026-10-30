Imports SMARTCORE_BL
Imports SMARTCORE_Common
Partial Public Class Srch
    Inherits System.Web.UI.Page
    Private m_RtnControl As Object
    Private m_RtnCol As Int16
    Private m_FilterFld As String
    Private m_SortFld As String
    Private m_Qry As String
    Private m_Heading As String
    Private m_Datatable As New DataTable
    Private m_SetHeading As Boolean
    Private m_ColDetails As Array
    Private m_ColSelected As Integer
    Private m_Sch_Code As String
    Private m_Br_Code As String
    Private M_SectionName As String
    Dim FormID As Integer = 1133
    Private Sub PopulateData()
        Try
            Dim M_Query As String

            M_Query = Session("SearchQuery")
            If InStr(LCase(M_Query), "where") > 1 Then
                M_Query = M_Query & " and " & CmbFilter.SelectedValue & " like  '%" & Replace(TxtSearch.Text, "*", "%") & "%'"
            Else
                M_Query = M_Query & " where " & CmbFilter.SelectedValue & " like  '%" & Replace(TxtSearch.Text, "*", "%") & "%'"
            End If

            If Len(TxtSearch.Text) > 0 Then
                If InStr(LCase(M_Query), "where") > 1 Then
                    M_Query = M_Query & " and " & CmbFilter1.SelectedValue & " like  '%" & Replace(TxtSearch1.Text, "*", "%") & "%'"
                Else
                    M_Query = M_Query & " where " & CmbFilter1.SelectedValue & " like  '%" & Replace(TxtSearch1.Text, "*", "%") & "%'"
                End If
            End If
            If Len(TxtSearch2.Text) > 0 Then
                If InStr(LCase(M_Query), "where") > 1 Then
                    M_Query = M_Query & " and " & CmbFilter2.SelectedValue & " like  '%" & Replace(TxtSearch2.Text, "*", "%") & "%'"
                Else
                    M_Query = M_Query & " where " & CmbFilter2.SelectedValue & " like  '%" & Replace(TxtSearch2.Text, "*", "%") & "%'"
                End If
            End If
            'M_Query = M_Query & " order by " & CmbFilter.SelectedValue

            If Session("SearchType") = "Branch" Then
                M_Query = M_Query + " Order By cast(Br_Code as Int) Asc"
            End If
            If Session("SearchType") = "PICKUP" Then
                M_Query = M_Query + " order by Pkt_Code,cast(Pkc_Code as int) asc"
            End If
            Dim M_SearchBL As New SearchBL
            m_Datatable = M_SearchBL.ExecuteQuery(M_Query)

            DgvSearch.DataSource = m_Datatable
            Session("tSrchTbl") = m_Datatable
            DgvSearch.DataBind()
            M_SearchBL = Nothing
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub
    Private Sub LoggError(ByVal ee As Exception)
        Try
            Dim M_CommonCls As New CommonClass
            Call M_CommonCls.LogErrorToLogFile(ee, "")
            M_CommonCls = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ShowAlertMessage(ByVal [error] As String)
        Dim page As Page = TryCast(HttpContext.Current.Handler, Page)
        If page IsNot Nothing Then
            [error] = [error].Replace("'", "`")
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "err_msg", "alert('" & [error] & "');", True)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim SrchTbl As New DataTable("Table")
                SrchTbl.Columns.Add("COLNM", GetType(String))
                SrchTbl.Columns.Add("HEADING", GetType(String))
                m_Sch_Code = ""
                m_Br_Code = ""
                M_SectionName = ""
                'If Session("SearchScheme") <> Nothing Then m_Sch_Code = Session("SearchScheme")
                If Session("SearchBranch") <> Nothing Then m_Br_Code = Session("SearchBranch")
                ' If Session("SearchSection") <> Nothing Then M_SectionName = Session("SearchSection")



                If m_Sch_Code.Length = 0 Then m_Sch_Code = "%"
                If m_Br_Code.Length = 0 Then m_Br_Code = "%"
                If M_SectionName.Length = 0 Then M_SectionName = "%"

                Select Case Session("SearchType")
                    Case "CUSTOMER"
                        SrchTbl.Rows.Add("CUST_NAME", "NAME")
                        SrchTbl.Rows.Add("CUST_ID", "CUSTOMER ID")
                        SrchTbl.Rows.Add("CUST_RELNM", "RELATIVE")
                        SrchTbl.Rows.Add("add_aDDRESS1", "ADDRESS")
                        SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                        SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                        SrchTbl.Rows.Add("ID_Details", "ID Details")
                        'Session("SearchRtnKey") = "CUST_ID"
                        Session("SearchReturn") = 2
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "Select  TOP(200) CUST_NAME as Name,CUST_ID as ID,CUST_RELNM ,ADD_ADDRESS1 AS ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE,ID_Details as Cust_IDDetails from Cust_Details_Vw where Br_Code like '%" & m_Br_Code.ToString & "' "
                    Case "VENUE"
                        SrchTbl.Rows.Add("V_ID", "VENUE ID")
                        SrchTbl.Rows.Add("V_Name", "VENUE NAME")
                        SrchTbl.Rows.Add("V_Address", "VENUE ADDRESS")
                        SrchTbl.Rows.Add("V_PhoneNo", "PHONE NO")
                        SrchTbl.Rows.Add("V_ContactPName", "CONTACT PERSON")
                        SrchTbl.Rows.Add("V_MobileNo", "MOBILE NO")
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "Select  TOP(200) V_ID as VENUEID,V_Name as NAME,V_Address as ADDRESS ,V_PhoneNo AS PHONENO,V_ContactPName AS CONTACTNAME,V_MobileNo as MOBILENO from Venue_Detail_Vw"
                    Case "ACCOUNTNO", "TRACCOUNTNO"
                        SrchTbl.Rows.Add("Acc_No", "Account NO")
                        SrchTbl.Rows.Add("ACC_STATUS", "STATUS")
                        SrchTbl.Rows.Add("Cust_Name", "CUSTOMER NAME")
                        SrchTbl.Rows.Add("Cust_Id", "CUSTOMER ID")
                        SrchTbl.Rows.Add("Sch_Section_Name", "SECTION NAME")
                        SrchTbl.Rows.Add("SCH_NAME", "SCHEME NAME")
                        SrchTbl.Rows.Add("ADDRESS", "ADDRESS")
                        SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                        SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                        'Session("SearchRtnKey") = "CUST_ID"
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        'Session("SearchQuery") = "Select TOP(200) MemberNo AS MEMBER_NO,custname AS NAME,custid AS CUSTOMER_ID,sch_name AS SCHEME_NAME,ADDRESS from Member_Detail_Vw "
                        Session("SearchQuery") = "Select TOP(200) Acc_No AS [ACC NO],CASE WHEN ACC_STATUS ='Y' then 'OPEN' Else 'CLOSED' END as STATUS, Cust_Name AS NAME,Cust_Id AS [CUST ID],SCH_NAME AS SCHEME,ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE from Account_Detail_Vw Where  Br_Code Like '%" & m_Br_Code.ToString & "' "

                    Case "GOLDACCNO"
                        SrchTbl.Rows.Add("Acc_No", "Account NO")
                        SrchTbl.Rows.Add("Cust_Name", "CUSTOMER NAME")
                        SrchTbl.Rows.Add("Cust_Id", "CUSTOMER ID")
                        SrchTbl.Rows.Add("Sch_Section_Name", "SECTION NAME")
                        SrchTbl.Rows.Add("SCH_NAME", "SCHEME NAME")
                        SrchTbl.Rows.Add("ADDRESS", "ADDRESS")
                        SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                        SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                        'Session("SearchRtnKey") = "CUST_ID"	
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        'Session("SearchQuery") = "Select TOP(200) MemberNo AS MEMBER_NO,custname AS NAME,custid AS CUSTOMER_ID,sch_name AS SCHEME_NAME,ADDRESS from Member_Detail_Vw "	
                        Session("SearchQuery") = "Select TOP(200) Acc_No AS [ACC NO], Cust_Name AS NAME,Cust_Id AS [CUST ID],SCH_NAME AS SCHEME,ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE from Account_Detail_Vw Where  Br_Code = '" & m_Br_Code & "'  AND Sch_Code like '%" & m_Sch_Code & "' AND SCH_MODULE_CATG='GOLDLOAN' AND ACC_STATUS ='Y'"



                    Case "ACCNO"
                        SrchTbl.Rows.Add("Acc_No", "Account NO")
                        SrchTbl.Rows.Add("ACC_STATUS", "STATUS")
                        SrchTbl.Rows.Add("Cust_Name", "CUSTOMER NAME")
                        SrchTbl.Rows.Add("Cust_Id", "CUSTOMER ID")
                        SrchTbl.Rows.Add("Sch_Section_Name", "SECTION NAME")
                        SrchTbl.Rows.Add("SCH_NAME", "SCHEME NAME")
                        SrchTbl.Rows.Add("ADDRESS", "ADDRESS")

                        SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                        SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                        'Session("SearchRtnKey") = "CUST_ID"
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        'Session("SearchQuery") = "Select TOP(200) MemberNo AS MEMBER_NO,custname AS NAME,custid AS CUSTOMER_ID,sch_name AS SCHEME_NAME,ADDRESS from Member_Detail_Vw "
                        Session("SearchQuery") = "Select TOP(200) Acc_No AS [ACC NO],CASE WHEN ACC_STATUS ='Y' then 'OPEN' Else 'CLOSED' END as STATUS, Cust_Name AS NAME,Cust_Id AS [CUST ID],SCH_NAME AS SCHEME,ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE from Account_Detail_Vw Where  Br_Code Like '%" & m_Br_Code.ToString & "' and Sch_Code like '%" & m_Sch_Code.ToString & "'  and Sch_Section_Name like '%" & M_SectionName.ToString & "' "

                    Case "ACCID_NO"
                        SrchTbl.Rows.Add("Acc_No", "Account NO")
                        SrchTbl.Rows.Add("Cust_Name", "CUSTOMER NAME")
                        SrchTbl.Rows.Add("Cust_Id", "CUSTOMER ID")
                        SrchTbl.Rows.Add("SCH_NAME", "SCHEME NAME")
                        SrchTbl.Rows.Add("ADDRESS", "ADDRESS")
                        'Session("SearchRtnKey") = "CUST_ID"
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        'Session("SearchQuery") = "Select TOP(200) MemberNo AS MEMBER_NO,custname AS NAME,custid AS CUSTOMER_ID,sch_name AS SCHEME_NAME,ADDRESS from Member_Detail_Vw "
                        Session("SearchQuery") = "Select TOP(200) Acc_No,Cust_Name,Cust_Id,SCH_NAME,ADDRESS,ACC_ID ,Sch_Code from Account_Detail_Vw Where Sch_Code Like '" & m_Sch_Code.ToString & "'"
                    Case "EMPLOYEE"
                        SrchTbl.Rows.Add("Emp_ID", "Employee ID")
                        SrchTbl.Rows.Add("Emp_Name", "Employee NAME")
                        Session("SearchColDtls") = SrchTbl
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "Select TOP(200) Emp_ID,Emp_Name from Employee_M_Tbl"
                    Case "StID"
                        SrchTbl.Rows.Add("St_ID", "Standing Id")
                        SrchTbl.Rows.Add("St_Status", "Status")
                        SrchTbl.Rows.Add("De_Acc_No", "Standing From Acc No")
                        Session("SearchColDtls") = SrchTbl
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "Select TOP(200) St_ID,St_Status,De_Acc_No from Gen_StandingInstruction_M_Tbl"
                    Case "StID"
                        SrchTbl.Rows.Add("St_ID", "Standing Id")
                        SrchTbl.Rows.Add("St_Status", "Status")
                        SrchTbl.Rows.Add("De_Acc_No", "Standing From Acc No")
                        Session("SearchColDtls") = SrchTbl
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "Select TOP(200) St_ID,St_Status,De_Acc_No from Gen_StandingInstruction_M_Tbl"
                    Case "ACCOUNTHEAD"
                        SrchTbl.Rows.Add("Acc_Name", "NAME")
                        SrchTbl.Rows.Add("Acc_Code", "ACCOUNT CODE")
                        SrchTbl.Rows.Add("Acc_ShortName", "SHORT NAME")
                        'Session("SearchRtnKey") = "CUST_ID"
                        Session("SearchReturn") = 2
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "Select TOP(200) Acc_Name,Acc_Code,Acc_ShortName  From Gen_Accountmaster_P_Tbl "
                    Case "Scheme"
                        SrchTbl.Rows.Add("Sch_Name", "NAME")
                        SrchTbl.Rows.Add("Sch_Code", "Scheme CODE")
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "select TOP(200) Sch_Code,Sch_Name,Sch_ShortName from [dbo].[Gen_SchemeMaster_P_Tbl] "
                    Case "Branch"
                        SrchTbl.Rows.Add("Br_Name", "BRANCH")
                        SrchTbl.Rows.Add("Br_Code", "BRANCH CODE")
                        Session("SearchReturn") = 2
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "select Br_Name,Br_Code from Gen_BranchDetails_P_Tbl"
                    Case "AssetGrp"
                        SrchTbl.Rows.Add("Grp_Code", "GROUP CODE")
                        SrchTbl.Rows.Add("Grp_Name", "GROUP NAME")
                        SrchTbl.Rows.Add("Grp_ShortName", "SHORT NAME")
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = "select top 200 Grp_Code,Grp_Name,Grp_ShortName from FAGroup_M_Tbl"
                    Case "ASSET"
                        SrchTbl.Rows.Add("I.FAItem_Code", "ASSET CODE")
                        SrchTbl.Rows.Add("I.FAItem_Name", "ASSET NAME")
                        SrchTbl.Rows.Add("G.Grp_Name", "GROUP NAME")
                        SrchTbl.Rows.Add("G.Grp_Code", "GROUP CODE")
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = " Select TOP 200 I.FAItem_Code,I.FAItem_Name,G.Grp_Name,G.Grp_Code from FAItem_M_Tbl I JOIN FAGroup_M_Tbl G ON G.Grp_Code=I.FAItem_GroupCode where FAItem_Br_Code like '" & m_Br_Code & "' "
                    Case "USER"
                        SrchTbl.Rows.Add("User_Emp_ID", "EMP ID")
                        SrchTbl.Rows.Add("User_Name", "USER NAME")
                        SrchTbl.Rows.Add("Emp_Name", "EMPLOYEE NAME")
                        SrchTbl.Rows.Add("Cust_Id", "CUST ID")
                        SrchTbl.Rows.Add("Br_Name", "BRANCH")
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = " select User_Emp_ID,User_Name,Emp_Name,Cust_Id,Br_Name  from dbo.Get_Users_Fn('') "
                    Case "PICKUP"
                        SrchTbl.Rows.Add("Pkc_Code", "PICKUP CODE")
                        SrchTbl.Rows.Add("Pkc_Description", "PICKUP DESCRIPTION")
                        SrchTbl.Rows.Add("Pkt_Description", "PICKUP TYPE")
                        Session("SearchReturn") = 1
                        Session("SearchResult") = ""
                        Session("SearchQuery") = " select Top 200 Pkc_Code,Pkc_Description,Pkt_Description from pickup_view "
                    Case "CANVASEDBY"

                        SrchTbl.Rows.Add("NAME", "NAME")
                        SrchTbl.Rows.Add("ID", "ID")
                        SrchTbl.Rows.Add("ADDRESS", "ADDRESS")
                        SrchTbl.Rows.Add("CONTACTNO", "CONTACTNO")
                        Session("SearchReturn") = 2
                        Session("SearchReturn1") = 1
                        Session("SearchResult") = ""
                        Session("SearchResult1") = ""
                        Session("SearchQuery") = "select Top (200) NAME,ID,ADDRESS,CONTACTNO from GetCanvasedByDetails_fn()"
                        Session("SearchType_1") = "CANVASEDBY"
                    Case "SUERTYSRC"
                        If Request.QueryString("SrType") = "ACCOUNTNO" Then
                            SrchTbl.Rows.Add("Acc_No", "Account NO")
                            SrchTbl.Rows.Add("ACC_STATUS", "STATUS")
                            SrchTbl.Rows.Add("Cust_Name", "CUSTOMER NAME")
                            SrchTbl.Rows.Add("Cust_Id", "CUSTOMER ID")
                            SrchTbl.Rows.Add("Sch_Section_Name", "SECTION NAME")
                            SrchTbl.Rows.Add("SCH_NAME", "SCHEME NAME")
                            SrchTbl.Rows.Add("ADDRESS", "ADDRESS")
                            SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                            SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                            'Session("SearchRtnKey") = "CUST_ID"
                            Session("SearchReturn") = 1
                            Session("SearchResult") = ""
                            'Session("SearchQuery") = "Select TOP(200) MemberNo AS MEMBER_NO,custname AS NAME,custid AS CUSTOMER_ID,sch_name AS SCHEME_NAME,ADDRESS from Member_Detail_Vw "
                            Session("SearchQuery") = "Select TOP(200) Acc_No AS [ACC NO],CASE WHEN ACC_STATUS ='Y' then 'OPEN' Else 'CLOSED' END as STATUS, Cust_Name AS NAME,Cust_Id AS [CUST ID],SCH_NAME AS SCHEME,ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE from Account_Detail_Vw Where  Br_Code Like '%" & m_Br_Code.ToString & "' "
                            Session("SearchType_1") = "ACCOUNTNO"
                        ElseIf Request.QueryString("SrType") = "CUSTOMER" Then
                            SrchTbl.Rows.Add("CUST_NAME", "NAME")
                            SrchTbl.Rows.Add("CUST_ID", "CUSTOMER ID")
                            SrchTbl.Rows.Add("CUST_RELNM", "RELATIVE")
                            SrchTbl.Rows.Add("add_aDDRESS1", "ADDRESS")
                            SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                            SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                            SrchTbl.Rows.Add("ID_Details", "ID Details")
                            'Session("SearchRtnKey") = "CUST_ID"
                            Session("SearchReturn") = 2
                            Session("SearchResult") = ""
                            Session("SearchQuery") = "Select  TOP(200) CUST_NAME as Name,CUST_ID as ID,CUST_RELNM ,ADD_ADDRESS1 AS ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE,ID_Details as Cust_IDDetails from Cust_Details_Vw where Br_Code like '%" & m_Br_Code.ToString & "' "
                            Session("SearchType_1") = "CUSTOMER"
                        End If
                    Case "CUSTOMERREGISTER"
                        If Request.QueryString("SrType") = "CUSTOMER1" Then
                            SrchTbl.Rows.Add("CUST_NAME", "NAME")
                            SrchTbl.Rows.Add("CUST_ID", "CUSTOMER ID")
                            SrchTbl.Rows.Add("Sm_MemNo", "ACCOUNT NO")

                            SrchTbl.Rows.Add("Sch_Name", "SCHEME")

                            SrchTbl.Rows.Add("CUST_RELNM", "RELATIVE")
                            SrchTbl.Rows.Add("add_aDDRESS1", "ADDRESS")
                            SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                            SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                            SrchTbl.Rows.Add("ID_Details", "ID Details")
                            'Session("SearchRtnKey") = "CUST_ID"
                            Session("SearchReturn") = 2
                            Session("SearchResult") = ""
                            Session("SearchQuery") = "Select  TOP(200) CUST_NAME as Name,C.CUST_ID as ID,S.Sm_MemNo,SH.Sch_Name,CUST_RELNM ,ADD_ADDRESS1 AS ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE,ID_Details as Cust_IDDetails from Cust_Details_Vw C join Share_Master_M_Tbl S on S.Cust_Id=C.Cust_Id join Gen_SchemeMaster_P_Tbl SH on S.Sch_Code=SH.Sch_Code"
                            Session("SearchType_1") = "CUSTOMER1"

                        ElseIf Request.QueryString("SrType") = "CUSTOMER2" Then
                            SrchTbl.Rows.Add("CUST_NAME", "NAME")
                            SrchTbl.Rows.Add("CUST_ID", "CUSTOMER ID")
                            SrchTbl.Rows.Add("CUST_RELNM", "RELATIVE")
                            SrchTbl.Rows.Add("add_aDDRESS1", "ADDRESS")
                            SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                            SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                            SrchTbl.Rows.Add("ID_Details", "ID Details")
                            'Session("SearchRtnKey") = "CUST_ID"
                            Session("SearchReturn") = 2
                            Session("SearchResult") = ""
                            Session("SearchQuery") = "Select  TOP(200) CUST_NAME as Name,CUST_ID as ID,CUST_RELNM ,ADD_ADDRESS1 AS ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE,ID_Details as Cust_IDDetails from Cust_Details_Vw"
                            Session("SearchType_1") = "CUSTOMER2"
                        ElseIf Request.QueryString("SrType") = "CUSTOMER3" Then
                            SrchTbl.Rows.Add("CUST_NAME", "NAME")
                            SrchTbl.Rows.Add("CUST_ID", "CUSTOMER ID")
                            SrchTbl.Rows.Add("CUST_RELNM", "RELATIVE")
                            SrchTbl.Rows.Add("add_aDDRESS1", "ADDRESS")
                            SrchTbl.Rows.Add("Cust_Phone1", "PHONE")
                            SrchTbl.Rows.Add("Cust_Mobile", "MOBILE")
                            SrchTbl.Rows.Add("ID_Details", "ID Details")
                            'Session("SearchRtnKey") = "CUST_ID"
                            Session("SearchReturn") = 2
                            Session("SearchResult") = ""
                            Session("SearchQuery") = "Select  TOP(200) CUST_NAME as Name,CUST_ID as ID,CUST_RELNM ,ADD_ADDRESS1 AS ADDRESS,Cust_Phone1 as PHONE,Cust_Mobile AS MOBILE,ID_Details as Cust_IDDetails from Cust_Details_Vw"
                            Session("SearchType_1") = "CUSTOMER3"
                        End If
                End Select

                If Session("SearchQuery") <> "" Then
                    Dim i As Integer

                    Dim Item As ListItem
                    CmbFilter.Items.Clear()
                    For i = 0 To SrchTbl.Rows.Count - 1
                        Item = New ListItem
                        Item.Text = SrchTbl.Rows(i).Item("HEADING")
                        Item.Value = SrchTbl.Rows(i).Item("COLNM")
                        CmbFilter.Items.Add(Item)
                        CmbFilter1.Items.Add(Item)
                        CmbFilter2.Items.Add(Item)
                    Next
                    PopulateData()
                End If
            End If
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
            LoggError(ex)
        End Try
    End Sub

    Protected Sub CmbFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbFilter.SelectedIndexChanged, CmbFilter1.SelectedIndexChanged, CmbFilter2.SelectedIndexChanged
        PopulateData()
    End Sub

    Protected Sub TxtSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSearch.TextChanged, TxtSearch1.TextChanged, TxtSearch2.TextChanged
        Try
            Call PopulateData()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub DgvSearch_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DgvSearch.PageIndexChanging
        Try
            DgvSearch.PageIndex = e.NewPageIndex
            m_Datatable = DirectCast(Session("tSrchTbl"), DataTable)
            DgvSearch.DataSource = m_Datatable
            DgvSearch.DataBind()
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub DgvSearch_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles DgvSearch.RowEditing
        'Dim RtnCol As Integer = Session("SearchReturn")
        'Dim SrchResult As String = DgvSearch.Rows(e.NewEditIndex).Cells(RtnCol).Text
        'Session("SearchResult") = SrchResult
        ''Response.Write("<script language='javascript'>self.close();</script>")
        'ScriptManager.RegisterStartupScript(Me.UpdatePanel1, GetType(String), "script1", "window.opener.document.forms[0].submit(); self.close();", True)
    End Sub

    Private Sub DgvSearch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgvSearch.SelectedIndexChanged
        Try
            Dim RtnCol As Integer = Session("SearchReturn")
            Dim SrchResult As String = Replace(DgvSearch.SelectedRow.Cells(RtnCol).Text, "&nbsp;", "")
            Session("SearchResult") = SrchResult
            ScriptManager.RegisterStartupScript(Me.UpdatePanel1, GetType(String), "script1", "window.opener.document.forms[0].submit(); self.close();", True)
        Catch ex As Exception
            ShowAlertMessage(ex.Message)
        End Try
    End Sub

    Private Sub DgvSearch_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles DgvSearch.SelectedIndexChanging
    End Sub


End Class