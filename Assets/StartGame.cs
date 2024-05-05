using UnityEngine;
using UnityEngine.SceneManagement;  // 引用场景管理库

public class StartGame : MonoBehaviour
{
    // 这个方法将通过按钮的OnClick事件调用
    public void LoadGame()
    {
        SceneManager.LoadScene("MainGame");  // 确保替换为你的游戏主场景名
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
