using UnityEngine;

public class MainMenuControl : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.LoadGame();
    }
    public void ExitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
