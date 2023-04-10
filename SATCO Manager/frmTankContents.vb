Public Class frmTankContents
    Dim CurrentRecord As Integer
    Private SQL As New SQLControl

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub frmTankContents_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        If FileControl = 0 Then
            cmdSave.Visible = False
        Else
            cmdSave.Visible = True
        End If
        'Fill up ComboBox
        cboAdjType.Items.Clear()
        cboAdjType.Text = ""
        ComboFill()
        ClearForm()
        If TankSearch <> "" Then
            cboTankID.Text = TankSearch
            txtAdjTons.Focus()
            GetTankInfo(TankSearch)
        End If

    End Sub

    Private Sub ComboFill()
        SQL.RunQuery("Select * from Tank")
        cboTankID.Items.Clear()
        For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
            cboTankID.Items.Add(r("Id"))
        Next
        cboAdjType.Items.Add("Dilution")
        cboAdjType.Items.Add("Book Adjustment")
        cboAdjType.Items.Add("Vessel")
        cboAdjType.Items.Add("Railcar")
    End Sub

    Private Sub ClearForm()
        txtDate.Text = Format(Now, "MM-dd-yyyy")
        txtTime.Text = Format(Now, "HH:mm")
        cboTankID.Text = ""
        txtAdjTons.Text = ""
        txtCurrentLevel.Text = ""
        'txtCurrentTons.Text = ""
        txtDescription.Text = ""
        txtMaxTankTons.Text = ""
        txtReason.Text = ""

    End Sub

    Private Sub GetTankInfo(ByVal TankID As String)
        Dim Commodity As clsCommodity
        Dim Tank As clsTank

        Try
            Commodity = New clsCommodity
            Tank = New clsTank

            Tank.FindRecord(TankID)
            'Now get Commodity info related to tank info
            Commodity.FindRecord(Tank.Commodity)
            txtDescription2.Text = Commodity.Description2
            txtDescription3.Text = Commodity.Description3
            txtDescription4.Text = Commodity.Description4
            Commodity = Nothing
            Tank = Nothing

            Dim Active As Integer
            SQL.RunQuery("Select * from Tank where Id = '" & TankID & "'")
            'FillBoxes
            With SQL.SQLDataset.Tables(0).Rows(0)
                'cboTankID.Text = .Item("ID")
                txtDescription.Text = .Item("Description")
                txtCurrentLevel.Text = .Item("CurrentLevel")
                'txtCurrentTons.Text = Val(txtCurrentLevel.Text) / 2000
                txtMaxTankTons.Text = .Item("MaxTankTons")
                'txtLowAlarmValue.Text = .Item("LowAlarmValue")
                'txtCommodity.Text = .Item("Commodity")
                'txtActiveScale.Text = .Item("ActiveScale")
                Dim CommodityType As Integer = .Item("Type")
                Select Case CommodityType
                    Case 0
                        optTypeSA.Checked = True
                    Case 1
                        optTypeUC.Checked = True
                    Case 2
                        optTypeMO.Checked = True
                    Case 3
                        optTypeBN.Checked = True
                End Select
                Active = .Item("Active")
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cboTankID_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboTankID.SelectedIndexChanged
        GetTankInfo(cboTankID.Text)
        If frmMain.optStockton.Checked = True Then
            If cboTankID.Text = 100 Or cboTankID.Text = 103 Then
                cboAdjType.Visible = True
                Label5.Visible = True
            Else
                cboAdjType.Visible = False
                Label5.Visible = False
            End If
        Else
            If cboTankID.Text = 1 Or cboTankID.Text = 2 Or cboTankID.Text = 5 Then
                cboAdjType.Visible = True
                Label5.Visible = True
            Else
                cboAdjType.Visible = False
                Label5.Visible = False
            End If
        End If

    End Sub

    Private Sub cmdSave_Click(sender As System.Object, e As System.EventArgs) Handles cmdSave.Click
        'Dim Adjustments As clsTankAdjustments
        Dim Trans As clsTransaction
        Dim Consignee As clsConsignee
        Dim Tank As clsTank

        If txtReason.Text = "" Then
            MsgBox("Please describe Reason for Adjustment")
            Exit Sub
        End If
        If cboAdjType.Text = "" Then
            If cboTankID.Text = 1 Or cboTankID.Text = 2 Or cboTankID.Text = 5 Or cboTankID.Text = 100 Or cboTankID.Text = 103 Then
                MsgBox("Please select Adjustment Code")
                Exit Sub
            End If
        End If

        'Adjustments = New clsTankAdjustments
        Trans = New clsTransaction
        Consignee = New clsConsignee
        Tank = New clsTank
        If IsDate(txtDate.Text) And IsDate(txtTime.Text) Then
            If IsNumeric(txtAdjTons.Text) Then
                Tank.FindRecord(cboTankID.Text)
                If Not Tank.EOF Then
                    'If Tank.Active = False Then
                    '    MsgBox "Tank is INACTIVE !", vbOKOnly, "Tank Error"
                    'Else
                    If (Tank.MaxTankTons) >= (Tank.CurrentLevel) + Val(txtAdjTons.Text) Then
                        If (Val(Tank.CurrentLevel) + Val(txtAdjTons.Text)) < 0 Then
                            MsgBox("Adjustment Error !  Tank under 0 !", vbOKOnly, "Adjustment Error")
                        Else

                            ' Update Tank Record
                            Tank.CurrentLevel = Tank.CurrentLevel + Val(txtAdjTons.Text)
                            frmTankInventory.txtLevel4.Text = Tank.CurrentLevel
                            Tank.UpdateRecord(Tank.Id)
                            ' Now add to Transaction Table in database
                            AddLogEntry("Adjusting level in tank " & cboTankID.Text)
                            If cboTankID.Text = 1 Then
                                frmTankInventory.txtLevel4.Text = Tank.CurrentLevel
                                Tank.UpdateRecord(Tank.Id)

                                Trans.TankId = "01"
                                Trans.Commodity = "SA"
                                Trans.TDate = CDate(txtDate.Text & " " & txtTime.Text)
                                Trans.Gross = Val(txtAdjTons.Text) * 2000
                                Trans.TankLevel = Tank.CurrentLevel
                                Trans.ReleaseNumber = "INV"

                                Trans.Code = "SA00000"
                                Select Case cboAdjType.Text
                                    Case "Dilution"
                                        Trans.Code = "SA00001"
                                    Case "Book Adjustment"
                                        Trans.Code = "SA00002"
                                    Case "Vessel"
                                        Trans.Code = "SA00003"
                                    Case "Railcar"
                                        Trans.Code = "SA00004"
                                End Select
                                Trans.Id = Consignee.GetNextTransNumber(Trans.Code)
                                Trans.DriverId = "SAT01"
                                Trans.Analysis = txtDescription2.Text
                                Trans.Adjustment = txtReason.Text
                                Trans.AddRecord()
                            End If
                            If cboTankID.Text = 2 Then
                                frmTankInventory.txtLevel1.Text = Tank.CurrentLevel
                                Tank.UpdateRecord(Tank.Id)

                                Trans.TankId = "02"
                                Trans.Commodity = "SA"
                                Trans.TDate = CDate(txtDate.Text & " " & txtTime.Text)
                                Trans.Gross = Val(txtAdjTons.Text) * 2000
                                Trans.TankLevel = Tank.CurrentLevel
                                Trans.ReleaseNumber = "INV"

                                Trans.Code = "SA00000"
                                Select Case cboAdjType.Text
                                    Case "Dilution"
                                        Trans.Code = "SA00001"
                                    Case "Book Adjustment"
                                        Trans.Code = "SA00002"
                                    Case "Vessel"
                                        Trans.Code = "SA00003"
                                    Case "Railcar"
                                        Trans.Code = "SA00004"
                                End Select
                                Trans.Id = Consignee.GetNextTransNumber(Trans.Code)
                                Trans.DriverId = "SAT01"
                                Trans.Analysis = txtDescription2.Text
                                Trans.Adjustment = txtReason.Text
                                Trans.AddRecord()
                            End If
                            If cboTankID.Text = 5 Then
                                frmTankInventory.txtLevel4.Text = Tank.CurrentLevel
                                Tank.UpdateRecord(Tank.Id)

                                Trans.TankId = "05"
                                Trans.Commodity = "SA"
                                Trans.TDate = CDate(txtDate.Text & " " & txtTime.Text)
                                Trans.Gross = Val(txtAdjTons.Text) * 2000
                                Trans.TankLevel = Tank.CurrentLevel
                                Trans.ReleaseNumber = "INV"
                                Trans.Code = "SA00000"
                                Select Case cboAdjType.Text
                                    Case "Dilution"
                                        Trans.Code = "SA00001"
                                    Case "Book Adjustment"
                                        Trans.Code = "SA00002"
                                    Case "Vessel"
                                        Trans.Code = "SA00003"
                                    Case "Railcar"
                                        Trans.Code = "SA00004"
                                End Select
                                Trans.Id = Consignee.GetNextTransNumber(Trans.Code)
                                Trans.DriverId = "SAT01"
                                Trans.Analysis = txtDescription2.Text
                                Trans.Adjustment = txtReason.Text
                                Trans.AddRecord()
                            End If
                            If cboTankID.Text = 3 Then
                                frmTankInventory.txtLevel2.Text = Tank.CurrentLevel
                                Tank.UpdateRecord(Tank.Id)

                                Trans.TankId = "03"
                                Trans.Commodity = "BN03"
                                Trans.TDate = CDate(txtDate.Text & " " & txtTime.Text)
                                Trans.Gross = Val(txtAdjTons.Text) * 2000
                                Trans.TankLevel = Tank.CurrentLevel
                                Trans.ReleaseNumber = "INV"
                                Trans.Code = "BN0001"
                                Trans.Id = Consignee.GetNextTransNumber("BN0001")
                                Trans.DriverId = "BT01"
                                Trans.Analysis = "50%"
                                Trans.Adjustment = txtReason.Text
                                Trans.AddRecord()
                            End If
                            If cboTankID.Text = 4 Then
                                frmTankInventory.txtLevel3.Text = Tank.CurrentLevel
                                Tank.UpdateRecord(Tank.Id)

                                Trans.TankId = "04"
                                Trans.Commodity = "MO04"
                                Trans.TDate = CDate(txtDate.Text & " " & txtTime.Text)
                                Trans.Gross = Val(txtAdjTons.Text) * 2000
                                Trans.TankLevel = Tank.CurrentLevel
                                Trans.ReleaseNumber = "INV"
                                Trans.Code = "MO00000"
                                Trans.Id = Consignee.GetNextTransNumber("MO00000")
                                Trans.DriverId = "MO01"
                                Trans.Analysis = "32%"
                                Trans.Adjustment = txtReason.Text
                                Trans.AddRecord()
                            End If
                            If cboTankID.Text > "05" And cboTankID.Text < "16" Then
                                frmTankInventory.txtLevel4.Text = Tank.CurrentLevel
                                Tank.UpdateRecord(Tank.Id)

                                Trans.TankId = Val(cboTankID.Text)
                                Trans.Commodity = "UC" & cboTankID.Text
                                Trans.TDate = CDate(txtDate.Text & " " & txtTime.Text)
                                Trans.Gross = Val(txtAdjTons.Text) * 2000
                                Trans.TankLevel = Tank.CurrentLevel
                                Trans.ReleaseNumber = "INV"
                                Trans.Code = "UC00000"
                                Trans.Id = Consignee.GetNextTransNumber("UC00000")
                                Trans.DriverId = "SAT01"
                                Trans.Analysis = txtDescription3.Text
                                Trans.Adjustment = txtReason.Text
                                Trans.AddRecord()
                            End If
                            If cboTankID.Text = 100 Then
                                frmTankInventory.txtLevel4.Text = Tank.CurrentLevel
                                Tank.UpdateRecord(Tank.Id)

                                Trans.TankId = "100"
                                Trans.Commodity = "SA"
                                Trans.TDate = CDate(txtDate.Text & " " & txtTime.Text)
                                Trans.Gross = Val(txtAdjTons.Text) * 2000
                                Trans.TankLevel = Tank.CurrentLevel
                                Trans.ReleaseNumber = "INV"

                                Trans.Code = "SA00000"
                                Select Case cboAdjType.Text
                                    Case "Dilution"
                                        Trans.Code = "SA00001"
                                    Case "Book Adjustment"
                                        Trans.Code = "SA00002"
                                    Case "Vessel"
                                        Trans.Code = "SA00003"
                                    Case "Railcar"
                                        Trans.Code = "SA00004"
                                End Select
                                Trans.Id = Consignee.GetNextTransNumber(Trans.Code)
                                Trans.DriverId = "SAT100"
                                Trans.Analysis = txtDescription2.Text
                                Trans.Adjustment = txtReason.Text
                                Trans.AddRecord()
                            End If
                            If cboTankID.Text = 103 Then
                                frmTankInventory.txtLevel1.Text = Tank.CurrentLevel
                                Tank.UpdateRecord(Tank.Id)

                                Trans.TankId = "103"
                                Trans.Commodity = "SA"
                                Trans.TDate = CDate(txtDate.Text & " " & txtTime.Text)
                                Trans.Gross = Val(txtAdjTons.Text) * 2000
                                Trans.TankLevel = Tank.CurrentLevel
                                Trans.ReleaseNumber = "INV"

                                Trans.Code = "SA00000"
                                Select Case cboAdjType.Text
                                    Case "Dilution"
                                        Trans.Code = "SA00001"
                                    Case "Book Adjustment"
                                        Trans.Code = "SA00002"
                                    Case "Vessel"
                                        Trans.Code = "SA00003"
                                    Case "Railcar"
                                        Trans.Code = "SA00004"
                                End Select
                                Trans.Id = Consignee.GetNextTransNumber(Trans.Code)
                                Trans.DriverId = "SAT103"
                                Trans.Analysis = txtDescription2.Text
                                Trans.Adjustment = txtReason.Text
                                Trans.AddRecord()
                            End If
                            NewTankLevel = Tank.CurrentLevel
                            ' Close form  all done
                            Me.Close()
                        End If
                    Else
                        MsgBox("Adjustment Error !  Tank over Max Tons !", vbOKOnly, "Adjustment Error")
                    End If
                    'End If
                Else
                    MsgBox("Invalid Tank Id !", vbOKOnly, "Tank Id Error")
                End If
            Else
                MsgBox("Invalid Adjustment amount !", vbOKOnly, "Adjustment Error")
            End If
        Else
            MsgBox("Invalid Date or Time entered !", vbOKOnly, "Date/Time Error")
        End If

        Tank = Nothing
        'Adjustments = Nothing

    End Sub

    Private Sub optTypeBN_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optTypeBN.CheckedChanged

    End Sub
End Class