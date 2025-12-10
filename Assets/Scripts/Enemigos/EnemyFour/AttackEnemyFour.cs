using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AttackEnemyFour : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip e4shotSound;
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
            audioSource.PlayOneShot(e4shotSound, 0.25f);
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
        bullet.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        bullet.transform.position = transform.position;                                  //Coloca la bala en la posición del enemigo
        bullet.transform.up = PlayerLocate();                                            //Ajusta el ángulo de la bala a la ubicación del player
    }

    public Vector2 PlayerLocate()
    {
        Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position; //Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;

        return direction;
    }
}
