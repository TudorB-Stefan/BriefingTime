using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;

namespace BookPlace.Api.Extensions;

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