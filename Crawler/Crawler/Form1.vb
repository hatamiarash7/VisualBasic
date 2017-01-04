Public Class Main
    Public _BLACK As Color = Color.FromArgb(0, 0, 0)
    Public _WHITE As Color = Color.FromArgb(255, 255, 255)
    Public _COLOR1 As Color = Color.FromArgb(41, 63, 90)
    Public Theme As String = "DEFAULT"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Width = 1100
        Height = 650
        Left = (screenWidth - Width) / 2
        Top = (screenHeight - Height) / 2
        Text = "پویشگر"
    End Sub
    Private Sub ThemeBlack_Click(sender As Object, e As EventArgs) Handles ThemeBlack.Click
        Dim ctrl As Control
        For Each ctrl In Controls
            If (TypeOf ctrl Is Label) Then
                ctrl.BackColor = _COLOR1
                ctrl.ForeColor = _WHITE
            End If
            BackColor = _COLOR1
        Next
        Theme = "BLACK"
    End Sub
    Private Sub ThemeWhite_Click(sender As Object, e As EventArgs) Handles ThemeWhite.Click
        Dim ctrl As Control
        For Each ctrl In Controls
            If (TypeOf ctrl Is Label) Then
                ctrl.BackColor = _WHITE
                ctrl.ForeColor = _BLACK
            End If
            BackColor = _WHITE
        Next
        Theme = "WHITE"
    End Sub
    Private Sub لیستسایتهاToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles لیستسایتهاToolStripMenuItem.Click
        SiteManager.ShowMe(Theme)
    End Sub
End Class