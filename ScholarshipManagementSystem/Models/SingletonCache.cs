
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Models
{
    public static class MyStaticClass
    {
        private static int InProcessFile = 0;
        private static int RejectedFile = 0;
        private static int WaitingFile = 0;
                
        public static void SetInProcessFile(int val)
        {
            InProcessFile = val;
        }        
        public static void SetRejectedFile(int val)
        {
            RejectedFile = val;
        }        
        public static void SetWaitingFile(int val)
        {
            WaitingFile = val;
        }

        public static Task<int> GetRejectedFile() => Task.FromResult(RejectedFile);
      
        public static Task<int> GetInProcessFile() => Task.FromResult(InProcessFile);

        public static Task<int> GetWaitingFile() => Task.FromResult(WaitingFile);
    }
}
