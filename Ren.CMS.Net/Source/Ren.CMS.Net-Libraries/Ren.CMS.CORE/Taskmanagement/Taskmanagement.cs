using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NReco.VideoConverter;

namespace Ren.CMS.CORE.Taskmanagement
{
    public class RTask
    {
        public RTask() 
        {
          
        
        }

        public int ID { get; set; }

        public int TaskID  { get;set;}

        public virtual bool Running { get;set; }

        public virtual string TaskName { get; set; }

        public virtual string CurrentAction { get; set; }

        public virtual string ModuleDBTable { get; set; }

        public virtual string ModuleDBIdentifier { get; set; }

        public virtual string ModuleDBidValue { get; set; }

        public virtual decimal Percentage { get; set; }


        public virtual void TaskAction()
        { 
        
        
        
        }
    
    
    
    }


    public static class Taskmanagement
    {

        public static T GetTask<T>(int id) where T : RTask, new()
        {

           return  new T();
        }


        public static void DeleteTask(int id)
        { 
        
        
        }

        public static void UpdateTask<T>(T task) where T : RTask, new()
        { 
        
        
        }

        public static int RegisterTask<T>(T task) where T : RTask, new()
        {
             
            Task tsk = new Task(task.TaskAction, cancellationToken: System.Threading.CancellationToken.None);
            
            tsk.RunSynchronously();
            task.TaskID = tsk.Id;

            /* Save in DB */

            return 0;
        }
    }


}
