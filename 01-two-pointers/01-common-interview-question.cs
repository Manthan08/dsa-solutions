// Two Pointers - runnable scratchpad for all topics in this chapter.
// Add a topic = add a method + a Main call. Keep Main small and printable.

using System;
using System.Collections.Generic;

public class TwoPointers
{
    // 01 - Pair Sum (sorted): does any pair add to target?
    public static bool PairSum(List<int> nums, int target)
    {
        int left  = 0;
        int right = nums.Count - 1;

        while (left < right)
        {
            int sum = nums[left] + nums[right];

            if (sum == target) return true;
            if (sum <  target) left++;
            if (sum >  target) right--;
        }
        return false;
    }

    // 02 - Triplet Sum: all unique triplets summing to 0.
    public static List<List<int>> TripletSum(List<int> nums)
    {
        nums.Sort();
        var result = new List<List<int>>();

        for (int i = 0; i < nums.Count - 2; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1]) {
                continue;
            }

            int fixedNumber = nums[i];
            int left  = i + 1;
            int right = nums.Count - 1;

            while (left < right)
            {
                int sum = fixedNumber + nums[left] + nums[right];

                if (sum == 0)
                {
                    result.Add(new List<int> { nums[i], nums[left], nums[right] });
                    left++;
                    right--;
                    while (left < right && nums[left]  == nums[left  - 1]) left++;
                    while (left < right && nums[right] == nums[right + 1]) right--;
                }
                if (sum < 0) left++;
                if (sum > 0) right--;
            }
        }
        return result;
    }

    // 03 - Valid Palindrome: ignore casing and non-alphanumeric characters.
    public static bool IsValidPalindrome(string s)
    {
        int left = 0;
        int right = s.Length - 1;

        string lowerString = s.ToLowerInvariant();

        while (left < right)
        {
            // to skip spaces and non alphanumeric character seperately
            while (left < right && !char.IsLetterOrDigit(lowerString[left]))
            {
                left++;
            }

            // to skip spaces and non alphanumeric character seperately
            while (left < right && !char.IsLetterOrDigit(lowerString[right]))
            {
                right--;
            }

            if (lowerString[left] != lowerString[right])
            {
                return false;
            }

            left++;
            right--;
        }

        return true;
    }

    // 04 - Largest Container: maximum water held by two height lines.
    public static int MaxArea(List<int> heights)
    {
        int left = 0;
        int right = heights.Count - 1;
        int maxArea = 0;

        while (left < right)
        {
            // calculate width
            int width = right - left ;

            // calculate height using Math.Min
            int height = Math.Min(heights[left],heights[right]);

            // calculate area
            int area = width * height;

            // update maxArea using Math.Max
            maxArea = Math.Max(area,maxArea);    

            // move shorter wall
            // if left is shorter -> move left
            // else -> move right

            if (heights[left] < heights[right])
            {
                left++;
            }
            else
            {
                right --;            
            }
        }

        return maxArea;
    }

    // 05 - Shift Zeros to End: keep non-zero order, modify the list in place.
    public static void MoveZeroes(List<int> nums)
    {
        int write = 0;

        for (int read = 0; read < nums.Count; read++)
        {
            if (nums[read] != 0)
            {
                int temp = nums[write];
                nums[write] = nums[read];
                nums[read] = temp;

                write++;
            }
        }
    }

    // 06 - Next Lexicographical Sequence: next bigger arrangement in place.
    public static void NextPermutation(List<int> nums)
    {
        int i = nums.Count - 2;

        while (i >= 0 && nums[i] >= nums[i + 1])
        {
            i--;
        }

        if (i >= 0)
        {
            int j = nums.Count - 1;

            while (nums[j] <= nums[i])
            {
                j--;
            }

            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        int left = i + 1;
        int right = nums.Count - 1;

        while (left < right)
        {
            int temp = nums[left];
            nums[left] = nums[right];
            nums[right] = temp;

            left++;
            right--;
        }
    }

    public static void Main()
    {
        // 01 - Pair Sum
        Console.WriteLine("Pair Sum:");
        Console.WriteLine(PairSum(new List<int> { 1, 3, 4, 5, 7, 11 }, 9));   // true
        Console.WriteLine(PairSum(new List<int> { 1, 3, 4, 5, 7, 11 }, 100)); // false

        // 02 - Triplet Sum
        Console.WriteLine("\nTriplet Sum:");
        var triplets = TripletSum(new List<int> { -1, 0, 1, 2, -1, -4 });
        foreach (var t in triplets)
            Console.WriteLine($"[{string.Join(", ", t)}]");
        // [-1, -1, 2]
        // [-1, 0, 1]

        // 03 - Valid Palindrome
        Console.WriteLine("\nValid Palindrome:");
        Console.WriteLine(IsValidPalindrome("racecar"));                         // true
        Console.WriteLine(IsValidPalindrome("hello"));                           // false
        Console.WriteLine(IsValidPalindrome("A man, a plan, a canal: Panama"));  // true
        Console.WriteLine(IsValidPalindrome("race a car"));                      // false

        // 04 - Largest Container
        Console.WriteLine("\nLargest Container:");
        Console.WriteLine(MaxArea(new List<int> { 1, 8, 6, 2, 5, 4, 8, 3, 7 })); // 49
        Console.WriteLine(MaxArea(new List<int> { 2, 4, 1, 3 }));                // 6

        // 05 - Shift Zeros to End
        Console.WriteLine("\nShift Zeros to End:");
        var zeros = new List<int> { 0, 1, 0, 3, 12 };
        MoveZeroes(zeros);
        Console.WriteLine($"[{string.Join(", ", zeros)}]"); // [1, 3, 12, 0, 0]

        // 06 - Next Lexicographical Sequence
        Console.WriteLine("\nNext Lexicographical Sequence:");
        var next = new List<int> { 1, 3, 2 };
        NextPermutation(next);
        Console.WriteLine($"[{string.Join(", ", next)}]"); // [2, 1, 3]

        var wrap = new List<int> { 3, 2, 1 };
        NextPermutation(wrap);
        Console.WriteLine($"[{string.Join(", ", wrap)}]"); // [1, 2, 3]
    }
}
