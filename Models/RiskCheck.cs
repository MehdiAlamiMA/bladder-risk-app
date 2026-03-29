using System;
using System.ComponentModel.DataAnnotations;

namespace BladderRiskApp.Models
{
    public class RiskCheck
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TotalScore { get; set; }

        public string RiskLevel { get; set; }

        // Personal Risk data
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        public bool HasFamilyHistory { get; set; }

        public int PersonalScore { get; set; }

        // Smoking Risk data
        public int SmokingScore { get; set; }

        [Required(ErrorMessage = "Smoking status is required")]
        public string SmokingStatus { get; set; }

        public bool ExposedToSecondhandSmoke { get; set; }

        public int YearsSmoked { get; set; }
        // Number of years the user has smoked

        public int CigarettesPerDay { get; set; }
        // Average number of cigarettes per day

        public int YearsSinceQuit { get; set; }
        // Number of years since quitting smoking

        public string SmokingType { get; set; }
        // Type of smoking: Cigarettes, Cigar, Pipe, Other


        public int OccupationalScore { get; set; }

        public int MedicalScore { get; set; }

    }
}