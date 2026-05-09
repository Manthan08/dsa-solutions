# Valid Palindrome

## Hook

Palindrome is true until a real mismatch proves false.

## When to Use

- Need to check whether text reads the same from both ends.
- Spaces, punctuation, and casing may need to be ignored.
- Reversing the whole string would use extra memory.

## Problem

Return whether the string is a valid palindrome after ignoring non-alphanumeric characters and casing.

```text
s = "A man, a plan, a canal: Panama"
answer = "amanaplanacanalpanama" reads the same both ways -> true

s = "race a car"
answer = "raceacar" does not read the same both ways -> false
```

## Core Idea

Move inward from both ends.

```text
invalid left  -> left++
invalid right -> right--
valid pair mismatch -> false
valid pair match    -> left++, right--
```

If the pointers meet or cross, no mismatch was found.

## Diagram

```text
s = "A man, a plan, a canal: Panama"

A ... a
^     ^
L     R

compare lowercase:
a == a -> move both

skip spaces and punctuation before comparing again
```

## Dry Run

```text
s = "a,"
```

| left | right | action |
|---|---|---|
| `a` | `,` | right is invalid -> right-- |
| `a` | `a` | pointers met -> true |

## C#

```csharp
public bool IsValidPalindrome(string s)
{
    int left = 0;
    int right = s.Length - 1;
    string lowerString = s.ToLowerInvariant();

    while (left < right)
    {
        while (left < right && !char.IsLetterOrDigit(lowerString[left]))
        {
            left++;
        }

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
```

## Traps

**1. Starting with `false`**
```text
"a" and "" are valid palindromes.
Palindrome should return true unless a mismatch proves false.
```

**2. Comparing `IsLetterOrDigit` results**
```csharp
char.IsLetterOrDigit('a') == char.IsLetterOrDigit('b') // true == true
```

This checks whether both are valid characters, not whether both characters match.

**3. Skipping only when both sides are invalid**
```text
"a," should be true.
left is valid, right is invalid, so each side must be skipped independently.
```

**4. Missing `!` in skip loops**
```csharp
while (char.IsLetterOrDigit(s[left])) left++;  // wrong: skips valid letters
while (!char.IsLetterOrDigit(s[left])) left++; // correct: skips junk
```

## Flashcards

Q: What is the failure condition?
A: A valid left character and valid right character do not match after casing is normalized.

Q: Why use `while` to skip junk?
A: There can be many spaces or punctuation characters in a row.

Q: Why does `",,"` return true?
A: All characters are skipped; no real mismatch exists.

Q: Time / space?
A: O(n) time / O(1) extra space, ignoring the lowercase string. Use direct `char.ToLowerInvariant(...)` to avoid that extra string.
