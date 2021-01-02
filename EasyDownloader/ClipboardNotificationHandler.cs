namespace EasyDownloader
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;
    using System.Windows.Forms;

    class ClipboardNotificationHandler
    {
        private static readonly int MAX_HISTORY_QUEUE_LENGTH = 100;

        private ClipboardNotification clipboardNotification;

        private static BlockingCollection<string> notificationQueue = new BlockingCollection<string>();

        private static HashSet<string> historySet = new HashSet<string>();

        private static Queue<string> historyQueue = new Queue<string>();

        public ClipboardNotificationHandler()
        {
            this.clipboardNotification = new ClipboardNotification(new Action(AddClipboardContentToQueue));
            Task.Run(() => this.GenerateAndDistrbuteTasks());
        }

        /// <summary>
        /// Add text from Clipboard to queue
        /// </summary>
        public static void AddClipboardContentToQueue()
        {
            Diagnostics.WriteDebugTrace("AddClipboardContentToQueue");
            string text = Clipboard.GetText();
            notificationQueue.Add(text);
        }

        public void GenerateAndDistrbuteTasks()
        {
            Diagnostics.WriteDebugTrace("Start GenerateAndDistrbuteTask");

            while (true)
            {
                string text = notificationQueue.Take();
                Diagnostics.WriteDebugTrace($"Get string for notification queue: {text}", Diagnostics.DebugLevel.Debug);

                if (historySet.Contains(text))
                {
                    continue;
                }

                if (Util.IsValidUrl(text))
                {
                    if (Util.IsImageUrl(text))
                    {
                        TaskManager.Instance.AddImageTask(text);
                    }
                    else if (Util.IsVideoUrl(text))
                    {
                        TaskManager.Instance.AddVideoTask(text);
                    }
                    else
                    {
                        // write to log
                        Diagnostics.WriteDebugTrace($"{text} is url but not image or video", Diagnostics.DebugLevel.Debug);
                        continue;
                    }
                }
                else
                {
                    // write to log
                    Diagnostics.WriteDebugTrace($"{text} is not url", Diagnostics.DebugLevel.Debug);
                    continue;
                }

                // Add text to history
                historySet.Add(text);
                historyQueue.Enqueue(text);

                if (historyQueue.Count > MAX_HISTORY_QUEUE_LENGTH)
                {
                    string oldText = historyQueue.Dequeue();
                    historySet.Remove(oldText);
                }
            }
        }
    }
}
