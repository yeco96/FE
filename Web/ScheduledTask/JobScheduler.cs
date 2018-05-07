using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSchoolWeb.ScheduledTask
{
    public class JobScheduler
    {
        public static async void Start()
        {
            // get a scheduler
            IScheduler sched = await StdSchedulerFactory.GetDefaultScheduler();
            await sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<JobsTask>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(60)
                  .RepeatForever())
              .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}
