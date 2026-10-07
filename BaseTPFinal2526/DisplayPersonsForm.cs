using BaseTPFinal2526.Models;
using BaseTPFinal2526.Models.Enums;
using BaseTPFinal2526.Repositories;

namespace BaseTPFinal2526
{
    public partial class DisplayPersonsForm : Form
    {
        private readonly ListMode _mode;
        private readonly PatientsRepository _patientsRepository = new PatientsRepository();
        private readonly DoctorsRepository _doctorsRepository = new DoctorsRepository();
        public DisplayPersonsForm(ListMode mode)
        {
            _mode = mode;
            InitializeComponent();
        }
        private void DisplayPersonsForm_Load(object sender, EventArgs e)
        {
            ConfigureUI();
            LoadData();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            if (_mode == ListMode.Patient)
            {
                var managePatientsForm = new ManagePatientsForm();
                managePatientsForm.ShowDialog();
                LoadData();
            }
            else
            {
                var manageDoctorsForm = new ManageDoctorsForm();
                manageDoctorsForm.ShowDialog();
                LoadData();
            }
        }

        private void ConfigureUI()
        {
            if (_mode == ListMode.Doctor)
            {
                this.Text = "Doctors List";
                btnAddPerson.Text = "Add Doctor";
                btnEditPerson.Text = "Edit Doctor";
                btnDeletePerson.Text = "Delete Doctor";
            }
            else
            {
                this.Text = "Patients List";
                btnAddPerson.Text = "Add Patient";
                btnEditPerson.Text = "Edit Patient";
                btnDeletePerson.Text = "Delete Patient";
            }
        }

        private void LoadData()
        {
            dgvPersons.Rows.Clear();
            dgvPersons.Columns.Clear();

            dgvPersons.Columns.Add("Id", "Id");
            dgvPersons.Columns.Add("FirstName", "First Name");
            dgvPersons.Columns.Add("LastName", "Last Name");
            dgvPersons.Columns.Add("PhoneNumber", "Phone");
            dgvPersons.Columns.Add("Email", "Email");

            if (_mode == ListMode.Doctor)
            {
                dgvPersons.Columns.Add("Specialty", "Specialty");
                dgvPersons.Columns.Add("INAMINumber", "INAMI");
                dgvPersons.Columns.Add("ConsultationFee", "Fee");

                LoadDoctorsData();
            }
            else
            {
                dgvPersons.Columns.Add("BirthDate", "Birth Date");
                dgvPersons.Columns.Add("MedicalRecordNumber", "Record #");
                dgvPersons.Columns.Add("IsInsured", "Insured");

                LoadPatientsData();
            }
        }

        private void LoadDoctorsData()
        {
            foreach (var doctor in _doctorsRepository.GetAll())
            {
                int index = dgvPersons.Rows.Add(
                    doctor.Id,
                    doctor.FirstName,
                    doctor.LastName,
                    doctor.PhoneNumber,
                    doctor.Email,
                    doctor.Specialty,
                    doctor.INAMINumber,
                    doctor.ConsultationFee
                );
                dgvPersons.Rows[index].Tag = doctor;
            }
        }   

        private void LoadPatientsData()
        {
            foreach (var patient in _patientsRepository.GetAll())
            {
                int index = dgvPersons.Rows.Add(
                    patient.Id,
                    patient.FirstName,
                    patient.LastName,
                    patient.PhoneNumber,
                    patient.Email,
                    patient.BirthDate.ToShortDateString(),
                    patient.MedicalRecordNumber,
                    patient.IsInsured
                );
                dgvPersons.Rows[index].Tag = patient;
            }
        }

        private void btnEditPerson_Click(object sender, EventArgs e)
        {
            if (dgvPersons.CurrentRow == null || dgvPersons.CurrentRow.Tag == null)
            {
                MessageBox.Show("Please select a row first.");
                return;
            }

            if (_mode == ListMode.Patient)
            {
                var patient = (Patient)dgvPersons.CurrentRow.Tag;

                Patient selectedPatient = _patientsRepository.GetById(patient.Id);

                var managePatientsForm = new ManagePatientsForm(selectedPatient);
                managePatientsForm.ShowDialog();
                LoadData();
            }
            else
            {
                var doctor = (Doctor)dgvPersons.CurrentRow.Tag;

                Doctor selectedDoctor = _doctorsRepository.GetById(doctor.Id);

                var manageDoctorsForm = new ManageDoctorsForm(selectedDoctor);
                manageDoctorsForm.ShowDialog();
                LoadData();
            }
        }

        private void btnDeletePerson_Click(object sender, EventArgs e)
        {
            if (dgvPersons.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selectionner une personne à supprimer");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Etes-vous sur de vouloir supprimer cette personne ?",
                "Confirmer",
                MessageBoxButtons.YesNo
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int selectedRowIndex = dgvPersons.SelectedRows[0].Index;

                    if (_mode == ListMode.Patient)
                    {
                        var patients = _patientsRepository.GetAll();
                        if (selectedRowIndex < patients.Count)
                        {
                            _patientsRepository.Delete(patients[selectedRowIndex].Id);
                            MessageBox.Show("Patient supprimer");
                            LoadData();
                        }
                    }
                    else
                    {
                        var doctors = _doctorsRepository.GetAll();
                        if (selectedRowIndex < doctors.Count)
                        {
                            _doctorsRepository.Delete(doctors[selectedRowIndex].Id);
                            MessageBox.Show("Docteur supprimer");
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur de suppression: " + ex.Message);
                }
            }
        }
    }
}
