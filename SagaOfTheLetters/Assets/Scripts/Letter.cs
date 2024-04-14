using System.Threading;
using UnityEditor.MPE;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [SerializeField] private char letterChar;
    [SerializeField] private int letterIndex;
    
    private void OnEnable() 
    {
       
    }
    private void OnDisable() 
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.AddSentence(letterChar);
        }

        Pooler.ReturnObject(gameObject);
        transform.position = GameManager.Instance.SetRandomPosition().position;
    }

    public char getLetterChar()
    {
        return letterChar;
    }   

    
}
