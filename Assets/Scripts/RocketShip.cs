using UnityEngine;
using UnityEngine.InputSystem;

public class RocketShip : MonoBehaviour
{
    private RoleSelectInput[] _inputs;
    private PlayerAction _pilot;
    private PlayerAction _gunner;

    private void Start(){
        _pilot = new PlayerAction();
        _pilot.Enable();
        _gunner = new PlayerAction();
        _gunner.Enable();
    }

    private void BindPilotControls(){
        _pilot.Pilot.Move.performed += OnPilotMove;
        _pilot.Pilot.Boost.performed += OnPilotBoost;
    }

    private void BindGunnerControls(){
        _gunner.Gunner.Aim.performed += OnGunnerAim;
        _gunner.Gunner.Shoot.performed += OnGunnerShoot;
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
