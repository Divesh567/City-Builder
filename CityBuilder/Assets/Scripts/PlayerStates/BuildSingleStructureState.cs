using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingSingleStructureState : PlayerState
{
    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildingSingleStructureState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnConfirmAction()
    {
        base.OnConfirmAction();
        this.buildingManager.ConfirmPlacement();
    }
    public override void OnInputPointerDown(Vector3 position)
    {
        Debug.Log("Facility Built");
        buildingManager.PlaceStructureAt(position, this.structureName, StructureType.SingleStructure);
    }

 
    public override void EnterState(string structureName)
    {
        base.EnterState(structureName);
        this.structureName = structureName;
    }

    public override void OnBuildArea(string structureName)
    {

        base.OnBuildArea(structureName);
        this.buildingManager.CancelPlacement();
    }

    public override void OnBuildRoad(string structureName)
    {

        base.OnBuildRoad(structureName);
        this.buildingManager.CancelPlacement();
    }

    public override void OnCancel()
    {
        this.buildingManager.CancelPlacement();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }
}