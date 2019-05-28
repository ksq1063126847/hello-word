using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class SortClass
    {
        //冒泡排序
        public int[] BoomSort(int[] arr)
        {
            if (arr != null)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    for (int j = 0; j < arr.Length - 1 - i; j++)
                    {
                        if (arr[j] > arr[j + 1])
                        {
                            var temp = arr[j];
                            arr[j] = arr[j + 1];
                            arr[j + 1] = temp;
                        }
                    }
                }
            }
            return arr;
        }
        //选择排序
        public int[] SelectSort(int[] arr)
        {
            int minIndex, temp;
            for (int i = 0; i < arr.Length; i++)
            {
                minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }
                temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }
            return arr;
        }
        //插入排序
        public int[] InsertSort(int[] arr)
        {
            if (arr != null)
            {
                for (int i = 1; i < arr.Length; i++)
                {
                    var current = arr[i];
                    var preIndex = i - i;
                    while (current < arr[preIndex] && preIndex > 0)
                    {
                        arr[preIndex + 1] = arr[preIndex];
                        preIndex--;
                    }
                    arr[preIndex + 1] = current;
                }
            }
            return arr;
        }

        //快速排序，前后指针法
        public int[] QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                var partitionIndex = PartSort(arr, left, right);
                QuickSort(arr, left, partitionIndex - 1);
                QuickSort(arr, partitionIndex + 1, right);
            }
            return arr;
        }
        int PartSort(int[] arr, int left, int right)
        {
            var cur = left;
            var prev = left - 1;
            var key = arr[right];
            while (cur < right)
            {
                if (arr[cur] < key && ++prev < cur)
                {
                    Swap(arr, cur, prev);
                }
                cur++;
            }
            Swap(arr, ++prev, right);
            return prev;
        }
        void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        //堆排序
        public int[]  HeapSortMain(int[] arr)
        {
            //1.构建大顶堆
            for (int i = arr.Length / 2 - 1; i >= 0; i--)
            {
                HeapSort(arr, i,arr.Length);
            }
            //2.交换堆顶与“末尾”元素位置，调整堆结构           
            for (int i = arr.Length - 1; i > 0; i--)
            {
                Swap(arr, 0, i);               
                HeapSort(arr, 0, i);
            }
            return arr;
        }
        private void HeapSort(int[] arr, int index,int len)
        {
            var left = 2 * index + 1;
            var right = 2 * index + 2;
            var largest = index;
            if (left < len && arr[left] > arr[largest])
                largest = left;
            if (right <len && arr[right] > arr[largest])
                largest = right;
            if (index != largest)
            {
                Swap(arr, index, largest);
                HeapSort(arr, largest, len);
            }
        }


    }
}
