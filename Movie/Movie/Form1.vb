Imports System.Data.SqlClient
Imports System.IO
''' <summary>
''' Author : Arash Hatami
''' Email : hatamiarash7@gmail.com
''' Website : http://arash-hatami.ir
''' </summary>
Public Class Form1
    Inherits System.Windows.Forms.Form
    Private myConn As SqlConnection   'Connection To Database
    Private myCmd As SqlCommand       'Query Command
    Private myReader As SqlDataReader 'Database Reader
    Dim Sample As Movie = New Movie() 'Class For Saving Movie Info
    ''' <summary>
    ''' Form Loading Sub
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'clear labels text
        For Each label In {Label1, Label2, Label3, Label4, Label5, Label6, Label7, Label8}
            label.Text = ""
        Next
        'hide labels
        For Each label In {Label10, Label11, Label12, Label13, Label14, Label15, Label16, Label17, Label27}
            label.Hide()
        Next
        Button2.Hide()
        PictureBox1.Hide()
        Me.Text = "Archive Manager"
        'set form in top level of other windows
        Me.TopMost = True
        Me.TopLevel = True
    End Sub
    ''' <summary>
    ''' Call Finding Function
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'check exist status of movie in the database
        If Find_Movie(TextBox1.Text.ToString()) Then
            'show all labels
            For Each label In {Label10, Label11, Label12, Label13, Label14, Label15, Label16, Label17}
                label.Show()
            Next
            Button2.Show()
            'print movies info on labels
            Label1.Text = Sample.Name
            Label2.Text = Sample.Year
            Label3.Text = Sample.Director
            Label4.Text = Sample.Genre
            Label5.Text = Sample.Point.ToString()
            Label6.Text = Sample.Age.ToString()
            Label7.Text = Sample.Seen.ToString()
            Label8.Text = Sample.Code
            'movie's cover path
            Dim image_file As String = System.IO.Path.Combine("img/", Sample.Code & ".jpg")
            If File.Exists(image_file) Then 'check that this movie has cover or not
                PictureBox1.Show()
                PictureBox1.Image = Image.FromFile(image_file)
            Else 'if this move have not any cover , set default cover
                image_file = System.IO.Path.Combine("img/" & "null.jpg")
                PictureBox1.Show()
                PictureBox1.Image = Image.FromFile(image_file)
            End If
        Else 'if there isn't any movie with the given name
            'clear labels text
            For Each label In {Label1, Label2, Label3, Label4, Label5, Label6, Label7, Label8}
                label.Text = ""
            Next
            'hide labels
            For Each label In {Label10, Label11, Label12, Label13, Label14, Label15, Label16, Label17}
                label.Hide()
            Next
            Button2.Hide()
            PictureBox1.Hide()
            'show an error msgbox
            MsgBox(String.Format("There Isn't Any Film In Your Archive With This Name : {0}", TextBox1.Text.ToString()))
        End If
    End Sub
    ''' <summary>
    ''' Find Movie In Database
    ''' </summary>
    ''' <param name="Name">Movie's Name</param>
    ''' <returns>True/False For Finding The Movie</returns>
    Public Function Find_Movie(Name As String)
        'server/database connection
        myConn = New SqlConnection("Data Source=HATAMIARASH7;Initial Catalog=Movie;User ID=sa;Password=3920512197;")
        'query command
        myCmd = myConn.CreateCommand
        'set query text
        myCmd.CommandText = String.Format("SELECT M_Code, M_Name, M_Year, M_Genre, M_Director, M_Point, M_Age, M_Seen FROM Main WHERE M_Name='{0}'", Name)
        'open connection
        myConn.Open()
        'define a reader
        myReader = myCmd.ExecuteReader()
        'read tabel's rows
        Do While myReader.Read()
            Sample.Code = myReader.GetString(0)
            Sample.Name = myReader.GetString(1)
            Sample.Year = myReader.GetValue(2)
            Sample.Genre = myReader.GetString(3)
            Sample.Director = myReader.GetString(4)
            Sample.Point = myReader.GetValue(5)
            Sample.Age = myReader.GetValue(6)
            Sample.Seen = myReader.GetValue(7)
        Loop
        'check the reader find anything with the giver query text
        If myReader.HasRows Then
            Return True
        Else
            Return False
        End If
        'close reader
        myReader.Close()
        'close connection
        myConn.Close()
    End Function
    ''' <summary>
    ''' Add Movie To Database
    ''' </summary>
    ''' <param name="Name">Movie's Name</param>
    ''' <param name="Code">Movie's Code</param>
    ''' <param name="Year">Movie's Year</param>
    ''' <param name="Genre">Movie's Genre</param>
    ''' <param name="Director">Movie's Director</param>
    ''' <param name="Point">Movie's Point</param>
    ''' <param name="Age">Movie's Age</param>
    ''' <param name="Seen">Movie's Seen Status</param>
    ''' <param name="Exist">Movie's Exist Status</param>
    ''' <returns>True/Flse For Adding Movie To Database</returns>
    Public Function Add_Movie(Name As String, Code As String, Year As Integer, Genre As String, Director As String, Point As Decimal, Age As Integer, Seen As Boolean, Exist As Boolean)
        'define a seprate query, not default
        Dim query As String = String.Empty
        'set query text
        query &= "INSERT INTO Main (M_Code, M_Name, M_Year, M_Genre, M_Director, M_Point, M_Age, M_Exist, M_Seen)"
        query &= "VALUES (@code, @name, @year, @genre, @director, @point, @age, @exist, @seen)"
        'define new connection to server/database
        myConn = New SqlConnection("Data Source=HATAMIARASH7;Initial Catalog=Movie;User ID=sa;Password=3920512197;")
        Using myConn
            Using comm As New SqlCommand
                With comm
                    .Connection = myConn
                    .CommandType = CommandType.Text
                    .CommandText = query
                    'set parameters with function's parameters
                    .Parameters.AddWithValue("@code", Code)
                    .Parameters.AddWithValue("@name", Name)
                    .Parameters.AddWithValue("@year", Year)
                    .Parameters.AddWithValue("@genre", Genre)
                    .Parameters.AddWithValue("@director", Director)
                    .Parameters.AddWithValue("@point", Point)
                    .Parameters.AddWithValue("@age", Age)
                    .Parameters.AddWithValue("@exist", Exist)
                    .Parameters.AddWithValue("@seen", Seen)
                End With
                Try
                    myConn.Open()          'open connection
                    comm.ExecuteNonQuery() 'execute query
                    myConn.Close()         'close connection
                    Return True
                Catch ex As SqlException   'if there is any error related to sql
                    Label27.Text = "Error : " & ex.Message.ToString()
                    Return False
                End Try
            End Using
        End Using
    End Function
    ''' <summary>
    ''' Class For Save Movie's Info
    ''' </summary>
    Public Class Movie
        Public Name As String
        Public Year As String
        Public Genre As String
        Public Director As String
        Public Point As Single
        Public Age As Integer
        Public Seen As Boolean
        Public Code As String
        Public Sub New()
            Name = ""
            Year = ""
            Genre = ""
            Director = ""
            Code = ""
            Point = vbNull
            Age = vbNull
            Seen = vbNull
        End Sub
    End Class
    ''' <summary>
    ''' Open Movie's Webpage
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'define a system process to open url in default web browser
        Process.Start(String.Format("http://tinyz.us/tt{0}", Sample.Code))
    End Sub
    ''' <summary>
    ''' Call Add Movie Function And Send Info
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Empty As Boolean = True
        'check that all fields be filled
        If Not TextBox2.Text = "" Then
            If Not TextBox3.Text = "" Then
                If Not TextBox4.Text = "" Then
                    If Not TextBox5.Text = "" Then
                        If Not TextBox6.Text = "" Then
                            If Not TextBox7.Text = "" Then
                                If Not TextBox8.Text = "" Then
                                    Empty = False
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
        If Not Empty Then 'if there isn't any empty field
            'variables must be set now because at the start of 
            'Function we() have exception If we have empty field
            Dim Name As String = TextBox2.Text.ToString()
            Dim Code As String = TextBox8.Text.ToString()
            Dim Year As Integer = Val(TextBox3.Text.ToString())
            Dim Genre As String = TextBox5.Text.ToString()
            Dim Director As String = TextBox4.Text.ToString()
            Dim Point As Decimal = Convert.ToDecimal(TextBox6.Text.ToString())
            Dim Age As Integer = Val(TextBox7.Text.ToString())
            Dim Seen As Boolean = False
            Dim Exist As Boolean = False
            'exist checkbox
            If CheckBox1.Checked Then
                Exist = True
            End If
            'seen checkbox
            If CheckBox2.Checked Then
                Seen = True
            End If
            'call function to add movie info
            If Add_Movie(Name, Code, Year, Genre, Director, Point, Age, Seen, Exist) Then
                Label27.Show()
                Label27.Text = "Done !"
                'clear all fields for adding another movie
                For Each txt In {TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7, TextBox8}
                    txt.Text = ""
                Next
                CheckBox1.CheckState = False
                CheckBox2.CheckState = False
            Else
                'if we have any error only show the hidden label
                'error message will be shown in add function
                Label27.Show()
            End If
        Else
            'if there is empty field
            Label27.Show()
            Label27.Text = "Please Fill All Fields !!"
        End If
    End Sub
    ''' <summary>
    ''' Clear All Fields
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'clear all textboxes
        For Each txt In {TextBox2, TextBox3, TextBox4, TextBox5, TextBox6, TextBox7, TextBox8}
            txt.Text = ""
        Next
        'clear checkbox's state
        CheckBox1.CheckState = False
        CheckBox2.CheckState = False
        Label27.Text = ""
    End Sub
End Class