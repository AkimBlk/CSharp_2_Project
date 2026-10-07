using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BaseTPFinal2526.Models;
using BaseTPFinal2526.Models.Enums;
using BaseTPFinal2526.Repositories;


namespace BaseTPFinal2526
{
    public partial class DisplayConsultationsForm : Form
    {
        private readonly ConsultationsRepository _consultationsRepository = new ConsultationsRepository();

        public DisplayConsultationsForm()
        {
            InitializeComponent();
        }

        private void DisplayConsultationsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dgvConsultations.Rows.Clear();
            dgvConsultations.Columns.Clear();

            dgvConsultations.Columns.Add("Id", "Id");
            dgvConsultations.Columns.Add("Doctor", "Doctor");
            dgvConsultations.Columns.Add("Patient", "Patient");
            dgvConsultations.Columns.Add("Date", "Date");
            dgvConsultations.Columns.Add("Duration", "Duration (min)");
            dgvConsultations.Columns.Add("Type", "Type");
            dgvConsultations.Columns.Add("Status", "Status");
            dgvConsultations.Columns.Add("TotalFee", "Total Fee");

            try
            {
                foreach (var consultation in _consultationsRepository.GetAll())
                {
                    // Vérifier que Doctor et Patient ne sont pas null
                    if (consultation.Doctor == null || consultation.Patient == null)
                    {
                        continue; // Skip cette consultation
                    }

                    int index = dgvConsultations.Rows.Add(
                        consultation.Id,
                        $"{consultation.Doctor.FirstName} {consultation.Doctor.LastName}",
                        $"{consultation.Patient.FirstName} {consultation.Patient.LastName}",
                        consultation.Date.ToString("dd/MM/yyyy HH:mm"),
                        consultation.Duration,
                        consultation.Type,
                        consultation.Status,
                        consultation.TotalFee.ToString("F2")
                    );
                    dgvConsultations.Rows[index].Tag = consultation;
                }
            }
            catch
            {
                MessageBox.Show("There are no doctor or patient");
            }
        }


        private void btnAddConsultation_Click(object sender, EventArgs e)
        {
            var manageConsultationsForm = new ManageConsultationsForm();
            manageConsultationsForm.ShowDialog();
            LoadData();
        }

        private void btnEditConsultation_Click(object sender, EventArgs e)
        {
            if (dgvConsultations.CurrentRow == null || dgvConsultations.CurrentRow.Tag == null)
            {
                MessageBox.Show("Please select a row first.");
                return;
            }

            var consultation = (Consultation)dgvConsultations.CurrentRow.Tag;

            if (consultation.Status == ConsultationStatus.Cancelled)
            {
                MessageBox.Show("Cancelled consultations cannot be edited");
                return;
            }

            Consultation selectedConsultation = _consultationsRepository.GetById(consultation.Id);

            var manageConsultationsForm = new ManageConsultationsForm(selectedConsultation);
            manageConsultationsForm.ShowDialog();
            LoadData();
        }

        private void btnDeleteConsultation_Click(object sender, EventArgs e)
        {
            if (dgvConsultations.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a consultation to delete");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this consultation?",
                "Confirm Delete",
                MessageBoxButtons.YesNo
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int selectedRowIndex = dgvConsultations.SelectedRows[0].Index;
                    var consultations = _consultationsRepository.GetAll();

                    if (selectedRowIndex < consultations.Count)
                    {
                        _consultationsRepository.Delete(consultations[selectedRowIndex].Id);
                        MessageBox.Show("Consultation deleted successfully");
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting consultation: " + ex.Message);
                }
            }
        }
    }
}
