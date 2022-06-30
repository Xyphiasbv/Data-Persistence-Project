using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainScene : MonoBehaviour
{
    public static UIMainScene Instance { get; private set; }  

    private void Awake()
    {
        Instance = this;        
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void BackToMenu()
    {
        ScoreManager.Instance.LoadName();
        SceneManager.LoadScene(0);
    }
}
