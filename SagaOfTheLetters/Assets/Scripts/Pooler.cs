using System.Collections.Generic;
using UnityEditor.Media;
using UnityEngine;
public class Pooler : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int poolSize;
    [SerializeField] private bool expandable;
    public static List<GameObject> freeList;
    public static  List<GameObject> usedList;
    public static int counter = 0;  
    private const int LETTER_COUNT = 26;
    #endregion
    
    private void OnEnable() 
    {
        freeList = new List<GameObject>();
        usedList = new List<GameObject>();

        for(int i = 0; i < poolSize; i++)
        {
            GenerateNewObject();
        }    
    }

    public GameObject GetObject()
    {
        if(freeList.Count == 0 && !expandable)
        {
            return null;
        }
        else if(freeList.Count == 0)
        {
            GenerateNewObject();
        }
    
        GameObject g = freeList[freeList.Count - 1];
        freeList.RemoveAt(freeList.Count - 1);
        usedList.Add(g);
        return g;
    }

    public static void ReturnObject(GameObject obj)
    {
        //Debug.Assert(usedList.Contains(obj));
        obj.SetActive(false);
        usedList.Remove(obj);
        freeList.Add(obj);
    }

    private void GenerateNewObject()
    {
        for(int i = 0; i < prefabs.Length; i++)
        {
            GameObject g = Instantiate(prefabs[i]);
            g.SetActive(false);

            if(g != null)
            {
                g.transform.parent = transform;
                freeList.Add(g);
            }      
        }
    }

    public  int GetAllObjectCount()
    {
        return prefabs.Length * poolSize;
    }

    public static void ReUse(GameObject gameObject)
    {   
        ReturnObject(gameObject);
        counter++;

        if(counter >= LETTER_COUNT)
        {
            for(int i = 0; i < freeList.Count; i++)
            {
                freeList[i].SetActive(true);
            }
            counter = 0;
        }
     
    }

    public static void StopPooler()
    {
        #region Performing Loop
        // Less garbage creating 
        foreach(GameObject used in usedList)
        {
            used.SetActive(false);
        }

        foreach(GameObject free in freeList)
        {
            free.SetActive(false);
        }

        #endregion
    }
}
