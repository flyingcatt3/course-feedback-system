# WindowsApp1

## 專案簡介

本專案為一套以 VB.NET（Windows Forms）開發的課程評價管理系統，提供課程查詢、評價新增、修改、刪除與個人評價管理等功能，並以 MySQL 作為後端資料庫。

## 主要功能
- 使用者登入驗證（整合網頁驗證）
- 課程查詢與篩選（依課程類型、年級等）
- 檢視所有課程評價
- 新增課程評價（含標籤、分數、心得等）
- 修改/刪除個人評價
- 個人評價管理

## 安裝步驟
1. **環境需求**：
   - Windows 10 以上
   - Visual Studio 2019 或更新版本
   - .NET Framework 4.8.1
   - MySQL 資料庫（需先建好資料表）
2. **還原 NuGet 套件**：
   - 開啟專案後，於 Visual Studio 工具列選擇「工具」>「NuGet 套件管理員」>「還原 NuGet 套件」
   - 主要依賴套件如下：
     - CefSharp.WinForms
     - CefSharp.Common
     - cef.redist.x64 / cef.redist.x86
     - MySql.Data
     - Google.Protobuf
     - K4os.Compression.LZ4 / Streams / xxHash
     - Portable.BouncyCastle
     - System.Buffers / IO.Pipelines / Memory / Numerics.Vectors / Runtime.CompilerServices.Unsafe / Threading.Tasks.Extensions

## 執行方式
1. 於 Visual Studio 開啟 `WindowsApp1.sln`。
2. 設定啟始專案為 `WindowsApp1`。
3. 執行（F5）即可。

## 資料庫需求
- 需有 MySQL 資料庫，並建立 `course`、`review` 等相關資料表。
- 連線資訊預設寫於程式碼中（可於 `home.vb` 調整）。
- 預設連線：
  - Server: 34.81.103.157
  - Database: dbms
  - User: root

## 專案結構簡述
- `Form1.vb`：登入與主視窗（整合CefSharp瀏覽器）
- `home.vb`：課程查詢與主頁
- `allreview.vb`：所有評價檢視
- `add.vb`：新增評價
- `modify.vb`：修改評價
- `profile.vb`：個人評價管理
- 其餘 Designer、resx 檔為表單設計與資源檔案
