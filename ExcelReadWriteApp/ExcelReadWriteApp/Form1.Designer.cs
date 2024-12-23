namespace ExcelReadWriteApp
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
            buttonReadExcel = new Button();
            listBox1 = new ListBox();
            buttonWriteExcel = new Button();
            SuspendLayout();
            // 
            // buttonReadExcel
            // 
            buttonReadExcel.Location = new Point(114, 101);
            buttonReadExcel.Name = "buttonReadExcel";
            buttonReadExcel.Size = new Size(94, 29);
            buttonReadExcel.TabIndex = 0;
            buttonReadExcel.Text = "Read Excel";
            buttonReadExcel.UseVisualStyleBackColor = true;
            buttonReadExcel.Click += buttonReadExcel_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(82, 163);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(625, 204);
            listBox1.TabIndex = 1;
            // 
            // buttonWriteExcel
            // 
            buttonWriteExcel.Location = new Point(252, 101);
            buttonWriteExcel.Name = "buttonWriteExcel";
            buttonWriteExcel.Size = new Size(94, 29);
            buttonWriteExcel.TabIndex = 0;
            buttonWriteExcel.Text = "Write Excel";
            buttonWriteExcel.UseVisualStyleBackColor = true;
            buttonWriteExcel.Click += buttonWriteExcel_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBox1);
            Controls.Add(buttonWriteExcel);
            Controls.Add(buttonReadExcel);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonReadExcel;
        private ListBox listBox1;
        private Button buttonWriteExcel;
    }
}
