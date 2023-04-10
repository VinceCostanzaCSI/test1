Public Class frmCommodityMaint
    Dim CurrentRecord As Integer

    Private SQL As New SQLControl

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub frmCommodityMaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        LoadTimer.Enabled = True
    End Sub

    Private Sub LoadTimer_Tick(sender As System.Object, e As System.EventArgs) Handles LoadTimer.Tick
        LoadTimer.Enabled = False
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

        cmdAdd.Text = "Add"
        cmdEdit.Text = "Edit"
        cmdFirst.PerformClick()
    End Sub

    Private Sub cmdFirst_Click(sender As System.Object, e As System.EventArgs) Handles cmdFirst.Click
        Try
            Dim Active As Integer

            SQL.RunQuery("Select * from Commodity")
            'FillBoxes
            CurrentRecord = 0
            With SQL.SQLDataset.Tables(0).Rows(CurrentRecord)
                txtID.Text = .Item("ID")
                txtDescription1.Text = .Item("Description1")
                txtDescription2.Text = .Item("Description2")
                txtDescription3.Text = .Item("Description3")
                txtDescription4.Text = .Item("Description4")
                txtDescription5.Text = .Item("Description5")
                txtVariableWt.Text = .Item("VariableWeight")
                Dim CommodityType As Integer = .Item("CType")
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
            LabelUpdate()
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
                txtDescription1.Text = .Item("Description1")
                txtDescription2.Text = .Item("Description2")
                txtDescription3.Text = .Item("Description3")
                txtDescription4.Text = .Item("Description4")
                txtDescription5.Text = .Item("Description5")
                txtVariableWt.Text = .Item("VariableWeight")
                Dim CommodityType As Integer = .Item("CType")
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
            LabelUpdate()
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
                txtDescription1.Text = .Item("Description1")
                txtDescription2.Text = .Item("Description2")
                txtDescription3.Text = .Item("Description3")
                txtDescription4.Text = .Item("Description4")
                txtDescription5.Text = .Item("Description5")
                txtVariableWt.Text = .Item("VariableWeight")
                Dim CommodityType As Integer = .Item("CType")
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
            LabelUpdate()
        Catch ex As Exception
            'MsgBox(ex.Message)
            If InStr(ex.Message, "There is no row") <> 0 Then
                cmdNext.Enabled = False
            End If
        End Try
    End Sub

    Private Sub cmdEdit_Click(sender As System.Object, e As System.EventArgs) Handles cmdEdit.Click
        Try


            Dim Active As Integer
            Dim CommodityType As Integer

            If cmdEdit.Text = "Edit" Then
                cmdEdit.Text = "Update"
                txtID.Enabled = True
                txtDescription1.Enabled = True
                txtDescription2.Enabled = True
                txtDescription3.Enabled = True
                txtDescription4.Enabled = True
                txtDescription5.Enabled = True
                txtVariableWt.Enabled = True
                GroupBox2.Enabled = True
                GroupBox3.Enabled = True

            Else
                cmdEdit.Text = "Edit"
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
                Dim UpdateCmd As String = "Update Commodity " & _
                                              "Set Id ='" & txtID.Text & "' , " & _
                                              "Description1 ='" & txtDescription1.Text & "' , " & _
                                              "Description2 ='" & txtDescription2.Text & "', " & _
                                              "Description3 ='" & txtDescription3.Text & "', " & _
                                              "Description4 ='" & txtDescription4.Text & "', " & _
                                              "Description5 ='" & txtDescription5.Text & "', " & _
                                              "VariableWeight ='" & txtVariableWt.Text & "', " & _
                                              "CType ='" & CommodityType & "', " & _
                                              "Active ='" & Active & "' " & _
                                              "Where Id = '" & txtID.Text & "';"

                If SQL.DataUpdate(UpdateCmd) = 0 Then
                    MsgBox("Error updating Commodity Record")
                    'Stop
                Else
                    MsgBox("Commodity Record Updated")
                    'Stop
                End If

                'Update tank description
                Dim TankDescription As String
                SQL.RunQuery("Select * from tank where Commodity = '" & txtID.Text & "'")
                If SQL.RecordCount <> 0 Then
                    Dim TankID As String = SQL.SQLDataset.Tables(0).Rows(0).Item("Id")
                    If Mid(txtID.Text, 1, 2) = "UC" Then
                        TankDescription = txtDescription1.Text & " " & txtDescription3.Text
                    Else
                        TankDescription = txtDescription1.Text & " " & txtDescription2.Text
                    End If
                    UpdateCmd = "Update Tank Set Description = '" & TankDescription & "' Where Id = '" & TankID & "';"

                    If SQL.DataUpdate(UpdateCmd) = 0 Then
                        MsgBox("Error updating Tank Record")
                        'Stop
                    Else
                        'MsgBox("Commodity Record Updated")
                        'Stop
                    End If
                Else
                    MsgBox("Tank not assigned to this commodity")
                End If

                txtID.Enabled = False
                txtDescription1.Enabled = False
                txtDescription2.Enabled = False
                txtDescription3.Enabled = False
                txtDescription4.Enabled = False
                txtDescription5.Enabled = False
                txtVariableWt.Enabled = False
                GroupBox2.Enabled = False
                GroupBox3.Enabled = False

                AddLogEntry("Commodity Record " & txtID.Text & " was edited ")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        Dim CommodityType As Integer
        Dim Active As Integer

        If cmdAdd.Text = "Add" Then
            cmdAdd.Text = "Confirm"
            txtID.Enabled = True
            txtID.Text = ""
            txtDescription1.Enabled = True
            txtDescription2.Enabled = True
            txtDescription3.Enabled = True
            txtDescription4.Enabled = True
            txtDescription5.Enabled = True
            txtVariableWt.Enabled = True
            GroupBox2.Enabled = True
            GroupBox3.Enabled = True
            txtID.Focus()
        Else
            cmdAdd.Text = "Add"

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
                Dim strInsert As String = "INSERT INTO Commodity (Id, Description1, Description2, Description3, Description4, Description5, VariableWeight, CType, Active) " & _
                                             "VALUES (" & _
                                             "'" & txtID.Text & "'," & _
                                             "'" & txtDescription1.Text & "'," & _
                                             "'" & txtDescription2.Text & "'," & _
                                             "'" & txtDescription3.Text & "'," & _
                                             "'" & txtDescription4.Text & "'," & _
                                             "'" & txtDescription5.Text & "'," & _
                                             "'" & txtVariableWt.Text & "'," & _
                                             "'" & CommodityType & "', " & _
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
            SQL.RunQuery("Select * from Commodity where Id = '" & txtID.Text & "' ")
            If SQL.RecordCount = 0 Then

                'Exit Sub
            Else
                'Delete record
                SQL.DataUpdate("DELETE FROM Commodity where Id ='" & txtID.Text & "' ")
                MsgBox("Commodity Record Deleted")
                cmdClear.PerformClick()
            End If
        Catch ex As Exception
            AddLogEntry("DeleteCommodity: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdClear_Click(sender As System.Object, e As System.EventArgs) Handles cmdClear.Click
        txtID.Text = ""
        txtDescription1.Text = ""
        txtDescription2.Text = ""
        txtDescription3.Text = ""
        txtDescription4.Text = ""
        txtDescription5.Text = ""
        txtVariableWt.Text = ""
    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click

        If txtID.Enabled = False Then
            'Enable all
            txtID.Enabled = True
            txtDescription1.Enabled = True
            txtDescription2.Enabled = True
            txtDescription3.Enabled = True
            txtDescription4.Enabled = True
            txtDescription5.Enabled = True
            txtVariableWt.Enabled = True
            GroupBox2.Enabled = True
            GroupBox3.Enabled = True
            'Now clear them
            txtID.Text = ""
            txtDescription1.Text = ""
            txtDescription2.Text = ""
            txtDescription3.Text = ""
            txtDescription4.Text = ""
            txtDescription5.Text = ""
            txtVariableWt.Text = ""
            Exit Sub
        End If
        'Check each field to see if data exists 
        'then create a Query to search the database
        Dim Query As String = ""
        If txtID.Text <> "" Then
            Query = "Id ='" & txtID.Text & "'"
        End If

        Try
            SQL.RunQuery("Select * from Commodity where " & Query)
            If SQL.RecordCount = 0 Then
                MsgBox("No Record Found")
            Else
                With SQL.SQLDataset.Tables(0).Rows(0)
                    txtID.Text = .Item("ID")
                    txtDescription1.Text = .Item("Description1")
                    txtDescription2.Text = .Item("Description2")
                    txtDescription3.Text = .Item("Description3")
                    txtDescription4.Text = .Item("Description4")
                    txtDescription5.Text = .Item("Description5")
                    txtVariableWt.Text = .Item("VariableWeight")
                    Dim CommodityType As Integer = .Item("CType")
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
            txtDescription1.Enabled = False
            txtDescription2.Enabled = False
            txtDescription3.Enabled = False
            txtDescription4.Enabled = False
            txtDescription5.Enabled = False
            txtVariableWt.Enabled = False
            GroupBox2.Enabled = False
            GroupBox3.Enabled = False
        Catch ex As Exception
            AddLogEntry("FindCommodity: " & ex.Message)
        End Try
    End Sub
    Private Sub LabelUpdate()
        If optTypeSA.Checked = True Then
            lblDesc1.Text = "Commodity Name:"
            lblDesc2.Text = "Strength:"
            lblDesc3.Text = "Specific Gravity:"
            lblDesc4.Text = "Iron (Fe):"
            lblDesc4.Visible = True
            lblDesc5.Visible = False
            txtDescription4.Visible = True
            txtDescription5.Visible = False
        End If

        If optTypeUC.Checked = True Then
            lblDesc1.Text = "Commodity Name:"
            lblDesc2.Text = "Production Date:"
            lblDesc3.Text = "MCDS %:"
            lblDesc4.Text = "Weight (Lbs/gal):"
            lblDesc5.Text = "Lab Tech:"
            lblDesc4.Visible = True
            lblDesc5.Visible = True
            txtDescription4.Visible = True
            txtDescription5.Visible = True
        End If
        If optTypeMO.Checked = True Then
            lblDesc1.Text = "Commodity Name:"
            lblDesc2.Text = "Description1:"
            lblDesc3.Text = "Description2:"
            lblDesc4.Text = "Description3:"
            lblDesc5.Text = "Description4:"
            lblDesc4.Visible = True
            lblDesc5.Visible = True
            txtDescription4.Visible = True
            txtDescription5.Visible = True
        End If
        If optTypeBN.Checked = True Then
            lblDesc1.Text = "Commodity Name:"
            lblDesc2.Text = "Description1:"
            lblDesc3.Text = "Description2:"
            lblDesc4.Text = "Description3:"
            lblDesc5.Text = "Description4:"
            lblDesc4.Visible = True
            lblDesc5.Visible = True
            txtDescription4.Visible = True
            txtDescription5.Visible = True
        End If
    End Sub

    Private Sub cmdGrid_Click(sender As System.Object, e As System.EventArgs) Handles cmdGrid.Click
        frmCommodity.ShowDialog()
        LoadTimer.Enabled = True
    End Sub
End Class