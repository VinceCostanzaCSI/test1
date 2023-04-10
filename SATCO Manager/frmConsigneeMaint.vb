Public Class frmConsigneeMaint
    Dim CurrentRecord As Integer

    Private SQL As New SQLControl

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub
   
    Private Sub frmConsigneeMaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        LoadTimer.Enabled = True
    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        LoadTimer.Enabled = False
        If FileControl = 0 Or FileControl = 1 Then
            cmdAdd.Visible = True
            cmdDelete.Visible = False
            cmdEdit.Visible = True
        End If
        If FileControl = 2 Then
            cmdAdd.Visible = True
            cmdDelete.Visible = True
            cmdEdit.Visible = True
        End If
        cmdAdd.Text = "Add"
        cmdEdit.Text = "Edit"
        cmdFirst.PerformClick()
    End Sub

    Private Sub cmdFirst_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirst.Click
        Try

            If SelectedConsignee = "" Then
                SQL.RunQuery("Select * From Consignee")
            Else
                SQL.RunQuery("Select * From Consignee where Code like '" & SelectedConsignee & "%'")
            End If
            'FillBoxes
            CurrentRecord = 0
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtCode.Text = .Item("Code")
                txtConsignee.Text = .Item("Consignee")
                txtNextTransNumber.Text = .Item("NextTransNumber")
                txtDestination.Text = .Item("Destination")
                txtDestination2.Text = .Item("Destination2")
                txtLimit.Text = .Item("Limit")
                txtUsed.Text = .Item("Used") & ""
                Dim Active As Integer = .Item("Active")
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                End If
                Dim NSF As Integer = .Item("NSF")
                If NSF = 0 Then
                    chkNSF.Checked = False
                Else
                    chkNSF.Checked = True
                End If
                Dim Release As Integer = .Item("Release")
                If Release = 0 Then
                    chkRelease.Checked = False
                Else
                    chkRelease.Checked = True
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
                txtConsignee.Text = .Item("Consignee")
                txtNextTransNumber.Text = .Item("NextTransNumber")
                txtDestination.Text = .Item("Destination")
                txtDestination2.Text = .Item("Destination2")
                txtLimit.Text = .Item("Limit")
                txtUsed.Text = .Item("Used") & ""
                Dim Active As Integer = .Item("Active")
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                End If
                Dim NSF As Integer = .Item("NSF")
                If NSF = 0 Then
                    chkNSF.Checked = False
                Else
                    chkNSF.Checked = True
                End If
                Dim Release As Integer = .Item("Release")
                If Release = 0 Then
                    chkRelease.Checked = False
                Else
                    chkRelease.Checked = True
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
                txtConsignee.Text = .Item("Consignee")
                txtNextTransNumber.Text = .Item("NextTransNumber")
                txtDestination.Text = .Item("Destination")
                txtDestination2.Text = .Item("Destination2")
                txtLimit.Text = .Item("Limit")
                txtUsed.Text = .Item("Used") & ""
                Dim Active As Integer = .Item("Active")
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                Else
                    optYes.Checked = True
                    optNo.Checked = False
                End If
                Dim NSF As Integer = .Item("NSF")
                If NSF = 0 Then
                    chkNSF.Checked = False
                Else
                    chkNSF.Checked = True
                End If
                Dim Release As Integer = .Item("Release")
                If Release = 0 Then
                    chkRelease.Checked = False
                Else
                    chkRelease.Checked = True
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
            txtConsignee.Enabled = True
            txtNextTransNumber.Enabled = True
            txtDestination.Enabled = True
            txtDestination2.Enabled = True
            txtLimit.Enabled = True
            txtUsed.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            chkNSF.Enabled = True
            chkRelease.Enabled = True
        Else
            cmdEdit.Text = "Edit"
            If optYes.Checked = True Then
                Active = 1
            Else
                Active = 0
            End If
            Dim NSF As Integer
            If chkNSF.Checked = True Then
                NSF = 1
            Else
                NSF = 0
            End If
            Dim Release As Integer
            If chkRelease.Checked = True Then
                Release = 1
            Else
                Release = 0
            End If
            txtConsignee.Text = Replace(txtConsignee.Text, "'", "")
            Dim UpdateCmd As String = "Update Consignee " & _
                                          "Set Code ='" & txtCode.Text & "' , " & _
                                          "Consignee='" & txtConsignee.Text & "' , " & _
                                          "NextTransNumber ='" & txtNextTransNumber.Text & "', " & _
                                          "Destination ='" & txtDestination.Text & "', " & _
                                          "Destination2 ='" & txtDestination2.Text & "', " & _
                                          "Limit ='" & txtLimit.Text & "', " & _
                                          "Used ='" & txtUsed.Text & "', " & _
                                          "NSF ='" & NSF & "', " & _
                                          "Release ='" & Release & "', " & _
                                          "Active ='" & Active & "' " & _
                                          "Where Code = '" & txtCode.Text & "';"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                MsgBox("Error updating Consignee Record")
                'Stop
            Else
                MsgBox("Consignee Record Updated")
                'Stop
            End If
            txtCode.Enabled = False
            txtConsignee.Enabled = False
            txtNextTransNumber.Enabled = False
            txtDestination.Enabled = False
            txtDestination2.Enabled = False
            txtLimit.Enabled = False
            txtUsed.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False
            chkNSF.Enabled = False
            chkRelease.Enabled = False

            AddLogEntry("Consignee Record " & txtCode.Text & " was edited ")
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim Active As Integer
        Dim NSF As Integer
        Dim Release As Integer

        If cmdAdd.Text = "Add" Then
            cmdAdd.Text = "Confirm"
            txtCode.Enabled = True
            txtConsignee.Enabled = True
            txtNextTransNumber.Enabled = True
            txtDestination.Enabled = True
            txtDestination2.Enabled = True
            txtLimit.Enabled = True
            txtUsed.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            chkNSF.Enabled = True
            chkRelease.Enabled = True
            txtCode.Text = ""
            txtCode.Focus()
        Else

            Try
                If optYes.Checked = True Then
                    Active = 1
                Else
                    Active = 0
                End If

                If chkNSF.Checked = True Then
                    NSF = 1
                Else
                    NSF = 0
                End If
                If chkRelease.Checked = True Then
                    Release = 1
                Else
                    Release = 0
                End If
                txtConsignee.Text = Replace(txtConsignee.Text, "'", "")
                Dim strInsert As String = "INSERT INTO Consignee (Code, Consignee, NextTransNumber, Destination, Destination2, Limit, Used, NSF, Release, Active) " & _
                                             "VALUES (" & _
                                              "'" & txtCode.Text & "' , " & _
                                              "'" & txtConsignee.Text & "' , " & _
                                              "'" & txtNextTransNumber.Text & "', " & _
                                              "'" & txtDestination.Text & "', " & _
                                              "'" & txtDestination2.Text & "', " & _
                                              "'" & txtLimit.Text & "', " & _
                                              "'" & txtUsed.Text & "', " & _
                                              "'" & NSF & "', " & _
                                              "'" & Release & "', " & _
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
            SQL.RunQuery("Select * from Consignee where Code = '" & txtCode.Text & "' ")
            If SQL.RecordCount = 0 Then

                'Exit Sub
            Else
                'Delete record
                SQL.DataUpdate("DELETE FROM Consignee where Code ='" & txtCode.Text & "' ")
                MsgBox("Consignee Record Deleted")
                cmdClear.PerformClick()
            End If
        Catch ex As Exception
            AddLogEntry("DeleteConsignee: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtCode.Text = ""
        txtConsignee.Text = ""
        txtNextTransNumber.Text = ""
        txtDestination.Text = ""
        txtDestination2.Text = ""
        txtLimit.Text = ""
        txtUsed.Text = ""

    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click

        If txtCode.Enabled = False Then
            'Enable all
            txtCode.Enabled = True
            txtConsignee.Enabled = True
            txtNextTransNumber.Enabled = True
            txtDestination.Enabled = True
            txtDestination2.Enabled = True
            txtLimit.Enabled = True
            txtUsed.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            chkNSF.Enabled = True
            chkRelease.Enabled = True
            'Now clear them
            txtCode.Text = ""
            txtConsignee.Text = ""
            txtNextTransNumber.Text = ""
            txtDestination.Text = ""
            txtDestination2.Text = ""
            txtLimit.Text = ""
            txtUsed.Text = ""
            Exit Sub
        End If
        'Check each field to see if data exists 
        'then create a Query to search the database
        Dim Query As String = ""
        If txtCode.Text <> "" Then
            Query = "Code ='" & txtCode.Text & "'"
        End If
        If txtConsignee.Text <> "" Then
            Query = " Consignee ='" & txtConsignee.Text & "'"
        End If
        If txtNextTransNumber.Text <> "" Then
            Query = " NextTransNumber ='" & txtNextTransNumber.Text & "'"
        End If

        Try
            SQL.RunQuery("Select * from Consignee where " & Query)
            If SQL.RecordCount = 0 Then
                MsgBox("No Record Found")
            Else
                With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                    txtCode.Text = .Item("Code")
                    txtConsignee.Text = .Item("Consignee")
                    txtNextTransNumber.Text = .Item("NextTransNumber")
                    txtDestination.Text = .Item("Destination")
                    txtDestination2.Text = .Item("Destination2")
                    txtLimit.Text = .Item("Limit")
                    txtUsed.Text = .Item("Used") & ""
                    Dim Active As Integer = .Item("Active")
                    If Active = 0 Then
                        optYes.Checked = False
                        optNo.Checked = True
                    Else
                        optYes.Checked = True
                        optNo.Checked = False
                    End If
                    Dim NSF As Integer = .Item("NSF")
                    If NSF = 0 Then
                        chkNSF.Checked = False
                    Else
                        chkNSF.Checked = True
                    End If
                    Dim Release As Integer = .Item("Release")
                    If Release = 0 Then
                        chkRelease.Checked = False
                    Else
                        chkRelease.Checked = True
                    End If
                End With
            End If
            txtCode.Enabled = False
            txtConsignee.Enabled = False
            txtNextTransNumber.Enabled = False
            txtDestination.Enabled = False
            txtDestination2.Enabled = False
            txtLimit.Enabled = False
            txtUsed.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False
            chkNSF.Enabled = False
            chkRelease.Enabled = False
        Catch ex As Exception
            AddLogEntry("FindConsignee: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click_1(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub cmdGrid_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrid.Click
        frmConsigneeGrid.ShowDialog()
        LoadTimer.Enabled = True
    End Sub


End Class