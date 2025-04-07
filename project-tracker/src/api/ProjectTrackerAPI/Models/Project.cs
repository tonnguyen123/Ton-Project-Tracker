using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTrackerAPI.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }


        public List<User> SharedUsers { get; set; } = new List<User>();
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
