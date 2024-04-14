using System;
using System.Collections.Generic;

public static class QuickSort
{
    public static void Sort(List<string> list, int start, int end)
    {
        if (start < end)
        {
            // Partition the list and get the pivot index
            int pivotIndex = Partition(list, start, end);

            // Sort the divided lists based on the pivot index
            Sort(list, start, pivotIndex - 1);
            Sort(list, pivotIndex + 1, end);
        }
    }

    public static int Partition(List<string> list, int start, int end)
    {
        string pivot = list[end];
        int i = start - 1;

        for (int j = start; j < end; j++)
        {
            if (String.Compare(list[j], pivot) < 0)
            {
                i++;
                string temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        string temp1 = list[i + 1];
        list[i + 1] = list[end];
        list[end] = temp1;

        return i + 1;
    }
}
