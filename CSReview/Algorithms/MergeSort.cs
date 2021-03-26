using Xunit;

namespace CSReview.Algorithms
{
    public class MergeSort
    {
        public static void Sort(int[] arr)
        {
            MergeSortInner(arr, 0, arr.Length - 1);
        }

        private static void MergeSortInner(int[] arr, int left, int right)
        {
            if (left < right)
            {
                var med = (left + right) / 2;
                MergeSortInner(arr, left, med);
                MergeSortInner(arr, med + 1, right);
                Merge(arr, left, med, right);
            }
        }

        private static void Merge(int[] arr, int left, int med, int right)
        {
            var leftSize = med - left + 1;
            var rightSize = right - med;
            var leftArr = new int[leftSize];
            var rightArr = new int[rightSize];

            int i = 0;
            for (; i < leftSize; i++)
            {
                leftArr[i] = arr[left + i];
            }

            int j = 0;
            for (j = 0; j < rightSize; j++)
            {
                rightArr[j] = arr[med + 1 + j];
            }

            i = j = 0;
            var k = left;
            for (; i < leftSize && j < rightSize; k++)
            {
                if (leftArr[i] < rightArr[j])
                {
                    arr[k] = leftArr[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArr[j];
                    j++;
                }
            }

            while (i < leftSize)
            {
                arr[k] = leftArr[i];
                k++;
                i++;
            }
            
            while (j < rightSize)
            {
                arr[k] = rightArr[j];
                k++;
                j++;
            }
        }

        [Fact]
        public void Tests()
        {
            var arr = new[]
            {
                1, 5, 125, 457, 23, 4, 213, 4364, 3, 213, 4613, 63, 3245, 2, 56, 54, 21, 5, 345, 36, 3, 12, 343, 2,
                333, 333
            };
            Sort(arr);
            for (int i = 1; i < arr.Length; i++)
            {
                Assert.True(arr[i] >= arr[i - 1]);
            }
        }
    }
}