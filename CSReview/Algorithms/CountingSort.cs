using Xunit;

namespace CSReview.Algorithms
{
    public class CountingSort
    {
        public static int[] Sort(int[] nums, int k)
        {
            var counts = new int[k];
            for (int i = 0; i < nums.Length; i++)
            {
                counts[nums[i] - 1]++;
            }

            for (int i = 1; i < k; i++)
            {
                counts[i] += counts[i - 1];
            }

            var res = new int[nums.Length];
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                res[counts[nums[i] - 1] - 1] = nums[i];
                counts[nums[i] - 1]--;
            }

            return res;
        }

        [Fact]
        public void Test()
        {
            var toSort = new[] {5, 2, 14, 566, 12, 5, 20, 14, 33, 14, 50};
            var res = CountingSort.Sort(toSort, 566);
            Assert.Equal(new []{2, 5, 5, 12, 14, 14, 14, 20, 33, 50, 566}, res);
        }
    }
}