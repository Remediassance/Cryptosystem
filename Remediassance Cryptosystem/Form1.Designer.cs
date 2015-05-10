namespace Remediassance_Cryptosystem
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.encodeKeyBtn = new System.Windows.Forms.Button();
            this.openKeyFileBtn = new System.Windows.Forms.Button();
            this.encodeBtn = new System.Windows.Forms.Button();
            this.decodeBtn = new System.Windows.Forms.Button();
            this.openTextFileBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseFileBtn = new System.Windows.Forms.Button();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChooseKeyBtn = new System.Windows.Forms.Button();
            this.keyNameBox = new System.Windows.Forms.TextBox();
            this.expandBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.eTextBox = new System.Windows.Forms.TextBox();
            this.dTextBox = new System.Windows.Forms.TextBox();
            this.nTextBox = new System.Windows.Forms.TextBox();
            this.decodeKeyBtn = new System.Windows.Forms.Button();
            this.createSeanseBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // encodeKeyBtn
            // 
            this.encodeKeyBtn.Enabled = false;
            this.encodeKeyBtn.Location = new System.Drawing.Point(107, 128);
            this.encodeKeyBtn.Name = "encodeKeyBtn";
            this.encodeKeyBtn.Size = new System.Drawing.Size(92, 36);
            this.encodeKeyBtn.TabIndex = 0;
            this.encodeKeyBtn.Text = "ШИФР КЛЮЧА";
            this.encodeKeyBtn.UseVisualStyleBackColor = true;
            this.encodeKeyBtn.Click += new System.EventHandler(this.encodeKeyBtn_Click);
            // 
            // openKeyFileBtn
            // 
            this.openKeyFileBtn.Enabled = false;
            this.openKeyFileBtn.Location = new System.Drawing.Point(303, 170);
            this.openKeyFileBtn.Name = "openKeyFileBtn";
            this.openKeyFileBtn.Size = new System.Drawing.Size(92, 36);
            this.openKeyFileBtn.TabIndex = 1;
            this.openKeyFileBtn.Text = "Открыть ключ";
            this.openKeyFileBtn.UseVisualStyleBackColor = true;
            this.openKeyFileBtn.Click += new System.EventHandler(this.openKeyBtn_Click);
            // 
            // encodeBtn
            // 
            this.encodeBtn.Enabled = false;
            this.encodeBtn.Location = new System.Drawing.Point(205, 128);
            this.encodeBtn.Name = "encodeBtn";
            this.encodeBtn.Size = new System.Drawing.Size(92, 36);
            this.encodeBtn.TabIndex = 2;
            this.encodeBtn.Text = "Шифровать файл";
            this.encodeBtn.UseVisualStyleBackColor = true;
            this.encodeBtn.Click += new System.EventHandler(this.encodeBtn_Click);
            // 
            // decodeBtn
            // 
            this.decodeBtn.Enabled = false;
            this.decodeBtn.Location = new System.Drawing.Point(205, 170);
            this.decodeBtn.Name = "decodeBtn";
            this.decodeBtn.Size = new System.Drawing.Size(92, 36);
            this.decodeBtn.TabIndex = 3;
            this.decodeBtn.Text = "Дешифровать файл";
            this.decodeBtn.UseVisualStyleBackColor = true;
            this.decodeBtn.Click += new System.EventHandler(this.decodeBtn_Click);
            // 
            // openTextFileBtn
            // 
            this.openTextFileBtn.Enabled = false;
            this.openTextFileBtn.Location = new System.Drawing.Point(303, 128);
            this.openTextFileBtn.Name = "openTextFileBtn";
            this.openTextFileBtn.Size = new System.Drawing.Size(92, 36);
            this.openTextFileBtn.TabIndex = 4;
            this.openTextFileBtn.Text = "Открыть файл";
            this.openTextFileBtn.UseVisualStyleBackColor = true;
            this.openTextFileBtn.Click += new System.EventHandler(this.openTextFileBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Выберите исходный файл";
            // 
            // chooseFileBtn
            // 
            this.chooseFileBtn.Location = new System.Drawing.Point(354, 31);
            this.chooseFileBtn.Name = "chooseFileBtn";
            this.chooseFileBtn.Size = new System.Drawing.Size(26, 21);
            this.chooseFileBtn.TabIndex = 10;
            this.chooseFileBtn.Text = "...";
            this.chooseFileBtn.UseVisualStyleBackColor = true;
            this.chooseFileBtn.Click += new System.EventHandler(this.chooseFileBtn_Click);
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(6, 31);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.Size = new System.Drawing.Size(342, 20);
            this.fileNameBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Выберите файл ключа";
            // 
            // ChooseKeyBtn
            // 
            this.ChooseKeyBtn.Location = new System.Drawing.Point(354, 70);
            this.ChooseKeyBtn.Name = "ChooseKeyBtn";
            this.ChooseKeyBtn.Size = new System.Drawing.Size(26, 21);
            this.ChooseKeyBtn.TabIndex = 13;
            this.ChooseKeyBtn.Text = "...";
            this.ChooseKeyBtn.UseVisualStyleBackColor = true;
            this.ChooseKeyBtn.Click += new System.EventHandler(this.chooseKeyBtn_Click);
            // 
            // keyNameBox
            // 
            this.keyNameBox.Location = new System.Drawing.Point(6, 71);
            this.keyNameBox.Name = "keyNameBox";
            this.keyNameBox.Size = new System.Drawing.Size(342, 20);
            this.keyNameBox.TabIndex = 12;
            // 
            // expandBtn
            // 
            this.expandBtn.Location = new System.Drawing.Point(9, 212);
            this.expandBtn.Name = "expandBtn";
            this.expandBtn.Size = new System.Drawing.Size(386, 22);
            this.expandBtn.TabIndex = 15;
            this.expandBtn.Text = "↓";
            this.expandBtn.UseVisualStyleBackColor = true;
            this.expandBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.eTextBox);
            this.groupBox1.Controls.Add(this.dTextBox);
            this.groupBox1.Controls.Add(this.nTextBox);
            this.groupBox1.Location = new System.Drawing.Point(9, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(386, 162);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Переменные";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "e";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "d";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "n";
            // 
            // eTextBox
            // 
            this.eTextBox.Location = new System.Drawing.Point(22, 142);
            this.eTextBox.Name = "eTextBox";
            this.eTextBox.ReadOnly = true;
            this.eTextBox.Size = new System.Drawing.Size(358, 20);
            this.eTextBox.TabIndex = 2;
            // 
            // dTextBox
            // 
            this.dTextBox.Location = new System.Drawing.Point(22, 116);
            this.dTextBox.Name = "dTextBox";
            this.dTextBox.ReadOnly = true;
            this.dTextBox.Size = new System.Drawing.Size(358, 20);
            this.dTextBox.TabIndex = 1;
            // 
            // nTextBox
            // 
            this.nTextBox.Location = new System.Drawing.Point(22, 90);
            this.nTextBox.Name = "nTextBox";
            this.nTextBox.ReadOnly = true;
            this.nTextBox.Size = new System.Drawing.Size(358, 20);
            this.nTextBox.TabIndex = 0;
            // 
            // decodeKeyBtn
            // 
            this.decodeKeyBtn.Enabled = false;
            this.decodeKeyBtn.Location = new System.Drawing.Point(107, 170);
            this.decodeKeyBtn.Name = "decodeKeyBtn";
            this.decodeKeyBtn.Size = new System.Drawing.Size(92, 36);
            this.decodeKeyBtn.TabIndex = 17;
            this.decodeKeyBtn.Text = "ДЕШИФР КЛЮЧА";
            this.decodeKeyBtn.UseVisualStyleBackColor = true;
            this.decodeKeyBtn.Click += new System.EventHandler(this.decodeKeyBtn_Click);
            // 
            // createSeanseBtn
            // 
            this.createSeanseBtn.Location = new System.Drawing.Point(9, 128);
            this.createSeanseBtn.Name = "createSeanseBtn";
            this.createSeanseBtn.Size = new System.Drawing.Size(92, 78);
            this.createSeanseBtn.TabIndex = 18;
            this.createSeanseBtn.Text = "СОЗДАТЬ КЛЮЧ";
            this.createSeanseBtn.UseVisualStyleBackColor = true;
            this.createSeanseBtn.Click += new System.EventHandler(this.createSeanseBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.fileNameBox);
            this.groupBox2.Controls.Add(this.chooseFileBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.keyNameBox);
            this.groupBox2.Controls.Add(this.ChooseKeyBtn);
            this.groupBox2.Location = new System.Drawing.Point(9, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 110);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Файлы";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Сеансовый ключ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(98, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(282, 20);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(98, 45);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(282, 20);
            this.textBox2.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Исходный текст";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 412);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.openKeyFileBtn);
            this.Controls.Add(this.createSeanseBtn);
            this.Controls.Add(this.openTextFileBtn);
            this.Controls.Add(this.decodeKeyBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.expandBtn);
            this.Controls.Add(this.decodeBtn);
            this.Controls.Add(this.encodeKeyBtn);
            this.Controls.Add(this.encodeBtn);
            this.Name = "Form1";
            this.Text = "Such Cryptosystem, Much Wow";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button encodeKeyBtn;
        private System.Windows.Forms.Button openKeyFileBtn;
        private System.Windows.Forms.Button encodeBtn;
        private System.Windows.Forms.Button decodeBtn;
        private System.Windows.Forms.Button openTextFileBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseFileBtn;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ChooseKeyBtn;
        private System.Windows.Forms.TextBox keyNameBox;
        private System.Windows.Forms.Button expandBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox eTextBox;
        private System.Windows.Forms.TextBox dTextBox;
        private System.Windows.Forms.TextBox nTextBox;
        private System.Windows.Forms.Button decodeKeyBtn;
        private System.Windows.Forms.Button createSeanseBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
    }
}

