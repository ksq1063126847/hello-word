using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace ConsoleApp1
{
    public class StringDemo
    {

        //"We are happy." 将 空格 ' '替换为 %20,在网络传输中，需要将空格、#等特殊字符，转成对应的ASCII编码值
        //length 为字符数组的容量
        public void RelpaceBlank(char[] arr, int length)
        {
            if (arr == null || arr.Length == 0)
            {
                return;
            }
            int originLength = arr.Count(p => p != char.MinValue);
            int spaceCount = arr.Count(p => p == ' ');
            int newLength = originLength + spaceCount * 2;
            if (newLength > length)
            {
                return;
                throw new ArgumentOutOfRangeException();
            }
            int indexOfOriginal = originLength;
            int indexofNew = newLength;
            while (indexOfOriginal >= 0 && indexofNew > indexOfOriginal)
            {
                if (arr[indexOfOriginal] == ' ')
                {
                    arr[indexofNew--] = '0';
                    arr[indexofNew--] = '2';
                    arr[indexofNew--] = '%';
                }
                else
                {
                    arr[indexofNew--] = arr[indexOfOriginal];
                }
                --indexOfOriginal;
            }
        }

        public int[] MergeArray(int[] arr1, int[] arr2)
        {
            if (arr1 == null || arr2 == null || arr1.Length == 0 || arr2.Length == 0)
                return null;
            int indexOfArr1_old = 0;

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == 0)
                {
                    indexOfArr1_old = i - 1;
                    break;
                }                   
            }
            int indexOfArr1_new = indexOfArr1_old + arr2.Length;
            int indexOfArr2 = arr2.Length - 1;
            //arr1长度不够
            if (indexOfArr1_new > arr1.Length)
                return null;
            while (indexOfArr1_old >= 0 && indexOfArr1_new > indexOfArr1_old)
            {
                if (arr1[indexOfArr1_old] > arr2[indexOfArr2])
                {
                    arr1[indexOfArr1_new--] = arr1[indexOfArr1_old];
                    indexOfArr1_old--;
                }
                else if (arr1[indexOfArr1_old] < arr2[indexOfArr2])
                {
                    arr1[indexOfArr1_new--] = arr2[indexOfArr2--];
                }
                else if (arr1[indexOfArr1_old] == arr2[indexOfArr2])
                {
                    arr1[indexOfArr1_new--] = arr2[indexOfArr2];
                    arr1[indexOfArr1_new--] = arr2[indexOfArr2];
                    indexOfArr1_old--;
                    indexOfArr2--;
                }
            }
            return arr1;

        }
    }
}
