using System;
using System.Data.Common;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace TeleWeb.Presentation.Controllers
{
    public class ControllerBase : Controller
    {
        protected ActionResult ExceptionResult(Exception exception)
        {
            if (exception is DbException dbException)
            {
                return BadRequest($"DB error: {dbException.Message}" + Environment.NewLine +
                                  $"{dbException.InnerException?.Message}");
            }

            if (exception is ArgumentException apiException)
            {
                return BadRequest(apiException.Message);
            }

            return BadRequest($"Something went wrong: {exception.Message}" + Environment.NewLine +
                              $"{exception.InnerException?.Message}");
        }
    }
}