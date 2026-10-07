namespace BaseTPFinal2526
{
    partial class ManageConsultationsForm
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
            label1 = new Label();
            cmbDoctor = new ComboBox();
            label2 = new Label();
            cmbPatient = new ComboBox();
            label3 = new Label();
            dtpConsultationDate = new DateTimePicker();
            label4 = new Label();
            numDuration = new NumericUpDown();
            label5 = new Label();
            cmbConsultationType = new ComboBox();
            label6 = new Label();
            cmbConsultationStatus = new ComboBox();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 25);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 0;
            label1.Text = "Doctor";
            // 
            // cmbDoctor
            // 
            cmbDoctor.FormattingEnabled = true;
            cmbDoctor.Location = new Point(49, 43);
            cmbDoctor.Name = "cmbDoctor";
            cmbDoctor.Size = new Size(277, 23);
            cmbDoctor.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(49, 77);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 2;
            label2.Text = "Patient";
            // 
            // cmbPatient
            // 
            cmbPatient.FormattingEnabled = true;
            cmbPatient.Location = new Point(49, 95);
            cmbPatient.Name = "cmbPatient";
            cmbPatient.Size = new Size(277, 23);
            cmbPatient.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(49, 132);
            label3.Name = "label3";
            label3.Size = new Size(84, 15);
            label3.TabIndex = 4;
            label3.Text = "Date and Time";
            // 
            // dtpConsultationDate
            // 
            dtpConsultationDate.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpConsultationDate.Format = DateTimePickerFormat.Custom;
            dtpConsultationDate.Location = new Point(49, 150);
            dtpConsultationDate.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dtpConsultationDate.Name = "dtpConsultationDate";
            dtpConsultationDate.Size = new Size(277, 23);
            dtpConsultationDate.TabIndex = 5;
            dtpConsultationDate.Value = new DateTime(2026, 1, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(362, 25);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 6;
            label4.Text = "Duration";
            // 
            // numDuration
            // 
            numDuration.Location = new Point(363, 43);
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(131, 23);
            numDuration.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(362, 77);
            label5.Name = "label5";
            label5.Size = new Size(32, 15);
            label5.TabIndex = 8;
            label5.Text = "Type";
            // 
            // cmbConsultationType
            // 
            cmbConsultationType.FormattingEnabled = true;
            cmbConsultationType.Location = new Point(362, 95);
            cmbConsultationType.Name = "cmbConsultationType";
            cmbConsultationType.Size = new Size(132, 23);
            cmbConsultationType.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(362, 132);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 10;
            label6.Text = "Status";
            // 
            // cmbConsultationStatus
            // 
            cmbConsultationStatus.FormattingEnabled = true;
            cmbConsultationStatus.Location = new Point(362, 150);
            cmbConsultationStatus.Name = "cmbConsultationStatus";
            cmbConsultationStatus.Size = new Size(132, 23);
            cmbConsultationStatus.TabIndex = 11;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(12, 220);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(535, 39);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // ManageConsultationsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(559, 271);
            Controls.Add(btnSave);
            Controls.Add(cmbConsultationStatus);
            Controls.Add(label6);
            Controls.Add(cmbConsultationType);
            Controls.Add(label5);
            Controls.Add(numDuration);
            Controls.Add(label4);
            Controls.Add(dtpConsultationDate);
            Controls.Add(label3);
            Controls.Add(cmbPatient);
            Controls.Add(label2);
            Controls.Add(cmbDoctor);
            Controls.Add(label1);
            Name = "ManageConsultationsForm";
            Text = "ManageConsultationsForm";
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbDoctor;
        private Label label2;
        private ComboBox cmbPatient;
        private Label label3;
        private DateTimePicker dtpConsultationDate;
        private Label label4;
        private NumericUpDown numDuration;
        private Label label5;
        private ComboBox cmbConsultationType;
        private Label label6;
        private ComboBox cmbConsultationStatus;
        private Button btnSave;
    }
}