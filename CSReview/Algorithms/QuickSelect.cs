using Xunit;

namespace CSReview.Algorithms
{
    public static class QuickSelect
    {
        public static int Recursive(int[] arr, int k)
        {
            return RecursiveImpl(arr, k, 0, arr.Length - 1);
        }

        private static int RecursiveImpl(int[] arr, int k, int left, int right)
        {
            if (left == right) return arr[left];

            var pivot = HoarePartition(arr, left, right);

            if (k <= pivot) return RecursiveImpl(arr, k, left, pivot - 0);
            return RecursiveImpl(arr, k, pivot + 1, right);
        }

        public static int Iterative(int[] arr, int k)
        {
            var left = 0;
            var right = arr.Length - 1;
            while (left < right)
            {
                var pivot = HoarePartition(arr, left, right);

                if (pivot == k) return arr[pivot];

                if (k > pivot) left = pivot + 1;

                if (k < pivot)
                {
                    right = pivot - 1;
                }
            }

            return arr[left];
        }

        private static int LomutoPartition(int[] arr, int left, int right)
        {
            var pivot = arr[right];
            var i = left - 1;
            for (int j = left; j <= right - 1; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, right);
            return i + 1;
        }

        private static int HoarePartition(int[] arr, int left, int right)
        {
            var pivot = arr[left];
            var i = left - 1;
            var j = right + 1;
            while (true)
            {
                do {
                    i++;
                } while (arr[i] < pivot);

                do {
                    j--;
                } while (arr[j] > pivot);

                if (i >= j)
                {
                    return j;
                }

                Swap(arr, i, j);
            }
         
            
        }

        private static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        
        [Theory]
        [InlineData(new[] {1, 5, 7, 8, 2, 9, 3, 4, 10, 6}, 2, 3)]
        [InlineData(new[] {1, 5, 7, 8, 2, 9, 3, 4, 10, 6}, 1, 2)]
        [InlineData(new[] {1, 5, 7, 8, 2, 9, 3, 4, 10, 6}, 0, 1)]
        public static void IsCorrect(int[] arr, int k, int expected)
        {
            var recursiveResult = QuickSelect.Recursive(arr, k);
            Assert.Equal(expected, recursiveResult);
            var iterativeResult = QuickSelect.Iterative(arr, k);
            Assert.Equal(recursiveResult, iterativeResult);
        }
    }

    
}