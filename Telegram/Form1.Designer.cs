namespace Telegram
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
           
            // 初始化控件
            _mainSplitContainer = new SplitContainer();
            _contactsListView = new ListView();
            _messagesListBox = new ListBox();
            _messageInputTextBox = new TextBox();
            _sendMessageButton = new Button();

            // 配置主界面布局
            _mainSplitContainer.Dock = DockStyle.Fill;
            _mainSplitContainer.Orientation = Orientation.Vertical;
            Controls.Add(_mainSplitContainer);

            // 配置联系人和群组列表
            _contactsListView.Dock = DockStyle.Fill;
            _contactsListView.View = View.Details;
            _contactsListView.FullRowSelect = true;
            _contactsListView.HeaderStyle = ColumnHeaderStyle.None;
            _contactsListView.Columns.Add("", -2, HorizontalAlignment.Left); // 添加一个隐藏的列头
            _mainSplitContainer.Panel1.Controls.Add(_contactsListView);

            // 配置消息列表和输入框
            _messagesListBox.Dock = DockStyle.Fill;
            _messagesListBox.Height=60;
            _mainSplitContainer.Panel2.Controls.Add(_messagesListBox);

            // 配置发送消息的输入框
            _messageInputTextBox.Dock = DockStyle.Bottom;
            _mainSplitContainer.Panel2.Controls.Add(_messageInputTextBox);

            // 配置发送按钮
            _sendMessageButton.Dock = DockStyle.Bottom;
            _sendMessageButton.Text = "发送";
            _mainSplitContainer.Panel2.Controls.Add(_sendMessageButton);

            // 添加示例数据
            this._contactsListView.TileSize = new Size(this._contactsListView.Width - 20, 90);


        

            // 设置窗口属性
            Text = "类似Telegram的聊天应用";
            Size = new System.Drawing.Size(800, 600);
            this._contactsListView.HeaderStyle = ColumnHeaderStyle.None;

            this._contactsListView.SelectedIndexChanged += new System.EventHandler(this.ContactsListView_SelectedIndexChanged);

            this._contactsListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ContactsListView_MouseClick);

              _contactsListView.OwnerDraw = true;
            _contactsListView.DrawItem += ContactsListView_DrawItem;

           // this._contactsListView.VScroll += new System.EventHandler(this.ContactsListView_VScroll);
        }


        private void ContactsListView_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = _contactsListView.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                item.Selected = true;
            }
        }

        private void ContactsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_contactsListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = _contactsListView.SelectedItems[0];
                string contactName = selectedItem.Text.Split('\n')[0];

                // 调用 LoadChatMessages 方法加载聊天消息
                LoadChatMessages(contactName);
            }
        }

        #endregion
        private const int ListViewRowHeight = 60; // 自定义行高

        private void SetListViewRowHeight()
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, ListViewRowHeight);
            _contactsListView.SmallImageList = imageList;
        }
        private void AddContact(Contact contact)
        {
            _contacts.Add(contact);
            _contacts = _contacts.OrderByDescending(c => c.DateTime).ToList();
            UpdateContactsListView();
        }
        private void UpdateContactsListView()
        {
            // 清空联系人列表
            _contactsListView.Items.Clear();

            // 遍历所有的 Contact 对象，并将其添加到联系人列表中
            foreach (var contact in _contacts)
            {
                ListViewItem item = new ListViewItem();

                // 添加名称和消息预览
                item.Text = $"{contact.Name}\n{contact.MessagePreview}";
                item.SubItems.Add(contact.MessagePreview);
                item.Font = new Font("Segoe UI", 10);
                item.ForeColor = Color.Red;

                // 将项目添加到联系人列表
                _contactsListView.Items.Add(item);
            }
        }

        private void LoadSampleData()
        {
                    List<Contact> contacts = new List<Contact>
            {
                new Contact { Name = "Alice", MessagePreview = "Hey! How's it going?" },
                new Contact { Name = "Bob", MessagePreview = "Did you finish the report?" },
                new Contact { Name = "Charlie", MessagePreview = "See you at the party tonight!" },
                new Contact { Name = "David", MessagePreview = "I'll call you later." },
                new Contact { Name = "Eva", MessagePreview = "Have a great day!" },
            };

            foreach (var contact in contacts)
            {
                AddContact(contact);
            }
        }

        private Dictionary<string, List<Message>> _chatData = new Dictionary<string, List<Message>>();

        private void LoadChatMessages(string contactName)
        {
            if (_chatData.ContainsKey(contactName))
            {
                List<Message> messages = _chatData[contactName];

                // 清空现有的聊天消息（如果有）
                _messagesListBox.Items.Clear();

                // 显示新的聊天消息
                foreach (var message in messages)
                {
                    _messagesListBox.Items.Add($"{message.Sender}: {message.Content}");
                }
            }
        }

        private void LoadChatData()
        {
            // ...省略其他代码...

            // 添加模拟聊天数据
            _chatData["Alice"] = new List<Message>
    {
        new Message { Sender = "张三", Content = "你好！", Timestamp = DateTime.Now.AddMinutes(-25) },
        new Message { Sender = "你", Content = "你好，张三！", Timestamp = DateTime.Now.AddMinutes(-20) },
    };

            _chatData["Bob"] = new List<Message>
    {
        new Message { Sender = "李四", Content = "今天天气真好！", Timestamp = DateTime.Now.AddMinutes(-15) },
        new Message { Sender = "你", Content = "是的，很适合出去玩！", Timestamp = DateTime.Now.AddMinutes(-10) },
    };

            // ...省略其他代码...
        }
    }
}