using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //setting scene names?
    public string nextSceneName;
 
    void Awake()
    {
        //if the GameManager isn't existing, load it in.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void LoadGame()
    {
        nextSceneName = ("Gameplay");   // SET FIRST
        SceneManager.LoadScene("Reset");     // LOAD SECOND

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMenu()
    {
        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.ResetCollectibles();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;






        nextSceneName = ("Main Menu");   // SET FIRST
        SceneManager.LoadScene("Reset1");    // LOAD SECOND
    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }
}
