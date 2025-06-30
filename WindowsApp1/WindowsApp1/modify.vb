Imports MySql.Data.MySqlClient


Public Class modify
    Dim rating As Integer = 0
    Dim tag As String

    Private Sub SQL()
        Call home.connect()
        '建立查詢字串
        Dim QueryString As String = "update review set rating='" + rating.ToString _
                                    + "',tag='" + tag _
                                    + "',approach='" + TextBox1.Text _
                                    + "',ea='" + TextBox2.Text _
                                    + "',summary='" + TextBox3.Text _
                                    + "',others='" + TextBox4.Text _
                                    + "' where rID=" + profile.rID.ToString

        '建立資料庫連線物件
        Using Connection As New MySqlConnection(home.ConString.ToString)
            '建立送入查詢字串物件
            Dim cmd As MySqlCommand = New MySqlCommand(QueryString, Connection)

            Try
                '開啟資料庫連線
                Connection.Open()

                '建立資料表橋接器
                Dim Adapter As New MySqlDataAdapter()
                '送出給MySql Server 執行的 SQL 指令
                Adapter.SelectCommand = cmd

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
        profile.datagridview()
        Me.Close()
    End Sub

    Private Sub modify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i = 1 To 5
            If profile.currentScore = i Then
                Select Case i
                    Case 1
                        RadioButton1.Checked = True
                    Case 2
                        RadioButton2.Checked = True
                    Case 3
                        RadioButton3.Checked = True
                    Case 4
                        RadioButton4.Checked = True
                    Case 5
                        RadioButton5.Checked = True
                End Select
            End If
        Next
        If profile.tag.Contains("甜") Then
            CheckBox1.Checked = True
        End If
        If profile.tag.Contains("涼") Then
            CheckBox2.Checked = True
        End If
        If profile.tag.Contains("紮") Then
            CheckBox3.Checked = True
        End If
        TextBox1.Text = profile.approach
        TextBox2.Text = profile.ea
        TextBox3.Text = profile.summary
        TextBox4.Text = profile.other
    End Sub
End Class
