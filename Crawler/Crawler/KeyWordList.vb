Public Class KeyWordList
    Private sql As SQL = New SQL()
    Private Color As Theme = New Theme()
    Private Sub KeyWordList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Left = (screenWidth - Width) / 2
        Top = (screenHeight - Height) / 2
        Text = "لغت ها"
        LoadAllData()
    End Sub
    ''' <summary>
    ''' Read From Database
    ''' </summary>
    Private Sub LoadAllData()
        sql._QUERY = "SELECT * FROM keywords"
        sql._DATABASE = "Crawler"
        Dim reader As List(Of KeyWord) = sql.Execute_Read_KeyWord()
        Dim i As Integer
        For i = 0 To reader.Count() - 1
            Dim word As KeyWord = reader.Item(i)
            CheckedListBox1.Items.Add(word.word)
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
            If CheckedListBox1.GetItemChecked(i) Then Main.ListBox2.Items.Add(CheckedListBox1.Items.Item(i).ToString())
        Next
        Main.GroupBox2.Show()
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