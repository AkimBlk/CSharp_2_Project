using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseTPFinal2526.Models;
using BaseTPFinal2526.Models.Enums;
using System.Data.SQLite;

namespace BaseTPFinal2526.Repositories
{
    public class ConsultationsRepository
    {
        private string _dbPath = "Data Source=medicalCenter.db;Version=3;";
        private DoctorsRepository _doctorsRepository = new DoctorsRepository();
        private PatientsRepository _patientsRepository = new PatientsRepository();

        public List<Consultation> GetAll()
        {
            var consultations = new List<Consultation>();

            try
            {
                using (var connection = new SQLiteConnection(_dbPath))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Consultation";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var consultation = new Consultation
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Doctor = _doctorsRepository.GetById(Convert.ToInt32(reader["DoctorId"])),
                                Patient = _patientsRepository.GetById(Convert.ToInt32(reader["PatientId"])),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Duration = Convert.ToInt32(reader["Duration"]),
                                Type = (ConsultationType)Enum.Parse(typeof(ConsultationType), reader["Type"].ToString()),
                                Status = (ConsultationStatus)Enum.Parse(typeof(ConsultationStatus), reader["Status"].ToString()),
                                TotalFee = Convert.ToDecimal(reader["TotalFee"])
                            };

                            if (consultation.Doctor != null && consultation.Patient != null)
                            {
                                consultations.Add(consultation);
                            }
                        }
                        return consultations;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Consultations from database.", ex);
            }
        }

        public Consultation GetById(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(_dbPath))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Consultation WHERE Id = @Id";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                return null;

                            return new Consultation
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Doctor = _doctorsRepository.GetById(Convert.ToInt32(reader["DoctorId"])),
                                Patient = _patientsRepository.GetById(Convert.ToInt32(reader["PatientId"])),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Duration = Convert.ToInt32(reader["Duration"]),
                                Type = (ConsultationType)Enum.Parse(typeof(ConsultationType), reader["Type"].ToString()),
                                Status = (ConsultationStatus)Enum.Parse(typeof(ConsultationStatus), reader["Status"].ToString()),
                                TotalFee = Convert.ToDecimal(reader["TotalFee"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Consultation from database.", ex);
            }
        }

        public void Add(Consultation consultation)
        {
            try
            {
                using (var connection = new SQLiteConnection(_dbPath))
                {
                    connection.Open();

                    string sql = @"
                    INSERT INTO Consultation (DoctorId, PatientId, Date, Duration, Type, Status, TotalFee)
                    VALUES (@DoctorId, @PatientId, @Date, @Duration, @Type, @Status, @TotalFee);";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@DoctorId", consultation.Doctor.Id);
                        cmd.Parameters.AddWithValue("@PatientId", consultation.Patient.Id);
                        cmd.Parameters.AddWithValue("@Date", consultation.Date);
                        cmd.Parameters.AddWithValue("@Duration", consultation.Duration);
                        cmd.Parameters.AddWithValue("@Type", consultation.Type.ToString());
                        cmd.Parameters.AddWithValue("@Status", consultation.Status.ToString());
                        cmd.Parameters.AddWithValue("@TotalFee", consultation.TotalFee);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding Consultation to database.", ex);
            }
        }

        public void Update(Consultation consultation)
        {
            try
            {
                using (var connection = new SQLiteConnection(_dbPath))
                {
                    connection.Open();

                    string sql = @"
                    UPDATE Consultation
                    SET DoctorId = @DoctorId,
                        PatientId = @PatientId,
                        Date = @Date,
                        Duration = @Duration,
                        Type = @Type,
                        Status = @Status,
                        TotalFee = @TotalFee
                    WHERE Id = @Id;";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", consultation.Id);
                        cmd.Parameters.AddWithValue("@DoctorId", consultation.Doctor.Id);
                        cmd.Parameters.AddWithValue("@PatientId", consultation.Patient.Id);
                        cmd.Parameters.AddWithValue("@Date", consultation.Date);
                        cmd.Parameters.AddWithValue("@Duration", consultation.Duration);
                        cmd.Parameters.AddWithValue("@Type", consultation.Type.ToString());
                        cmd.Parameters.AddWithValue("@Status", consultation.Status.ToString());
                        cmd.Parameters.AddWithValue("@TotalFee", consultation.TotalFee);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating Consultation in database.", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(_dbPath))
                {
                    connection.Open();

                    string sql = "DELETE FROM Consultation WHERE Id = @Id;";

                    using (var cmd = new SQLiteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting Consultation from database.", ex);
            }
        }

        // Vérifier chevauchement horaire pour un médecin
        public bool HasTimeConflict(int doctorId, DateTime date, int duration, int? excludeConsultationId = null)
        {
            try
            {
                // Récupérer toutes les consultations du médecin (non annulées)
                var consultations = GetAll()
                    .Where(c => c.Doctor != null
                             && c.Doctor.Id == doctorId
                             && c.Status != ConsultationStatus.Cancelled
                             && c.Id != (excludeConsultationId ?? 0))
                    .ToList();

                // Calculer l'heure de fin de la nouvelle consultation
                DateTime newEndTime = date.AddMinutes(duration);

                // Vérifier les chevauchements
                foreach (var consultation in consultations)
                {
                    DateTime existingEndTime = consultation.Date.AddMinutes(consultation.Duration);

                    // Chevauchement si :
                    // - La nouvelle commence avant que l'existante se termine
                    // - ET la nouvelle termine après que l'existante commence
                    if (date < existingEndTime && newEndTime > consultation.Date)
                    {
                        return true; // Conflit trouvé !
                    }
                }

                return false; // Pas de conflit
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking time conflict: " + ex.Message);
                return false;
            }
        }

    }
}

