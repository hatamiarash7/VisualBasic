Imports System.Data.SqlClient
Public Class SQL
    Public _QUERY As String
    Private _SERVER As String
    Public _DATABASE As String
    Private _USERNAME As String
    Private _PASSWORD As String
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Public Sub New()
        _QUERY = ""
        _SERVER = "HATAMIARASH7"
        _DATABASE = ""
        _USERNAME = "sa"
        _PASSWORD = "3920512197"
    End Sub
    Public Sub Execute()
        Dim Source As String = "Data Source=" & _SERVER & ";Initial Catalog=" & _DATABASE & ";User ID=" & _USERNAME & ";Password=" & _PASSWORD & ";"
        myConn = New SqlConnection(Source)
        myCmd = myConn.CreateCommand
        myCmd.CommandText = _QUERY
        myConn.Open()
        myCmd.ExecuteNonQuery()
        myConn.Close()
    End Sub
    Public Function Execute_Read_Site() As List(Of Site)
        Dim list As List(Of Site) = New List(Of Site)
        Dim Source As String = "Data Source=" & _SERVER & ";Initial Catalog=" & _DATABASE & ";User ID=" & _USERNAME & ";Password=" & _PASSWORD & ";"
        myConn = New SqlConnection(Source)
        myCmd = myConn.CreateCommand
        myCmd.CommandText = _QUERY
        myConn.Open()
        myReader = myCmd.ExecuteReader()
        Do While myReader.Read()
            Dim site As Site = New Site()
            site.address = myReader.GetString(0)
            site.category = myReader.GetString(1)
            site.author = myReader.GetString(2)
            list.Add(site)
        Loop
        myReader.Close()
        myConn.Close()
        Return list
    End Function
    Public Function Execute_Read_KeyWord() As List(Of KeyWord)
        Dim list As List(Of KeyWord) = New List(Of KeyWord)
        Dim Source As String = "Data Source=" & _SERVER & ";Initial Catalog=" & _DATABASE & ";User ID=" & _USERNAME & ";Password=" & _PASSWORD & ";"
        myConn = New SqlConnection(Source)
        myCmd = myConn.CreateCommand
        myCmd.CommandText = _QUERY
        myConn.Open()
        myReader = myCmd.ExecuteReader()
        Do While myReader.Read()
            Dim word As KeyWord = New KeyWord()
            word.word = myReader.GetString(0)
            word.category = myReader.GetString(1)
            list.Add(word)
        Loop
        myReader.Close()
        myConn.Close()
        Return list
    End Function
    Public Sub Execute_Write(address As List(Of String), category As List(Of String), author As List(Of String))
        Dim Source As String = "Data Source=" & _SERVER & ";Initial Catalog=" & _DATABASE & ";User ID=" & _USERNAME & ";Password=" & _PASSWORD & ";"
        Dim i As Integer
        For i = 0 To address.Count() - 1
            myConn = New SqlConnection(Source)
            Using myConn
                Using comm As New SqlCommand
                    With comm
                        .Connection = myConn
                        .CommandType = CommandType.Text
                        .CommandText = _QUERY
                        .Parameters.AddWithValue("@address", address.Item(i))
                        .Parameters.AddWithValue("@category", category.Item(i))
                        .Parameters.AddWithValue("@author", author.Item(i))
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
        Next
    End Sub
    Public Sub Execute_Write(address As List(Of String), category As List(Of String))
        Dim Source As String = "Data Source=" & _SERVER & ";Initial Catalog=" & _DATABASE & ";User ID=" & _USERNAME & ";Password=" & _PASSWORD & ";"
        Dim i As Integer
        For i = 0 To address.Count() - 1
            myConn = New SqlConnection(Source)
            Using myConn
                Using comm As New SqlCommand
                    With comm
                        .Connection = myConn
                        .CommandType = CommandType.Text
                        .CommandText = _QUERY
                        .Parameters.AddWithValue("@word", address.Item(i))
                        .Parameters.AddWithValue("@category", category.Item(i))
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
        Next
    End Sub
End Class