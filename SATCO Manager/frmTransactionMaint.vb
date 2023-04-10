Imports System.Runtime.InteropServices


Public Class frmTransactionMaint
    Private SQL As New SQLControl

    <DllImport("winspool.drv", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SetDefaultPrinter(Name As String) As Boolean
    End Function

    Private Sub frmTransactionMaint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            CenterForm(Me)
            If FileControl = 0 Then     'Customer Service
                'cmdAdd.Visible = False
                DGVData.AllowUserToDeleteRows = False
                cmdModify.Visible = False
                cmdCorrection.Visible = False
            End If
            If FileControl = 1 Then     'Operator
                'cmdAdd.Visible = True
                DGVData.AllowUserToDeleteRows = False
                cmdModify.Visible = False
                cmdCorrection.Visible = True
            End If
            If FileControl = 2 Then     'Management
                'cmdAdd.Visible = True
                DGVData.AllowUserToDeleteRows = True
                cmdModify.Visible = True
                cmdCorrection.Visible = True
            End If

            LoadDataGrid()

            LoadPrinters()

        Catch ex As Exception
            MsgBox("Load Form: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadDataGrid()

        Try

            SQL.RunQuery(Query)

            If SQL.RecordCount = 0 Then
                MsgBox("No Records Found")
            Else
                lblTotal.Text = SQL.RecordCount
                DGVData.DataSource = SQL.SQLDataset.Tables(0)
                'DGVData.Rows(0).Selected = True
                SQL.SQLDA.UpdateCommand = New SqlClient.SqlCommandBuilder(SQL.SQLDA).GetUpdateCommand
            End If

        Catch ex As Exception
            If InStr(ex.Message, "key column") <> 0 Then
                'Do nothing
            Else
                MsgBox("LoadDataGrid: " & ex.Message)
            End If

        End Try

    End Sub

    Private Sub LoadPrinters()
        Dim objSettings As New Printing.PrinterSettings
        Dim strPrinter As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings
        Dim DefaultPrinterName As String
        cboPrinters.Items.Clear()
        Try
            For Each strPrinter In Printing.PrinterSettings.InstalledPrinters
                cboPrinters.Items.Add(strPrinter)
                'End If
            Next
            'cboPrinters.SelectedText = objSettings.PrinterName.ToString
            DefaultPrinterName = oPS.PrinterName
            cboPrinters.SelectedText = oPS.PrinterName
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Dispose()
    End Sub

    Private Sub cmdModify_Click(sender As System.Object, e As System.EventArgs) Handles cmdModify.Click
        'Save Updates to the database

        SQL.SQLDA.Update(SQL.SQLDataset) 'Todo data validation

        'Refresh DataGrid
        DGVData.Refresh()
        'cmdModify.Enabled = False
    End Sub

    Private Sub cmdDelete_Click(sender As System.Object, e As System.EventArgs)
        DGVData.AllowUserToDeleteRows = True
    End Sub

    Private Sub cmdAdd_Click(sender As System.Object, e As System.EventArgs) Handles cmdAdd.Click
        DGVData.AllowUserToAddRows = True
    End Sub

    Private Sub cmdCorrection_Click(sender As System.Object, e As System.EventArgs) Handles cmdCorrection.Click
        SelectedCode = DGVData.Rows(SelectedRow).Cells(0).Value
        SelectedID = DGVData.Rows(SelectedRow).Cells(1).Value
        frmTransactionCorrection.ShowDialog()
        LoadDataGrid()
        Me.Close()

    End Sub

    Private Sub DGVData_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles DGVData.MouseUp
        SelectedRow = DGVData.CurrentRow.Index
    End Sub

    Private Sub cmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles cmdPrint.Click
        NewPrinterCode = DGVData.Rows(SelectedRow).Cells(0).Value
        NewPrinterID = DGVData.Rows(SelectedRow).Cells(1).Value
        SARelease = DGVData.Rows(SelectedRow).Cells(14).Value
        If SetDefaultPrinter(cboPrinters.SelectedItem) = True Then
            If InStr(SARelease, "RAIL") <> 0 Then
                PrintRailTicket()
            Else
                PrintLastTicket()
            End If
        End If
    End Sub

    Private Sub cmdPrintBOL_Click(sender As Object, e As EventArgs) Handles cmdPrintBOL.Click
        Dim i As Integer
        Dim BOLCount As Integer = 0
        Try
            For i = 0 To DGVData.RowCount - 1
                If DGVData.Rows(i).Cells(14).Value <> "INV" Then
                    BOLCount = BOLCount + 1
                End If
            Next i

            If MsgBox(BOLCount & " BOLs will be printed" & vbCrLf & "Do you wish to continue?", vbYesNo, "Multiple BOL Printing") = vbYes Then
                SetDefaultPrinter(cboPrinters.SelectedItem)
                For i = 0 To DGVData.RowCount - 1
                    If DGVData.Rows(i).Cells(14).Value <> "INV" Then
                        NewPrinterCode = DGVData.Rows(i).Cells(0).Value
                        NewPrinterID = DGVData.Rows(i).Cells(1).Value
                        SARelease = DGVData.Rows(i).Cells(14).Value

                        If InStr(SARelease, "RAIL") <> 0 Then
                            PrintRailTicket()
                        Else
                            PrintLastTicket()
                        End If

                        Delay(5000)   'wait 5 seconds before attempting another print
                    End If
                Next i
            End If
        Catch ex As Exception
            MsgBox("cmdPrintBOL: " & ex.Message)
        End Try
    End Sub

    Private Sub cboPrinters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPrinters.SelectedIndexChanged
        'Dim oPS As New System.Drawing.Printing.PrinterSettings
        'Dim DefaultPrinterName As String
        'Try
        '    DefaultPrinterName = oPS.PrinterName
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Capturing Default Printer", MessageBoxButtons.OK)
        '    oPS = Nothing
        'Finally
        '    oPS = Nothing
        'End Try
    End Sub

    Private Sub cmdRail_Click(sender As Object, e As EventArgs) Handles cmdRail.Click
        NewPrinterCode = DGVData.Rows(SelectedRow).Cells(0).Value
        NewPrinterID = DGVData.Rows(SelectedRow).Cells(1).Value
        SARelease = DGVData.Rows(SelectedRow).Cells(14).Value

        If InStr(SARelease, "RAIL") <> 0 Then
            frmRailDetail.ShowDialog()
        Else
            MsgBox("Not on a RAIL transaction")
        End If

    End Sub
End Class