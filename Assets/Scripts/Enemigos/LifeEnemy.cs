using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class LifeEnemy : MonoBehaviour
{   
    [SerializeField] private GameObject enemyDead;
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private int lifeEnemy = 4;    
    private const int RequiredEnemyDeath = 100; //Numero requerido de enemigos muertos para activar la invocacion del jefe

    public void CountEnemyDeath()
    {
        GameManager.NumberEnemyDeath++;
        print("Enemigos muertos: " + GameManager.NumberEnemyDeath);
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("trigger entro: " + collision.tag);
        if (collision.CompareTag("ShotPlayer") || collision.CompareTag("Ulti"))
        {
            lifeEnemy--;
            //print("Enemigo 1 tiene de vida: " + lifeEnemy);
            if (lifeEnemy <= 0)
            {
                GameObject death = Instantiate(enemyDead);
                death.transform.position = gameObject.transform.position;
                Destroy(gameObject);               
                StartCoroutine(PlayAudioForDuration(death, deadSound, 0.01f, 2.8f, 1f)); //Debe ser hijo del Death para que no se quede en jerarquia
                Destroy(death, 1f);                
                CountEnemyDeath();                               
            }
        }
    }

    //Sección audio
    IEnumerator PlayAudioForDuration(GameObject parent, AudioClip clip, float start, float duration, float volume)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.SetParent(parent.transform);                           //Hace que el audio sea hijo del objeto death

        AudioSource temp = tempGO.AddComponent<AudioSource>();
        temp.volume = volume;
        temp.clip = clip;
        temp.time = start;
        temp.Play();

        yield return new WaitForSeconds(duration);

        Destroy(tempGO);
    }


    //Condiciones para el boss
    public static int NumberEnemyDeathGet() 
    {
        return GameManager.NumberEnemyDeath;
    }

    public static int RequiredEnemyDeathGet()
    {
        return RequiredEnemyDeath;
    }
}