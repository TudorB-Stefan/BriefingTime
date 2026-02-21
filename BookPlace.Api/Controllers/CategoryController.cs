using BookPlace.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookPlace.Api.Controllers;

public class CategoryController(ICategoryRepository categoryRepository) : BaseController
{
}