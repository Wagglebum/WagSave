// -------------------------------------------------------------------------------------------------
// WaggleBum - WagSave
//  Copyright (c) 2026 WaggleBum, Inc. All Rights Reserved.
//
// File: GameController.cs
// -------------------------------------------------------------------------------------------------
using UnityEngine;
#if WAGGLEBUM_WAGSAVE
using WaggleBum.WagSave;
#endif

public class GameController : MonoBehaviour
{
#if WAGGLEBUM_WAGSAVE
    private WagSave _wagSave;
#endif

    public static GameController Instance { get; private set; }

    [Header("Enemy Spawner")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyCount = 5;
    [SerializeField] private float spawnRadius = 8f;
    [SerializeField] private Transform spawnCenter;

    [SerializeField] private int Score;
    public int CurrentScore => Score;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount = 1)
    {
        Score += amount;
    }

    public void ResetScore()
    {
        Score = 0;
    }

    private void Start()
    {
#if WAGGLEBUM_WAGSAVE
        _wagSave = WagSave.GetInstance();
#else
        Debug.LogError("WagSave is not installed. Please install the WaggleBum WagSave package.");
#endif
        SpawnEnemies();
    }

    private void SetupSaveEvents()
    {
#if WAGGLEBUM_WAGSAVE
        _wagSave.OnSaveCompleted += () =>
        {
            ScoreManager.Instance?.SetStatus("Saved");
        };

        _wagSave.OnLoadCompleted += () =>
        {
            ScoreManager.Instance?.SetStatus("Loaded");
        };
#else
        Debug.LogError("WagSave is not installed. Please install the WaggleBum WagSave package.");
#endif
    }

    private void SpawnEnemies()
    {
        Vector3 center = spawnCenter != null ? spawnCenter.position : Vector3.zero;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPoint = Random.insideUnitCircle * spawnRadius;
            var spawnPosition = center + new Vector3(randomPoint.x, randomPoint.y, 0f);
            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, null);
            enemy.name = $"Enemy_{i + 1}";
        }
    }

    public void OnSave()
    {
#if WAGGLEBUM_WAGSAVE
        _wagSave.Save();
#else
        Debug.LogError("WagSave is not installed. Please install the WaggleBum WagSave package.");
#endif
    }

    public void OnLoad()
    {
#if WAGGLEBUM_WAGSAVE
        _wagSave.Load();
#else
        Debug.LogError("WagSave is not installed. Please install the WaggleBum WagSave package.");
#endif
    }

    public void OnClear()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = Vector3.zero;

        foreach (var enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
            Destroy(enemy.gameObject);

        ResetScore();
    }

    public void OnReset()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = Vector3.zero;

        foreach (var enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
            Destroy(enemy.gameObject);

        ScoreManager.Instance?.ResetScore();
        ScoreManager.Instance?.SetStatus("");

        SpawnEnemies();
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = spawnCenter != null ? spawnCenter.position : Vector3.zero;
        Gizmos.color = new Color(1f, 0.8f, 0f, 0.3f);
        Gizmos.DrawSphere(center, spawnRadius);
        Gizmos.color = new Color(1f, 0.8f, 0f, 1f);
        Gizmos.DrawWireSphere(center, spawnRadius);
    }
}
