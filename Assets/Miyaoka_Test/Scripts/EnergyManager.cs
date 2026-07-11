using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    // エネルギー回復速度
    [SerializeField] private float _recoveryRate = 3f;
    
    // エネルギーのUIImage
    [SerializeField] private Image _energyImage;

    private float _currentEnergy;
    private const float MaxEnergy = 100f;

    void Start()
    {
        _currentEnergy = MaxEnergy;
        UpdateEnergyUI();
    }

    void Update()
    {
        if (_currentEnergy < MaxEnergy)
        {
            _currentEnergy += _recoveryRate * Time.deltaTime;
            if (_currentEnergy > MaxEnergy)
            {
                _currentEnergy = MaxEnergy;
            }
            UpdateEnergyUI();
        }
    }

    public bool ConsumeEnergyForShoot()
    {
        const float _shootCost = 2f;

        if (_currentEnergy >= _shootCost)
        {
            _currentEnergy -= _shootCost;
            UpdateEnergyUI();
            return true; 
        }

        Debug.Log("エネルギーが足りません！");
        return false; 
    }

    /// <summary>
    /// UIのImageを更新する
    /// </summary>
    private void UpdateEnergyUI()
    {
        if (_energyImage != null)
        {
            _energyImage.fillAmount = _currentEnergy / MaxEnergy;
        }
    }
}
