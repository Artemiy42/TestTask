namespace TestTask.Utility
{
    public class Timer
    {
        private float _period;
        private float _lastTime;
        
        public Timer(float period)
        {
            _period = period;
        }

        public bool CheckTime(float time)
        {
            if (_lastTime < time)
            {
                _lastTime = time + _period;
                return true;
            }

            return false;
        }
    }
}