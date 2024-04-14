using System.Collections.Generic;

public static class BinarySearch
{
    public static bool Search(List<string> list, string target)
    {
        int left = 0;
        int right = list.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int comparison = string.Compare(list[mid], target);

            if (comparison < 0)
            {
                left = mid + 1;
            }
            else if (comparison > 0)
            {
                right = mid - 1;
            }
            else
            {
                return true;
            }
        }

        return false;
    }
}
