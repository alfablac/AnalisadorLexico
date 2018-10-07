namespace AnalisadorLexico
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.rtbCodigoFonte = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbLexema = new System.Windows.Forms.ListBox();
            this.lbIdent = new System.Windows.Forms.ListBox();
            this.lbErro = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbIndex = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // rtbCodigoFonte
            // 
            this.rtbCodigoFonte.Location = new System.Drawing.Point(12, 22);
            this.rtbCodigoFonte.Name = "rtbCodigoFonte";
            this.rtbCodigoFonte.Size = new System.Drawing.Size(365, 335);
            this.rtbCodigoFonte.TabIndex = 0;
            this.rtbCodigoFonte.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(157, 363);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Analisar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbLexema
            // 
            this.lbLexema.FormattingEnabled = true;
            this.lbLexema.Location = new System.Drawing.Point(383, 54);
            this.lbLexema.Name = "lbLexema";
            this.lbLexema.Size = new System.Drawing.Size(266, 303);
            this.lbLexema.TabIndex = 2;
            this.lbLexema.SelectedIndexChanged += new System.EventHandler(this.lbLexema_SelectedIndexChanged);
            this.lbLexema.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbLexema_MouseMove);
            // 
            // lbIdent
            // 
            this.lbIdent.FormattingEnabled = true;
            this.lbIdent.Location = new System.Drawing.Point(655, 54);
            this.lbIdent.Name = "lbIdent";
            this.lbIdent.Size = new System.Drawing.Size(266, 303);
            this.lbIdent.TabIndex = 3;
            this.lbIdent.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbIdent_MouseMove);
            // 
            // lbErro
            // 
            this.lbErro.FormattingEnabled = true;
            this.lbErro.Location = new System.Drawing.Point(927, 54);
            this.lbErro.Name = "lbErro";
            this.lbErro.Size = new System.Drawing.Size(266, 303);
            this.lbErro.TabIndex = 4;
            this.lbErro.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbErro_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(494, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Lexema";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(756, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Identificador";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1047, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Erro";
            // 
            // lbIndex
            // 
            this.lbIndex.FormattingEnabled = true;
            this.lbIndex.Location = new System.Drawing.Point(1199, 54);
            this.lbIndex.Name = "lbIndex";
            this.lbIndex.Size = new System.Drawing.Size(19, 303);
            this.lbIndex.TabIndex = 8;
            this.lbIndex.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(383, 370);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(810, 16);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 391);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lbIndex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbErro);
            this.Controls.Add(this.lbIdent);
            this.Controls.Add(this.lbLexema);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtbCodigoFonte);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbCodigoFonte;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lbLexema;
        private System.Windows.Forms.ListBox lbIdent;
        private System.Windows.Forms.ListBox lbErro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListBox lbIndex;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

