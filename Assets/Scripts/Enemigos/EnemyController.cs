using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Contenido
Control de Spawn
Creación de enemigos alrededor del jugador
Asignación de vida del enemigo
Uso de cortina para intervalos a modo de prevención de saturación
Condiciones de comparación por colisión por Trigger (Destrucción por vida)
*/

public class EnemyController : MonoBehaviour
{  
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject[] EnemyPrefabEnemy;
    [SerializeField] private const float espera = 8f;   //Tiempo de espera entre spawneos

    public const int Maxquantrity = 125;               //Cantidad maxima de enemigos en escena
    public static int currentQuantity;                  //Cantidad actual de enemigos en escena
    
    private bool spawnActivate = false;
    private Coroutine currentSpawnRoutine;              //Cortina de refetencia    

    private void Start()
    {
        currentQuantity = 0;        
    }

    void Update()
    {        
        SpawnControll();
    }

    public void SpawnControll()
    {
        if (Input.GetKeyDown(KeyCode.F8))
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

    IEnumerator CortinaDeSpawneo() //Uso de cortina (en Seg.) para evitar saturación de enemigos
    {
        while (currentQuantity < Maxquantrity)
        {            
            SpawnEnemy();
            yield return new WaitForSeconds(espera);
        }
        print("Máximo de enemigos alcanzado.");
    }

    public void SpawnEnemy()
    {
        if (currentQuantity > Maxquantrity)
        {
            print("Limite alcanzado.");
            return;
        }

        BoundsInt bounds = tilemap.cellBounds;                                               // Obtener los límites del Tilemap

        int x = Random.Range(bounds.xMin, bounds.xMax);
        int y = Random.Range(bounds.yMin, bounds.yMax);

        Vector3Int cellPos = new Vector3Int (x, y, 0);                                       // Posición de la celda en el Tilemap

        if (tilemap.HasTile(cellPos))                                                        // Si el tile existe (no es vacío)
        {
            Vector3 worldPos = tilemap.CellToWorld(cellPos);                                 // Convertir a posición del mundo
            int cantidad = Random.Range(2, 6);
            for (int e = 0; e < cantidad; e++) 
            {
                if (currentQuantity > Maxquantrity)
                    return; // no crear más enemigos

                GameObject enemyPrefab = EnemyPrefabEnemy[Random.Range(0, EnemyPrefabEnemy.Length)];

                Vector3 offset = new Vector3 // Desplazamiento aleatorio para evitar superposición
                (
                    Random.Range(-0.3f, 0.3f), //x
                    Random.Range(-0.3f, 0.3f), //y
                    0                          //z 
                );

                Instantiate(enemyPrefab, worldPos + offset, Quaternion.identity, gameObject.transform); // Crear enemigo como hijo de este objeto
                currentQuantity++;
                print("Cantidad actual: " + currentQuantity);
            };
        }
    }
}