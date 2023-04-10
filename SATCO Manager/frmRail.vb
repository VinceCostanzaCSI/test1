Public Class frmRail
    Dim ActiveScale As Integer
    Dim TankMaterial As Integer
    Dim MaterialType As Integer

    Private SQL As New SQLControl

    Private Sub frmRail_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim SysOptions As clsSystem

        CenterForm(Me)

        SysOptions = New clsSystem
        ActiveScale = SysOptions.ScaleNumber - 1
        'Stop
        SysOptions = Nothing
        'Clear fields first
        cboCode.Items.Clear()
        cboCommodity.Items.Clear()
        cboTank.Items.Clear()
        cboDriver.Items.Clear()
        txtDescription1.Text = ""
        txtDescription2.Text = ""
        txtDescription3.Text = ""
        txtDescription4.Text = ""
        txtDescription5.Text = ""
        txtDriverID.Text = ""

        ComboFill()

        txtDate.Text = Format(Now, "MM/dd/yyyy")
        txtTime.Text = Format(Now, "HH:mm")
    End Sub

    Private Sub ComboFill()
        If Sql.HasConnection = True Then
            'Commodity
            SQL.RunQuery("SELECT * FROM COMMODITY WHERE Active = '1' ")
            If Sql.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In Sql.SQLDataset.Tables(0).Rows
                    cboCommodity.Items.Add(r("Id"))
                Next
            ElseIf SQL.SQLDataset.HasErrors <> "" Then
                MsgBox(SQL.SQLDataset.HasErrors)
            End If

            'Consignee
            SQL.RunQuery("SELECT * FROM Consignee")
            If SQL.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboCode.Items.Add(r("Code"))
                Next
            ElseIf SQL.SQLDataset.HasErrors <> "" Then
                MsgBox(SQL.SQLDataset.HasErrors)
            End If

            'Driver
            SQL.RunQuery("SELECT * FROM DRIVER WHERE LEFT(Carrier,5) = 'SATCO';")
            If SQL.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboDriver.Items.Add(r("Name"))
                Next
            ElseIf SQL.SQLDataset.HasErrors <> "" Then
                MsgBox(SQL.SQLDataset.HasErrors)
            End If

            'Tank
            SQL.RunQuery("SELECT * FROM Tank")
            If SQL.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboTank.Items.Add(r("Id"))
                Next
            ElseIf SQL.SQLDataset.HasErrors <> "" Then
                MsgBox(SQL.SQLDataset.HasErrors)
            End If

            'Release
            SQL.RunQuery("SELECT * FROM Brenntag where Active = '1'")
            If SQL.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboRelease.Items.Add(r("Release"))
                Next
            ElseIf SQL.SQLDataset.HasErrors <> "" Then
                MsgBox(SQL.SQLDataset.HasErrors)
            End If

        Else
            MsgBox("No SQL Connection")
        End If
    End Sub
   
    Private Sub cboCode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboCode.SelectedIndexChanged
        Dim Consignee As clsConsignee

        'AddLogEntry ("frmTransactionProcessing - cboCode_Change")
        Consignee = New clsConsignee
        Consignee.FindRecord(cboCode.Text)
        txtConsignee.Text = Consignee.Consignee
        txtDestination.Text = Consignee.Destination

        If UCase(Mid(cboCode.Text, 1, 2)) = "SA" Then
            MaterialType = 1
            TankMaterial = 0
        Else
            MaterialType = 2
            TankMaterial = 1
        End If

        Consignee = Nothing
    End Sub

    Private Sub cboCommodity_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboCommodity.SelectedIndexChanged
        Dim Commodity As clsCommodity

        Commodity = New clsCommodity

        Commodity.FindRecord(cboCommodity.Text)
        txtDescription1.Text = Commodity.Description1
        txtDescription2.Text = Commodity.Description2
        txtDescription3.Text = Commodity.Description3
        txtDescription4.Text = Commodity.Description4

        Commodity = Nothing

    End Sub

    Private Sub cboTank_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboTank.SelectedIndexChanged
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
            cboRelease.Visible = True
            lblRelease.Visible = True
        End If

    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub cmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrint.Click
        If cboCode.Text = "" Then
            MsgBox("Please choose Consignee Code")
            Exit Sub
        End If
        If cboCommodity.Text = "" Then
            MsgBox("Please choose Commodity")
            Exit Sub
        End If
        If cboDriver.Text = "" Then
            MsgBox("Please choose Driver")
            Exit Sub
        End If
        If cboTank.Text = "" Then
            MsgBox("Please choose Tank Number")
            Exit Sub
        End If
        If txtCar1.Text = "" Then
            MsgBox("Please enter data for at least one car")
            Exit Sub
        End If
        If txtNet1.Text = "" Then
            MsgBox("Please enter weight for at least one car")
            Exit Sub
        End If
        cmdPrint.Enabled = False
        SavePrint()

    End Sub

    Private Sub SavePrint()

        Dim Transaction As clsTransaction
        Dim Consignee As clsConsignee
        Dim SysOptions As clsSystem
        Dim TransId As Long
        Dim Tank As clsTank
        Dim Rail As clsRail

        Try
            AddLogEntry("Entering frmRail - SavePrint")
            Transaction = New clsTransaction
            Consignee = New clsConsignee
            SysOptions = New clsSystem
            Tank = New clsTank
            Rail = New clsRail

            ' ----- Update Tank Level -----
            Tank.FindRecord(cboTank.Text)
            Tank.CurrentLevel = Format(Tank.CurrentLevel - ((Val(txtTotalNet.Text) / 2000)), "#####.##")
            If Tank.CurrentLevel < 0 Then Tank.CurrentLevel = 0
            Transaction.TankLevel = Tank.CurrentLevel
            AddLogEntry("New Level for Tank " & Tank.Id & "  Current Level " & Tank.CurrentLevel)
            Tank.UpdateRecord(Tank.Id)
            Tank = Nothing

            '------ Save Transaction -----
            Transaction.ScaleNumber = SysOptions.ScaleNumber
            Transaction.Id = Consignee.GetNextTransNumber(cboCode.Text)
            Transaction.TDate = txtDate.Text & " " & Mid(txtTime.Text, 1, 5)
            Transaction.InTime = Mid(txtTime.Text, 1, 5)
            Transaction.OutTime = Mid(txtTime.Text, 1, 5)
            Transaction.DriverId = txtDriverId.Text
            Transaction.Code = cboCode.Text
            Transaction.VehicleID = "" 'txtCarPrefix1.Text
            Transaction.TrailerID = "" 'txtCar1.Text
            Transaction.FillWt = Val(txtTotalNet.Text)
            Transaction.PO = txtPO.Text
            Transaction.Seal1 = "" 'txtSeal1_1.Text
            Transaction.Seal2 = "" 'txtSeal2_1.Text
            Transaction.Seal3 = "" 'txtSeal3_1.Text
            Transaction.Seal4 = "" 'txtSeal4_1.Text
            If UCase(Mid(cboCode.Text, 1, 2)) = "SA" Then
                cboCommodity.Text = "SA"
            End If
            Transaction.Commodity = cboCommodity.Text
            Transaction.Gross = Val(txtTotalNet.Text)
            Transaction.Tare = 0
            Transaction.Net = Val(txtTotalNet.Text)
            Transaction.Adjustment = "0"
            If cboTank.Text = "03" Then
                Transaction.ReleaseNumber = cboRelease.Text
            Else
                Transaction.ReleaseNumber = "RAIL" 'txtReleaseNumber.Text
            End If
            Transaction.TankId = cboTank.Text  'Tank.GetFillTank(ActiveScale, TankMaterial)

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

            'Now add each car to Rail Table
            'Car1
            Rail.Code = Transaction.Code
            Rail.Id = Transaction.Id
            Rail.Number = 1
            Rail.CarPrefix = txtCarPrefix1.Text
            Rail.Car = txtCar1.Text
            Rail.Seal1 = txtSeal1_1.Text
            Rail.Seal2 = txtSeal2_1.Text
            Rail.Seal3 = txtSeal3_1.Text
            Rail.Seal4 = txtSeal4_1.Text
            Rail.NetWt = Val(txtNet1.Text)
            Rail.AddRecord(Transaction.Code, Transaction.Id, 1)

            'Car2
            If txtCar2.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 2
                Rail.CarPrefix = txtCarPrefix2.Text
                Rail.Car = txtCar2.Text
                Rail.Seal1 = txtSeal1_2.Text
                Rail.Seal2 = txtSeal2_2.Text
                Rail.Seal3 = txtSeal3_2.Text
                Rail.Seal4 = txtSeal4_2.Text
                Rail.NetWt = Val(txtNet2.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 2)
            End If

            'Car3
            If txtCar3.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 3
                Rail.CarPrefix = txtCarPrefix3.Text
                Rail.Car = txtCar3.Text
                Rail.Seal1 = txtSeal1_3.Text
                Rail.Seal2 = txtSeal2_3.Text
                Rail.Seal3 = txtSeal3_3.Text
                Rail.Seal4 = txtSeal4_3.Text
                Rail.NetWt = Val(txtNet3.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 3)
            End If

            'Car4
            If txtCar4.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 4
                Rail.CarPrefix = txtCarPrefix4.Text
                Rail.Car = txtCar4.Text
                Rail.Seal1 = txtSeal1_4.Text
                Rail.Seal2 = txtSeal2_4.Text
                Rail.Seal3 = txtSeal3_4.Text
                Rail.Seal4 = txtSeal4_4.Text
                Rail.NetWt = Val(txtNet4.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 4)
            End If

            'Car5
            If txtCar5.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 5
                Rail.CarPrefix = txtCarPrefix5.Text
                Rail.Car = txtCar5.Text
                Rail.Seal1 = txtSeal1_5.Text
                Rail.Seal2 = txtSeal2_5.Text
                Rail.Seal3 = txtSeal3_5.Text
                Rail.Seal4 = txtSeal4_5.Text
                Rail.NetWt = Val(txtNet5.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 5)
            End If

            'Car6
            If txtCar6.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 6
                Rail.CarPrefix = txtCarPrefix6.Text
                Rail.Car = txtCar6.Text
                Rail.Seal1 = txtSeal1_6.Text
                Rail.Seal2 = txtSeal2_6.Text
                Rail.Seal3 = txtSeal3_6.Text
                Rail.Seal4 = txtSeal4_6.Text
                Rail.NetWt = Val(txtNet6.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 6)
            End If

            'Car7
            If txtCar7.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 7
                Rail.CarPrefix = txtCarPrefix7.Text
                Rail.Car = txtCar7.Text
                Rail.Seal1 = txtSeal1_7.Text
                Rail.Seal2 = txtSeal2_7.Text
                Rail.Seal3 = txtSeal3_7.Text
                Rail.Seal4 = txtSeal4_7.Text
                Rail.NetWt = Val(txtNet7.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 7)
            End If

            'Car8
            If txtCar8.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 8
                Rail.CarPrefix = txtCarPrefix8.Text
                Rail.Car = txtCar8.Text
                Rail.Seal1 = txtSeal1_8.Text
                Rail.Seal2 = txtSeal2_8.Text
                Rail.Seal3 = txtSeal3_8.Text
                Rail.Seal4 = txtSeal4_8.Text
                Rail.NetWt = Val(txtNet8.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 8)
            End If

            'Car9
            If txtCar9.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 9
                Rail.CarPrefix = txtCarPrefix9.Text
                Rail.Car = txtCar9.Text
                Rail.Seal1 = txtSeal1_9.Text
                Rail.Seal2 = txtSeal2_9.Text
                Rail.Seal3 = txtSeal3_9.Text
                Rail.Seal4 = txtSeal4_9.Text
                Rail.NetWt = Val(txtNet9.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 9)
            End If

            'Car10
            If txtCar10.Text <> "" Then
                Rail.Code = Transaction.Code
                Rail.Id = Transaction.Id
                Rail.Number = 10
                Rail.CarPrefix = txtCarPrefix10.Text
                Rail.Car = txtCar10.Text
                Rail.Seal1 = txtSeal1_10.Text
                Rail.Seal2 = txtSeal2_10.Text
                Rail.Seal3 = txtSeal3_10.Text
                Rail.Seal4 = txtSeal4_10.Text
                Rail.NetWt = Val(txtNet10.Text)
                Rail.AddRecord(Transaction.Code, Transaction.Id, 10)
            End If

            NewPrinterID = Transaction.ID
            NewPrinterCode = Transaction.Code
            'Set Tank = Nothing
            SysOptions = Nothing
            Consignee = Nothing
            Transaction = Nothing
            Rail = Nothing

            'Now Print Ticket
            If MsgBox("Do you wish to Print Ticket ?", vbYesNo, "Print Ticket") = vbYes Then
                PrintRailTicket()
            End If
            'Clear out fields and enable Complete/Print Button
            txtCarPrefix1.Text = ""
            txtCarPrefix2.Text = ""
            txtCarPrefix3.Text = ""
            txtCarPrefix4.Text = ""
            txtCarPrefix5.Text = ""
            txtCarPrefix6.Text = ""
            txtCarPrefix7.Text = ""
            txtCarPrefix8.Text = ""
            txtCarPrefix9.Text = ""
            txtCarPrefix10.Text = ""
            txtCar1.Text = ""
            txtCar2.Text = ""
            txtCar3.Text = ""
            txtCar4.Text = ""
            txtCar5.Text = ""
            txtCar6.Text = ""
            txtCar7.Text = ""
            txtCar8.Text = ""
            txtCar9.Text = ""
            txtCar10.Text = ""
            txtNet1.Text = ""
            txtNet2.Text = ""
            txtNet3.Text = ""
            txtNet4.Text = ""
            txtNet5.Text = ""
            txtNet6.Text = ""
            txtNet7.Text = ""
            txtNet8.Text = ""
            txtNet9.Text = ""
            txtNet10.Text = ""
            txtTons1.Text = ""
            txtTons2.Text = ""
            txtTons3.Text = ""
            txtTons4.Text = ""
            txtTons5.Text = ""
            txtTons6.Text = ""
            txtTons7.Text = ""
            txtTons8.Text = ""
            txtTons9.Text = ""
            txtTons10.Text = ""

            cboCode.Text = ""
            cboCommodity.Text = ""
            cboTank.Text = ""
            cboDriver.Text = ""
            cboRelease.Text = ""
            lblRelease.Visible = False
            cboRelease.Visible = False
            txtTotalNet.Text = ""
            txtTotalTons.Text = ""
            cmdPrint.Enabled = True
            txtConsignee.Text = ""
            txtDescription1.Text = ""
            txtDescription2.Text = ""
            txtDescription3.Text = ""
            txtDescription4.Text = ""
            txtDescription5.Text = ""
            txtDestination.Text = ""
            txtDriverID.Text = ""
            txtPO.Text = ""
            txtSeal1_1.Text = ""
            txtSeal2_1.Text = ""
            txtSeal3_1.Text = ""
            txtSeal4_1.Text = ""
            txtSeal1_2.Text = ""
            txtSeal2_2.Text = ""
            txtSeal3_2.Text = ""
            txtSeal4_2.Text = ""
            txtSeal1_3.Text = ""
            txtSeal2_3.Text = ""
            txtSeal3_3.Text = ""
            txtSeal4_3.Text = ""
            txtSeal1_4.Text = ""
            txtSeal2_4.Text = ""
            txtSeal3_4.Text = ""
            txtSeal4_4.Text = ""
            txtSeal1_5.Text = ""
            txtSeal2_5.Text = ""
            txtSeal3_5.Text = ""
            txtSeal4_5.Text = ""
            txtSeal1_6.Text = ""
            txtSeal2_6.Text = ""
            txtSeal3_6.Text = ""
            txtSeal4_6.Text = ""
            txtSeal1_7.Text = ""
            txtSeal2_7.Text = ""
            txtSeal3_7.Text = ""
            txtSeal4_7.Text = ""
            txtSeal1_8.Text = ""
            txtSeal2_8.Text = ""
            txtSeal3_8.Text = ""
            txtSeal4_8.Text = ""
            txtSeal1_9.Text = ""
            txtSeal2_9.Text = ""
            txtSeal3_9.Text = ""
            txtSeal4_9.Text = ""
            txtSeal1_10.Text = ""
            txtSeal2_10.Text = ""
            txtSeal3_10.Text = ""
            txtSeal4_10.Text = ""

        Catch ex As Exception
            AddLogEntry("frmRail-SavePrint: " & ex.Message)
        End Try
    End Sub

    Private Sub cboDriver_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboDriver.SelectedIndexChanged
        Dim Driver As clsDriver
        Driver = New clsDriver

        Driver.FindName(cboDriver.Text)
        txtDriverID.Text = Driver.ID
        'Stop
        Driver = Nothing
    End Sub

    Private Sub txtNet1_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtNet1.TextChanged
        txtTons1.Text = Format(Val(txtNet1.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        txtTime.Text = Format(Now, "HH:mm")
    End Sub

    Private Sub txtNet2_TextChanged(sender As Object, e As EventArgs) Handles txtNet2.TextChanged
        txtTons2.Text = Format(Val(txtNet2.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub txtNet3_TextChanged(sender As Object, e As EventArgs) Handles txtNet3.TextChanged
        txtTons3.Text = Format(Val(txtNet3.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub txtNet4_TextChanged(sender As Object, e As EventArgs) Handles txtNet4.TextChanged
        txtTons4.Text = Format(Val(txtNet4.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub txtNet5_TextChanged(sender As Object, e As EventArgs) Handles txtNet5.TextChanged
        txtTons5.Text = Format(Val(txtNet5.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub txtNet6_TextChanged(sender As Object, e As EventArgs) Handles txtNet6.TextChanged
        txtTons6.Text = Format(Val(txtNet6.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub txtNet7_TextChanged(sender As Object, e As EventArgs) Handles txtNet7.TextChanged
        txtTons7.Text = Format(Val(txtNet7.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub txtNet8_TextChanged(sender As Object, e As EventArgs) Handles txtNet8.TextChanged
        txtTons8.Text = Format(Val(txtNet8.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub txtNet9_TextChanged(sender As Object, e As EventArgs) Handles txtNet9.TextChanged
        txtTons9.Text = Format(Val(txtNet9.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub

    Private Sub txtNet10_TextChanged(sender As Object, e As EventArgs) Handles txtNet10.TextChanged
        txtTons10.Text = Format(Val(txtNet10.Text) / 2000, "####.##")
        txtTotalNet.Text = Val(txtNet1.Text) + Val(txtNet2.Text) + Val(txtNet3.Text) + Val(txtNet4.Text) + Val(txtNet5.Text)
        txtTotalNet.Text = txtTotalNet.Text + Val(txtNet6.Text) + Val(txtNet7.Text) + Val(txtNet8.Text) + Val(txtNet9.Text) + Val(txtNet10.Text)
        txtTotalTons.Text = Format(Val(txtTotalNet.Text) / 2000, "####.##")
    End Sub
End Class