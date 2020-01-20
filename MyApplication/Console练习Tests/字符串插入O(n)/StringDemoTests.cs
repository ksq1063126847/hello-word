using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Tests
{
    [TestClass()]
    public class StringDemoTests
    {
        //[TestMethod()]
        //public void RelpaceBlankTest()
        //{
        //    // "We are happy." 将 空格 ' '替换为 % 20。此方法的时间复杂度O(n)
        //    //在网络传输中，需要将空格、#等特殊字符，转成对应的ASCII编码值
        //    //length 为字符数组的容量
        //    StringDemo stringDemo = new StringDemo();
        //    string testStr = "We are happy.";
        //    char[] arr = new char[20];
        //    for (int i = 0; i < testStr.ToCharArray().Length; i++)
        //    {
        //        arr[i] = testStr[i];
        //    }
        //    stringDemo.RelpaceBlank(arr, arr.Length);
        //    Assert.AreEqual("We%20are%20happy.\0\0\0", string.Join("", arr));
        //}

        //[TestMethod()]
        //public void MergeArrayTest()
        //{
        //    int[] arr1 = new int[10] { 1, 2, 5, 7, 9, 0, 0, 0, 0 ,0};
        //    int[] arr2 = new int[2] { 1,2 };
        //    int[] rightArr = { 1, 1, 2, 2, 5, 7, 9, 0, 0, 0 };

        //    var result = new StringDemo().MergeArray(arr1, arr2);
        //    for (int i = 0; i < rightArr.Length; i++)
        //    {
        //        Assert.AreEqual(rightArr[i], result[i]);
        //    }
        //}
    }
}