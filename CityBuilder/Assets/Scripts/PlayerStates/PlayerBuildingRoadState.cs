using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingRoadState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildingRoadState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnConfirmAction()
    {
        base.OnConfirmAction();
        this.buildingManager.ConfirmModification();
    }

    public override void EnterState(string structureName)
    {
        base.EnterState(structureName);
        this.structureName = structureName;
        this.buildingManager.PrepareBuildingManager(this.GetType());
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        Debug.Log("Road Built");
        buildingManager.PrepareStructureForPlacement(position, this.structureName, StructureType.Road);
    }

    public override void OnBuildArea(string structureName)
    {

        base.OnBuildArea(structureName);
        
        this.buildingManager.CancelModification();
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        base.OnBuildSingleStructure(structureName);
        this.buildingManager.CancelModification();
    }

    public override void OnCancel()
    {
        this.buildingManager.CancelModification();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }
}