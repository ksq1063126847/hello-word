using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class BackGroundWorker : Form
    {
        private readonly TaskScheduler m_syncContextTaskScheduler;
        public BackGroundWorker()
        {
            InitializeComponent();
            m_syncContextTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Text = "Synchronization Context Task Scheduler Demo";

            progressBar1.Maximum = 100;
            backgroundWorker1.WorkerReportsProgress = true; //允许报告进度
            backgroundWorker1.WorkerSupportsCancellation = true;//支持取消操作
        }

        private CancellationTokenSource m_cts;
        protected override void OnMouseClick(MouseEventArgs e)
        {
            //if (m_cts != null)
            //{
            //    m_cts.Cancel();
            //    m_cts = null;
            //}
            //else
            //{
            //    Text = "Operation running";
            //    m_cts = new CancellationTokenSource();
            //    Task<Int32> task = Task.Run(() => Sum(m_cts.Token, 20000), m_cts.Token);

            //    task.ContinueWith(p => Text = "Result：" + p.Result,
            //        CancellationToken.None,
            //        TaskContinuationOptions.OnlyOnRanToCompletion,
            //        m_syncContextTaskScheduler);

            //    task.ContinueWith(p => Text = "Operation canceled",
            //       CancellationToken.None,
            //       TaskContinuationOptions.OnlyOnCanceled,
            //       m_syncContextTaskScheduler);

            //    task.ContinueWith(p => Text = "Operation faulted",
            //       CancellationToken.None,
            //       TaskContinuationOptions.OnlyOnFaulted,
            //       m_syncContextTaskScheduler);

            //}

            //base.OnMouseClick(e);
        }
        public Int32 Sum(CancellationToken ct, Int32 n)
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

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //1.do work
            for (int i = 0; i < 100; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;                   
                    return;
                }       
                else
                {
                    //模拟任务执行
                    Thread.Sleep(500);
                    //报告进度
                    backgroundWorker1.ReportProgress(i);
                }             
            }            
            //2.Done
            e.Result = "Finish";
            backgroundWorker1.ReportProgress(100);
        }
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.ProgressPercentage + "%";
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label1.Text = "已取消";
            }
            else if (e.Error != null)
            {
                label1.Text = "出错";
            }
            else 
            {
                label1.Text = e.Result.ToString();
            } 
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
                return;
            backgroundWorker1.RunWorkerAsync();
            
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
    }
}
