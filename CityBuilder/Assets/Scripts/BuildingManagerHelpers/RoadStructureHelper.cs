using UnityEngine;
using static RoadStructureSO;

public class RoadStructureHelper 
{
    public RotationValue RoadPrefabRotation { get; set; }
    public GameObject RoadPrefab { get; set; }

    public RoadStructureHelper(GameObject roadPrefab, RotationValue roadPrefabRotation)
    {

        RoadPrefabRotation = roadPrefabRotation;

        RoadPrefab = roadPrefab;
    }
}
