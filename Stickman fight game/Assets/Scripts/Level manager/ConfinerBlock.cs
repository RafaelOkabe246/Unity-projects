using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConfinerBlock : MonoBehaviour
{
    public PolygonCollider2D cameraLimits;

    [SerializeField] private GameObject leftBound, rightBound;

    //New model
    public Transform invisibleWallsContainer;
    public List<GameObject> invisibleWalls;
    public int invisibleWallIndex;

    private void Start()
    {
        //SetBlockAreaLimits();
        invisibleWallIndex = 0;

        invisibleWalls = new List<GameObject>();
        for (int i = 0; i < invisibleWallsContainer.childCount; i++)
        {
            invisibleWalls.Add(invisibleWallsContainer.GetChild(i).gameObject);
        }

    }

    public void SetBounds()
    {
        ChangeColliderSize();
    }

    private void ChangeColliderSize()
    {
        invisibleWalls[invisibleWallIndex].SetActive(false);

        // Get the existing points of the collider
        Vector2[] points = cameraLimits.points;

        invisibleWallIndex++;

        points[0].x = invisibleWalls[invisibleWallIndex].transform.position.x;
        points[3].x = invisibleWalls[invisibleWallIndex].transform.position.x;

        // Assign the modified points back to the collider
        cameraLimits.points = points;

    }


    public void SetRightBound(bool i)
    {
        rightBound.SetActive(i);
    }
    public void SetLeftBound(bool i)
    {
        leftBound.SetActive(i);
    }


}
