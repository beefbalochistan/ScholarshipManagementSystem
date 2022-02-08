
namespace ScholarshipManagementSystem.Models
{
    public class SingletonCache
    {
        private static int InProcessFile = 0;
        private static int RejectedFile = 0;
        private static int WaitingFile = 0;
        private static readonly SingletonCache _mySingletonServiceInstance = new SingletonCache();
        public SingletonCache()
        {            
        }
        public static SingletonCache GetInstance() => _mySingletonServiceInstance;
        public int GetInProcessFile() => InProcessFile;
        public void SetInProcessFile(int val)
        {
            InProcessFile = val;
        }
        public int GetRejectedFile() => RejectedFile;
        public void SetRejectedFile(int val)
        {
            RejectedFile = val;
        }
        public int GetWaitingFile() => WaitingFile;
        public void SetWaitingFile(int val)
        {
            WaitingFile = val;
        }
    }
}
