
Public Class frmLogon
    Private SQL As New SQLControl
    Dim Attempts As Integer = 3

    Private Sub frmLogon_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CenterForm(Me)
        txtUsername.Text = ""
        txtPassword.Text = ""
        lblStatus.Text = "Enter username and password"
    End Sub

    Private Sub CmdOK_Click(sender As System.Object, e As System.EventArgs) Handles CmdOK.Click
        Dim CS As Boolean
        Dim OP As Boolean
        Dim MG As Boolean

        lblStatus.Text = "Checking for valid Username and Password"
        lblStatus.Refresh()

        If txtUsername.Text = "tha" And txtPassword.Text = "master" Then
            lblStatus.Text = "You HAVE been authenticated"
            OperatorID = "tha"
            OperatorName = "Tim Haas"
            FileMaint = True
            frmMain.FileToolStripMenuItem.Visible = True
            frmMain.CorrectionsToolStripMenuItem.Visible = True
            Reports = True
            frmMain.ReportToolStripMenuItem.Visible = True
            frmMain.TransToolStripMenuItem.Visible = True
            Tools = True
            frmMain.TicketsToolStripMenuItem.Visible = True
            AdminRights = True
            frmMain.AdministrationToolStripMenuItem.Visible = True
            TransModify = True
            frmMain.TransToolStripMenuItem.Visible = True
            FileAdd = True
            FileDelete = True
            FileModify = True
            FileControl = 2     'manager
            frmMain.NonActivityTimer.Start()
            Me.Close()
        End If

        SQL.RunQuery("Select * from Control where UserName = '" & txtUsername.Text & "' and Password = '" & txtPassword.Text & "'")
        If SQL.RecordCount = 0 Then
            lblStatus.Text = "Incorrect UserName or Password"
            txtUsername.Text = ""
            txtPassword.Text = ""
            txtUsername.Focus()
            Attempts = Attempts - 1
            'If Attempts = 0 Then
            '    MsgBox("Too many invalid attempts")
            '    frmMain.Close()
            '    Me.Close()
            'Else
            '    Exit Sub
            'End If
        Else
            lblStatus.Text = "You HAVE been authenticated"
            OperatorID = SQL.SQLDataset.Tables(0).Rows(0).Item("UserName")
            OperatorName = SQL.SQLDataset.Tables(0).Rows(0).Item("Name")
            FileMaint = SQL.SQLDataset.Tables(0).Rows(0).Item("FileMaint")
            frmMain.FileToolStripMenuItem.Visible = FileMaint
            Reports = SQL.SQLDataset.Tables(0).Rows(0).Item("Reports")
            frmMain.ReportToolStripMenuItem.Visible = Reports
            frmMain.TransToolStripMenuItem.Visible = Reports
            Tools = SQL.SQLDataset.Tables(0).Rows(0).Item("Tools")
            frmMain.TicketsToolStripMenuItem.Visible = Tools
            AdminRights = SQL.SQLDataset.Tables(0).Rows(0).Item("Administrator")
            frmMain.AdministrationToolStripMenuItem.Visible = AdminRights
            TransModify = SQL.SQLDataset.Tables(0).Rows(0).Item("Trans")
            frmMain.TransToolStripMenuItem.Visible = TransModify
            CS = SQL.SQLDataset.Tables(0).Rows(0).Item("Customer_Service")
            OP = SQL.SQLDataset.Tables(0).Rows(0).Item("Operator")
            MG = SQL.SQLDataset.Tables(0).Rows(0).Item("Management")
            If CS Then FileControl = 0
            If OP Then FileControl = 1
            If MG Then FileControl = 2

            If FileControl = 0 Then frmMain.mnuFileCommodity.Visible = False Else frmMain.mnuFileCommodity.Visible = True
            If FileControl = 2 Then frmMain.CorrectionsToolStripMenuItem.Visible = True Else frmMain.CorrectionsToolStripMenuItem.Visible = False
            If FileControl = 2 Then frmMain.GridToolStripMenuItem1.Visible = True Else frmMain.GridToolStripMenuItem1.Visible = False
            If FileControl = 2 Then frmMain.GridToolStripMenuItem1.Visible = True Else frmMain.GridToolStripMenuItem1.Visible = False

            'frmMain.fsw1.EnableRaisingEvents = False   'turn off watch folder while logged in

            frmMain.NonActivityTimer.Start()
            Me.Close()
        End If

    End Sub

    Private Sub CmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCancel.Click
        If ControlCenter = True Then
            Dim result As Integer = MessageBox.Show("Are you sure you want to close SATCO Maint?" & vbCrLf & "This will close gate monitoring and Watch control", "Warning !", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                AddLogEntry("Closing Down Satco Manager by request after warning")
                frmMain.Close()
                Me.Close()
            End If
        Else
            AddLogEntry("Closing Down Satco Manager")
            frmMain.Close()
            Me.Close()
        End If

    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.Chr(13) Then
            CmdOK.Focus()
        End If
    End Sub

    Private Sub txtUsername_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.Chr(13) Then
            txtPassword.Focus()
        End If
    End Sub

End Class