using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

/*Contenido
Cortinas para intervalos en ataques enemigos
Creación de objetos de disparo
*/

public class EnemyOneAttack : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip e1shotSound;
    [SerializeField] private GameObject BulletPrefabEnemy;    
    [SerializeField] private float intervalo = 1f;
    private bool attacking = false; //Control de bucle, caso contrario las balas no se moverán

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!attacking) 
        {
            audioSource.PlayOneShot(e1shotSound, 0.25f);
            //StartCoroutine(PlayAudioForDuration(e1shotSound, 0.0f, 5.3f, 0.4f));
            StartCoroutine(CoroutineAttack());
        }
    }

    IEnumerator CoroutineAttack()
    {
        attacking = true;
        
        ShootProyectile();
        yield return new WaitForSeconds(intervalo);

        attacking = false;
    }

    public void ShootProyectile()
    {
        GameObject bullet = Instantiate(BulletPrefabEnemy, transform);                   //-> Crear bala
        bullet.transform.position = transform.position;                                  //Coloca la bala en la posición del enemigo
        bullet.transform.up = PlayerLocate();                                            //Ajusta el ángulo de la bala a la ubicación del player
    }

    public Vector2 PlayerLocate()
    {
        Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position; //Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;

        return direction;
    }

    IEnumerator PlayAudioForDuration(AudioClip clip, float start, float duration, float volume)
    {
        GameObject tempGO = new GameObject("TempAudio");        //Crea un objeto temporal
        AudioSource temp = tempGO.AddComponent<AudioSource>();  //Agrega un componente AudioSource al objeto temporal

        temp.volume = volume;
        temp.clip = clip;
        temp.time = start;
        temp.Play();

        yield return new WaitForSeconds(duration);

        Destroy(tempGO);   //Para no ocupar memoria 
    }

}
