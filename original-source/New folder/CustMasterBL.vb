Imports SMARTCORE_DA
Imports SMARTCORE_BO
Imports SMARTCORE_Common.CommonClass
Public Class CustMasterBL
    Private M_CustId As Integer
    Public Function Save_Data(ByVal M_CustMaster As CustMasterBO, ByVal M_PresAddress As CustAddressBO, ByVal M_PermAddress As CustAddressBO, ByVal M_PhotoIdentity As CustIdentityBO, ByVal M_AddIdentity As CustIdentityBO, ByVal M_EntryMode As String) As Integer
        Try
            Dim M_CustMasterDA As New CustMasterDA
            M_CustId = M_CustMasterDA.Save_Data(M_CustMaster, M_PresAddress, M_PermAddress, M_PhotoIdentity, M_AddIdentity, M_EntryMode)
            M_CustMasterDA = Nothing
            Return M_CustId
        Catch ex As Exception

        End Try
    End Function

    Public Function Locate_Data(ByVal M_CustId As Integer, ByRef M_PresAdd As CustAddressBO, ByRef M_PermAdd As CustAddressBO) As CustMasterBO
        Try
            Dim M_CustMasterDA As New CustMasterDA
            Dim M_CustMasterBO As CustMasterBO
            M_CustMasterBO = M_CustMasterDA.Locate_Data(M_CustId, M_PresAdd, M_PermAdd)
            M_CustMasterDA = Nothing
            Return M_CustMasterBO
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Get_CustomerName(ByVal M_CustId As Integer) As String
        Try
            Dim M_CustMasterDA As New CustMasterDA
            Dim M_Str As String
            M_Str = M_CustMasterDA.Get_CustomerName(M_CustId)
            M_CustMasterDA = Nothing
            Return M_Str
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Cust_Updatedata(ByVal M_CustMaster As CustMasterBO, ByVal M_PresAddress As CustAddressBO) As Integer
        Try
            Dim M_CustMasterDA As New CustMasterDA
            Return M_CustMasterDA.Cust_Updatedata(M_CustMaster, M_PresAddress)
        Catch ex As Exception

        End Try
    End Function
End Class

