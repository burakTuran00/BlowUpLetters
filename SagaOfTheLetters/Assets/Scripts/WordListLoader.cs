using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class WordListLoader : MonoBehaviour
{
    public List<string> words;
    public List<string> subWords;
    public List<string> findedWords;
    [SerializeField] private string path;
    
    void Start()
    {
        //path = "D:\\Projects\\Improving_Projects\\FruitNinja\\Assets\\Resource\\wordListMIT.txt";
        string filePath = Path.Combine(Application.streamingAssetsPath, path);

        if (File.Exists(filePath))
        {
            words = new List<string>();
            string[] columns = File.ReadAllLines(filePath);

            foreach (string column in columns)
            {
                string[] columnWords = column.Trim().Split(' ');
                
                foreach (string words in columnWords)
                {
                    this.words.Add(words.ToUpper());
                }
            }
        }
        else
        {
            Debug.LogError("File doesn't exist: " + filePath);
        }

        QuickSort.Sort(words, 0, words.Count- 1);
    }

    public int GetPositionOfSentence(string target)
    {
        int left = 0;
        int right = words.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int comparison = string.Compare(words[mid], target);

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
                return mid;
            }
        }

        return -1;
    }

    public List<string> GetAllSubstrings(string str)
    {
        // aynı kelimeyi denerse başarısız olsun.
        subWords.Clear();

        for (int i = 0; i < str.Length; i++)
        {
            for (int j = i + 1; j <= str.Length; j++)
            {
                subWords.Add(str.Substring(i, j - i));
            }
        }

        return subWords;
    }

    public void AddFindedWord(string sentence)
    {
        findedWords.Add(sentence);
        QuickSort.Sort(findedWords, 0 , findedWords.Count - 1);
    }

    public void RemoveAtSentence(string sentence)
    {
        words.RemoveAt(GetPositionOfSentence(sentence));
    }
}
