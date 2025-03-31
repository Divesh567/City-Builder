using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public IInputManager inputManager;
    public UiController uiController;
    public int width, length;
    public CameraMovement cameraMovement;
    private BuildingManager buildingManager;
    private int cellSize = 3;

    private bool buildingModeActive = false;
    public LayerMask inputMask;

    private PlayerState state;
    public PlayerState State { get => state; }

    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerDemolitionState demolishState;
    public PlayerBuildingRoadState buildingRoadState;
    public PlayerBuildingAreaState buildingAreaState;


    public StructureRepository structureRepository;
    private void Awake()
    {

        PrepareStates();



#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if (UNITY_IOS || UNITY_ANDROID)

#endif
    }

    private void PrepareStates()
    {
        buildingManager = new BuildingManager(cellSize, width, length, placementManager, structureRepository);
        selectionState = new PlayerSelectionState(this);
        demolishState = new PlayerDemolitionState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        buildingAreaState = new PlayerBuildingAreaState(this, buildingManager);
        buildingRoadState = new PlayerBuildingRoadState(this, buildingManager);
        state = selectionState;
        state.EnterState(null);
    }
    private void PrepareGameComponenets()
    {
        inputManager.MouseInputMask = inputMask;
        cameraMovement.SetCameraLimits(0, width, 0, length);
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();

    }

    void Start()
    {

        PrepareGameComponenets();


        AssignInputListeners();
        AssignUiControllerListeners();
    }


    private void AssignUiControllerListeners()
    {
        uiController.AddListenerOnBuildAreaEvent((structureName) => state.OnBuildArea(structureName));
        uiController.AddListenerOnBuildSingleStructureEvent((structureName) => state.OnBuildSingleStructure(structureName));
        uiController.AddListenerOnBuildRoadEvent((structureName) => state.OnBuildRoad(structureName));
        uiController.AddListenerOnCancleActionEvent(() => state.OnCancel());
        uiController.AddListenerOnDemolishActionEvent(() => state.OnDemolishAction());
        uiController.AddListenerOnConfirmActionEvent(() => state.OnConfirmAction());
    }
    private void AssignInputListeners()
    {
        inputManager.AddListenerOnPointerDownEvent((position) => state.OnInputPointerDown(position));
        inputManager.AddListenerOnPointerSecondDownEvent((position) => state.OnInputPanChange(position));
        inputManager.AddListenerOnPointerSecondUpEvent(() => state.OnInputPanUp());
        inputManager.AddListenerOnPointerChangeEvent((position) => state.OnInputPointerChange(position));
        inputManager.AddListenerOnPointerUpEvent(() => state.OnInputPointerUp());
    }

    public void TransitionToState(PlayerState newState, string variable)
    {
        this.state = newState;
        this.state.EnterState(variable);
    }

}