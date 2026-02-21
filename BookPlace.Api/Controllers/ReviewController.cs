using BookPlace.Api.DTOs;
using BookPlace.Core.Interfaces.Repositories;
using BookPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookPlace.Api.Controllers;

public class ReviewController(IReviewRepository reviewRepository) : BaseController
{
}