using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoIntro : MonoBehaviour
{
    [SerializeField] private float delayBeforeChange;

    private void Start()
    {
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        SceneManager.LoadScene("Menu");
    }
}
