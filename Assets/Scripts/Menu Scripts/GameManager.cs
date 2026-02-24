using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    //setting scene names?
    public string nextSceneName;
    [SerializeField]
    private string
   mainMenuSceneName = "Main Menu";
    [SerializeField]
    private string
    resetSceneName = "Reset";
    [SerializeField]
    private string
   gameplaySceneName = "Gameplay";

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

        SceneManager.LoadScene(resetSceneName);
        nextSceneName = gameplaySceneName;
            }

    public void ReturnToMenu()
    {
        // Reset totals before leaving gameplay
        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.ResetCollectibles()
            ;
        }
        SceneManager.LoadScene("Reset");
        nextSceneName = "Main Menu";
    }

}
