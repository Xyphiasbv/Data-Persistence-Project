using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHigScoreScene : MonoBehaviour
{
    public static UIHigScoreScene Instance { get; private set; }  

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
