using BookPlace.Api.DTOs;
using BookPlace.Core.Entities;

namespace BookPlace.Api.Extensions;

public static class ReviewExtensions
{
    public static ReviewDetailDto ToDetailDto(this Review review)
    {
        return new ReviewDetailDto
        {
            Id = review.Id,
            Grade = review.Grade,
            Title = review.Title,
            Description = review.Description,
            CreatedAt = review.CreatedAt,
            UserName = review.User.UserName,
            UserFirstName = review.User.FirstName,
            UserLastName = review.User.LastName 
        };
    }
}