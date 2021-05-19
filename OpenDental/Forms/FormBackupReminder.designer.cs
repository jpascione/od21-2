namespace OpenDental{
	partial class FormBackupReminder {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBackupReminder));
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.checkNoBackups = new System.Windows.Forms.CheckBox();
			this.checkBackupMethodOnline = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.checkBackupMethodRemovable = new System.Windows.Forms.CheckBox();
			this.checkBackupMethodOther = new System.Windows.Forms.CheckBox();
			this.checkBackupMethodNetwork = new System.Windows.Forms.CheckBox();
			this.checkRestoreServer = new System.Windows.Forms.CheckBox();
			this.checkRestoreHome = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkNoProof = new System.Windows.Forms.CheckBox();
			this.checkNoStrategy = new System.Windows.Forms.CheckBox();
			this.checkSecondaryMethodHardCopy = new System.Windows.Forms.CheckBox();
			this.checkSecondaryMethodArchive = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Location = new System.Drawing.Point(525, 406);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(552, 74);
			this.label1.TabIndex = 4;
			this.label1.Text = resources.GetString("label1.Text");
			// 
			// checkNoBackups
			// 
			this.checkNoBackups.Location = new System.Drawing.Point(45, 191);
			this.checkNoBackups.Name = "checkNoBackups";
			this.checkNoBackups.Size = new System.Drawing.Size(151, 20);
			this.checkNoBackups.TabIndex = 6;
			this.checkNoBackups.Text = "No backups";
			this.checkNoBackups.UseVisualStyleBackColor = true;
			// 
			// checkBackupMethodOnline
			// 
			this.checkBackupMethodOnline.Location = new System.Drawing.Point(45, 111);
			this.checkBackupMethodOnline.Name = "checkBackupMethodOnline";
			this.checkBackupMethodOnline.Size = new System.Drawing.Size(200, 20);
			this.checkBackupMethodOnline.TabIndex = 8;
			this.checkBackupMethodOnline.Text = "Online";
			this.checkBackupMethodOnline.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(42, 95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(523, 18);
			this.label3.TabIndex = 7;
			this.label3.Text = "Do you make backups every single day?  Backup method:";
			// 
			// checkBackupMethodRemovable
			// 
			this.checkBackupMethodRemovable.Location = new System.Drawing.Point(45, 131);
			this.checkBackupMethodRemovable.Name = "checkBackupMethodRemovable";
			this.checkBackupMethodRemovable.Size = new System.Drawing.Size(530, 20);
			this.checkBackupMethodRemovable.TabIndex = 9;
			this.checkBackupMethodRemovable.Text = "Removable (external HD, USB drive, etc)";
			this.checkBackupMethodRemovable.UseVisualStyleBackColor = true;
			// 
			// checkBackupMethodOther
			// 
			this.checkBackupMethodOther.Location = new System.Drawing.Point(45, 171);
			this.checkBackupMethodOther.Name = "checkBackupMethodOther";
			this.checkBackupMethodOther.Size = new System.Drawing.Size(151, 20);
			this.checkBackupMethodOther.TabIndex = 11;
			this.checkBackupMethodOther.Text = "Other backup method";
			this.checkBackupMethodOther.UseVisualStyleBackColor = true;
			// 
			// checkBackupMethodNetwork
			// 
			this.checkBackupMethodNetwork.Location = new System.Drawing.Point(45, 151);
			this.checkBackupMethodNetwork.Name = "checkBackupMethodNetwork";
			this.checkBackupMethodNetwork.Size = new System.Drawing.Size(302, 20);
			this.checkBackupMethodNetwork.TabIndex = 10;
			this.checkBackupMethodNetwork.Text = "Network (to another computer in your office)";
			this.checkBackupMethodNetwork.UseVisualStyleBackColor = true;
			// 
			// checkRestoreServer
			// 
			this.checkRestoreServer.Location = new System.Drawing.Point(45, 261);
			this.checkRestoreServer.Name = "checkRestoreServer";
			this.checkRestoreServer.Size = new System.Drawing.Size(250, 20);
			this.checkRestoreServer.TabIndex = 14;
			this.checkRestoreServer.Text = "Run backup from a second server";
			this.checkRestoreServer.UseVisualStyleBackColor = true;
			// 
			// checkRestoreHome
			// 
			this.checkRestoreHome.Location = new System.Drawing.Point(45, 241);
			this.checkRestoreHome.Name = "checkRestoreHome";
			this.checkRestoreHome.Size = new System.Drawing.Size(352, 20);
			this.checkRestoreHome.TabIndex = 13;
			this.checkRestoreHome.Text = "Restore to home computer at least once a week";
			this.checkRestoreHome.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(42, 225);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(523, 18);
			this.label2.TabIndex = 12;
			this.label2.Text = "What proof do you have that your recent backups are good?";
			// 
			// checkNoProof
			// 
			this.checkNoProof.Location = new System.Drawing.Point(45, 281);
			this.checkNoProof.Name = "checkNoProof";
			this.checkNoProof.Size = new System.Drawing.Size(250, 20);
			this.checkNoProof.TabIndex = 15;
			this.checkNoProof.Text = "No proof";
			this.checkNoProof.UseVisualStyleBackColor = true;
			// 
			// checkNoStrategy
			// 
			this.checkNoStrategy.Location = new System.Drawing.Point(45, 369);
			this.checkNoStrategy.Name = "checkNoStrategy";
			this.checkNoStrategy.Size = new System.Drawing.Size(250, 20);
			this.checkNoStrategy.TabIndex = 19;
			this.checkNoStrategy.Text = "No strategy";
			this.checkNoStrategy.UseVisualStyleBackColor = true;
			// 
			// checkSecondaryMethodHardCopy
			// 
			this.checkSecondaryMethodHardCopy.Location = new System.Drawing.Point(45, 349);
			this.checkSecondaryMethodHardCopy.Name = "checkSecondaryMethodHardCopy";
			this.checkSecondaryMethodHardCopy.Size = new System.Drawing.Size(312, 20);
			this.checkSecondaryMethodHardCopy.TabIndex = 18;
			this.checkSecondaryMethodHardCopy.Text = "Saved hardcopy paper reports";
			this.checkSecondaryMethodHardCopy.UseVisualStyleBackColor = true;
			// 
			// checkSecondaryMethodArchive
			// 
			this.checkSecondaryMethodArchive.Location = new System.Drawing.Point(45, 329);
			this.checkSecondaryMethodArchive.Name = "checkSecondaryMethodArchive";
			this.checkSecondaryMethodArchive.Size = new System.Drawing.Size(352, 20);
			this.checkSecondaryMethodArchive.TabIndex = 17;
			this.checkSecondaryMethodArchive.Text = "Completely separate archives stored offsite (DVD, HD, etc)";
			this.checkSecondaryMethodArchive.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(42, 313);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(523, 18);
			this.label4.TabIndex = 16;
			this.label4.Text = "What secondary long-term mechanism do you use to ensure minimal data loss?";
			// 
			// FormBackupReminder
			// 
			this.ClientSize = new System.Drawing.Size(612, 442);
			this.Controls.Add(this.checkNoStrategy);
			this.Controls.Add(this.checkSecondaryMethodHardCopy);
			this.Controls.Add(this.checkSecondaryMethodArchive);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.checkNoProof);
			this.Controls.Add(this.checkRestoreServer);
			this.Controls.Add(this.checkRestoreHome);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkBackupMethodOther);
			this.Controls.Add(this.checkBackupMethodNetwork);
			this.Controls.Add(this.checkBackupMethodRemovable);
			this.Controls.Add(this.checkBackupMethodOnline);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkNoBackups);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Name = "FormBackupReminder";
			this.Text = "Backup Reminder";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBackupReminder_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkNoBackups;
		private System.Windows.Forms.CheckBox checkBackupMethodOnline;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox checkBackupMethodRemovable;
		private System.Windows.Forms.CheckBox checkBackupMethodOther;
		private System.Windows.Forms.CheckBox checkBackupMethodNetwork;
		private System.Windows.Forms.CheckBox checkRestoreServer;
		private System.Windows.Forms.CheckBox checkRestoreHome;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkNoProof;
		private System.Windows.Forms.CheckBox checkNoStrategy;
		private System.Windows.Forms.CheckBox checkSecondaryMethodHardCopy;
		private System.Windows.Forms.CheckBox checkSecondaryMethodArchive;
		private System.Windows.Forms.Label label4;
	}
}