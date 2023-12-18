using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;


namespace FirstTest.Handler
{
    public class Timer
    {
        private readonly System.Threading.Timer timer;
        private TimeSpan timeSpan;
        private TimeSpan lastKnownTimeSpan;
        private readonly object lockObject = new object();

        private bool isRunning = false;

        public bool CheckIsRunning =>  isRunning;
        
        public Timer()
        {
            timer = new System.Threading.Timer(OnTimedEvent, null, Timeout.Infinite, 1000); // Set up the timer to tick every 1000 milliseconds (1 second), but don't start it immediately
        }

        public void StartTimer()
        {
            isRunning = true;
            lock (lockObject)
            {
                timeSpan = TimeSpan.Zero; // Reset the TimeSpan
                lastKnownTimeSpan = TimeSpan.Zero; // Reset the last known TimeSpan
            }
            timer.Change(0, 1000); // Start the timer
        }

        public void StopTimer()
        {
            isRunning = false;
            timer.Change(Timeout.Infinite, 1000); // Stop the timer
        }

        public TimeSpan GetTimeSpan()
        {
            try
            {
                Monitor.TryEnter(lockObject, 0);
                lastKnownTimeSpan = timeSpan; // Store the last known TimeSpan
            }
            catch (Exception)
            {
                // If OnTimedEvent is running, an exception will be caught here and the method will return the last known TimeSpan
            }
            finally
            {
                if (Monitor.IsEntered(lockObject))
                {
                    Monitor.Exit(lockObject);
                }
            }
            return lastKnownTimeSpan; // Return the last known TimeSpan
        }

        private void OnTimedEvent(Object? state)
        {
            lock (lockObject)
            {
                timeSpan = timeSpan.Add(TimeSpan.FromSeconds(1)); // Add one second to the TimeSpan
            }
        }
    }
}