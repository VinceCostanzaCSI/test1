Public Class frmTankReport
    Private SQL As New SQLControl

    Private Sub frmTankReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterForm(Me)
        If frmMain.optStockton.Checked = True Then
            CheckBox1.Text = "Tank 100"
            CheckBox2.Text = "Tank 103"
            CheckBox3.Visible = False
            CheckBox4.Visible = False
            CheckBox5.Visible = False
            CheckBox6.Visible = False
            CheckBox7.Visible = False
            CheckBox8.Visible = False
            CheckBox9.Visible = False
            CheckBox10.Visible = False
        End If

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        CheckBox6.Checked = False
        CheckBox7.Checked = False
        CheckBox8.Checked = False
        CheckBox9.Checked = False
        CheckBox10.Checked = False
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        Dim i As Integer
        Dim TID(10) As String
        Dim TLevel(10) As String
        Dim TAnalysis(10) As String
        Dim ReportTank(10) As Boolean
        Try
            'See which tanks will be reported
            If CheckBox1.Checked Then ReportTank(0) = True Else ReportTank(0) = False
            If CheckBox2.Checked Then ReportTank(1) = True Else ReportTank(1) = False
            If CheckBox3.Checked Then ReportTank(2) = True Else ReportTank(2) = False
            If CheckBox4.Checked Then ReportTank(3) = True Else ReportTank(3) = False
            If CheckBox5.Checked Then ReportTank(4) = True Else ReportTank(4) = False
            If CheckBox6.Checked Then ReportTank(5) = True Else ReportTank(5) = False
            If CheckBox7.Checked Then ReportTank(6) = True Else ReportTank(6) = False
            If CheckBox8.Checked Then ReportTank(7) = True Else ReportTank(7) = False
            If CheckBox9.Checked Then ReportTank(8) = True Else ReportTank(8) = False
            If CheckBox10.Checked Then ReportTank(9) = True Else ReportTank(9) = False

            'Update Formulas in ReportInfo Table
            Query = "As of " & frmMain.EndDatePicker.Text & " " & frmMain.txtEndTime.Text
            'Dim TQuery As String = "From " & frmMain.txtStartTime.Text & " to " & frmMain.txtEndTime.Text
            Dim UpdateCmd As String = "UPDATE ReportInfo " &
                                      "SET Formula ='" & Query & "' , " &
                                      "Formula2 ='  '"

            If SQL.DataUpdate(UpdateCmd) = 0 Then
                AddLogEntry("Error updating ReportInfo File")
            Else

                AddLogEntry("ReportInfo File Updated")
            End If
            'Stop
            'Query = "SELECT TankID, TDate, TankLevel, Analysis FROM Trans a WHERE Tdate = (SELECT MAX( Tdate )
            'FROM Trans b where TDate < '" & frmMain.EndDatePicker.Text & " 23:59:59' and TDate > '" & frmMain.EndDatePicker.Text & " 00:00:00' and  a.TankID = b.TankID) order by TankID asc"

            Query = "SELECT TankID, TDate, TankLevel, Analysis FROM Trans a WHERE Tdate = (SELECT MAX( Tdate )
                 FROM Trans b where TDate < '" & frmMain.EndDatePicker.Text & " " & frmMain.txtEndTime.Text & "' and  a.TankID = b.TankID) order by TankID asc"

            SQL.RunQuery(Query)
            Stop
            If SQL.RecordCount > 0 Then
                Dim rCount As Integer = SQL.RecordCount - 1

                For i = 0 To rCount
                    TID(i) = SQL.SQLDataset.Tables(0).Rows(i).Item("TankId") & ""
                    TLevel(i) = SQL.SQLDataset.Tables(0).Rows(i).Item("TankLevel") & 0
                    TAnalysis(i) = SQL.SQLDataset.Tables(0).Rows(i).Item("Analysis") & ""
                Next i
                'Clear out table for new report
                SQL.RunQuery("Delete from TankReport")

                'Now save it to the TankReport Table - Insert
                For i = 0 To rCount
                    If ReportTank(i) = True Then
                        SQL.RunQuery("Insert TankReport(TankID, TankLevel, Analysis) VALUES('" & TID(i) & "', '" & TLevel(i) & "', '" & TAnalysis(i) & "')")
                    End If
                Next i

                If frmMain.optStockton.Checked = True Then
                    frmViewReport.rptViewer.ReportSource = RptPath & "Tank-Inventory-SQL-CA.rpt"
                Else
                    frmViewReport.rptViewer.ReportSource = RptPath & "Tank-Inventory-SQL.rpt"
                End If

                frmViewReport.Show()
                Me.Close()
            Else
                MsgBox("No Tank info found")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cmdAll_Click(sender As Object, e As EventArgs) Handles cmdAll.Click
        CheckBox1.Checked = True
        CheckBox2.Checked = True
        CheckBox3.Checked = True
        CheckBox4.Checked = True
        CheckBox5.Checked = True
        CheckBox6.Checked = True
        CheckBox7.Checked = True
        CheckBox8.Checked = True
        CheckBox9.Checked = True
        CheckBox10.Checked = True
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        CheckBox6.Checked = False
        CheckBox7.Checked = False
        CheckBox8.Checked = False
        CheckBox9.Checked = False
        CheckBox10.Checked = False
    End Sub

End Class