Public Class SiteManager
    Private sql As SQL = New SQL()
    Private Color As Theme = New Theme()
    Private Sub SiteManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Left = (screenWidth - Width) / 2
        Top = (screenHeight - Height) / 2
        Text = "مدیریت سایت ها"
        LoadAllData()
        TextBox1.TabIndex = 0
        TextBox2.TabIndex = 1
        TextBox3.TabIndex = 2
        Button1.TabIndex = 3
    End Sub
    ''' <summary>
    ''' Add Item To List
    ''' </summary>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveAllData()
    End Sub
    ''' <summary>
    ''' Convert Normal Texts To Standard URL
    ''' </summary>
    Public Function ConvertToURL(address As String)
        If (Not address.StartsWith("http://www.")) Then
            If (Not address.StartsWith("http://")) Then
                If (Not address.StartsWith("www.")) Then
                    address = "http://" + address
                End If
            End If
        End If
        If (Not address.StartsWith("http://www")) Then
            If (address.StartsWith("www.")) Then
                address = Replace(address, "www.", "http://")
            End If
        End If
        Return address
    End Function
    ''' <summary>
    ''' Edit Item From List
    ''' </summary>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim count = -1, i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(i) Or ListBox2.GetSelected(i) Or ListBox3.GetSelected(i) Then count = i
        Next
        If (count <> -1) Then
            TextBox1.Text = ListBox1.Items.Item(count)
            TextBox2.Text = ListBox2.Items.Item(count)
            TextBox3.Text = ListBox3.Items.Item(count)
            ListBox1.Items.RemoveAt(count)
            ListBox2.Items.RemoveAt(count)
            ListBox3.Items.RemoveAt(count)
        End If
    End Sub
    ''' <summary>
    ''' Remove Item From List
    ''' </summary>
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim count = -1, i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(i) Or ListBox2.GetSelected(i) Or ListBox3.GetSelected(i) Then count = i
        Next
        If (count <> -1) Then
            ListBox1.Items.RemoveAt(count)
            ListBox2.Items.RemoveAt(count)
            ListBox3.Items.RemoveAt(count)
        End If
    End Sub
    ''' <summary>
    ''' Add To Database
    ''' </summary>
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim i As Integer
        sql._QUERY = "drop table if exists sites;create table sites(site nvarchar(200) NOT NULL PRIMARY KEY,category nvarchar(100) null,author nvarchar(100) null);"
        sql._DATABASE = "Crawler"
        sql.Execute()
        Dim address As List(Of String) = New List(Of String)
        Dim category As List(Of String) = New List(Of String)
        Dim author As List(Of String) = New List(Of String)
        For i = 0 To ListBox1.Items.Count - 1
            address.Add(ListBox1.Items.Item(i).ToString())
            category.Add(ListBox2.Items.Item(i).ToString())
            author.Add(ListBox3.Items.Item(i).ToString())
        Next
        sql._QUERY = "INSERT INTO sites (site, category, author) VALUES (@address, @category, @author)"
        sql._DATABASE = "Crawler"
        sql.Execute_Write(address, category, author)
        MsgBox("داده ها ذخیره شد")
        Close()
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
            ListBox1.Items.Add(ConvertToURL(site.address))
            ListBox2.Items.Add(site.category)
            ListBox3.Items.Add(site.author)
        Next
    End Sub
    ''' <summary>
    ''' Add All Items To ListBoxes
    ''' </summary>
    Private Sub SaveAllData()
        If (TextBox1.Text <> "") Then
            ListBox1.Items.Add(ConvertToURL(TextBox1.Text))
            If (TextBox2.Text = "") Then
                ListBox2.Items.Add("-")
            Else
                ListBox2.Items.Add(TextBox2.Text)
            End If
            If (TextBox3.Text = "") Then
                ListBox3.Items.Add("-")
            Else
                ListBox3.Items.Add(TextBox3.Text)
            End If
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
        Else
            MsgBox("لطفا آدرس را وارد نمایید")
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
    Private Sub Textbox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) = 13 Then SaveAllData()
    End Sub
    Private Sub Textbox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) = 13 Then SaveAllData()
    End Sub
    Private Sub Textbox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
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