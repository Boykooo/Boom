namespace Client
{
    partial class MainForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.играToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.начатьИгруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OneShip = new System.Windows.Forms.RadioButton();
            this.TwoShip = new System.Windows.Forms.RadioButton();
            this.ThreeShip = new System.Windows.Forms.RadioButton();
            this.FourShip = new System.Windows.Forms.RadioButton();
            this.ClearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(130, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(327, 305);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(506, 27);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(327, 305);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.играToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(847, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // играToolStripMenuItem
            // 
            this.играToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.начатьИгруToolStripMenuItem});
            this.играToolStripMenuItem.Name = "играToolStripMenuItem";
            this.играToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.играToolStripMenuItem.Text = "Игра";
            // 
            // начатьИгруToolStripMenuItem
            // 
            this.начатьИгруToolStripMenuItem.Name = "начатьИгруToolStripMenuItem";
            this.начатьИгруToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.начатьИгруToolStripMenuItem.Text = "Начать игру";
            this.начатьИгруToolStripMenuItem.Click += new System.EventHandler(this.начатьИгруToolStripMenuItem_Click);
            // 
            // OneShip
            // 
            this.OneShip.AutoSize = true;
            this.OneShip.Location = new System.Drawing.Point(14, 213);
            this.OneShip.Name = "OneShip";
            this.OneShip.Size = new System.Drawing.Size(69, 17);
            this.OneShip.TabIndex = 8;
            this.OneShip.TabStop = true;
            this.OneShip.Text = "1 палуба";
            this.OneShip.UseVisualStyleBackColor = true;
            // 
            // TwoShip
            // 
            this.TwoShip.AutoSize = true;
            this.TwoShip.Location = new System.Drawing.Point(14, 236);
            this.TwoShip.Name = "TwoShip";
            this.TwoShip.Size = new System.Drawing.Size(71, 17);
            this.TwoShip.TabIndex = 9;
            this.TwoShip.TabStop = true;
            this.TwoShip.Text = "2 палубы";
            this.TwoShip.UseVisualStyleBackColor = true;
            // 
            // ThreeShip
            // 
            this.ThreeShip.AutoSize = true;
            this.ThreeShip.Location = new System.Drawing.Point(14, 259);
            this.ThreeShip.Name = "ThreeShip";
            this.ThreeShip.Size = new System.Drawing.Size(71, 17);
            this.ThreeShip.TabIndex = 10;
            this.ThreeShip.TabStop = true;
            this.ThreeShip.Text = "3 палубы";
            this.ThreeShip.UseVisualStyleBackColor = true;
            // 
            // FourShip
            // 
            this.FourShip.AutoSize = true;
            this.FourShip.Location = new System.Drawing.Point(14, 282);
            this.FourShip.Name = "FourShip";
            this.FourShip.Size = new System.Drawing.Size(71, 17);
            this.FourShip.TabIndex = 11;
            this.FourShip.TabStop = true;
            this.FourShip.Text = "4 палубы";
            this.FourShip.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(12, 315);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(71, 31);
            this.ClearButton.TabIndex = 12;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 358);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.FourShip);
            this.Controls.Add(this.ThreeShip);
            this.Controls.Add(this.TwoShip);
            this.Controls.Add(this.OneShip);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Морской бой";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem играToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem начатьИгруToolStripMenuItem;
        private System.Windows.Forms.RadioButton OneShip;
        private System.Windows.Forms.RadioButton TwoShip;
        private System.Windows.Forms.RadioButton ThreeShip;
        private System.Windows.Forms.RadioButton FourShip;
        private System.Windows.Forms.Button ClearButton;
    }
}

