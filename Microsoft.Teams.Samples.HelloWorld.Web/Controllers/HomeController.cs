using Microsoft.Teams.Samples.HelloWorld.Web.Helper;
using ProactiveMessageTest.Helper;
using System.Web.Mvc;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{
    public class HomeController : Controller
    {

        [Route("postmessage")]
        public void PostMessage(string id = null)
        {
             ReminderHelper.PostMessageToChannel(TempStorage.ServiceUrl, TempStorage.ChannelId);
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("hello")]
        public ActionResult Hello()
        {
            return View("Index");
        }

        [Route("first")]
        public ActionResult First()
        {
            return View();
        }

        [Route("second")]
        public ActionResult Second()
        {
            return View();
        }

        [Route("configure")]
        public ActionResult Configure()
        {
            return View();
        }
    }
}
