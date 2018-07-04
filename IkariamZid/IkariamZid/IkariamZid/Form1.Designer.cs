namespace IkariamZid
{
    partial class FormIkariamZid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIkariamZid));
            this.upDownStopnjaZidu = new System.Windows.Forms.NumericUpDown();
            this.comboBoxOrozje = new System.Windows.Forms.ComboBox();
            this.upDownNadgradnja = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStEnot = new System.Windows.Forms.TextBox();
            this.buttonIzracunaj = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sloveneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.upDownStopnjaZidu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownNadgradnja)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // upDownStopnjaZidu
            // 
            resources.ApplyResources(this.upDownStopnjaZidu, "upDownStopnjaZidu");
            this.upDownStopnjaZidu.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.upDownStopnjaZidu.Name = "upDownStopnjaZidu";
            // 
            // comboBoxOrozje
            // 
            resources.ApplyResources(this.comboBoxOrozje, "comboBoxOrozje");
            this.comboBoxOrozje.FormattingEnabled = true;
            this.comboBoxOrozje.Items.AddRange(new object[] {
            resources.GetString("comboBoxOrozje.Items"),
            resources.GetString("comboBoxOrozje.Items1"),
            resources.GetString("comboBoxOrozje.Items2")});
            this.comboBoxOrozje.Name = "comboBoxOrozje";
            this.comboBoxOrozje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxOrozje_KeyPress);
            // 
            // upDownNadgradnja
            // 
            resources.ApplyResources(this.upDownNadgradnja, "upDownNadgradnja");
            this.upDownNadgradnja.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.upDownNadgradnja.Name = "upDownNadgradnja";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxStEnot
            // 
            resources.ApplyResources(this.textBoxStEnot, "textBoxStEnot");
            this.textBoxStEnot.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxStEnot.Name = "textBoxStEnot";
            this.textBoxStEnot.ReadOnly = true;
            // 
            // buttonIzracunaj
            // 
            resources.ApplyResources(this.buttonIzracunaj, "buttonIzracunaj");
            this.buttonIzracunaj.Name = "buttonIzracunaj";
            this.buttonIzracunaj.UseVisualStyleBackColor = true;
            this.buttonIzracunaj.Click += new System.EventHandler(this.buttonIzracunaj_Click);
            // 
            // buttonAbout
            // 
            resources.ApplyResources(this.buttonAbout, "buttonAbout");
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            // 
            // languageToolStripMenuItem
            // 
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.sloveneToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            // 
            // englishToolStripMenuItem
            // 
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // sloveneToolStripMenuItem
            // 
            resources.ApplyResources(this.sloveneToolStripMenuItem, "sloveneToolStripMenuItem");
            this.sloveneToolStripMenuItem.Name = "sloveneToolStripMenuItem";
            this.sloveneToolStripMenuItem.Click += new System.EventHandler(this.sloveneToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // FormIkariamZid
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonIzracunaj);
            this.Controls.Add(this.textBoxStEnot);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.upDownNadgradnja);
            this.Controls.Add(this.comboBoxOrozje);
            this.Controls.Add(this.upDownStopnjaZidu);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormIkariamZid";
            ((System.ComponentModel.ISupportInitialize)(this.upDownStopnjaZidu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownNadgradnja)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown upDownStopnjaZidu;
        private System.Windows.Forms.ComboBox comboBoxOrozje;
        private System.Windows.Forms.NumericUpDown upDownNadgradnja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStEnot;
        private System.Windows.Forms.Button buttonIzracunaj;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sloveneToolStripMenuItem;
    }
}

