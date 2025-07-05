using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace LocalisationSheet.Server.Utils
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result) => result.IsSuccess
           ? new OkObjectResult(new { success = true, data = result.Value })
           : result.Errors.ToErrorResponse();

        public static IActionResult ToActionResult(this Result result) => result.IsSuccess
                   ? new NoContentResult()
                   : result.Errors.ToErrorResponse();

        private static IActionResult ToErrorResponse(this IEnumerable<IError> errors)
        {
            var first = errors.First();
            var httpCode = first.Metadata.TryGetValue("HttpCode", out var codeObj)
                            ? (int)codeObj
                            : StatusCodes.Status400BadRequest;

            return new ObjectResult(new
            {
                success = false,
                errors = errors.Select(e => new
                {
                    code = e.Metadata.TryGetValue("Code", out var v) ? v : "Error",
                    description = e.Message
                })
            })
            { StatusCode = httpCode };
        }
    }
}