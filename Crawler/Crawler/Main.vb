Public Class Main
    Private Color As Theme = New Theme()
    Public Theme As String = "DEFAULT"
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Width = 1100
        Height = 650
        Left = (screenWidth - Width) / 2
        Top = (screenHeight - Height) / 2
        Text = "پویشگر"
        GroupBox1.Hide()
        GroupBox2.Hide()
        Button2.Enabled = False
        LightTheme()
        Button1.FlatStyle = FlatStyle.Flat
        Button2.FlatStyle = FlatStyle.Flat
    End Sub
    Private Sub ThemeBlack_Click(sender As Object, e As EventArgs) Handles ThemeBlack.Click
        DarkTheme()
    End Sub
    Private Sub ThemeWhite_Click(sender As Object, e As EventArgs) Handles ThemeWhite.Click
        LightTheme()
    End Sub
    Private Sub لیستسایتهاToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles لیستسایتهاToolStripMenuItem.Click
        SiteManager.ShowMe(Theme)
    End Sub
    Private Sub لیستلغاتکلیدیToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles لیستلغاتکلیدیToolStripMenuItem.Click
        KeyWordManager.ShowMe(Theme)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SiteList.ShowMe(Theme)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        KeyWordList.ShowMe(Theme)
    End Sub
    Public Sub LightTheme()
        Dim ctrl As Control
        For Each ctrl In Controls
            Color.SetLightTheme(ctrl)
        Next
        BackColor = Color._WHITE
        Theme = "LIGHT"
    End Sub
    Public Sub DarkTheme()
        Dim ctrl As Control
        For Each ctrl In Controls
            Color.SetDarkTheme(ctrl)
        Next
        BackColor = Color._ROYALBLUE
        ListBox1.BackColor = Color._ROYALBLUE
        ListBox1.ForeColor = Color._GOLD
        ListBox2.BackColor = Color._ROYALBLUE
        ListBox2.ForeColor = Color._GOLD
        Theme = "DARK"
    End Sub
End Class