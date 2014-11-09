using Ren.CMS.CORE.Taskmanagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content.Tasks
{
    public class ConvertVideoTask : RTask
    {

        public ConvertVideoTask()
        {
            //Setting up standart values for our task:
 
            //The 3 Lines above tells the Taskmanagement Class this task references to  the table "Strings" for the Entry with ID 1
            this.TaskName = "Convert Videos";
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

            if (this.TaskData.Any(e => e.Key == "_ATTACHMENTS") && this.TaskData.Any(e => e.Key == "_FORMATS"))
            {
                var list = TaskData["_ATTACHMENTS"] as List<nContentAttachment>;
                var listFormats = TaskData["_FORMATS"] as List<Ren.CMS.Persistence.Domain.FilemanagementCrossBrowsers>;
                
                int i = 0;
                int max = list.Count * listFormats.Count;

                foreach (var attachment in list)
                {
                    foreach(var format in listFormats)
                    {
                        this.CurrentAction = "Converting Attachment \"" + attachment.AttachmentID + "\" to \"*." + format.FileFormat + "\"";
                        this.Running = true;
                        attachment.AttachmentType.Handler.Convert(format.FileFormat);
                        this.Percentage = (i > 0 ? (i * 100 / max) : 0);
                        i++;
                    }
                   
                }

            }

            //Called if Task is finished.
            base.TaskAction();
        }

    }
}
