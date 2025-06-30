Imports MySql.Data.MySqlClient

Public Class allreview
    Dim d(100) As String
    Dim score(100) As Single
    Dim id(100) As Integer
    Dim isReviewAdded As Boolean = False

    Private Sub allreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = home.currentCourse + " - " + home.currentTeacher
        datagridview()
    End Sub

    Public Sub datagridview()
        DataGridView1.Rows.Clear()
        SQL("SELECT date,rating,rID FROM review WHERE cID=" + home.currentID.ToString, 0)
        Dim i = 0
        While d(i) <> Nothing
            DataGridView1.Rows.Add(d(i), score(i))
            i += 1
        End While
        If i = 0 Then
            Button5.Enabled = False
            Button5.BackColor = Color.LightGray
        Else
            Button5.Enabled = True
            Button5.BackColor = Color.FloralWhite
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
                        d(i) = dataReader(0)
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
                        i += 1
                    Loop
                Else
                    Do While dataReader.Read()
                        If dataReader(0).ToString <> Nothing Then
                            isReviewAdded = True
                        Else

                        End If
                    Loop
                End If
                dataReader.Close()

            Catch ex As Exception
                MsgBox(ex.Message)

            Finally
                Connection.Close()
            End Try
        End Using

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        SQL("SELECT tag,approach,ea,summary,others FROM review WHERE rID=" + id(DataGridView1.CurrentRow.Index).ToString, 1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SQL("SELECT * FROM review WHERE cID=" + home.currentID.ToString, 2)

        If Not isReviewAdded Then
            add.Show()
        Else
            MsgBox("你已新增過此課程的評價！")
        End If
    End Sub
End Class