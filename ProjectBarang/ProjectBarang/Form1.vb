Imports System.Data.SqlClient

Public Class Form1
    Dim Conn As SqlConnection
    Dim Da As SqlDataAdapter
    Dim Cmd As SqlCommand
    Dim Rd As SqlDataReader
    Dim Ds As DataSet
    Dim MyDB As String
    Sub koneksi()
        MyDB = "data source=NITRO5\SQLEXPRESS;initial catalog=DB_Indomaret;integrated security=true"
        Conn = New SqlConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

    Sub kondisiAwal()
        Call koneksi()
        Da = New SqlDataAdapter("Select * from TBL_Barang", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "TBL_Barang")
        display.DataSource = (Ds.Tables("TBL_Barang"))
        TextBox1.MaxLength = 10
        Call clearText()

    End Sub
    Sub clearText()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call kondisiAwal()

    End Sub

   
    Private Sub ins_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ins.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data Belum Lengkap")
        Else
            Call koneksi()
            Dim InputData As String = "insert into TBL_Barang values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
            Cmd = New SqlCommand(InputData, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("Data Terinput")
            Call kondisiAwal()
        End If
    End Sub

    Private Sub upd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles upd.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Data Belum Lengkap")
        Else
            Call koneksi()
            Dim UpdateData As String = "update TBL_Barang set Nama_Barang ='" & TextBox2.Text & "',Harga_Barang='" & TextBox3.Text & "' where Kode_Barang='" & TextBox1.Text & "' "
            Cmd = New SqlCommand(UpdateData, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("Data Terupdate")
            Call kondisiAwal()
        End If
    End Sub

    Private Sub del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles del.Click
        Call koneksi()
        Dim DeleteData As String = "delete TBL_Barang where Kode_Barang='" & TextBox1.Text & "'"
        Cmd = New SqlCommand(DeleteData, Conn)
        Cmd.ExecuteNonQuery()
        MsgBox("Data Dihapus")
        Call kondisiAwal()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(10) Then
            Call koneksi()
            Cmd = New SqlCommand("select * from TBL_Barang where Kode_Barang='" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                TextBox2.Text = Rd.Item("Nama_Barang")
                TextBox3.Text = Rd.Item("Harga_Barang")
            Else
                MsgBox("Data Tidak Ada")
            End If
        End If
    End Sub

End Class
