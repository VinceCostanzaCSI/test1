﻿Public Class frmFindDriver

    Dim Driver As clsDriver
    Private SQL As New SQLControl

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frmFindDriver_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim LoopCtr As Integer
        Dim TmpStr As String

        Driver = New clsDriver
        CenterForm(Me)
        TmpStr = Driver.SearchField
        For LoopCtr = 0 To 3
            If TmpStr = cmbField.Items(LoopCtr) Then
                cmbField.SelectedIndex = LoopCtr
                Exit For
            End If
        Next LoopCtr

        ComboFill()
    End Sub

    Private Sub ComboFill()
        If Sql.HasConnection = True Then

            Sql.RunQuery("SELECT * FROM COMMODITY WHERE ID <> 'SA' AND ACTIVE = '1' ORDER BY ID")
            If Sql.SQLDataset.Tables.Count > 0 Then
                For Each r As DataRow In Sql.SQLDataset.Tables(0).Rows
                    cmbField.Items.Add(r("SAUC"))
                Next

            ElseIf Sql.SQLDataset.HasErrors <> "" Then
                MsgBox(Sql.SQLDataset.HasErrors)
            End If

        Else
            MsgBox("No SQL Connection")
        End If

    End Sub

    Private Sub cmdFind_Click(sender As System.Object, e As System.EventArgs) Handles cmdFind.Click
        Driver.SearchData = txtSearchStr.Text
        Driver = Nothing
        Me.Close()
    End Sub
End Class