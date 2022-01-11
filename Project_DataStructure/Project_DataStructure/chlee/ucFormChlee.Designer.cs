namespace Project_DataStructure.chlee
{
    partial class ucFormChlee
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.ListView = new System.Windows.Forms.ListView();
            this._columnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columnAge = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._columSex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbx_ListView = new System.Windows.Forms.GroupBox();
            this.txt_count = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gbx_memberData = new System.Windows.Forms.GroupBox();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.txt_modifyDate = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_createDate = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chk_AgeUpDown = new System.Windows.Forms.CheckBox();
            this.rad_woman = new System.Windows.Forms.RadioButton();
            this.rad_man = new System.Windows.Forms.RadioButton();
            this.txt_pw_ok = new System.Windows.Forms.TextBox();
            this.txt_tall = new System.Windows.Forms.TextBox();
            this.txt_age = new System.Windows.Forms.TextBox();
            this.txt_pw = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.gbx_Connect = new System.Windows.Forms.GroupBox();
            this.btn_serverStart = new System.Windows.Forms.Button();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_CancelDB = new System.Windows.Forms.Button();
            this.btn_ConnectDB = new System.Windows.Forms.Button();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.gbx_ListView.SuspendLayout();
            this.gbx_memberData.SuspendLayout();
            this.gbx_Connect.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(217, 361);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(47, 23);
            this.btn_Add.TabIndex = 1;
            this.btn_Add.Text = "저장";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(270, 361);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(47, 23);
            this.btn_create.TabIndex = 1;
            this.btn_create.Text = "생성";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(323, 361);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(47, 23);
            this.btn_update.TabIndex = 1;
            this.btn_update.Text = "수정";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(378, 361);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(47, 23);
            this.btn_delete.TabIndex = 1;
            this.btn_delete.Text = "삭제";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // ListView
            // 
            this.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnID,
            this._columnAge,
            this._columSex});
            this.ListView.FullRowSelect = true;
            this.ListView.GridLines = true;
            this.ListView.Location = new System.Drawing.Point(6, 20);
            this.ListView.MultiSelect = false;
            this.ListView.Name = "ListView";
            this.ListView.Size = new System.Drawing.Size(188, 329);
            this.ListView.TabIndex = 3;
            this.ListView.UseCompatibleStateImageBehavior = false;
            this.ListView.View = System.Windows.Forms.View.Details;
            this.ListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListView_ItemSelectionChanged);
            // 
            // _columnID
            // 
            this._columnID.Text = "ID";
            // 
            // _columnAge
            // 
            this._columnAge.Text = "Age";
            // 
            // _columSex
            // 
            this._columSex.Text = "성별";
            // 
            // gbx_ListView
            // 
            this.gbx_ListView.Controls.Add(this.txt_count);
            this.gbx_ListView.Controls.Add(this.label7);
            this.gbx_ListView.Controls.Add(this.ListView);
            this.gbx_ListView.Location = new System.Drawing.Point(16, 105);
            this.gbx_ListView.Name = "gbx_ListView";
            this.gbx_ListView.Size = new System.Drawing.Size(200, 395);
            this.gbx_ListView.TabIndex = 4;
            this.gbx_ListView.TabStop = false;
            this.gbx_ListView.Text = "ListView";
            // 
            // txt_count
            // 
            this.txt_count.Location = new System.Drawing.Point(54, 355);
            this.txt_count.Name = "txt_count";
            this.txt_count.Size = new System.Drawing.Size(61, 21);
            this.txt_count.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "항목수";
            // 
            // gbx_memberData
            // 
            this.gbx_memberData.Controls.Add(this.txt_id);
            this.gbx_memberData.Controls.Add(this.txt_modifyDate);
            this.gbx_memberData.Controls.Add(this.label9);
            this.gbx_memberData.Controls.Add(this.txt_createDate);
            this.gbx_memberData.Controls.Add(this.label8);
            this.gbx_memberData.Controls.Add(this.chk_AgeUpDown);
            this.gbx_memberData.Controls.Add(this.rad_woman);
            this.gbx_memberData.Controls.Add(this.rad_man);
            this.gbx_memberData.Controls.Add(this.txt_pw_ok);
            this.gbx_memberData.Controls.Add(this.txt_tall);
            this.gbx_memberData.Controls.Add(this.txt_age);
            this.gbx_memberData.Controls.Add(this.txt_pw);
            this.gbx_memberData.Controls.Add(this.label5);
            this.gbx_memberData.Controls.Add(this.label4);
            this.gbx_memberData.Controls.Add(this.label3);
            this.gbx_memberData.Controls.Add(this.label2);
            this.gbx_memberData.Controls.Add(this.label1);
            this.gbx_memberData.Location = new System.Drawing.Point(222, 105);
            this.gbx_memberData.Name = "gbx_memberData";
            this.gbx_memberData.Size = new System.Drawing.Size(273, 250);
            this.gbx_memberData.TabIndex = 5;
            this.gbx_memberData.TabStop = false;
            this.gbx_memberData.Text = "회원정보";
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(62, 20);
            this.txt_id.MaxLength = 15;
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(127, 21);
            this.txt_id.TabIndex = 17;
            this.txt_id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_id_KeyPress);
            // 
            // txt_modifyDate
            // 
            this.txt_modifyDate.Location = new System.Drawing.Point(62, 201);
            this.txt_modifyDate.Name = "txt_modifyDate";
            this.txt_modifyDate.Size = new System.Drawing.Size(153, 21);
            this.txt_modifyDate.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 204);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "수정날짜";
            // 
            // txt_createDate
            // 
            this.txt_createDate.Location = new System.Drawing.Point(62, 171);
            this.txt_createDate.Name = "txt_createDate";
            this.txt_createDate.Size = new System.Drawing.Size(153, 21);
            this.txt_createDate.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "생성날짜";
            // 
            // chk_AgeUpDown
            // 
            this.chk_AgeUpDown.AutoSize = true;
            this.chk_AgeUpDown.Location = new System.Drawing.Point(6, 228);
            this.chk_AgeUpDown.Name = "chk_AgeUpDown";
            this.chk_AgeUpDown.Size = new System.Drawing.Size(128, 16);
            this.chk_AgeUpDown.TabIndex = 12;
            this.chk_AgeUpDown.Text = "30세 이상이면 체크";
            this.chk_AgeUpDown.UseVisualStyleBackColor = true;
            // 
            // rad_woman
            // 
            this.rad_woman.AutoSize = true;
            this.rad_woman.Location = new System.Drawing.Point(221, 227);
            this.rad_woman.Name = "rad_woman";
            this.rad_woman.Size = new System.Drawing.Size(35, 16);
            this.rad_woman.TabIndex = 11;
            this.rad_woman.Text = "여";
            this.rad_woman.UseVisualStyleBackColor = true;
            // 
            // rad_man
            // 
            this.rad_man.AutoSize = true;
            this.rad_man.Checked = true;
            this.rad_man.Location = new System.Drawing.Point(180, 226);
            this.rad_man.Name = "rad_man";
            this.rad_man.Size = new System.Drawing.Size(35, 16);
            this.rad_man.TabIndex = 10;
            this.rad_man.TabStop = true;
            this.rad_man.Text = "남";
            this.rad_man.UseVisualStyleBackColor = true;
            // 
            // txt_pw_ok
            // 
            this.txt_pw_ok.Location = new System.Drawing.Point(62, 82);
            this.txt_pw_ok.MaxLength = 15;
            this.txt_pw_ok.Name = "txt_pw_ok";
            this.txt_pw_ok.PasswordChar = '*';
            this.txt_pw_ok.Size = new System.Drawing.Size(127, 21);
            this.txt_pw_ok.TabIndex = 9;
            // 
            // txt_tall
            // 
            this.txt_tall.Location = new System.Drawing.Point(62, 115);
            this.txt_tall.MaxLength = 7;
            this.txt_tall.Name = "txt_tall";
            this.txt_tall.Size = new System.Drawing.Size(127, 21);
            this.txt_tall.TabIndex = 8;
            this.txt_tall.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_tall_KeyPress);
            // 
            // txt_age
            // 
            this.txt_age.Location = new System.Drawing.Point(62, 144);
            this.txt_age.MaxLength = 3;
            this.txt_age.Name = "txt_age";
            this.txt_age.Size = new System.Drawing.Size(127, 21);
            this.txt_age.TabIndex = 7;
            this.txt_age.TextChanged += new System.EventHandler(this.txt_age_TextChanged);
            this.txt_age.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_age_KeyPress);
            // 
            // txt_pw
            // 
            this.txt_pw.Location = new System.Drawing.Point(62, 54);
            this.txt_pw.MaxLength = 15;
            this.txt_pw.Name = "txt_pw";
            this.txt_pw.PasswordChar = '*';
            this.txt_pw.Size = new System.Drawing.Size(127, 21);
            this.txt_pw.TabIndex = 5;
            this.txt_pw.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_pw_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "나이";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "키";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "PW 확인";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "PW";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // gbx_Connect
            // 
            this.gbx_Connect.Controls.Add(this.btn_serverStart);
            this.gbx_Connect.Controls.Add(this.txt_port);
            this.gbx_Connect.Controls.Add(this.label10);
            this.gbx_Connect.Controls.Add(this.btn_connect);
            this.gbx_Connect.Controls.Add(this.btn_send);
            this.gbx_Connect.Controls.Add(this.txt_ip);
            this.gbx_Connect.Controls.Add(this.label6);
            this.gbx_Connect.Location = new System.Drawing.Point(222, 390);
            this.gbx_Connect.Name = "gbx_Connect";
            this.gbx_Connect.Size = new System.Drawing.Size(273, 110);
            this.gbx_Connect.TabIndex = 6;
            this.gbx_Connect.TabStop = false;
            this.gbx_Connect.Text = "접속";
            // 
            // btn_serverStart
            // 
            this.btn_serverStart.Location = new System.Drawing.Point(5, 81);
            this.btn_serverStart.Name = "btn_serverStart";
            this.btn_serverStart.Size = new System.Drawing.Size(76, 23);
            this.btn_serverStart.TabIndex = 16;
            this.btn_serverStart.Text = "서버시작";
            this.btn_serverStart.UseVisualStyleBackColor = true;
            this.btn_serverStart.Click += new System.EventHandler(this.btn_serverStart_Click);
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(39, 47);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(78, 21);
            this.txt_port.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "Port";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(199, 81);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(68, 23);
            this.btn_connect.TabIndex = 12;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(134, 81);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(55, 23);
            this.btn_send.TabIndex = 13;
            this.btn_send.Text = "보내기";
            this.btn_send.UseVisualStyleBackColor = true;
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(39, 20);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(228, 21);
            this.txt_ip.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "IP";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(431, 361);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(47, 23);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "취소";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_CancelDB);
            this.groupBox4.Controls.Add(this.btn_ConnectDB);
            this.groupBox4.Location = new System.Drawing.Point(16, 39);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(161, 60);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DB접속";
            // 
            // btn_CancelDB
            // 
            this.btn_CancelDB.Location = new System.Drawing.Point(77, 20);
            this.btn_CancelDB.Name = "btn_CancelDB";
            this.btn_CancelDB.Size = new System.Drawing.Size(64, 23);
            this.btn_CancelDB.TabIndex = 10;
            this.btn_CancelDB.Text = "접속해제";
            this.btn_CancelDB.UseVisualStyleBackColor = true;
            this.btn_CancelDB.Click += new System.EventHandler(this.btn_CancelDB_Click);
            // 
            // btn_ConnectDB
            // 
            this.btn_ConnectDB.Location = new System.Drawing.Point(9, 20);
            this.btn_ConnectDB.Name = "btn_ConnectDB";
            this.btn_ConnectDB.Size = new System.Drawing.Size(62, 23);
            this.btn_ConnectDB.TabIndex = 9;
            this.btn_ConnectDB.Text = "접속";
            this.btn_ConnectDB.UseVisualStyleBackColor = true;
            this.btn_ConnectDB.Click += new System.EventHandler(this.btn_ConnectDB_Click);
            // 
            // ucFormChlee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.gbx_Connect);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.gbx_memberData);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.gbx_ListView);
            this.Name = "ucFormChlee";
            this.Size = new System.Drawing.Size(614, 547);
            this.gbx_ListView.ResumeLayout(false);
            this.gbx_ListView.PerformLayout();
            this.gbx_memberData.ResumeLayout(false);
            this.gbx_memberData.PerformLayout();
            this.gbx_Connect.ResumeLayout(false);
            this.gbx_Connect.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.ListView ListView;
        private System.Windows.Forms.GroupBox gbx_ListView;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox gbx_memberData;
        private System.Windows.Forms.TextBox txt_pw_ok;
        private System.Windows.Forms.TextBox txt_tall;
        private System.Windows.Forms.TextBox txt_age;
        private System.Windows.Forms.TextBox txt_pw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.GroupBox gbx_Connect;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.RadioButton rad_woman;
        private System.Windows.Forms.RadioButton rad_man;
        private System.Windows.Forms.TextBox txt_count;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chk_AgeUpDown;
        private System.Windows.Forms.MaskedTextBox txt_createDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox txt_modifyDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColumnHeader _columnID;
        private System.Windows.Forms.ColumnHeader _columnAge;
        private System.Windows.Forms.ColumnHeader _columSex;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_CancelDB;
        private System.Windows.Forms.Button btn_ConnectDB;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Button btn_serverStart;
    }
}
