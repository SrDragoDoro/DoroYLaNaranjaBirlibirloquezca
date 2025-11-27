using System.Collections;
using UnityEngine;

/* Contenido
Creación de enemigos alrededor del jugador
Asignación de vida
Uso de cortina para intervalos y evitar colapsar el juego

*/

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPrefabEnemy;
    [SerializeField] private GameObject Player;
    [SerializeField] private float radioSpawn = 5f; 
    [SerializeField] private float espera = 3f;
    [SerializeField] public int life = 4;
    private bool corutineActivate; 

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            corutineActivate = !corutineActivate;

            if (corutineActivate)
            {                
                StartCoroutine("CortinaDeSpawneo");
                //Destroy(SpawnPrefabEnemy);
                print("Spawner Activado");
            }
            else
            {
                StopCoroutine("CortinaDeSpawneo");
                Destroy(SpawnPrefabEnemy);
                print("Spawner Desactivado");
            }
        }
    }    

    public void Spawner() 
    {       
        float x = Random.Range(-1f, 1f); 
        float y = Random.Range(-1f, 1f);
        
        Vector2 direccion = new Vector2(x, y).normalized; //Normalizamos los ejes "x" como "y"     
        Vector2 spawnPos = (Vector2)Player.transform.position + direccion * radioSpawn;        
        Instantiate(SpawnPrefabEnemy, spawnPos, SpawnPrefabEnemy.transform.rotation);        
    }

    IEnumerator CortinaDeSpawneo() //Uso de cortina (en Seg.) para evitar saturación de enemigos
    {
        while (corutineActivate)
        {
            Spawner();
            yield return new WaitForSeconds(espera);
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("trigger entro: " + collision.tag);
        if (collision.CompareTag("ShotPlayer")) //revisar collision.Tag == "Bullet" hay otros!
        {
            life--;
            if (life <= 0) Destroy(gameObject);
        } 
    }
}
