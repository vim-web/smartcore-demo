Imports SMARTCORE_DAL
Imports SMARTCORE_BO
Imports SMARTCORE_Common
Public Class CustMasterDA
    Private M_DBConn As Connection
    Private M_CustId As Integer
    Public Function Save_Data(ByVal M_CustMaster As CustMasterBO, ByVal M_PresAddress As CustAddressBO, ByVal M_PermAddress As CustAddressBO, ByVal M_PhotoIdentity As CustIdentityBO, ByVal M_AddIdentity As CustIdentityBO, ByVal M_EntryMode As String) As Integer
        Try
            Dim M_Sql As String

            With M_CustMaster
                M_Sql = "Exec InsertUpdate_Cust_Master_M_Tbl "
                M_Sql = M_Sql & "'" & M_EntryMode & "',"
                M_Sql = M_Sql & .CustId & ","
                M_Sql = M_Sql & "'" & .BrCode & "',"
                M_Sql = M_Sql & .CustTypeCode & ","
                M_Sql = M_Sql & .CustCatgCode & ","
                M_Sql = M_Sql & .CustTitleCode & ","
                M_Sql = M_Sql & "'" & .CustName & "',"
                M_Sql = M_Sql & "'" & .CustFirstName & "',"
                M_Sql = M_Sql & "'" & .CustMiddleName & "',"
                M_Sql = M_Sql & "'" & .CustLastName & "',"
                M_Sql = M_Sql & "'" & .CustShortName & "',"
                M_Sql = M_Sql & .CustGender & ","
                M_Sql = M_Sql & .CustRelTypCode & ","
                M_Sql = M_Sql & "'" & .CustRelNm & "',"
                M_Sql = M_Sql & .CustMinor & ","
                M_Sql = M_Sql & "'" & .CustGuardianId & "',"
                M_Sql = M_Sql & .CustGuardianRelationCode & ","
                M_Sql = M_Sql & "'" & Format(.CustDobInc, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & .CustAge & ","
                M_Sql = M_Sql & .CustReligionCode & ","
                M_Sql = M_Sql & .CustCasteCode & ","
                M_Sql = M_Sql & .CustOccupationCode & ","
                M_Sql = M_Sql & .CustEducationCode & ","
                M_Sql = M_Sql & "'" & .CustStaff & "',"
                M_Sql = M_Sql & .CustStaffId & ","
                M_Sql = M_Sql & .CustNationalityCode & ","
                M_Sql = M_Sql & .CustMaritalStatusCode & ","
                M_Sql = M_Sql & .CustNoOfDependents & ","
                M_Sql = M_Sql & .CustDefAddTypeCode & ","
                M_Sql = M_Sql & .CustIncomeRangeCode & ","
                M_Sql = M_Sql & "'" & .CustIntroCustId & "',"
                M_Sql = M_Sql & .CustItDocumentCode & ","
                M_Sql = M_Sql & "'" & .CustRemarks & "',"
                M_Sql = M_Sql & .CustLocationCode & ","
                M_Sql = M_Sql & "'" & .CustIslanderCardNo & "',"
                M_Sql = M_Sql & "'" & .CustLocalCertNo & "',"
                M_Sql = M_Sql & "'" & .CustIdentificationMark & "',"
                M_Sql = M_Sql & .CustHouseTypeCode & ","
                M_Sql = M_Sql & .CustResidentStatusCode & ","
                M_Sql = M_Sql & .CustInsuranceCode & ","
                M_Sql = M_Sql & .CustVehicleOwnedCode & ","
                M_Sql = M_Sql & .CustInvestmentCode & ","
                M_Sql = M_Sql & "'" & .CustBloodGroup & "',"
                M_Sql = M_Sql & "'" & .CustWebsite & "',"
                M_Sql = M_Sql & "'" & .CustFax & "',"
                M_Sql = M_Sql & "'" & .CustEmail & "',"
                M_Sql = M_Sql & "'" & Format(.CustRegDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & .CustPhone1 & "',"
                M_Sql = M_Sql & .CustPhone1TypeCode & ","
                M_Sql = M_Sql & "'" & .CustPhone2 & "',"
                M_Sql = M_Sql & .CustPhone2TypeCode & ","
                M_Sql = M_Sql & "'" & .CustMobile & "',"
                M_Sql = M_Sql & .CustGradeCode & ","
                M_Sql = M_Sql & "'" & Format(.CustRatingDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & .CustDisabilityCode & ","
                M_Sql = M_Sql & .CustCurrencyCode & ","
                M_Sql = M_Sql & "'" & .CustITDocRemarks & "',"
                M_Sql = M_Sql & "'" & .CustStatus & "',"
                M_Sql = M_Sql & .CustAuthorised & ","
                M_Sql = M_Sql & .LogID & ","
                M_Sql = M_Sql & "'" & .MakerID & "',"
                M_Sql = M_Sql & "'" & .CheckerID & "',"
                M_Sql = M_Sql & "'" & Format(.MakingTime, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & Format(.CheckingTime, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & .CustAdhaarNo & "',"
                M_Sql = M_Sql & .InstitutionCode & ","
                M_Sql = M_Sql & "'" & .IsCompleted & "',"
                M_Sql = M_Sql & .CustDesignationCode & ","
                M_Sql = M_Sql & .CusGrpID & ","
            End With
            With M_PresAddress
                M_Sql = M_Sql & .AddTypeId & ","
                M_Sql = M_Sql & "'" & .AddAddress1 & "',"
                M_Sql = M_Sql & "'" & .AddAddress2 & "',"
                M_Sql = M_Sql & "'" & .AddAddress3 & "',"
                M_Sql = M_Sql & "'" & .AddPostoffice & "',"
                M_Sql = M_Sql & "'" & .AddPinCode & "',"
                M_Sql = M_Sql & "'" & .AddDist & "',"
                M_Sql = M_Sql & "'" & .AddHouseNo & "',"
                M_Sql = M_Sql & "'" & .AddCountry & "',"
            End With
            With M_PermAddress
                M_Sql = M_Sql & .AddTypeId & ","
                M_Sql = M_Sql & "'" & .AddAddress1 & "',"
                M_Sql = M_Sql & "'" & .AddAddress2 & "',"
                M_Sql = M_Sql & "'" & .AddAddress3 & "',"
                M_Sql = M_Sql & "'" & .AddPostoffice & "',"
                M_Sql = M_Sql & "'" & .AddPinCode & "',"
                M_Sql = M_Sql & "'" & .AddDist & "',"
                M_Sql = M_Sql & "'" & .AddHouseNo & "',"
                M_Sql = M_Sql & "'" & .AddCountry & "',"
            End With

            With M_PhotoIdentity
                M_Sql = M_Sql & .CidntCode & ","
                M_Sql = M_Sql & "'" & .CidntNo & "',"
                M_Sql = M_Sql & "'" & .Cidntdetails & "',"
                M_Sql = M_Sql & "'" & .CidntIssueAuthority & "',"
                M_Sql = M_Sql & "'" & Format(.CidntIssueDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & Format(.CidntExpiryDate, "MM/dd/yyyy") & "',"
            End With

            With M_AddIdentity
                M_Sql = M_Sql & .CidntCode & ","
                M_Sql = M_Sql & "'" & .CidntNo & "',"
                M_Sql = M_Sql & "'" & .Cidntdetails & "',"
                M_Sql = M_Sql & "'" & .CidntIssueAuthority & "',"
                M_Sql = M_Sql & "'" & Format(.CidntIssueDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & Format(.CidntExpiryDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'Y',"
                M_Sql = M_Sql & "0,"
            End With

            With M_CustMaster
                M_Sql = M_Sql & "'" & .LetterNo & "',"
                M_Sql = M_Sql & "'" & Format(.LetterDate, "MM/dd/yyyy") & "',"
                M_Sql = M_Sql & "'" & .ModifyReason & "'"
            End With

            M_CustId = M_DBConn.ExecuteScalar(M_Sql, 0, False)
            Return M_CustId
        Catch ex As Exception
            '  MsgBox(ex.Message)
            Return False
        End Try

    End Function

    Public Function Get_CustomerName(ByVal M_CustId As String) As String
        Try
            Dim M_Dt As New DataTable
            Dim M_CustMaster As New CustMasterBO
            Dim M_CustName As String = ""
            Dim Qry As String
            Qry = "Select * from Cust_Master_M_Tbl where Cust_Id = " & M_CustId
            M_DBConn.ExecuteDataTable(M_Dt, Qry, False)
            If M_Dt.Rows.Count > 0 Then
                M_CustName = M_Dt.Rows(0).Item("Cust_Name")
            End If
            Return M_CustName
        Catch ex As Exception
            Return ""
        End Try

    End Function



    Public Function Locate_Data(ByVal M_CustId As String, ByRef M_PresAdd As CustAddressBO, ByRef M_PermAdd As CustAddressBO) As CustMasterBO
        Try
            Dim M_Dt As New DataTable
            Dim M_CustMaster As New CustMasterBO
            Dim Qry As String
            Qry = "Select * from Cust_Master_M_Tbl where Cust_Id = " & M_CustId
            M_DBConn.ExecuteDataTable(M_Dt, Qry, False)
            If M_Dt.Rows.Count > 0 Then
                With M_CustMaster
                    .CustId = M_Dt.Rows(0).Item("Cust_Id")
                    .CustAge = M_Dt.Rows(0).Item("Cust_Age")
                    .CustNoOfDependents = M_Dt.Rows(0).Item("Cust_NoOfDependents")
                    .CustId = M_Dt.Rows(0).Item("Cust_Id")
                    .CustTypeCode = M_Dt.Rows(0).Item("Cust_Type_Code")
                    .CustCatgCode = M_Dt.Rows(0).Item("Cust_Catg_Code")
                    .CustTitleCode = M_Dt.Rows(0).Item("Cust_Title_Code")
                    .CustGender = M_Dt.Rows(0).Item("Cust_Gender")
                    .CustRelTypCode = M_Dt.Rows(0).Item("Cust_RelTyp_Code")
                    .CustGradeCode = M_Dt.Rows(0).Item("Cust_Grade_Code")
                    .CustDisabilityCode = M_Dt.Rows(0).Item("Cust_Disability_Code")
                    .CustCurrencyCode = M_Dt.Rows(0).Item("Cust_Currency_Code")
                    .LogID = M_Dt.Rows(0).Item("Log_ID")
                    .CustHouseTypeCode = M_Dt.Rows(0).Item("Cust_HouseType_Code")
                    .CustResidentStatusCode = M_Dt.Rows(0).Item("Cust_ResidentStatus_Code")
                    .CustInsuranceCode = M_Dt.Rows(0).Item("Cust_Insurance_Code")
                    .CustVehicleOwnedCode = M_Dt.Rows(0).Item("Cust_VehicleOwned_Code")
                    .CustInvestmentCode = M_Dt.Rows(0).Item("Cust_Investment_Code")
                    .CustPhone1TypeCode = M_Dt.Rows(0).Item("Cust_Phone1Type_Code")
                    .CustNationalityCode = M_Dt.Rows(0).Item("Cust_Nationality_Code")
                    .CustMaritalStatusCode = M_Dt.Rows(0).Item("Cust_MaritalStatus_Code")
                    .CustDefAddTypeCode = M_Dt.Rows(0).Item("Cust_DefAddType_Code")
                    .CustIncomeRangeCode = M_Dt.Rows(0).Item("Cust_IncomeRange_Code")
                    .CustItDocumentCode = M_Dt.Rows(0).Item("Cust_ItDocument_Code")
                    .CustLocationCode = M_Dt.Rows(0).Item("Cust_Location_Code")
                    .CustGuardianRelationCode = M_Dt.Rows(0).Item("Cust_GuardianRelation_Code")
                    .CustReligionCode = M_Dt.Rows(0).Item("Cust_Religion_Code")
                    .CustCasteCode = M_Dt.Rows(0).Item("Cust_Caste_Code")
                    .CustOccupationCode = M_Dt.Rows(0).Item("Cust_Occupation_Code")
                    .CustEducationCode = M_Dt.Rows(0).Item("Cust_Education_Code")
                    .CustStaffId = M_Dt.Rows(0).Item("Cust_StaffId")
                    .CustDobInc = M_Dt.Rows(0).Item("Cust_Dob_Inc")
                    .CustRegDate = M_Dt.Rows(0).Item("Cust_RegDate")
                    .CustRatingDate = M_Dt.Rows(0).Item("Cust_RatingDate")
                    .MakingTime = M_Dt.Rows(0).Item("Making_Time")
                    .CheckingTime = M_Dt.Rows(0).Item("Checking_Time")
                    .CustMinor = M_Dt.Rows(0).Item("Cust_Minor")
                    .CustAuthorised = M_Dt.Rows(0).Item("Cust_Authorised")
                    .BrCode = M_Dt.Rows(0).Item("Br_Code")
                    .CustName = M_Dt.Rows(0).Item("Cust_Name")
                    .CustFirstName = M_Dt.Rows(0).Item("Cust_FirstName")
                    .CustMiddleName = M_Dt.Rows(0).Item("Cust_MiddleName")
                    .CustLastName = M_Dt.Rows(0).Item("Cust_LastName")
                    .CustShortName = M_Dt.Rows(0).Item("Cust_ShortName")
                    .CustPhone2 = M_Dt.Rows(0).Item("Cust_Phone2")
                    .CustMobile = M_Dt.Rows(0).Item("Cust_Mobile")
                    .CustITDocRemarks = M_Dt.Rows(0).Item("Cust_ITDoc_Remarks")
                    .MakerID = M_Dt.Rows(0).Item("MakerID")
                    .CheckerID = M_Dt.Rows(0).Item("CheckerID")
                    .CustIdentificationMark = M_Dt.Rows(0).Item("Cust_IdentificationMark")
                    .CustBloodGroup = M_Dt.Rows(0).Item("Cust_BloodGroup")
                    .CustWebsite = M_Dt.Rows(0).Item("Cust_Website")
                    .CustFax = M_Dt.Rows(0).Item("Cust_Fax")
                    .CustEmail = M_Dt.Rows(0).Item("Cust_Email")
                    .CustPhone1 = M_Dt.Rows(0).Item("Cust_Phone1")
                    .CustRelNm = M_Dt.Rows(0).Item("Cust_RelNm")
                    .CustGuardianId = M_Dt.Rows(0).Item("Cust_GuardianId")
                    .CustIntroCustId = M_Dt.Rows(0).Item("Cust_IntroCustId")
                    .CustRemarks = M_Dt.Rows(0).Item("Cust_Remarks")
                    .CustIslanderCardNo = M_Dt.Rows(0).Item("Cust_IslanderCardNo")
                    .CustLocalCertNo = M_Dt.Rows(0).Item("Cust_LocalCertNo")
                    .CustStaff = M_Dt.Rows(0).Item("Cust_Staff")
                    .CustStatus = M_Dt.Rows(0).Item("Cust_Status")

                    .IsCompleted = M_Dt.Rows(0).Item("Is_Completed")
                    '.CustDesignationCode = M_Dt.Rows(0).Item("Cust_Designation_Code")
                    .CustAdhaarNo = M_Dt.Rows(0).Item("Cust_AdhaarNo")
                    '.InstitutionCode = M_Dt.Rows(0).Item("Institution_Code")

                    .CustPhone2TypeCode = M_Dt.Rows(0).Item("Cust_Phone2Type_Code")
                    .CusGrpID = M_Dt.Rows(0).Item("Cust_Grp_Code")


                End With
            End If
            Dim M_PresDt As DataTable
            Qry = "Select * from Cust_Address_M_Tbl where Cust_Id = " & M_CustId & " and Add_TypeID = 96"
            M_DBConn.ExecuteDataTable(M_PresDt, Qry, False)
            If M_PresDt.Rows.Count > 0 Then
                M_PresAdd.AddAddress1 = M_PresDt.Rows(0).Item("Add_Address1")
                M_PresAdd.AddAddress2 = M_PresDt.Rows(0).Item("Add_Address2")
                M_PresAdd.AddAddress3 = M_PresDt.Rows(0).Item("Add_Address3")
                M_PresAdd.AddPinCode = M_PresDt.Rows(0).Item("Add_PinCode")
                M_PresAdd.AddPostoffice = M_PresDt.Rows(0).Item("Add_Postoffice")
                M_PresAdd.AddDist = M_PresDt.Rows(0).Item("Add_Dist")
                M_PresAdd.AddHouseNo = M_PresDt.Rows(0).Item("Add_HouseNo")
                M_PresAdd.AddCountry = M_PresDt.Rows(0).Item("Add_Country")
            End If

            Dim M_PermDt As DataTable
            Qry = "Select * from Cust_Address_M_Tbl where Cust_Id = " & M_CustId & " and Add_TypeID = 95"
            M_DBConn.ExecuteDataTable(M_PermDt, Qry, False)
            If M_PermDt.Rows.Count > 0 Then
                M_PermAdd.AddAddress1 = M_PermDt.Rows(0).Item("Add_Address1")
                M_PermAdd.AddAddress2 = M_PermDt.Rows(0).Item("Add_Address2")
                M_PermAdd.AddAddress3 = M_PermDt.Rows(0).Item("Add_Address3")
                M_PermAdd.AddPinCode = M_PermDt.Rows(0).Item("Add_PinCode")
                M_PermAdd.AddPostoffice = M_PermDt.Rows(0).Item("Add_Postoffice")
                M_PermAdd.AddDist = M_PermDt.Rows(0).Item("Add_Dist")
                M_PermAdd.AddHouseNo = M_PermDt.Rows(0).Item("Add_HouseNo")
                M_PermAdd.AddCountry = M_PermDt.Rows(0).Item("Add_Country")
            End If

            Return M_CustMaster
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function Cust_Updatedata(ByVal M_CustMaster As CustMasterBO, ByVal M_PresAddress As CustAddressBO) As Integer
        Try
            Dim StrQry As String = ""
            StrQry = "exec UpdateCust_Master_M_Tbl "
            StrQry = StrQry + "'" + M_CustMaster.CustId.ToString() + "','" + M_CustMaster.CustName + "','" + M_CustMaster.CustRelNm + "',"
            StrQry = StrQry + "'" + M_CustMaster.CustDobInc.ToString() + "','" + M_CustMaster.CustAge.ToString() + "','" + M_CustMaster.CustMobile.ToString() + "','" + M_CustMaster.CustPhone1.ToString() + "',"
            StrQry = StrQry + "'" + M_PresAddress.AddAddress1 + "','" + M_PresAddress.AddAddress2 + "','" + M_PresAddress.AddPinCode + "',"
            StrQry = StrQry + "'" + M_PresAddress.AddDist + "','" + M_PresAddress.AddCountry.ToString() + "','" + M_PresAddress.AddAddress3 + "'"
            M_DBConn.ExecuteScalar(StrQry, Connection.SQLSource.STOREDPROCEDURE, False)
            Return 1
        Catch ex As Exception

        End Try
    End Function

    Public Sub New()
        M_DBConn = New Connection
    End Sub


    Protected Overrides Sub Finalize()
        M_DBConn = Nothing
        MyBase.Finalize()
    End Sub
End Class

