using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph 
{
    public List<BattleTile> pathlist = new List<BattleTile>();
    public List<BattleTile> wayPoints = new List<BattleTile>();


    public const int COST_MOVE_STRAIGHT = 10;
    public const int COST_MOVE_DIAGONAL = 14;

    public List<BattleTile> AStarIgnoreOccupiedTiles(Grid levelGrid, BattleTile startId, BattleTile endId)
    {
        foreach (KeyValuePair<Vector2, BattleTile> value in levelGrid.battleTiles)
        {
            wayPoints.Add(value.Value);
        }

        BattleTile start = startId;
        BattleTile end = endId;

        if (!start || !end)
        {
            return null;
        }

        List<BattleTile> open = new List<BattleTile> { start }; //New nodes
        List<BattleTile> closed = new List<BattleTile>(); //Searched nodes

        float tentative_g_score = 0;


        foreach (BattleTile _node in wayPoints)
        {
            BattleTile nodePathfind = _node;
            nodePathfind.gcost = int.MaxValue;
            nodePathfind.cameFrom = null;
        }

        start.gcost = 0;
        start.hcost = DistanceBtwNodes(start, end);
        start.fcost = start.hcost;

        open.Add(start);
        while (open.Count > 0)
        {
            int i = LowestF(open);
            BattleTile currentNode = open[i];
            if (currentNode == endId) //Reached the end node
            {
                ReconstructPath(end);
                return pathlist;
            }
            //Debug.Log(open.Count +" "+ open[i]);
            //Node is not the lowest, add to closed list 
            open.RemoveAt(i);
            closed.Add(currentNode);

            //Search the neighbours
            foreach (BattleTile neighbourNode in currentNode.neighbourTiles)
            {
                if (closed.Contains(neighbourNode))
                    continue;
                /*
                if (neighbourNode.CompareStateWith(TileState.Occupied))
                {
                    //Debug.Log(neighbourNode);
                    closed.Add(neighbourNode);
                    continue;
                }
                */
                //Get the G score from this node to the neighbour node (g = cost distance from start node)
                tentative_g_score = currentNode.gcost + DistanceBtwNodes(currentNode, neighbourNode);

                if (tentative_g_score < neighbourNode.gcost)
                {
                    neighbourNode.cameFrom = currentNode;
                    neighbourNode.gcost = tentative_g_score;
                    neighbourNode.hcost = DistanceBtwNodes(currentNode, end);
                    neighbourNode.CalculateFCost();

                    if (!open.Contains(neighbourNode))
                    {
                        //Debug.Log(neighbourNode);
                        open.Add(neighbourNode);
                    }
                }
            }
        }

        return null;//Didn't found path
    }

    public List<BattleTile> AStar(Grid levelGrid, BattleTile startId, BattleTile endId)
    {
        foreach (KeyValuePair<Vector2,BattleTile> value in levelGrid.battleTiles)
        {
            wayPoints.Add(value.Value);
        }

        BattleTile start = startId;
        BattleTile end = endId;

        if (!start || !end)
        {
            return null;
        }

        List<BattleTile> open = new List<BattleTile> { start }; //New nodes
        List<BattleTile> closed = new List<BattleTile>(); //Searched nodes

        float tentative_g_score = 0;
        

        foreach (BattleTile _node in wayPoints)
        {
            BattleTile nodePathfind = _node;
            nodePathfind.gcost = int.MaxValue;
            nodePathfind.cameFrom = null;
        }
        
        start.gcost = 0;
        start.hcost = DistanceBtwNodes(start, end);
        start.fcost = start.hcost;
        
        open.Add(start);
        while (open.Count > 0)
        {
            int i = LowestF(open);
            BattleTile currentNode = open[i];
            if (currentNode == endId) //Reached the end node
            {
                ReconstructPath(end);
                return pathlist;
            }
            //Debug.Log(open.Count +" "+ open[i]);
            //Node is not the lowest, add to closed list 
            open.RemoveAt(i);
            closed.Add(currentNode);

            //Search the neighbours
            foreach (BattleTile neighbourNode in currentNode.neighbourTiles)
            {
                if (closed.Contains(neighbourNode)) 
                    continue;
                if(neighbourNode.CompareStateWith(TileState.Occupied))
                {
                    //Debug.Log(neighbourNode);
                    closed.Add(neighbourNode);
                    continue;
                }

                //Get the G score from this node to the neighbour node (g = cost distance from start node)
                tentative_g_score = currentNode.gcost + DistanceBtwNodes(currentNode, neighbourNode);

                if (tentative_g_score < neighbourNode.gcost)
                {
                    neighbourNode.cameFrom = currentNode;
                    neighbourNode.gcost = tentative_g_score;
                    neighbourNode.hcost = DistanceBtwNodes(currentNode, end);
                    neighbourNode.CalculateFCost();

                    if (!open.Contains(neighbourNode))
                    {
                        //Debug.Log(neighbourNode);
                        open.Add(neighbourNode);
                    }
                }
            }
        }

        return null;//Didn't found path
    }

    public void ReconstructPath(BattleTile endId)
    {
        pathlist.Clear();
        pathlist.Add(endId);

        BattleTile currentNode = endId;

        while (currentNode.cameFrom != null)
        {
            pathlist.Add(currentNode.cameFrom);
            currentNode = currentNode.cameFrom;
        }
    }

    float DistanceBtwNodes(BattleTile a, BattleTile b)
    {
        //return COST_MOVE_HORIZONTAL;
        float dx = Mathf.Abs(a.gridPosition.x - b.gridPosition.x);
        float dy = Mathf.Abs(a.gridPosition.y - b.gridPosition.y);
        float remaing = Mathf.Abs(dx - dy);
        return COST_MOVE_DIAGONAL * Mathf.Min(dx,dy) + COST_MOVE_STRAIGHT * remaing;
    }

    int LowestF(List<BattleTile> _nodesInOpenList)
    {
        float lowestF = 0;
        int count = 0;
        int iteratorCount = 0;

        lowestF = _nodesInOpenList[0].fcost;

        for (int i = 0; i < _nodesInOpenList.Count; i++)
        {
            if (_nodesInOpenList[i].fcost <= lowestF)
            {
                lowestF = _nodesInOpenList[i].fcost;
                iteratorCount = count;
            }
            count++;
        }
        //Node with the lowest f value
        return iteratorCount;
    }

}
