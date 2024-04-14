using System;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager : MonoBehaviour
{
    public  event Action<char> OnSentenceAddedEvent;

    #region Variables
    //[SerializeField] private Text scoreText;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private Blade blade;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Timer timer;
    [SerializeField] private WordListLoader wordListLoader;
    [SerializeField] private PositionAdjuster positionAdjuster;
    private int score = 0;
    public string sentence{ get; set;}
    private int currentSentenceScore;
    private List<string> subList;
    private int totalAmountOfTimeToAdd;
    private string findenWordText;
    #endregion

    #region  Singleton

    public static GameManager Instance;
    private void Awake() 
    {
        Instance = this;
    }
    
    #endregion
    
    private void Start()
    {
        StartGame();
    }
    
    public void StartGame()
    {
        subList = new List<string>();

        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
    }

    public void AddSentence(char value) // needs some performing thins
    {  
        sentence += value;
        blade.BlastEffect();

        if(BinarySearch.Search(wordListLoader.words, sentence))
        {
            subList.Clear();
            subList = wordListLoader.GetAllSubstrings(sentence);

            foreach(string subword in subList)
            {
                if (BinarySearch.Search(wordListLoader.words, subword) && !BinarySearch.Search(wordListLoader.findedWords, subword))
                {
                    totalAmountOfTimeToAdd += Mathf.CeilToInt(subword.Length); // todo: find a proper time calculation.
                }
                else if(BinarySearch.Search(wordListLoader.findedWords, subword))
                {
                    totalAmountOfTimeToAdd += Mathf.FloorToInt(subword.Length / 2); // todo: find a proper time calculation.
                }
            }

            wordListLoader.RemoveAtSentence(sentence);
            wordListLoader.AddFindedWord(sentence);

            timer.getMoreTime(totalAmountOfTimeToAdd);

            totalAmountOfTimeToAdd = 0;
            sentence = "";
        }

        uIManager.SetColorOfWordText((BinarySearch.Search(wordListLoader.findedWords, sentence)) ? Color.yellow : Color.white);
        
        if(sentence.Length > 10)
        {
            GetRidOfSentence();
        }

        //uIManager.SetWordText(sentence);
        uIManager.WordText.text = sentence.ToUpper().ToString();
        totalAmountOfTimeToAdd = 0;
    }

    public void GetRidOfSentence()
    {
        if(sentence != "")
        {
            sentence = "";
        }

        uIManager.SetWordText(sentence);
    }

    public void RemoveLastCharacterAtSentence()
    {
        // This structure doesn't work properly.

        if(sentence != null && sentence != "")
        {
            sentence = sentence.Remove(sentence.Length - 1); // remove last char.
            uIManager.SetWordText(sentence);
        }
        else
        {
            return;
        }
    }
    public void GameOver()
    {
        if(timer.getRemainingTime() > 0)
        {
            return;
        }

        Pooler.StopPooler();

        foreach(string sentence in wordListLoader.findedWords)
        {
            findenWordText += sentence + "\n";
        }
        
        uIManager.SetFindedText(findenWordText);
        Time.timeScale = 0f;
    }

    public void BlastEffect()
    {

    }
    public Transform SetRandomPosition()
    {
        return positionAdjuster.RandomPosition();
    }

    public int GetRemainingTime()
    {
        return timer.getRemainingTime();
    }
}
