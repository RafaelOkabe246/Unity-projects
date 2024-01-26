using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Layout", menuName = "Terrain/Layouts")]
public class TerrainLayout : ScriptableObject
{
    public List<TerrainScript> _terrainScript;

    [Header("Grid manager size")]
    public int width, height;

    [Header("Units and spawn points")]
    public List<Unit> enemyUnits;
    public List<Vector2> enemyUnitsSpawns;

    public List<Unit> playerUnits;
    public List<Vector2> playerUnitsSpawns;
}
