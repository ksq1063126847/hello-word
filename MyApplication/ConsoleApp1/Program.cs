using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] arr = new int[] { 3, 7, 5, 4, 7, 8, 0, 9, 1, 2, 10, 6 };
            //SortClass sort = new SortClass();           
            //arr = sort.QuickSort(arr, 0, arr.Length - 1);
            //System.Console.WriteLine(string.Join(",", arr.Select(p => p.ToString())));
            //System.Console.ReadLine();

            //System.Console.WriteLine("Main thread: queuing a asynchronous operation");
            //ThreadPool.QueueUserWorkItem(GetMsg, 5);
            //System.Console.WriteLine("Main thread: Doing Other work here");
            //Thread.Sleep(3000);
            //System.Console.WriteLine("Hit <Enter> to end this program");
            //System.Console.ReadLine();

           
            System.Console.ReadLine();
        }

        #region 异步Task模拟
        public static void AsyncTaskMethod()
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
        public static int Sum(CancellationToken token, int count)
        {

            for (int i = 0; i < count; i++)
            {
                token.ThrowIfCancellationRequested();
                System.Console.WriteLine("this is " + i.ToString());
            }
            return -1;
        }
        #endregion

        private string GetString(string s)
        {
            //StringBuilder newStr = new StringBuilder();
            //var sList = s.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //if (sList.Count() == 0)
            //    return s;
            //foreach (var item in sList)
            //{
            //    for (int i = 0; i < item.Length; i++)
            //    {
            //        if (i % 2 == 0)
            //            newStr.Append(item[i]);
            //        else
            //            newStr.Append(item[i].ToString().ToLower());
            //    }
            //    newStr.Append(" ");
            //}
            //return newStr.ToString().TrimEnd(new char[] { ' ' });

            return string.Join(" ", s.Split(' ').Select(k => string.Concat(k.Select((p, i) => i % 2 == 0 ? char.ToLower(p) : char.ToUpper(p)))));

        }
        private static void Marshalling()
        {
            AppDomain adCallingThreadDomain = Thread.GetDomain();
            string exeAssembly = Assembly.GetEntryAssembly().FullName;

            AppDomain ad2 = null;
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            MarshalByRefObject mbro = null;
            mbro = (MarshalByRefObject)ad2.CreateInstanceAndUnwrap(exeAssembly, "MarshalByRefObject");
            System.Console.WriteLine("Type={0}", mbro.GetType());
            System.Console.WriteLine("Is Proxy=", RemotingServices.IsTransparentProxy(mbro));
            mbro.ToString();
            AppDomain.Unload(ad2);
        }
        private static void test1()
        {
            var arr = new[] { 1, 2, 3 };
            var arrTest = new int[3];
            Array.Copy(arr, arrTest, arr.Length - 1);
        }
        private static bool test2(string input)
        {
            bool result = false;
            var arr = input.Where(p => p == '(' || p == ')').ToList();
            if (arr.Count() % 2 != 0)
                result = false;
            else
            {
                int index = 0;
                while (arr.Count() > 0 && index < arr.Count())
                {
                    if (index >= arr.Count / 2 || arr[0] != '(')
                    {
                        result = false;
                        break;
                    }
                    index = test3(arr, index);
                }
                if (arr.Count == 0)
                    result = true;
            }
            return result;
        }
        private static int test3(List<char> arr, int index)
        {
            if (arr[index] != arr[index + 1])
            {
                arr.RemoveAt(index + 1);
                arr.RemoveAt(index);
                return 0;
            }
            else
                return index + 1;
        }

        #region 判断括号是否对称
        public static bool ValidParentheses(string input)
        {
            //eg: ((1)(2)3) 
            var count = 0;
            foreach (var c in input)
            {
                if (count == -1)
                    return false;
                if (c == '(')
                    count++;
                if (c == ')')
                    count--;
            }
            return count == 0;
        }
        #endregion

        #region 最大公约数，最小公倍数
        public static string convertFrac(long[,] lst)
        {
            string result = "";
            try
            {
                //1.取得最小公倍数               
                long minBeishu = 0;
                long maxYueShu = 0;
                for (int i = 0; i < lst.GetLength(0) - 1; i++)
                {
                    maxYueShu = MaxYueShu(lst[i, 1], lst[i + 1, 1]);
                    long temp = lst[i, 1] * lst[i + 1, 1] / maxYueShu;
                    minBeishu = Math.Max(temp, minBeishu);
                }
                //2.结果
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < lst.GetLength(0); i++)
                {
                    long num = minBeishu / lst[i, 1];
                    lst[i, 0] = lst[i, 0] * num;
                    lst[i, 1] = lst[i, 1] * num;
                    str.AppendFormat("({0},{1})", lst[i, 0], lst[i, 1]);
                }
                result = str.ToString();
            }
            catch (IndexOutOfRangeException ex)
            {
                throw ex;
            }
            return result;
        }
        public static long MaxYueShu(long num1, long num2)
        {
            long a = Math.Max(num1, num2);
            long b = Math.Min(num1, num2);
            long temp;
            while (b != 0)
            {
                temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }
        #endregion

        #region 经典递归算法
        public static bool isMerge(string s, string part1, string part2)
        {
            //isMerge("Can we merge it? Yes, we can!", "n ee tYw n!", "Cawe mrgi? es, eca");
            // "Can we merge it? Yes, we can!" 是否能够通过 part1和part2 拼凑出来
            bool empty1 = part1.Length == 0,
                 empty2 = part2.Length == 0,
                 works1 = false,
                 works2 = false;

            if (s.Length == 0)
            {
                if (part1.Length == 0 && part2.Length == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if (!empty1 && s[0] == part1[0]) works1 = isMerge(s.Substring(1), part1.Substring(1), part2);
                if (!empty2 && s[0] == part2[0]) works2 = isMerge(s.Substring(1), part1, part2.Substring(1));
                return works1 || works2;
            }
        }
        #endregion

    }
}
