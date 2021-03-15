using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderController : MonoBehaviour
{
    public static SceneLoaderController Instance;

    [SerializeField] private GameObject transitionObject;

    private GameObject currentAnimation;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneWithTransition(string sceneName, float duration)
    {
        if (currentAnimation != null) {

            DestroyImmediate(currentAnimation);
        }

        StartCoroutine(LoadLevel(sceneName, duration));
    }

    IEnumerator LoadLevel(string nameScene, float duration)
    {
        var transition = Instantiate(transitionObject, transform);

        transition.GetComponent<Animator>().SetTrigger("Start");

        currentAnimation = transition;

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(nameScene);
    }
}
