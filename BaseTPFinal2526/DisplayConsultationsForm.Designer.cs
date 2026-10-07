namespace BaseTPFinal2526
{
    partial class DisplayConsultationsForm
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
            dgvConsultations = new DataGridView();
            btnAddConsultation = new Button();
            btnEditConsultation = new Button();
            btnDeleteConsultation = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvConsultations).BeginInit();
            SuspendLayout();
            // 
            // dgvConsultations
            // 
            dgvConsultations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvConsultations.Location = new Point(12, 12);
            dgvConsultations.Name = "dgvConsultations";
            dgvConsultations.Size = new Size(1069, 448);
            dgvConsultations.TabIndex = 0;
            // 
            // btnAddConsultation
            // 
            btnAddConsultation.Location = new Point(12, 465);
            btnAddConsultation.Margin = new Padding(2);
            btnAddConsultation.Name = "btnAddConsultation";
            btnAddConsultation.Size = new Size(344, 91);
            btnAddConsultation.TabIndex = 4;
            btnAddConsultation.Text = "Add Consultation";
            btnAddConsultation.UseVisualStyleBackColor = true;
            btnAddConsultation.Click += btnAddConsultation_Click;
            // 
            // btnEditConsultation
            // 
            btnEditConsultation.Location = new Point(370, 465);
            btnEditConsultation.Margin = new Padding(2);
            btnEditConsultation.Name = "btnEditConsultation";
            btnEditConsultation.Size = new Size(354, 91);
            btnEditConsultation.TabIndex = 5;
            btnEditConsultation.Text = "Edit Consultation";
            btnEditConsultation.UseVisualStyleBackColor = true;
            btnEditConsultation.Click += btnEditConsultation_Click;
            // 
            // btnDeleteConsultation
            // 
            btnDeleteConsultation.Location = new Point(739, 465);
            btnDeleteConsultation.Margin = new Padding(2);
            btnDeleteConsultation.Name = "btnDeleteConsultation";
            btnDeleteConsultation.Size = new Size(344, 91);
            btnDeleteConsultation.TabIndex = 6;
            btnDeleteConsultation.Text = "Delete Consultation";
            btnDeleteConsultation.UseVisualStyleBackColor = true;
            btnDeleteConsultation.Click += btnDeleteConsultation_Click;
            // 
            // DisplayConsultationsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1093, 559);
            Controls.Add(btnDeleteConsultation);
            Controls.Add(btnEditConsultation);
            Controls.Add(btnAddConsultation);
            Controls.Add(dgvConsultations);
            Margin = new Padding(2);
            Name = "DisplayConsultationsForm";
            Text = "Consultations";
            Load += DisplayConsultationsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvConsultations).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvConsultations;
        private Button btnAddConsultation;
        private Button btnEditConsultation;
        private Button btnDeleteConsultation;
    }
}