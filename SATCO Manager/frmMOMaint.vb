Public Class frmMOMaint
    Dim CurrentRecord As Integer

    Private SQL As New SQLControl

    Private Sub frmMOMaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        LoadTimer.Enabled = True
    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        LoadTimer.Enabled = False
        cmdAdd.Text = "Add"
        cmdEdit.Text = "Edit"
        cmdFirst.PerformClick()
    End Sub

    Private Sub cmdFirst_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirst.Click
        Try
            SQL.RunQuery("Select * from Mosaic")
            'FillBoxes
            CurrentRecord = 0
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtRelease.Text = .Item("Release")
                txtConsignee.Text = .Item("Consignee")
                txtDeliveryDate.Text = .Item("DeliveryDate") & ""
                txtProduct.Text = .Item("Product") & ""
                txtDocItem.Text = .Item("DocItem") & ""
                txtShipTo.Text = .Item("ShipTo") & ""
                txtQuantity.Text = .Item("Quantity") & ""
                Dim Active As Integer = .Item("Active")
                If Active = 0 Then
                    optYes.Checked = False
                    optNo.Checked = True
                    cmdDelete.Enabled = True
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
                txtConsignee.Text = .Item("Consignee")
                txtDeliveryDate.Text = .Item("DeliveryDate") & ""
                txtProduct.Text = .Item("Product") & ""
                txtDocItem.Text = .Item("DocItem") & ""
                txtShipTo.Text = .Item("ShipTo") & ""
                txtQuantity.Text = .Item("Quantity") & ""
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
                txtConsignee.Text = .Item("Consignee")
                txtDeliveryDate.Text = .Item("DeliveryDate") & ""
                txtProduct.Text = .Item("Product") & ""
                txtDocItem.Text = .Item("DocItem") & ""
                txtShipTo.Text = .Item("ShipTo") & ""
                txtQuantity.Text = .Item("Quantity") & ""
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
                Dim NSF As Integer = .Item("NSF")
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
            txtConsignee.Enabled = True
            txtDeliveryDate.Enabled = True
            txtProduct.Enabled = True
            txtDocItem.Enabled = True
            txtShipTo.Enabled = True
            txtQuantity.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
        Else
            cmdEdit.Text = "Edit"
            If optYes.Checked = True Then
                Active = 1
            Else
                Active = 0
            End If
            txtConsignee.Text = Replace(txtConsignee.Text, "'", "")
            Dim UpdateCmd As String = "Update Mosaic " & _
                                          "Set Release ='" & txtRelease.Text & "' , " & _
                                          "Consignee='" & txtConsignee.Text & "' , " & _
                                          "DeliveryDate ='" & txtDeliveryDate.Text & "', " & _
                                          "Product ='" & txtProduct.Text & "', " & _
                                          "DocItem ='" & txtDocItem.Text & "', " & _
                                          "ShipTo ='" & txtShipTo.Text & "', " & _
                                          "Quantity ='" & txtQuantity.Text & "', " & _
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
            txtConsignee.Enabled = False
            txtDeliveryDate.Enabled = False
            txtProduct.Enabled = False
            txtDocItem.Enabled = False
            txtShipTo.Enabled = False
            txtQuantity.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False

            AddLogEntry("Mosaic Record " & txtRelease.Text & " was edited ")
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim Active As Integer

        If cmdAdd.Text = "Add" Then
            cmdAdd.Text = "Confirm"
            txtRelease.Enabled = True
            txtConsignee.Enabled = True
            txtDeliveryDate.Enabled = True
            txtProduct.Enabled = True
            txtDocItem.Enabled = True
            txtShipTo.Enabled = True
            txtQuantity.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            txtRelease.Text = ""
            txtRelease.Focus()
        Else

            Try
                If optYes.Checked = True Then
                    Active = 1
                Else
                    Active = 0
                End If

                txtConsignee.Text = Replace(txtConsignee.Text, "'", "")
                Dim strInsert As String = "INSERT INTO Consignee (Release, Consignee, DeliveryDate, Product, DocItem, ShipTo, Quantity, Active) " & _
                                             "VALUES (" & _
                                              "'" & txtRelease.Text & "' , " & _
                                              "'" & txtConsignee.Text & "' , " & _
                                              "'" & txtDeliveryDate.Text & "', " & _
                                              "'" & txtProduct.Text & "', " & _
                                              "'" & txtDocItem.Text & "', " & _
                                              "'" & txtShipTo.Text & "', " & _
                                              "'" & txtQuantity.Text & "', " & _
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
            SQL.RunQuery("Select * from Mosaic where Release = '" & txtRelease.Text & "' ")
            If SQL.RecordCount = 0 Then

                'Exit Sub
            Else
                'Delete record
                SQL.DataUpdate("DELETE FROM Mosaic where Release ='" & txtRelease.Text & "' ")
                MsgBox("Consignee Record Deleted")
                cmdClear.PerformClick()
            End If
        Catch ex As Exception
            AddLogEntry("DeleteMosaic: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtRelease.Text = ""
        txtConsignee.Text = ""
        txtDeliveryDate.Text = ""
        txtProduct.Text = ""
        txtDocItem.Text = ""
        txtShipTo.Text = ""
        txtQuantity.Text = ""

    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click

        If txtRelease.Enabled = False Then
            'Enable all
            txtRelease.Enabled = True
            txtConsignee.Enabled = True
            txtDeliveryDate.Enabled = True
            txtProduct.Enabled = True
            txtDocItem.Enabled = True
            txtShipTo.Enabled = True
            txtQuantity.Enabled = True
            optYes.Enabled = True
            optNo.Enabled = True
            'Now clear them
            txtRelease.Text = ""
            txtConsignee.Text = ""
            txtDeliveryDate.Text = ""
            txtProduct.Text = ""
            txtDocItem.Text = ""
            txtShipTo.Text = ""
            txtQuantity.Text = ""
            Exit Sub
        End If
        'Check each field to see if data exists 
        'then create a Query to search the database
        Dim Query As String = ""
        If txtRelease.Text <> "" Then
            Query = "Release ='" & txtRelease.Text & "'"
        End If
        If txtConsignee.Text <> "" Then
            Query = " Consignee ='" & txtConsignee.Text & "'"
        End If
        If txtDeliveryDate.Text <> "" Then
            Query = " DeliveryDate ='" & txtDeliveryDate.Text & "'"
        End If

        Try
            SQL.RunQuery("Select * from Mosaic where " & Query)
            If SQL.RecordCount = 0 Then
                MsgBox("No Record Found")
            Else
                With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                    txtRelease.Text = .Item("Release")
                    txtConsignee.Text = .Item("Consignee")
                    txtDeliveryDate.Text = .Item("DeliveryDate")
                    txtProduct.Text = .Item("Product")
                    txtDocItem.Text = .Item("DocItem")
                    txtShipTo.Text = .Item("ShipTo")
                    txtQuantity.Text = .Item("Quantity") & ""
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
            txtConsignee.Enabled = False
            txtDeliveryDate.Enabled = False
            txtProduct.Enabled = False
            txtDocItem.Enabled = False
            txtShipTo.Enabled = False
            txtQuantity.Enabled = False
            optYes.Enabled = False
            optNo.Enabled = False
        Catch ex As Exception
            AddLogEntry("FindConsignee: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click_1(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdGrid_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrid.Click
        frmMosaicGrid.ShowDialog()
    End Sub
   
End Class