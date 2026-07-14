using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackGroundMover : MonoBehaviour
{
    private const float k_maxLength = 1f;
    private const string k_propName = "_MainTex";

    [SerializeField]
    private Vector2 m_offsetSpeed;
    public Vector2 m_NowSpeed;
    private Vector2 m_Reset = new Vector2(0,0);

    private Material m_copiedMaterial;

    private BossHealth bossHealth;

    private void Start()
    {
        var image = GetComponent<Image>();
        m_copiedMaterial = image.material;
        m_NowSpeed = m_offsetSpeed;

        // �}�e���A����null���������O���o�܂��B
        Assert.IsNotNull(m_copiedMaterial);
    }

    public void Restart()
    {
        m_NowSpeed = m_offsetSpeed;
    }

    private void Update()
    {
        if (MaingameManager.Instance.IsGaming == false) return;
        if (Time.timeScale == 0f)
        {
            return;
        }

        // x��y�̒l��0 �` 1�Ń��s�[�g����悤�ɂ���
        var x = Mathf.Repeat(Time.time * m_NowSpeed.x, k_maxLength);
        var y = Mathf.Repeat(Time.time * m_NowSpeed.y, k_maxLength);
        var offset = new Vector2(x, y);
        m_copiedMaterial.SetTextureOffset(k_propName, offset);
    }

    private void OnDestroy()
    {
        // �Q�[���I�u�W�F�N�g�j�󎞂Ƀ}�e���A���̃R�s�[�������Ă���
        m_copiedMaterial.SetTextureOffset(k_propName, m_Reset);
        Destroy(m_copiedMaterial);
        m_copiedMaterial = null;
    }
}