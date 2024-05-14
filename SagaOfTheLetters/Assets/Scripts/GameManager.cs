using System;
using System.Collections.Generic;
using UnityEngine;

public  class GameManager : MonoBehaviour
{
    public  event Action<char> OnSentenceAddedEvent;

    #region Managers
    [SerializeField] private WordManager wordManager;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private Blade blade;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Timer timer;
    [SerializeField] private PositionAdjuster positionAdjuster;
    #endregion

    #region Variables
    private int score = 0;
    public string sentence{ get; set;}
    private int currentSentenceScore;
    private int totalAmountOfTimeToAdd;
    private string AllFindedWordText;
    private const int MAX_CHAR_NUMBER = 11;
    #endregion

    #region  Singleton
    
    public static GameManager Instance;
    private void Awake() 
    {
        DontDestroyOnLoad(this);
        
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    public void StartGame()
    {
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        sentence = "";
        uIManager.SetWordText(sentence);

        timer.Pause = false;
        spawner.Pause = false;

        timer.StartTimer();
        spawner.StartSpawner();

        score = 0;
    }

    public void AddSentence(char value) 
    {  
        sentence += value;
        BlastEffect();

        if(BinarySearch.Search(wordManager.words, sentence))
        {
            wordManager.GetAllSubstrings(sentence);

            foreach(string subword in wordManager.subWords)
            {
                if (BinarySearch.Search(wordManager.words, subword) && !BinarySearch.Search(wordManager.findedWords, subword))
                {
                    totalAmountOfTimeToAdd += Mathf.RoundToInt(subword.Length); // todo: find a proper time calculation.
                }
                else if(BinarySearch.Search(wordManager.findedWords, subword))
                {
                    totalAmountOfTimeToAdd += Mathf.RoundToInt(subword.Length / 2); // todo: find a proper time calculation.
                }
            }

            wordManager.RemoveAtSentence(sentence);
            wordManager.AddFindedWord(sentence);

            timer.getMoreTime(Mathf.RoundToInt(totalAmountOfTimeToAdd * 2));
            sentence = "";
            score++;
        }

        uIManager.SetColorOfWordText((BinarySearch.Search(wordManager.findedWords, sentence)) ? Color.yellow : Color.white);
        
        if(sentence.Length > MAX_CHAR_NUMBER)
        {
            GetRidOfSentence();
        }

        uIManager.SetWordText(sentence);
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
        if(GetRemainingTime() > 0)
        {
            return;
        }

        Pooler.StopPooler();
        wordManager.ShowAllFindedWordToText(ref AllFindedWordText, score);
        uIManager.SetFindedText(AllFindedWordText);

        StopAllCoroutines();
        Time.timeScale = 1f;
        blade.enabled = false;
    }

    public void BlastEffect()
    {
        blade.BlastEffect();
    }

    public Transform SetRandomPosition()
    {
        return positionAdjuster.RandomPosition();
    }

    public int GetRemainingTime()
    {
        return timer.getRemainingTime();
    }

    public void SetConditionOfStartGameText(bool condition)
    {
        uIManager.SetFirstStartCondition(condition);
    }

    public void PauseGame(bool condition)
    {
        timer.Pause = condition;
        spawner.Pause = condition;
    }
}
