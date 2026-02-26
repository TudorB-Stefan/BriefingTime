using BriefingTime.Api.DTOs;
using BriefingTime.Core.Entities;

namespace BriefingTime.Api.Extensions;

public static class CommentExtensions
{
    public static CommentDetailDto ToDetailDto(this Comment comment)
    {
        return new CommentDetailDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Description = comment.Description,
            CreatedAt = comment.CreatedAt,
            UserName = comment.User.UserName,
            UserFirstName = comment.User.FirstName,
            UserLastName = comment.User.LastName 
        };
    }
}