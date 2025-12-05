using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/* Contenido
Control de Spawn
Creaci�n de enemigos alrededor del jugador
Asignaci�n de vida del enemigo
Uso de cortina para intervalos a modo de prevenci�n de saturaci�n
Condiciones de comparacion por colisi�n por Trigger (Destrucci�n por vida)
*/

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPrefabEnemy;
    [SerializeField] private GameObject Player;
    [SerializeField] private const float radioSpawn = 6f;
    private bool BossHave = false;

    void Update()
    {
        if (LifeEnemy.NumberEnemyDeathGet() >= LifeEnemy.RequiredEnemyDeathGet() && !BossHave) //medoto est�tico para obtener el n�mero de enemigos muertos y el requerido
        {
            Spawner();
            BossHave = true;
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
}