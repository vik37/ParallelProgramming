namespace AsynchronusProgramming
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
			Calculate = new Button();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			DisplayText = new Label();
			SuspendLayout();
			// 
			// Calculate
			// 
			Calculate.AccessibleName = "Calculate";
			Calculate.BackColor = Color.Teal;
			Calculate.BackgroundImageLayout = ImageLayout.Stretch;
			Calculate.CausesValidation = false;
			Calculate.Cursor = Cursors.Hand;
			Calculate.FlatAppearance.BorderColor = Color.FromArgb(64, 64, 64);
			Calculate.FlatAppearance.BorderSize = 2;
			Calculate.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0, true);
			Calculate.ForeColor = Color.WhiteSmoke;
			Calculate.Location = new Point(341, 21);
			Calculate.Margin = new Padding(3, 2, 3, 2);
			Calculate.Name = "Calculate";
			Calculate.Size = new Size(123, 49);
			Calculate.TabIndex = 0;
			Calculate.Text = "Calculate";
			Calculate.UseVisualStyleBackColor = false;
			Calculate.Click += button1_Click;
			// 
			// DisplayText
			// 
			DisplayText.AccessibleName = "DisplayText";
			DisplayText.AutoSize = true;
			DisplayText.Location = new Point(341, 102);
			DisplayText.Name = "DisplayText";
			DisplayText.Size = new Size(108, 15);
			DisplayText.TabIndex = 1;
			DisplayText.Text = "Displey Calculation";
			DisplayText.TextAlign = ContentAlignment.BottomLeft;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(700, 338);
			Controls.Add(DisplayText);
			Controls.Add(Calculate);
			Margin = new Padding(3, 2, 3, 2);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button Calculate;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private Label DisplayText;
	}
}
