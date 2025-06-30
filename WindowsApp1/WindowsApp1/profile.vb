Imports System.Management
Imports Microsoft.SqlServer.Server
Imports MySql.Data.MySqlClient

Public Class profile

    Public rID As Integer
    Public tag As String
    Public approach As String
    Public ea As String
    Public summary As String
    Public other As String
    Public currentScore As Integer

    Dim score(100) As Integer
    Dim id(100) As Integer
    Dim cID(100) As Integer
    Dim course(100) As String
    Dim count As Integer = 0

    Private Sub profile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        datagridview()
    End Sub

    Public Sub datagridview()
        DataGridView1.Rows.Clear()
        'MsgBox(home.account)
        SQL("SELECT cID,rating,rID FROM review WHERE aID=" + home.account.ToString, 0)

        Dim i = 0
        Dim tmpSet As New HashSet(Of Integer)(cID)
        Dim first_cID_index As New List(Of Integer)()
        Dim new_cID() As Integer = tmpSet.Take(tmpSet.Count - 1).ToArray()

        Array.Sort(new_cID)


        For Each element As Integer In cID
            first_cID_index.Add(Array.IndexOf(new_cID, element))
        Next

        Dim qs As String = "SELECT name FROM course WHERE id in ("

        While cID(i) <> 0
            qs += cID(i).ToString + ","
            i += 1
        End While
        qs += "0)"
        SQL(qs, 2)

        i = 0
        While cID(i) <> 0
            'MsgBox("test")
            DataGridView1.Rows.Add(course(first_cID_index(i)), score(i))
            i += 1
        End While

        If DataGridView1.Rows.Count = 0 Then
            PictureBox8.Image = My.Resources.delete_gray
            Button2.Enabled = False
            Button2.BackColor = Color.LightGray
            Button2.FlatAppearance.BorderColor = Color.Gray

            Button5.Enabled = False
            Button5.BackColor = Color.LightGray

            Button1.Enabled = False
            Button1.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub SQL(qs As String, op As Integer)
        Call home.connect()
        '建立查詢字串
        Dim QueryString As String = qs
        '建立資料庫連線物件
        Using Connection As New MySqlConnection(home.ConString.ToString)
            '建立送入查詢字串物件
            Dim MyCommand As MySqlCommand = Connection.CreateCommand()
            MyCommand.CommandText = QueryString
            Try
                '開啟資料庫連線
                Connection.Open()

                '建立資料表橋接器
                Dim Adapter As New MySqlDataAdapter()
                '送出給MySql Server 執行的 SQL 指令
                Adapter.SelectCommand = MyCommand

                'DataSet可以存放多個表格資料，把資料放到 DataSet1 的第一個表格
                Adapter.Fill(DataSet1.Tables(0))

                '如果程式操作期間有對DataSet1中的任何資料做修改且需要更新資料庫，則可利用 update 方法把資料送回MySql Server
                'Adapter.Update(DataSet1)

                '設定繫結資料來源
                BindingSource1.DataSource = DataSet1
                '設定有繫結作用的資料來源中的哪個表格
                BindingSource1.DataMember = DataSet1.Tables(0).ToString
                'DataGridView1.DataSource = BindingSource1

                Dim dataReader As MySqlDataReader = MyCommand.ExecuteReader()
                Dim i = 0

                If op = 0 Then
                    Do While dataReader.Read()
                        cID(i) = dataReader(0)
                        score(i) = dataReader(1)
                        id(i) = dataReader(2)
                        i += 1
                    Loop
                ElseIf op = 1 Then
                    Do While dataReader.Read()
                        Label13.Text = dataReader(0)
                        Label8.Text = dataReader(1)
                        Label9.Text = dataReader(2)
                        Label10.Text = dataReader(3)
                        Label11.Text = dataReader(4)
                        Label2.Text = dataReader(5)
                        i += 1
                    Loop
                ElseIf op = 2 Then
                    Do While dataReader.Read()
                        course(i) = dataReader(0)
                        i += 1
                    Loop
                Else
                    MyCommand.ExecuteNonQuery()
                End If
                dataReader.Close()

            Catch ex As Exception
                'MsgBox(ex.Message)

            Finally
                Connection.Close()
            End Try
        End Using

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        SQL("SELECT tag,approach,ea,summary,others,date FROM review WHERE rID=" + id(DataGridView1.CurrentRow.Index).ToString, 1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SQL("delete from review where rID =" + id(DataGridView1.CurrentRow.Index).ToString, 3)
        DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)

        If DataGridView1.Rows.Count = 0 Then
            PictureBox8.Image = My.Resources.delete_gray
            Button2.Enabled = False
            Button2.BackColor = Color.LightGray
            Button2.FlatAppearance.BorderColor = Color.Gray

            Button5.Enabled = False
            Button5.BackColor = Color.LightGray

            Button1.Enabled = False
            Button1.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SQL("SELECT tag,approach,ea,summary,others,date FROM review WHERE rID=" + id(DataGridView1.CurrentRow.Index).ToString, 1)
        tag = Label13.Text
        approach = Label8.Text
        ea = Label9.Text
        summary = Label10.Text
        other = Label11.Text
        currentScore = score(DataGridView1.CurrentRow.Index)
        rID = id(DataGridView1.CurrentRow.Index)
        modify.Show()
    End Sub

End Class