Public Class frmDriverMaint
    Dim CurrentRecord As Integer

    Private SQL As New SQLControl

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

   
    Private Sub frmDriverMaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
        If FileControl = 1 Then
            cmdAdd.Visible = True
            cmdDelete.Visible = False
            cmdEdit.Visible = True
        End If
        If FileControl = 2 Then
            cmdAdd.Visible = True
            cmdDelete.Visible = True
            cmdEdit.Visible = True
        End If
        'ComboFill()
        cmdAdd.Text = "Add"
        cmdEdit.Text = "Edit"
        cmdFirst.PerformClick()
    End Sub

    Private Sub ComboFill()
        'Carrier
        cboCarrier.Items.Clear()
        SQL.RunQuery("SELECT * FROM Carrier")
        If SQL.SQLDataset.Tables.Count > 0 Then
            For Each r As DataRow In SQL.SQLDataset.Tables(0).Rows
                cboCarrier.Items.Add(r("Code"))
            Next
        ElseIf SQL.SQLDataset.HasErrors <> "" Then
            MsgBox(SQL.SQLDataset.HasErrors)
        End If
    End Sub

    Private Sub cmdFirst_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirst.Click
        'Try
        SQL.RunQuery("Select * from Driver")
        'FillBoxes
        CurrentRecord = 0
        With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
            txtID.Text = .Item("Id")
            txtName.Text = .Item("Name")
            txtCardId.Text = .Item("CardId")
            txtPIN.Text = .Item("PIN")
            cboCarrier.Text = .Item("Carrier")
            txtTwic.Text = .Item("Twic")
            txtWarning.Text = .Item("Warning") & ""
            txtTraining.Text = .Item("Training") & ""
            txtExpires.Text = .Item("Expires") & ""
            Dim Active As Integer = .Item("Active")
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
        'Catch ex As Exception
        'MsgBox(ex.Message)
        'End Try
    End Sub



    Private Sub cmdPrevious_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrevious.Click

        Try
            cmdNext.Enabled = True
            If CurrentRecord <= 0 Then
                cmdPrevious.Enabled = False
                Exit Sub
            End If
            CurrentRecord = CurrentRecord - 1
            'FillBoxes
              With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtID.Text = .Item("Id")
                txtName.Text = .Item("Name")
                txtCardId.Text = .Item("CardId")
                txtPIN.Text = .Item("PIN")
                cboCarrier.Text = .Item("Carrier")
                txtTwic.Text = .Item("Twic")
                txtWarning.Text = .Item("Warning")
                txtTraining.Text = .Item("Training")
                txtExpires.Text = .Item("Expires")
                Dim Active As Integer = .Item("Active")
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
        Try
            cmdPrevious.Enabled = True
            CurrentRecord = CurrentRecord + 1
            'FillBoxes
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtID.Text = .Item("Id")
                txtName.Text = .Item("Name")
                txtCardId.Text = .Item("CardId")
                txtPIN.Text = .Item("PIN")
                cboCarrier.Text = .Item("Carrier")
                txtTwic.Text = .Item("Twic")
                txtWarning.Text = .Item("Warning")
                txtTraining.Text = .Item("Training")
                txtExpires.Text = .Item("Expires")
                Dim Active As Integer = .Item("Active")
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

        If cmdEdit.Text = "Edit" Then
            cmdEdit.Text = "Update"
            'txtID.Enabled = True
            txtName.Enabled = True
            txtCardId.Enabled = True
            txtPIN.Enabled = True
            cboCarrier.Enabled = True
            ComboFill()

            txtTwic.Enabled = True
            txtWarning.Enabled = True
            txtTraining.Enabled = True
            txtExpires.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
        Else
            cmdEdit.Text = "Edit"
            If optYes.Checked = True Then
                Active = 1
            Else
                Active = 0
            End If
            Dim UpdateCmd As String = "Update Driver " &
                                          "Set Id ='" & txtID.Text & "' , " &
                                          "Name ='" & txtName.Text & "' , " &
                                          "CardId ='" & txtCardId.Text & "', " &
                                          "PIN ='" & txtPIN.Text & "', " &
                                          "Carrier ='" & cboCarrier.Text & "', " &
                                          "Twic ='" & txtTwic.Text & "', " &
                                          "Warning ='" & txtWarning.Text & "', " &
                                          "Training ='" & txtTraining.Text & "', " &
                                          "Expires ='" & txtExpires.Text & "', " &
                                          "Active ='" & Active & "' " &
                                          "Where Id = '" & txtID.Text & "';"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                MsgBox("Error updating Driver Record")
                'Stop
            Else
                MsgBox("Driver Record Updated")
                'Stop
            End If
            txtID.Enabled = False
            txtName.Enabled = False
            txtCardId.Enabled = False
            txtPIN.Enabled = False
            cboCarrier.Enabled = False
            txtTwic.Enabled = False
            txtWarning.Enabled = False
            txtTraining.Enabled = False
            txtExpires.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False

            AddLogEntry("Driver Record " & txtID.Text & " was edited ")
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim Active As Integer

        If cmdAdd.Text = "Add" Then
            cmdAdd.Text = "Confirm"
            txtID.Enabled = True
            txtID.Text = ""
            txtName.Enabled = True
            txtCardId.Enabled = True
            txtPIN.Enabled = True
            cboCarrier.Enabled = True
            txtTwic.Enabled = True
            txtWarning.Enabled = True
            txtTraining.Enabled = True
            txtExpires.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            ComboFill()

            txtID.Focus()
        Else
            cmdAdd.Text = "Add"
            Try
                If optYes.Checked = True Then
                    Active = 1
                Else
                    Active = 0
                End If
                Dim strInsert As String = "INSERT INTO Driver (Id, Name, CardId, PIN, Carrier, Twic, Warning, Training, Expires, Active) " &
                                             "VALUES (" &
                                             "'" & txtID.Text & "' , " &
                                             "'" & txtName.Text & "' , " &
                                              "'" & txtCardId.Text & "', " &
                                              "'" & txtPIN.Text & "', " &
                                              "'" & cboCarrier.Text & "', " &
                                              "'" & txtTwic.Text & "', " &
                                              "'" & txtWarning.Text & "', " &
                                              "'" & txtTraining.Text & "', " &
                                              "'" & txtExpires.Text & "', " &
                                             "'" & Active & "') "
                SQL.DataUpdate(strInsert)
                'AddRecord = True
            Catch ex As Exception
                'AddRecord = False
            End Try
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelete.Click
        Try
            SQL.RunQuery("Select * from Driver where Id = '" & txtID.Text & "' ")
            If SQL.RecordCount = 0 Then
                Exit Sub
            Else
                'Delete record
                SQL.DataUpdate("DELETE FROM Driver where Id ='" & txtID.Text & "' ")
                MsgBox("Driver Record Deleted")
                cmdClear.PerformClick()
            End If
        Catch ex As Exception
            AddLogEntry("DeleteDriver: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtID.Text = ""
        txtName.Text = ""
        txtCardId.Text = ""
        txtPIN.Text = ""
        cboCarrier.Text = ""
        txtTwic.Text = ""
        txtWarning.Text = ""
        txtTraining.Text = ""
        txtExpires.Text = ""
       
    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click

        If txtID.Enabled = False Then
            'Enable all
            txtID.Enabled = True
            txtName.Enabled = True
            txtCardId.Enabled = True
            txtPIN.Enabled = True
            cboCarrier.Enabled = True
            txtTwic.Enabled = True
            txtWarning.Enabled = True
            txtTraining.Enabled = True
            txtExpires.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            'Now clear them
              txtID.Text = ""
            txtName.Text = ""
            txtCardId.Text = ""
            txtPIN.Text = ""
            cboCarrier.Text = ""
            txtTwic.Text = ""
            txtWarning.Text = ""
            txtTraining.Text = ""
            txtExpires.Text = ""
            Exit Sub
        End If
        'Check each field to see if data exists 
        'then create a Query to search the database
        Dim Query As String = ""
        If txtID.Text <> "" Then
            Query = "Id ='" & txtID.Text & "'"
        End If
        If txtName.Text <> "" Then
            Query = " Name ='" & txtName.Text & "'"
        End If
        If txtCardId.Text <> "" Then
            Query = " CardId ='" & txtCardId.Text & "'"
        End If

        If txtPIN.Text <> "" Then
            Query = " PIN ='" & txtPIN.Text & "'"
        End If
        Try
            SQL.RunQuery("Select * from Driver where " & Query)
            If SQL.RecordCount = 0 Then
                MsgBox("No Record Found")
            Else
                With SQL.SQLDataset.Tables(0).Rows(0)
                    txtID.Text = .Item("Id")
                    txtName.Text = .Item("Name")
                    txtCardId.Text = .Item("CardId")
                    txtPIN.Text = .Item("PIN")
                    cboCarrier.Text = .Item("Carrier")
                    txtTwic.Text = .Item("Twic")
                    txtWarning.Text = .Item("Warning")
                    txtTraining.Text = .Item("Training")
                    txtExpires.Text = .Item("Expires")
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
            txtName.Enabled = False
            txtCardId.Enabled = False
            txtPIN.Enabled = False
            cboCarrier.Enabled = False
            txtTwic.Enabled = False
            txtWarning.Enabled = False
            txtTraining.Enabled = False
            txtExpires.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False
        Catch ex As Exception
            AddLogEntry("FindDriver: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdGrid_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrid.Click
        frmDriver.ShowDialog()
        LoadTimer.Enabled = True
    End Sub
End Class