namespace FirstTest.Handler
{
    public class Timer
    {
        private System.Threading.Timer timer;
        
        private readonly object lockObject = new object();

        private bool isRunning = false;
        private bool lastKnownRunningState = false;

        private DateTime targetTime;
        private List<TimeSpan> intervals = [];
        private int currentIndex = 0;
        
        public Timer()
        {
            timer = new System.Threading.Timer(OnTimedEvent); // set the callback but do not start the timer
        }

        public void StartTimer(TimeSpan timeTobuild)
        {
            if(IsRunning()) throw new MustRestartException("Timer can't be overrided.");
            
            targetTime = DateTime.Now.Add(timeTobuild);
            
            if(timeTobuild.Days > 0)
            {
                intervals.Add(new TimeSpan(timeTobuild.Days,0,0,0));
            }

            if(timeTobuild.Hours > 0)
            {
                intervals.Add(new TimeSpan(timeTobuild.Hours,0,0));
            }
            
            if(timeTobuild.Minutes > 0)
            {
                intervals.Add(new TimeSpan(0,timeTobuild.Minutes,0));
            }
            
            if(timeTobuild.Seconds > 0)
            {
                intervals.Add(new TimeSpan(0,0,timeTobuild.Seconds));
            }

            currentIndex = 0;
            isRunning = true;
            lastKnownRunningState = true;
            timer.Change((long)intervals[currentIndex].TotalMilliseconds, Timeout.Infinite); // Start the timer
        }

        public void StopTimer()
        {
            if(!IsRunning()) throw new MustRestartException("Timer should not be stopped");
            
            timer.Change(Timeout.Infinite, Timeout.Infinite); // Stop the timer

            //reset properties to initial value
            currentIndex = 0;
            intervals = [];
            isRunning = false;
            lastKnownRunningState = false;
            targetTime = DateTime.MinValue;
        }

        public bool IsRunning()
        {
            try
            {
                Monitor.TryEnter(lockObject, 0);
                lastKnownRunningState = isRunning; // Store the last known TimeSpan
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
            return lastKnownRunningState; // Return the last known TimeSpan
        }

        private void OnTimedEvent(Object state)
        {
            lock (lockObject)
            {
                if (DateTime.Now > targetTime)
                {
                    StopTimer();
                }
                else
                {
                    currentIndex = currentIndex + 1;

                    if (currentIndex < intervals.Count)
                    {
                        timer.Change((long)intervals[currentIndex].TotalMilliseconds, Timeout.Infinite);
                    }
                    else
                    {
                        timer = new System.Threading.Timer(OnTimedEventEnds, null, 1000, 1000);
                    }
                }
            } 
        }

        private void OnTimedEventEnds(Object state)
        {
            lock (lockObject)
            {
                if (DateTime.Now > targetTime)
                {
                    StopTimer();
                }
            }
        }
    }
}