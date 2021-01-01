
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
        public static readonly int DEAFULT_MAX_LENGTH = 20;

        public int MaxLength { get; }

        public int Length
        {
            get
            {
                return this.Queue.Count;
            }
        }

        public BlockingCollection<SingleTask> Queue { get; private set; }

        public TaskQueue() : this(DEAFULT_MAX_LENGTH)
        {
        }

        public TaskQueue(int maxLength)
        {
            this.MaxLength = maxLength > 0 ? maxLength : DEAFULT_MAX_LENGTH;
            this.Queue = new BlockingCollection<SingleTask>(this.MaxLength);
        }

        public void Add(SingleTask task)
        {
            this.Queue.Add(task);
        }

        public SingleTask Take()
        {
            return this.Queue.Take();
        }
    }
}
