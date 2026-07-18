using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    //　クリアしたか
    [HideInInspector] public bool DoClear = false;
    //　スコア
    [HideInInspector] public int Score = 0;
}
