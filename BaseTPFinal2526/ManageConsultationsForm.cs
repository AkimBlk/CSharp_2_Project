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
using BaseTPFinal2526.Validators;

namespace BaseTPFinal2526
{
    public partial class ManageConsultationsForm : Form
    {
        private readonly ConsultationsRepository _consultationsRepository;
        private readonly ConsultationsValidator _consultationsValidator;
        private readonly DoctorsRepository _doctorsRepository;
        private readonly PatientsRepository _patientsRepository;
        private Consultation _consultation;

        public ManageConsultationsForm(Consultation consultation = null)
        {
            InitializeComponent();

            _consultation = consultation ?? new Consultation();
            _consultationsRepository = new ConsultationsRepository();
            _consultationsValidator = new ConsultationsValidator(_consultationsRepository);
            _doctorsRepository = new DoctorsRepository();
            _patientsRepository = new PatientsRepository();

            InitializeComboboxes();

            if (_consultation.Id != 0)
            {
                PopulateFields();
            }
        }

        private void InitializeComboboxes()
        {
            cmbConsultationType.DataSource = Enum.GetValues(typeof(ConsultationType));
            cmbConsultationStatus.DataSource = Enum.GetValues(typeof(ConsultationStatus));

            var doctors = _doctorsRepository.GetAll()
                .Select(d => new
                {
                    Id = d.Id,
                    Display = $"{d.FirstName} {d.LastName} - {d.Specialty}"
                })
                .ToList();
            cmbDoctor.DataSource = doctors;
            cmbDoctor.DisplayMember = "Display";
            cmbDoctor.ValueMember = "Id";

            var patients = _patientsRepository.GetAll()
                .Select(p => new
                {
                    Id = p.Id,
                    Display = $"{p.FirstName} {p.LastName} - {p.MedicalRecordNumber}"
                })
                .ToList();
            cmbPatient.DataSource = patients;
            cmbPatient.DisplayMember = "Display";
            cmbPatient.ValueMember = "Id";
        }

        private void PopulateFields()
        {
            cmbDoctor.SelectedValue = _consultation.Doctor.Id;
            cmbPatient.SelectedValue = _consultation.Patient.Id;
            dtpConsultationDate.Value = _consultation.Date;
            numDuration.Value = _consultation.Duration;
            cmbConsultationType.SelectedItem = _consultation.Type;
            cmbConsultationStatus.SelectedItem = _consultation.Status;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var consultation = _consultation ?? new Consultation();

            if (cmbDoctor.SelectedValue == null || cmbPatient.SelectedValue == null)
            {
                MessageBox.Show("Please select a doctor and a patient");
                return;
            }

            if (consultation?.Status == ConsultationStatus.Cancelled && consultation?.Id != 0)
            {
                MessageBox.Show("Cancelled consultations cannot be modified");
                return;
            }

            consultation.Doctor = _doctorsRepository.GetById(Convert.ToInt32(cmbDoctor.SelectedValue));
            consultation.Patient = _patientsRepository.GetById(Convert.ToInt32(cmbPatient.SelectedValue));
            consultation.Date = dtpConsultationDate.Value;
            consultation.Duration = (int)numDuration.Value;
            consultation.Type = (ConsultationType)cmbConsultationType.SelectedItem;
            consultation.Status = (ConsultationStatus)cmbConsultationStatus.SelectedItem;

            consultation.TotalFee = consultation.Doctor.ConsultationFee * consultation.Duration;

            if (_consultationsValidator.IsValid(consultation))
            {
                if (_consultation?.Id != 0)
                {
                    _consultationsRepository.Update(consultation);
                }
                else
                {
                    _consultationsRepository.Add(consultation);
                }
                Close();
            }
            else
            {
                MessageBox.Show("Invalid consultation informations");
            }
        }
    }
}
