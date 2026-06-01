// -------------------------------------------------------------------------------------------------
// WaggleBum - WagSave
//  Copyright (c) 2026 WaggleBum, Inc. All Rights Reserved.
//
// File: ScoreManager.cs
// -------------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int _displayedScore = -1;
    private Label _scoreLabel;
    private Label _statusLabel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var doc = GetComponentInChildren<UIDocument>();
        if (doc == null) { Debug.LogError("ScoreManager: no UIDocument found"); return; }

        var root = doc.rootVisualElement;

        _scoreLabel = root.Q<Label>("score-label");
        if (_scoreLabel == null) Debug.LogError("ScoreManager: 'score-label' not found in UXML");

        _statusLabel = root.Q<Label>("status-label");
        if (_statusLabel == null) Debug.LogError("ScoreManager: 'status-label' not found in UXML");

        var saveButton = root.Q<Button>("save-button");
        if (saveButton == null) Debug.LogError("ScoreManager: 'save-button' not found in UXML");
        else saveButton.clicked += () => { Debug.Log("Save clicked"); GameController.Instance.OnSave(); };

        var loadButton = root.Q<Button>("load-button");
        if (loadButton == null) Debug.LogError("ScoreManager: 'load-button' not found in UXML");
        else loadButton.clicked += () => { Debug.Log("Load clicked"); GameController.Instance.OnLoad(); };

        var resetButton = root.Q<Button>("reset-button");
        if (resetButton == null) Debug.LogError("ScoreManager: 'reset-button' not found in UXML");
        else resetButton.clicked += () => GameController.Instance.OnReset();

        var clearButton = root.Q<Button>("clear-button");
        if (clearButton == null) Debug.LogError("ScoreManager: 'clear-button' not found in UXML");
        else clearButton.clicked += () => GameController.Instance.OnClear();

        UpdateLabel();
    }

    private void Update()
    {
        if (GameController.Instance == null) return;
        int current = GameController.Instance.CurrentScore;
        if (current != _displayedScore)
        {
            _displayedScore = current;
            UpdateLabel();
        }
    }

    public void AddScore(int points = 1)
    {
        GameController.Instance?.AddScore(points);
    }

    public void ResetScore()
    {
        GameController.Instance?.ResetScore();
    }

    public void SetStatus(string message)
    {
        if (_statusLabel != null)
            _statusLabel.text = message;
    }

    private void UpdateLabel()
    {
        if (_scoreLabel != null)
            _scoreLabel.text = $"Score: {_displayedScore}";
    }
}
