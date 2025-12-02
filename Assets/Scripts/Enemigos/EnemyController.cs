using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/* Contenido
Control de Spawn
Creación de enemigos alrededor del jugador
Asignación de vida del enemigo
Uso de cortina para intervalos a modo de prevención de saturación
Condiciones de comparación por colisión por Trigger (Destrucción por vida)
*/

public class EnemyController : MonoBehaviour
{    
    [SerializeField] private GameObject SpawnPrefabEnemy;
    [SerializeField] private GameObject Player;
    [SerializeField] private float radioSpawn = 5f; 
    [SerializeField] private float espera = 3f;    
    private bool spawnActivate = false;
    private Coroutine currentSpawnRoutine; //Cortina de refetencia

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnActivate = !spawnActivate;

            switch (spawnActivate)
            {
                case true:
                    currentSpawnRoutine = StartCoroutine(CortinaDeSpawneo()); //Hacemos uso de la referencia (ahora no está vacia)
                    print("Spawner Activado");
                    break;

                case false:                    
                    if (currentSpawnRoutine != null)           // Detiene usando la referencia 
                    {
                        StopCoroutine(currentSpawnRoutine);
                        currentSpawnRoutine = null;         // Limpia la referencia
                    }
                    print("Spawner Desactivado");
                    break;
                default:                    
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
        while (spawnActivate)
        {
            Spawner();
            yield return new WaitForSeconds(espera);
        }
    }
}
