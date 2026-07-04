using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] private float _recoveryEnergyRate = 3f;
    
    [SerializeField] private Slider _energySlider;

    private float _currentEnergy;
    private const float _maxEnergy = 100f;

    void Start()
    {
        // ゲーム開始時はエネルギーを満タンにする
        _currentEnergy = _maxEnergy;
        UpdateEnergyUI();
    }

    void Update()
    {
        // 毎秒指定された値ずつ自動回復
        if (_currentEnergy < _maxEnergy)
        {
            _currentEnergy += _recoveryEnergyRate * Time.deltaTime;
            
            // 最大値を超えないように制限
            if (_currentEnergy > _maxEnergy)
            {
                _currentEnergy = _maxEnergy;
            }
            
            UpdateEnergyUI();
        }
    }

    /// <summary>
    /// プレイヤー（砲撃士）が撃つときに呼び出す関数
    /// </summary>
    /// <returns>エネルギーが足りていて消費に成功した場合はtrue</returns>
    public bool ConsumeEnergyForShoot()
    {
        const float shootCost = 2f;

        if (_currentEnergy >= shootCost)
        {
            _currentEnergy -= shootCost;
            UpdateEnergyUI();
            return true; // 射撃成功
        }

        Debug.Log("エネルギーが足りません！");
        return false; // エネルギー不足で射撃不可
    }

    /// <summary>
    /// UIのゲージを更新する
    /// </summary>
    private void UpdateEnergyUI()
    {
        if (_energySlider != null)
        {
            // SliderのMax Valueを100に設定しておくか、ここで同期させてください
            _energySlider.value = _currentEnergy;
        }
    }
}
