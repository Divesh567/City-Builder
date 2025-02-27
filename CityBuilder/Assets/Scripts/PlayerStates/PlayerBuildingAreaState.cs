using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingAreaState : PlayerState
{

    BuildingManager buildingManager;
    string structureName;

    public PlayerBuildingAreaState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
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
        this.buildingManager.PrepareBuildingManager(this.GetType());
        this.structureName = structureName;
    }

    public override void OnInputPointerDown(Vector3 position)
    {
        Debug.Log("Area Built");
        buildingManager.PrepareStructureForPlacement(position, this.structureName, StructureType.Zone);
    }

    public override void OnBuildSingleStructure(string structureName)
    {
        base.OnBuildSingleStructure(structureName);
        this.buildingManager.CancelModification();
    }

    public override void OnBuildRoad(string structureName)
    {

        base.OnBuildRoad(structureName);
        this.buildingManager.CancelModification();
    }

    public override void OnCancel()
    {
        this.buildingManager.CancelModification();
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }
}