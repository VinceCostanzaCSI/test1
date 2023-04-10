Public Class frmBrenntagMaint
    Dim CurrentRecord As Integer

    Private SQL As New SQLControl

    Private Sub frmBrenntagMaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        LoadTimer.Enabled = True
    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        LoadTimer.Enabled = False

        cmdAdd.Text = "Add"
        cmdEdit.Text = "Edit"
        cmdFirst.PerformClick()
    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub cmdFirst_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirst.Click
        Try
            SQL.RunQuery("Select * from Brenntag")
            'FillBoxes
            CurrentRecord = 0
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtRelease.Text = .Item("Release")
                txtBOL.Text = .Item("BOL")
                txtPO.Text = .Item("PO")
                txtAltPO.Text = .Item("AltPO")
                txtAltCode.Text = .Item("AltCode")
                txtName.Text = .Item("Name")
                txtAddress1.Text = .Item("Address1")
                txtAddress2.Text = .Item("Address2")
                txtCSZ.Text = .Item("CSZ")
                txtMaxLoad.Text = .Item("MaxLoad")
                txtNotes.Text = .Item("Notes")
                txtPCName.Text = .Item("PCName")
                txtPC.Text = .Item("PC")
                If .Item("Active") = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                    cmdDelete.Enabled = False
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                    cmdDelete.Enabled = True
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
                txtRelease.Text = .Item("Release")
                txtBOL.Text = .Item("BOL")
                txtPO.Text = .Item("PO")
                txtAltPO.Text = .Item("AltPO")
                txtAltCode.Text = .Item("AltCode")
                txtName.Text = .Item("Name")
                txtAddress1.Text = .Item("Address1")
                txtAddress2.Text = .Item("Address2")
                txtCSZ.Text = .Item("CSZ")
                txtMaxLoad.Text = .Item("MaxLoad")
                txtNotes.Text = .Item("Notes")
                txtPCName.Text = .Item("PCName")
                txtPC.Text = .Item("PC")
                Active = .Item("Active")
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                    cmdDelete.Enabled = False
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                    cmdDelete.Enabled = True
                End If
                'Stop
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
                txtRelease.Text = .Item("Release")
                txtBOL.Text = .Item("BOL")
                txtPO.Text = .Item("PO")
                txtAltPO.Text = .Item("AltPO")
                txtAltCode.Text = .Item("AltCode")
                txtName.Text = .Item("Name")
                txtAddress1.Text = .Item("Address1")
                txtAddress2.Text = .Item("Address2")
                txtCSZ.Text = .Item("CSZ")
                txtMaxLoad.Text = .Item("MaxLoad")
                txtNotes.Text = .Item("Notes")
                txtPCName.Text = .Item("PCName")
                txtPC.Text = .Item("PC")
                Active = .Item("Active")
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                    cmdDelete.Enabled = False
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                    cmdDelete.Enabled = True
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
            txtRelease.Enabled = True
            txtBOL.Enabled = True
            txtPO.Enabled = True
            txtAltPO.Enabled = True
            txtAltCode.Enabled = True
            txtName.Enabled = True
            txtAddress1.Enabled = True
            txtAddress2.Enabled = True
            txtCSZ.Enabled = True
            txtMaxLoad.Enabled = True
            txtPCName.Enabled = True
            txtPC.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
        Else
            cmdEdit.Text = "Edit"
            If optYes.Checked = True Then
                Active = 1
            Else
                Active = 0
            End If
            Dim UpdateCmd As String = "Update Brenntag " &
                                          "Set Release ='" & txtRelease.Text & "' , " &
                                          "BOL ='" & txtBOL.Text & "' , " &
                                          "PO ='" & txtPO.Text & "', " &
                                          "AltPO ='" & txtAltPO.Text & "', " &
                                          "AltCode ='" & txtAltCode.Text & "', " &
                                          "Name ='" & txtName.Text & "', " &
                                          "Address1 ='" & txtAddress1.Text & "', " &
                                          "Address2 ='" & txtAddress2.Text & "', " &
                                          "CSZ ='" & txtCSZ.Text & "', " &
                                          "MaxLoad ='" & txtMaxLoad.Text & "', " &
                                          "Notes ='" & txtNotes.Text & "', " &
                                          "PCName ='" & txtPCName.Text & "', " &
                                          "PC ='" & txtPC.Text & "', " &
                                          "Active ='" & Active & "' " &
                                          "Where Release = '" & txtRelease.Text & "';"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                MsgBox("Error updating Brenntag Record")
                'Stop
            Else
                MsgBox("Brenntag Record Updated")
                'Stop
            End If
            txtRelease.Enabled = False
            txtBOL.Enabled = False
            txtPO.Enabled = False
            txtAltPO.Enabled = False
            txtAltCode.Enabled = False
            txtName.Enabled = False
            txtAddress1.Enabled = False
            txtAddress2.Enabled = False
            txtCSZ.Enabled = False
            txtMaxLoad.Enabled = False
            txtPCName.Enabled = False
            txtPC.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False

            AddLogEntry("Brenntag Record " & txtRelease.Text & " was edited ")
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click

        If cmdAdd.Text = "Add" Then
            cmdAdd.Text = "Confirm"
            txtRelease.Enabled = True
            txtRelease.Text = ""
            txtBOL.Enabled = True
            txtPO.Enabled = True
            txtAltPO.Enabled = True
            txtAltCode.Enabled = True
            txtName.Enabled = True
            txtAddress1.Enabled = True
            txtAddress2.Enabled = True
            txtCSZ.Enabled = True
            txtMaxLoad.Enabled = True
            txtPCName.Enabled = True
            txtPC.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            txtRelease.Focus()
        Else
            cmdAdd.Text = "Add"

            Dim Active As Integer
            Try
                If optYes.Checked = True Then
                    Active = 1
                Else
                    Active = 0
                End If
                Dim strInsert As String = "INSERT INTO Brenntag (Release, PO, BOL, AltPO, AltCode, Name, Address1, Address2, CSZ, MaxLoad, Notes, PC, Active) " &
                                             "VALUES (" &
                                             "'" & txtRelease.Text & "'," &
                                             "'" & txtPO.Text & "'," &
                                             "'" & txtBOL.Text & "'," &
                                             "'" & txtAltPO.Text & "'," &
                                             "'" & txtAltCode.Text & "'," &
                                             "'" & txtName.Text & "'," &
                                             "'" & txtAddress1.Text & "'," &
                                             "'" & txtAddress2.Text & "'," &
                                             "'" & txtCSZ.Text & "'," &
                                             "'" & txtMaxLoad.Text & "'," &
                                             "'" & txtNotes.Text & "'," &
                                             "'" & txtPCName.Text & "'," &
                                             "'" & txtPC.Text & "'," &
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
            SQL.RunQuery("Select * from Brenntag where Release = '" & txtRelease.Text & "' ")
            If SQL.RecordCount = 0 Then
                MsgBox("Brenntag record not found")
            Else
                'Delete record if Active
                If SQL.SQLDataset.Tables(0).Rows(0).Item("Active") Then
                    SQL.DataUpdate("DELETE FROM Brenntag where Release ='" & txtRelease.Text & "' ")
                    MsgBox("Brenntag Record Deleted")
                    cmdClear.PerformClick()
                Else
                    MsgBox("Cannot delete Brenntag order after it has been loaded")
                End If
            End If

        Catch ex As Exception
            AddLogEntry("DeleteBrenntag: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtRelease.Text = ""
        txtBOL.Text = ""
        txtPO.Text = ""
        txtAltPO.Text = ""
        txtAltCode.Text = ""
        txtName.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtCSZ.Text = ""
        txtMaxLoad.Text = ""
        txtNotes.Text = ""
        txtPCName.Text = ""
        txtPC.Text = ""
    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click

        If txtRelease.Enabled = False Then
            'Enable all
            txtRelease.Enabled = True
            txtBOL.Enabled = True
            txtPO.Enabled = True
            txtAltPO.Enabled = True
            txtAltCode.Enabled = True
            txtName.Enabled = True
            txtAddress1.Enabled = True
            txtAddress2.Enabled = True
            txtCSZ.Enabled = True
            txtMaxLoad.Enabled = True
            txtPCName.Enabled = True
            txtPC.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            'Now clear them
            txtRelease.Text = ""
            txtBOL.Text = ""
            txtPO.Text = ""
            txtAltPO.Text = ""
            txtAltCode.Text = ""
            txtName.Text = ""
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            txtCSZ.Text = ""
            txtMaxLoad.Text = ""
            txtNotes.Text = ""
            txtPCName.Text = ""
            txtPC.Text = ""
            Exit Sub
        End If
        'Check each field to see if data exists 
        'then create a Query to search the database
        Dim Query As String = ""
        If txtRelease.Text <> "" Then
            Query = "Release ='" & txtRelease.Text & "'"
        End If
        If txtBOL.Text <> "" Then
            Query = ", BOL ='" & txtBOL.Text & "'"
        End If
        If txtPO.Text <> "" Then
            Query = ", PO ='" & txtPO.Text & "'"
        End If
        
        If txtName.Text <> "" Then
            Query = ", Name ='" & txtName.Text & "'"
        End If
        Try
            SQL.RunQuery("Select * from Brenntag where " & Query)
            If SQL.RecordCount = 0 Then
                MsgBox("No Record Found")
            Else
                With SQL.SQLDataset.Tables(0).Rows(0)
                    txtRelease.Text = .Item("Release")
                    txtBOL.Text = .Item("BOL")
                    txtPO.Text = .Item("PO")
                    txtAltPO.Text = .Item("AltPO")
                    txtAltCode.Text = .Item("AltCode")
                    txtName.Text = .Item("Name")
                    txtAddress1.Text = .Item("Address1")
                    txtAddress2.Text = .Item("Address2")
                    txtCSZ.Text = .Item("CSZ")
                    txtMaxLoad.Text = .Item("MaxLoad")
                    txtNotes.Text = .Item("Notes")
                    txtPCName.Text = .Item("PCName")
                    txtPC.Text = .Item("PC")
                    Dim Active As Integer = .Item("Active")
                    If Active = 0 Then
                        optYes.Checked = False
                        optNo.Checked = True
                        cmdDelete.Enabled = False
                    Else
                        optYes.Checked = True
                        optNo.Checked = False
                        cmdDelete.Enabled = True
                    End If
                End With
            End If
            txtRelease.Enabled = False
            txtBOL.Enabled = False
            txtPO.Enabled = False
            txtAltPO.Enabled = False
            txtAltCode.Enabled = False
            txtName.Enabled = False
            txtAddress1.Enabled = False
            txtAddress2.Enabled = False
            txtCSZ.Enabled = False
            txtMaxLoad.Enabled = False
            txtPCName.Enabled = False
            txtPC.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False
        Catch ex As Exception
            AddLogEntry("FindBrenntag: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdGrid_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrid.Click
        frmBrenntagGrid.ShowDialog()
        LoadTimer.Enabled = True
    End Sub
End Class