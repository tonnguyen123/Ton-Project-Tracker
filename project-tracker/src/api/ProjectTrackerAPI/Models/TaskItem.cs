using System.ComponentModel.DataAnnotations;  // Add this line

namespace ProjectTrackerAPI.Models
{
    public class TaskItem
    {
        [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "pending";

    public int ProjectId { get; set; }
    
    [Required]  // Mark Project as required
    public Project Project { get; set; }

    // Constructor to initialize non-nullable properties
    public TaskItem()
    {
        Project = new Project();  // Initialize Project if needed
    }
    }
}
