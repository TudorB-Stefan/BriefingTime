namespace BriefingTime.Api.DTOs;

public class DepartmentDetailDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<BriefingListDto> Briefings { get; set; }
}