Public Class frmTankInventory
    Dim StartTank As Integer
    Dim EndTank As Integer
    Dim Page As Integer = 0

    Private SQL As New SQLControl

    Private Sub frmTankInventory_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        CenterForm(Me)
        'FillCombos()
        ClearTanks()
        GetTankInfo(0)
        SelectedTank = 0    'This will be changed if a tank is chosen to open the correct Tank in the frmTankMaint form

        StartTank = 1
        Page = 0
        cmdEdit.Text = "Change Activity"
        chkActive1.Enabled = False
        chkActive2.Enabled = False
        chkActive3.Enabled = False
        chkActive4.Enabled = False
        chkActive5.Enabled = False

        If ControlCenter = True Then   'FileControl = 1 Or FileControl = 2 Then
            cmdEdit.Visible = True
            lblStatus.Visible = False
        Else
            cmdEdit.Visible = False
            lblStatus.Visible = True
        End If

        If frmMain.optStockton.Checked = True Then
            cmdPrevious.Visible = False
            cmdNext.Visible = False
            'Remove Tank informatin for Tanks 3-5
            'Tank 3
            txtTankId3.Visible = False
            txtDescription3.Visible = False
            txtType3.Visible = False
            FillTank3.Visible = False
            txtLevel3.Visible = False
            txtMaxWeight3.Visible = False
            txtAlarm3.Visible = False
            chkActive3.Visible = False
            cboScale3.Visible = False
            cboCommodity3.Visible = False
            lblAlarm3.Visible = False
            'Tank 4
            txtTankId4.Visible = False
            txtDescription4.Visible = False
            txtType4.Visible = False
            FillTank4.Visible = False
            txtLevel4.Visible = False
            txtMaxWeight4.Visible = False
            txtAlarm4.Visible = False
            chkActive4.Visible = False
            cboScale4.Visible = False
            cboCommodity4.Visible = False
            lblAlarm4.Visible = False
            'Tank 5
            txtTankId5.Visible = False
            txtDescription5.Visible = False
            txtType5.Visible = False
            FillTank5.Visible = False
            txtLevel5.Visible = False
            txtMaxWeight5.Visible = False
            txtAlarm5.Visible = False
            chkActive5.Visible = False
            cboScale5.Visible = False
            cboCommodity5.Visible = False
            lblAlarm5.Visible = False

        Else
            cmdPrevious.Visible = True
            cmdNext.Visible = True
        End If

        If SALoadingFlag = True Then
            cmdEdit.Text = "Loading in Progress"
            cmdEdit.Enabled = False
        Else
            cmdEdit.Text = "Change Activity"
            cmdEdit.Enabled = True
        End If

    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        'Me.Close()
        Me.Dispose()
    End Sub


    Private Sub FillCombos()

        Dim Counter As Integer

        If SQL.HasConnection = True Then
            'Commodity
            SQL.RunQuery("SELECT * FROM COMMODITY WHERE Active = '1' ")
            For Counter = 0 To 3
                If SQL.SQLDataset.Tables.Count > 0 Then
                    For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                        cboCommodity4.Items.Add(r("Id"))
                    Next
                ElseIf SQL.SQLDataset.HasErrors <> "" Then
                    MsgBox(SQL.SQLDataset.HasErrors)
                End If
                'Stop
            Next Counter
        End If
    End Sub

    Private Sub GetTankInfo(ByVal Index As Integer)
        Dim LowValue As Double
        Dim Tics As Double
        Dim YValue As Double

        Try

            If SQL.HasConnection = True Then
                'Stop
                SQL.RunQuery("SELECT * FROM Tank")
                If SQL.RecordCount > 0 Then

                    With SQL.SQLDataset.Tables(0).Rows(Index)
                        cboCommodity1.Text = .Item("Commodity")
                        txtMaxWeight1.Text = .Item("MaxTankTons")
                        txtTankId1.Text = .Item("Id")
                        txtDescription1.Text = .Item("Description")
                        txtLevel1.Text = .Item("CurrentLevel")
                        txtAlarm1.Text = .Item("LowAlarmValue")
                        If .Item("Active") = True Then
                            chkActive1.Checked = True
                        Else
                            chkActive1.Checked = False
                        End If
                        Select Case .Item("Type")
                            Case 0
                                txtType1.Text = "SA"
                            Case 1
                                txtType1.Text = "UC"
                            Case 2
                                txtType1.Text = "MO"
                            Case 3
                                txtType1.Text = "BN"
                        End Select
                        ' Scale 1 = 0     Scale 2 = 1     Scale Both = 2     Scale None = 3
                        Select Case .Item("ActiveScale")
                            Case 0
                                cboScale1.Text = "1"
                            Case 1
                                cboScale1.Text = "2"
                            Case 2
                                cboScale1.Text = "Both"
                            Case 3
                                cboScale1.Text = "None"
                        End Select
                        If Val(.Item("MaxTankTons")) > 0 Then
                            FillTank1.Maximum = Val(.Item("MaxTankTons")) '/ 1000
                        End If
                        If Val(.Item("CurrentLevel")) > 0 And Val(.Item("CurrentLevel")) < Val(.Item("MaxTankTons")) Then
                            FillTank1.Value = Val(.Item("CurrentLevel")) '/ 1000
                        Else
                            FillTank1.Value = 0
                        End If
                        ' ----- Set the bar for Low alarm value -----
                        LowValue = Val(.Item("LowAlarmValue")) '/ 1000
                        If FillTank1.Maximum <= 0 Or LowValue <= 0 Then
                            Tics = 0
                        Else
                            Tics = (FillTank1.Height / FillTank1.Maximum) * (LowValue)
                        End If

                        YValue = (FillTank1.Top + FillTank1.Height) - Tics
                        'lneAlarm1.Y1 = YValue
                        'lneAlarm1.Y2 = YValue
                        'lneAlarm1.Visible = True
                        lblAlarm1.Top = YValue - 10
                        lblAlarm1.Visible = True
                        If .Item("CurrentLevel") >= .Item("LowAlarmValue") Then
                            ' ----- Set background depending on whether over or under alarm value -----
                            txtMaxWeight1.BackColor = Color.White
                            txtTankId1.BackColor = Color.White
                            txtDescription1.BackColor = Color.White
                            txtLevel1.BackColor = Color.White
                        Else
                            txtMaxWeight1.BackColor = Color.Red
                            txtTankId1.BackColor = Color.Red
                            txtDescription1.BackColor = Color.Red
                            txtLevel1.BackColor = Color.Red
                        End If
                    End With

                    With SQL.SQLDataset.Tables(0).Rows(Index + 1)
                        cboCommodity2.Text = .Item("Commodity")
                        txtMaxWeight2.Text = .Item("MaxTankTons")
                        txtTankId2.Text = .Item("Id")
                        txtDescription2.Text = .Item("Description")
                        txtLevel2.Text = .Item("CurrentLevel")
                        txtAlarm2.Text = .Item("LowAlarmValue")
                        If .Item("Active") = True Then
                            chkActive2.Checked = True
                        Else
                            chkActive2.Checked = False
                        End If
                        Select Case .Item("Type")
                            Case 0
                                txtType2.Text = "SA"
                            Case 1
                                txtType2.Text = "UC"
                            Case 2
                                txtType2.Text = "MO"
                            Case 3
                                txtType2.Text = "BN"
                        End Select
                        ' Scale 1 = 0     Scale 2 = 1     Scale Both = 2     Scale None = 3
                        Select Case .Item("ActiveScale")
                            Case 0
                                cboScale2.Text = "1"
                            Case 1
                                cboScale2.Text = "2"
                            Case 2
                                cboScale2.Text = "Both"
                            Case 3
                                cboScale2.Text = "None"
                        End Select
                        If Val(.Item("MaxTankTons")) > 0 Then
                            FillTank2.Maximum = Val(.Item("MaxTankTons")) '/ 1000
                        End If
                        If Val(.Item("CurrentLevel")) > 0 And Val(.Item("CurrentLevel")) < Val(.Item("MaxTankTons")) Then
                            FillTank2.Value = Val(.Item("CurrentLevel")) '/ 1000
                        Else
                            FillTank2.Value = 0
                        End If
                        ' ----- Set the bar for Low alarm value -----
                        LowValue = Val(.Item("LowAlarmValue")) '/ 1000
                        If FillTank1.Maximum <= 0 Or LowValue <= 0 Then
                            Tics = 0
                        Else
                            Tics = (FillTank1.Height / FillTank1.Maximum) * (LowValue)
                        End If
                        YValue = (FillTank2.Top + FillTank2.Height) - Tics
                        'lneAlarm2.Y1 = YValue
                        'lneAlarm2.Y2 = YValue
                        'lneAlarm2.Visible = True
                        lblAlarm2.Top = YValue - 10
                        lblAlarm2.Visible = True
                        If .Item("CurrentLevel") >= .Item("LowAlarmValue") Then
                            ' ----- Set background depending on whether over or under alarm value -----
                            txtMaxWeight2.BackColor = Color.White
                            txtTankId2.BackColor = Color.White
                            txtDescription2.BackColor = Color.White
                            txtLevel2.BackColor = Color.White
                        Else
                            txtMaxWeight2.BackColor = Color.Red
                            txtTankId2.BackColor = Color.Red
                            txtDescription2.BackColor = Color.Red
                            txtLevel2.BackColor = Color.Red
                        End If
                    End With

                    If DefaultLocation = "S" Then
                        Exit Sub
                    End If

                    With SQL.SQLDataset.Tables(0).Rows(Index + 2)
                        cboCommodity3.Text = .Item("Commodity")
                        txtMaxWeight3.Text = .Item("MaxTankTons")
                        txtTankId3.Text = .Item("Id")
                        txtDescription3.Text = .Item("Description")
                        txtLevel3.Text = .Item("CurrentLevel")
                        txtAlarm3.Text = .Item("LowAlarmValue")
                        If .Item("Active") = True Then
                            chkActive3.Checked = True
                        Else
                            chkActive3.Checked = False
                        End If
                        Select Case .Item("Type")
                            Case 0
                                txtType3.Text = "SA"
                            Case 1
                                txtType3.Text = "UC"
                            Case 2
                                txtType3.Text = "MO"
                            Case 3
                                txtType3.Text = "BN"
                        End Select
                        ' Scale 1 = 0     Scale 2 = 1     Scale Both = 2     Scale None = 3
                        Select Case .Item("ActiveScale")
                            Case 0
                                cboScale3.Text = "1"
                            Case 1
                                cboScale3.Text = "2"
                            Case 2
                                cboScale3.Text = "Both"
                            Case 3
                                cboScale3.Text = "None"
                        End Select
                        If Val(.Item("MaxTankTons")) > 0 Then
                            FillTank3.Maximum = Val(.Item("MaxTankTons")) '/ 1000
                        End If
                        If Val(.Item("CurrentLevel")) > 0 And Val(.Item("CurrentLevel")) < Val(.Item("MaxTankTons")) Then
                            FillTank3.Value = Val(.Item("CurrentLevel")) '/ 1000
                        Else
                            FillTank3.Value = 0
                        End If
                        ' ----- Set the bar for Low alarm value -----
                        LowValue = Val(.Item("LowAlarmValue")) '/ 1000
                        If FillTank1.Maximum <= 0 Or LowValue <= 0 Then
                            Tics = 0
                        Else
                            Tics = (FillTank1.Height / FillTank1.Maximum) * (LowValue)
                        End If
                        YValue = (FillTank3.Top + FillTank3.Height) - Tics
                        'lneAlarm3.Y1 = YValue
                        'lneAlarm3.Y2 = YValue
                        'lneAlarm3.Visible = True
                        lblAlarm3.Top = YValue - 10
                        lblAlarm3.Visible = True
                        If .Item("CurrentLevel") >= .Item("LowAlarmValue") Then
                            ' ----- Set background depending on whether over or under alarm value -----
                            txtMaxWeight3.BackColor = Color.White
                            txtTankId3.BackColor = Color.White
                            txtDescription3.BackColor = Color.White
                            txtLevel3.BackColor = Color.White
                        Else
                            txtMaxWeight3.BackColor = Color.Red
                            txtTankId3.BackColor = Color.Red
                            txtDescription3.BackColor = Color.Red
                            txtLevel3.BackColor = Color.Red
                        End If
                    End With

                    With SQL.SQLDataset.Tables(0).Rows(Index + 3)
                        cboCommodity4.Text = .Item("Commodity")
                        txtMaxWeight4.Text = Format(Val(.Item("MaxTankTons")), "##,###,##0.00")
                        txtLevel4.Text = Format(Val(.Item("CurrentLevel")), "##,###,##0.00")
                        'txtMaxWeight0.Text = .Item("MaxTankTons")
                        txtTankId4.Text = .Item("Id")
                        txtDescription4.Text = .Item("Description")
                        'txtLevel0.Text = .Item("CurrentLevel")
                        txtAlarm4.Text = .Item("LowAlarmValue")
                        If .Item("Active") = True Then
                            chkActive4.Checked = True
                        Else
                            chkActive4.Checked = False
                        End If
                        Select Case .Item("Type")
                            Case 0
                                txtType4.Text = "SA"
                            Case 1
                                txtType4.Text = "UC"
                            Case 2
                                txtType4.Text = "MO"
                            Case 3
                                txtType4.Text = "BN"
                        End Select
                        ' Scale 1 = 0     Scale 2 = 1     Scale Both = 2     Scale None = 3
                        Select Case .Item("ActiveScale")
                            Case 0
                                cboScale4.Text = "1"
                            Case 1
                                cboScale4.Text = "2"
                            Case 2
                                cboScale4.Text = "Both"
                            Case 3
                                cboScale4.Text = "None"
                        End Select
                        If Val(.Item("MaxTankTons")) > 0 Then
                            FillTank4.Maximum = Val(.Item("MaxTankTons")) '/ 1000
                        End If
                        If Val(.Item("CurrentLevel")) > 0 And Val(.Item("CurrentLevel")) < Val(.Item("MaxTankTons")) Then
                            FillTank4.Value = Val(.Item("CurrentLevel")) '/ 1000
                        Else
                            FillTank4.Value = 0
                        End If
                        ' ----- Set the bar for Low alarm value -----
                        LowValue = Val(.Item("LowAlarmValue")) '/ 1000
                        If FillTank1.Maximum <= 0 Or LowValue <= 0 Then
                            Tics = 0
                        Else
                            Tics = (FillTank1.Height / FillTank1.Maximum) * (LowValue)
                        End If
                        YValue = (FillTank4.Top + FillTank4.Height) - Tics
                        'lneAlarm4.Y1 = YValue
                        'lneAlarm4.Y2 = YValue
                        'lneAlarm4.Visible = True
                        lblAlarm4.Top = YValue - 10
                        lblAlarm4.Visible = True
                        If .Item("CurrentLevel") >= .Item("LowAlarmValue") Then
                            ' ----- Set background depending on whether over or under alarm value -----
                            txtMaxWeight4.BackColor = Color.White
                            txtTankId4.BackColor = Color.White
                            txtDescription4.BackColor = Color.White
                            txtLevel4.BackColor = Color.White
                        Else
                            txtMaxWeight4.BackColor = Color.Red
                            txtTankId4.BackColor = Color.Red
                            txtDescription4.BackColor = Color.Red
                            txtLevel4.BackColor = Color.Red
                        End If
                    End With

                    With SQL.SQLDataset.Tables(0).Rows(Index + 4)
                        cboCommodity5.Text = .Item("Commodity")
                        txtMaxWeight5.Text = Format(Val(.Item("MaxTankTons")), "##,###,##0.00")
                        txtLevel5.Text = Format(Val(.Item("CurrentLevel")), "##,###,##0.00")
                        'txtMaxWeight0.Text = .Item("MaxTankTons")
                        txtTankId5.Text = .Item("Id")
                        txtDescription5.Text = .Item("Description")
                        'txtLevel5.Text = .Item("CurrentLevel")
                        txtAlarm5.Text = .Item("LowAlarmValue")
                        If .Item("Active") = True Then
                            chkActive5.Checked = True
                        Else
                            chkActive5.Checked = False
                        End If
                        Select Case .Item("Type")
                            Case 0
                                txtType5.Text = "SA"
                            Case 1
                                txtType5.Text = "UC"
                            Case 2
                                txtType5.Text = "MO"
                            Case 3
                                txtType5.Text = "BN"
                        End Select
                        ' Scale 1 = 0     Scale 2 = 1     Scale Both = 2     Scale None = 3
                        Select Case .Item("ActiveScale")
                            Case 0
                                cboScale5.Text = "1"
                            Case 1
                                cboScale5.Text = "2"
                            Case 2
                                cboScale5.Text = "Both"
                            Case 3
                                cboScale5.Text = "None"
                        End Select
                        If Val(.Item("MaxTankTons")) > 0 Then
                            FillTank5.Maximum = Val(.Item("MaxTankTons")) '/ 1000
                        End If
                        If Val(.Item("CurrentLevel")) > 0 And Val(.Item("CurrentLevel")) < Val(.Item("MaxTankTons")) Then
                            FillTank5.Value = Val(.Item("CurrentLevel")) '/ 1000
                        Else
                            FillTank5.Value = 0
                        End If
                        ' ----- Set the bar for Low alarm value -----
                        LowValue = Val(.Item("LowAlarmValue")) '/ 1000
                        If FillTank1.Maximum <= 0 Or LowValue <= 0 Then
                            Tics = 0
                        Else
                            Tics = (FillTank1.Height / FillTank1.Maximum) * (LowValue)
                        End If
                        YValue = (FillTank5.Top + FillTank5.Height) - Tics
                        'lneAlarm5.Y1 = YValue
                        'lneAlarm5.Y2 = YValue
                        'lneAlarm5.Visible = True
                        lblAlarm5.Top = YValue - 10
                        lblAlarm5.Visible = True
                        If .Item("CurrentLevel") >= .Item("LowAlarmValue") Then
                            ' ----- Set background depending on whether over or under alarm value -----
                            txtMaxWeight5.BackColor = Color.White
                            txtTankId5.BackColor = Color.White
                            txtDescription5.BackColor = Color.White
                            txtLevel5.BackColor = Color.White
                        Else
                            txtMaxWeight5.BackColor = Color.Red
                            txtTankId5.BackColor = Color.Red
                            txtDescription5.BackColor = Color.Red
                            txtLevel5.BackColor = Color.Red
                        End If
                    End With

                ElseIf SQL.SQLDataset.HasErrors <> "" Then
                    MsgBox(SQL.SQLDataset.HasErrors)
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ClearTanks()

        Dim LoopCtr As Integer

        For LoopCtr = 0 To 3
            ' ----- Backgrounds set to white and no text -----
            txtMaxWeight4.BackColor = Color.White
            txtMaxWeight4.Text = ""
            txtTankId4.BackColor = Color.White
            txtTankId4.Text = ""
            txtDescription4.BackColor = Color.White
            txtDescription4.Text = ""
            txtLevel4.BackColor = Color.White
            txtLevel4.Text = ""
            txtAlarm4.BackColor = Color.White
            txtAlarm4.Text = ""
        Next LoopCtr

    End Sub

    Private Sub FillTank1_Click(sender As System.Object, e As System.EventArgs) Handles FillTank1.Click
        SelectedTank = 0 + Page
        frmTankMaint.ShowDialog()
        Me.Close()
    End Sub

    Private Sub FillTank2_Click(sender As System.Object, e As System.EventArgs) Handles FillTank2.Click
        SelectedTank = 1 + Page
        frmTankMaint.ShowDialog()
        Me.Close()
    End Sub

    Private Sub FillTank3_Click(sender As System.Object, e As System.EventArgs) Handles FillTank3.Click
        SelectedTank = 2 + Page
        frmTankMaint.ShowDialog()
        Me.Close()
    End Sub

    Private Sub FillTank4_Click(sender As System.Object, e As System.EventArgs) Handles FillTank4.Click
        SelectedTank = 3 + Page
        frmTankMaint.ShowDialog()
        Me.Close()
    End Sub

    Private Sub FillTank5_Click(sender As System.Object, e As System.EventArgs) Handles FillTank5.Click
        SelectedTank = 4 + Page
        frmTankMaint.ShowDialog()
        Me.Close()
    End Sub

    Private Sub cmdNext_Click(sender As System.Object, e As System.EventArgs) Handles cmdNext.Click
        GetTankInfo(5)
        Page = 5
        chkActive1.Enabled = False
        chkActive2.Enabled = False
        chkActive3.Enabled = False
        chkActive4.Enabled = False
        chkActive5.Enabled = False
        cmdEdit.Text = "Change Activity"
    End Sub

    Private Sub cmdPrevious_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrevious.Click
        GetTankInfo(0)
        Page = 0
        chkActive1.Enabled = False
        chkActive2.Enabled = False
        chkActive3.Enabled = False
        chkActive4.Enabled = False
        chkActive5.Enabled = False
        cmdEdit.Text = "Change Activity"
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        Dim ActiveTank As Integer
        Dim ScaleNumber As Integer
        Try


            If cmdEdit.Text = "Change Activity" Then
                cmdEdit.Text = "Update Activity"
                If Page = 0 Then
                    chkActive1.Enabled = True
                    chkActive2.Enabled = True
                    chkActive3.Enabled = True
                    chkActive4.Enabled = True
                    chkActive5.Enabled = True
                Else
                    chkActive1.Enabled = True
                    chkActive2.Enabled = True
                    chkActive3.Enabled = True
                    chkActive4.Enabled = True
                End If
            Else
                AddLogEntry(OperatorName & " Changing Tank Activity")
                'Save changes to activity to database
                If Page = 0 Then
                    'Tank 1
                    If chkActive1.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale1.Text = "1" Then
                        ScaleNumber = 0     'Scale 1
                    Else
                        ScaleNumber = 3     'None
                    End If

                    If frmMain.optStockton.Checked = True Then
                        SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "',  Active = '" & ActiveTank & "' where Id = '100'")
                    Else
                        SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "',  Active = '" & ActiveTank & "' where Id = '01'")
                    End If


                    'Tank 2
                    If chkActive2.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale2.Text = "1" Then
                        ScaleNumber = 0     'Scale 1
                    Else
                        ScaleNumber = 3     'None
                    End If

                    If frmMain.optStockton.Checked = True Then
                        SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "',  Active = '" & ActiveTank & "' where Id = '103'")
                        Exit Sub   'no more tanks to check
                    Else
                        SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "',  Active = '" & ActiveTank & "' where Id = '02'")
                    End If

                    'Tank 3
                    If chkActive3.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale3.Text = "2" Then
                        ScaleNumber = 1     'Scale 2
                    Else
                        ScaleNumber = 3     'None
                    End If
                    SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "', Active = '" & ActiveTank & "' where Id = '03'")

                    'Tank 4
                    If chkActive4.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale4.Text = "2" Then
                        ScaleNumber = 1     'Scale 2
                    Else
                        ScaleNumber = 3     'None
                    End If
                    SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "', Active = '" & ActiveTank & "' where Id = '04'")

                    'Tank 5
                    If chkActive5.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale5.Text = "1" Then
                        ScaleNumber = 0     'Scale 1
                    Else
                        ScaleNumber = 3     'None
                    End If
                    SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "', Active = '" & ActiveTank & "' where Id = '05'")

                Else
                    'Tank 11
                    If chkActive1.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale1.Text = "1" Then
                        ScaleNumber = 0     'Scale 1
                    Else
                        ScaleNumber = 3     'None
                    End If
                    SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "', Active = '" & ActiveTank & "' where Id = '11'")

                    'Tank 12
                    If chkActive2.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale2.Text = "1" Then
                        ScaleNumber = 0     'Scale 1
                    Else
                        ScaleNumber = 3     'None
                    End If
                    SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "', Active = '" & ActiveTank & "' where Id = '12'")

                    'Tank 13
                    If chkActive3.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale3.Text = "1" Then
                        ScaleNumber = 0     'Scale 1
                    Else
                        ScaleNumber = 3     'None
                    End If
                    SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "', Active = '" & ActiveTank & "' where Id = '13'")

                    'Tank 14
                    If chkActive4.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale4.Text = "1" Then
                        ScaleNumber = 0     'Scale 1
                    Else
                        ScaleNumber = 3     'None
                    End If
                    SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "', Active = '" & ActiveTank & "' where Id = '14'")

                    'Tank 15
                    If chkActive5.Checked Then
                        ActiveTank = 1
                    Else
                        ActiveTank = 0
                    End If
                    If cboScale5.Text = "1" Then
                        ScaleNumber = 0     'Scale 1
                    Else
                        ScaleNumber = 3     'None
                    End If
                    SQL.RunQuery("Update tank set ActiveScale = '" & ScaleNumber & "', Active = '" & ActiveTank & "' where Id = '15'")
                End If
                cmdEdit.Text = "Change Activity"
                chkActive1.Enabled = False
                chkActive2.Enabled = False
                chkActive3.Enabled = False
                chkActive4.Enabled = False
                chkActive5.Enabled = False
            End If

        Catch ex As Exception
            AddLogEntry("TankInv cmdEdit: " & ex.Message)
        End Try
    End Sub

    Private Sub chkActive1_Click(sender As Object, e As EventArgs) Handles chkActive1.Click
        If Page = 0 Then
            If chkActive1.Checked Then
                chkActive2.Checked = False
                chkActive5.Checked = False
                cboScale1.Text = "1"
                cboScale2.Text = "None"
                cboScale5.Text = "None"
            Else
                cboScale1.Text = "None"
            End If
        Else
            If chkActive1.Checked Then
                chkActive2.Checked = False
                chkActive3.Checked = False
                chkActive4.Checked = False
                cboScale1.Text = "1"
                cboScale2.Text = "None"
                cboScale3.Text = "None"
                cboScale4.Text = "None"
            Else
                cboScale1.Text = "None"
            End If
        End If
    End Sub

    Private Sub chkActive2_Click(sender As Object, e As EventArgs) Handles chkActive2.Click
        If Page = 0 Then
            If chkActive2.Checked Then
                chkActive1.Checked = False
                chkActive5.Checked = False
                cboScale2.Text = "1"
                cboScale1.Text = "None"
                cboScale5.Text = "None"
            Else
                cboScale2.Text = "None"
            End If
        Else
            If chkActive2.Checked Then
                chkActive1.Checked = False
                chkActive3.Checked = False
                chkActive4.Checked = False
                cboScale2.Text = "1"
                cboScale1.Text = "None"
                cboScale3.Text = "None"
                cboScale4.Text = "None"
            Else
                cboScale2.Text = "None"
            End If
        End If
    End Sub

    Private Sub chkActive3_Click(sender As Object, e As EventArgs) Handles chkActive3.Click
        If Page = 0 Then
            If chkActive3.Checked Then
                cboScale3.Text = "2"
            Else
                cboScale3.Text = "None"
            End If
        Else
            If chkActive3.Checked Then
                chkActive1.Checked = False
                chkActive2.Checked = False
                chkActive4.Checked = False
                cboScale3.Text = "1"
                cboScale1.Text = "None"
                cboScale2.Text = "None"
                cboScale4.Text = "None"
            Else
                cboScale3.Text = "None"
            End If
        End If
    End Sub

    Private Sub chkActive4_Click(sender As Object, e As EventArgs) Handles chkActive4.Click
        If Page = 0 Then
            If chkActive4.Checked Then
                cboScale4.Text = "2"
            Else
                cboScale4.Text = "None"
            End If
        Else
            If chkActive4.Checked Then
                chkActive1.Checked = False
                chkActive2.Checked = False
                chkActive3.Checked = False
                cboScale4.Text = "1"
                cboScale1.Text = "None"
                cboScale2.Text = "None"
                cboScale3.Text = "None"
            Else
                cboScale4.Text = "None"
            End If
        End If
    End Sub

    Private Sub chkActive5_Click(sender As Object, e As EventArgs) Handles chkActive5.Click
        If Page = 0 Then
            If chkActive5.Checked Then
                chkActive1.Checked = False
                chkActive2.Checked = False
                cboScale5.Text = "1"
                cboScale1.Text = "None"
                cboScale2.Text = "None"
            Else
                cboScale5.Text = "None"
            End If
        End If
    End Sub

End Class