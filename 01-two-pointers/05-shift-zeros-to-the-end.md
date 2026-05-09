# Shift Zeros to End

## Hook

`read` finds non-zero values; `write` places them at the front.

## When to Use

- Need to move unwanted values to the end.
- Need to keep the order of wanted values.
- Need to modify the same list in place.

## Problem

Move all zeros to the end while keeping non-zero order.

```text
nums   = [0, 1, 0, 3, 12]
answer = [1, 3, 12, 0, 0]
```

## Core Idea

Use same-direction pointers.

```text
read  -> scans every value
write -> next position for a non-zero value
```

`read` moves every loop. `write` moves only when a non-zero is placed.

## Diagram

```text
nums = [0, 1, 0, 3, 12]

start:
[0, 1, 0, 3, 12]
 ^
 R/W

read sees 1:
[0, 1, 0, 3, 12]
 ^  ^
 W  R

swap into write:
[1, 0, 0, 3, 12]
    ^
    W
```

## Dry Run

```text
nums = [0, 1, 0, 3, 12]
```

| read value | write | action | nums |
|---:|---:|---|---|
| 0 | 0 | ignore | `[0, 1, 0, 3, 12]` |
| 1 | 0 | swap, write++ | `[1, 0, 0, 3, 12]` |
| 0 | 1 | ignore | `[1, 0, 0, 3, 12]` |
| 3 | 1 | swap, write++ | `[1, 3, 0, 0, 12]` |
| 12 | 2 | swap, write++ | `[1, 3, 12, 0, 0]` |

## C#

```csharp
public void MoveZeroes(List<int> nums)
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
```

## Traps

**1. `.Length` on `List<int>`**
```csharp
nums.Length // wrong
nums.Count  // correct
```

**2. Adjacent swap leaves zeros stuck**
```text
[0, 1, 0, 3, 12]
one-pass adjacent swaps can produce [1, 0, 3, 12, 0]
```

Non-zero values may need to jump across more than one zero.

**3. `right` can go out of range**
```text
valid indexes for 5 items: 0..4
right can become 5 if the loop only checks left < Count
```

**4. We do not directly move zeros**
```text
We pull non-zero values forward.
Zeros end up behind them.
```

## Flashcards

Q: What does `read` do?
A: Scans every value.

Q: What does `write` do?
A: Marks where the next non-zero belongs.

Q: When does `write` move?
A: Only after placing a non-zero.

Q: Why is order preserved?
A: `read` sees non-zero values from left to right, and `write` places them in that same order.

Q: Time / space?
A: O(n) / O(1).
