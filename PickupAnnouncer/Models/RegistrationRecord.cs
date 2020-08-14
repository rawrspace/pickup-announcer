using CsvHelper.Configuration.Attributes;

namespace PickupAnnouncer.Models
{
    public class RegistrationRecord
    {
        [Name("Registration Number")]
        public int RegistrationNumber { get; set; }
        [Name("Student First Name")]
        public string StudentFirstName { get; set; }
        [Name("Student Last Name")]
        public string StudentLastName { get; set; }
        [Name("Teacher Name")]
        public string TeacherName { get; set; }
        [Name("Grade Level")]
        public int GradeLevel { get; set; }
    }
}
