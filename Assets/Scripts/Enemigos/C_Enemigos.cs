using UnityEngine;
using System.Collections;

public class C_Enemigos : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPrefabEnemy;
    [SerializeField] private GameObject Doro;
    [SerializeField] private float radioSpawneo = 5f; 
    [SerializeField] private float espera = 3f;
    private bool activarCreacion;   


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            activarCreacion = !activarCreacion;

            if (activarCreacion)
            {                
                StartCoroutine(CortinaDeSpawneo());
                print("Spawner Activado");
            }
            else
            {
                StopCoroutine(CortinaDeSpawneo());
                Destroy(SpawnPrefabEnemy);
                print("Spawner Desactivado");
            }
        }
    }

    public void Spawner()
    {       
        float x = Random.Range(0f, 2f);
        float y = Random.Range(0f, 2f);
        
        Vector2 direccion = new Vector2(x, y).normalized;        
        Vector2 spawnPos = (Vector2)Doro.transform.position + direccion * radioSpawneo;        
        Instantiate(SpawnPrefabEnemy, spawnPos, SpawnPrefabEnemy.transform.rotation);        
    }

    IEnumerator CortinaDeSpawneo()
    {
        while (activarCreacion)
        {
            Spawner();
            yield return new WaitForSeconds(espera);
        }       
    }
}
