﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SiteList
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
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(13, 13)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(241, 400)
        Me.CheckedListBox1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Calibri", 14.25!)
        Me.Button1.Location = New System.Drawing.Point(13, 419)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(241, 33)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "ثبت"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'SiteList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(266, 459)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckedListBox1)
        Me.Name = "SiteList"
        Me.Text = "SiteList"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents Button1 As Button
End Class
