
namespace EasyDownloader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;

    internal class TaskQueue
    {
        public int Length
        {
            get
            {
                return this.Queue.Count;
            }
        }

        public BlockingCollection<TaskInfo> Queue { get; private set; }

        public TaskQueue()
        {
            this.Queue = new BlockingCollection<TaskInfo>();
        }

        public void Add(TaskInfo taskInfo)
        {
            this.Queue.Add(taskInfo);
        }

        public TaskInfo Take()
        {
            return this.Queue.Take();
        }
    }
}
