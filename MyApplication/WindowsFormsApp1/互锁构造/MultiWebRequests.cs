using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public sealed class MultiWebRequests
    {

        private AsyncCoordinator m_ac = new AsyncCoordinator();

        private Dictionary<string, object> m_services = new Dictionary<string, object>()
        {
            { "http://www.baidu.com//",null },
            { "http://www.microsoft.com",null }
        };

        public MultiWebRequests(Int32 timeout = Timeout.Infinite)
        {
            //以异步方式一次性发出所有请求
            HttpClient httpClient = new HttpClient();
            foreach (var server in m_services.Keys)
            {
                m_ac.AboutToBegin(1);
                httpClient.GetByteArrayAsync(server).ContinueWith(p => ComputeResult(server, p));
            }
            m_ac.AllBegun(AllDone, timeout);
        }
        public void ComputeResult(string server, Task<byte[]> task)
        {
            Object result;
            if (task.Exception != null)
            {
                result = task.Exception.InnerException;
            }
            else
            {
                result = task.Result.Length;
            }
            m_services[server] = result;
            m_ac.JustEnded();
        }

        public void Cancel() => m_ac.Cancel();

        private void AllDone(CoordinatorStatus status)
        {
            switch (status)
            {
                case CoordinatorStatus.AllDone:
                    Console.WriteLine("Operation completed; result below:");
                    foreach (var server in m_services)
                    {
                        Console.Write("{0} ", server.Key);
                        Object result = m_services[server.Key];
                        if (result is Exception)
                        {
                            Console.WriteLine("fail due to {0}.", result.GetType().Name);
                        }
                        else
                        {
                            Console.WriteLine("returned {0} bytes.", result);
                        }
                    }
                    break;
                case CoordinatorStatus.Timeout:
                    Console.WriteLine("Operation timed-out");
                    break;
                case CoordinatorStatus.Cancel:
                    Console.WriteLine("Operation canceled");
                    break;
                default:
                    break;
            }
        }

    }

    public enum CoordinatorStatus { AllDone, Timeout, Cancel }
    public sealed class AsyncCoordinator
    {
        Int32 m_opCount = 1;//Allbegun 内部调用JustEnded来递减他
        Int32 m_statusReport = 0;// 0=false ,1 =true
        Action<CoordinatorStatus> m_callback;
        Timer m_timer;

        //该方法必须在发起一个操作之前调用
        public void AboutToBegin(int opsToAdd = 1)
        {
            Interlocked.Add(ref m_opCount, opsToAdd);
        }

        //该方法必须在处理好一个方法之后调用
        public void JustEnded()
        {
            if (Interlocked.Decrement(ref m_opCount) == 0)
            {
                ReportStatus(CoordinatorStatus.AllDone);
            }
        }
        //该方法必须在发起所有操作之后调用
        public void AllBegun(Action<CoordinatorStatus> callback, Int32 timeout = Timeout.Infinite)
        {
            m_callback = callback;
            if (timeout != Timeout.Infinite)
            {
                m_timer = new Timer(TimerExpired, null, timeout, Timeout.Infinite);
            }
            JustEnded();
        }

        private void TimerExpired(Object o) => ReportStatus(CoordinatorStatus.Timeout);

        public void Cancel() => ReportStatus(CoordinatorStatus.Timeout);

        private void ReportStatus(CoordinatorStatus status)
        {
            //如果状态从未报告过，就报告他，否则忽略他；
            if (Interlocked.Exchange(ref m_statusReport, 1) == 0)
            {
                m_callback(status);
            }
        }
    }
}
