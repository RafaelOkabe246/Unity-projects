using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Terrain", menuName = "Terrain/Terrain")]
public class TerrainScript : ScriptableObject
{
    public List<Vector2> _terrainVector;
    public int _terrainIndex;
}
