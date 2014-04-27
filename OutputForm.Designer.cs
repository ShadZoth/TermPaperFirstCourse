namespace WindowsFormsApplication1
{
    partial class OutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputForm));
            this.prevButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.task = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.saveAll = new System.Windows.Forms.ToolStripButton();
            this.editButton = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // prevButton
            // 
            this.prevButton.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prevButton.Location = new System.Drawing.Point(16, 737);
            this.prevButton.Margin = new System.Windows.Forms.Padding(4);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(165, 41);
            this.prevButton.TabIndex = 1;
            this.prevButton.Text = "< Назад";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nextButton.Location = new System.Drawing.Point(588, 737);
            this.nextButton.Margin = new System.Windows.Forms.Padding(4);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(165, 41);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Вперед >";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // task
            // 
            this.task.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.task.Location = new System.Drawing.Point(16, 39);
            this.task.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.task.Multiline = true;
            this.task.Name = "task";
            this.task.ReadOnly = true;
            this.task.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.task.Size = new System.Drawing.Size(737, 677);
            this.task.TabIndex = 2;
            this.task.Text = "label1";
            this.task.WordWrap = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.save,
            this.saveAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(775, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // save
            // 
            this.save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.save.Image = ((System.Drawing.Image)(resources.GetObject("save.Image")));
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(23, 22);
            this.save.Text = "Сохранить";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // saveAll
            // 
            this.saveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAll.Image = ((System.Drawing.Image)(resources.GetObject("saveAll.Image")));
            this.saveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAll.Name = "saveAll";
            this.saveAll.Size = new System.Drawing.Size(23, 22);
            this.saveAll.Text = "Сохранить всё";
            this.saveAll.Click += new System.EventHandler(this.saveAll_Click);
            // 
            // editButton
            // 
            this.editButton.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.Location = new System.Drawing.Point(206, 737);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(354, 41);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "Редактировать";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 790);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.task);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.prevButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(785, 829);
            this.MinimumSize = new System.Drawing.Size(785, 829);
            this.Name = "OutputForm";
            this.Text = "Form2";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.TextBox task;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripButton saveAll;
        private System.Windows.Forms.Button editButton;

    }
}
