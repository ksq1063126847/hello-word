using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
     public class Class1
    {
        public int[] HeapSortMain(int[] arr)
        {
            //1.构建大顶堆
            for (int i = arr.Length / 2 - 1; i >= 0; i--)
            {
                HeapSort(arr, i, arr.Length);
            }
            //2.开始交换位置，并重新构建大顶堆二叉树
            for (int i = arr.Length - 1; i > 0; i--)
            {
                swap(arr, 0, i);
                HeapSort(arr, 0, i);
            }
            return arr;
        }

        public void HeapSort(int[] arr, int index, int len)
        {
            var largest = index;
            var left = 2 * index + 1;
            var right = 2 * index + 2;
            if (left < len && arr[left] > arr[largest])
                largest = left;
            if (right < len && arr[right] > arr[largest])
                largest = right;
            if (largest != index)
            {
                swap(arr, index, largest);
                HeapSort(arr, largest, len);
            }
            
        }

        public void swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
