# Next Lexicographical Sequence

## Hook

Make the sequence just bigger, then make the suffix as small as possible.

## When to Use

- Need the next arrangement in dictionary order.
- Need to modify the list in place.
- If no bigger arrangement exists, wrap to the smallest arrangement.

## Problem

Find the next bigger sequence.

```text
[1, 2, 3] -> [1, 3, 2]
[1, 3, 2] -> [2, 1, 3]
[2, 3, 1] -> [3, 1, 2]
[3, 2, 1] -> [1, 2, 3]
```

## Core Idea

Use three pieces.

```text
pivot     = rightmost position we can increase
candidate = smallest bigger value on the right
suffix    = made smallest by reversing
```

Algorithm:

```text
1. Scan from right to find first nums[i] < nums[i + 1].
2. If no pivot exists, reverse the whole list.
3. Scan from right to find first nums[j] > nums[i].
4. Swap nums[i] and nums[j].
5. Reverse everything after i.
```

## Diagram

```text
nums = [1, 3, 2]

scan from right:
3 < 2 ? no
1 < 3 ? yes

pivot = 1

right side = [3, 2]
smallest bigger than 1 = 2

swap:
[2, 3, 1]

reverse suffix:
[2, 1, 3]
```

## Dry Run

```text
nums = [2, 3, 1]
```

| step | result |
|---|---|
| pivot | `2`, because `2 < 3` |
| candidate | `3`, smallest bigger value on the right |
| swap | `[3, 2, 1]` |
| reverse suffix | `[3, 1, 2]` |

answer = [3, 1, 2]

## C#

```csharp
public void NextPermutation(List<int> nums)
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
```

## Traps

**1. Assuming pivot is index `0`**
```text
[1, 2, 3] pivot is 2 at index 1.
[1, 3, 2] pivot is 1 at index 0.
```

Pivot must be found from the right.

**2. Candidate must be bigger than pivot**
```csharp
while (nums[j] <= nums[i]) j--;
```

This stops at the first value from the right that is bigger than pivot.

**3. Pivot and candidate are different**
```text
pivot = position to increase
candidate = value used to increase it
```

**4. Reverse suffix, not whole list after every swap**
```text
After swapping, the suffix must become the smallest possible order.
```

**5. No pivot means full reverse**
```text
[3, 2, 1] is already biggest, so answer is [1, 2, 3].
```

## Flashcards

Q: What is the pivot?
A: The rightmost index where `nums[i] < nums[i + 1]`.

Q: What is the candidate?
A: The smallest bigger value on the right side of the pivot.

Q: Why scan from the right for the pivot?
A: To change the sequence as late as possible, giving the next bigger sequence.

Q: Why reverse the suffix?
A: To make everything after the pivot as small as possible.

Q: Time / space?
A: O(n) / O(1).
