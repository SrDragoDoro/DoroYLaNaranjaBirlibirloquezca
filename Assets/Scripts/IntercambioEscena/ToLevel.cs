using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel : MonoBehaviour
{
    [SerializeField] private GameObject Loading;       // Animación
    [SerializeField] private float animTime = 2f;      // Duración de la animación
    private bool isChanging = false;                   // Evita spam de la tecla

    void Start()
    {
        Loading.SetActive(false);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Return)) && !isChanging)
        {
            StartCoroutine(StartAnimationAndChangeScene());
        }
    }

    IEnumerator StartAnimationAndChangeScene()
    {
        isChanging = true;          
                
        Loading.SetActive(true);                        // Inicia animación de carga
        yield return new WaitForSeconds(animTime);        
        Loading.SetActive(false);
                
        SceneManager.LoadScene("LevelGame");
    }
}
