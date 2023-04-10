Imports System.Data.OleDb
Imports System.IO

Public Class frmOrderUploadMosaic
    Private SQL As New SQLControl

    Dim MyConnection As System.Data.OleDb.OleDbConnection
    Dim DtSet As System.Data.DataSet
    Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
    Dim Recordcounter As Integer = 0
    Dim FileCounter As Integer = 0

    Private Sub frmOrderUploadMosaic_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        LoadTimer.Enabled = True

    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        Try
            LoadTimer.Enabled = False
            ListBox1.Items.Clear()
            Recordcounter = 0
            FileCounter = 0
            GetXLSFiles()

        Catch ex As Exception
            AddLogEntry("OrderUploadMosaic.LoadTimer_Tick: " & ex.Message)
        End Try

    End Sub

    Private Sub GetXLSFiles()

        Dim AllowedExtension As String = "xlsx"

        Try
            For Each file As String In IO.Directory.GetFiles(WatchPath, "*.xlsx")
                Dim filename As String = Path.GetFileName(file)
                ListBox1.Items.Add(filename)
            Next

            AddLogEntry("Found " & ListBox1.Items.Count & " xlsx Files to process")
            'AddOrderEntry("Found " & ListBox1.Items.Count & " xlsx Files to process")
            'Now process each file
            For l_index As Integer = 0 To ListBox1.Items.Count - 1
                Dim XLSFile As String = CStr(ListBox1.Items(l_index))
                FileCounter = FileCounter + 1
                ProcessXLSXFile(XLSFile)
            Next
            lblMsg.Text = FileCounter & " Files processed and " & Recordcounter & " records processed"
            AddLogEntry(FileCounter & " Files processed and " & Recordcounter & " records processed")
            'AddOrderEntry(FileCounter & " Files processed and " & Recordcounter & " records processed")
            frmMain.txtStatus.Text = "Order upload process complete"
            CloseTimer.Enabled = True

        Catch ex As Exception
            AddLogEntry("GetXLSFiles: " & ex.Message)
            AddOrderEntry("GetXLSFiles: " & ex.Message)
            CloseTimer.Enabled = True
        End Try
    End Sub

    Private Sub ProcessXLSXFile(ByVal XLSFile As String)
        'First Read xlsx file into DGV
        'Then move DGV contents into database
        Dim Mosaic As clsMosaic
        Dim FilePath As String = WatchPath & XLSFile

        Try
            Mosaic = New clsMosaic

            MyConnection = New System.Data.OleDb.OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", FilePath))
            MyConnection.Open()
            Dim dtSheets As DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim listSheet As New List(Of String)
            Dim drSheet As DataRow

            For Each drSheet In dtSheets.Rows
                listSheet.Add(drSheet("TABLE_NAME").ToString())
            Next

            Dim Sheet1 As String = listSheet.Item(0)
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & Sheet1 & "]", MyConnection)
            MyCommand.TableMappings.Add("Table", " ")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)
            DGV1.DataSource = DtSet.Tables(0)
            MyConnection.Close()

            '0	A DeliveryDate
            '1	B Create Date
            '2	C Sold To Party
            '3	D Sold To Name
            '4	E Sales Group Desc
            '5	F SSR Name
            '6	G SSR Name
            '7	H Sales Document
            '8	I Sales Doc Item
            '9	J CDS Instructions
            '10	K PO Header
            '11	L PO Item
            '12	M Plant
            '13	N Plant Desc
            '14	O Means of Transport
            '15	P Material
            '16	Q Material Name
            '17	R Ship To Name
            '18	S Ship To Street
            '19	T Ship To City
            '20	U Ship To Region
            '21	V Open Order Quantity

            'Now move DGV into database if valid Release number
            For Each row As DataGridViewRow In DGV1.Rows
                If Not row.IsNewRow Then
                    Dim DocItem As Integer = Val(row.Cells(8).Value.ToString) + 0
                    If DocItem > 0 And DocItem < 9000 Then
                        txtRelease.Text = row.Cells(7).Value.ToString & "-" & Trim(Str(DocItem))
                        txtConsignee.Text = row.Cells(17).Value.ToString
                        txtDeliveryDate.Text = row.Cells(0).Value.ToString
                        txtProduct.Text = row.Cells(16).Value.ToString
                        txtDocItem.Text = Trim(Str(DocItem))
                        txtShipTo.Text = row.Cells(17).Value.ToString
                        txtQuantity.Text = "40"

                        If ValidateMId(txtRelease.Text) = True Then
                            'Check to see if updating or adding
                            If MosaicUpdate = True Then
                                Mosaic.FindRecord(txtRelease.Text)
                                If Mosaic.Active = False Then
                                    'Dont update - already Loaded
                                    AddLogEntry("Mosaic Release " & txtRelease.Text & " not updated - Already Loaded")
                                    AddOrderEntry("Mosaic Release " & txtRelease.Text & " not updated - Already Loaded")
                                Else
                                    'Now assign to database fields
                                    Mosaic.Release = Truncate(txtRelease.Text, 15)
                                    Mosaic.Consignee = Truncate(txtConsignee.Text, 25)
                                    Mosaic.DeliveryDate = txtDeliveryDate.Text
                                    Mosaic.Product = Truncate(txtProduct.Text, 15)
                                    Mosaic.DocItem = Truncate(txtDocItem.Text, 15)
                                    Mosaic.ShipTo = Truncate(txtShipTo.Text, 30)
                                    Mosaic.Quantity = Truncate(txtQuantity.Text, 8)
                                    Mosaic.Active = 1
                                    Mosaic.UpdateRecord(txtRelease.Text)
                                    AddLogEntry("Mosaic Release " & txtRelease.Text & " updated")
                                    'AddOrderEntry("Mosaic Release " & txtRelease.Text & " updated")
                                End If
                            Else
                                'Now assign to database fields
                                Mosaic.Release = Truncate(txtRelease.Text, 15)
                                Mosaic.Consignee = Truncate(txtConsignee.Text, 25)
                                Mosaic.DeliveryDate = txtDeliveryDate.Text
                                Mosaic.Product = Truncate(txtProduct.Text, 15)
                                Mosaic.DocItem = Truncate(txtDocItem.Text, 15)
                                Mosaic.ShipTo = Truncate(txtShipTo.Text, 30)
                                Mosaic.Quantity = Truncate(txtQuantity.Text, 8)
                                Mosaic.Active = 1
                                If Mosaic.AddRecord(txtRelease.Text) = True Then
                                    AddLogEntry("Mosaic Record added")
                                    'AddOrderEntry("Mosaic Record added")
                                End If
                            End If
                        End If

                        Recordcounter = Recordcounter + 1
                    End If
                End If
            Next
            CompletedFile(XLSFile, "C:\Satco\Completed\")

        Catch ex As Exception
            'MsgBox(ex.Message)
            AddOrderEntry(ex.Message)
            CompletedFile(XLSFile, "C:\Satco\Failed\")
            MyConnection.Close()
        End Try
    End Sub

    Private Function ValidateMId(ID As String) As Boolean

        Dim Mosaic As clsMosaic

        Mosaic = New clsMosaic
        ValidateMId = False
        Try



            If Len(ID) > 0 Then
                If Mosaic.FindRecord(txtRelease.Text) = True Then
                    AddLogEntry("Release # " & txtRelease.Text & " already exists - Overwrite record")
                    AddOrderEntry("Release # " & txtRelease.Text & " already exists - Overwrite record")
                    '**Overwrite record
                    ValidateMId = True
                    MosaicUpdate = True
                Else
                    AddLogEntry("Release # " & txtRelease.Text & " not found - Adding record")
                    AddOrderEntry("Release # " & txtRelease.Text & " not found - Adding record")
                    ValidateMId = True
                    MosaicUpdate = False
                End If
            Else
                lblMsg.Text = "A valid Release # is required! Mosaic Error"
                AddOrderEntry("A valid Release # is required! Mosaic Error")
            End If

            Mosaic = Nothing

        Catch ex As Exception
            AddLogEntry("OrderUploadMosaic.ValidateMId: " & ex.Message)
        End Try

    End Function

    Private Sub CompletedFile(ByVal File2Move As String, ByVal DestPath As String)
        Try
            AddLogEntry("Moving " & File2Move & "  to " & DestPath)
            'AddOrderEntry("Moving " & File2Move & " to " & DestPath)
            File.Copy(WatchPath & File2Move, DestPath & File2Move, True)
            File.Delete(WatchPath & File2Move)

        Catch ex As Exception
            AddLogEntry("CompletedFile: " & ex.Message)
            AddOrderEntry("CompletedFile: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub CloseTimer_Tick(sender As System.Object, e As System.EventArgs) Handles CloseTimer.Tick
        CloseTimer.Enabled = False
        Me.Close()
    End Sub
End Class