# Largest Container

## Hook

Width shrinks every move, so move the shorter wall.

## When to Use

- Need the largest area between two vertical lines.
- Area depends on two endpoints.
- Brute force would check every pair.

## Problem

Find the largest water container.

```text
heights = [1, 8, 6, 2, 5, 4, 8, 3, 7]
answer = index 1 and index 8
       = min(8, 7) * (8 - 1)
       = 7 * 7
       = 49
```

## Core Idea

Area uses the shorter wall and the distance between indexes.

```text
area = min(heights[left], heights[right]) * (right - left)
```

After each check, move the shorter wall.

```text
left shorter  -> left++
right shorter -> right--
equal height  -> move either one, but one pointer must move
```

## Diagram

```text
heights = [1, 8, 6, 2, 5, 4, 8, 3, 7]
           ^                       ^
           L                       R

area = min(1, 7) * 8 = 8
move L because 1 is shorter

heights = [1, 8, 6, 2, 5, 4, 8, 3, 7]
              ^                    ^
              L                    R

area = min(8, 7) * 7 = 49
```

## Dry Run

```text
heights = [2, 4, 1, 3]
```

| left | right | area | action |
|---:|---:|---:|---|
| 2 | 3 | min(2, 3) * 3 = 6 | move left |
| 4 | 3 | min(4, 3) * 2 = 6 | move right |
| 4 | 1 | min(4, 1) * 1 = 1 | move right |

answer = 6

## C#

```csharp
public int MaxArea(List<int> heights)
{
    int left = 0;
    int right = heights.Count - 1;
    int maxArea = 0;

    while (left < right)
    {
        int width = right - left;
        int height = Math.Min(heights[left], heights[right]);
        int area = width * height;

        maxArea = Math.Max(maxArea, area);

        if (heights[left] < heights[right])
        {
            left++;
        }
        else
        {
            right--;
        }
    }

    return maxArea;
}
```

## Traps

**1. Wrong width formula**
```csharp
int width = right = left;  // wrong: assigns left into right
int width = right - left;  // correct
```

**2. `MaxArea` vs `maxArea`**
```text
C# is case-sensitive.
```

**3. Moving both pointers**
```csharp
left++;
right--;
```

This can skip the best answer. Move only the shorter wall.

**4. Two separate `if` statements**
```csharp
if (heights[left] > heights[right]) right--;
if (heights[right] > heights[left]) left++;
```

Equal heights move neither pointer. Use `if/else`.

**5. Equal heights**
```text
If heights are equal, either pointer can move.
The width shrinks either way; the important rule is that one pointer must move.
```

## Flashcards

Q: Formula?
A: `min(heights[left], heights[right]) * (right - left)`.

Q: Which pointer moves?
A: The shorter wall.

Q: Why move the shorter wall?
A: Width always shrinks, so the only hope is to find a taller limiting wall.

Q: What if both heights are equal?
A: Move either side; one pointer must move to avoid an infinite loop.

Q: Time / space?
A: O(n) / O(1).
