using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ren.CMS.CORE.Taskmanagement;
using System.Text;
namespace Ren.CMS.Controllers
{

    public class MyDemoTask : RTask
    {

        public MyDemoTask()
        {
           //Setting up standart values for our task:
            this.ModuleDBTable = "Strings";
            this.ModuleDBIdentifier = "Id";
            this.ModuleDBidValue = "1"; 
            //The 3 Lines above tells the Taskmanagement Class this task references to  the table "Strings" for the Entry with ID 1
            this.TaskName = "Doing cool Stuff with Strings";
        }

        public override void TaskAction()
        {
            //This adds the Watcher Task.
            //Note if you leave this out you have to update / insert into DB by your self.
            //Note also that you need to call the base.TaskAction();  at the end to tell the watcher its finished.
            base.addWatcherTask();
            //Dont Forget: 
            //All you have to do in this example is doing your action and changing the Properties doing runtime
            //like Percentage, Status etc...
            //The watcher Task will automatically register the task and automatically update it

            if(this.TaskData.Any(e => e.Key == "_LIST"))
            {
                var list  = TaskData["_LIST"] as List<string>;

                StringBuilder  b = new StringBuilder();
                int i = 0;
                foreach (var str in list)
                {
                    i++;
                    this.Percentage = (list.Count * 100 / i); //This will update the Percentage, note: The DB is rounding on decimal digits.
                    this.CurrentAction = str; //Tell our users what the f. are we doing here. This shows up in the task manager(Backend)
                  
                    b.AppendLine(str);
                }

            }

            //Called if Task is finished.
            base.TaskAction();
        }

    }

    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            //Create a new Instace of  MyDemoTask
            MyDemoTask Task = new MyDemoTask();
            //Filling it with our Data
            Task.TaskData.Add("_LIST", new List<string>() { "Test1", "Test2", "Test3" });
            //Register and Start the Task
            Taskmanagement.RegisterTask<MyDemoTask>(Task);

           
            return View();
        }

    }
}
