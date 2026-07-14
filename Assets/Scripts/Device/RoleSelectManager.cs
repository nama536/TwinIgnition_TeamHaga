using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class RoleSelectManager : MonoBehaviour
{
    //　キャラセレクトに参加用ボタン
    [SerializeField] InputAction _joinButton;
    [SerializeField] InputAction _joinKeyboardWASD;
    [SerializeField] InputAction _joinKeyboardArrowKeys;
    //　キャラ選択用プレイヤーインプットオブジェクト（プレハブ）
    [SerializeField] GameObject _playerInputObject;
    //　何を選んでいるか表示
    [SerializeField] TextMeshProUGUI[] texts;
    //　準備完了表示
    [SerializeField] GameObject[] _doReadys;
    [SerializeField] GameObject _rocketPrefab;
    private RocketShip _rocketShip;

    //　どの役職を選択したか
    public enum RoleSelect
    {
        Pilot,
        Gunner,
    }
    //　どのコントローラーか
    public enum WhichController
    {
        One = 0,
        Two = 1,
    }
    //　現在のコントローラー参加数
    private int _joinControllerCount = 0;
    //　最初に参加したデバイス
    private InputDevice _firstDevice;
    //　セット用
    private RoleSelectInput[] _roleSelectInputs = new RoleSelectInput[2];


    //　参加ボタン有効化
    void Awake() { StartCoroutine(EnableSouthButton()); }
    private IEnumerator EnableSouthButton()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        _joinButton.Enable();
        _joinButton.performed += PushSouthButton;

        _joinKeyboardArrowKeys.Enable();
        _joinKeyboardArrowKeys.performed += JoinKeyboardArrowKeys;

        _joinKeyboardWASD.Enable();
        _joinKeyboardWASD.performed += JoinKeyboardWASD;
    }
    //WASDキーボード入力用
    private void JoinKeyboardWASD(InputAction.CallbackContext context){
        if (_joinControllerCount >= 2) return;
        Debug.Log("Joining WASD");

        PlayerInput input = PlayerInput.Instantiate(_playerInputObject, _joinControllerCount, "KeyboardWASD", pairWithDevice: context.control.device);
        texts[_joinControllerCount].text = "Pilot";
        //　スクリプト初期設定
        _roleSelectInputs[_joinControllerCount] = input.GetComponent<RoleSelectInput>();
        _roleSelectInputs[_joinControllerCount].ThisController = (WhichController)_joinControllerCount;
        _roleSelectInputs[_joinControllerCount].RoleSelectManager = this;
        _joinButton.performed -= JoinKeyboardWASD;
        //　１つ目参加終了処理
        if (_joinControllerCount  < 1){
            _joinControllerCount++;
            _firstDevice = context.control.device;
        }
        else{
            _joinControllerCount++;
            _joinButton.Disable();

            _roleSelectInputs[0].OtherRoleSelectInput = _roleSelectInputs[1];
            _roleSelectInputs[1].OtherRoleSelectInput = _roleSelectInputs[0];
        }
    }

    private void JoinKeyboardArrowKeys(InputAction.CallbackContext context){
        if (_joinControllerCount >= 2) return;
        Debug.Log("Joining arrow keys");

        PlayerInput input = PlayerInput.Instantiate(_playerInputObject, _joinControllerCount, "KeyboardArrowKeys", pairWithDevice: context.control.device);
        texts[0].text = "Pilot";
        //　スクリプト初期設定
        _roleSelectInputs[_joinControllerCount] = input.GetComponent<RoleSelectInput>();
        _roleSelectInputs[_joinControllerCount].ThisController = (WhichController)_joinControllerCount;
        _roleSelectInputs[_joinControllerCount].RoleSelectManager = this;
        _joinButton.performed -= JoinKeyboardArrowKeys;
        //　１つ目参加終了処理
        if (_joinControllerCount  < 1){
            _joinControllerCount++;
            _firstDevice = context.control.device;
        }
        else{
            _joinControllerCount++;
            _joinButton.Disable();

            _roleSelectInputs[0].OtherRoleSelectInput = _roleSelectInputs[1];
            _roleSelectInputs[1].OtherRoleSelectInput = _roleSelectInputs[0];
        }
    }

    private void PushSouthButton(InputAction.CallbackContext context)
    {
        //　参加コントローラーが２つ以上なら処理終了
        if (_joinControllerCount >= 2) return;

        switch (_joinControllerCount)
        {
            //　参加コントローラーが０なら
            case 0:
                //　１つ目のセレクト召喚
                PlayerInput p1 = PlayerInput.Instantiate(_playerInputObject, _joinControllerCount, "Controller", pairWithDevice: context.control.device);
                texts[0].text = "Pilot";
                //　スクリプト初期設定
                _roleSelectInputs[0] = p1.GetComponent<RoleSelectInput>();
                _roleSelectInputs[0].ThisController = WhichController.One;
                _roleSelectInputs[0].RoleSelectManager = this;
                //　１つ目参加終了処理
                ++_joinControllerCount;
                _firstDevice = context.control.device;
                break;
            case 1:
                //　もし既に参加してるデバイスなら処理終了
                if (context.control.device == _firstDevice) return;
                //　２つ目のセレクト召喚
                PlayerInput p2 = PlayerInput.Instantiate(_playerInputObject, pairWithDevice: context.control.device);
                texts[1].text = "Pilot";
                //　スクリプト初期設定
                _roleSelectInputs[1] = p2.GetComponent<RoleSelectInput>();
                _roleSelectInputs[1].ThisController = WhichController.Two;
                _roleSelectInputs[1].RoleSelectManager = this;
                _roleSelectInputs[0].OtherRoleSelectInput = _roleSelectInputs[1];
                _roleSelectInputs[1].OtherRoleSelectInput = _roleSelectInputs[0];
                //　２つ目参加終了処理
                ++_joinControllerCount;
                _joinButton.performed -= PushSouthButton;
                _joinButton.Disable();
                break;
        }
    }

    /// <summary>
    /// ロール選択
    /// </summary>
    /// <param name="whichController">どのコントローラーの処理か</param>
    /// <param name="roleSelect">どの役職を選んだか</param>
    public void SelectRole(WhichController whichController, RoleSelect roleSelect)
    {
        //　コントローラーに対応した役職表示
        if (roleSelect == RoleSelect.Pilot) texts[(int)whichController].text = "Pilot";
        else if (roleSelect == RoleSelect.Gunner) texts[(int)whichController].text = "Gunner";
    }

    /// <summary>
    /// 準備完了表示
    /// </summary>
    /// <param name="whichController">どのコントローラーからの処理か</param>
    /// <param name="ready">準備完了中かどうか</param>
    public void ReadyDisplay(WhichController whichController, bool ready)
    {
        //　コントローラーに対応した準備完了表示
        _doReadys[(int)whichController].SetActive(ready);
    }

    //　ゲーム開始
    public void GameStart()
    {
        foreach (var roleSelectInput in _roleSelectInputs)
        {
            //　選択した役職のマップを割り当て
            switch (roleSelectInput.ThisRoleSelect)
            {
                case RoleSelect.Pilot:
                    roleSelectInput.PlayerInput.SwitchCurrentActionMap("Pilot");
                    break;
                case RoleSelect.Gunner:
                    roleSelectInput.PlayerInput.SwitchCurrentActionMap("Gunner");
                    break;
            }
            Debug.Log(roleSelectInput.ThisController + roleSelectInput.PlayerInput.currentActionMap.name);
        }
        Debug.Log("ゲーム開始");
        _rocketShip = Instantiate(_rocketPrefab).GetComponent<RocketShip>();;
        _rocketShip.SetPlayerInputs(_roleSelectInputs);
        this.gameObject.SetActive(false);
    }
}
