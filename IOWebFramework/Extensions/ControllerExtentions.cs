using IOWebFramework.Controllers;
using IOWebFramework.Core.Constants;

namespace IOWebFramework.Extensions
{
    public static class ControllerExtentions
    {
        /// <summary>
        /// Set TempData necessary for displaying notifications
        /// </summary>
        /// <param name="result">Effect on UI side: result = true -> green messsage OK, result = false -> red message fail</param>
        public static void ShowNotificationMessageOnUI<T>(this T controller, bool result) where T : BaseController
        {
            if (result)
            {
                controller.TempData[MessageConstant.SuccessMessage] = MessageConstant.Values.SaveOK;
            }
            else
            {
                controller.TempData[MessageConstant.ErrorMessage] = MessageConstant.Values.SaveFailed;
            }
        }
        public static void ShowWarningNotificationMessageOnUI<T>(this T controller, string message) where T : BaseController
        {
            controller.TempData[MessageConstant.ErrorMessage] = message;
        }
    }
}
