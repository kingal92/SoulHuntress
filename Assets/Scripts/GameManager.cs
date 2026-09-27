using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    public PlayerController player;
    public EnemySpawner spawner;
    public ShopManager shopManager;
    public UIManager uiManager;

    [Header("Progression")]
    public int souls = 0;
    public int wave = 0;
    public int maxPlayerHealth = 100;
    public int playerHealth = 100;

    [Header("Boss")]
    public GameObject bossPrefab;
    public int bossReward = 80;

    private bool shopOpen;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        if (spawner == null)
            spawner = FindObjectOfType<EnemySpawner>();

        if (shopManager == null)
            shopManager = FindObjectOfType<ShopManager>();

        if (uiManager == null)
            uiManager = FindObjectOfType<UIManager>();

        playerHealth = maxPlayerHealth;
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (shopOpen)
            return;

        wave += 1;

        if (uiManager != null)
            uiManager.UpdateWaveLabel(wave);

        if (wave % 3 == 0 && bossPrefab != null)
        {
            if (spawner != null)
                spawner.StartBossWave(wave);
        }
        else
        {
            if (spawner != null)
                spawner.StartWave(wave);
        }
    }

    public void OnEnemyDefeated(EnemyController enemy)
    {
        if (spawner != null && !spawner.IsBossWave && spawner.IsWaveClear())
        {
            StartCoroutine(OpenShopAfterDelay());
        }
    }

    public void OnBossDefeated()
    {
        AddSoul(bossReward);
        StartCoroutine(OpenShopAfterDelay());
    }

    private IEnumerator OpenShopAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        shopOpen = true;

        if (shopManager != null)
            shopManager.OpenShop();
    }

    public void CloseShopAndContinue()
    {
        shopOpen = false;
        StartNextWave();
    }

    public void AddSoul(int amount)
    {
        souls += amount;
        if (uiManager != null)
            uiManager.UpdateSoulLabel(souls);
    }

    public void SpendSoul(int amount)
    {
        souls -= amount;
        if (uiManager != null)
            uiManager.UpdateSoulLabel(souls);
    }

    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;

        if (uiManager != null)
            uiManager.UpdateHealthLabel(playerHealth, maxPlayerHealth);

        if (playerHealth <= 0)
        {
            ResetRun();
        }
    }

    public void HealPlayer(int amount)
    {
        playerHealth = Mathf.Min(maxPlayerHealth, playerHealth + amount);
        if (uiManager != null)
            uiManager.UpdateHealthLabel(playerHealth, maxPlayerHealth);
    }

    private void ResetRun()
    {
        souls = 0;
        wave = 0;
        playerHealth = maxPlayerHealth;

        if (player != null)
            player.ResetCharacter();

        if (uiManager != null)
            uiManager.UpdateSoulLabel(0);

        StartNextWave();
    }
}
