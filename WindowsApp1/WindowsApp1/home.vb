Imports MySql.Data.MySqlClient

Public Class home
    Public ConString As New MySqlConnectionStringBuilder
    Dim ID(100) As Integer
    Dim course(100) As String
    Dim teacher(100) As String
    Dim url(100) As String
    Dim type(100) As Integer
    Dim grade(100) As Integer
    Dim level(100) As Integer
    Dim count As Integer = 0
    Dim score(100) As Single
    Dim currentType As Integer = 0
    Dim currentLevel As Integer = 0
    Dim where1 As String
    Dim where2 As String
    Public currentScore As Single
    Public currentID As Integer
    Public currentCourse As String
    Public currentTeacher As String
    Public account As String
    Dim queryStr As String = "SELECT * FROM `course`"

    Public Sub connect()
        '建立連線字串
        ConString.Database = "dbms"
        ConString.Server = "34.81.103.157" 'Google Cloud SQL
        ConString.UserID = "root"
        ConString.SslMode = MySqlSslMode.Disabled
    End Sub

    Private Sub start(sender As Object, e As EventArgs) Handles MyBase.Load
        FormManager.AttachClosingEvent(Me)

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub level1_Click(sender As Object, e As EventArgs) Handles level1.Click
        level1.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
        level1.ForeColor = Color.FromArgb(255, 64, 64, 64)

        level2.BackColor = Color.Transparent
        level3.BackColor = Color.Transparent
        level4.BackColor = Color.Transparent
        level2.ForeColor = Color.White
        level3.ForeColor = Color.White
        level4.ForeColor = Color.White

        where1 = "level=1"

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub level2_Click(sender As Object, e As EventArgs) Handles level2.Click
        level2.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
        level2.ForeColor = Color.FromArgb(255, 64, 64, 64)

        level1.BackColor = Color.Transparent
        level3.BackColor = Color.Transparent
        level4.BackColor = Color.Transparent
        level1.ForeColor = Color.White
        level3.ForeColor = Color.White
        level4.ForeColor = Color.White

        where1 = "level=2"

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub level3_Click(sender As Object, e As EventArgs) Handles level3.Click
        level3.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
        level3.ForeColor = Color.FromArgb(255, 64, 64, 64)

        level2.BackColor = Color.Transparent
        level1.BackColor = Color.Transparent
        level4.BackColor = Color.Transparent
        level2.ForeColor = Color.White
        level1.ForeColor = Color.White
        level4.ForeColor = Color.White

        where1 = "level=3"

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub level4_Click(sender As Object, e As EventArgs) Handles level4.Click
        level4.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
        level4.ForeColor = Color.FromArgb(255, 64, 64, 64)

        level2.BackColor = Color.Transparent
        level3.BackColor = Color.Transparent
        level1.BackColor = Color.Transparent
        level2.ForeColor = Color.White
        level3.ForeColor = Color.White
        level1.ForeColor = Color.White

        where1 = "level=4"

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
        Button1.ForeColor = Color.FromArgb(255, 64, 64, 64)

        Button2.BackColor = Color.Transparent
        Button3.BackColor = Color.Transparent
        Button4.BackColor = Color.Transparent
        Button2.ForeColor = Color.White
        Button3.ForeColor = Color.White
        Button4.ForeColor = Color.White

        where2 = "type=1"

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button3.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
        Button3.ForeColor = Color.FromArgb(255, 64, 64, 64)

        Button2.BackColor = Color.Transparent
        Button1.BackColor = Color.Transparent
        Button4.BackColor = Color.Transparent
        Button2.ForeColor = Color.White
        Button1.ForeColor = Color.White
        Button4.ForeColor = Color.White

        where2 = "type=2"

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button4.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
        Button4.ForeColor = Color.FromArgb(255, 64, 64, 64)

        Button2.BackColor = Color.Transparent
        Button1.BackColor = Color.Transparent
        Button3.BackColor = Color.Transparent
        Button2.ForeColor = Color.White
        Button1.ForeColor = Color.White
        Button3.ForeColor = Color.White

        where2 = "type=3"

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2.BackColor = Color.FromKnownColor(KnownColor.GradientInactiveCaption)
        Button2.ForeColor = Color.FromArgb(255, 64, 64, 64)

        Button3.BackColor = Color.Transparent
        Button1.BackColor = Color.Transparent
        Button4.BackColor = Color.Transparent
        Button3.ForeColor = Color.White
        Button1.ForeColor = Color.White
        Button4.ForeColor = Color.White

        where2 = "type=4"

        datagridview()
        'Task.Run(Sub() datagridview())
    End Sub

    Private Sub datagridview()
        DataGridView1.Rows.Clear()
        count = 0
        Dim qs As String

        If where1 <> Nothing And where2 <> Nothing Then
            qs = queryStr + " WHERE " + where1 + " AND " + where2
            'MsgBox(qs)
            SQL(qs, 0)
        ElseIf where1 = Nothing And where2 = Nothing Then
            qs = queryStr
            'MsgBox(qs)
            SQL(qs, 0)

        Else
            qs = queryStr + " WHERE " + where1 + where2
            'MsgBox(qs)
            SQL(qs, 0)
        End If

        Dim typeName(100) As String
        qs = "SELECT "
        For i = 0 To count - 1
            If type(i) = 1 Then
                typeName(i) = "通識必修"
            ElseIf type(i) = 2 Then
                typeName(i) = "通識選修"
            ElseIf type(i) = 3 Then
                typeName(i) = "專業必修"
            Else
                typeName(i) = "專業選修"
            End If
            If i = count - 1 Then
                qs += "AVG(case when cID =" + ID(i).ToString + " THEN rating END) FROM review;"
            Else
                qs += "AVG(case when cID =" + ID(i).ToString + " THEN rating END),"
            End If
        Next
        'MsgBox(qs)
        If count <> 0 Then
            SQL(qs, 1)
        End If

        For i = 0 To count - 1
            If score(i) = 0 Then
                DataGridView1.Rows.Add(grade(i), course(i), teacher(i), typeName(i), level(i), "沒有評價")
            Else
                DataGridView1.Rows.Add(grade(i), course(i), teacher(i), typeName(i), level(i), score(i))
            End If
        Next
    End Sub

    Private Sub SQL(qs As String, op As Integer)
        Call connect()
        '建立查詢字串
        Dim QueryString As String = qs
        '建立資料庫連線物件
        Using Connection As New MySqlConnection(ConString.ToString)
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

                If op = 0 Then
                    Do While dataReader.Read()
                        ID(count) = dataReader(0)
                        course(count) = dataReader(1).ToString
                        teacher(count) = dataReader(2).ToString
                        url(count) = dataReader(3).ToString
                        type(count) = dataReader(4)
                        grade(count) = dataReader(5)
                        level(count) = dataReader(6)
                        count += 1
                    Loop
                ElseIf op = 1 Then
                    Dim output
                    dataReader.Read()
                    For i = 0 To dataReader.FieldCount - 1
                        output = dataReader(i)
                        If TypeOf output IsNot DBNull Then
                            score(i) = output
                        Else
                            score(i) = 0
                        End If
                    Next
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
        If IsNumeric(DataGridView1.CurrentRow.Cells.Item(5).Value) Then
            currentScore = DataGridView1.CurrentRow.Cells.Item(5).Value
        Else
            currentScore = 0
        End If
        currentID = ID(DataGridView1.CurrentRow.Index)
        currentCourse = course(DataGridView1.CurrentRow.Index)
        currentTeacher = teacher(DataGridView1.CurrentRow.Index)

        allreview.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim myProcess = New Process()
        Dim index = DataGridView1.CurrentRow.Index
        myProcess.StartInfo.UseShellExecute = True
        If type(index) = 2 Then
            course(index) = course(index).Split("(").GetValue(0)
        End If
        myProcess.StartInfo.FileName = "https://www.google.com/search?q=" + course(index) + "+" + teacher(index) + "+site%3Ahttps%3A%2F%2Fwww.dcard.tw%2Ff%2Fncyu"
        myProcess.Start()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim myProcess = New Process()
        Dim index = DataGridView1.CurrentRow.Index
        myProcess.StartInfo.UseShellExecute = True
        myProcess.StartInfo.FileName = url(index)
        myProcess.Start()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        profile.Show()
    End Sub

End Class