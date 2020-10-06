<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBase
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                'If varfrmИзмеренныеПараметры IsNot Nothing Then
                '    varfrmИзмеренныеПараметры.Dispose()
                '    varfrmИзмеренныеПараметры = Nothing
                'End If
                'If varfrmНастроечныеПараметры IsNot Nothing Then
                '    varfrmНастроечныеПараметры.Dispose()
                '    varfrmНастроечныеПараметры = Nothing
                'End If
                'If varfrmРасчетныеПараметры IsNot Nothing Then
                '    varfrmРасчетныеПараметры.Dispose()
                '    varfrmРасчетныеПараметры = Nothing
                'End If
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
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBase))
        Me.WindowManagerPanel1 = New MDIWindowManager.WindowManagerPanel()
        Me.SuspendLayout()
        '
        'WindowManagerPanel1
        '
        Me.WindowManagerPanel1.AllowUserVerticalRepositioning = False
        Me.WindowManagerPanel1.AutoDetectMdiChildWindows = True
        Me.WindowManagerPanel1.AutoHide = False
        Me.WindowManagerPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.WindowManagerPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.WindowManagerPanel1.ButtonRenderMode = MDIWindowManager.ButtonRenderMode.Professional
        Me.WindowManagerPanel1.DisableCloseAction = False
        Me.WindowManagerPanel1.DisableHTileAction = False
        Me.WindowManagerPanel1.DisablePopoutAction = False
        Me.WindowManagerPanel1.DisableTileAction = False
        Me.WindowManagerPanel1.EnableTabPaintEvent = False
        Me.WindowManagerPanel1.Location = New System.Drawing.Point(2, 2)
        Me.WindowManagerPanel1.MinMode = False
        Me.WindowManagerPanel1.Name = "WindowManagerPanel1"
        Me.WindowManagerPanel1.Orientation = MDIWindowManager.WindowManagerOrientation.Top
        Me.WindowManagerPanel1.ShowCloseButton = False
        Me.WindowManagerPanel1.ShowIcons = True
        Me.WindowManagerPanel1.ShowLayoutButtons = True
        Me.WindowManagerPanel1.ShowTitle = True
        Me.WindowManagerPanel1.Size = New System.Drawing.Size(644, 56)
        Me.WindowManagerPanel1.Style = MDIWindowManager.TabStyle.AngledHiliteTabs
        Me.WindowManagerPanel1.TabIndex = 42
        Me.WindowManagerPanel1.TabRenderMode = MDIWindowManager.TabsProvider.Standard
        Me.WindowManagerPanel1.Text = "Выбрать окно"
        Me.WindowManagerPanel1.TitleBackColor = System.Drawing.Color.Lavender
        Me.WindowManagerPanel1.TitleForeColor = System.Drawing.SystemColors.MenuText
        '
        'FrmBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 292)
        Me.Controls.Add(Me.WindowManagerPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "FrmBase"
        Me.Text = "BaseForm"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents WindowManagerPanel1 As MDIWindowManager.WindowManagerPanel
End Class
