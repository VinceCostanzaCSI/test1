<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrderUploadSA
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPO = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtOrderDate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.txtConsigneeID = New System.Windows.Forms.TextBox()
        Me.txtAnalysis = New System.Windows.Forms.TextBox()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.txtShipDate = New System.Windows.Forms.TextBox()
        Me.txtConsigneeName = New System.Windows.Forms.TextBox()
        Me.txtRelease = New System.Windows.Forms.TextBox()
        Me.DGV1 = New System.Windows.Forms.DataGridView()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.LoadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.CloseTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTank = New System.Windows.Forms.TextBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtTank)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtPO)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtOrderDate)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtQuantity)
        Me.GroupBox2.Controls.Add(Me.txtConsigneeID)
        Me.GroupBox2.Controls.Add(Me.txtAnalysis)
        Me.GroupBox2.Controls.Add(Me.txtProduct)
        Me.GroupBox2.Controls.Add(Me.txtShipDate)
        Me.GroupBox2.Controls.Add(Me.txtConsigneeName)
        Me.GroupBox2.Controls.Add(Me.txtRelease)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 281)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(432, 289)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SA Fields"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(35, 255)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(25, 13)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "PO:"
        '
        'txtPO
        '
        Me.txtPO.Location = New System.Drawing.Point(94, 252)
        Me.txtPO.Name = "txtPO"
        Me.txtPO.Size = New System.Drawing.Size(185, 20)
        Me.txtPO.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(34, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Order Date:"
        '
        'txtOrderDate
        '
        Me.txtOrderDate.Location = New System.Drawing.Point(94, 19)
        Me.txtOrderDate.Name = "txtOrderDate"
        Me.txtOrderDate.Size = New System.Drawing.Size(133, 20)
        Me.txtOrderDate.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(34, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(26, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Qty:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(34, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Ship To:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(35, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Analysis:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(35, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Product:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(35, 229)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Ship Date:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Consignee:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 205)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Release:"
        '
        'txtQuantity
        '
        Me.txtQuantity.Location = New System.Drawing.Point(94, 97)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(70, 20)
        Me.txtQuantity.TabIndex = 6
        '
        'txtConsigneeID
        '
        Me.txtConsigneeID.Location = New System.Drawing.Point(94, 45)
        Me.txtConsigneeID.Name = "txtConsigneeID"
        Me.txtConsigneeID.Size = New System.Drawing.Size(133, 20)
        Me.txtConsigneeID.TabIndex = 5
        '
        'txtAnalysis
        '
        Me.txtAnalysis.Location = New System.Drawing.Point(94, 149)
        Me.txtAnalysis.Name = "txtAnalysis"
        Me.txtAnalysis.Size = New System.Drawing.Size(70, 20)
        Me.txtAnalysis.TabIndex = 4
        '
        'txtProduct
        '
        Me.txtProduct.Location = New System.Drawing.Point(94, 123)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.Size = New System.Drawing.Size(185, 20)
        Me.txtProduct.TabIndex = 3
        '
        'txtShipDate
        '
        Me.txtShipDate.Location = New System.Drawing.Point(94, 226)
        Me.txtShipDate.Name = "txtShipDate"
        Me.txtShipDate.Size = New System.Drawing.Size(133, 20)
        Me.txtShipDate.TabIndex = 2
        '
        'txtConsigneeName
        '
        Me.txtConsigneeName.Location = New System.Drawing.Point(94, 71)
        Me.txtConsigneeName.Name = "txtConsigneeName"
        Me.txtConsigneeName.Size = New System.Drawing.Size(293, 20)
        Me.txtConsigneeName.TabIndex = 1
        '
        'txtRelease
        '
        Me.txtRelease.Location = New System.Drawing.Point(94, 200)
        Me.txtRelease.Name = "txtRelease"
        Me.txtRelease.Size = New System.Drawing.Size(133, 20)
        Me.txtRelease.TabIndex = 0
        '
        'DGV1
        '
        Me.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV1.Location = New System.Drawing.Point(12, 128)
        Me.DGV1.Name = "DGV1"
        Me.DGV1.Size = New System.Drawing.Size(701, 147)
        Me.DGV1.TabIndex = 26
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(120, 593)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(56, 20)
        Me.lblMsg.TabIndex = 25
        Me.lblMsg.Text = "Status"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(246, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(226, 25)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "SA Upload Information"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(124, 53)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(451, 69)
        Me.ListBox1.TabIndex = 23
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(547, 551)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(113, 42)
        Me.cmdExit.TabIndex = 22
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'LoadTimer
        '
        '
        'CloseTimer
        '
        Me.CloseTimer.Interval = 5000
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(35, 178)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Tank:"
        '
        'txtTank
        '
        Me.txtTank.Location = New System.Drawing.Point(94, 175)
        Me.txtTank.Name = "txtTank"
        Me.txtTank.Size = New System.Drawing.Size(40, 20)
        Me.txtTank.TabIndex = 18
        '
        'frmOrderUploadSA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 631)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DGV1)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.cmdExit)
        Me.Name = "frmOrderUploadSA"
        Me.Text = "frmOrderUploadSA"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DGV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents txtConsigneeID As TextBox
    Friend WithEvents txtAnalysis As TextBox
    Friend WithEvents txtProduct As TextBox
    Friend WithEvents txtShipDate As TextBox
    Friend WithEvents txtConsigneeName As TextBox
    Friend WithEvents txtRelease As TextBox
    Friend WithEvents DGV1 As DataGridView
    Friend WithEvents lblMsg As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents cmdExit As Button
    Friend WithEvents LoadTimer As Timer
    Friend WithEvents CloseTimer As Timer
    Friend WithEvents Label8 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents txtOrderDate As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtPO As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents txtTank As TextBox
End Class
