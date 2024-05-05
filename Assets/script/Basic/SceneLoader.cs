using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Awake()
    {
        SetUpSingleton();
    }

    void SetUpSingleton()
    {
        int numberOfMain = FindObjectsOfType<SceneLoader>().Length;
        if ( numberOfMain > 1 )
        {
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }
    
    public void LoadTargetScene(string target)
    {
        SceneManager.LoadScene(target);
    }

    public void reloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
