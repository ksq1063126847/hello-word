using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class MergeSort
    {
        public MergeSort() { }
        // 归并法，左右两侧数列，按值大小依次放入
        public void Merge(int[] arr, int left, int mid, int right, int[] temp)
        {
            int i = left;//左序列指针
            int j = mid+1;//右序列指针
            int t = 0;//临时数组指针
            while (i <= mid && j <= right) //left --> mid , mid+1 -->right
            {
                if (arr[i] <= arr[j])
                    temp[t++] = arr[i++];
                else
                    temp[t++] = arr[j++];
            }
            while (i <= mid)//将左边剩余元素填充进temp中
                temp[t++] = arr[i++];
            while (j <= right)//将右序列剩余元素填充进temp中
                temp[t++] = arr[j++];
            t = 0;
            while (left <= right)
                arr[left++] = temp[t++];
        }
    }
}
