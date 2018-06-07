using System;

namespace PrimeDating.BusinessLayer
{
    public static class Extension
    {
        public static string GetErrorMessage(this Exception ex)
        {
            var message = ex.Message;

            if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message))
            {
                message += $". {GetErrorMessage(ex.InnerException)}";
            }

            return message;
        }
    }
}
