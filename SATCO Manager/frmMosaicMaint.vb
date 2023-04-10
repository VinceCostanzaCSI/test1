Public Class frmMosaicMaint
    Dim CurrentRecord As Integer

    Private SQL As New SQLControl

    Private Sub frmMosaicMaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

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
        Me.Close()
    End Sub

    Private Sub cmdFirst_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirst.Click
        Try
            SQL.RunQuery("Select * from Mosaic")
            'FillBoxes
            CurrentRecord = 0
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtRelease.Text = .Item("Release")
                txtGrade.Text = .Item("Grade")
                txtProduct.Text = .Item("Product")
                txtConsignee.Text = .Item("Consignee")
                txtPO.Text = .Item("PO")
                txtFreight.Text = .Item("Freight")
                txtCode.Text = .Item("Code")
                Dim Evoqua As Boolean = .Item("Evoqua")
                If Evoqua = True Then
                    chkEvoqua.Checked = True
                Else
                    chkEvoqua.Checked = False
                End If
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
                txtRelease.Text = .Item("Release")
                txtGrade.Text = .Item("Grade")
                txtProduct.Text = .Item("Product")
                txtConsignee.Text = .Item("Consignee")
                txtPO.Text = .Item("PO")
                txtFreight.Text = .Item("Freight")
                txtCode.Text = .Item("Code")
                Dim Evoqua As Boolean = .Item("Evoqua")
                If Evoqua = True Then
                    chkEvoqua.Checked = True
                Else
                    chkEvoqua.Checked = False
                End If
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
                txtRelease.Text = .Item("Release")
                txtGrade.Text = .Item("Grade")
                txtProduct.Text = .Item("Product")
                txtConsignee.Text = .Item("Consignee")
                txtPO.Text = .Item("PO")
                txtFreight.Text = .Item("Freight")
                txtCode.Text = .Item("Code")
                Dim Evoqua As Boolean = .Item("Evoqua")
                If Evoqua = True Then
                    chkEvoqua.Checked = True
                Else
                    chkEvoqua.Checked = False
                End If
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
        Dim Evoqua As Boolean
        If cmdEdit.Text = "Edit" Then
            cmdEdit.Text = "Update"
            txtRelease.Enabled = True
            txtGrade.Enabled = True
            txtProduct.Enabled = True
            txtConsignee.Enabled = True
            txtPO.Enabled = True
            txtFreight.Enabled = True
            txtCode.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
        Else
            cmdEdit.Text = "Edit"
            If optYes.Checked = True Then
                Active = 1
            Else
                Active = 0
            End If
            If chkEvoqua.Checked = True Then
                Evoqua = True
            Else
                Evoqua = False
            End If
            Dim UpdateCmd As String = "Update Mosaic " & _
                                          "Set Release ='" & txtRelease.Text & "' , " & _
                                          "Product ='" & txtProduct.Text & "' , " & _
                                          "Grade ='" & txtGrade.Text & "', " & _
                                          "Consignee ='" & txtConsignee.Text & "', " & _
                                          "PO ='" & txtPO.Text & "', " & _
                                          "Freight ='" & txtFreight.Text & "', " & _
                                          "Code ='" & txtCode.Text & "', " & _
                                          "Evoqua ='" & Evoqua & "', " & _
                                          "Active ='" & Active & "' " & _
                                          "Where Release = '" & txtRelease.Text & "';"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                MsgBox("Error updating Mosaic Record")
                'Stop
            Else
                MsgBox("Mosaic Record Updated")
                'Stop
            End If
            txtRelease.Enabled = False
            txtGrade.Enabled = False
            txtProduct.Enabled = False
            txtConsignee.Enabled = False
            txtPO.Enabled = False
            txtFreight.Enabled = False
            txtCode.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False

            AddLogEntry("Mosaic Record " & txtRelease.Text & " was edited ")
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim Active As Integer
        Dim Evoqua As Boolean
        If cmdAdd.Text = "Add" Then
            cmdAdd.Text = "Confirm"
            txtRelease.Enabled = True
            txtGrade.Enabled = True
            txtProduct.Enabled = True
            txtConsignee.Enabled = True
            txtPO.Enabled = True
            txtFreight.Enabled = True
            txtCode.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            chkEvoqua.Enabled = True
            txtRelease.Text = ""
            txtPO.Text = ""
            txtRelease.Focus()
        Else

            Try
                If optYes.Checked = True Then
                    Active = 1
                Else
                    Active = 0
                End If
                If chkEvoqua.Checked = True Then
                    Evoqua = True
                Else
                    Evoqua = False
                End If
                Dim strInsert As String = "INSERT INTO Mosaic (Release, Product, Grade, Consignee, PO, Freight, Code, Evoqua, Active) " & _
                                             "VALUES (" & _
                                             "'" & txtRelease.Text & "'," & _
                                              "'" & txtProduct.Text & "' , " & _
                                              "'" & txtGrade.Text & "', " & _
                                              "'" & txtConsignee.Text & "', " & _
                                              "'" & txtPO.Text & "', " & _
                                              "'" & txtFreight.Text & "', " & _
                                              "'" & txtCode.Text & "', " & _
                                              "'" & Evoqua & "', " & _
                                             "'" & Active & "') "
                SQL.DataUpdate(strInsert)
                txtRelease.Enabled = False
                txtGrade.Enabled = False
                txtProduct.Enabled = False
                txtConsignee.Enabled = False
                txtPO.Enabled = False
                txtFreight.Enabled = False
                txtCode.Enabled = False
                optYes.Enabled = False
                optNo.Enabled = False
                chkEvoqua.Enabled = False

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs) Handles cmdDelete.Click
        Try
            SQL.RunQuery("Select * from Mosaic where Release = '" & txtRelease.Text & "' ")
            If SQL.RecordCount = 0 Then

                'Exit Sub
            Else
                'Delete record
                SQL.DataUpdate("DELETE FROM Mosaic where Release ='" & txtRelease.Text & "' ")
                MsgBox("Mosaic Record Deleted")
                cmdClear.PerformClick()
            End If
        Catch ex As Exception
            AddLogEntry("DeleteMosaic: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtRelease.Text = ""
        txtGrade.Text = ""
        txtProduct.Text = ""
        txtConsignee.Text = ""
        txtPO.Text = ""
        txtFreight.Text = ""
        txtCode.Text = ""

    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click

        If txtRelease.Enabled = False Then
            'Enable all
            txtRelease.Enabled = True
            txtGrade.Enabled = True
            txtProduct.Enabled = True
            txtConsignee.Enabled = True
            txtPO.Enabled = True
            txtFreight.Enabled = True
            txtCode.Enabled = True

            optYes.Enabled = True
            optNo.Enabled = True
            'Now clear them
            txtRelease.Text = ""
            txtGrade.Text = ""
            txtProduct.Text = ""
            txtConsignee.Text = ""
            txtPO.Text = ""
            txtFreight.Text = ""
            txtCode.Text = ""

            Exit Sub
        End If
        'Check each field to see if data exists 
        'then create a Query to search the database
        Dim Query As String = ""
        If txtRelease.Text <> "" Then
            Query = "Release ='" & txtRelease.Text & "'"
        End If
        If txtGrade.Text <> "" Then
            Query = ", Grade ='" & txtGrade.Text & "'"
        End If
        If txtProduct.Text <> "" Then
            Query = ", Product ='" & txtProduct.Text & "'"
        End If

        If txtFreight.Text <> "" Then
            Query = ", Freight ='" & txtFreight.Text & "'"
        End If
        Try
            SQL.RunQuery("Select * from Mosaic where " & Query)
            If SQL.RecordCount = 0 Then
                MsgBox("No Record Found")
            Else
                With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                    txtRelease.Text = .Item("Release")
                    txtGrade.Text = .Item("Grade")
                    txtProduct.Text = .Item("Product")
                    txtConsignee.Text = .Item("Consignee")
                    txtPO.Text = .Item("PO")
                    txtFreight.Text = .Item("Freight")
                    txtCode.Text = .Item("Code")
                    Dim Evoqua As Boolean = .Item("Evoqua")
                    If Evoqua = True Then
                        chkEvoqua.Checked = True
                    Else
                        chkEvoqua.Checked = False
                    End If
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
            txtGrade.Enabled = False
            txtProduct.Enabled = False
            txtConsignee.Enabled = False
            txtPO.Enabled = False
            txtFreight.Enabled = False
            txtCode.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False
        Catch ex As Exception
            AddLogEntry("FindMosaic: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdGrid_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrid.Click
        frmMosaicGrid.ShowDialog()
        LoadTimer.Enabled = True
    End Sub

    Private Sub txtRelease_TextChanged(sender As Object, e As EventArgs) Handles txtRelease.TextChanged

    End Sub
End Class