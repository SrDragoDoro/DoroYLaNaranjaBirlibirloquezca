using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        StartGame();
    }

    public void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {           
            SceneManager.LoadScene("Controles");
        }        
    }
}
