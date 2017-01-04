Imports System.Data.SqlClient
Public Class SiteManager
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
        Left = (screenWidth - Width) / 2
        Top = (screenHeight - Height) / 2
        Text = "مدیریت سایت ها"
        LoadAllWebsites()
        TextBox1.Focus()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListBox1.Items.Add(ConvertToURL(TextBox1.Text))
        TextBox1.Text = ""
    End Sub
    Private Function ConvertToURL(address As String)
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
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim count, i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(i) Then count += 1
        Next
        If (count > 0) Then
            TextBox1.Text = ListBox1.SelectedItem.ToString
            ListBox1.Items.Remove(ListBox1.SelectedItem)
        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim count, i As Integer
        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(i) Then count += 1
        Next
        If (count > 0) Then ListBox1.Items.Remove(ListBox1.SelectedItem)
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim i As Integer
        myConn = New SqlConnection("Data Source=HATAMIARASH7;Initial Catalog=Crawler;User ID=sa;Password=3920512197;")
        myCmd = myConn.CreateCommand
        myCmd.CommandText = "drop table if exists site_list;create table site_list(site_address nvarchar(200) not null primary key);"
        myConn.Open()
        myCmd.ExecuteNonQuery()
        myConn.Close()
        For i = 0 To ListBox1.Items.Count - 1
            Dim address As String = ListBox1.Items.Item(i).ToString()
            SaveAllWebsites(address)
        Next
        MsgBox("داده ها ذخیره شد")
        Close()
    End Sub
    Private Sub SaveAllWebsites(address As String)
        Dim query As String = String.Empty
        myConn = New SqlConnection("Data Source=HATAMIARASH7;Initial Catalog=Crawler;User ID=sa;Password=3920512197;")
        query &= "INSERT INTO site_list (site_address)"
        query &= "VALUES (@address)"
        Using myConn
            Using comm As New SqlCommand
                With comm
                    .Connection = myConn
                    .CommandType = CommandType.Text
                    .CommandText = query
                    .Parameters.AddWithValue("@address", address)
                End With
                Try
                    myConn.Open()
                    comm.ExecuteNonQuery()
                    myConn.Close()
                Catch ex As SqlException
                    MsgBox("Error : " & ex.Message.ToString())
                End Try
            End Using
        End Using
    End Sub
    Private Sub LoadAllWebsites()
        myConn = New SqlConnection("Data Source=HATAMIARASH7;Initial Catalog=Crawler;User ID=sa;Password=3920512197;")
        myCmd = myConn.CreateCommand
        myCmd.CommandText = "SELECT site_address FROM site_list"
        myConn.Open()
        myReader = myCmd.ExecuteReader()
        Do While myReader.Read()
            ListBox1.Items.Add(ConvertToURL(myReader.GetString(0)))
        Loop
        myReader.Close()
        myConn.Close()
    End Sub
    Public Sub ShowMe(Theme As String)
        If Theme.Equals("BLACK") Then
            Dim ctrl As Control
            For Each ctrl In Controls
                If (TypeOf ctrl Is Label) Then
                    ctrl.BackColor = Main._COLOR1
                    ctrl.ForeColor = Main._WHITE
                End If
                BackColor = Main._COLOR1
            Next
        End If
        Show()
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.KeyPress
        If e.Equals("13") Then
            Me.Text = "dddd"
        End If
    End Sub
End Class