using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenuGame : MonoBehaviour
{   
    void Update()
    {
        StartGame();
    }

    public void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
