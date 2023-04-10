Imports System.IO

Public Class frmSetup
    Private SQL As New SQLControl

    Dim ActiveCom As Integer
    Dim Port(4) As Integer
    Dim Baud(4) As Integer
    Dim Parity(4) As Integer
    Dim DataBits(4) As Integer
    Dim StopBits(4) As Integer
    Dim Status(4) As Integer
    Dim Active(5) As Integer

    Private Sub frmSetup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim SysOptions As clsSystem
        CenterForm(Me)
        Try
            SysOptions = New clsSystem
            TabControl1.TabIndex = SysOptions.CurrentTab
            FillCombos()
            FillVariables()

            'Read from database for DriverInfo, MosaicCOA and BrenntagCOA
            SysOptions.GetCurrentRecord()

            'Driver Tab
            txtDriverTraining.Text = SysOptions.DriverTraining
            txtDriverWarning.Text = SysOptions.DriverWarning
            txtDriverExpires.Text = SysOptions.DriverExpires
            txtDriverTWIC.Text = SysOptions.DriverTWIC

            'SATCO Tab
            chkSARelease.CheckState = SysOptions.SARelease

            'MosaicCOA Tab
            txtMODate.Text = SysOptions.MODate
            txtMOProduct.Text = SysOptions.MOProduct
            txtMOTotalNi.Text = SysOptions.MOTotalNi
            txtMOUreaNi.Text = SysOptions.MOUreaNi
            txtMOGravity.Text = SysOptions.MOGravity
            txtMONitrate.Text = SysOptions.MONitrate
            txtMOppg.Text = SysOptions.MOppg

            'BrenntagCOA Tab
            txtBNGravity.Text = SysOptions.BNGravity
            txtBNppg.Text = SysOptions.BNppg
            txtBNCoA0.Text = SysOptions.BNCOA1
            txtBNCoA1.Text = SysOptions.BNCOA2
            txtBNCoA2.Text = SysOptions.BNCOA3
            txtBNCoA3.Text = SysOptions.BNCOA4
            txtBNCoA4.Text = SysOptions.BNCOA5
            txtBNCoA5.Text = SysOptions.BNCOA6
            txtBNCoA6.Text = SysOptions.BNCOA7
            txtBNCoA7.Text = SysOptions.BNCOA8
            txtBNCoA8.Text = SysOptions.BNCOA9
            txtBNCoA9.Text = SysOptions.BNCOA10
            txtBNCoA10.Text = SysOptions.BNCOA11
            txtBNCoA11.Text = SysOptions.BNCOA12
            txtBNCoA12.Text = SysOptions.BNCOA13
            txtBNCoA13.Text = SysOptions.BNCOA14
            '25% Caustic
            txtBTGravity.Text = SysOptions.BTGravity
            txtBTppg.Text = SysOptions.BTppg
            txtBTCoA0.Text = SysOptions.BTCOA1
            txtBTCoA1.Text = SysOptions.BTCOA2
            txtBTCoA2.Text = SysOptions.BTCOA3
            txtBTCoA3.Text = SysOptions.BTCOA4
            txtBTCoA4.Text = SysOptions.BTCOA5
            txtBTCoA5.Text = SysOptions.BTCOA6
            txtBTCoA6.Text = SysOptions.BTCOA7
            txtBTCoA7.Text = SysOptions.BTCOA8
            txtBTCoA8.Text = SysOptions.BTCOA9
            txtBTCoA9.Text = SysOptions.BTCOA10
            txtBTCoA10.Text = SysOptions.BTCOA11
            txtBTCoA11.Text = SysOptions.BTCOA12
            txtBTCoA12.Text = SysOptions.BTCOA13
            txtBTCoA13.Text = SysOptions.BTCOA14
            txtH2OFlow.Text = SysOptions.H2OFlow

            txtCardStart.Text = SysOptions.CardReaderStart
            txtCardLength.Text = SysOptions.CardReaderLength
            txtGateCardStart.Text = SysOptions.AccessReaderStart
            txtGateCardLength.Text = SysOptions.AccessReaderLength

            'Path Tab
            txtDBPath.Text = SysOptions.DatabasePath
            txtRptPath.Text = SysOptions.ReportPath
            txtDocPath.Text = SysOptions.DocumentPath
            txtWatchPath1.Text = SysOptions.WatchPath1
            txtWatchPath2.Text = SysOptions.WatchPath2
            txtWatchPath3.Text = SysOptions.WatchPath3

            'Control Tab
            chkControl.CheckState = SysOptions.Control
            txtIPAddress.Text = SysOptions.AccessReaderIP
            txtIPAddress2.Text = SysOptions.AccessReaderIP2
            txtIPAddress3.Text = SysOptions.AccessRelayIP

            ScaleNumber = SysOptions.ScaleNumber

            optType0.Checked = False

            SysOptions = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
            SysOptions = Nothing
        End Try
    End Sub

    Sub FillCombos()
        cmbDevicePort.Items.Clear()
        cmbDeviceBaud.Items.Clear()
        cmbDeviceDataBits.Items.Clear()
        cmbDeviceStopBits.Items.Clear()
        cmbDeviceParity.Items.Clear()

        cmbDevicePort.Items.Add("COM1:")
        cmbDevicePort.Items.Add("COM2:")
        cmbDevicePort.Items.Add("COM3:")
        cmbDevicePort.Items.Add("COM4:")
        cmbDevicePort.Items.Add("COM5:")
        cmbDevicePort.Items.Add("COM6:")
        cmbDevicePort.Items.Add("COM7:")
        cmbDevicePort.Items.Add("COM8:")

        cmbDeviceBaud.Items.Add(" 1200")
        cmbDeviceBaud.Items.Add(" 2400")
        cmbDeviceBaud.Items.Add(" 4800")
        cmbDeviceBaud.Items.Add(" 9600")
        cmbDeviceBaud.Items.Add("14400")
        cmbDeviceBaud.Items.Add("19200")
        cmbDeviceBaud.Items.Add("38400")

        cmbDeviceDataBits.Items.Add("7")
        cmbDeviceDataBits.Items.Add("8")

        cmbDeviceStopBits.Items.Add("1")
        cmbDeviceStopBits.Items.Add("2")

        cmbDeviceParity.Items.Add("NONE ")
        cmbDeviceParity.Items.Add("EVEN ")
        cmbDeviceParity.Items.Add("ODD  ")
        cmbDeviceParity.Items.Add("SPACE")
    End Sub

    Sub FillVariables()
        Dim SysOptions As clsSystem

        SysOptions = New clsSystem

        Port(0) = SysOptions.ScalePort
        Baud(0) = SysOptions.ScaleBaud
        Parity(0) = SysOptions.ScaleParity
        DataBits(0) = SysOptions.ScaleDataBits
        StopBits(0) = SysOptions.ScaleStopBits
        Status(0) = SysOptions.ScaleStatus
        Active(0) = SysOptions.ScaleActive

        Port(1) = SysOptions.CardReaderPort
        Baud(1) = SysOptions.CardReaderBaud
        Parity(1) = SysOptions.CardReaderParity
        DataBits(1) = SysOptions.CardReaderDataBits
        StopBits(1) = SysOptions.CardReaderStopBits
        Status(1) = SysOptions.CardReaderStatus
        Active(1) = SysOptions.CardReaderActive

        Port(2) = SysOptions.AccessReaderPort
        Baud(2) = SysOptions.AccessReaderBaud
        Parity(2) = SysOptions.AccessReaderParity
        DataBits(2) = SysOptions.AccessReaderDataBits
        StopBits(2) = SysOptions.AccessReaderStopBits
        Status(2) = SysOptions.AccessReaderStatus
        Active(2) = SysOptions.AccessReaderActive
        SysOptions = Nothing

    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        Dim SysOptions As clsSystem
        Dim UpdateCmd As String

        SysOptions = New clsSystem
        Try

            'Scale Indicator
            SysOptions.ScalePort = Port(0)
            SysOptions.ScaleBaud = Baud(0)
            SysOptions.ScaleParity = Parity(0)
            SysOptions.ScaleDataBits = DataBits(0)
            SysOptions.ScaleStopBits = StopBits(0)
            SysOptions.ScaleStatus = Status(0)
            SysOptions.ScaleActive = Active(0)

            'RFID Reader
            SysOptions.CardReaderPort = Port(1)
            SysOptions.CardReaderBaud = Baud(1)
            SysOptions.CardReaderParity = Parity(1)
            SysOptions.CardReaderDataBits = DataBits(1)
            SysOptions.CardReaderStopBits = StopBits(1)
            SysOptions.CardReaderStatus = Status(1)
            SysOptions.CardReaderActive = Active(1)

            'Gate RFID Reader
            SysOptions.AccessReaderPort = Port(2)
            SysOptions.AccessReaderBaud = Baud(2)
            SysOptions.AccessReaderParity = Parity(2)
            SysOptions.AccessReaderDataBits = DataBits(2)
            SysOptions.AccessReaderStopBits = StopBits(2)
            SysOptions.AccessReaderStatus = Status(2)
            SysOptions.AccessReaderActive = Active(2)

            'Control Tab
            SysOptions.Control = chkControl.CheckState

            'Paths Tab
            SysOptions.DatabasePath = txtDBPath.Text
            SysOptions.ReportPath = txtRptPath.Text
            SysOptions.DocumentPath = txtDocPath.Text
            If txtWatchPath1.Text = txtDBPath.Text Then
                MsgBox("Warning: Watch folder is the same as the Database path")
            End If
            SysOptions.WatchPath1 = txtWatchPath1.Text
            SysOptions.WatchPath2 = txtWatchPath2.Text
            SysOptions.WatchPath3 = txtWatchPath3.Text

            SysOptions.CardReaderLength = Val(txtCardLength.Text)
            SysOptions.CardReaderStart = Val(txtCardStart.Text)
            SysOptions.AccessReaderLength = Val(txtGateCardLength.Text)
            SysOptions.AccessReaderStart = Val(txtGateCardStart.Text)
            SysOptions.AccessReaderIP = txtIPAddress.Text
            SysOptions.AccessReaderIP2 = txtIPAddress2.Text
            SysOptions.AccessRelayIP = txtIPAddress3.Text

            'SATCO Tab
            SysOptions.SARelease = chkSARelease.CheckState

            'Mosaic tab
            SysOptions.MODate = txtMODate.Text
            SysOptions.MOProduct = txtMOProduct.Text
            SysOptions.MOTotalNi = txtMOTotalNi.Text
            SysOptions.MOUreaNi = txtMOUreaNi.Text
            SysOptions.MOGravity = txtMOGravity.Text
            SysOptions.MONitrate = txtMOUreaNi.Text
            SysOptions.MOppg = txtMOppg.Text

            'Brenntag tab
            SysOptions.BNGravity = txtBNGravity.Text
            SysOptions.BNppg = txtBNppg.Text
            SysOptions.BNCOA1 = txtBNCoA0.Text
            SysOptions.BNCOA2 = txtBNCoA1.Text
            SysOptions.BNCOA3 = txtBNCoA2.Text
            SysOptions.BNCOA4 = txtBNCoA3.Text
            SysOptions.BNCOA5 = txtBNCoA4.Text
            SysOptions.BNCOA6 = txtBNCoA5.Text
            SysOptions.BNCOA7 = txtBNCoA6.Text
            SysOptions.BNCOA8 = txtBNCoA7.Text
            SysOptions.BNCOA9 = txtBNCoA8.Text
            SysOptions.BNCOA10 = txtBNCoA9.Text
            SysOptions.BNCOA11 = txtBNCoA10.Text
            SysOptions.BNCOA12 = txtBNCoA11.Text
            SysOptions.BNCOA13 = txtBNCoA12.Text
            SysOptions.BNCOA14 = txtBNCoA13.Text
            '25% Caustic
            SysOptions.BTGravity = txtBTGravity.Text
            SysOptions.BTppg = txtBTppg.Text
            SysOptions.BTCOA1 = txtBTCoA0.Text
            SysOptions.BTCOA2 = txtBTCoA1.Text
            SysOptions.BTCOA3 = txtBTCoA2.Text
            SysOptions.BTCOA4 = txtBTCoA3.Text
            SysOptions.BTCOA5 = txtBTCoA4.Text
            SysOptions.BTCOA6 = txtBTCoA5.Text
            SysOptions.BTCOA7 = txtBTCoA6.Text
            SysOptions.BTCOA8 = txtBTCoA7.Text
            SysOptions.BTCOA9 = txtBTCoA8.Text
            SysOptions.BTCOA10 = txtBTCoA9.Text
            SysOptions.BTCOA11 = txtBTCoA10.Text
            SysOptions.BTCOA12 = txtBTCoA11.Text
            SysOptions.BTCOA13 = txtBTCoA12.Text
            SysOptions.BTCOA14 = txtBTCoA13.Text
            SysOptions.H2OFlow = txtH2OFlow.Text

            SysOptions = Nothing

            'Update DriverInfo, SATCO, MosaicCOA and BrenntagCOA
            Try
                'Update DriverInfo
                UpdateCmd = "UPDATE DriverInfo " &
                                      "SET Warning ='" & txtDriverWarning.Text & "' , " &
                                      "Training ='" & txtDriverTraining.Text & "' , " &
                                      "Expires ='" & txtDriverExpires.Text & "' , " &
                                      "TWIC ='" & txtDriverTWIC.Text & "'"

                If SQL.DataUpdate(UpdateCmd) = 0 Then
                    AddLogEntry("Error updating Driver File")
                Else
                    'AddLogEntry("System File Updated")
                End If
            Catch ex As Exception
                AddLogEntry("MOUpdateRecord: " & ex.Message)
            End Try

            Try
                If chkSARelease.Checked Then
                    UpdateCmd = "Update Consignee set Release = '1' where Code like 'SA%'"
                Else
                    UpdateCmd = "Update Consignee set Release = '0' where Code like 'SA%'"
                End If
                If SQL.DataUpdate(UpdateCmd) = 0 Then
                    AddLogEntry("Error updating Consignee File to change Release requirements")
                Else
                    'AddLogEntry("System File Updated")
                End If
            Catch ex As Exception
                AddLogEntry("SAUpdateRecord: " & ex.Message)
            End Try

            Try
                'Update MosaicCOA
                UpdateCmd = "UPDATE MosaicCOA " &
                                      "SET MOGravity ='" & txtMOGravity.Text & "' , " &
                                      "MOppg ='" & txtMOppg.Text & "' , " &
                                      "MODate ='" & txtMODate.Text & "' , " &
                                      "MOProduct ='" & txtMOProduct.Text & "' , " &
                                      "MOTotalNi ='" & txtMOTotalNi.Text & "' , " &
                                      "MOUreaNi ='" & txtMOUreaNi.Text & "' , " &
                                      "MONitrate ='" & txtMONitrate.Text & "'"
                If SQL.DataUpdate(UpdateCmd) = 0 Then
                    AddLogEntry("Error updating MosaicCOA File")
                Else
                    'AddLogEntry("MosaicCOA File Updated")
                End If
            Catch ex As Exception
                AddLogEntry("MOUpdateRecord: " & ex.Message)
            End Try

            Try
                'Update BrenntagCOA
                UpdateCmd = "UPDATE BrenntagCOA " &
                        "SET BNGravity ='" & txtBNGravity.Text & "' , " &
                        "BNppg = '" & txtBNppg.Text & "' , " &
                        "BNCOA1 = '" & txtBNCoA0.Text & "' , " &
                        "BNCOA2 = '" & txtBNCoA1.Text & "' , " &
                        "BNCOA3 = '" & txtBNCoA2.Text & "' , " &
                        "BNCOA4 = '" & txtBNCoA3.Text & "' , " &
                        "BNCOA5 = '" & txtBNCoA4.Text & "' , " &
                        "BNCOA6 = '" & txtBNCoA5.Text & "' , " &
                        "BNCOA7 = '" & txtBNCoA6.Text & "' , " &
                        "BNCOA8 = '" & txtBNCoA7.Text & "' , " &
                        "BNCOA9 = '" & txtBNCoA8.Text & "' , " &
                        "BNCOA10 = '" & txtBNCoA9.Text & "' , " &
                        "BNCOA11 = '" & txtBNCoA10.Text & "' , " &
                        "BNCOA12 = '" & txtBNCoA11.Text & "' , " &
                        "BNCOA13 = '" & txtBNCoA12.Text & "' , " &
                        "BNCOA14 = '" & txtBNCoA13.Text & "' , " &
                        "BTGravity ='" & txtBTGravity.Text & "' , " &
                        "BTppg = '" & txtBTppg.Text & "' , " &
                        "BTCOA1 = '" & txtBTCoA0.Text & "' , " &
                        "BTCOA2 = '" & txtBTCoA1.Text & "' , " &
                        "BTCOA3 = '" & txtBTCoA2.Text & "' , " &
                        "BTCOA4 = '" & txtBTCoA3.Text & "' , " &
                        "BTCOA5 = '" & txtBTCoA4.Text & "' , " &
                        "BTCOA6 = '" & txtBTCoA5.Text & "' , " &
                        "BTCOA7 = '" & txtBTCoA6.Text & "' , " &
                        "BTCOA8 = '" & txtBTCoA7.Text & "' , " &
                        "BTCOA9 = '" & txtBTCoA8.Text & "' , " &
                        "BTCOA10 = '" & txtBTCoA9.Text & "' , " &
                        "BTCOA11 = '" & txtBTCoA10.Text & "' , " &
                        "BTCOA12 = '" & txtBTCoA11.Text & "' , " &
                        "BTCOA13 = '" & txtBTCoA12.Text & "' , " &
                        "BTCOA14 = '" & txtBTCoA13.Text & "' , " &
                        "H2OFlow = '" & txtH2OFlow.Text & "'"

                If SQL.DataUpdate(UpdateCmd) = 0 Then
                    AddLogEntry("Error updating BrenntagCOA File")
                Else
                    'AddLogEntry("BrenntagCOA File Updated")
                End If
            Catch ex As Exception
                AddLogEntry("BNUpdateRecord: " & ex.Message)
            End Try

            Me.Close()

        Catch ex As Exception
            SysOptions = Nothing
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub optType0_Click(sender As Object, e As System.EventArgs) Handles optType0.Click
        ActiveCom = 0
        lblIP.Visible = False
        txtIPAddress.Visible = False
        lblIP2.Visible = False
        txtIPAddress2.Visible = False
        lblIP3.Visible = False
        txtIPAddress3.Visible = False
        SetType(0)
    End Sub
    Private Sub optType1_Click(sender As Object, e As System.EventArgs) Handles optType1.Click
        ActiveCom = 1
        lblIP.Visible = False
        txtIPAddress.Visible = False
        lblIP2.Visible = False
        txtIPAddress2.Visible = False
        lblIP3.Visible = False
        txtIPAddress3.Visible = False
        SetType(1)
    End Sub
    Private Sub optType2_Click(sender As Object, e As System.EventArgs) Handles optType2.Click
        ActiveCom = 2
        lblIP.Visible = True
        txtIPAddress.Visible = True
        lblIP2.Visible = True
        txtIPAddress2.Visible = True
        lblIP3.Visible = True
        txtIPAddress3.Visible = True
        SetType(2)
    End Sub

    Sub SetType(Index As Integer)
        Try
            cmbDevicePort.SelectedIndex = Port(Index)
            cmbDeviceBaud.SelectedIndex = Baud(Index)
            cmbDeviceParity.SelectedIndex = Parity(Index)
            cmbDeviceDataBits.SelectedIndex = DataBits(Index)
            cmbDeviceStopBits.SelectedIndex = StopBits(Index)

            If Active(Index) = 1 Then
                optActive0.Checked = True
            Else
                optActive1.Checked = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            'Stop
        End Try
        'Stop
    End Sub

    Private Sub cmbDevicePort_Click(sender As Object, e As System.EventArgs) Handles cmbDevicePort.Click
        Port(ActiveCom) = cmbDevicePort.SelectedIndex
    End Sub

    Private Sub cmbDeviceBaud_Click(sender As Object, e As System.EventArgs) Handles cmbDeviceBaud.Click
        Baud(ActiveCom) = cmbDeviceBaud.SelectedIndex
    End Sub

    Private Sub cmbDeviceParity_Click(sender As Object, e As System.EventArgs) Handles cmbDeviceParity.Click
        Parity(ActiveCom) = cmbDeviceParity.SelectedIndex
    End Sub

    Private Sub cmbDeviceDataBits_Click(sender As Object, e As System.EventArgs) Handles cmbDeviceDataBits.Click
        DataBits(ActiveCom) = cmbDeviceDataBits.SelectedIndex
    End Sub

    Private Sub cmbDeviceStopBits_Click(sender As Object, e As System.EventArgs) Handles cmbDeviceStopBits.Click
        StopBits(ActiveCom) = cmbDeviceStopBits.SelectedIndex
    End Sub

    Private Sub cmbDevicePort_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDevicePort.SelectedIndexChanged
        Port(ActiveCom) = cmbDevicePort.SelectedIndex
    End Sub

    Private Sub cmbDeviceBaud_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDeviceBaud.SelectedIndexChanged
        Baud(ActiveCom) = cmbDeviceBaud.SelectedIndex
    End Sub

    Private Sub cmbDeviceParity_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDeviceParity.SelectedIndexChanged
        Parity(ActiveCom) = cmbDeviceParity.SelectedIndex
    End Sub

    Private Sub cmbDeviceDataBits_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDeviceDataBits.SelectedIndexChanged
        DataBits(ActiveCom) = cmbDeviceDataBits.SelectedIndex
    End Sub

    Private Sub cmbDeviceStopBits_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbDeviceStopBits.SelectedIndexChanged
        StopBits(ActiveCom) = cmbDeviceStopBits.SelectedIndex
    End Sub

    Private Sub optActive0_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optActive0.CheckedChanged
        If optActive0.Checked = True Then
            Active(ActiveCom) = 1
        Else
            Active(ActiveCom) = 0
        End If
    End Sub

    Private Sub txtMOGravity_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtMOGravity.TextChanged
        txtMOppg.Text = Format(Val(txtMOGravity.Text) * 8.341, "0.00")
    End Sub

    Private Sub txtBNGravity_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtBNGravity.TextChanged
        txtBNppg.Text = Format(Val(txtBNGravity.Text) * 8.337, "0.00")
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click

        Try
            If MsgBox("Do you wish to copy Report files " & vbCrLf & "from the server to the local machine?", vbYesNo, "Update Files") = vbYes Then
                'Copy Report files from SATCOMAINT to local Reports Path
                'Create the target directory if necessary
                Dim toPath = txtRptPath.Text
                Dim fromPath = "\\" & txtDBPath.Text & "\Satco\Reports\"
                Dim toPathInfo = New DirectoryInfo(toPath)
                If (Not toPathInfo.Exists) Then
                    toPathInfo.Create()
                End If
                Dim fromPathInfo = New DirectoryInfo(fromPath)
                'copy all files 
                For Each file As FileInfo In fromPathInfo.GetFiles()
                    file.CopyTo(Path.Combine(toPath, file.Name), True)
                Next
                AddLogEntry("Transferring files to Report folder")
            Else
                AddLogEntry("Report file update cancelled")
            End If

            If MsgBox("Do you wish to copy Document files " & vbCrLf & "from the server to the local machine?", vbYesNo, "Update Files") = vbYes Then
                'Copy BOL files from SATCOMAINT to local Documents Path
                'Create the target directory if necessary
                Dim toPath = txtDocPath.Text
                Dim fromPath = "\\" & txtDBPath.Text & "\Satco\Documents\"
                Dim toPathInfo = New DirectoryInfo(toPath)
                If (Not toPathInfo.Exists) Then
                    toPathInfo.Create()
                End If
                Dim fromPathInfo = New DirectoryInfo(fromPath)
                'copy all files 
                For Each file As FileInfo In fromPathInfo.GetFiles()
                    file.CopyTo(Path.Combine(toPath, file.Name), True)
                Next
                AddLogEntry("Transferring files to Document folder")
                MsgBox("Files have been updated")
            Else
                AddLogEntry("Document file update cancelled")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

End Class