using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
#region Variables
    [SerializeField] private float minSpawnDelay = 0.25f;

    [SerializeField] private float maxSpawnDelay = 1f;

    private float delay;

    private int randomValueToSpawnCount;

    private int randomValue;

    public bool Pause { get; set; }
#endregion


    private Vector3 lastPosition;

    public void StartSpawner()
    {
        //delay = Random.Range(minSpawnDelay,maxSpawnDelay);
        delay = (minSpawnDelay + maxSpawnDelay) / 2;
        Vector3 lastPosition = GameManager.Instance.SetRandomPosition().position;

        StartCoroutine(ToSpawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Spawn()
    {
        if (!Pause)
        {
            randomValue = Random.Range(0, Pooler.freeList.Count);

            if (Pooler.freeList.Contains(Pooler.freeList[randomValue]) && Pooler.freeList.Count > 0)
            {
                Pooler.freeList[randomValue].SetActive(true);
                Pooler.freeList[randomValue].transform.position =
                    GameManager.Instance.SetRandomPosition().position;

                if (lastPosition.x == Pooler.freeList[randomValue].transform.position.x)
                {
                    if (Pooler.freeList[randomValue].transform.position.x > 7f)
                    {
                        Pooler.freeList[randomValue].transform.position -= new Vector3(2f, 0f, 0f);
                    }
                    else
                    {
                        Pooler.freeList[randomValue].transform.position += new Vector3(2f, 0f, 0f);
                    }
                }

                lastPosition = Pooler.freeList[randomValue].transform.position;
                Pooler.usedList.Add(Pooler.freeList[randomValue]);
                Pooler.freeList.RemoveAt (randomValue);
            }
        }
    }

    private IEnumerator ToSpawn()
    {
        while (GameManager.Instance.GetRemainingTime() > 0)
        {
            Spawn();
            yield return new WaitForSeconds(delay);
        }
    }

    public void StopSpawner()
    {
        StopCoroutine(nameof(ToSpawn));
        this.enabled = false;
    }
}
