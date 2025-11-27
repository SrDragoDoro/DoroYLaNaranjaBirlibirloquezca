using UnityEngine;

public class ControlPersonaje : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
  
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            ShootProyectile();

        if (Input.GetMouseButton(0))
            MoveToDirection();
    }

    public void MoveToDirection()
    {
        Vector2 worldPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 moveDirection = (worldPositon - (Vector2)transform.position).normalized;

        transform.position += moveDirection * Speed * Time.deltaTime;
    }

    public void ShootProyectile()
    {        
        Vector2 mousePosition = Input.mousePosition;                            //-> obtener posicon del mouse respecto a la resolucion de la patanlla
        Vector2 worldPositon = Camera.main.ScreenToWorldPoint(mousePosition);   // convirtiendo la posicon del mouse a la posicion en el mundo
        Vector2 shootDirection = worldPositon - (Vector2)transform.position;    //calculdo la direccion de disparo
        Vector2 normalizeShootDirection = shootDirection.normalized;            //normalizando la direccion
        GameObject bullet = Instantiate(BulletPrefab);                          //-> Crear 
        bullet.transform.position = transform.position;            //Coloca la bala en la posición del player
        bullet.transform.up = normalizeShootDirection;             //Ajusta el ángulo de la bala a la ubicación del mouse
        print(worldPositon);                                       //Imprime la ubicación del mouse al dar click
    }
}
