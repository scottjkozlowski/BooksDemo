using System.Linq;
using System.Web.Mvc;
using Elmah;
using System;
using System.Web;
namespace Odh.BooksDemo.Web.Infrastructure
{
    public static class Extensions
    {
        private static HttpApplication _httpApplication = null;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static ErrorFilterConsole _errorFilter = new ErrorFilterConsole();
        public static ErrorMailModule ErrorEmail = new ErrorMailModule();
        public static ErrorLogModule ErrorLog = new ErrorLogModule();
        public static ErrorTweetModule ErrorTweet = new ErrorTweetModule();

        public static string GetAllErrors(this ModelStateDictionary modelState)
        {
            var errors = string.Join("<br />",
                    modelState.Values.Where(e => e.Errors.Count > 0)
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray());
            return errors;
        }
        public static void LogToElmah(this Exception ex)
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                }
                else
                {
                    if (_httpApplication == null) InitNoContext();
                    ErrorSignal.Get(_httpApplication).Raise(ex);
                }
            }
            catch { }
        }

        private static void InitNoContext()
        {
            _httpApplication = new HttpApplication();
            _errorFilter.Init(_httpApplication);
            (ErrorEmail as IHttpModule).Init(_httpApplication);
            _errorFilter.HookFiltering(ErrorEmail);
            (ErrorLog as IHttpModule).Init(_httpApplication);
            _errorFilter.HookFiltering(ErrorLog);
            (ErrorTweet as IHttpModule).Init(_httpApplication);
            _errorFilter.HookFiltering(ErrorTweet);
        }
        private class ErrorFilterConsole : ErrorFilterModule
        {
            public void HookFiltering(IExceptionFiltering module)
            {
                module.Filtering += new ExceptionFilterEventHandler(base.OnErrorModuleFiltering);
            }
        }
    }
}