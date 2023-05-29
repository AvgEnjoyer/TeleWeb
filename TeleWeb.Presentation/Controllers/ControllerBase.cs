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
            var response = new ApiResponse()
            {
                Success = false
            };
            if (exception is DbException dbException)
            {
                response.Message = $"DB error: {dbException.Message}" + Environment.NewLine +
                                        $"{dbException.InnerException?.Message}";
                return BadRequest(response);
            }

            if (exception is ArgumentException apiException)
            {
                response.Message = apiException.Message;
                return BadRequest(response);
            }

            response.Message = $"Something went wrong: {exception.Message}" + Environment.NewLine +
                                    $"{exception.InnerException?.Message}";
            return BadRequest(response);
        }
    }
}