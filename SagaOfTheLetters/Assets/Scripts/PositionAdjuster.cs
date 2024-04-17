using UnityEngine;

public class PositionAdjuster : MonoBehaviour
{
    #region Letter Position Variables
    [SerializeField] private Transform[] positionMatrix;
    private int randomXPosition;

    #endregion

    public Transform RandomPosition()
    {
        randomXPosition = Random.Range(0, positionMatrix.Length-1);
        return positionMatrix[randomXPosition]; 
    }
}
