using System;
using System.Timers;

using MethodInvocationLoggingSample.Core.Service;
using MethodInvocationLoggingSample.Logic;

namespace MethodInvocationLoggingSample
{
    public class Service : IService
    {
        private const int Interval = 3000; // # in terms of milliseconds

        private readonly INeverLoggedObject neverLoggedObject;
        private readonly IPartiallyLoggedObject partiallyLoggedObject;
        private readonly IAlwaysLoggedObject alwaysLoggedObject;
        private readonly Timer timer;

        public Service(INeverLoggedObject neverLoggedObject, IPartiallyLoggedObject partiallyLoggedObject, IAlwaysLoggedObject alwaysLoggedObject)
        {
            this.partiallyLoggedObject = partiallyLoggedObject;
            this.alwaysLoggedObject = alwaysLoggedObject;
            this.neverLoggedObject = neverLoggedObject;

            timer = new Timer(Interval) { AutoReset = false } ;
            timer.Elapsed += RunScheduledJob;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Dispose();
        }

        private void RunScheduledJob(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            #if DEBUG // # Run synchronized in debug mode
            timer.Stop();
            #endif

            RunScheduledJob();

            #if DEBUG
            timer.Start();
            #endif
        }

        private void RunScheduledJob()
        {
            neverLoggedObject.NotLoggedMethod();

            partiallyLoggedObject.NotLoggedMethodWithoutDecoration();
            partiallyLoggedObject.LoggedMethodWithDecoration(Guid.NewGuid(), "some string"); // # Guid value is expected to be different each time and return value is expected to be current day each time!

            alwaysLoggedObject.LoggedMethodWithoutDecoration(1, "some other string"); // # Int value is expected to be 1 each time and return value is expected to be current date each time!
        }
    }
}