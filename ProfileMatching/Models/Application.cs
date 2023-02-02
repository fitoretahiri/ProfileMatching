using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileMatching.Models
{
    public class Application
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int JobPositionId { get; set; }
        public JobPosition JobPosition { get; set; }
        public AppUser Applicant { get; set; }

        [ForeignKey("ApplicantId")]
        public string ApplicantId { get; set; }
    }
}