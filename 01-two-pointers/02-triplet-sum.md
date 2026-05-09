# Triplet Sum

## Hook

Sort, fix one number, then do Pair Sum on the rest.

## When to Use

- Need 3 numbers that hit a target, usually `0`.
- Order in the answer does not matter, so sorting is allowed.
- Duplicate answers must be avoided.

## Problem

Return all unique triplets where `a + b + c = 0`.

```text
nums   = [-1, 0, 1, 2, -1, -4]
sorted = [-4, -1, -1, 0, 1, 2]
answer = [[-1, -1, 2], [-1, 0, 1]]
         -1 + -1 + 2 = 0
         -1 +  0 + 1 = 0
```

## Core Idea

Fix one number. The other two must make the opposite value.

```text
a + b + c = 0
fix a -> b + c = -a
```

Then use Pair Sum with `left` and `right`.

## Diagram

```text
sorted = [-4, -1, -1, 0, 1, 2]

i fixed, left/right search after i:

[-4, -1, -1, 0, 1, 2]
      ^   ^          ^
      i   L          R
```

## Dry Run

```text
sorted = [-4, -1, -1, 0, 1, 2]
```

| fixed | left | right | sum | action |
|---:|---:|---:|---:|---|
| -4 | -1 | 2 | -3 | too small -> left++ |
| -4 | -1 | 2 | -3 | too small -> left++ |
| -4 | 0 | 2 | -2 | too small -> left++ |
| -1 | -1 | 2 | 0 | add [-1, -1, 2] |
| -1 | 0 | 1 | 0 | add [-1, 0, 1] |

## C#

```csharp
public List<List<int>> TripletSum(List<int> nums)
{
    nums.Sort();
    var result = new List<List<int>>();

    for (int i = 0; i < nums.Count - 2; i++)
    {
        if (i > 0 && nums[i] == nums[i - 1]) continue;   // skip duplicate fixed

        int left  = i + 1;
        int right = nums.Count - 1;

        while (left < right)
        {
            int sum = nums[i] + nums[left] + nums[right];

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
```

## Traps

**1. Pointer index vs value**
```text
left = -1                  // wrong - left is an index
left = 2,  nums[left] = -1 // correct
```

**2. Fixed only the first element**
```csharp
int fixedNumber = nums[0];   // wrong - only checks triplets with nums[0]
for (int i = 0; i < nums.Count - 2; i++) { ... }   // correct
```

**3. Wrong list creation syntax**
```csharp
result.Add(new List<int> num = { nums[left], nums[right] });    // wrong
result.Add(new List<int> { nums[i], nums[left], nums[right] }); // correct
```

**4. Moved pointers outward after a hit**
```csharp
left--; right++;   // wrong
left++; right--;   // correct
```

**5. Forgot duplicate skipping** at `i`, `left`, or `right` -> returns repeated triplets.

**6. Used `left > right` inside duplicate skipping**
```csharp
while (left > right && nums[left] == nums[left - 1]) left++; // wrong
while (left < right && nums[left] == nums[left - 1]) left++; // correct
```

`left < right` means the search is still valid.

**7. Used `if` instead of `while` for duplicate blocks**
```text
if    -> skips one duplicate
while -> skips the whole duplicate block
```

## Flashcards

Q: Key trick?
A: Sort, fix one number, run Pair Sum on the rest.

Q: Why sort first?
A: Pair Sum needs the sorted movement rule.

Q: Why `left = i + 1`?
A: Pair search must start after the fixed index so positions never overlap.

Q: After a hit, which pointers move?
A: Both inward - `left++` and `right--`.

Q: Three places to skip duplicates?
A: `i` (start of for), `left` and `right` (after a hit).

Q: Time / space?
A: O(n^2) time / O(1) extra space, not counting the result.
