Public Class frmCarrierMaint
    Dim CurrentRecord As Integer

    Private SQL As New SQLControl

    Private Sub frmCarrierMaint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            SQL.RunQuery("Select * from Carrier")
            'FillBoxes
            CurrentRecord = 0
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtCode.Text = .Item("Code")
                txtName.Text = .Item("Name")
                txtAddress.Text = .Item("Address")
                txtCSZ.Text = .Item("CSZ")
                txtMaxLoad.Text = .Item("MaxLoad")

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
            cmdNext.Enabled = True
            cmdPrevious.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
                txtCode.Text = .Item("Code")
                txtName.Text = .Item("Name") & ""
                txtAddress.Text = .Item("Address") & ""
                txtCSZ.Text = .Item("CSZ") & ""
                txtMaxLoad.Text = .Item("MaxLoad") & ""
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
                txtCode.Text = .Item("Code")
                txtName.Text = .Item("Name") & ""
                txtAddress.Text = .Item("Address") & ""
                txtCSZ.Text = .Item("CSZ") & ""
                txtMaxLoad.Text = .Item("MaxLoad") & ""
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
                'Stop
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
            txtCode.Enabled = True
            txtName.Enabled = True
            txtAddress.Enabled = True
            txtCSZ.Enabled = True
            txtMaxLoad.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
        Else
            cmdEdit.Text = "Edit"
            If optYes.Checked = True Then
                Active = 1
            Else
                Active = 0
            End If

            Dim UpdateCmd As String = "Update Carrier " &
                                          "Set Code ='" & txtCode.Text & "' , " &
                                          "Name ='" & txtName.Text & "' , " &
                                          "Address ='" & txtAddress.Text & "', " &
                                          "CSZ ='" & txtCSZ.Text & "', " &
                                          "MaxLoad ='" & txtMaxLoad.Text & "', " &
                                          "Active ='" & Active & "' " &
                                          "Where Code = '" & txtCode.Text & "';"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                MsgBox("Error updating Carrier Record")
                'Stop
            Else
                MsgBox("Carrier Record Updated")
                'Stop
            End If
            txtCode.Enabled = False
            txtName.Enabled = False
            txtAddress.Enabled = False
            txtCSZ.Enabled = False
            txtMaxLoad.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False

            AddLogEntry("Carrier Record " & txtName.Text & " was edited ")
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim Active As Integer

        If cmdAdd.Text = "Add" Then
            cmdAdd.Text = "Confirm"
            txtCode.Enabled = True
            txtName.Enabled = True
            txtAddress.Enabled = True
            txtCSZ.Enabled = True
            txtMaxLoad.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            txtCode.Text = ""
            txtCode.Focus()
        Else

            Try
                If optYes.Checked = True Then
                    Active = 1
                Else
                    Active = 0
                End If

                Dim strInsert As String = "INSERT INTO Carrier (Code, Name, Address, CSZ, MaxLoad, Active) " &
                                             "VALUES (" &
                                             "'" & txtCode.Text & "', " &
                                             "'" & txtName.Text & "'," &
                                              "'" & txtAddress.Text & "' , " &
                                              "'" & txtCSZ.Text & "', " &
                                              "'" & txtMaxLoad.Text & "', " &
                                              "'" & Active & "') "
                SQL.DataUpdate(strInsert)
                txtCode.Enabled = False
                txtName.Enabled = False
                txtAddress.Enabled = False
                txtCSZ.Enabled = False
                txtMaxLoad.Enabled = False

                optYes.Enabled = False
                optNo.Enabled = False

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelete.Click
        Try
            SQL.RunQuery("Select * from Carrier where Release = '" & txtName.Text & "' ")
            If SQL.RecordCount = 0 Then

                'Exit Sub
            Else
                'Delete record
                SQL.DataUpdate("DELETE FROM Carrier where Release ='" & txtName.Text & "' ")
                MsgBox("Carrier Record Deleted")
                cmdClear.PerformClick()
            End If
        Catch ex As Exception
            AddLogEntry("DeleteCarrier: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtCode.Text = ""
        txtName.Text = ""
        txtAddress.Text = ""
        txtCSZ.Text = ""
        txtMaxLoad.Text = ""

    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click

        If txtName.Enabled = False Then
            'Enable all
            txtCode.Enabled = True
            txtName.Enabled = True
            txtAddress.Enabled = True
            txtCSZ.Enabled = True
            txtMaxLoad.Enabled = True

            optYes.Enabled = True
            optNo.Enabled = True
            'Now clear them
            txtCode.Text = ""
            txtName.Text = ""
            txtAddress.Text = ""
            txtCSZ.Text = ""
            txtMaxLoad.Text = ""

            Exit Sub
        End If
        'Check each field to see if data exists 
        'then create a Query to search the database
        Dim Query As String = ""
        If txtCode.Text <> "" Then
            Query = "Code ='" & txtCode.Text & "'"
        End If
        If txtName.Text <> "" Then
            Query = ", Name ='" & txtName.Text & "'"
        End If
        If txtAddress.Text <> "" Then
            Query = ", Address ='" & txtAddress.Text & "'"
        End If

        If txtMaxLoad.Text <> "" Then
            Query = ", MaxLoad ='" & txtMaxLoad.Text & "'"
        End If
        Try
            SQL.RunQuery("Select * from Carrier where " & Query)
            If SQL.RecordCount = 0 Then
                MsgBox("No Record Found")
            Else
                With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                    txtCode.Text = .Item("Code")
                    txtName.Text = .Item("Name")
                    txtAddress.Text = .Item("Address")
                    txtCSZ.Text = .Item("CSZ")
                    txtMaxLoad.Text = .Item("MaxLoad")
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
            txtCode.Enabled = False
            txtName.Enabled = False
            txtAddress.Enabled = False
            txtCSZ.Enabled = False
            txtMaxLoad.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False
        Catch ex As Exception
            AddLogEntry("FindCarrier: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdGrid_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrid.Click
        frmCarrierGrid.ShowDialog()
        LoadTimer.Enabled = True
    End Sub
End Class