using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class RoleSelectManager : MonoBehaviour
{
    //　キャラセレクトに参加用ボタン
    [SerializeField] InputAction _joinButton;
    [SerializeField] InputAction _joinKeyboardWASD;
    [SerializeField] InputAction _joinKeyboardArrowKeys;
    //　参加待ちテキスト
    [SerializeField] GameObject[] _waitTexts;
    //　キャラ選択用プレイヤーインプットオブジェクト（プレハブ）
    [SerializeField] GameObject _playerInputObject;
    //　何を選んでいるか表示
    [SerializeField] Image[] _roleTexts;
    //　役職文字画像
    [SerializeField] Sprite _pilotText, _gunnerText;
    //　矢印　0=p1左 1=p1右 2=p2左 3=p2右
    [SerializeField] Image[] _rArrows;
    [SerializeField] Image[] _lArrows;
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
        //　スクリプト初期設定
        _roleSelectInputs[_joinControllerCount] = input.GetComponent<RoleSelectInput>();
        _roleSelectInputs[_joinControllerCount].ThisController = (WhichController)_joinControllerCount;
        _roleSelectInputs[_joinControllerCount].RoleSelectManager = this;
        _joinButton.performed -= JoinKeyboardWASD;
        _waitTexts[_joinControllerCount].SetActive(false);
        _roleTexts[_joinControllerCount].gameObject.SetActive(true);
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

        SoundManager.Instance.Play("Click");
    }

    private void JoinKeyboardArrowKeys(InputAction.CallbackContext context){
        if (_joinControllerCount >= 2) return;
        Debug.Log("Joining arrow keys");

        PlayerInput input = PlayerInput.Instantiate(_playerInputObject, _joinControllerCount, "KeyboardArrowKeys", pairWithDevice: context.control.device);
        //　スクリプト初期設定
        _roleSelectInputs[_joinControllerCount] = input.GetComponent<RoleSelectInput>();
        _roleSelectInputs[_joinControllerCount].ThisController = (WhichController)_joinControllerCount;
        _roleSelectInputs[_joinControllerCount].RoleSelectManager = this;
        _joinButton.performed -= JoinKeyboardArrowKeys;
        _waitTexts[_joinControllerCount].SetActive(false);
        _roleTexts[_joinControllerCount].gameObject.SetActive(true);
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

        SoundManager.Instance.Play("Click");
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
                SelectRole(WhichController.One, RoleSelect.Pilot);
                _waitTexts[0].SetActive(false);
                _roleTexts[0].gameObject.SetActive(true);
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
                PlayerInput p2 = PlayerInput.Instantiate(_playerInputObject, _joinControllerCount, "Controller", pairWithDevice: context.control.device);
                SelectRole(WhichController.Two, RoleSelect.Pilot);
                _waitTexts[1].SetActive(false);
                _roleTexts[1].gameObject.SetActive(true);
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

        SoundManager.Instance.Play("Click");
    }

    /// <summary>
    /// ロール選択
    /// </summary>
    /// <param name="whichController">どのコントローラーの処理か</param>
    /// <param name="roleSelect">どの役職を選んだか</param>
    public void SelectRole(WhichController whichController, RoleSelect roleSelect)
    {
        //　コントローラーに対応した役職表示
        if (roleSelect == RoleSelect.Pilot)
        {
            _roleTexts[(int)whichController].sprite = _pilotText;
            _rArrows[(int)whichController].enabled = true;
            _lArrows[(int)whichController].enabled = false;
        }
        else if (roleSelect == RoleSelect.Gunner)
        {
            _roleTexts[(int)whichController].sprite = _gunnerText;
            _rArrows[(int)whichController].enabled = false;
            _lArrows[(int)whichController].enabled = true;
        }

        SoundManager.Instance.Play("Click");
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
        //　矢印の表示非表示
        if (ready)
        {
            _rArrows[(int)whichController].color = new Color(1f, 1f, 1f, 0f);
            _lArrows[(int)whichController].color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            _rArrows[(int)whichController].color = Color.white;
            _lArrows[(int)whichController].color = Color.white;
        }

        SoundManager.Instance.Play("Click");
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
        MaingameManager.Instance.IsGaming = true;
        this.gameObject.SetActive(false);
    }
}
