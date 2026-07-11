using UnityEngine;
using UnityEngine.InputSystem;

public class RocketShip : MonoBehaviour
{
    private RoleSelectInput[] _inputs;
    private PlayerInput _pilot;
    private PlayerInput _gunner;

    private void InitializeInputs(){
        _inputs = FindObjectsByType<RoleSelectInput>(FindObjectsSortMode.None);
        for (uint i = 0; i < _inputs.Length; i++){
            if (_inputs[i].ThisRoleSelect == RoleSelectManager.RoleSelect.Pilot){
                PlayerInput _pilot = _inputs[i].GetComponent<PlayerInput>();
            }
            else if(_inputs[i].ThisRoleSelect == RoleSelectManager.RoleSelect.Gunner){
                PlayerInput _gunner = _inputs[i].GetComponent<PlayerInput>();
            }
        }
        if (_pilot == null || _gunner == null){
            Debug.LogError("Couldn't find PlayerInputs!");
        }
        else{
            _pilot.onActionTriggered += HandlePilotInput;
            _gunner.onActionTriggered += HandleGunnerInput;
        }
    }

    private void OnEnable(){
        if (_pilot != null && _gunner != null){
            _pilot.onActionTriggered += HandlePilotInput;
            _gunner.onActionTriggered += HandleGunnerInput;
        }
    }

    private void OnDisable(){
        if (_pilot != null && _gunner != null){
            _pilot.onActionTriggered -= HandlePilotInput;
            _gunner.onActionTriggered -= HandleGunnerInput;
        }
    }

    void HandlePilotInput(InputAction.CallbackContext context){
        Debug.Log("Detected Pilot Input!");
    }

    void HandleGunnerInput(InputAction.CallbackContext context){
        Debug.Log("Detected Gunner Input!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeInputs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
