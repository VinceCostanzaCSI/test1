<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCommodityMaint
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
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdFind = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdEdit = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdPrevious = New System.Windows.Forms.Button()
        Me.cmdFirst = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.optTypeBN = New System.Windows.Forms.RadioButton()
        Me.optTypeMO = New System.Windows.Forms.RadioButton()
        Me.optTypeUC = New System.Windows.Forms.RadioButton()
        Me.optTypeSA = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtVariableWt = New System.Windows.Forms.TextBox()
        Me.txtDescription5 = New System.Windows.Forms.TextBox()
        Me.txtDescription4 = New System.Windows.Forms.TextBox()
        Me.txtDescription3 = New System.Windows.Forms.TextBox()
        Me.txtDescription2 = New System.Windows.Forms.TextBox()
        Me.txtDescription1 = New System.Windows.Forms.TextBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optNo = New System.Windows.Forms.RadioButton()
        Me.optYes = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblDesc5 = New System.Windows.Forms.Label()
        Me.lblDesc4 = New System.Windows.Forms.Label()
        Me.lblDesc3 = New System.Windows.Forms.Label()
        Me.lblDesc2 = New System.Windows.Forms.Label()
        Me.lblDesc1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LoadTimer = New System.Windows.Forms.Timer(Me.components)
        Me.optType0 = New System.Windows.Forms.RadioButton()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.cmdGrid = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.Location = New System.Drawing.Point(624, 593)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(95, 48)
        Me.cmdExit.TabIndex = 39
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdFind
        '
        Me.cmdFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFind.Location = New System.Drawing.Point(624, 305)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(95, 48)
        Me.cmdFind.TabIndex = 38
        Me.cmdFind.Text = "Find"
        Me.cmdFind.UseVisualStyleBackColor = True
        '
        'cmdDelete
        '
        Me.cmdDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.Location = New System.Drawing.Point(624, 236)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(95, 48)
        Me.cmdDelete.TabIndex = 37
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = True
        Me.cmdDelete.Visible = False
        '
        'cmdEdit
        '
        Me.cmdEdit.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.Location = New System.Drawing.Point(624, 165)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.Size = New System.Drawing.Size(95, 48)
        Me.cmdEdit.TabIndex = 36
        Me.cmdEdit.Text = "Edit"
        Me.cmdEdit.UseVisualStyleBackColor = True
        Me.cmdEdit.Visible = False
        '
        'cmdAdd
        '
        Me.cmdAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.Location = New System.Drawing.Point(624, 95)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(95, 48)
        Me.cmdAdd.TabIndex = 35
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        Me.cmdAdd.Visible = False
        '
        'cmdNext
        '
        Me.cmdNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNext.Location = New System.Drawing.Point(388, 593)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(95, 48)
        Me.cmdNext.TabIndex = 33
        Me.cmdNext.Text = "Next"
        Me.cmdNext.UseVisualStyleBackColor = True
        '
        'cmdPrevious
        '
        Me.cmdPrevious.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrevious.Location = New System.Drawing.Point(266, 593)
        Me.cmdPrevious.Name = "cmdPrevious"
        Me.cmdPrevious.Size = New System.Drawing.Size(95, 48)
        Me.cmdPrevious.TabIndex = 32
        Me.cmdPrevious.Text = "Previous"
        Me.cmdPrevious.UseVisualStyleBackColor = True
        '
        'cmdFirst
        '
        Me.cmdFirst.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFirst.Location = New System.Drawing.Point(149, 593)
        Me.cmdFirst.Name = "cmdFirst"
        Me.cmdFirst.Size = New System.Drawing.Size(95, 48)
        Me.cmdFirst.TabIndex = 31
        Me.cmdFirst.Text = "First"
        Me.cmdFirst.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(119, 25)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(430, 37)
        Me.Label13.TabIndex = 30
        Me.Label13.Text = "Commodity File Maintenance"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtVariableWt)
        Me.GroupBox1.Controls.Add(Me.txtDescription5)
        Me.GroupBox1.Controls.Add(Me.txtDescription4)
        Me.GroupBox1.Controls.Add(Me.txtDescription3)
        Me.GroupBox1.Controls.Add(Me.txtDescription2)
        Me.GroupBox1.Controls.Add(Me.txtDescription1)
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblDesc5)
        Me.GroupBox1.Controls.Add(Me.lblDesc4)
        Me.GroupBox1.Controls.Add(Me.lblDesc3)
        Me.GroupBox1.Controls.Add(Me.lblDesc2)
        Me.GroupBox1.Controls.Add(Me.lblDesc1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(27, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(555, 479)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optTypeBN)
        Me.GroupBox3.Controls.Add(Me.optTypeMO)
        Me.GroupBox3.Controls.Add(Me.optTypeUC)
        Me.GroupBox3.Controls.Add(Me.optTypeSA)
        Me.GroupBox3.Enabled = False
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(215, 290)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(313, 57)
        Me.GroupBox3.TabIndex = 22
        Me.GroupBox3.TabStop = False
        '
        'optTypeBN
        '
        Me.optTypeBN.AutoSize = True
        Me.optTypeBN.Location = New System.Drawing.Point(217, 19)
        Me.optTypeBN.Name = "optTypeBN"
        Me.optTypeBN.Size = New System.Drawing.Size(54, 28)
        Me.optTypeBN.TabIndex = 3
        Me.optTypeBN.TabStop = True
        Me.optTypeBN.Text = "BN"
        Me.optTypeBN.UseVisualStyleBackColor = True
        '
        'optTypeMO
        '
        Me.optTypeMO.AutoSize = True
        Me.optTypeMO.Location = New System.Drawing.Point(146, 19)
        Me.optTypeMO.Name = "optTypeMO"
        Me.optTypeMO.Size = New System.Drawing.Size(59, 28)
        Me.optTypeMO.TabIndex = 2
        Me.optTypeMO.TabStop = True
        Me.optTypeMO.Text = "MO"
        Me.optTypeMO.UseVisualStyleBackColor = True
        '
        'optTypeUC
        '
        Me.optTypeUC.AutoSize = True
        Me.optTypeUC.Location = New System.Drawing.Point(75, 19)
        Me.optTypeUC.Name = "optTypeUC"
        Me.optTypeUC.Size = New System.Drawing.Size(54, 28)
        Me.optTypeUC.TabIndex = 1
        Me.optTypeUC.TabStop = True
        Me.optTypeUC.Text = "UC"
        Me.optTypeUC.UseVisualStyleBackColor = True
        '
        'optTypeSA
        '
        Me.optTypeSA.AutoSize = True
        Me.optTypeSA.Location = New System.Drawing.Point(16, 19)
        Me.optTypeSA.Name = "optTypeSA"
        Me.optTypeSA.Size = New System.Drawing.Size(53, 28)
        Me.optTypeSA.TabIndex = 0
        Me.optTypeSA.TabStop = True
        Me.optTypeSA.Text = "SA"
        Me.optTypeSA.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(122, 308)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 24)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Type:"
        '
        'txtVariableWt
        '
        Me.txtVariableWt.Enabled = False
        Me.txtVariableWt.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVariableWt.Location = New System.Drawing.Point(215, 242)
        Me.txtVariableWt.Name = "txtVariableWt"
        Me.txtVariableWt.Size = New System.Drawing.Size(99, 29)
        Me.txtVariableWt.TabIndex = 20
        '
        'txtDescription5
        '
        Me.txtDescription5.Enabled = False
        Me.txtDescription5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription5.Location = New System.Drawing.Point(215, 208)
        Me.txtDescription5.Name = "txtDescription5"
        Me.txtDescription5.Size = New System.Drawing.Size(313, 29)
        Me.txtDescription5.TabIndex = 19
        '
        'txtDescription4
        '
        Me.txtDescription4.Enabled = False
        Me.txtDescription4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription4.Location = New System.Drawing.Point(215, 173)
        Me.txtDescription4.Name = "txtDescription4"
        Me.txtDescription4.Size = New System.Drawing.Size(313, 29)
        Me.txtDescription4.TabIndex = 18
        '
        'txtDescription3
        '
        Me.txtDescription3.Enabled = False
        Me.txtDescription3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription3.Location = New System.Drawing.Point(215, 139)
        Me.txtDescription3.Name = "txtDescription3"
        Me.txtDescription3.Size = New System.Drawing.Size(313, 29)
        Me.txtDescription3.TabIndex = 17
        '
        'txtDescription2
        '
        Me.txtDescription2.Enabled = False
        Me.txtDescription2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription2.Location = New System.Drawing.Point(215, 102)
        Me.txtDescription2.Name = "txtDescription2"
        Me.txtDescription2.Size = New System.Drawing.Size(313, 29)
        Me.txtDescription2.TabIndex = 16
        '
        'txtDescription1
        '
        Me.txtDescription1.Enabled = False
        Me.txtDescription1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription1.Location = New System.Drawing.Point(215, 67)
        Me.txtDescription1.Name = "txtDescription1"
        Me.txtDescription1.Size = New System.Drawing.Size(313, 29)
        Me.txtDescription1.TabIndex = 15
        '
        'txtID
        '
        Me.txtID.Enabled = False
        Me.txtID.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtID.Location = New System.Drawing.Point(215, 32)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(99, 29)
        Me.txtID.TabIndex = 14
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optNo)
        Me.GroupBox2.Controls.Add(Me.optYes)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(215, 373)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(144, 57)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        '
        'optNo
        '
        Me.optNo.AutoSize = True
        Me.optNo.Location = New System.Drawing.Point(75, 19)
        Me.optNo.Name = "optNo"
        Me.optNo.Size = New System.Drawing.Size(53, 28)
        Me.optNo.TabIndex = 1
        Me.optNo.TabStop = True
        Me.optNo.Text = "No"
        Me.optNo.UseVisualStyleBackColor = True
        '
        'optYes
        '
        Me.optYes.AutoSize = True
        Me.optYes.Location = New System.Drawing.Point(9, 18)
        Me.optYes.Name = "optYes"
        Me.optYes.Size = New System.Drawing.Size(60, 28)
        Me.optYes.TabIndex = 0
        Me.optYes.TabStop = True
        Me.optYes.Text = "Yes"
        Me.optYes.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(122, 391)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 24)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Active:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(23, 245)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(148, 24)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Variable Weight:"
        '
        'lblDesc5
        '
        Me.lblDesc5.AutoSize = True
        Me.lblDesc5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc5.Location = New System.Drawing.Point(23, 211)
        Me.lblDesc5.Name = "lblDesc5"
        Me.lblDesc5.Size = New System.Drawing.Size(124, 24)
        Me.lblDesc5.TabIndex = 5
        Me.lblDesc5.Text = "Description 5:"
        '
        'lblDesc4
        '
        Me.lblDesc4.AutoSize = True
        Me.lblDesc4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc4.Location = New System.Drawing.Point(23, 176)
        Me.lblDesc4.Name = "lblDesc4"
        Me.lblDesc4.Size = New System.Drawing.Size(124, 24)
        Me.lblDesc4.TabIndex = 4
        Me.lblDesc4.Text = "Description 4:"
        '
        'lblDesc3
        '
        Me.lblDesc3.AutoSize = True
        Me.lblDesc3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc3.Location = New System.Drawing.Point(23, 142)
        Me.lblDesc3.Name = "lblDesc3"
        Me.lblDesc3.Size = New System.Drawing.Size(124, 24)
        Me.lblDesc3.TabIndex = 3
        Me.lblDesc3.Text = "Description 3:"
        '
        'lblDesc2
        '
        Me.lblDesc2.AutoSize = True
        Me.lblDesc2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc2.Location = New System.Drawing.Point(23, 105)
        Me.lblDesc2.Name = "lblDesc2"
        Me.lblDesc2.Size = New System.Drawing.Size(124, 24)
        Me.lblDesc2.TabIndex = 2
        Me.lblDesc2.Text = "Description 2:"
        '
        'lblDesc1
        '
        Me.lblDesc1.AutoSize = True
        Me.lblDesc1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc1.Location = New System.Drawing.Point(23, 70)
        Me.lblDesc1.Name = "lblDesc1"
        Me.lblDesc1.Size = New System.Drawing.Size(124, 24)
        Me.lblDesc1.TabIndex = 1
        Me.lblDesc1.Text = "Description 1:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(111, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID:"
        '
        'LoadTimer
        '
        '
        'optType0
        '
        Me.optType0.AutoSize = True
        Me.optType0.Location = New System.Drawing.Point(9, 18)
        Me.optType0.Name = "optType0"
        Me.optType0.Size = New System.Drawing.Size(53, 28)
        Me.optType0.TabIndex = 0
        Me.optType0.TabStop = True
        Me.optType0.Text = "SA"
        Me.optType0.UseVisualStyleBackColor = True
        '
        'cmdClear
        '
        Me.cmdClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.Location = New System.Drawing.Point(624, 380)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(95, 48)
        Me.cmdClear.TabIndex = 40
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.UseVisualStyleBackColor = True
        '
        'cmdGrid
        '
        Me.cmdGrid.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGrid.Location = New System.Drawing.Point(624, 464)
        Me.cmdGrid.Name = "cmdGrid"
        Me.cmdGrid.Size = New System.Drawing.Size(95, 58)
        Me.cmdGrid.TabIndex = 53
        Me.cmdGrid.Text = "Show Grid"
        Me.cmdGrid.UseVisualStyleBackColor = True
        '
        'frmCommodityMaint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 667)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdGrid)
        Me.Controls.Add(Me.cmdClear)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdFind)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.cmdPrevious)
        Me.Controls.Add(Me.cmdFirst)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmCommodityMaint"
        Me.Text = "frmCommodityMaint"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents cmdDelete As System.Windows.Forms.Button
    Friend WithEvents cmdEdit As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents cmdPrevious As System.Windows.Forms.Button
    Friend WithEvents cmdFirst As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents optTypeBN As System.Windows.Forms.RadioButton
    Friend WithEvents optTypeMO As System.Windows.Forms.RadioButton
    Friend WithEvents optTypeUC As System.Windows.Forms.RadioButton
    Friend WithEvents optTypeSA As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtVariableWt As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription5 As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription4 As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription3 As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDescription1 As System.Windows.Forms.TextBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optNo As System.Windows.Forms.RadioButton
    Friend WithEvents optYes As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblDesc5 As System.Windows.Forms.Label
    Friend WithEvents lblDesc4 As System.Windows.Forms.Label
    Friend WithEvents lblDesc3 As System.Windows.Forms.Label
    Friend WithEvents lblDesc2 As System.Windows.Forms.Label
    Friend WithEvents lblDesc1 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LoadTimer As System.Windows.Forms.Timer
    Friend WithEvents optType0 As System.Windows.Forms.RadioButton
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents cmdGrid As System.Windows.Forms.Button
End Class
