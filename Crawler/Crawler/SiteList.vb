Public Class SiteList
    Private Color As Theme = New Theme()
    Private sql As SQL = New SQL()
    Private Sub SiteList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Left = (screenWidth - Width) / 2
        Top = (screenHeight - Height) / 2
        Text = "سایت ها"
        LoadAllData()
    End Sub
    ''' <summary>
    ''' Read From Database
    ''' </summary>
    Private Sub LoadAllData()
        sql._QUERY = "SELECT * FROM sites"
        sql._DATABASE = "Crawler"
        Dim reader As List(Of Site) = sql.Execute_Read_Site()
        Dim i As Integer
        For i = 0 To reader.Count() - 1
            Dim site As Site = reader.Item(i)
            CheckedListBox1.Items.Add(SiteManager.ConvertToURL(site.address))
        Next
    End Sub
    ''' <summary>
    ''' Show This Form With Parent Theme
    ''' </summary>
    Public Sub ShowMe(Theme As String)
        If Theme.Equals("DARK") Then
            DarkTheme()
        ElseIf Theme.Equals("LIGHT") Then
            LightTheme()
        End If
        Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i As Integer
        For i = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(i) Then Main.ListBox1.Items.Add(CheckedListBox1.Items.Item(i).ToString())
        Next
        Main.GroupBox1.Show()
        Main.Button2.Enabled = True
        Close()
    End Sub
    Public Sub LightTheme()
        Dim ctrl As Control
        For Each ctrl In Controls
            Color.SetLightTheme(ctrl)
        Next
        BackColor = Color._WHITE
    End Sub
    Public Sub DarkTheme()
        Dim ctrl As Control
        For Each ctrl In Controls
            Color.SetDarkTheme(ctrl)
        Next
        BackColor = Color._ROYALBLUE
    End Sub
End Class