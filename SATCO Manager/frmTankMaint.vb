Public Class frmTankMaint
    Dim CurrentRecord As Integer

    Private SQL As New SQLControl

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmTankMaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        LoadTimer.Enabled = True
    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        LoadTimer.Enabled = False
        If FileControl = 0 Then
            cmdAdd.Visible = False
            cmdDelete.Visible = False
            cmdEdit.Visible = False
        End If
        If FileControl = 1 Or FileControl = 2 Then
            cmdAdd.Visible = True
            cmdDelete.Visible = False
            cmdEdit.Visible = True
        End If
        cmdAdd.Text = "Add"
        cmdEdit.Text = "Edit"
        'cmdAdd.Visible = True
        'cmdDelete.Visible = True
        cmdFind.Visible = True
        cmdClear.Visible = True
        txtID.Enabled = False
        txtDescription.Enabled = False
        'txtCurrentLevel.Enabled = False
        cmdLevel.Visible = False
        txtMaxTankTons.Enabled = False
        txtLowAlarmValue.Enabled = False
        cboCommodity.Enabled = False
        txtActiveScale.Enabled = False
        GroupBox2.Enabled = False
        GroupBox3.Enabled = False
        GroupBox4.Enabled = False
        FillCombos()
        cmdFirst.PerformClick()
    End Sub

    Private Sub FillCombos()

        Dim Counter As Integer

        If SQL.HasConnection = True Then
            'Commodity
            cboCommodity.Items.Clear()
            SQL.RunQuery("SELECT * FROM COMMODITY WHERE Active = '1' ")
            For Counter = 0 To 3
                If SQL.SQLDataset.Tables.Count > 0 Then
                    For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                        cboCommodity.Items.Add(r("Id"))
                    Next
                ElseIf SQL.SQLDataset.HasErrors <> "" Then
                    txtStatus.Text = SQL.SQLDataset.HasErrors
                End If
                'Stop
            Next Counter
        End If
    End Sub

    Private Sub cmdFirst_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirst.Click
        Try
            Dim Active As Integer
            SQL.RunQuery("Select * from Tank")
            'FillBoxes
            CurrentRecord = SelectedTank
            SelectedTank = 0
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtID.Text = .Item("ID")
                txtDescription.Text = .Item("Description")
                txtCurrentLevel.Text = .Item("CurrentLevel")
                txtMaxTankTons.Text = .Item("MaxTankTons")
                txtLowAlarmValue.Text = .Item("LowAlarmValue")
                cboCommodity.Text = .Item("Commodity")
                txtActiveScale.Text = .Item("ActiveScale")
                Select Case txtActiveScale.Text
                    Case 0
                        optScale1.Checked = True
                    Case 1
                        optScale2.Checked = True
                    Case 2
                        optBoth.Checked = True
                    Case 3
                        optNone.Checked = True
                End Select

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
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                End If
            End With
            cmdNext.Enabled = True
            cmdPrevious.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdPrevious_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrevious.Click
        Dim Active As Integer
        Try
            cmdNext.Enabled = True
            If CurrentRecord <= 0 Then
                cmdPrevious.Enabled = False
                Exit Sub
            End If
            CurrentRecord = CurrentRecord - 1
            'FillBoxes
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtID.Text = .Item("ID")
                txtDescription.Text = .Item("Description")
                txtCurrentLevel.Text = .Item("CurrentLevel")
                txtMaxTankTons.Text = .Item("MaxTankTons")
                txtLowAlarmValue.Text = .Item("LowAlarmValue")
                cboCommodity.Text = .Item("Commodity")
                txtActiveScale.Text = .Item("ActiveScale")
                Select Case txtActiveScale.Text
                    Case 0
                        optScale1.Checked = True
                    Case 1
                        optScale2.Checked = True
                    Case 2
                        optBoth.Checked = True
                    Case 3
                        optNone.Checked = True
                End Select
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
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                End If
            End With
        Catch ex As Exception
            'MsgBox(ex.Message)
            If InStr(ex.Message, "There is no row") <> 0 Then
                cmdPrevious.Enabled = False
            End If
        End Try
    End Sub

    Private Sub cmdNext_Click(sender As System.Object, e As System.EventArgs) Handles cmdNext.Click
        Dim Active As Integer
        Try
            cmdPrevious.Enabled = True
            CurrentRecord = CurrentRecord + 1
            'FillBoxes
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtID.Text = .Item("ID")
                txtDescription.Text = .Item("Description")
                txtCurrentLevel.Text = .Item("CurrentLevel")
                txtMaxTankTons.Text = .Item("MaxTankTons")
                txtLowAlarmValue.Text = .Item("LowAlarmValue")
                cboCommodity.Text = .Item("Commodity")
                txtActiveScale.Text = .Item("ActiveScale")
                Select Case txtActiveScale.Text
                    Case 0
                        optScale1.Checked = True
                    Case 1
                        optScale2.Checked = True
                    Case 2
                        optBoth.Checked = True
                    Case 3
                        optNone.Checked = True
                End Select
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
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                End If
            End With
        Catch ex As Exception
            'MsgBox(ex.Message)
            If InStr(ex.Message, "There is no row") <> 0 Then
                cmdNext.Enabled = False
            End If
        End Try
    End Sub

    Private Sub cmdEdit_Click(sender As System.Object, e As System.EventArgs) Handles cmdEdit.Click
        Dim Active As Integer
        Dim CommodityType As Integer
        If cmdEdit.Text = "Edit" Then
            cmdEdit.Text = "Update"
            cmdAdd.Visible = False
            cmdDelete.Visible = False
            cmdFind.Visible = False
            cmdClear.Visible = False

            txtID.Enabled = True
            txtDescription.Enabled = True
            'txtCurrentLevel.Enabled = True
            cmdLevel.Visible = True
            txtMaxTankTons.Enabled = True
            txtLowAlarmValue.Enabled = True
            cboCommodity.Enabled = True
            txtActiveScale.Enabled = True
            GroupBox2.Enabled = True
            GroupBox3.Enabled = True
            GroupBox4.Enabled = True

        Else
            cmdEdit.Text = "Edit"
            cmdAdd.Visible = True
            cmdDelete.Visible = True
            cmdFind.Visible = True
            cmdClear.Visible = True

            If optTypeSA.Checked = True Then
                CommodityType = 0
            End If
            If optTypeUC.Checked = True Then
                CommodityType = 1
            End If
            If optTypeMO.Checked = True Then
                CommodityType = 2
            End If
            If optTypeBN.Checked = True Then
                CommodityType = 3
            End If
            If optYes.Checked = True Then
                Active = 1
            Else
                Active = 0
            End If
            Dim UpdateCmd As String = "Update Tank " & _
                                          "Set Id ='" & txtID.Text & "' , " & _
                                          "Description ='" & txtDescription.Text & "' , " & _
                                          "CurrentLevel ='" & txtCurrentLevel.Text & "', " & _
                                          "MaxTankTons ='" & txtMaxTankTons.Text & "', " & _
                                          "LowAlarmValue ='" & txtLowAlarmValue.Text & "', " & _
                                          "Commodity ='" & cboCommodity.Text & "', " & _
                                          "ActiveScale ='" & txtActiveScale.Text & "', " & _
                                          "Type ='" & CommodityType & "', " & _
                                          "Active ='" & Active & "' " & _
                                          "Where Id = '" & txtID.Text & "';"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                txtStatus.Text = "Error updating Tank Record"
                'Stop
            Else
                txtStatus.Text = "Tank Record Updated"
                'Stop
            End If
            txtID.Enabled = False
            txtDescription.Enabled = False
            'txtCurrentLevel.Enabled = False
            cmdLevel.Visible = False
            txtMaxTankTons.Enabled = False
            txtLowAlarmValue.Enabled = False
            cboCommodity.Enabled = False
            txtActiveScale.Enabled = False
            GroupBox2.Enabled = False
            GroupBox3.Enabled = False
            GroupBox4.Enabled = False
            AddLogEntry("Tank Record " & txtID.Text & " was edited ")
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim CommodityType As Integer
        Dim Active As Integer
        Try
            If optTypeSA.Checked = True Then
                CommodityType = 0
            End If
            If optTypeUC.Checked = True Then
                CommodityType = 1
            End If
            If optTypeMO.Checked = True Then
                CommodityType = 2
            End If
            If optTypeBN.Checked = True Then
                CommodityType = 3
            End If
            If optYes.Checked = True Then
                Active = 1
            Else
                Active = 0
            End If
            Dim strInsert As String = "INSERT INTO Tank (Id, Description, CurrentLevel, MaxTankTons, LowAlarmVale, Commodity, ActiveScale, Type, Active) " & _
                                         "VALUES (" & _
                                         "'" & txtID.Text & "'," & _
                                         "'" & txtDescription.Text & "'," & _
                                         "'" & txtCurrentLevel.Text & "'," & _
                                         "'" & txtMaxTankTons.Text & "'," & _
                                         "'" & txtLowAlarmValue.Text & "'," & _
                                         "'" & cboCommodity.Text & "'," & _
                                         "'" & txtActiveScale.Text & "'," & _
                                         "'" & CommodityType & "', " & _
                                         "'" & Active & "') "
            SQL.DataUpdate(strInsert)
            'AddRecord = True
        Catch ex As Exception
            'AddRecord = False
        End Try
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelete.Click
        Try
            SQL.RunQuery("Select * from Tank where Id = '" & txtID.Text & "' ")
            If SQL.RecordCount = 0 Then

                'Exit Sub
            Else
                'Delete record
                SQL.DataUpdate("DELETE FROM Tank where Id ='" & txtID.Text & "' ")

            End If
        Catch ex As Exception
            AddLogEntry("DeleteTank: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtID.Text = ""
        txtDescription.Text = ""
        txtCurrentLevel.Text = ""
        cmdLevel.Visible = False
        txtMaxTankTons.Text = ""
        txtLowAlarmValue.Text = ""
        cboCommodity.Text = ""
        txtActiveScale.Text = ""
    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click

        If txtID.Enabled = False Then
            'Enable all
            txtID.Enabled = True
            'txtDescription.Enabled = True
            'txtCurrentLevel.Enabled = True
            'txtMaxTankTons.Enabled = True
            'txtLowAlarmValue.Enabled = True
            'cboCommodity.Enabled = True
            'txtActiveScale.Enabled = True
            GroupBox2.Enabled = True
            GroupBox3.Enabled = True
            GroupBox4.Enabled = True
            'Now clear them
            txtID.Text = ""
            txtDescription.Text = ""
            txtCurrentLevel.Text = ""
            txtMaxTankTons.Text = ""
            txtLowAlarmValue.Text = ""
            cboCommodity.Text = ""
            txtActiveScale.Text = ""
            Exit Sub
        End If
        'Check each field to see if data exists 
        'then create a Query to search the database
        Dim Query As String = ""
        If txtID.Text <> "" Then
            Query = "Id ='" & txtID.Text & "'"
        End If

        Try
            SQL.RunQuery("Select * from Tank where " & Query)
            If SQL.RecordCount = 0 Then
                txtStatus.Text = "No Record Found"
            Else
                With SQL.SQLDataset.Tables(0).Rows(0)
                    txtID.Text = .Item("ID")
                    txtDescription.Text = .Item("Description")
                    txtCurrentLevel.Text = .Item("CurrentLevel")
                    txtMaxTankTons.Text = .Item("MaxTankTons")
                    txtLowAlarmValue.Text = .Item("LowAlarmValue")
                    cboCommodity.Text = .Item("Commodity")
                    txtActiveScale.Text = .Item("ActiveScale")
                    Select Case txtActiveScale.Text
                        Case 0
                            optScale1.Checked = True
                        Case 1
                            optScale2.Checked = True
                        Case 2
                            optBoth.Checked = True
                        Case 3
                            optNone.Checked = True
                    End Select
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
                    Dim Active As Integer = .Item("Active")
                    If Active = 0 Then
                        optYes.Checked = False
                        optNo.Checked = True
                    Else
                        optYes.Checked = True
                        optNo.Checked = False
                    End If
                End With
            End If
            txtID.Enabled = False
            txtDescription.Enabled = False
            txtCurrentLevel.Enabled = False
            cmdLevel.Visible = False
            txtMaxTankTons.Enabled = False
            txtLowAlarmValue.Enabled = False
            cboCommodity.Enabled = False
            txtActiveScale.Enabled = False
            GroupBox2.Enabled = False
            GroupBox3.Enabled = False
            GroupBox4.Enabled = False
        Catch ex As Exception
            AddLogEntry("FindSA: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click_1(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub optTypeSA_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optTypeSA.CheckedChanged

    End Sub

    Private Sub optScale1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optScale1.CheckedChanged
        If optScale1.Checked = True Then txtActiveScale.Text = 0
    End Sub

    Private Sub optScale2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optScale2.CheckedChanged
        If optScale2.Checked = True Then txtActiveScale.Text = 1
    End Sub

    Private Sub optBoth_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optBoth.CheckedChanged
        If optBoth.Checked = True Then txtActiveScale.Text = 2
    End Sub

    Private Sub optNone_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optNone.CheckedChanged
        If optNone.Checked = True Then txtActiveScale.Text = 3
    End Sub

    Private Sub cmdLevel_Click(sender As System.Object, e As System.EventArgs) Handles cmdLevel.Click
        TankSearch = txtID.Text
        frmTankContents.ShowDialog()
        SelectedTank = Val(TankSearch) - 1
        Me.Close()

    End Sub

    Private Sub txtID_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtID.TextChanged

    End Sub
End Class