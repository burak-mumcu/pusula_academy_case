public static class MaxIncreasingSubarray
{
    public static int[] Solve(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return Array.Empty<int>();

        int start = 0, bestStart = 0, bestLength = 1, length = 1;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > nums[i - 1])
            {
                length++;
                if (length > bestLength)
                {
                    bestLength = length;
                    bestStart = start;
                }
            }
            else
            {
                start = i;
                length = 1;
            }
        }

        int[] result = new int[bestLength];
        Array.Copy(nums, bestStart, result, 0, bestLength);
        return result;
    }
}
