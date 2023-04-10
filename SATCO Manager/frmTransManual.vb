Public Class frmTransManual
    Dim ActiveScale As Integer
    Dim TankMaterial As Integer
    Dim MaterialType As Integer
    Dim GrossWt As Long
    Dim TareWt As Long
    Dim cmdCancelFlag As Integer
    Dim cmdContinueFlag As Integer
    Dim cmdPrintFlag As Integer
    Dim TareSet As Integer
    Dim CurrentTarget As Long
    Dim DriverReady As Boolean
    Dim PauseMode As Boolean
    Dim TransactionComplete As Boolean
    Dim PrintButtonPressed As Boolean

    Private SQL As New SQLControl

    Private Sub frmTransManual_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim SysOptions As clsSystem

        CenterForm(Me)

        SysOptions = New clsSystem
        ActiveScale = SysOptions.ScaleNumber - 1
        'Stop
        SysOptions = Nothing

        ClearFields()

        ComboFill()
        

        txtDate.Text = Format(Now, "MM/dd/yyyy")

        txtDate.Text = Format(Now, "MM/dd/yyyy")
        txtTime.Text = Format(Now, "HH:mm")

    End Sub

    Private Sub ClearFields()
        ''Clear all textboxes
        'Dim a As Control
        'For Each a In Me.Controls
        '    If TypeOf a Is TextBox Then
        '        a.Text = ""
        '    End If
        'Next

        txtCardId.Text = ""
        txtCarrier.Text = ""
        txtConsignee.Text = ""
        txtDriverId.Text = ""
        txtDriverName.Text = ""
        txtDescription1.Text = ""
        txtDescription2.Text = ""
        txtDescription3.Text = ""
        txtDescription4.Text = ""
        txtDescription5.Text = ""
        txtDestination.Text = ""
        txtReleaseNumber.Text = ""
        txtSeal1.Text = ""
        txtSeal2.Text = ""

        txtVehicle.Text = ""
        txtTrailer.Text = ""
        GrossBox.Text = ""
        TareBox.Text = ""
        NetBox.Text = ""

    End Sub

    Private Sub ComboFill()
        If Sql.HasConnection = True Then
            'Commodity
            cboCommodity.Items.Clear()
            SQL.RunQuery("SELECT * FROM COMMODITY WHERE Active = '1' ")
            If Sql.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In Sql.SQLDataset.Tables(0).Rows
                    cboCommodity.Items.Add(r("Id"))
                Next
            ElseIf SQL.SQLDataset.HasErrors <> "" Then
                MsgBox(SQL.SQLDataset.HasErrors)
            End If

            'Consignee
            cboCode.Items.Clear()
            SQL.RunQuery("SELECT * FROM Consignee")
            If SQL.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboCode.Items.Add(r("Code"))
                Next
            ElseIf SQL.SQLDataset.HasErrors <> "" Then
                MsgBox(SQL.SQLDataset.HasErrors)
            End If

            'Driver
            'SQL.RunQuery("SELECT * FROM DRIVER WHERE LEFT(Carrier,5) = 'SATCO';")
            'If SQL.SQLDataset.Tables.Count > 0 Then
            '    For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
            '        cboDriver.Items.Add(r("Name"))
            '    Next
            'ElseIf SQL.SQLDataset.HasErrors <> "" Then
            '    MsgBox(SQL.SQLDataset.HasErrors)
            'End If

            'Tank
            cboTank.Items.Clear()
            SQL.RunQuery("SELECT * FROM Tank")
            If SQL.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboTank.Items.Add(r("Id"))
                Next
            ElseIf SQL.SQLDataset.HasErrors <> "" Then
                MsgBox(SQL.SQLDataset.HasErrors)
            End If

            'Release
            'SQL.RunQuery("SELECT * FROM Brenntag where Active = '1'")
            'If SQL.SQLDataset.Tables.Count > 0 Then
            '    For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
            '        cboRelease.Items.Add(r("Release"))
            '    Next
            'ElseIf SQL.SQLDataset.HasErrors <> "" Then
            '    MsgBox(SQL.SQLDataset.HasErrors)
            'End If

        Else
            MsgBox("No SQL Connection")
        End If
    End Sub

    Private Sub cboCode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim Consignee As clsConsignee

        'AddLogEntry ("frmTransactionProcessing - cboCode_Change")
        Consignee = New clsConsignee
        Consignee.FindRecord(cboCode.Text)
        txtConsignee.Text = Consignee.Consignee
        txtDestination.Text = Consignee.Destination

        If UCase$(Mid(cboCode.Text, 1, 2)) = "SA" Then
            MaterialType = 1
            TankMaterial = 0
        Else
            MaterialType = 2
            TankMaterial = 1
        End If

        Consignee = Nothing
    End Sub

    Private Sub cboCommodity_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim Commodity As clsCommodity

        'AddLogEntry ("frmTransactionProcessing - cboCode_Change")
        Commodity = New clsCommodity

        Commodity.FindRecord(cboCommodity.Text)
        txtDescription1.Text = Commodity.Description1
        txtDescription2.Text = Commodity.Description2
        txtDescription3.Text = Commodity.Description3
        txtDescription4.Text = Commodity.Description4

        Commodity = Nothing

    End Sub

    Private Sub cboTank_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        Dim Commodity As clsCommodity
        Dim Tank As clsTank

        Commodity = New clsCommodity
        Tank = New clsTank

        Tank.FindRecord(cboTank.Text)
        'Now get Commodity info related to tank info
        Commodity.FindRecord(Tank.Commodity)
        txtDescription2.Text = Commodity.Description2
        txtDescription3.Text = Commodity.Description3
        txtDescription4.Text = Commodity.Description4
        txtDescription5.Text = Commodity.Description5
        Commodity = Nothing
        Tank = Nothing
        If cboTank.Text = "03" Then
            'cboRelease.Visible = True
            'lblRelease.Visible = True
        End If

    End Sub

    Private Sub cmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrint.Click
        If cboCode.Text = "" Then
            MsgBox("Please choose Consignee/UC Code")
            Exit Sub
        End If
        If cboCommodity.Text = "" Then
            MsgBox("Please choose Commodity")
            Exit Sub
        End If
        'If cboDriver.Text = "" Then
        '    MsgBox("Please choose Driver")
        '    Exit Sub
        'End If
        If cboTank.Text = "" Then
            MsgBox("Please choose Tank Number")
            Exit Sub
        End If

        cmdPrint.Enabled = False
        SavePrint()

        ClearFields()

        cmdPrint.Enabled = True
        txtCardId.Focus()

    End Sub

    Private Sub SavePrint()

        Dim Transaction As clsTransaction
        Dim Consignee As clsConsignee
        Dim SysOptions As clsSystem
        'Dim Ticket As clsTicket
        Dim TransId As Long
        Dim Tank As clsTank
        Dim Mosaic As clsMosaic
        Dim Brenntag As clsBrenntag
        Dim MosaicProduct As String = ""

        Try
            AddLogEntry("frmTransManual - SavePrint")
            Transaction = New clsTransaction
            Consignee = New clsConsignee
            SysOptions = New clsSystem
            Tank = New clsTank

            ' ----- Get Tank Level -----
            Tank.FindRecord(cboTank.Text)
            'AddLogEntry ("Updating Tank Level for Tank " & Tank.ID & "  Current Level " & Tank.CurrentLevel)
            Tank.CurrentLevel = Format(Tank.CurrentLevel - ((Val(NetBox.Text) / 2000)), "#####.##")
            If Tank.CurrentLevel < 0 Then Tank.CurrentLevel = 0
            AddLogEntry("New Level for Tank " & Tank.Id & "  Current Level " & Tank.CurrentLevel)
            Transaction.TankLevel = Tank.CurrentLevel

            ' ----- Add Transaction Record ----
            Transaction.ScaleNumber = SysOptions.ScaleNumber
            Transaction.Id = Consignee.GetNextTransNumber(cboCode.Text)
            Dim dtTime As DateTime = Convert.ToDateTime(txtTime.Text)

            Transaction.TDate = txtDate.Text & " " & Mid(txtTime.Text, 1, 5)
            Transaction.InTime = Mid(txtTime.Text, 1, 5)
            'If txtOutTime.Text = "" Then txtOutTime.Text = txtInTime.Text
            Transaction.OutTime = Mid(txtTime.Text, 1, 5)
            Transaction.DriverId = txtDriverId.Text
            Transaction.Code = cboCode.Text
            Transaction.VehicleID = txtVehicle.Text
            Transaction.TrailerID = txtTrailer.Text
            Transaction.FillWt = Val(GrossBox.Text)  'Val(txtTargetWt.Text)
            If Mid(cboCommodity.Text, 1, 2) = "SA" Then
                Transaction.Commodity = "SA"
            Else
                Transaction.Commodity = cboCommodity.Text
            End If
            Transaction.Gross = Val(GrossBox.Text)
            Transaction.Tare = Val(TareBox.Text)
            Transaction.Net = Val(GrossBox.Text) - Val(TareBox.Text)
            Transaction.ReleaseNumber = txtReleaseNumber.Text
            Transaction.TankId = Tank.Id
            Transaction.Seal1 = txtSeal1.Text & ""
            Transaction.Seal2 = txtSeal2.Text & ""
            Transaction.Adjustment = "0"

            If Mid(cboCode.Text, 1, 2) = "SA" Then
                Transaction.Analysis = txtDescription2.Text
            End If
            If Val(cboCode.Text) >= 1 Or Mid(cboCode.Text, 1, 2) = "UC" Then
                Transaction.Analysis = txtDescription3.Text
            End If
            If Mid(cboCode.Text, 1, 2) = "MO" Then
                Transaction.Analysis = "32%"
            End If
            If Mid(cboCode.Text, 1, 2) = "BN" Then
                Transaction.Analysis = "50%"
            End If

            TransId = Transaction.AddRecord
            AddLogEntry("Transaction Added " & Transaction.Id)
            ' ----- Transaction Record added ----

            ' ----- Update Consignee Record -----
            Consignee.FindRecord(cboCode.Text)
            AddLogEntry("Updating amount used for Consignee " & Consignee.Id)
            Consignee.Used = Consignee.Used + ((Val(GrossBox.Text) / 2000) - (Val(TareBox.Text) / 2000))
            Consignee.UpdateRecord(cboCode.Text)
            Consignee = Nothing

            ' ----- Update Tank Record -----
            AddLogEntry("Now updating Tank" & Tank.Id)
            Tank.UpdateRecord(Tank.Id)
            AddLogEntry("Tank updated successfully")
            Tank = Nothing
            '  --- Tank Updated ----

            NewPrinterID = Transaction.Id
            NewPrinterCode = Transaction.Code

            Transaction = Nothing

            'Ticket Print Area
            PrintLastTicket()

            PrintExcel("Q074 Hazmat.xls")

            'Brenntag check
            Brenntag = New clsBrenntag
            If BrenntagDriver = True Then
                If Len(Brenntag.Notes) > 3 Then
                    PrintBNNotes("Brenntag Notes.doc")
                End If
                PrintBNCoA50("Brenntag CoA 50.doc")
            End If

            Mosaic = Nothing
            Brenntag = Nothing
            SysOptions = Nothing

        Catch ex As Exception
            AddLogEntry("frmTransManual-SavePrint: " & ex.Message)
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtCardId_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCardId.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.Chr(13) Then
            cboCode.Focus()
        End If
    End Sub

    Private Sub txtCardId_LostFocus(sender As Object, e As System.EventArgs) Handles txtCardId.LostFocus
        Dim Driver As clsDriver

        Driver = New clsDriver
        Driver.SearchData = txtCardId.Text
        If Len(Driver.SearchData) > 0 Then
            'Stop
            If Driver.Search("CardId", Driver.SearchData) = True Then
                Driver.FindRecord(Driver.ID)
                txtDriverName.Text = Driver.Name
                txtCarrier.Text = Driver.Carrier
                txtDriverId.Text = Driver.ID
                txtDriverName.Refresh()
                txtCarrier.Refresh()
            Else
                MsgBox("Record not found !", vbOKOnly, "Search Error")
            End If
        End If
        'Stop
        Driver = Nothing
        txtDriverId.Text = UCase(txtDriverId.Text)
    End Sub

    'Private Sub txtCardId_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCardId.TextChanged
    '    Dim Driver As clsDriver

    '    'AddLogEntry ("frmCardId - Next button hit")
    '    If Len(txtCardId.Text) > 0 Then
    '        AddLogEntry("Validating CardId")
    '        Driver = New clsDriver

    '        Driver.Search("CardId", txtCardId.Text)
    '        If Driver.EOF Then
    '            AddLogEntry("Invalid CardId found")
    '            lblMsg.Text = "INVALID CARD ID ENTERED !"
    '        Else
    '            AddLogEntry("Valid CardId found")
    '            Dim TDate As Date = Format(Driver.Twic, "MM/dd/yyyy")
    '            If TDate < Now Then
    '                AddLogEntry("Driver Twic is expired")
    '                lblMsg.Text = "DRIVER TWIC IS EXPIRED !"
    '                'Stop
    '                Driver = Nothing
    '                Exit Sub
    '            End If
    '            If Driver.Active = False Then
    '                AddLogEntry("Driver is INACTIVE")
    '                lblMsg.Text = "DRIVER IS INACTIVE !"
    '            Else
    '                AddLogEntry("Driver is ACTIVE")
    '                'Process = New clsProcess
    '                'Process.ProcessDate = Format(Now, "MM/dd/yyyy")
    '                'Process.ProcessTime = Format(Now, "hh:mm")
    '                'Process.CardId = txtCardId.Text
    '                'Process.DriverId = Driver.ID
    '                'Process.DriverName = Driver.Name
    '                'Process.Carrier = Driver.Carrier
    '                'Process = Nothing
    '                'comReader.RThreshold = 0
    '                'On Error Resume Next
    '                'comReader.Close()
    '                'AddLogEntry ("Unload frmCardId")
    '                'Me.Close()
    '                'AddLogEntry ("frmCardId loading frmPIN")
    '                'frmPin.Show()
    '            End If
    '        End If
    '        Driver = Nothing
    '    Else
    '        AddLogEntry("No card Id was entered")
    '        lblMsg.Text = "NO CARD ID ENTERED !"
    '    End If
    'End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        cboCommodity.Text = Nothing
        cboCode.Text = Nothing
        cboTank.Text = Nothing
        Me.Dispose()
    End Sub

    Private Sub txtDriverId_LostFocus(sender As Object, e As System.EventArgs) Handles txtDriverId.LostFocus
        Dim Driver As clsDriver

        Driver = New clsDriver
        Driver.SearchData = txtDriverId.Text
        If Len(Driver.SearchData) > 0 Then
            If Driver.Search("ID", Driver.SearchData) = True Then
                Driver.FindRecord(Driver.ID)
                txtDriverName.Text = Driver.Name
                txtCarrier.Text = Driver.Carrier
                txtCardId.Text = Driver.CardNumber
                txtDriverName.Refresh()
                txtCarrier.Refresh()
            Else
                MsgBox("Record not found !", vbOKOnly, "Search Error")
            End If
        End If
        'Stop
        Driver = Nothing
        txtDriverId.Text = UCase(txtDriverId.Text)
    End Sub

    Private Sub txtDriverId_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtDriverId.TextChanged

    End Sub

    Private Sub GrossBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles GrossBox.TextChanged
        NetBox.Text = Val(GrossBox.Text) - Val(TareBox.Text)
        TonBox.text = Val(NetBox.Text) / 2000
    End Sub

    Private Sub cboCommodity_SelectedIndexChanged_1(sender As System.Object, e As System.EventArgs) Handles cboCommodity.SelectedIndexChanged
        Dim Commodity As clsCommodity

        'AddLogEntry ("frmTransactionProcessing - cboCode_Change")
        Commodity = New clsCommodity

        Commodity.FindRecord(cboCommodity.Text)
        txtDescription1.Text = Commodity.Description1

        Commodity = Nothing
    End Sub

    Private Sub cboCode_SelectedIndexChanged_1(sender As System.Object, e As System.EventArgs) Handles cboCode.SelectedIndexChanged
        Dim Consignee As clsConsignee

        'AddLogEntry ("frmTransactionProcessing - cboConsignee_Change")
        Consignee = New clsConsignee
        Consignee.FindRecord(cboCode.Text)
        txtConsignee.Text = Consignee.Consignee
        txtDestination.Text = Consignee.Destination

        If UCase(Mid(cboCode.Text, 1, 2)) = "SA" Then
            MaterialType = 1
            TankMaterial = 0
        End If
        If UCase(Mid(cboCode.Text, 1, 2)) = "UC" Then
            MaterialType = 2
            TankMaterial = 1
        End If
        If UCase(Mid(cboCode.Text, 1, 2)) = "MO" Then
            MaterialType = 2
            TankMaterial = 2
        End If

        Consignee = Nothing
    End Sub

    Private Sub cboTank_SelectedIndexChanged_1(sender As System.Object, e As System.EventArgs) Handles cboTank.SelectedIndexChanged
        Dim Commodity As clsCommodity
        Dim Tank As clsTank

        Commodity = New clsCommodity
        Tank = New clsTank

        Tank.FindRecord(cboTank.Text)
        'Now get Commodity info related to tank info
        Commodity.FindRecord(Tank.Commodity)
        txtDescription2.Text = Commodity.Description2
        txtDescription3.Text = Commodity.Description3
        txtDescription4.Text = Commodity.Description4
        txtDescription5.Text = Commodity.Description5
        Commodity = Nothing
        Tank = Nothing
    End Sub

    Private Sub TareBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles TareBox.TextChanged
        NetBox.Text = Val(GrossBox.Text) - Val(TareBox.Text)
        TonBox.Text = Val(NetBox.Text) / 2000
    End Sub

    Private Sub txtCardId_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCardId.TextChanged

    End Sub
End Class