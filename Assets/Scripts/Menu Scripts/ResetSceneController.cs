using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class ResetSceneController : MonoBehaviour
{
   
    //set values for fade and delay
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration = 1.5f;
    [SerializeField] float delayBeforeLoad = 0.5f;
    private void Start()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        yield return StartCoroutine(FadeIn());
        yield return new
       WaitForSeconds(delayBeforeLoad); //setting WaitForSeconds to value so it's changable later

        yield return null;

        SceneManager.LoadScene(GameManager.instance.nextSceneName); //load next scene after (Gameplay) if I ordered it correctly lol
        Debug.Log("Next Scene: " + GameManager.instance.nextSceneName);
    }
    IEnumerator FadeIn()
    {
        float t = 0;
        Color c = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
    }
}
