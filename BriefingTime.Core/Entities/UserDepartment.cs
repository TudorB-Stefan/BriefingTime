namespace BriefingTime.Core.Entities;

public class UserDepartment
{
    public string UserId { get; set; }
    public User User { get; set; }
    public string DepartmentId { get; set; }
    public Department Department { get; set; }
}