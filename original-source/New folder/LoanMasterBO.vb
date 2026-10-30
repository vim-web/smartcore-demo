
Public Class LoanMasterBO



    Private M_AccID As Integer
    Private M_CustId As Integer
    Private M_LmHolidayPeriod As Integer
    Private M_LmInstallmentNo As Integer
    Private M_LmApprID As Integer
    Private M_LmMainpurpose As Integer
    Private M_LmAgentCode As Integer
    Private M_LmJewName As Integer
    Private M_LmSubpurpose As Integer
    Private M_LmPriority As Integer
    Private M_LmSanctionBy As Integer
    Private M_LmNomNos As Integer
    Private M_LmCumType As Integer
    Private M_LogID As Integer
    Private M_LmLoanDate As Date
    Private M_LmDueDate As Date
    Private M_LmLastIntDate As Date
    Private M_LmResDate As Date
    Private M_LmLastUpdate As Date
    Private M_LmOpIntdate As Date
    Private M_LmOrnamentsReleasedDate As Date
    Private M_Cashiertime As Date
    Private M_LmjewBillDate As Date
    Private M_LmLegalDate As Date
    Private M_LmNpaDate As Date
    Private M_LmFirstNpaDate As Date
    Private M_LmClosedDate As Date
    Private M_MakingTime As Date
    Private M_CheckingTime As Date
    Private M_LmLoanAmount As Double
    Private M_LmBalance As Double
    Private M_LmPeriod As Double
    Private M_LmMarketRate As Double
    Private M_LmBankRate As Double
    Private M_LmIntRate As Double
    Private M_LmBillAmt As Double
    Private M_LmPenalRate As Double
    Private M_LmInstallmentAmount As Double
    Private M_LmApprValue As Double
    Private M_LmOpBal As Double
    Private M_LmOpPayAmt As Double
    Private M_LmNetWt As Double
    Private M_LmGrossWt As Double
    Private M_LmOpIntrate As Double
    Private M_AccNo As String
    Private M_SchCode As String
    Private M_BrCode As String
    Private M_LmApplSlno As String
    Private M_AccCode As String
    Private M_LmShrBrCode As String
    Private M_CheckerID As String
    Private M_CashierID As String
    Private M_BankAccNo As String
    Private M_LmJewBillno As String
    Private M_LmRegNo As String
    Private M_LmLbrCode As String
    Private M_LmLbrActCode As String
    Private M_LmIntroducedby As String
    Private M_TranNo As String
    Private M_MakerID As String
    Private M_LmShrSchCode As String
    Private M_LmMemNo As String
    Private M_LmResNo As String
    Private M_LmSanctionNo As String
    Private M_LmSecurityDetails As String
    Private M_LmRemarks As String
    Private M_LmBalType As Char
    Private M_LmPeriodType As Char
    Private M_LmHolidayPeriodType As Char
    Private M_LmOperationMode As String
    Private M_LmInsurance As Char
    Private M_LmRegType As Char
    Private M_Authorised As Char
    Private M_LmOrnamentsReleased As Char
    Private M_LmStandingForSecDeposit As Char
    Private M_LmOpBalType As Char
    Private M_LmStatus As Char
    Private M_LmEnhancement As Char
    Private M_LmLegalStatus As Char
    Private M_LmNpaStatus As Char
    Private M_LmFirstNpaStatus As Char
    Private M_LMDirector As String
    Private M_LMWardCode As String
    Private M_LMReligion As Integer
    Private M_LMCaste As Integer
    Private M_LMGender As Integer
    Private M_LMRegOffice As String
    Private M_LMExtend As String
    Private M_LMSurveyNo As String
    Private M_LMDocumentNo As String
    Private M_LMResurveyNo As String
    Private M_LMCent As Integer
    Private M_LMSroNo As String
    Public Property AccID() As Integer
        Get
            Return M_AccID
        End Get
        Set(ByVal value As Integer)
            M_AccID = value
        End Set
    End Property


    Public Property CustId() As Integer
        Get
            Return M_CustId
        End Get
        Set(ByVal value As Integer)
            M_CustId = value
        End Set
    End Property
    Public Property LmHolidayPeriod() As Integer
        Get
            Return M_LmHolidayPeriod
        End Get
        Set(ByVal value As Integer)
            M_LmHolidayPeriod = value
        End Set
    End Property
    Public Property LmInstallmentNo() As Integer
        Get
            Return M_LmInstallmentNo
        End Get
        Set(ByVal value As Integer)
            M_LmInstallmentNo = value
        End Set
    End Property
    Public Property LmApprID() As Integer
        Get
            Return M_LmApprID
        End Get
        Set(ByVal value As Integer)
            M_LmApprID = value
        End Set
    End Property
    Public Property LmMainpurpose() As Integer
        Get
            Return M_LmMainpurpose
        End Get
        Set(ByVal value As Integer)
            M_LmMainpurpose = value
        End Set
    End Property
    Public Property LmAgentCode() As Integer
        Get
            Return M_LmAgentCode
        End Get
        Set(ByVal value As Integer)
            M_LmAgentCode = value
        End Set
    End Property
    Public Property LmJewName() As Integer
        Get
            Return M_LmJewName
        End Get
        Set(ByVal value As Integer)
            M_LmJewName = value
        End Set
    End Property
    Public Property LmSubpurpose() As Integer
        Get
            Return M_LmSubpurpose
        End Get
        Set(ByVal value As Integer)
            M_LmSubpurpose = value
        End Set
    End Property
    Public Property LmPriority() As Integer
        Get
            Return M_LmPriority
        End Get
        Set(ByVal value As Integer)
            M_LmPriority = value
        End Set
    End Property
    Public Property LmSanctionBy() As Integer
        Get
            Return M_LmSanctionBy
        End Get
        Set(ByVal value As Integer)
            M_LmSanctionBy = value
        End Set
    End Property
    Public Property LmNomNos() As Integer
        Get
            Return M_LmNomNos
        End Get
        Set(ByVal value As Integer)
            M_LmNomNos = value
        End Set
    End Property
    Public Property LmCumType() As Integer
        Get
            Return M_LmCumType
        End Get
        Set(ByVal value As Integer)
            M_LmCumType = value
        End Set
    End Property
    Public Property LogID() As Integer
        Get
            Return M_LogID
        End Get
        Set(ByVal value As Integer)
            M_LogID = value
        End Set
    End Property
    Public Property LmLoanDate() As Date
        Get
            Return M_LmLoanDate
        End Get
        Set(ByVal value As Date)
            M_LmLoanDate = value
        End Set
    End Property
    Public Property LmDueDate() As Date
        Get
            Return M_LmDueDate
        End Get
        Set(ByVal value As Date)
            M_LmDueDate = value
        End Set
    End Property
    Public Property LmLastIntDate() As Date
        Get
            Return M_LmLastIntDate
        End Get
        Set(ByVal value As Date)
            M_LmLastIntDate = value
        End Set
    End Property
    Public Property LmResDate() As Date
        Get
            Return M_LmResDate
        End Get
        Set(ByVal value As Date)
            M_LmResDate = value
        End Set
    End Property
    Public Property LmLastUpdate() As Date
        Get
            Return M_LmLastUpdate
        End Get
        Set(ByVal value As Date)
            M_LmLastUpdate = value
        End Set
    End Property
    Public Property LmOpIntdate() As Date
        Get
            Return M_LmOpIntdate
        End Get
        Set(ByVal value As Date)
            M_LmOpIntdate = value
        End Set
    End Property
    Public Property LmOrnamentsReleasedDate() As Date
        Get
            Return M_LmOrnamentsReleasedDate
        End Get
        Set(ByVal value As Date)
            M_LmOrnamentsReleasedDate = value
        End Set
    End Property
    Public Property Cashiertime() As Date
        Get
            Return M_Cashiertime
        End Get
        Set(ByVal value As Date)
            M_Cashiertime = value
        End Set
    End Property
    Public Property LmjewBillDate() As Date
        Get
            Return M_LmjewBillDate
        End Get
        Set(ByVal value As Date)
            M_LmjewBillDate = value
        End Set
    End Property
    Public Property LmLegalDate() As Date
        Get
            Return M_LmLegalDate
        End Get
        Set(ByVal value As Date)
            M_LmLegalDate = value
        End Set
    End Property
    Public Property LmNpaDate() As Date
        Get
            Return M_LmNpaDate
        End Get
        Set(ByVal value As Date)
            M_LmNpaDate = value
        End Set
    End Property
    Public Property LmFirstNpaDate() As Date
        Get
            Return M_LmFirstNpaDate
        End Get
        Set(ByVal value As Date)
            M_LmFirstNpaDate = value
        End Set
    End Property
    Public Property LmClosedDate() As Date
        Get
            Return M_LmClosedDate
        End Get
        Set(ByVal value As Date)
            M_LmClosedDate = value
        End Set
    End Property
    Public Property MakingTime() As Date
        Get
            Return M_MakingTime
        End Get
        Set(ByVal value As Date)
            M_MakingTime = value
        End Set
    End Property
    Public Property CheckingTime() As Date
        Get
            Return M_CheckingTime
        End Get
        Set(ByVal value As Date)
            M_CheckingTime = value
        End Set
    End Property
    Public Property LmLoanAmount() As Double
        Get
            Return M_LmLoanAmount
        End Get
        Set(ByVal value As Double)
            M_LmLoanAmount = value
        End Set
    End Property
    Public Property LmBalance() As Double
        Get
            Return M_LmBalance
        End Get
        Set(ByVal value As Double)
            M_LmBalance = value
        End Set
    End Property
    Public Property LmPeriod() As Double
        Get
            Return M_LmPeriod
        End Get
        Set(ByVal value As Double)
            M_LmPeriod = value
        End Set
    End Property
    Public Property LmMarketRate() As Double
        Get
            Return M_LmMarketRate
        End Get
        Set(ByVal value As Double)
            M_LmMarketRate = value
        End Set
    End Property
    Public Property LmBankRate() As Double
        Get
            Return M_LmBankRate
        End Get
        Set(ByVal value As Double)
            M_LmBankRate = value
        End Set
    End Property
    Public Property LmIntRate() As Double
        Get
            Return M_LmIntRate
        End Get
        Set(ByVal value As Double)
            M_LmIntRate = value
        End Set
    End Property
    Public Property LmBillAmt() As Double
        Get
            Return M_LmBillAmt
        End Get
        Set(ByVal value As Double)
            M_LmBillAmt = value
        End Set
    End Property
    Public Property LmPenalRate() As Double
        Get
            Return M_LmPenalRate
        End Get
        Set(ByVal value As Double)
            M_LmPenalRate = value
        End Set
    End Property
    Public Property LmInstallmentAmount() As Double
        Get
            Return M_LmInstallmentAmount
        End Get
        Set(ByVal value As Double)
            M_LmInstallmentAmount = value
        End Set
    End Property
    Public Property LmApprValue() As Double
        Get
            Return M_LmApprValue
        End Get
        Set(ByVal value As Double)
            M_LmApprValue = value
        End Set
    End Property
    Public Property LmOpBal() As Double
        Get
            Return M_LmOpBal
        End Get
        Set(ByVal value As Double)
            M_LmOpBal = value
        End Set
    End Property

    Public Property LmNetWt() As Double
        Get
            Return M_LmNetWt
        End Get
        Set(ByVal value As Double)
            M_LmNetWt = value
        End Set
    End Property

    Public Property LmGrossWt() As Double
        Get
            Return M_LmGrossWt
        End Get
        Set(ByVal value As Double)
            M_LmGrossWt = value
        End Set
    End Property

    Public Property LmOpPayAmt() As Double
        Get
            Return M_LmOpPayAmt
        End Get
        Set(ByVal value As Double)
            M_LmOpPayAmt = value
        End Set
    End Property
    Public Property LmOpIntrate() As Double
        Get
            Return M_LmOpIntrate
        End Get
        Set(ByVal value As Double)
            M_LmOpIntrate = value
        End Set
    End Property
    Public Property AccNo() As String
        Get
            Return M_AccNo
        End Get
        Set(ByVal value As String)
            M_AccNo = value
        End Set
    End Property
    Public Property SchCode() As String
        Get
            Return M_SchCode
        End Get
        Set(ByVal value As String)
            M_SchCode = value
        End Set
    End Property
    Public Property BrCode() As String
        Get
            Return M_BrCode
        End Get
        Set(ByVal value As String)
            M_BrCode = value
        End Set
    End Property
    Public Property LmApplSlno() As String
        Get
            Return M_LmApplSlno
        End Get
        Set(ByVal value As String)
            M_LmApplSlno = value
        End Set
    End Property
    Public Property AccCode() As String
        Get
            Return M_AccCode
        End Get
        Set(ByVal value As String)
            M_AccCode = value
        End Set
    End Property
    Public Property LmShrBrCode() As String
        Get
            Return M_LmShrBrCode
        End Get
        Set(ByVal value As String)
            M_LmShrBrCode = value
        End Set
    End Property
    Public Property CheckerID() As String
        Get
            Return M_CheckerID
        End Get
        Set(ByVal value As String)
            M_CheckerID = value
        End Set
    End Property
    Public Property CashierID() As String
        Get
            Return M_CashierID
        End Get
        Set(ByVal value As String)
            M_CashierID = value
        End Set
    End Property
    Public Property BankAccNo() As String
        Get
            Return M_BankAccNo
        End Get
        Set(ByVal value As String)
            M_BankAccNo = value
        End Set
    End Property
    Public Property LmJewBillno() As String
        Get
            Return M_LmJewBillno
        End Get
        Set(ByVal value As String)
            M_LmJewBillno = value
        End Set
    End Property
    Public Property LmRegNo() As String
        Get
            Return M_LmRegNo
        End Get
        Set(ByVal value As String)
            M_LmRegNo = value
        End Set
    End Property
    Public Property LmLbrCode() As String
        Get
            Return M_LmLbrCode
        End Get
        Set(ByVal value As String)
            M_LmLbrCode = value
        End Set
    End Property
    Public Property LmLbrActCode() As String
        Get
            Return M_LmLbrActCode
        End Get
        Set(ByVal value As String)
            M_LmLbrActCode = value
        End Set
    End Property
    Public Property LmIntroducedby() As String
        Get
            Return M_LmIntroducedby
        End Get
        Set(ByVal value As String)
            M_LmIntroducedby = value
        End Set
    End Property
    Public Property TranNo() As String
        Get
            Return M_TranNo
        End Get
        Set(ByVal value As String)
            M_TranNo = value
        End Set
    End Property
    Public Property MakerID() As String
        Get
            Return M_MakerID
        End Get
        Set(ByVal value As String)
            M_MakerID = value
        End Set
    End Property
    Public Property LmShrSchCode() As String
        Get
            Return M_LmShrSchCode
        End Get
        Set(ByVal value As String)
            M_LmShrSchCode = value
        End Set
    End Property
    Public Property LmMemNo() As String
        Get
            Return M_LmMemNo
        End Get
        Set(ByVal value As String)
            M_LmMemNo = value
        End Set
    End Property
    Public Property LmResNo() As String
        Get
            Return M_LmResNo
        End Get
        Set(ByVal value As String)
            M_LmResNo = value
        End Set
    End Property
    Public Property LmSanctionNo() As String
        Get
            Return M_LmSanctionNo
        End Get
        Set(ByVal value As String)
            M_LmSanctionNo = value
        End Set
    End Property
    Public Property LmSecurityDetails() As String
        Get
            Return M_LmSecurityDetails
        End Get
        Set(ByVal value As String)
            M_LmSecurityDetails = value
        End Set
    End Property
    Public Property LmRemarks() As String
        Get
            Return M_LmRemarks
        End Get
        Set(ByVal value As String)
            M_LmRemarks = value
        End Set
    End Property

    Public Property LmBalType() As Char
        Get
            Return M_LmBalType
        End Get
        Set(ByVal value As Char)
            M_LmBalType = value
        End Set
    End Property
    Public Property LmPeriodType() As Char
        Get
            Return M_LmPeriodType
        End Get
        Set(ByVal value As Char)
            M_LmPeriodType = value
        End Set
    End Property
    Public Property LmHolidayPeriodType() As Char
        Get
            Return M_LmHolidayPeriodType
        End Get
        Set(ByVal value As Char)
            M_LmHolidayPeriodType = value
        End Set
    End Property
    Public Property LmOperationMode() As String
        Get
            Return M_LmOperationMode
        End Get
        Set(ByVal value As String)
            M_LmOperationMode = value
        End Set
    End Property
    Public Property LmInsurance() As Char
        Get
            Return M_LmInsurance
        End Get
        Set(ByVal value As Char)
            M_LmInsurance = value
        End Set
    End Property
    Public Property LmRegType() As Char
        Get
            Return M_LmRegType
        End Get
        Set(ByVal value As Char)
            M_LmRegType = value
        End Set
    End Property
    Public Property Authorised() As Char
        Get
            Return M_Authorised
        End Get
        Set(ByVal value As Char)
            M_Authorised = value
        End Set
    End Property
    Public Property LmOrnamentsReleased() As Char
        Get
            Return M_LmOrnamentsReleased
        End Get
        Set(ByVal value As Char)
            M_LmOrnamentsReleased = value
        End Set
    End Property
    Public Property LmStandingForSecDeposit() As Char
        Get
            Return M_LmStandingForSecDeposit
        End Get
        Set(ByVal value As Char)
            M_LmStandingForSecDeposit = value
        End Set
    End Property
    Public Property LmOpBalType() As Char
        Get
            Return M_LmOpBalType
        End Get
        Set(ByVal value As Char)
            M_LmOpBalType = value
        End Set
    End Property
    Public Property LmStatus() As Char
        Get
            Return M_LmStatus
        End Get
        Set(ByVal value As Char)
            M_LmStatus = value
        End Set
    End Property
    Public Property LmEnhancement() As Char
        Get
            Return M_LmEnhancement
        End Get
        Set(ByVal value As Char)
            M_LmEnhancement = value
        End Set
    End Property
    Public Property LmLegalStatus() As Char
        Get
            Return M_LmLegalStatus
        End Get
        Set(ByVal value As Char)
            M_LmLegalStatus = value
        End Set
    End Property
    Public Property LmNpaStatus() As Char
        Get
            Return M_LmNpaStatus
        End Get
        Set(ByVal value As Char)
            M_LmNpaStatus = value
        End Set
    End Property
    Public Property LmFirstNpaStatus() As Char
        Get
            Return M_LmFirstNpaStatus
        End Get
        Set(ByVal value As Char)
            M_LmFirstNpaStatus = value
        End Set
    End Property


    Public Property LMDirector() As Integer
        Get
            Return M_LMDirector
        End Get
        Set(ByVal value As Integer)
            M_LMDirector = value
        End Set
    End Property
    Public Property LMWardCode() As String
        Get
            Return M_LMWardCode
        End Get
        Set(ByVal value As String)
            M_LMWardCode = Replace(Replace(value, "'", "`"), ";", ",")
        End Set
    End Property

    Public Property LMReligion() As Integer
        Get
            Return M_LMReligion
        End Get
        Set(ByVal value As Integer)
            M_LMReligion = value
        End Set
    End Property
    Public Property LMCaste() As Integer
        Get
            Return M_LMCaste
        End Get
        Set(ByVal value As Integer)
            M_LMCaste = value
        End Set
    End Property
    Public Property LMGender() As Integer
        Get
            Return M_LMGender
        End Get
        Set(ByVal value As Integer)
            M_LMGender = value
        End Set
    End Property
    Public Property LMRegOffice() As String
        Get
            Return M_LMRegOffice
        End Get
        Set(ByVal value As String)
            M_LMRegOffice = Replace(Replace(value, "'", "`"), ";", ",")
        End Set
    End Property
    Public Property LMExtend() As String
        Get
            Return M_LMExtend
        End Get
        Set(ByVal value As String)
            M_LMExtend = Replace(Replace(value, "'", "`"), ";", ",")
        End Set
    End Property
    Public Property LMSurveyNo() As String
        Get
            Return M_LMSurveyNo
        End Get
        Set(ByVal value As String)
            M_LMSurveyNo = Replace(Replace(value, "'", "`"), ";", ",")
        End Set
    End Property
    Public Property LMDocumentNo() As String
        Get
            Return M_LMDocumentNo
        End Get
        Set(ByVal value As String)
            M_LMDocumentNo = Replace(Replace(value, "'", "`"), ";", ",")
        End Set
    End Property
    Public Property LMResurveyNo() As String
        Get
            Return M_LMResurveyNo
        End Get
        Set(ByVal value As String)
            M_LMResurveyNo = Replace(Replace(value, "'", "`"), ";", ",")
        End Set
    End Property
    Public Property LM_Cent() As Integer
        Get
            Return M_LMCent
        End Get
        Set(ByVal value As Integer)
            M_LMCent = value
        End Set
    End Property
    Public Property LM_SroNo As String
        Get
            Return M_LMSroNo
        End Get
        Set(value As String)
            M_LMSroNo = value
        End Set
    End Property
End Class

