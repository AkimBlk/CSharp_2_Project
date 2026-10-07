using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseTPFinal2526.Models;
using BaseTPFinal2526.Models.Enums;
using BaseTPFinal2526.Repositories;

namespace BaseTPFinal2526.Validators
{
    public class ConsultationsValidator
    {
        private readonly ConsultationsRepository _consultationsRepository;

        public ConsultationsValidator(ConsultationsRepository consultationsRepository)
        {
            _consultationsRepository = consultationsRepository;
        }

        public bool IsValid(Consultation consultation)
        {
            return IsDoctorValid(consultation.Doctor)
                && IsPatientValid(consultation.Patient)
                && IsDateValid(consultation.Date)
                && IsDurationValid(consultation.Duration)
                && IsTypeValid(consultation.Type)
                && IsStatusValid(consultation.Status)
                && !HasTimeConflict(consultation);
        }

        private bool IsDoctorValid(Doctor doctor)
        {
            if (doctor == null)
                return false;

            if (doctor.Id <= 0)
                return false;

            return true;
        }

        private bool IsPatientValid(Patient patient)
        {
            if (patient == null)
                return false;

            if (patient.Id <= 0)
                return false;

            return true;
        }

        private bool IsDateValid(DateTime date)
        {
            if (date <= DateTime.Now)
                return false;

            return true;
        }

        private bool IsDurationValid(int duration)
        {
            if (duration <= 0)
                return false;

            return true;
        }

        private bool IsTypeValid(ConsultationType type)
        {
            return type == ConsultationType.General 
                || type == ConsultationType.FollowUp 
                || type == ConsultationType.Emergency;
        }

        private bool IsStatusValid(ConsultationStatus status)
        {
            return status == ConsultationStatus.Scheduled 
                || status == ConsultationStatus.Completed 
                || status == ConsultationStatus.Cancelled;
        }

        private bool HasTimeConflict(Consultation consultation)
        {
            if (consultation.Doctor == null)
                return false;

            return _consultationsRepository.HasTimeConflict(
                consultation.Doctor.Id,
                consultation.Date,
                consultation.Duration,
                consultation.Id > 0 ? consultation.Id : null
            );
        }
    }
}

