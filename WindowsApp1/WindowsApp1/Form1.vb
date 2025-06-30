Imports CefSharp
Imports CefSharp.WinForms

Public Class Form1
    'https://www.it-sideways.com/2014/04/vbnet-form-with-background-image.html
    Protected Overrides ReadOnly Property CreateParams() As Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Public account As String

    Private WithEvents browser As ChromiumWebBrowser

    Private Sub start() Handles MyBase.Load
        InitializeComponent()

        ' Assign closing event for the main form
        FormManager.AttachClosingEvent(Me)

        ' 初始化 CefSharp
        'Cef.Initialize(New CefSettings())

        ' 建立 ChromiumWebBrowser 控制項
        browser = New ChromiumWebBrowser("http://35.221.246.100:5000/") '驗證伺服器(Google Cloud)
        browser.Dock = DockStyle.Fill

        ' 導航完成事件處理
        AddHandler browser.FrameLoadEnd, AddressOf Browser_FrameLoadEnd

        ' 將 ChromiumWebBrowser 控制項加入主表單
        Controls.Add(browser)

        ' 設定視窗大小
        Me.Size = New Size(1600, 900)
        Me.CenterToScreen()
    End Sub

    Private Sub Browser_FrameLoadEnd(sender As Object, e As FrameLoadEndEventArgs)
        ' 檢查是否是主框架載入完成
        If e.Frame.IsMain Then
            ' 檢查網址是否為指定的 URL (是否登入成功)
            If e.Url.Contains("home") Then
                ' 載入完成，開啟第二個表單
                account = e.Url.Split("?").GetValue(1)
                home.Show()
                home.Text = "登入身分：" + account
                home.account = account
                Me.Hide()
            End If
        End If
    End Sub

End Class

'ChatGPT:how to assign the Closing event in all forms, using vb.net
Public Class FormManager
    Public Shared Sub AttachClosingEvent(form As Form)
        AddHandler form.FormClosing, AddressOf FormClosingEventHandler
    End Sub

    Private Shared Sub FormClosingEventHandler(sender As Object, e As FormClosingEventArgs)
        ' Perform any closing event handling logic here
        ' You can access the closing form using "DirectCast(sender, Form)"

        Application.Exit()
    End Sub
End Class

