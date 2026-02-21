using BookPlace.Api.DTOs;
using BookPlace.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookPlace.Api.Controllers;

public class DownloadLogController(IDownloadLogRepository downloadLogRepository) : BaseController
{
}