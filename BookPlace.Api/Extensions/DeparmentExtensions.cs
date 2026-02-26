using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;

namespace BookPlace.Api.Extensions;

public static class DeparmentExtensions
{
    public static DepartmentDetailDto ToDetailDto(this Department department)
    {
        return new DepartmentDetailDto
        {
            Id = department.Id,
            Name = department.Name,
            Briefings = department.Briefings != null
                ? department.Briefings.Select(b => b.ToListDto()).ToList()
                : new List<BriefingListDto>()
        };
    }

    public static DepartmentListDto ToListDto(this Department department)
    {
        return new DepartmentListDto
        {
            Id = department.Id,
            Name = department.Name,
            Count = department.Briefings?.Count() ?? 0
        };
    }
}