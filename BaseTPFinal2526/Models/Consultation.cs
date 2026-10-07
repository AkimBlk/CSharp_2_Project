using BaseTPFinal2526.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BaseTPFinal2526.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; } //minutes
        public ConsultationType Type { get; set; }
        public ConsultationStatus Status { get; set; }
        public decimal TotalFee { get; set; }
    }
}
