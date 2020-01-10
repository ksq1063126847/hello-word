﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.TaskLog
{
    public static class TaskLogger
    {
        public enum TaskLoggerLevel { None, Pending }
        public static TaskLoggerLevel LogLevel { get; set; }

        public sealed class TaskLogEntry
        {
            public Task Task { get; internal set; }
            public string Tag { get; internal set; }
            public DateTime LogTime { get; internal set; }
            public string CallerMemberName { get; internal set; }
            public string CallerFilePath { get; internal set; }
            public Int32 CallerLineNumber { get; internal set; }
            public override string ToString()
            {
                return string.Format("LogTime={0},Tag={1},Member={2},File={3}{4}", LogTime, Tag ?? "none", CallerMemberName, CallerFilePath, CallerLineNumber);
            }

        }
        private static readonly ConcurrentDictionary<Task, TaskLogEntry> s_log = new ConcurrentDictionary<Task, TaskLogEntry>();
        public static IEnumerable<TaskLogEntry> GetLogEntries() => s_log.Values;
        public static Task<T> Log<T>(this Task<T> task, string tag = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = -1
        )
        {
            return (Task<T>)Log((Task)task, tag, callerMemberName, callerFilePath, callerLineNumber);
        }

        public static Task Log(this Task task,
            string tag = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = -1
        )
        {
            if (LogLevel == TaskLoggerLevel.None)
                return task;
            var logEntry = new TaskLogEntry()
            {
                Task = task,
                LogTime = DateTime.Now,
                Tag = tag,
                CallerMemberName = callerMemberName,
                CallerFilePath = callerFilePath,
                CallerLineNumber = callerLineNumber
            };
            s_log[task] = logEntry;
            task.ContinueWith(t => { TaskLogEntry entry; s_log.TryRemove(t, out entry); }, TaskContinuationOptions.ExecuteSynchronously);
            return task;
        }
        public static async Task Go()
        {
# if DEBUG
            TaskLogger.LogLevel = TaskLoggerLevel.Pending;
#endif
            var tasks = new List<Task>()
            {
                Task.Delay(2000).Log("2s op"),
                Task.Delay(5000).Log("5s op"),
                Task.Delay(6000).Log("6s op")
            };

        }

    }
}