using UnityEngine;
using UnityEngine.InputSystem;

public class RocketShip : MonoBehaviour
{
    private RoleSelectInput[] _inputs;
    private PlayerInput _pilot;
    private PlayerInput _gunner;

    public void InitializeInputs(){
        _inputs = FindObjectsByType<RoleSelectInput>(FindObjectsSortMode.None);
        for (uint i = 0; i < _inputs.Length; i++){
            if (_inputs[i].ThisRoleSelect == RoleSelectManager.RoleSelect.Pilot){
                _pilot = _inputs[i].gameObject.GetComponent<PlayerInput>();
            }
            else if(_inputs[i].ThisRoleSelect == RoleSelectManager.RoleSelect.Gunner){
                _gunner = _inputs[i].gameObject.GetComponent<PlayerInput>();
            }
        }
        if (_pilot == null || _gunner == null){
            Debug.LogError("Couldn't find PlayerInputs!");
        }
        else{
            BindPilotControls();
            BindGunnerControls();
        }

    }

    private void BindPilotControls(){
        for (int i = 0; i < _pilot.actionEvents.Count; i++){
            if (_pilot.actionEvents[i].actionName == "Pilot/Move"){
                _pilot.actionEvents[i].AddListener(OnPilotMove);
            }
            if (_pilot.actionEvents[i].actionName == "Pilot/Boost"){
                _pilot.actionEvents[i].AddListener(OnPilotBoost);
            }
        }
    }

    private void BindGunnerControls(){
        for (int i = 0; i < _gunner.actionEvents.Count; i++){
            if (_gunner.actionEvents[i].actionName == "Gunner/Aim"){
                _gunner.actionEvents[i].AddListener(OnGunnerAim);
            }
            if (_gunner.actionEvents[i].actionName == "Gunner/Shoot"){
                _gunner.actionEvents[i].AddListener(OnGunnerShoot);
            }
        }
    }

    void OnPilotMove(InputAction.CallbackContext context){
        Debug.Log("Detected Pilot Move!");
    }

    void OnPilotBoost(InputAction.CallbackContext context){
        Debug.Log("Detected Pilot Boost!");
    }

    void OnGunnerAim(InputAction.CallbackContext context){
        Debug.Log("Detected Gunner Aiming!");
    }

    void OnGunnerShoot(InputAction.CallbackContext context){
        Debug.Log("FIRE!");
    }

    void HandleGunnerInput(InputAction.CallbackContext context){
        Debug.Log("Detected Gunner Input!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
