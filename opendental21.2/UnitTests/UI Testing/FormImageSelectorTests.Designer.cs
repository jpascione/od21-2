﻿namespace UnitTests
{
	partial class FormImageSelectorTests
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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// FormImageSelectorTests
			// 
			this.ClientSize = new System.Drawing.Size(315, 272);
			this.Controls.Add(this.button1);
			this.Name = "FormImageSelectorTests";
			this.Text = "Image Selector Tests";
			this.Load += new System.EventHandler(this.FormImageSelectorTests_Load);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button button1;
	}
}