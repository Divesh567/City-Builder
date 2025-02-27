using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManager;
    StructureRepository structureRepository;
    Dictionary<Vector3Int, GameObject> structuresToBeModified = new Dictionary<Vector3Int, GameObject>();
    StructureModificationFactory helperFactory;
    StructureModificationHelper helper;
    public BuildingManager(int cellSize, int width, int length, PlacementManager placementManager, StructureRepository structureRepository)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManager = placementManager;
        this.structureRepository = structureRepository;
        StructureModificationFactory.PrepareFactory(structureRepository, grid, placementManager);

    }

    public void PrepareStructureForPlacement(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        helper.PrepareStructureForModification(inputPosition, structureName, structureType);
        
    }

    public void PrepareBuildingManager(Type classType)
    {
        helper = StructureModificationFactory.GetHelper(classType);
    }

    public void ConfirmModification()
    {
        helper.ConfirmModifications();
    }

    public void CancelModification()
    {
        helper.CancelModifications();
    }

    public void PrepareStructureForDemolitionAt(Vector3 inputPosition)
    {
        helper.PrepareStructureForModification(inputPosition, "", StructureType.None);
    }

   

    public GameObject CheckForStructureInGrid(Vector3 inputPosition)
    {
        Vector3 gridPositoion = grid.CalculateGridPosition(inputPosition);
        if (grid.IsCellTaken(gridPositoion))
        {
            return grid.GetStructureFromTheGrid(gridPositoion);
        }
        return null;
    }

    public GameObject CheckForStructureInDictionary(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        GameObject structureToReturn = null;
        structureToReturn = helper.AccessStructureInDictionary(gridPosition);
        if (structureToReturn != null)
        {
            return structureToReturn;
        }
        structureToReturn = helper.AccessStructureInDictionary(gridPosition);
        return structureToReturn;
    }
}