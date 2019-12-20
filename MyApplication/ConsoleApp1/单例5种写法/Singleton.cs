using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    //注：第4、第5,都是可供多线程时使用的最优单例实现方式

    //5.保证 按“需要”时才实例化单例
    //同样利用了 静态构造函数，只加载一次
    public class Singleton
    {
        private Singleton() { }
        public static Singleton Instance
        {
            get { return Nested.Instance; }
        }
        private class Nested
        {
            static Nested() { }
            internal static readonly Singleton Instance = new Singleton();

        }
    }


    //1.单例（单线程）
    public class Singleton1
    {
        private static Singleton1 instance = null;
        private Singleton1() { }

        public static Singleton1 Instance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton1();
                return instance;
            }
        }
    }

    //2.可异步 调用的单例
    public class Singleton2
    {
        private static Object syncobj = new object();
        private Singleton2() { }

        public static Singleton2 instance = null;

        public static Singleton2 Instance
        {
            get
            {
                lock (syncobj)
                {
                    if (instance == null)
                    {
                        instance = new Singleton2();
                    }
                }
                return instance;
            }
        }
    }

    //3.可异步 调用的单例,在2的基础上优化
    public class Singleton3
    {
        private static Object syncobj = new object();

        public static Singleton3 instance = null;
        private Singleton3() { }

        public static Singleton3 Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncobj)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton3();
                        }
                    }
                }
                return instance;
            }
        }
    }

    //4.利用了 静态构造函数只加载一次
    public class Singleton4
    {
        private Singleton4() { }

        private static readonly Singleton4 instance = new Singleton4();

        public static Singleton4 Instance
        {
            get => instance;
        }
    }
}





