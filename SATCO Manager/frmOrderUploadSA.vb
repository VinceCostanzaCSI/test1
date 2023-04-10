Imports System.Data.OleDb
Imports System.IO
Imports Spire.Xls

Public Class frmOrderUploadSA
    Private SQL As New SQLControl

    Dim DtSet As System.Data.DataSet
    Dim Recordcounter As Integer = 0
    Dim FileCounter As Integer = 0

    Private Sub frmOrderUploadSA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            AddLogEntry("OrderUploadSA.LoadTimer_Tick: " & ex.Message)
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
            Dim XLSFile As String
            'Dim UpdatedXLSFile As String
            'For l_index As Integer = 0 To ListBox1.Items.Count - 1
            '    XLSFile = CStr(ListBox1.Items(l_index))
            '    'FileCounter = FileCounter + 1
            '    'First Open and Resave it before processing
            '    ResaveExcel(WatchPath, XLSFile)
            'Next
            ''Redo the file List again
            'ListBox1.Items.Clear()
            'For Each file As String In IO.Directory.GetFiles(WatchPath, "*.xlsx")
            '    Dim filename As String = Path.GetFileName(file)
            '    ListBox1.Items.Add(filename)
            'Next
            For l_index As Integer = 0 To ListBox1.Items.Count - 1
                XLSFile = CStr(ListBox1.Items(l_index))
                FileCounter = FileCounter + 1
                'Now process the modified Excel file
                ProcessXFile(XLSFile)

            Next
            'Exit Sub
            lblMsg.Text = FileCounter & " Files processed and " & Recordcounter & " records processed"
            AddLogEntry(FileCounter & " Files processed and " & Recordcounter & " records processed")
            'AddOrderEntry(FileCounter & " Files processed and " & Recordcounter & " records processed")
            frmMain.txtStatus.Text = "Order upload process complete"
            CloseTimer.Enabled = True

        Catch ex As Exception
            AddLogEntry("SA-GetXLSFiles: " & ex.Message)
            AddOrderEntry("SA-GetXLSFiles: " & ex.Message)
            CloseTimer.Enabled = True
        End Try
    End Sub

    Private Sub ProcessXFile(ByVal XLSFile As String)
        'First Read xlsx file into DGV
        'Then move DGV contents into database
        Dim SA As clsSA
        Dim FilePath As String = WatchPath & XLSFile
        Dim ShiftRows As Integer

        Dim dataSet As System.Data.DataSet
        Dim wb As Workbook = New Workbook()
        SA = New clsSA

        Try

            dataSet = New System.Data.DataSet

            'Load an Excel file
            wb.LoadFromFile(FilePath)
            Dim sheet As Worksheet = wb.Worksheets(0)

            Dim startIdx As Integer
            Dim endIdx As Integer
            'Get the row count
            Dim rowCount = sheet.LastRow
            'Get the column count
            Dim columnCount As Integer = endIdx - startIdx + 1

            'Export the data of specified cells to datatable
            DGV1.DataSource = sheet.ExportDataTable

            'First check to see if ConsigneeID is located where it is supposed to be
            txtConsigneeID.Text = DGV1.Rows(23).Cells(19).Value.ToString & ""
            If txtConsigneeID.Text = "" Then
                'Shift rows up by 2
                ShiftRows = -2
            Else
                ShiftRows = 0
            End If

            'Now Get Ship To ID, PO, Tank
            txtOrderDate.Text = DGV1.Rows(59 + ShiftRows).Cells(6).Value.ToString & ""
            txtConsigneeID.Text = DGV1.Rows(23 + ShiftRows).Cells(19).Value.ToString & ""
            If txtConsigneeID.Text <> "" Then
                txtConsigneeID.Text = txtConsigneeID.Text.Substring(txtConsigneeID.Text.Length - 10)
            End If
            txtConsigneeID.Text = txtConsigneeID.Text.Trim
            txtConsigneeName.Text = DGV1.Rows(30 + ShiftRows).Cells(19).Value.ToString & ""
            txtPO.Text = DGV1.Rows(63 + ShiftRows).Cells(6).Value.ToString & ""
            txtTank.Text = DGV1.Rows(58 + ShiftRows).Cells(25).Value.ToString & ""
            If DefaultLocation = "T" Then
                txtTank.Text = Truncate(txtTank.Text, 2)    'Tampa Tank = 2 characters
            Else
                txtTank.Text = Truncate(txtTank.Text, 3)    'Stockton Tank = 3 characters
            End If

            lblMsg.Text = "Number of Rows: " & DGV1.RowCount

            'Next move DGV into database if valid Release number
            For row = 72 + ShiftRows To DGV1.RowCount - 2
                If DGV1.Rows(row).Cells(0).Value.ToString & "" <> "" Then
                    txtQuantity.Text = DGV1.Rows(row).Cells(0).Value.ToString & ""
                    txtProduct.Text = DGV1.Rows(row).Cells(7).Value.ToString & ""
                    txtAnalysis.Text = DGV1.Rows(row).Cells(9).Value.ToString & ""
                    txtRelease.Text = DGV1.Rows(row).Cells(17).Value.ToString & ""
                    txtShipDate.Text = DGV1.Rows(row).Cells(28).Value.ToString & ""

                    If ValidateId(txtRelease.Text) = True Then
                        'Check to see if updating or adding
                        If SAUpdate = True Then
                            SA.FindRecord(txtRelease.Text)
                            If SA.Active = False Then
                                'Dont update - already Loaded
                                AddLogEntry("SA Release " & txtRelease.Text & " not updated - Already Loaded")
                                AddOrderEntry("SA Release " & txtRelease.Text & " not updated - Already Loaded")
                            Else
                                'Now assign to database fields
                                SA.Release = Truncate(txtRelease.Text, 16)
                                SA.Consignee = Truncate(txtConsigneeID.Text, 25)
                                SA.Quantity = Truncate(txtQuantity.Text, 8)
                                SA.Product = Truncate(txtProduct.Text, 15)
                                SA.Analysis = Truncate(txtAnalysis.Text, 8)
                                SA.Tank = txtTank.Text
                                SA.DeliveryDate = txtShipDate.Text
                                SA.ShipTo = Truncate(txtConsigneeName.Text, 35)
                                SA.PO = Truncate(txtPO.Text, 20)
                                If Val(txtQuantity.Text) < 30 Then
                                    SA.Active = 1
                                Else
                                    SA.Active = 0
                                    AddLogEntry("Quantity over 30 Tons - marking Order inactive")
                                End If
                                'SA.Active = 1
                                SA.UpdateRecord(txtRelease.Text)
                                AddLogEntry("SA Release " & txtRelease.Text & " updated")
                                AddOrderEntry("SA Release " & txtRelease.Text & " updated")
                            End If
                        Else
                            'Now assign to database fields
                            SA.Release = Truncate(txtRelease.Text, 16)
                            SA.Consignee = Truncate(txtConsigneeID.Text, 25)
                            SA.Quantity = Truncate(txtQuantity.Text, 8)
                            SA.Product = Truncate(txtProduct.Text, 15)
                            SA.Analysis = Truncate(txtAnalysis.Text, 8)
                            SA.Tank = txtTank.Text
                            SA.DeliveryDate = txtShipDate.Text
                            SA.ShipTo = Truncate(txtConsigneeName.Text, 35)
                            SA.PO = Truncate(txtPO.Text, 20)
                            If Val(txtQuantity.Text) < 30 Then
                                SA.Active = 1
                            Else
                                SA.Active = 0
                                AddLogEntry("Quantity over 30 Tons - marking Order inactive")
                            End If
                            'SA.Active = 1
                            If SA.AddRecord(txtRelease.Text) = True Then
                                AddLogEntry("SA Record added")
                                'AddOrderEntry("SA Record added")
                            End If
                        End If
                        Recordcounter = Recordcounter + 1
                    End If
                End If
            Next
            If Recordcounter = 0 Then
                CompletedFile(XLSFile, "C:\Satco\Failed\")
            Else
                CompletedFile(XLSFile, "C:\Satco\Completed\")
            End If

        Catch ex As Exception
            AddOrderEntry("SA-ProcessXLSXFile: " & ex.Message)
            AddLogEntry("SA-ProcessXLSXFile: " & ex.Message)
            CompletedFile(XLSFile, "C:\Satco\Failed\")

        End Try
    End Sub


    Private Function ValidateId(ID As String) As Boolean

        Dim SA As clsSA
        Dim Consignee As clsConsignee

        SA = New clsSA
        Consignee = New clsConsignee

        ValidateId = False
        Try
            'First check to see if this is a valid Consignee
            If Consignee.FindRecord("SA" & txtConsigneeID.Text) = False Then
                AddLogEntry("Consignee ID " & txtConsigneeID.Text & " does not exist")
                AddOrderEntry("Consignee ID " & txtConsigneeID.Text & " does not exist")
                lblMsg.Text = "Consignee ID " & txtConsigneeID.Text & " does not exist"
                ValidateId = False
            Else
                If Len(ID) > 0 Then
                    If SA.FindRecord(txtRelease.Text) = True Then
                        AddLogEntry("Release # " & txtRelease.Text & " already exists - Overwrite record")
                        AddOrderEntry("Release # " & txtRelease.Text & " already exists - Overwrite record")
                        '**Overwrite record
                        ValidateId = True
                        SAUpdate = True
                    Else
                        AddLogEntry("Release # " & txtRelease.Text & " not found - Adding record")
                        AddOrderEntry("Release # " & txtRelease.Text & " not found - Adding record")
                        ValidateId = True
                        SAUpdate = False
                    End If
                Else
                    lblMsg.Text = "A valid Release # is required! SA Error"
                    AddOrderEntry("A valid Release # is required! SA Error")
                End If
            End If

            SA = Nothing
            Consignee = Nothing

        Catch ex As Exception
            AddLogEntry("OrderUploadSA.ValidateId: " & ex.Message)
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
            'AddOrderEntry("CompletedFile: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub CloseTimer_Tick(sender As System.Object, e As System.EventArgs) Handles CloseTimer.Tick
        CloseTimer.Enabled = False
        Me.Close()
    End Sub

    Private Sub DGV1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGV1.CellDoubleClick
        Try
            txtRelease.Text = DGV1.CurrentRow.Index
            txtConsigneeName.Text = DGV1.CurrentCellAddress.ToString

        Catch ex As Exception
            AddLogEntry("OrderUploadSA.DGV1_CellDoubleClick: " & ex.Message)
        End Try

    End Sub
End Class