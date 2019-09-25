using ConsoleApp1.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AsyncDemo : IDemo
    {
        public void Run()
        {
            //1.异步Task
            //AsyncTaskMethod();
            //2.任务工厂
            AsyncTaskFactoryMethod();
        }

        #region 异步Task模拟
        public void AsyncTaskMethod()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> task = new Task<int>(() => Sum(cts.Token, 5));
            task.Start();

            cts.Cancel();
            try
            {
                System.Console.WriteLine(task.Result);
            }
            catch (AggregateException ex)
            {
                ex.Handle(p => p is OperationCanceledException);
                System.Console.WriteLine("Sum was canceled !");
            }
        }
        public int Sum(CancellationToken token, int count)
        {
            int result = 0;
            for (int i = 0; i < count; i++)
            {
                token.ThrowIfCancellationRequested();
                result += i;
                System.Console.WriteLine("this is " + i.ToString());
            }
            return result;
        }
        #endregion

        public void AsyncTaskFactoryMethod()
        {
            Task parent = new Task(() =>
            {
                var cts = new CancellationTokenSource();
                var tf = new TaskFactory<Int32>(
                    cts.Token,
                    TaskCreationOptions.AttachedToParent,
                    TaskContinuationOptions.ExecuteSynchronously,
                    TaskScheduler.Default);

                //这个任务工厂，创建并且启动3个子任务
                var childTasks = new[] {
                     tf.StartNew(()=>SumInt32(cts.Token,10000)),
                     tf.StartNew(()=>SumInt32(cts.Token,20000)),
                     tf.StartNew(()=>SumInt32(cts.Token,Int32.MaxValue))
                 };

                //任何子任务抛出异常，就取消其余子任务
                for (int i = 0; i < childTasks.Length; i++)
                    childTasks[i].ContinueWith(t => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);

                //所有子任务完成后，从无出错/未取消的任务获取返回的最大值
                //然后将最大值传给另一个任务来显示
                tf.ContinueWhenAll(
                    childTasks,
                    completedTasks => completedTasks.Where(t => !t.IsFaulted && !t.IsCanceled).Max(t => t.Result),
                    CancellationToken.None).ContinueWith(t => System.Console.WriteLine("The max is " + t.Result),TaskContinuationOptions.ExecuteSynchronously);
            });

            parent.ContinueWith(p =>
            {
                StringBuilder sb = new StringBuilder(
                    "The following exceptions occured: " + Environment.NewLine
                    );
                foreach (var e in p.Exception.Flatten().InnerExceptions)
                {
                    sb.AppendFormat("  " + e.GetType().ToString());
                }
                Console.WriteLine(sb.ToString());
            },TaskContinuationOptions.OnlyOnFaulted);

            parent.Start();

        }

        public Int32 SumInt32(CancellationToken ct, Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                //在取消标志引用的CancellationTokenSource上调用Cancel。 cts.Cancel()
                //下面这行代码就会抛出OperationCanceledExceptioon
                ct.ThrowIfCancellationRequested();
                checked { sum += n; } //如果n太大，会抛出System.OverFlowException
            }
            return sum;
        }
    }
}
