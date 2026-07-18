using UnityEngine;
using UnityEngine.InputSystem;

public class RoleSelectInput : MonoBehaviour
{
    //　PlayerInput
    [SerializeField] public PlayerInput PlayerInput;
    //　相手のRolrSelectInput
    [HideInInspector] public RoleSelectInput OtherRoleSelectInput;
    //　現在自分が何を選択中か
    [HideInInspector] public RoleSelectManager.RoleSelect ThisRoleSelect = RoleSelectManager.RoleSelect.Pilot;
    //　自分がどのコントローラーか
    [HideInInspector] public RoleSelectManager.WhichController ThisController;
    //　準備完了してるか
    [HideInInspector] public bool Ready = false;

    [HideInInspector] public RoleSelectManager RoleSelectManager;

    InputAction select;
    InputAction confirm;
    InputAction cancel;

    public void Start(){
        select = PlayerInput.actions.FindActionMap("Select").FindAction("Select");
        confirm = PlayerInput.actions.FindActionMap("Select").FindAction("Confirm");
        cancel = PlayerInput.actions.FindActionMap("Select").FindAction("Cancel");

        select.performed += Select;
        confirm.performed += Confirm;
        cancel.performed += Cancel;
    }


    /// <summary>
    /// 役職選択
    /// </summary>
    /// <param name="context">左スティック</param>
    public void Select(InputAction.CallbackContext context)
    {
        //　入力の瞬間以外は弾く
        if (context.performed == false) return;
        //　準備完了してるなら処理終了
        if (Ready == true) return;

        Vector2 vector2 = context.ReadValue<Vector2>();
        //　右に倒したら
        if (vector2.x > 0.5f)
        {
            //　Gunnerを選択
            ThisRoleSelect = RoleSelectManager.RoleSelect.Gunner;
            RoleSelectManager.SelectRole(ThisController, ThisRoleSelect);
        }
        //　左に倒したら
        else if (vector2.x < -0.5f)
        {
            //　Pilotを選択
            ThisRoleSelect = RoleSelectManager.RoleSelect.Pilot;
            RoleSelectManager.SelectRole(ThisController, ThisRoleSelect);
        }
    }

    /// <summary>
    /// 準備完了
    /// </summary>
    /// <param name="context">南ボタン</param>
    public void Confirm(InputAction.CallbackContext context)
    {
        //　入力の瞬間以外は弾く
        if (context.performed == false) return;
        //　準備完了してるなら処理終了
        if (Ready == true) return;

        //　もう一方が存在して準備完了していて役職が被っていたら処理終了
        if (OtherRoleSelectInput != null && OtherRoleSelectInput.Ready == true && OtherRoleSelectInput.ThisRoleSelect == ThisRoleSelect)
        {
            Debug.Log("役職被り");
            return;
        }

        //　準備完了
        Ready = true;
        RoleSelectManager.ReadyDisplay(ThisController, Ready);

        // もう一方が存在して準備完了していたらゲーム開始
        if (OtherRoleSelectInput != null && OtherRoleSelectInput.Ready == true) RoleSelectManager.GameStart();
    }

    /// <summary>
    /// 準備完了取り消し
    /// </summary>
    /// <param name="context">東ボタン</param>
    public void Cancel(InputAction.CallbackContext context)
    {
        //　入力の瞬間以外は弾く
        if (context.performed == false) return;
        //　準備完了してるなら取り消し
        if (Ready == true) Ready = false;
        RoleSelectManager.ReadyDisplay(ThisController, Ready);
    }
}
