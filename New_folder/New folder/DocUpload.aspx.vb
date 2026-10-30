Imports SMARTCORE_BL
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Partial Public Class DocUpload
    Inherits System.Web.UI.Page
    Dim M_DOCID As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TxtDate.Attributes.Add("readonly", "readonly")
        TxtDate.Text = Session("TransDate")

        If Not IsPostBack Then

            Session("M_EntryMode") = "NEW"
            Dim M_DocCategoryBL As New DocCategoryBL
            M_DocCategoryBL.Fill_DOCCATG(CmbCategory)
            M_DocCategoryBL = Nothing
            Get_MaxID()
            CmbAccess.Items.Add("Public")
            CmbAccess.Items.Add("Section")
            CmbAccess.SelectedIndex = 1
        End If

    End Sub

    Private Sub Get_MaxID()
        Dim M_DocumentMasterBL As New DocumentMasterBL
        TxtDocNo.Text = M_DocumentMasterBL.Get_MaxID()
        M_DocumentMasterBL = Nothing
    End Sub
    Private Sub Locate_Data(ByVal M_DocumentMaster As SMARTCORE_BO.DocumentMasterBO)
        If M_DocumentMaster.DOCID = 0 Then
            Session("M_EntryMode") = "NEW"
            Clear_Controls()
            Get_MaxID()
            Exit Sub
        End If
        With M_DocumentMaster
            TxtDate.Text = .DOCDATE
            TxtDocNo.Text = .DOCID
            CmbCategory.SelectedValue = .DOCCATG
            TxtTitle.Text = .DOCTITLE
            TxtDescription.Text = .DOCDESCRIPTION
            HdnFileNm.Value = .DOCFILENM
            If .DOCACCESS = "P" Then
                CmbAccess.SelectedIndex = 0
            Else
                CmbAccess.SelectedIndex = 1
            End If
        End With
        Session("M_EntryMode") = "VIEW"
    End Sub


    Protected Sub CmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdSave.Click
        Dim M_FileNm As String
        Dim M_DocumentMaster As New SMARTCORE_BO.DocumentMasterBO



        If Validate_Data() = False Then
            Exit Sub
        End If

        If fileUpEx.HasFile Then
            M_FileNm = fileUpEx.PostedFile.FileName
        End If
        With M_DocumentMaster
            .DOCDATE = TxtDate.Text
            .DOCID = TxtDocNo.Text
            .DOCCATG = CmbCategory.SelectedValue
            .DOCTITLE = TxtTitle.Text
            .DOCDESCRIPTION = TxtDescription.Text
            .DOCFILENM = M_FileNm
            .USER = Session("UserID")
            .DOCACCESS = CmbAccess.Text.Substring(0, 1)
        End With

        Try
            Dim M_DocumentMasterBL As New DocumentMasterBL
            M_DOCID = M_DocumentMasterBL.Save_Data(M_DocumentMaster, Session("M_EntryMode"))

            If fileUpEx.HasFile Then


                ''save the file to the server 
                'fileUpEx.PostedFile.SaveAs(Server.MapPath(".\EMPFILES\") & fileNm)

                Dim strImageName As String = M_DOCID.ToString()
                If fileUpEx.PostedFile IsNot Nothing AndAlso fileUpEx.PostedFile.FileName <> "" Then
                    Dim imageSize As Byte() = New Byte(fileUpEx.PostedFile.ContentLength - 1) {}
                    Dim uploadedImage__1 As HttpPostedFile = fileUpEx.PostedFile()
                    uploadedImage__1.InputStream.Read(imageSize, 0, CInt(fileUpEx.PostedFile.ContentLength))
                    ' Create SQL Connection 
                    Dim con As New SqlConnection()
                    con.ConnectionString = AppSettings("SQL_CONNSTR")
                    Dim cmd As New SqlCommand()
                    cmd.CommandText = "INSERT INTO DocUpload(DocID, FlNm,ContentType,BinaryData) VALUES(@DocID, @FlNm,@ContentType,@BinaryData)"
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = con

                    Dim ImageName As New SqlParameter("@DocID", SqlDbType.BigInt)
                    ImageName.Value = strImageName.ToString()
                    cmd.Parameters.Add(ImageName)

                    Dim UploadedImage__2 As New SqlParameter("@BinaryData", SqlDbType.VarBinary, imageSize.Length)
                    UploadedImage__2.Value = imageSize
                    cmd.Parameters.Add(UploadedImage__2)

                    Dim FlName As New SqlParameter("@FlNm", SqlDbType.VarChar, 50)
                    FlName.Value = M_FileNm
                    cmd.Parameters.Add(FlName)

                    Dim ContentTyp As New SqlParameter("@ContentType", SqlDbType.VarChar, 50)
                    ContentTyp.Value = fileUpEx.PostedFile.ContentType
                    cmd.Parameters.Add(ContentTyp)

                    con.Open()
                    Dim result As Integer = cmd.ExecuteNonQuery()
                    con.Close()
                    If result > 0 Then
                        lblStatus.Text = "File Uploaded"
                    End If

                End If

            End If

            TxtDocNo.Text = M_DOCID.ToString
            Clear_Controls()
            Get_MaxID()
            M_DocumentMasterBL = Nothing
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Clear_Controls()
        TxtTitle.Text = ""
        TxtDescription.Text = ""
        HdnFileNm.Value = ""
    End Sub
    Private Function Validate_Data() As Boolean
        If Len(TxtTitle.Text) = 0 Then
            LblMsg.Text = "Invalid Title Entered"
            Validate_Data = False
            TxtTitle.Focus()
            PnlMsg.Visible = True
            Exit Function
        End If

        If fileUpEx.HasFile = False Then
            LblMsg.Text = "Invalid File Uploaded"
            Validate_Data = False
            PnlMsg.Visible = True
            Exit Function
        End If

        PnlMsg.Visible = False
        Validate_Data = True
    End Function

    Protected Sub TxtDocNo_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtDocNo.TextChanged
        If TxtDocNo.Text.Length = 0 Then
            Call Clear_Controls()
            Session("M_EntryMode") = "NEW"
        Else
            Try
                Dim M_DocumentMaster As New SMARTCORE_BO.DocumentMasterBO
                Dim M_DocumentMasterBL As New DocumentMasterBL
                M_DocumentMaster = M_DocumentMasterBL.Locate_Data(TxtDocNo.Text)
                Locate_Data(M_DocumentMaster)
                M_DocumentMasterBL = Nothing
            Catch ex As Exception

            End Try
        End If

    End Sub

    Protected Sub CmbAccess_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CmbAccess.SelectedIndexChanged

    End Sub
End Class