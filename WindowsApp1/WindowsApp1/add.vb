Imports MySql.Data.MySqlClient

Public Class add
    Dim rating As Integer = 0
    Dim tag As String

    Private Sub SQL()
        Call home.connect()
        '建立查詢字串
        Dim QueryString As String = "SELECT * FROM review;"
        Dim qs As String =
            "INSERT INTO review(cID,aID,tag,rating,approach,ea,others,date,summary) VALUES(" &
            home.currentID & "," & home.account & ",'" & tag & "'," & rating & ",'" & TextBox1.Text & "','" &
            TextBox2.Text & "','" & TextBox4.Text & "','" & Format(Now, "yyyy/MM/dd") & "','" & TextBox3.Text & "');"

        'MsgBox(qs)
        '建立資料庫連線物件
        Using Connection As New MySqlConnection(home.ConString.ToString)
            '建立送入查詢字串物件
            Dim MyCommand As MySqlCommand = Connection.CreateCommand()
            Dim cmd As MySqlCommand = New MySqlCommand(qs, Connection)

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

                cmd.ExecuteNonQuery()

                Adapter.Fill(DataSet1.Tables(0))

                '如果程式操作期間有對DataSet1中的任何資料做修改且需要更新資料庫，則可利用 update 方法把資料送回MySql Server
                'Adapter.Update(DataSet1)

                '設定繫結資料來源
                BindingSource1.DataSource = DataSet1
                '設定有繫結作用的資料來源中的哪個表格
                BindingSource1.DataMember = DataSet1.Tables(0).ToString
                'DataGridView1.DataSource = BindingSource1

            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Connection.Close()
            End Try
        End Using

    End Sub

    Private Sub setAddBtnState() Handles TextBox1.TextChanged, TextBox2.TextChanged, TextBox3.TextChanged
        If TextBox1.Text <> Nothing And TextBox2.Text <> Nothing And TextBox3.Text <> Nothing And rating <> 0 Then
            Button1.Enabled = True
            Button1.BackColor = Color.FromKnownColor(KnownColor.FloralWhite)
        Else
            Button1.Enabled = False
            Button1.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        rating = 1
        setAddBtnState()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        rating = 2
        setAddBtnState()
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        rating = 3
        setAddBtnState()
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        rating = 4
        setAddBtnState()
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        rating = 5
        setAddBtnState()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked Then
            tag += "甜課"
        End If
        If CheckBox2.Checked Then
            If tag <> Nothing Then
                tag += " 涼課"
            Else
                tag += "涼課"
            End If
        End If
        If CheckBox3.Checked Then
            If tag <> Nothing Then
                tag += " 紮實課"
            Else
                tag += "紮實課"
            End If
        End If

        SQL()

        allreview.datagridview()
        Me.Close()
    End Sub

End Class