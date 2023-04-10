Imports System.IO

Public Class frmOrderUploadBrenntag
    Dim Recordcounter As Integer = 0
    Dim FileCounter As Integer = 0

    Private Sub frmOrderUpload_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)

        'Check watch folder and loop through list of files
        'If any file ends with CSV then process

        '  Validate - make sure number of fields is correct and release field is a valid one
        '  Parse file and save to Brenntag or Mosaic tables
        '  If Release number already exists then overwrite unless active = False (already loaded)
        LoadTimer.Enabled = True
    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        Try
            LoadTimer.Enabled = False
            ListBox1.Items.Clear()
            FileCounter = 0
            Recordcounter = 0

            'First try CSV files and process for Brenntag
            GetCSVFiles()

        Catch ex As Exception
            AddLogEntry("OrderUploadBrenntag.LoadTimer_Tick: " & ex.Message)
        End Try
    End Sub

    Private Sub GetCSVFiles()

        Dim AllowedExtension As String = "csv"

        Try
            For Each file As String In IO.Directory.GetFiles(WatchPath, "*.csv")
                Dim filename As String = Path.GetFileName(file)
                ListBox1.Items.Add(filename)
            Next
            AddLogEntry("Found " & ListBox1.Items.Count & " csv Files to process")
            'AddOrderEntry("Found " & ListBox1.Items.Count & " csv Files to process")
            'Now process each file
            For l_index As Integer = 0 To ListBox1.Items.Count - 1
                Dim CSVFile As String = CStr(ListBox1.Items(l_index))
                FileCounter = FileCounter + 1
                If CSVFile.Contains("BL_") Then
                    'Brenntag file
                    ProcessCSVFile(CSVFile)
                Else
                    'Mosaic file
                    ProcessMCSVFile(CSVFile)
                End If
            Next

            lblMsg.Text = FileCounter & " Files processed and " & Recordcounter & " records processed"
            AddLogEntry(FileCounter & " Files processed and " & Recordcounter & " records processed")
            'AddOrderEntry(FileCounter & " Files processed and " & Recordcounter & " records processed")
            frmMain.txtStatus.Text = "Order upload process complete"
            CloseTimer.Enabled = True

        Catch ex As Exception
            AddLogEntry("GetCSVFiles: " & ex.Message)
            AddOrderEntry("GetCSVFiles: " & ex.Message)
            CloseTimer.Enabled = True
        End Try

    End Sub

    Private Sub ProcessCSVFile(ByVal CSVFile As String)
        Dim Brenntag As clsBrenntag
        Dim i As Integer = 0
        Dim j As Integer = 0

        Try
            Brenntag = New clsBrenntag
            'Dim rdr As StreamReader
            Dim lineCount = File.ReadAllLines(WatchPath & CSVFile).Length

            Using rdr = New StreamReader(WatchPath & CSVFile)

                For j = 1 To lineCount

                    Dim parts() As String = rdr.ReadLine().Split(","c)
                    For i = 0 To parts.Length - 1
                        parts(i) = Replace(parts(i), Chr(34), String.Empty)
                        parts(i) = Replace(parts(i), Chr(39), String.Empty)
                    Next

                    txtRelease.Text = parts(0)
                    txtPO.Text = parts(1)
                    txtBOL.Text = parts(2)
                    txtAltPO.Text = parts(3)
                    txtAltCode.Text = parts(4)
                    txtName.Text = parts(5)
                    txtAddress1.Text = parts(6)
                    txtAddress2.Text = parts(7)
                    txtCSZ.Text = parts(8)
                    txtMaxLoad.Text = parts(9)
                    txtNotes.Text = parts(10)

                    If i = 11 Then
                        txtPercentName.Text = "SODIUM HYDROXIDE 50% MEMBRANE"
                        txtPercent.Text = "50"
                    Else
                        txtPercentName.Text = parts(11) & ""    'Name of Caustic
                        If Val(parts(12)) = 1 Then
                            txtPercent.Text = "50"
                        Else
                            txtPercent.Text = Trim(Str(Val(parts(12) * 100))) & ""   '50% or 25% Caustic
                        End If

                    End If

                    'Validate Release number
                    'check to see if it already exists - overwrite if it does

                    If ValidateBId(txtRelease.Text) = True Then
                        'Check to see if updating or adding
                        If BrenntagUpdate = True Then
                            Brenntag.FindRecord(txtRelease.Text)
                            'Now assign to database fields
                            Brenntag.PO = txtPO.Text
                            Brenntag.BOL = txtBOL.Text
                            Brenntag.AltPO = txtAltPO.Text
                            Brenntag.AltCode = txtAltCode.Text
                            Brenntag.Name = txtName.Text
                            Brenntag.Address1 = txtAddress1.Text
                            Brenntag.Address2 = txtAddress2.Text
                            Brenntag.CSZ = txtCSZ.Text
                            Brenntag.MaxLoad = txtMaxLoad.Text
                            Brenntag.Notes = txtNotes.Text
                            Brenntag.PCName = Truncate(txtPercentName.Text, 35)
                            Brenntag.PC = Truncate(txtPercent.Text, 2)
                            If Val(txtMaxLoad.Text) < 60000 Then
                                Brenntag.Active = 1
                            Else
                                Brenntag.Active = 0
                            End If

                            Brenntag.Release = txtRelease.Text
                            Brenntag.UpdateRecord(txtRelease.Text)
                            AddLogEntry("Brenntag Release " & txtRelease.Text & " updated")
                        Else

                            'Now assign to database fields
                            Brenntag.PO = txtPO.Text
                            Brenntag.BOL = txtBOL.Text
                            Brenntag.AltPO = txtAltPO.Text
                            Brenntag.AltCode = txtAltCode.Text
                            Brenntag.Name = txtName.Text
                            Brenntag.Address1 = txtAddress1.Text
                            Brenntag.Address2 = txtAddress2.Text
                            Brenntag.CSZ = txtCSZ.Text
                            Brenntag.MaxLoad = txtMaxLoad.Text
                            Brenntag.Notes = txtNotes.Text
                            Brenntag.PCName = txtPercentName.Text
                            Brenntag.PC = txtPercent.Text
                            If Val(txtMaxLoad.Text) < 60000 Then
                                Brenntag.Active = 1
                            Else
                                Brenntag.Active = 0
                            End If
                            Brenntag.Release = txtRelease.Text
                            'Stop
                            If Brenntag.AddRecord(txtRelease.Text) = True Then
                                AddLogEntry("Brenntag Record added")
                            End If
                        End If
                        'Stop
                    End If
                    Recordcounter = Recordcounter + 1
                    'Stop
                Next j
                Brenntag = Nothing
            End Using
            CompletedFile(CSVFile, "C:\Satco\Completed\")

        Catch ex As Exception
            AddLogEntry("ProcessCSVFile: " & ex.Message)
            AddOrderEntry("ProcessCSVFile: " & ex.Message)
            Brenntag = Nothing
            CompletedFile(CSVFile, "C:\Satco\Failed\")
        End Try
    End Sub

    Private Function ValidateBId(ID As String) As Boolean

        Dim ValidateBrenntag As clsBrenntag

        ValidateBrenntag = New clsBrenntag
        ValidateBId = False
        Try

            If Len(ID) > 0 Then
                If ValidateBrenntag.FindRecord(txtRelease.Text) = True Then
                    AddLogEntry("Release # " & txtRelease.Text & " already exists - Overwrite record")
                    'MsgBox "Release # already exists !", vbOKOnly, "Brenntag Error"
                    '**Overwrite record
                    ValidateBId = True
                    BrenntagUpdate = True
                Else
                    ValidateBId = True
                    BrenntagUpdate = False
                End If
            Else
                MsgBox("A valid Release # is required !", vbOKOnly, "Brenntag Error")
                AddLogEntry("A valid Release # is required ! Brenntag Error")
                AddOrderEntry("A valid Release # is required ! Brenntag Error")
            End If

            ValidateBrenntag = Nothing

        Catch ex As Exception
            AddLogEntry("OrderUploadBrenntag.ValidateId: " & ex.Message)
        End Try

    End Function

    Private Sub ProcessMCSVFile(ByVal CSVFile As String)
        Dim Mosaic As clsMosaic
        Dim i As Integer = 0
        Dim j As Integer = 0

        Try
            Mosaic = New clsMosaic

            'Dim rdr As StreamReader
            Dim lineCount = File.ReadAllLines(WatchPath & CSVFile).Length

            Using rdr = New StreamReader(WatchPath & CSVFile)

                For j = 1 To lineCount

                    Dim parts() As String = rdr.ReadLine().Split(","c)
                    For i = 0 To parts.Length - 1
                        parts(i) = Replace(parts(i), Chr(34), String.Empty)
                        parts(i) = Replace(parts(i), Chr(39), String.Empty)
                    Next
                    txtMosaicRelease.Text = parts(7) & "-" & parts(8)
                    txtConsignee.Text = parts(5)
                    txtDeliveryDate.Text = parts(0)
                    txtProduct.Text = parts(17)
                    txtDocItem.Text = parts(8)
                    txtShipTo.Text = parts(18)
                    txtQuantity.Text = parts(22)
                    'Validate Release number

                    'check to see if it already exists - overwrite if it does

                    If ValidateMId(txtMosaicRelease.Text) = True Then
                        'Check to see if updating or adding
                        If MosaicUpdate = True Then
                            Mosaic.FindRecord(txtMosaicRelease.Text)
                            If Mosaic.Active = False Then
                                'Dont update - already Loaded
                                AddLogEntry("Mosaic Release " & txtMosaicRelease.Text & " not updated - Already Loaded")
                                AddOrderEntry("Mosaic Release " & txtMosaicRelease.Text & " not updated - Already Loaded")
                            Else
                                'Now assign to database fields
                                Mosaic.Release = txtMosaicRelease.Text
                                Mosaic.Consignee = txtConsignee.Text
                                Mosaic.DeliveryDate = txtDeliveryDate.Text
                                Mosaic.Product = txtProduct.Text
                                Mosaic.DocItem = txtDocItem.Text
                                Mosaic.ShipTo = txtShipTo.Text
                                Mosaic.Quantity = txtQuantity.Text
                                Mosaic.Active = 1
                                Mosaic.UpdateRecord(txtMosaicRelease.Text)
                                AddLogEntry("Mosaic Release " & txtMosaicRelease.Text & " updated")
                                'AddOrderEntry("Mosaic Release " & txtMosaicRelease.Text & " updated")
                            End If
                        Else
                            'Now assign to database fields
                            Mosaic.Release = txtMosaicRelease.Text
                            Mosaic.Consignee = txtConsignee.Text
                            Mosaic.DeliveryDate = txtDeliveryDate.Text
                            Mosaic.Product = txtProduct.Text
                            Mosaic.DocItem = txtDocItem.Text
                            Mosaic.ShipTo = txtShipTo.Text
                            Mosaic.Quantity = txtQuantity.Text
                            Mosaic.Active = 1
                            If Mosaic.AddRecord(txtMosaicRelease.Text) = True Then
                                AddLogEntry("Mosaic Record added")
                                'AddOrderEntry("Mosaic Record added")
                            End If
                        End If
                        'Stop
                    End If
                    Recordcounter = Recordcounter + 1
                Next j

                Mosaic = Nothing
            End Using
            CompletedFile(CSVFile, "C:\Satco\Completed\")

        Catch ex As Exception
            AddLogEntry("ProcessMCSVFile: " & ex.Message)
            AddOrderEntry("ProcessMCSVFile: " & ex.Message)
            Mosaic = Nothing
            CompletedFile(CSVFile, "C:\Satco\Failed\")
        End Try
    End Sub

    Private Function ValidateMId(ID As String) As Boolean

        Dim Mosaic As clsMosaic

        Mosaic = New clsMosaic
        ValidateMId = False
        Try

            If Len(ID) > 0 Then
                If Mosaic.FindRecord(txtMosaicRelease.Text) = True Then
                    AddLogEntry("Release # " & txtMosaicRelease.Text & " already exists - Overwrite record")
                    AddOrderEntry("Release # " & txtMosaicRelease.Text & " already exists - Overwrite record")
                    '**Overwrite record
                    ValidateMId = True
                    MosaicUpdate = True
                Else
                    AddLogEntry("Release # " & txtMosaicRelease.Text & " not found - Adding record")
                    AddOrderEntry("Release # " & txtMosaicRelease.Text & " not found - Adding record")
                    ValidateMId = True
                    MosaicUpdate = False
                End If
            Else
                MsgBox("A valid Release # is required !", vbOKOnly, "Mosaic Error")
                AddLogEntry("A valid Release # is required ! Mosaic Error")
                AddOrderEntry("A valid Release # is required ! Mosaic Error")
            End If

            Mosaic = Nothing

        Catch ex As Exception
            AddLogEntry("OrderUploadBrenntag.ValidateMId: " & ex.Message)
        End Try

    End Function

    Private Sub CompletedFile(ByVal File2Move As String, ByVal DestPath As String)
        Try
            AddLogEntry("Moving " & File2Move & "  to " & DestPath)
            'AddOrderEntry("Moving " & File2Move & "  to " & DestPath)
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