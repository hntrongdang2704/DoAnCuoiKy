using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnCuoiKy
{
    public partial class Login : Form
    {

        private TcpClient client;
        public Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // sử dụng bất đồng bộ async, await
            string username = txtUser.Text;
            string password = txtPass.Text;
            // Kiểm tra kết nối đến server
            try
            {
                client = new TcpClient("127.0.0.1", 5555); // Địa chỉ IP của server và cổng
                string loginInfo = username + ":" + password; // Định dạng gửi thông tin
                byte[] data = Encoding.UTF8.GetBytes(loginInfo);

                // Gửi thông tin đến server
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(data, 0, data.Length);

                // Nhận phản hồi từ server
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

             //   MessageBox.Show(response); // Hiển thị phản hồi từ server
                if (response == "1")
                {
                    this.Hide();
                    main baitap1 = new main();
                    baitap1.FormClosed += (s, args) => this.Show();
                    baitap1.Show();
                }
                else
                    MessageBox.Show("Sai user hoặc pass! Xin mời nhập lại!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối: {ex.Message}");
            }
            
        }
    }
}
