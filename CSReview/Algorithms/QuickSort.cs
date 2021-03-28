using System;
using Xunit;

namespace CSReview.Algorithms
{
    public class QuickSort
    {
        public static void Sort(int[] arr)
        {
            SortInner(arr, 0, arr.Length - 1);
        }

        public static void RandomizedSort(int[] arr)
        {
            RandomizedSortInner(arr, 0, arr.Length - 1);
        }

        private static void RandomizedSortInner(int[] arr, int left, int right)
        {
            if (left < right)
            {
                // var pivot = LomutoPartition(arr, left, right);
                // SortInner(arr, left, pivot - 1);

                var pivot = RandomizedHoarePartition(arr, left, right);
                RandomizedSortInner(arr, left, pivot - 1);

                RandomizedSortInner(arr, pivot, right);
            }
        }

        private static void SortInner(int[] arr, int left, int right)
        {
            if (left < right)
            {
                // var pivot = LomutoPartition(arr, left, right);
                // SortInner(arr, left, pivot - 1);

                var pivot = HoarePartition(arr, left, right);
                SortInner(arr, left, pivot - 1);

                SortInner(arr, pivot, right);
            }
        }

        private static int LomutoPartition(int[] arr, int left, int right)
        {
            var pivot = arr[right];
            var i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, right);
            return i + 1;
        }

        private static int RandomizedHoarePartition(int[] arr, int left, int right)
        {
            var p = new Random().Next(left, right);
            Swap(arr, p, right);
            return HoarePartition(arr, left, right);
        }

        private static int HoarePartition(int[] arr, int left, int right)
        {
            var pivot = arr[right];
            var i = left - 1;
            var j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                } while (arr[i] < pivot);

                do
                {
                    j--;
                } while (arr[j] > pivot);

                if (j <= i) return i;

                Swap(arr, i, j);
            }
        }

        private static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        [Fact]
        public void Test()
        {
            var rand = new Random();
            var numCount = 1000000;
            var arr = new int[numCount];
            for (int i = 0; i < numCount; i++)
            {
                // arr[i] = rand.Next();
                arr[i] = i;
            }

            //Sort(arr);
            RandomizedSort(arr);
            for (int i = 1; i < numCount; i++)
            {
                Assert.True(arr[i] >= arr[i - 1]);
            }
        }
    }
}