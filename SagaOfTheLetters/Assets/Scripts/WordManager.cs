using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordManager : MonoBehaviour
{
    #region Variables
    public List<string> words;
    public List<string> subWords;
    public List<string> findedWords;
    [SerializeField] private string path;
    #endregion

    private void Start() 
    {
        LoadList();    
    }

    public void LoadList()
    {
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
        return BinarySearch.SearchPositionOfSentence(words, target);
    }

    public void GetAllSubstrings(string str)
    {
        subWords.Clear();

        for (int i = 0; i < str.Length; i++)
        {
            for (int j = i + 1; j <= str.Length; j++)
            {
                subWords.Add(str.Substring(i, j - i));
            }
        }
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

    public void ShowAllFindedWordToText(ref string AllFindedWordTex, int score)
    {
        foreach(string word in findedWords)
        {
            AllFindedWordTex += word + "\n";
        }

        AllFindedWordTex += "\nTotal Score:" + score.ToString();
    }
}
