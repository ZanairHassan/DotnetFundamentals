using ASP.NetFundamentals.Services;

namespace ASP.NetFundamentals.Middlewares
{
    public sealed class StudentMiddleware
    {
        private readonly RequestDelegate _next;

        public StudentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, StudentStore studentStore)
        {
            var students = studentStore.GetStudents();

            context.Items["Students"] = students;

            await _next(context);
        }
    }
}
