using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIcontroller : MonoBehaviour
{
    [SerializeField] private Button _title;
    [SerializeField] private Button _retry;


    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene(0);
    }

}