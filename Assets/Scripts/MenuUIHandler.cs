using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(ScoreManager.Instance.Name == null)
        {
            ScoreManager.Instance.Name = GameObject.FindGameObjectWithTag("Name").GetComponent<InputField>();    
        }
        
    }

    public void StartNew()
    {
        //ScoreManager.Instance.SaveName();   
        
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        //ScoreManager.Instance.SaveName();
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit(); // original code to quit Unity player
        #endif
    }

    public void HighScores(){
        
        //ScoreManager.Instance.SaveName();
        
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
