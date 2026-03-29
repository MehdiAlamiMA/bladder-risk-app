using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BladderRiskApp.Data;
using BladderRiskApp.Models;
using System.Security.Claims; // access user ID from authentication
using System.ComponentModel.DataAnnotations;

namespace BladderRiskApp.Pages
{
    public class CheckModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public CheckModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [BindProperty]
        public bool WorksWithChemicals { get; set; }


        // Personal Risk fields
        [BindProperty]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [BindProperty]
        public bool HasFamilyHistory { get; set; }

        // Smoking Risk fields
        [BindProperty]
        public bool IsSmoker { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Smoking status is required")]
        public int SmokingStatus { get; set; }

        [BindProperty]
        public bool ExposedToSecondhandSmoke { get; set; }

        // Smoking Risk detailed fields

        [BindProperty]
        public int YearsSmoked { get; set; }
        // Number of years the user has smoked

        [BindProperty]
        public int CigarettesPerDay { get; set; }
        // Average number of cigarettes smoked per day

        [BindProperty]
        public int YearsSinceQuit { get; set; }
        // Number of years since the user stopped smoking

        [BindProperty]
        public string SmokingType { get; set; }
        // Type of smoking: Cigarettes, Pipe, Cigar, Other

        // Occupational risk fields
        [BindProperty]
        public bool HasChronicBladderInflammation { get; set; }

        [BindProperty]
        public bool HadPreviousCancer { get; set; }

        // Radiation treatment in pelvis area

        [BindProperty]
        public bool HadPelvicRadiation { get; set; }
        // Indicates if the user had radiation therapy in the pelvic region

        [BindProperty]
        public int YearsSinceRadiation { get; set; }
        // Number of years since radiation treatment


        // Cyclophosphamide chemotherapy

        [BindProperty]
        public bool HadCyclophosphamide { get; set; }
        // Indicates if the user received Cyclophosphamide chemotherapy

        [BindProperty]
        public bool IsLongTermTherapy { get; set; }
        // Indicates if therapy was long-term

        [BindProperty]
        public int TherapyDurationYears { get; set; }
        // Duration of therapy in years


        // Section scores
        public int Score { get; set; }
        public int PersonalScore { get; set; }
        public int SmokingScore { get; set; }
        public int OccupationalScore { get; set; }
        public int MedicalScore { get; set; }

        // Determine risk level based on score
        private string CalculateRiskLevel(int score)
        {
            if (score >= 7)
                return "High";

            if (score >= 4)
                return "Medium";

            return "Low";
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Reset section scores
            PersonalScore = 0;
            SmokingScore = 0;
            OccupationalScore = 0;
            MedicalScore = 0;

            // Section Personal Risk

            if (Age == 2)
                PersonalScore += 2;

            if (Age == 3)
                PersonalScore += 4;

            // Gender risk
            if (Gender == "Male")
                PersonalScore += 1;

            // Family history risk
            if (HasFamilyHistory)
                PersonalScore += 3;

            // Section Medical Risk

            if (HasChronicBladderInflammation)
                MedicalScore += 3;

            if (HadPreviousCancer)
                MedicalScore += 5;


            /* Section Smoking Risk */

            // Smoking status
            if (SmokingStatus == 2) // Former smoker
                SmokingScore += 3;

            if (SmokingStatus == 3) // Current smoker
                SmokingScore += 5;

            // Secondhand smoke exposure
            if (ExposedToSecondhandSmoke)
                SmokingScore += 2;

            // Smoking status
            if (SmokingStatus == 2) // Former smoker
                SmokingScore += 3;

            if (SmokingStatus == 3) // Current smoker
                SmokingScore += 5;

            // Years smoked (long exposure increases risk)
            if (YearsSmoked >= 10)
                SmokingScore += 2;

            if (YearsSmoked >= 20)
                SmokingScore += 3;

            // Cigarettes per day (intensity factor)
            if (CigarettesPerDay >= 10)
                SmokingScore += 2;

            if (CigarettesPerDay >= 20)
                SmokingScore += 3;

            // Years since quit (risk decreases over time)
            if (SmokingStatus == 2) // Former smoker
            {
                if (YearsSinceQuit >= 10)
                    SmokingScore -= 2;

                if (YearsSinceQuit >= 20)
                    SmokingScore -= 3;
            }

            // Smoking type (optional additional risk)
            if (SmokingType == "Cigar" || SmokingType == "Pipe")
                SmokingScore += 1;

            // Secondhand smoke exposure
            if (ExposedToSecondhandSmoke)
                SmokingScore += 2;


            // Section Occupational Risk

            if (WorksWithChemicals)
                OccupationalScore += 3;


            // Calculate total score

            Score =
                PersonalScore +
                SmokingScore +
                OccupationalScore +
                MedicalScore;
        }

        public IActionResult OnPostSave()
        {
            // Stop saving if validation fails
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // Recalculate scores using same logic
            OnPost();
            int score = Score;

            // Get the unique user ID from the logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Get today's date
            var today = DateTime.Today;

            // Find today's check
            var existingCheck = _context.RiskChecks
                .FirstOrDefault(r =>
                    r.UserId == userId &&
                    r.CreatedAt >= today &&
                    r.CreatedAt < today.AddDays(1));

            if (existingCheck != null)
            {
                // Update existing record
                existingCheck.TotalScore = score;
                existingCheck.RiskLevel = CalculateRiskLevel(score);
                existingCheck.OccupationalScore = WorksWithChemicals ? 3 : 0;

                // Update personal Risk data
                existingCheck.PersonalScore =
                    (Age == 2 ? 2 : 0) +
                    (Age == 3 ? 4 : 0) +
                    (Gender == "Male" ? 1 : 0) +
                    (HasFamilyHistory ? 3 : 0);
                existingCheck.Gender = Gender;
                existingCheck.HasFamilyHistory = HasFamilyHistory;

                // Update smoking data
                existingCheck.SmokingScore =
                    (SmokingStatus == 2 ? 3 : 0) +
                    (SmokingStatus == 3 ? 5 : 0) +
                    (ExposedToSecondhandSmoke ? 2 : 0);
                existingCheck.SmokingStatus = SmokingStatus.ToString();
                existingCheck.ExposedToSecondhandSmoke = ExposedToSecondhandSmoke;
                existingCheck.YearsSmoked = YearsSmoked;
                existingCheck.CigarettesPerDay = CigarettesPerDay;
                existingCheck.YearsSinceQuit = YearsSinceQuit;
                existingCheck.SmokingType = SmokingType;

                // Update medical score
                existingCheck.MedicalScore =
                    (HasChronicBladderInflammation ? 3 : 0) +
                    (HadPreviousCancer ? 5 : 0);

                existingCheck.CreatedAt = DateTime.Now;

                _context.SaveChanges();
            }
            else
            {
                // Create new record
                var riskCheck = new RiskCheck
                {
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                    TotalScore = score,
                    RiskLevel = CalculateRiskLevel(score),

                    // Set personal risk score
                    PersonalScore =
                        (Age == 2 ? 2 : 0) +
                        (Age == 3 ? 4 : 0) +
                        (Gender == "Male" ? 1 : 0) +
                        (HasFamilyHistory ? 3 : 0),
                    Gender = Gender,
                    HasFamilyHistory = HasFamilyHistory,

                    // Set smoking data
                    SmokingStatus = SmokingStatus.ToString(),
                    ExposedToSecondhandSmoke = ExposedToSecondhandSmoke,
                    YearsSmoked = YearsSmoked,
                    CigarettesPerDay = CigarettesPerDay,
                    YearsSinceQuit = YearsSinceQuit,
                    SmokingType = SmokingType,
                    SmokingScore =
                        (SmokingStatus == 2 ? 3 : 0) +
                        (SmokingStatus == 3 ? 5 : 0) +
                        (ExposedToSecondhandSmoke ? 2 : 0),

                    // Set medical score
                    MedicalScore =
                        (HasChronicBladderInflammation ? 3 : 0) +
                        (HadPreviousCancer ? 5 : 0),

                    OccupationalScore = WorksWithChemicals ? 3 : 0
                };

                _context.RiskChecks.Add(riskCheck);
                _context.SaveChanges();
            }

            TempData["SuccessMessage"] = "Risk check saved successfully.";

            return Page();
        }
    }
}
