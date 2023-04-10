Public Class frmTransactionCorrection
    Private SQL As New SQLControl

    Dim PreID As String
    Dim PreDriverId As String
    Dim PreTDate As String
    Dim PreInTime As String
    Dim PreOutTime As String
    Dim PreCode As String
    Dim PreTrailerId As String
    Dim PreVehicleId As String
    Dim PreTankId As String
    Dim PreScaleNumber As String
    Dim PreTargetFillWt As String
    Dim PreCommodityId As String
    Dim PreGrossWt As String
    Dim PreTareWt As String
    Dim PreNetWt As String
    Dim PreReleaseNumber As String
    Dim PreScaleTicket As String
    Dim PreTankLevel As String
    Dim PreAnalysis As String
    Dim PreSeal1 As String
    Dim PreSeal2 As String
    Dim PreSeal3 As String
    Dim PreSeal4 As String
    Dim PreAdjustment As String

    Dim WeightChanged As Boolean
    Dim TankChanged As Boolean
    Dim Tank As clsTank

    Private Sub frmTransactionCorrection_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        LoadTimer.Enabled = True
    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        LoadTimer.Enabled = False
        cmdSave.Visible = TransModify
        cboCommodity.Items.Clear()
        cboTank.Items.Clear()
        ComboFill()
        cboCode.Text = SelectedCode
        txtID.Text = SelectedID
        FindTransaction(SelectedCode, SelectedID)
        Status.Text = ""
        WeightChanged = False
        TankChanged = False

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
            SQL.RunQuery("SELECT * FROM DRIVER")
            If SQL.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                    cboDriverId.Items.Add(r("ID"))
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

    Private Sub FindTransaction(ByVal Code As String, ByVal ID As String)

        'Try
        SQL.RunQuery("Select * from Trans where Code = '" & Code & "' and Id = '" & ID & "'")
        If SQL.RecordCount = 0 Then
            MsgBox("Transaction not found")
        Else
            'Fill in the blanks
            With SQL.SQLDataset.Tables(0).Rows(0)
                txtDate.Text = .Item("TDate") & ""
                cboDriverId.Text = .Item("DriverId") & ""
                txtVehicle.Text = .Item("VehicleId") & ""
                txtTrailer.Text = .Item("TrailerId") & ""
                txtScaleNumber.Text = .Item("ScaleNumber") & ""
                cboTank.Text = .Item("TankId") & ""
                cboCommodity.Text = .Item("CommodityId") & ""
                txtTargetWt.Text = .Item("TargetFillWt") & ""
                GrossBox.Text = .Item("GrossWt") & ""
                TareBox.Text = .Item("TareWt") & ""
                NetBox.Text = .Item("NetWt") & ""
                txtReleaseNumber.Text = .Item("ReleaseNumber") & ""
                txtScaleTicket.Text = .Item("ScaleTicket") & ""
                txtInTime.Text = .Item("InTime") & ""
                txtOutTime.Text = .Item("OutTime") & ""
                txtTankLevel.Text = .Item("TankLevel") & ""
                txtAnalysis.Text = .Item("Analysis") & ""
                txtSeal1.Text = .Item("Seal1") & ""
                txtSeal2.Text = .Item("Seal2") & ""
                txtSeal3.Text = .Item("Seal3") & ""
                txtSeal4.Text = .Item("Seal4") & ""
                txtAdjustment.Text = .Item("Adjustment") & ""

                'TonBox.Text = Val(.Item("NetWt")) / 2000
            End With

            SelectedTank = cboTank.Text
            txtTank.Text = cboTank.Text
            SelectedCode = Code
            SelectedID = ID
            lblInfoSA.Text = ""
            lblTankInfo.Text = ""
            Status.Text = "Selected transaction found"

            'Save PRE information for comparing later
            PreID = txtID.Text
            PreDriverId = cboDriverId.Text
            PreTDate = txtDate.Text
            PreInTime = txtInTime.Text
            PreOutTime = txtOutTime.Text
            PreCode = cboCode.Text
            PreTrailerId = txtTrailer.Text
            PreVehicleId = txtVehicle.Text
            PreTankId = cboTank.Text
            PreScaleNumber = txtScaleNumber.Text
            PreTargetFillWt = txtTargetWt.Text
            PreCommodityId = cboCommodity.Text
            PreGrossWt = GrossBox.Text
            PreTareWt = TareBox.Text
            PreNetWt = NetBox.Text
            PreReleaseNumber = txtReleaseNumber.Text
            PreScaleTicket = txtScaleTicket.Text
            PreTankLevel = txtTankLevel.Text
            PreAnalysis = txtAnalysis.Text
            PreSeal1 = txtSeal1.Text
            PreSeal2 = txtSeal2.Text
            PreSeal3 = txtSeal3.Text
            PreSeal4 = txtSeal4.Text
            PreAdjustment = txtAdjustment.Text
        End If

        'Catch ex As Exception
        '    MsgBox("FindTransaction: " & ex.Message)
        'End Try

    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        cboCommodity.Text = ""
        Me.Dispose()
    End Sub

    Private Sub cboDriverId_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboDriverId.SelectedIndexChanged

    End Sub

    Private Sub cboDriverId_TextChanged(sender As Object, e As System.EventArgs) Handles cboDriverId.TextChanged
        Try
            SQL.RunQuery("Select * from driver where Id = '" & cboDriverId.Text & "'")
            If SQL.RecordCount = 0 Then
                Status.Text = "Driver ID Not Found"
            Else
                txtDriverName.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("Name")
                txtCardId.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("CardId")
                txtCarrier.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("Carrier")
                Status.Text = "Driver ID Found"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboCode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboCode.SelectedIndexChanged
        Try
            SQL.RunQuery("Select * from Consignee where Code = '" & cboCode.Text & "'")
            If SQL.RecordCount = 0 Then
                Status.Text = "Consignee Code Not Found"
            Else
                txtConsignee.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("Consignee")
                txtDestination.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("Destination")
                txtUsed.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("Used")
                'Get last transaction number and add 1 to it
                txtID.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("NextTransNumber")
                Status.Text = "Pulling up Next ID for this Consignee"
                lblInfoSA.Text = "Consignee info has changed"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click

        Dim UpdateCmd As String
        Dim NextNum As Long
        Dim Tons As Double = Val(NetBox.Text) / 2000
        Dim UsedUpdate As Double

        Try
            'If Consignee code was changed then remove old transaction and save new one with new Code and ID (NextTransNumber)
            If SelectedCode <> cboCode.Text Then
                SQL.RunQuery("UPDATE Trans SET ReleaseNumber = 'VOID' WHERE Id = '" & SelectedID & "' and Code = '" & SelectedCode & "';")

                'Now Add new transaction
                Dim strInsert As String = "INSERT INTO Trans (Code, Id, TDate, DriverId, VehicleId, TrailerId, ScaleNumber, TankId, CommodityId, TargetFillWt," &
                                       " GrossWt, TareWt, NetWt, ReleaseNumber, ScaleTicket, InTime, OutTime, TankLevel, Analysis, Seal1, Seal2, Seal3, Seal4, Adjustment) VALUES (" &
                                         "'" & cboCode.Text & "'," &
                                         "'" & txtID.Text & "'," &
                                         "'" & txtDate.Text & "'," &
                                         "'" & cboDriverId.Text & "'," &
                                         "'" & txtVehicle.Text & "'," &
                                         "'" & txtTrailer.Text & "'," &
                                         "'" & txtScaleNumber.Text & "'," &
                                         "'" & cboTank.Text & "'," &
                                         "'" & cboCommodity.Text & "'," &
                                         "'" & txtTargetWt.Text & "'," &
                                         "'" & GrossBox.Text & "'," &
                                         "'" & TareBox.Text & "'," &
                                         "'" & NetBox.Text & "'," &
                                         "'" & txtReleaseNumber.Text & "'," &
                                         "'" & txtScaleTicket.Text & "'," &
                                         "'" & txtInTime.Text & "'," &
                                         "'" & txtOutTime.Text & "'," &
                                         "'" & txtTankLevel.Text & "'," &
                                         "'" & txtAnalysis.Text & "'," &
                                         "'" & txtSeal1.Text & "'," &
                                         "'" & txtSeal2.Text & "'," &
                                         "'" & txtSeal3.Text & "'," &
                                         "'" & txtSeal4.Text & "'," &
                                         "'" & txtAdjustment.Text & "') "

                SQL.DataUpdate(strInsert)

                'Update Consignee info
                'Save nextTrans number and ADD Used amount
                NextNum = txtID.Text + 1
                UsedUpdate = Val(txtUsed.Text) + Tons
                SQL.RunQuery("UPDATE Consignee SET NextTransNumber = '" & NextNum & "', Used = '" & UsedUpdate & "' WHERE Code = '" & cboCode.Text & "';")

                'Removed Used from old Consignee
                SQL.RunQuery("Select * from Consignee where Code = '" & SelectedCode & "'")
                If SQL.RecordCount = 0 Then
                    Status.Text = "Consignee Code Not Found"
                Else
                    txtUsed.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("Used")
                    UsedUpdate = Val(txtUsed.Text) - Tons
                    SQL.RunQuery("UPDATE Consignee SET Used = '" & UsedUpdate & "' WHERE Code = '" & SelectedCode & "';")
                End If

            Else

                'If Tank was changed then get old Tank total and reduce NetWt get new tank and add NetWt
                Dim TankLevel As Double
                If TankChanged = True Then
                    Status.Text = "Tank information will be updated"
                    ' ----- Update New Tank - Subtract tons----
                    SQL.RunQuery("Select * from Tank where Id = '" & cboTank.Text & "';")
                    If SQL.RecordCount = 0 Then
                        MsgBox("Tank not found")
                    Else
                        TankLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("CurrentLevel") & ""
                        TankLevel = TankLevel - ((Val(NetBox.Text) / 2000))
                        txtTankLevel.Text = TankLevel
                        UpdateCmd = "Update Tank Set CurrentLevel = '" & TankLevel & "' Where Id = '" & cboTank.Text & "';"

                        If SQL.DataUpdate(UpdateCmd) = 0 Then
                            MsgBox("Error updating Tank Record")
                        End If
                    End If

                    ' ----- Update Original Tank - Add Tons back ----
                    SQL.RunQuery("Select * from Tank where Id = '" & txtTank.Text & "';")
                    If SQL.RecordCount = 0 Then
                        MsgBox("Tank not found")
                    Else
                        TankLevel = SQL.SQLDataset.Tables(0).Rows(0).Item("CurrentLevel") & ""
                        TankLevel = TankLevel + ((Val(NetBox.Text) / 2000))
                        UpdateCmd = "Update Tank Set CurrentLevel = '" & TankLevel & "' Where Id = '" & txtTank.Text & "';"

                        If SQL.DataUpdate(UpdateCmd) = 0 Then
                            MsgBox("Error updating Tank Record")
                        End If
                    End If
                End If

                UpdateCmd = "UPDATE Trans " &
                                          "SET Id ='" & txtID.Text & "' , " &
                                          "DriverId ='" & cboDriverId.Text & "' , " &
                                          "TDate ='" & txtDate.Text & "' , " &
                                          "InTime ='" & txtInTime.Text & "' , " &
                                          "OutTime ='" & txtOutTime.Text & "' , " &
                                          "Code ='" & cboCode.Text & "' , " &
                                          "TrailerId ='" & txtTrailer.Text & "' , " &
                                          "VehicleId ='" & txtVehicle.Text & "' , " &
                                          "TankId ='" & cboTank.Text & "' , " &
                                          "ScaleNumber ='" & txtScaleNumber.Text & "' , " &
                                          "TargetFillWt ='" & txtTargetWt.Text & "' , " &
                                          "CommodityId ='" & cboCommodity.Text & "' , " &
                                          "GrossWt ='" & GrossBox.Text & "' , " &
                                          "TareWt ='" & TareBox.Text & "' , " &
                                          "NetWt ='" & NetBox.Text & "' , " &
                                          "ReleaseNumber ='" & txtReleaseNumber.Text & "' , " &
                                          "ScaleTicket ='" & txtScaleTicket.Text & "' , " &
                                          "TankLevel ='" & txtTankLevel.Text & "' , " &
                                          "Analysis ='" & txtAnalysis.Text & "' , " &
                                          "Seal1 ='" & txtSeal1.Text & "' , " &
                                          "Seal2 ='" & txtSeal2.Text & "' , " &
                                          "Seal3 ='" & txtSeal3.Text & "' , " &
                                          "Seal4 ='" & txtSeal4.Text & "' , " &
                                          "Adjustment ='" & txtAdjustment.Text & "' " &
                                          "WHERE Id = '" & txtID.Text & "' and Code = '" & cboCode.Text & "';"

                If SQL.DataUpdate(UpdateCmd) = 0 Then
                    Status.Text = "Error updating Trans File"
                    AddLogEntry("Error updating Trans File")
                    'Stop
                Else
                    AddLogEntry("Trans File Updated")
                    Status.Text = "Trans File Updated"
                End If
            End If

            'Now Add new correction
            'First PRE information
            Dim strInsert2 As String = "INSERT INTO Corrections (Operator, Code, Id, TDate, DriverId, VehicleId, TrailerId, ScaleNumber, TankId, CommodityId, TargetFillWt," &
                                       " GrossWt, TareWt, NetWt, ReleaseNumber, ScaleTicket, InTime, OutTime, TankLevel, Analysis, Seal1, Seal2, Seal3, Seal4, Adjustment) VALUES (" &
                                         "'" & OpID & "'," &
                                         "'" & PreCode & "'," &
                                         "'" & PreID & "'," &
                                         "'" & PreTDate & "'," &
                                         "'" & PreDriverId & "'," &
                                         "'" & PreVehicleId & "'," &
                                         "'" & PreTrailerId & "'," &
                                         "'" & PreScaleNumber & "'," &
                                         "'" & PreTankId & "'," &
                                         "'" & PreCommodityId & "'," &
                                         "'" & PreTargetFillWt & "'," &
                                         "'" & PreGrossWt & "'," &
                                         "'" & PreTareWt & "'," &
                                         "'" & PreNetWt & "'," &
                                         "'" & PreReleaseNumber & "'," &
                                         "'" & PreScaleTicket & "'," &
                                         "'" & PreInTime & "'," &
                                         "'" & PreOutTime & "'," &
                                         "'" & PreTankLevel & "'," &
                                         "'" & PreAnalysis & "'," &
                                         "'" & PreSeal1 & "'," &
                                         "'" & PreSeal2 & "'," &
                                         "'" & PreSeal3 & "'," &
                                         "'" & PreSeal4 & "'," &
                                         "'" & PreAdjustment & "') "

            SQL.DataUpdate(strInsert2)

            Dim strInsert3 As String = "INSERT INTO Corrections (Operator, Code, Id, TDate, DriverId, VehicleId, TrailerId, ScaleNumber, TankId, CommodityId, TargetFillWt," &
                                       " GrossWt, TareWt, NetWt, ReleaseNumber, ScaleTicket, InTime, OutTime, TankLevel, Analysis, Seal1, Seal2, Seal3, Seal4, Adjustment) VALUES (" &
                                         "'" & OpID & "'," &
                                         "'" & cboCode.Text & "'," &
                                         "'" & txtID.Text & "'," &
                                         "'" & txtDate.Text & "'," &
                                         "'" & cboDriverId.Text & "'," &
                                         "'" & txtVehicle.Text & "'," &
                                         "'" & txtTrailer.Text & "'," &
                                         "'" & txtScaleNumber.Text & "'," &
                                         "'" & cboTank.Text & "'," &
                                         "'" & cboCommodity.Text & "'," &
                                         "'" & txtTargetWt.Text & "'," &
                                         "'" & GrossBox.Text & "'," &
                                         "'" & TareBox.Text & "'," &
                                         "'" & NetBox.Text & "'," &
                                         "'" & txtReleaseNumber.Text & "'," &
                                         "'" & txtScaleTicket.Text & "'," &
                                         "'" & txtInTime.Text & "'," &
                                         "'" & txtOutTime.Text & "'," &
                                         "'" & txtTankLevel.Text & "'," &
                                         "'" & PreAnalysis & "'," &
                                         "'" & txtSeal1.Text & "'," &
                                         "'" & txtSeal2.Text & "'," &
                                         "'" & txtSeal3.Text & "'," &
                                         "'" & txtSeal4.Text & "'," &
                                         "'" & txtAdjustment.Text & "') "

            SQL.DataUpdate(strInsert3)

            Status.Text = "Record Updated"

        Catch ex As Exception
            Status.Text = "Error updating  File"
            MsgBox(ex.Message)
            AddLogEntry("frmTransactionCorrection error " & ex.Message)
        End Try
    End Sub

    Private Sub cmdReprint_Click(sender As System.Object, e As System.EventArgs) Handles cmdReprint.Click
        NewPrinterCode = cboCode.Text
        NewPrinterID = txtID.Text
        If NewPrinterCode <> "" And NewPrinterID <> "" Then
            PrintLastTicket()
        End If
    End Sub

    Private Sub cboTank_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboTank.SelectedIndexChanged

    End Sub

    Private Sub cboTank_TextChanged(sender As Object, e As System.EventArgs) Handles cboTank.TextChanged
        Try
            SQL.RunQuery("Select * from tank where Id = '" & cboTank.Text & "'")
            If SQL.RecordCount = 0 Then
                Status.Text = "Tank Not Found"
            Else
                If Mid(cboCommodity.Text, 1, 2) <> Mid(SQL.SQLDataset.Tables(0).Rows(0).Item("Commodity"), 1, 2) And cboCommodity.Text <> "" Then
                    MsgBox("WARNING: Tank has different product")
                End If
                cboCommodity.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("Commodity")
                txtDescription.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("Description")
                txtTankLevel.Text = SQL.SQLDataset.Tables(0).Rows(0).Item("CurrentLevel")
                Status.Text = "Tank Found"
            End If

            If SelectedTank <> Val(cboTank.Text) Then
                lblTankInfo.Text = "Tank information will be updated"
                TankChanged = True
            Else
                lblTankInfo.Text = ""
                TankChanged = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtID_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtID.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.Chr(13) Then
            Try
                FindTransaction(cboCode.Text, txtID.Text)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub txtID_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtID.TextChanged

    End Sub

    Private Sub txtTankLevel_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTankLevel.TextChanged

    End Sub

    Private Sub txtTankLevel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTankLevel.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.Chr(13) Then
            Try
                txtTankLevel.Text = Format(Val(txtTankLevel.Text), "#####.##")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End If
    End Sub

    Private Sub GrossBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles GrossBox.TextChanged
        NetBox.Text = Val(GrossBox.Text) - Val(TareBox.Text)
        WeightChanged = True
    End Sub

    Private Sub TareBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles TareBox.TextChanged
        NetBox.Text = Val(GrossBox.Text) - Val(TareBox.Text)
        WeightChanged = True
    End Sub


End Class