using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public Text shopInfoText;

    private void Start()
    {
        if (shopPanel != null)
            shopPanel.SetActive(false);
    }

    public void OpenShop()
    {
        if (shopPanel != null)
            shopPanel.SetActive(true);

        if (shopInfoText != null)
            shopInfoText.text = "Souls: " + GameManager.Instance.souls;
    }

    public void CloseShop()
    {
        if (shopPanel != null)
            shopPanel.SetActive(false);

        if (GameManager.Instance != null)
            GameManager.Instance.CloseShopAndContinue();
    }

    public void BuyDamageUpgrade()
    {
        int cost = 15 + (GameManager.Instance.wave * 5);
        if (GameManager.Instance.souls < cost)
            return;

        GameManager.Instance.SpendSoul(cost);

        if (GameManager.Instance.player != null)
            GameManager.Instance.player.UpgradeDamage(8);

        CloseShop();
    }

    public void BuyHealthUpgrade()
    {
        int cost = 20 + (GameManager.Instance.wave * 5);
        if (GameManager.Instance.souls < cost)
            return;

        GameManager.Instance.SpendSoul(cost);

        if (GameManager.Instance.player != null)
            GameManager.Instance.player.UpgradeMaxHealth(15);

        CloseShop();
    }

    public void BuySpeedUpgrade()
    {
        int cost = 18 + (GameManager.Instance.wave * 5);
        if (GameManager.Instance.souls < cost)
            return;

        GameManager.Instance.SpendSoul(cost);

        if (GameManager.Instance.player != null)
            GameManager.Instance.player.UpgradeMoveSpeed(0.6f);

        CloseShop();
    }
}
