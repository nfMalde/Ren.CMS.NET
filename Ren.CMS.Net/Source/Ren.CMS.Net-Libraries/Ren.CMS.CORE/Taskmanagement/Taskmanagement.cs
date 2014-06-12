using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NReco.VideoConverter;
using Ren.CMS.Persistence.Domain;
using Ren.CMS.Persistence.Mapping;
using Ren.CMS.Persistence.Base;
using Ren.CMS.Persistence.Repositories;
namespace Ren.CMS.CORE.Taskmanagement
{
    public class RTask
    {
        private BaseRepository<TbTasks> taskrepo = new BaseRepository<TbTasks>();
        public RTask(string name = "Unknown Task") 
        {
            this.Running = false;
            this.TaskName = name;
            this.CurrentAction = "Not started...";

           


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

        public Dictionary<string, object> TaskData = new Dictionary<string, object>();

        public virtual void addWatcherTask()
        {
            //Insert Task
            int id =  (int) taskrepo.AddAndGetId(new TbTasks()
            {
                Taskname = this.TaskName,
                Currentaction = this.CurrentAction,
                Running = this.Running,
                Taskid = this.TaskID,
                 Moduledbidentifier = this.ModuleDBIdentifier,
                 Moduledbidvalue = this.ModuleDBidValue,
                 Moduledbtable = this.ModuleDBTable,
                  Percentage = 0

            });
            this.ID = id;
            //Here Update DB
            //Watcher Task
            Task TSK = new Task(() =>
            {
                while (this.Running)
                {
                    //Nhibernate Update for Task
                    var Taskx = this.taskrepo.GetOne(NHibernate.Criterion.Expression.Where<TbTasks>(e => e.Id == this.ID));
                    if (Taskx == null)
                    {
                        this.Running = false;
                        continue;
                    }

                    Taskx.Taskid = this.TaskID;
                    Taskx.Taskname = this.TaskName;
                    Taskx.Currentaction = this.CurrentAction;
                    Taskx.Moduledbidentifier = this.ModuleDBIdentifier;
                    Taskx.Moduledbidvalue = this.ModuleDBidValue;
                    Taskx.Moduledbtable = this.ModuleDBTable;
                    Taskx.Percentage = this.Percentage;
                    //Taskx.Running = this.Running;
                    this.taskrepo.Update(Taskx);

                    System.Threading.Thread.Sleep(new TimeSpan(0, 0, 1));//Sleeping for 1 Second
                }

            });

            TSK.Start();
        }
        public virtual void TaskAction()
        {

                    

            //At the end were setting running to false 
            this.Running = false;

            var Taskx = this.taskrepo.GetOne(NHibernate.Criterion.Expression.Where<TbTasks>(e => e.Id == this.ID));
            if (Taskx != null)
            {
                this.Running = false;


                Taskx.Taskid = this.TaskID;
                Taskx.Taskname = this.TaskName;
                Taskx.Currentaction = this.CurrentAction;
                Taskx.Moduledbidentifier = this.ModuleDBIdentifier;
                Taskx.Moduledbidvalue = this.ModuleDBidValue;
                Taskx.Moduledbtable = this.ModuleDBTable;
                Taskx.Percentage = this.Percentage;
                Taskx.Running = this.Running;
                this.taskrepo.Update(Taskx);
            }
        }
    
    
    
    }


    public static class Taskmanagement
    {

        public static T GetTask<T>(int id) where T : RTask, new()
        {
            BaseRepository<TbTasks> TaskRepo = new BaseRepository<TbTasks>();
            var entry = TaskRepo.GetOne(NHibernate.Criterion.Expression.Where<TbTasks>(e => e.Id == task.ID));
            if(entry != null)
                return new T() {
                 ID = entry.Id,
                 TaskName = entry.Taskname,
                 TaskID = entry.Taskid,
                 CurrentAction = entry.Currentaction,
                 Running = entry.Running,
                 Percentage= entry.Percentage,
                 ModuleDBIdentifier = entry.Moduledbidentifier,
                 ModuleDBTable=entry.Moduledbtable,
                 ModuleDBidValue = entry.Moduledbidvalue
            
            
            };

           return  new T();
        }


        public static void DeleteTask(int id)
        {
            BaseRepository<TbTasks> TaskRepo = new BaseRepository<TbTasks>();

            var entry = TaskRepo.GetOne(NHibernate.Criterion.Expression.Where<TbTasks>(e => e.Id == id));

            if (entry != null)
                TaskRepo.Delete(entry);

        
        }

        public static T UpdateTask<T>(T task) where T : RTask, new()
        {
            BaseRepository<TbTasks> TaskRepo = new BaseRepository<TbTasks>();
            var entry = TaskRepo.GetOne(NHibernate.Criterion.Expression.Where<TbTasks>(e => e.Id == task.ID));

            entry.Currentaction = task.CurrentAction;
            entry.Moduledbidentifier = task.ModuleDBIdentifier;
            entry.Moduledbidvalue = task.ModuleDBidValue;
            entry.Moduledbtable = task.ModuleDBTable;
            entry.Percentage = task.Percentage;
            entry.Running = task.Running;
            entry.Taskid = task.TaskID;
            entry.Taskname = task.TaskName;

            TaskRepo.Update(entry);

            return task;
        }

        public static int RegisterTask<T>(T task) where T : RTask, new()
        {
             
            Task tsk = new Task(task.TaskAction, cancellationToken: System.Threading.CancellationToken.None);
           
            tsk.Start();
            task.TaskID = tsk.Id;

            /* Save in DB */

            return task.ID;
        }
    }


}
