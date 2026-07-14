using UnityEngine;
using UnityEngine.InputSystem;

public class RocketShip : MonoBehaviour
{
    private RoleSelectInput[] _roleSelectInputs;

    private RoleSelectInput _pilot;
    private RoleSelectInput _gunner;

    private PlayerInput _pilotInput;
    private PlayerInput _gunnerInput;

    private InputAction _aim;
    private InputAction _shoot;
    
    private InputAction _move;
    private InputAction _boost;

    public void SetPlayerInputs(RoleSelectInput[] roleSelectInputs){
        _roleSelectInputs = roleSelectInputs;
        for (uint i = 0; i < _roleSelectInputs.Length; i++){
            if (_roleSelectInputs[i].ThisRoleSelect == RoleSelectManager.RoleSelect.Pilot){
                _pilot = _roleSelectInputs[i];
            }
            else{
                _gunner = _roleSelectInputs[i];
            }
        }

        _pilotInput = _pilot.PlayerInput;
        _gunnerInput = _gunner.PlayerInput;

        BindControls();
    }

    private void BindControls(){
        _aim = _gunnerInput.actions.FindActionMap("Gunner").FindAction("Aim");
        _shoot = _gunnerInput.actions.FindActionMap("Gunner").FindAction("Shoot");

        _move = _pilotInput.actions.FindActionMap("Pilot").FindAction("Move");
        _boost = _pilotInput.actions.FindActionMap("Pilot").FindAction("Boost");

        _shoot.performed += GunnerShoot;
        _boost.performed += PilotBoost;
    }

    private void GunnerShoot(InputAction.CallbackContext context){
        Debug.Log("Shoot");
    }

    private void PilotBoost(InputAction.CallbackContext context){
        Debug.Log("Boost");
    }
}
