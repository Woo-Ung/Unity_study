using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private Button _title;
    [SerializeField] private Button _gameScene;

    public bool IsGameRunning { get; private set; }
    public static GameManager Instance;

    private void Awake() => SetSingleton();
    private void Start()
    {
        // Title씬이면 UnlockCursor
        Run();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Run()
    {
        LockCursor();
        Time.timeScale = 1;
        IsGameRunning = true;
    }

    public void Pause()
    {
        UnlockCursor();
        Time.timeScale = 0;
        IsGameRunning = false;
    } 

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}