# Pair Sum - Sorted

## Hook

Too small -> left++. Too big -> right--.

## When to Use

- Input is sorted.
- Need two different values that match a sum or difference rule.
- Nested loops would check every pair.

## Problem

Find whether two values add up to the target.

```text
nums   = [1, 3, 4, 5, 7, 11]
target = 9
answer = 4 + 5 = 9 -> true
```

## Core Idea

Sorted order tells which side to move.

```text
sum < target  -> need bigger  -> left++
sum > target  -> need smaller -> right--
sum == target -> done
```

## Diagram

```text
nums = [1, 3, 4, 5, 7, 11]

start:
[1, 3, 4, 5, 7, 11]
 ^              ^
 L              R

after right--:
[1, 3, 4, 5, 7, 11]
 ^           ^
 L           R

found:
[1, 3, 4, 5, 7, 11]
       ^  ^
       L  R
```

## Dry Run

```text
nums = [1, 3, 4, 5, 7, 11]   target = 9
```

| left | right | sum | action |
|---:|---:|---:|---|
| 1 | 11 | 12 | too big -> right-- |
| 1 | 7 | 8 | too small -> left++ |
| 4 | 5 | 9 | found |

## C#

```csharp
public bool PairSum(List<int> nums, int target)
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
```

Variants change only the return:

```csharp
return new int[] { left, right };               // indexes: { -1, -1 } on miss
return new int[] { nums[left], nums[right] };   // values:  { } on miss
```

## Traps

**1. `.Length` on `List<int>`** -> use `.Count`. Arrays use `.Length`.

**2. Combined declaration**
```csharp
int left = 0, int right = nums.Count - 1;   // wrong
int left = 0;
int right = nums.Count - 1;                 // correct
```

**3. `num[left]` vs `nums[left]`** - identifiers are exact.

**4. Blind `for` loop** - pointer scan needs `while (left < right)`.

**5. `left <= right` allows the same element twice**
```text
nums = [1, 5], target = 10 -> 5 + 5 is invalid because there is only one 5.
```

## Flashcards

Q: When does this pattern apply?
A: Sorted input + need a pair satisfying a sum/difference rule.

Q: sum < target - which pointer?
A: left++ (need bigger).

Q: sum > target - which pointer?
A: right-- (need smaller).

Q: Why `left < right` not `<=`?
A: Avoids using the same element twice.

Q: Time / space?
A: O(n) / O(1).
