# Two Pointers Summary

## Hook

Two indexes + one clear movement rule.

## Quick Summary

- A pointer is just an index or position.
- Two pointers let you compare two places at the same time.
- Brute force compares many pairs with nested loops.
- Two pointers work when movement is predictable.
- Sorted arrays are predictable: move `left` right -> bigger, move `right` left -> smaller.

## Diagram

```text
1. Inward traversal

[1, 3, 4, 5, 7, 11]
 ^              ^
 L              R

L moves right when we need bigger.
R moves left when we need smaller.


2. Same direction traversal

[0, 1, 0, 3, 12]
 ^  ^
 W  R

R scans every value.
W marks where the next useful value goes.


3. Staged traversal

[1, 2, 4, 3]
       ^
       scan first, then fix the suffix
```

## Cheat Sheet

| Situation | Pointer Movement | Remember |
|---|---|---|
| Pair in sorted array | `left` starts first, `right` starts last | too small -> `left++`, too big -> `right--` |
| Triplet sum | fix `i`, then pair-sum with `left/right` | skip duplicates at `i`, `left`, `right` |
| Palindrome | compare both ends inward | skip junk, mismatch -> false |
| Largest container | compare both ends inward | move the shorter wall |
| Shift zeros | both move forward | one finds, one places |
| Next sequence | scan from right, then reverse suffix | pivot, candidate, smallest suffix |

## Topic Index

| # | Topic | Status |
|---|---|---|
| 01 | [Pair Sum - Sorted](01-pair-sum-sorted.md) | done |
| 02 | [Triplet Sum](02-triplet-sum.md) | done |
| 03 | [Valid Palindrome](03-is-palindrome-valid.md) | done |
| 04 | [Largest Container](04-largest-container.md) | done |
| 05 | [Shift Zeros to End](05-shift-zeros-to-the-end.md) | done |
| 06 | [Next Lexicographical Sequence](06-next-lexicographical-sequence.md) | done |

Code: [01-common-interview-question.cs](01-common-interview-question.cs)
