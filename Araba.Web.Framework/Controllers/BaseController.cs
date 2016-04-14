using Araba.Core.Application;
using Araba.Data.UnitOfWork;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Best.Web.Framework.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _uow;

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    if (filterContext.ExceptionHandled)
        //        return;

        //    //Let the request know what went wrong
        //    Error(filterContext.Exception.Message);

        //    //redirect to error handler
        //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
        //        new { controller = "Common", action = "HandleError", area = "" }));

        //    // Stop any other exception handlers from running
        //    filterContext.ExceptionHandled = true;

        //    // CLear out anything already in the response
        //    filterContext.HttpContext.Response.Clear();

        //    base.OnException(filterContext);
        //}

        #region messages for view
        public void Default(string message)
        {
            AddMessage(message, MessageTypes.Default);
        }

        public void Info(string message)
        {
            AddMessage(message, MessageTypes.Info);
        }

        public void Success(string message)
        {
            AddMessage(message, MessageTypes.Success);
        }

        public void Warning(string message)
        {
            AddMessage(message, MessageTypes.Warning);
        }

        public void Error(string message)
        {
            AddMessage(message, MessageTypes.Danger);
        }
        #endregion

        #region private methods
        private void AddMessage(string message, MessageTypes type)
        {
            var messages = new List<MessageForView>();

            if (TempData[AppConstants.TempDataKey_MessageForView] != null)
                messages = (List<MessageForView>)TempData[AppConstants.TempDataKey_MessageForView];

            messages.Add(new MessageForView { MessageType = type, Message = message });

            TempData[AppConstants.TempDataKey_MessageForView] = messages;
        }
        #endregion
    }
}
