<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTransactionMaint
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGVData = New System.Windows.Forms.DataGridView()
        Me.cmdCorrection = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdPrintBOL = New System.Windows.Forms.Button()
        Me.cboPrinters = New System.Windows.Forms.ComboBox()
        Me.cmdRail = New System.Windows.Forms.Button()
        CType(Me.DGVData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(446, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(305, 29)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Transaction Maintenance"
        '
        'cmdAdd
        '
        Me.cmdAdd.Location = New System.Drawing.Point(439, 509)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(69, 31)
        Me.cmdAdd.TabIndex = 27
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        Me.cmdAdd.Visible = False
        '
        'cmdModify
        '
        Me.cmdModify.Location = New System.Drawing.Point(530, 509)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.Size = New System.Drawing.Size(69, 31)
        Me.cmdModify.TabIndex = 25
        Me.cmdModify.Text = "Modify"
        Me.cmdModify.UseVisualStyleBackColor = True
        Me.cmdModify.Visible = False
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(1147, 507)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(71, 32)
        Me.cmdExit.TabIndex = 24
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Location = New System.Drawing.Point(84, 517)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(13, 13)
        Me.lblTotal.TabIndex = 23
        Me.lblTotal.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(41, 517)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Total :"
        '
        'DGVData
        '
        Me.DGVData.AllowUserToAddRows = False
        Me.DGVData.AllowUserToDeleteRows = False
        Me.DGVData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DGVData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVData.Location = New System.Drawing.Point(12, 48)
        Me.DGVData.Name = "DGVData"
        Me.DGVData.Size = New System.Drawing.Size(1224, 438)
        Me.DGVData.TabIndex = 21
        '
        'cmdCorrection
        '
        Me.cmdCorrection.Location = New System.Drawing.Point(319, 509)
        Me.cmdCorrection.Name = "cmdCorrection"
        Me.cmdCorrection.Size = New System.Drawing.Size(96, 31)
        Me.cmdCorrection.TabIndex = 30
        Me.cmdCorrection.Text = "Correction Form"
        Me.cmdCorrection.UseVisualStyleBackColor = True
        '
        'cmdPrint
        '
        Me.cmdPrint.Location = New System.Drawing.Point(652, 509)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.Size = New System.Drawing.Size(109, 31)
        Me.cmdPrint.TabIndex = 31
        Me.cmdPrint.Text = "Print Selected"
        Me.cmdPrint.UseVisualStyleBackColor = True
        '
        'cmdPrintBOL
        '
        Me.cmdPrintBOL.Location = New System.Drawing.Point(782, 509)
        Me.cmdPrintBOL.Name = "cmdPrintBOL"
        Me.cmdPrintBOL.Size = New System.Drawing.Size(107, 31)
        Me.cmdPrintBOL.TabIndex = 32
        Me.cmdPrintBOL.Text = "Print BOLs"
        Me.cmdPrintBOL.UseVisualStyleBackColor = True
        '
        'cboPrinters
        '
        Me.cboPrinters.FormattingEnabled = True
        Me.cboPrinters.Location = New System.Drawing.Point(904, 514)
        Me.cboPrinters.Name = "cboPrinters"
        Me.cboPrinters.Size = New System.Drawing.Size(227, 21)
        Me.cboPrinters.TabIndex = 33
        '
        'cmdRail
        '
        Me.cmdRail.Location = New System.Drawing.Point(170, 509)
        Me.cmdRail.Name = "cmdRail"
        Me.cmdRail.Size = New System.Drawing.Size(84, 31)
        Me.cmdRail.TabIndex = 34
        Me.cmdRail.Text = "Rail Detail"
        Me.cmdRail.UseVisualStyleBackColor = True
        '
        'frmTransactionMaint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1248, 561)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdRail)
        Me.Controls.Add(Me.cboPrinters)
        Me.Controls.Add(Me.cmdPrintBOL)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdCorrection)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdModify)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DGVData)
        Me.Name = "frmTransactionMaint"
        Me.Text = "frmTransactionMaint"
        CType(Me.DGVData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdModify As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DGVData As System.Windows.Forms.DataGridView
    Friend WithEvents cmdCorrection As System.Windows.Forms.Button
    Friend WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents cmdPrintBOL As Button
    Friend WithEvents cboPrinters As ComboBox
    Friend WithEvents cmdRail As Button
End Class
