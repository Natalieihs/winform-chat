using Microsoft.AspNetCore.SignalR.Client;
using System.Text.Json;

namespace Telegram
{
    public partial class Form1 : Form
    {
        private SplitContainer _mainSplitContainer;
        private ListView _contactsListView;
        private ListBox _messagesListBox;
        private TextBox _messageInputTextBox;
        private Button _sendMessageButton;
        List<Contact> _contacts = new List<Contact>();

        HubConnection connection;
        public Form1()
        {
            InitializeComponent();


            LoadSampleData();
            SetListViewRowHeight();
            LoadChatData();
            LoadData();
        }


        public async void LoadData()
        {
            connection = new HubConnectionBuilder()
          .WithUrl("http://localhost:5130/ChatHub")
          .Build();


            connection.On<string>("testMessage", message =>
            {
                Console.WriteLine(message);
            });
            // 注册客户端回调方法
            connection.On<object>("ReceiveMessage", message =>
            {
                // 在客户端应用程序中处理来自服务器的消息
                this.Invoke((MethodInvoker)(() =>
                {
                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    var contact = System.Text.Json.JsonSerializer.Deserialize<Contact>(message.ToString(), options);
                    AddContact(contact);
                }));

            });

            try
            {
                // 连接到 SignalR 服务器
                await connection.StartAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ContactsListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (!_isScrolling) // 如果没有滚动，则执行自动滚动
            {
                if (e.ItemIndex == _contactsListView.Items.Count - 1)
                {
                    e.Item.EnsureVisible(); // 自动滚动到最后一行
                }
            }
            int unreadCount = 99;

            // 创建未读消息角标的背景圆形
            // 创建未读消息角标的背景圆形
            using (Brush unreadBgBrush = new SolidBrush(Color.Red))
            {
                e.Graphics.FillEllipse(unreadBgBrush, e.Bounds.Left + 10, e.Bounds.Top + 10, 20, 20);
            }

            // 在未读消息角标上绘制未读消息计数
            using (Brush unreadTextBrush = new SolidBrush(Color.White))
            {
                e.Graphics.DrawString(unreadCount.ToString(), e.Item.Font, unreadTextBrush, e.Bounds.Left + 14, e.Bounds.Top + 10);
            }

            e.DrawDefault = false;

            // 设置填充和背景颜色
            int padding = 10;
            Color backgroundColor = Color.White;
            Color selectedBackgroundColor = Color.LightGray;
            Color borderColor = Color.FromArgb(230, 230, 230);

            // 绘制头像
            using (Bitmap avatar = new Bitmap(40, 40))
            {
                using (Graphics g = Graphics.FromImage(avatar))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(Brushes.Gray, 0, 0, 40, 40);
                    g.DrawString(e.Item.Text[0].ToString(), new Font("Segoe UI", 12), Brushes.White, 12, 10);
                }

                // 根据是否选定项目设置背景颜色
                if (e.Item.Selected)
                {
                    using (SolidBrush brush = new SolidBrush(selectedBackgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(backgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }
                }

                // 绘制头像和文本
                e.Graphics.DrawImage(avatar, e.Bounds.X + padding, e.Bounds.Y + padding);

                // 绘制名称和消息预览文本
                string[] lines = e.Item.Text.Split('\n');
                e.Graphics.DrawString(lines[0], e.Item.Font, new SolidBrush(e.Item.ForeColor), e.Bounds.X + 40 + 2 * padding, e.Bounds.Y + padding);
                e.Graphics.DrawString(lines[1], new Font(e.Item.Font.FontFamily, e.Item.Font.Size - 2), new SolidBrush(Color.Gray), e.Bounds.X + 40 + 2 * padding, e.Bounds.Y + padding + e.Item.Font.Height);

                // 绘制分隔线
                using (Pen pen = new Pen(borderColor))
                {
                    e.Graphics.DrawLine(pen, e.Bounds.X, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1);
                }
            }
        }

        private bool _isScrolling = false; // 用来记录是否正在滚动
        private void ContactsListView_VScroll(object sender, EventArgs e)
        {
            if (_contactsListView.AutoScrollOffset.Y != 0)
            {
                _isScrolling = true;
            }
        }

    }
}