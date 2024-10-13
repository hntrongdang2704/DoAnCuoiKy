using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        private TcpListener listener;
        //  private int port = 5555;
        private List<TcpClient> clients = new List<TcpClient>(); // Danh sách client kết nối
        private const int maxClients = 4; // Số lượng kết nối tối đa
        private List<Taikhoan> taikhoans; // Danh sách tài khoản
        private bool isRunning = false;  // Biến cờ để kiểm soát luồng
        public Server()
        {
            InitializeComponent();
            KhoiTaoTaiKhoan();
        }

        private void KhoiTaoTaiKhoan()
        {
            // Khởi tạo danh sách tài khoản với 4 tài khoản mẫu
            taikhoans = new List<Taikhoan>
            {
                new Taikhoan("user1", "pass1"),
                new Taikhoan("user2", "pass2"),
                new Taikhoan("user3", "pass3"),
                new Taikhoan("user4", "pass4")
            };
        }
        public void StartServer()
        {
            // mở cổng 5555 lắng nghe trên tất cả IP mà server đang có
            listener = new TcpListener(IPAddress.Any, 5555);
            listener.Start();
            // khởi tạo luồng kết nối cho client
            isRunning = true;
            Thread acceptClient = new Thread(AcceptClient);
            acceptClient.Start();

        }
        public void AcceptClient()
        {
            while (isRunning)
            {
                try
                {
                    if (clients.Count < maxClients)
                    {
                        // Chấp nhận kết nối từ client
                        TcpClient client = listener.AcceptTcpClient();
                        IPEndPoint remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;

                        // Hiển thị thông tin client kết nối
                        
                        // Khởi động luồng mới để xử lý client
                        Thread clientN = new Thread(() => NhanMessage(client));
                        clientN.Start();
                    }
                    else
                    {
                        // Từ chối kết nối nếu đã đạt tối đa
                        TcpClient rejectedClient = listener.AcceptTcpClient();
                        // Đóng kết nối ngay lập tức
                        rejectedClient.Close();
                        Invoke(new Action(() =>
                        {
                            rtbClientConnect.AppendText("Đã đủ số lượng client kết nối. Từ chối client mới.\n");
                        }));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void NhanMessage(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            try
            {
                while (isRunning)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] credentials = message.Split(':');
                    // Kiểm tra xem có phải là thông tin đăng nhập không
                    if (credentials.Length == 2) // Ví dụ: thông điệp đăng nhập bắt đầu bằng "LOGIN:"
                    {
                        bool loginSuccess = HandleLogin(client, message);
                        if (loginSuccess)
                        {
                            // Hiển thị thông tin client sau khi đăng nhập thành công
                            IPEndPoint remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                            Invoke(new Action(() =>
                            {
                                rtbClientConnect.AppendText($"Client kết nối: {remoteEndPoint.Address}:{remoteEndPoint.Port}\n");
                            }));
                        }
                        else
                        {
                            // Đóng kết nối nếu đăng nhập thất bại
                            client.Close();
                            return;
                        }
                    }
                    else
                    {
                        // Xử lý tin nhắn khác
                        byte[] data = Encoding.UTF8.GetBytes(message); // Chuyển đổi tin nhắn thành mảng byte
                        foreach (var cl in clients)
                        {
                            if (cl != client) // Không gửi lại cho client đã gửi tin
                            {
                                NetworkStream str = cl.GetStream();
                                str.Write(data, 0, data.Length); // Gửi tin nhắn đến từng client
                            }
                        } // Gọi hàm gửi tin nhắn cho tất cả client
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //Invoke(new Action(() =>
                //{
                //    rtbMessage.AppendText($"Lỗi: {ex.Message}\n");
                //}));
            }
            finally
            {
                clients.Remove(client);
                client.Close();
            }
        }

        private bool HandleLogin(TcpClient client, string message)
        {
            NetworkStream stream = client.GetStream();
            string[] thongtin = message.Split(':');

            if (thongtin.Length == 2)
            {
                string username = thongtin[0];
                string password = thongtin[1];

                // Kiểm tra tài khoản
                if (taikhoans.Any(t => t.Username == username && t.Password == password))
                {
                    // Đăng nhập thành công
                    byte[] responseData = Encoding.UTF8.GetBytes("1");
                    stream.Write(responseData, 0, responseData.Length);
                    clients.Add(client);
                    return true;
                }
                else
                {
                    // Đăng nhập không thành công
                    byte[] responseData = Encoding.UTF8.GetBytes("0");
                    stream.Write(responseData, 0, responseData.Length);
                }
            }
            return false;
        }

        private void rtbClientConnect_TextChanged(object sender, EventArgs e)
        {

        }

        private void rtbMessage_TextChanged(object sender, EventArgs e)
        {

        }
        public class Taikhoan
        {
            public string Username { get; set; }
            public string Password { get; set; }

            public Taikhoan(string username, string password)
            {
                Username = username;
                Password = password;
            }
        }
        public void StopServer()
        {
            isRunning = false;  // Dừng server
            // Dừng lắng nghe kết nối mới
            listener.Stop();

            // Đóng tất cả các kết nối client
            foreach (var client in clients)
            {
                client.Close();
            }

            // Xóa danh sách client
            clients.Clear();
        }
        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            StartServer();
        }
    }
}