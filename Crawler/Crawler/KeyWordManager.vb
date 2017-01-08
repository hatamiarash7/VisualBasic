Imports System.Data.SqlClient
Public Class KeyWordManager
    Private sql As SQL = New SQL()
    Private Color As Theme = New Theme()
    Private Sub KeyWordManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Left = (screenWidth - Width) / 2
        Top = (screenHeight - Height) / 2
        Text = "مدیریت لغات"
        LoadAllData()
        TextBox1.TabIndex = 0
        TextBox2.TabIndex = 1
        Button1.TabIndex = 2
    End Sub
    ''' <summary>
    ''' Add Item To List
    ''' </summary>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveAllData()
    End Sub
    ''' <summary>
    ''' Edit Item From List
    ''' </summary>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim count = -1, i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(i) Or ListBox2.GetSelected(i) Then count = i
        Next
        If (count <> -1) Then
            TextBox1.Text = ListBox1.Items.Item(count)
            TextBox2.Text = ListBox2.Items.Item(count)
            ListBox1.Items.RemoveAt(count)
            ListBox2.Items.RemoveAt(count)
        End If
    End Sub
    ''' <summary>
    ''' Add All Items To ListBoxes
    ''' </summary>
    Private Sub SaveAllData()
        If (TextBox1.Text <> "") Then
            ListBox1.Items.Add(TextBox1.Text)
            If (TextBox2.Text = "") Then
                ListBox2.Items.Add("-")
            Else
                ListBox2.Items.Add(TextBox2.Text)
            End If
            TextBox1.Text = ""
            TextBox2.Text = ""
        Else
            MsgBox("لطفا لغت را وارد نمایید")
        End If
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
    ''' <summary>
    ''' Add To Database
    ''' </summary>
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim i As Integer
        sql._QUERY = "drop table if exists keywords;create table keywords(word nvarchar(100) NOT NULL PRIMARY KEY,category nvarchar(100) null);"
        sql._DATABASE = "Crawler"
        sql.Execute()
        Dim address As List(Of String) = New List(Of String)
        Dim category As List(Of String) = New List(Of String)
        For i = 0 To ListBox1.Items.Count - 1
            address.Add(ListBox1.Items.Item(i).ToString())
            category.Add(ListBox2.Items.Item(i).ToString())
        Next
        sql._QUERY = "INSERT INTO keywords (word, category) VALUES (@word, @category)"
        sql._DATABASE = "Crawler"
        sql.Execute_Write(address, category)
        MsgBox("داده ها ذخیره شد")
        Close()
    End Sub
    ''' <summary>
    ''' Remove Item From List
    ''' </summary>
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim count = -1, i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(i) Or ListBox2.GetSelected(i) Then count = i
        Next
        If (count <> -1) Then
            ListBox1.Items.RemoveAt(count)
            ListBox2.Items.RemoveAt(count)
        End If
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
            ListBox1.Items.Add(word.word)
            ListBox2.Items.Add(word.category)
        Next
    End Sub
    Private Sub Textbox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) = 13 Then SaveAllData()
    End Sub
    Private Sub Textbox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) = 13 Then SaveAllData()
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