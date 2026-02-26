namespace BriefingTime.Core.Entities;

public class Department
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<Briefing> Briefings { get; set; }
}