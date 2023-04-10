Public Class frmBrenntagReport

    Private Sub cmdExit_Click(sender As System.Object, e As System.EventArgs) Handles cmdExit.Click
        Me.Close()

    End Sub

    Private Sub txtStartDate_Click(sender As Object, e As System.EventArgs) Handles txtStartDate.Click
        txtStartDate.Text = DTPicker.Text
    End Sub

    Private Sub txtStartDate_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtStartDate.TextChanged

    End Sub

    Private Sub txtEndDate_Click(sender As Object, e As System.EventArgs) Handles txtEndDate.Click
        txtEndDate.Text = DTPicker.Text
    End Sub

    Private Sub txtEndDate_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtEndDate.TextChanged

    End Sub

    Private Sub cmdTime_Click(sender As System.Object, e As System.EventArgs) Handles cmdTime.Click
        txtEndTime.Text = "23:59"
        txtStartTime.Text = "00:01"
    End Sub
End Class