namespace Project_DataStructure.chlee
{
    partial class chlee
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
            this.btnRemoveList = new System.Windows.Forms.Button();
            this.btnAddList = new System.Windows.Forms.Button();
            this.btnRemoveQueue = new System.Windows.Forms.Button();
            this.btnAddQueue = new System.Windows.Forms.Button();
            this.btnRemoveScack = new System.Windows.Forms.Button();
            this.btnAddScack = new System.Windows.Forms.Button();
            this.tbxEmail = new System.Windows.Forms.TextBox();
            this.tbxPhoneNumber = new System.Windows.Forms.TextBox();
            this.tbxAge = new System.Windows.Forms.TextBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxCurrentCount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnRemoveList
            // 
            this.btnRemoveList.Location = new System.Drawing.Point(109, 176);
            this.btnRemoveList.Name = "btnRemoveList";
            this.btnRemoveList.Size = new System.Drawing.Size(98, 23);
            this.btnRemoveList.TabIndex = 6;
            this.btnRemoveList.Text = "리스트 제거";
            this.btnRemoveList.UseVisualStyleBackColor = true;
            this.btnRemoveList.Click += new System.EventHandler(this.btnRemoveList_Click);
            // 
            // btnAddList
            // 
            this.btnAddList.Location = new System.Drawing.Point(5, 176);
            this.btnAddList.Name = "btnAddList";
            this.btnAddList.Size = new System.Drawing.Size(98, 23);
            this.btnAddList.TabIndex = 7;
            this.btnAddList.Text = "리스트 넣기";
            this.btnAddList.UseVisualStyleBackColor = true;
            this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
            // 
            // btnRemoveQueue
            // 
            this.btnRemoveQueue.Location = new System.Drawing.Point(109, 147);
            this.btnRemoveQueue.Name = "btnRemoveQueue";
            this.btnRemoveQueue.Size = new System.Drawing.Size(98, 23);
            this.btnRemoveQueue.TabIndex = 8;
            this.btnRemoveQueue.Text = "큐 제거";
            this.btnRemoveQueue.UseVisualStyleBackColor = true;
            this.btnRemoveQueue.Click += new System.EventHandler(this.btnRemoveQueue_Click);
            // 
            // btnAddQueue
            // 
            this.btnAddQueue.Location = new System.Drawing.Point(5, 147);
            this.btnAddQueue.Name = "btnAddQueue";
            this.btnAddQueue.Size = new System.Drawing.Size(98, 23);
            this.btnAddQueue.TabIndex = 9;
            this.btnAddQueue.Text = "큐 넣기";
            this.btnAddQueue.UseVisualStyleBackColor = true;
            this.btnAddQueue.Click += new System.EventHandler(this.btnAddQueue_Click);
            // 
            // btnRemoveScack
            // 
            this.btnRemoveScack.Location = new System.Drawing.Point(109, 118);
            this.btnRemoveScack.Name = "btnRemoveScack";
            this.btnRemoveScack.Size = new System.Drawing.Size(98, 23);
            this.btnRemoveScack.TabIndex = 10;
            this.btnRemoveScack.Text = "스택 제거";
            this.btnRemoveScack.UseVisualStyleBackColor = true;
            this.btnRemoveScack.Click += new System.EventHandler(this.btnRemoveScack_Click);
            // 
            // btnAddScack
            // 
            this.btnAddScack.Location = new System.Drawing.Point(5, 118);
            this.btnAddScack.Name = "btnAddScack";
            this.btnAddScack.Size = new System.Drawing.Size(98, 23);
            this.btnAddScack.TabIndex = 11;
            this.btnAddScack.Text = "스택 넣기";
            this.btnAddScack.UseVisualStyleBackColor = true;
            this.btnAddScack.Click += new System.EventHandler(this.btnAddScack_Click);
            // 
            // tbxEmail
            // 
            this.tbxEmail.Location = new System.Drawing.Point(3, 84);
            this.tbxEmail.Name = "tbxEmail";
            this.tbxEmail.Size = new System.Drawing.Size(100, 21);
            this.tbxEmail.TabIndex = 2;
            this.tbxEmail.TextChanged += new System.EventHandler(this.tbxEmail_TextChanged);
            // 
            // tbxPhoneNumber
            // 
            this.tbxPhoneNumber.Location = new System.Drawing.Point(3, 57);
            this.tbxPhoneNumber.Name = "tbxPhoneNumber";
            this.tbxPhoneNumber.Size = new System.Drawing.Size(100, 21);
            this.tbxPhoneNumber.TabIndex = 3;
            this.tbxPhoneNumber.TextChanged += new System.EventHandler(this.tbxPhoneNumber_TextChanged);
            // 
            // tbxAge
            // 
            this.tbxAge.Location = new System.Drawing.Point(3, 30);
            this.tbxAge.Name = "tbxAge";
            this.tbxAge.Size = new System.Drawing.Size(100, 21);
            this.tbxAge.TabIndex = 4;
            this.tbxAge.TextChanged += new System.EventHandler(this.tbxAge_TextChanged);
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(3, 3);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(100, 21);
            this.tbxName.TabIndex = 5;
            this.tbxName.TextChanged += new System.EventHandler(this.tbxName_TextChanged);
            // 
            // tbxCurrentCount
            // 
            this.tbxCurrentCount.Location = new System.Drawing.Point(107, 84);
            this.tbxCurrentCount.Name = "tbxCurrentCount";
            this.tbxCurrentCount.Size = new System.Drawing.Size(100, 21);
            this.tbxCurrentCount.TabIndex = 2;
            this.tbxCurrentCount.TextChanged += new System.EventHandler(this.tbxCurrentCount_TextChanged);
            // 
            // chlee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemoveList);
            this.Controls.Add(this.btnAddList);
            this.Controls.Add(this.btnRemoveQueue);
            this.Controls.Add(this.btnAddQueue);
            this.Controls.Add(this.btnRemoveScack);
            this.Controls.Add(this.btnAddScack);
            this.Controls.Add(this.tbxCurrentCount);
            this.Controls.Add(this.tbxEmail);
            this.Controls.Add(this.tbxPhoneNumber);
            this.Controls.Add(this.tbxAge);
            this.Controls.Add(this.tbxName);
            this.Name = "chlee";
            this.Size = new System.Drawing.Size(214, 204);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRemoveList;
        private System.Windows.Forms.Button btnAddList;
        private System.Windows.Forms.Button btnRemoveQueue;
        private System.Windows.Forms.Button btnAddQueue;
        private System.Windows.Forms.Button btnRemoveScack;
        private System.Windows.Forms.Button btnAddScack;
        private System.Windows.Forms.TextBox tbxEmail;
        private System.Windows.Forms.TextBox tbxPhoneNumber;
        private System.Windows.Forms.TextBox tbxAge;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxCurrentCount;
    }
}
