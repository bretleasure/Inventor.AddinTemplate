using System.ComponentModel;

namespace Inventor.AddinTemplate
{
	partial class DefaultUserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			btn_Close = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.Location = new System.Drawing.Point(29, 35);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(214, 39);
			label1.TabIndex = 0;
			label1.Text = "label1";
			// 
			// button1
			// 
			button1.Location = new System.Drawing.Point(70, 117);
			button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(128, 30);
			button1.TabIndex = 1;
			button1.Text = "button1";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// btn_Close
			// 
			btn_Close.Location = new System.Drawing.Point(105, 190);
			btn_Close.Name = "btn_Close";
			btn_Close.Size = new System.Drawing.Size(114, 37);
			btn_Close.TabIndex = 2;
			btn_Close.Text = "Close";
			btn_Close.UseVisualStyleBackColor = true;
			btn_Close.Click += btn_Close_Click;
			// 
			// UserControl1
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(btn_Close);
			Controls.Add(button1);
			Controls.Add(label1);
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Size = new System.Drawing.Size(404, 355);
			ResumeLayout(false);
		}

		private System.Windows.Forms.Button btn_Close;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;

		#endregion
	}
}