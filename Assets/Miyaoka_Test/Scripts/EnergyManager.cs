using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    // �G�l���M�[�񕜑��x
    [SerializeField] private float _recoveryRate = 3f;
    
    // �G�l���M�[��UIImage
    [SerializeField] private Image _energyImage;

    private float _currentEnergy;
    private const float MaxEnergy = 30f;

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
            MaingameManager.Instance.CanShot = true;
            _currentEnergy -= _shootCost;
            UpdateEnergyUI();
            return true; 
        }

        MaingameManager.Instance.CanShot = false;
        Debug.Log("�G�l���M�[������܂���I");
        return false; 
    }

    /// <summary>
    /// UI��Image���X�V����
    /// </summary>
    private void UpdateEnergyUI()
    {
        if (_energyImage != null)
        {
            _energyImage.fillAmount = _currentEnergy / MaxEnergy;
        }
    }
}
