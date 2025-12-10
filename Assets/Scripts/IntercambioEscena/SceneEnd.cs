using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneEnd : MonoBehaviour
{
    [SerializeField] private float delayBeforeChange;

    private void Start()
    {
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        SceneManager.LoadScene("GameOver2");
    }
}
