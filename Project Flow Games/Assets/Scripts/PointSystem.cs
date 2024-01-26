using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public static int points;
    public int publicPoints;
    public int winPoints;
    
    private void Update()
    {
        publicPoints = points;
    }

    public static void AddPoints(int pointsValue)
    {
        points += pointsValue;
    }

    public static void ResetPoints()
    {
        points = 0;
    }
}
