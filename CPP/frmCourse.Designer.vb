﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCourse
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
        Dim COURSE_IDLabel As System.Windows.Forms.Label
        Dim DESCRIPTIONLabel As System.Windows.Forms.Label
        Dim UNITSLabel As System.Windows.Forms.Label
        Me.CPP_COURSESDataGridView = New System.Windows.Forms.DataGridView()
        Me.COURSE_IDTextBox = New System.Windows.Forms.TextBox()
        Me.DESCRIPTIONTextBox = New System.Windows.Forms.TextBox()
        Me.UNITSTextBox = New System.Windows.Forms.TextBox()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.gbCourseList = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.gbCourseDetail = New System.Windows.Forms.GroupBox()
        COURSE_IDLabel = New System.Windows.Forms.Label()
        DESCRIPTIONLabel = New System.Windows.Forms.Label()
        UNITSLabel = New System.Windows.Forms.Label()
        CType(Me.CPP_COURSESDataGridView,System.ComponentModel.ISupportInitialize).BeginInit
        Me.gbCourseList.SuspendLayout
        Me.gbCourseDetail.SuspendLayout
        Me.SuspendLayout
        '
        'COURSE_IDLabel
        '
        COURSE_IDLabel.AutoSize = true
        COURSE_IDLabel.Location = New System.Drawing.Point(27, 42)
        COURSE_IDLabel.Name = "COURSE_IDLabel"
        COURSE_IDLabel.Size = New System.Drawing.Size(69, 13)
        COURSE_IDLabel.TabIndex = 2
        COURSE_IDLabel.Text = "COURSE ID:"
        '
        'DESCRIPTIONLabel
        '
        DESCRIPTIONLabel.AutoSize = true
        DESCRIPTIONLabel.Location = New System.Drawing.Point(27, 68)
        DESCRIPTIONLabel.Name = "DESCRIPTIONLabel"
        DESCRIPTIONLabel.Size = New System.Drawing.Size(83, 13)
        DESCRIPTIONLabel.TabIndex = 4
        DESCRIPTIONLabel.Text = "DESCRIPTION:"
        '
        'UNITSLabel
        '
        UNITSLabel.AutoSize = true
        UNITSLabel.Location = New System.Drawing.Point(27, 94)
        UNITSLabel.Name = "UNITSLabel"
        UNITSLabel.Size = New System.Drawing.Size(43, 13)
        UNITSLabel.TabIndex = 6
        UNITSLabel.Text = "UNITS:"
        '
        'CPP_COURSESDataGridView
        '
        Me.CPP_COURSESDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CPP_COURSESDataGridView.Location = New System.Drawing.Point(41, 34)
        Me.CPP_COURSESDataGridView.Name = "CPP_COURSESDataGridView"
        Me.CPP_COURSESDataGridView.Size = New System.Drawing.Size(541, 220)
        Me.CPP_COURSESDataGridView.TabIndex = 1
        '
        'COURSE_IDTextBox
        '
        Me.COURSE_IDTextBox.Location = New System.Drawing.Point(116, 39)
        Me.COURSE_IDTextBox.Name = "COURSE_IDTextBox"
        Me.COURSE_IDTextBox.Size = New System.Drawing.Size(100, 20)
        Me.COURSE_IDTextBox.TabIndex = 3
        '
        'DESCRIPTIONTextBox
        '
        Me.DESCRIPTIONTextBox.Location = New System.Drawing.Point(116, 65)
        Me.DESCRIPTIONTextBox.Name = "DESCRIPTIONTextBox"
        Me.DESCRIPTIONTextBox.Size = New System.Drawing.Size(351, 20)
        Me.DESCRIPTIONTextBox.TabIndex = 5
        '
        'UNITSTextBox
        '
        Me.UNITSTextBox.Location = New System.Drawing.Point(116, 91)
        Me.UNITSTextBox.Name = "UNITSTextBox"
        Me.UNITSTextBox.Size = New System.Drawing.Size(100, 20)
        Me.UNITSTextBox.TabIndex = 7
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(316, 275)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(75, 23)
        Me.btnFind.TabIndex = 22
        Me.btnFind.Text = "Find"
        Me.btnFind.UseVisualStyleBackColor = true
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(220, 275)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 21
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = true
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(128, 275)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 20
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = true
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(38, 275)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 19
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = true
        '
        'gbCourseList
        '
        Me.gbCourseList.Controls.Add(Me.CPP_COURSESDataGridView)
        Me.gbCourseList.Controls.Add(Me.btnFind)
        Me.gbCourseList.Controls.Add(Me.btnDelete)
        Me.gbCourseList.Controls.Add(Me.btnAdd)
        Me.gbCourseList.Controls.Add(Me.btnUpdate)
        Me.gbCourseList.Location = New System.Drawing.Point(36, 266)
        Me.gbCourseList.Name = "gbCourseList"
        Me.gbCourseList.Size = New System.Drawing.Size(607, 322)
        Me.gbCourseList.TabIndex = 23
        Me.gbCourseList.TabStop = false
        Me.gbCourseList.Text = "Course List Information"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(136, 142)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 26
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(35, 142)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 25
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = true
        '
        'gbCourseDetail
        '
        Me.gbCourseDetail.Controls.Add(Me.btnCancel)
        Me.gbCourseDetail.Controls.Add(Me.btnSave)
        Me.gbCourseDetail.Controls.Add(COURSE_IDLabel)
        Me.gbCourseDetail.Controls.Add(Me.COURSE_IDTextBox)
        Me.gbCourseDetail.Controls.Add(DESCRIPTIONLabel)
        Me.gbCourseDetail.Controls.Add(Me.DESCRIPTIONTextBox)
        Me.gbCourseDetail.Controls.Add(UNITSLabel)
        Me.gbCourseDetail.Controls.Add(Me.UNITSTextBox)
        Me.gbCourseDetail.Location = New System.Drawing.Point(36, 45)
        Me.gbCourseDetail.Name = "gbCourseDetail"
        Me.gbCourseDetail.Size = New System.Drawing.Size(607, 215)
        Me.gbCourseDetail.TabIndex = 27
        Me.gbCourseDetail.TabStop = false
        Me.gbCourseDetail.Text = "Course Information"
        '
        'frmCourse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 637)
        Me.Controls.Add(Me.gbCourseDetail)
        Me.Controls.Add(Me.gbCourseList)
        Me.Name = "frmCourse"
        Me.Text = "CPP COURSE INFORMATION"
        CType(Me.CPP_COURSESDataGridView,System.ComponentModel.ISupportInitialize).EndInit
        Me.gbCourseList.ResumeLayout(false)
        Me.gbCourseDetail.ResumeLayout(false)
        Me.gbCourseDetail.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents CPP_COURSESDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents COURSE_IDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DESCRIPTIONTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UNITSTextBox As System.Windows.Forms.TextBox
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents gbCourseList As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents gbCourseDetail As System.Windows.Forms.GroupBox
End Class
