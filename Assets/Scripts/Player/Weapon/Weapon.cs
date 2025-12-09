/*
Contenido:
Control de disparo del arma del jugador
Creación de proyectiles en la dirección del mouse
Contenedor de balas
*/

using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletParent; //Padre de las balas
    [SerializeField] private GameObject BulletPrefab; //Hija de las balas

    [SerializeField] private Transform weaponLeft;
            [SerializeField] private Transform FirePointLeft;
    [SerializeField] private Transform weaponRight;
            [SerializeField] private Transform FirePointRight;
    private void Start()
    {
        bulletParent = GameObject.Find("BulletContainer");
    }

    void Update()
    {
        // Siempre apunta las armas al mouse
        RotateWeaponsToMouse();

        if (Input.GetMouseButtonDown(1))
            ShootProyectileTriple();
    }

    private void RotateWeaponsToMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Posición del mouse en el mundo

        // Dirección hacia el mouse
        Vector2 dirLeft = mousePos - (Vector2)weaponLeft.position;
        Vector2 dirRight = mousePos - (Vector2)weaponRight.position;

        // Rota armas apuntando hacia el mouse
        weaponLeft.up = dirLeft.normalized;
        weaponRight.up = dirRight.normalized;
    }

    public void ShootProyectile()
    {
        Vector2 mousePosition = Input.mousePosition;                            //-> obtener posicon del mouse respecto a la resolucion de la patanlla
        Vector2 worldPositon = Camera.main.ScreenToWorldPoint(mousePosition);   // convirtiendo la posicon del mouse a la posicion en el mundo
        Vector2 shootDirection = worldPositon - (Vector2)transform.position;    //calculdo la direccion de disparo
        Vector2 normalizeShootDirection = shootDirection.normalized;            //normalizando la direccion

        GameObject bulletLeft = Instantiate(BulletPrefab, bulletParent.transform);                          //-> Crear 
        bulletLeft.transform.position = (Vector2)FirePointLeft.position;         //Coloca la bala en la posición del player
        bulletLeft.transform.up = normalizeShootDirection;                        //Ajusta el ángulo de la bala a la ubicación del mouse


        GameObject bulletRight = Instantiate(BulletPrefab, bulletParent.transform);                         //-> Crear 
        bulletRight.transform.position = (Vector2)FirePointRight.position;       //Coloca la bala en la posición del player
        bulletRight.transform.up = normalizeShootDirection;                       //Ajusta el ángulo de la bala a la ubicación del mouse
    }

    public void ShootProyectileDoble()
    {
        Vector2 mousePosition = Input.mousePosition;                            //-> obtener posicon del mouse respecto a la resolucion de la patanlla
        Vector2 worldPositon = Camera.main.ScreenToWorldPoint(mousePosition);   // convirtiendo la posicon del mouse a la posicion en el mundo
        Vector2 shootDirection = (worldPositon - (Vector2)transform.position).normalized;    //calculdo la direccion de disparo
        
        int bullets = 2; // Numero de balas a disparar por cada arma       

        for (int i = 0; i < bullets; i++)
        {            
            Vector2 offset = new Vector2(0, Random.Range(-0.4f, 0.4f));
                        
            GameObject bulletL = Instantiate(BulletPrefab, bulletParent.transform);
            bulletL.transform.position = (Vector2)FirePointLeft.position + offset;
            bulletL.transform.up = shootDirection;
                        
            GameObject bulletR = Instantiate(BulletPrefab, bulletParent.transform);
            bulletR.transform.position = (Vector2)FirePointRight.position + offset;
            bulletR.transform.up = shootDirection;
        }
    }

    public void ShootProyectileTriple()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (worldPosition - (Vector2)transform.position).normalized;

        float spreadAngle = 10f; // grados a cada lado

        SpawnBulletWithAngle(direction, -spreadAngle);  // Bala izquierda
        SpawnBulletWithAngle(direction, 0f);            // Bala central    
        SpawnBulletWithAngle(direction, spreadAngle);   // Bala derecha
    }
    private void SpawnBulletWithAngle(Vector2 direction, float angle)
    {
        GameObject bulletL = Instantiate(BulletPrefab, bulletParent.transform);
        bulletL.transform.position = FirePointLeft.position;                 // Usa el FirePointLeft o uno central
        Vector2 newDirL = Quaternion.Euler(0, 0, angle) * direction;         // Rotar la dirección
        bulletL.transform.up = newDirL;

        GameObject bulletR = Instantiate(BulletPrefab, bulletParent.transform);
        bulletR.transform.position = FirePointRight.position;                // Usa el FirePointLeft o uno central
        Vector2 newDirR = Quaternion.Euler(0, 0, angle) * direction;         // Rotar la dirección
        bulletR.transform.up = newDirR;
    }























}
