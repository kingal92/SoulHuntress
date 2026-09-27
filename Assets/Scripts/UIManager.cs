using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text soulText;
    public Text waveText;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            UpdateHealthLabel(GameManager.Instance.playerHealth, GameManager.Instance.maxPlayerHealth);
            UpdateSoulLabel(GameManager.Instance.souls);
            UpdateWaveLabel(GameManager.Instance.wave);
        }
    }

    public void UpdateHealthLabel(int current, int max)
    {
        if (healthText != null)
            healthText.text = "Health: " + current + " / " + max;
    }

    public void UpdateSoulLabel(int souls)
    {
        if (soulText != null)
            soulText.text = "Souls: " + souls;
    }

    public void UpdateWaveLabel(int wave)
    {
        if (waveText != null)
            waveText.text = "Wave: " + wave;
    }
}
