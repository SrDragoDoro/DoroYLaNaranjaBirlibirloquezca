using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;

    [SerializeField] private Transform weaponLeft;
            [SerializeField] private Transform FirePointLeft;
    [SerializeField] private Transform weaponRight;
            [SerializeField] private Transform FirePointRight;

    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Siempre apuntar las armas al mouse
        RotateWeaponsToMouse();

        if (Input.GetMouseButtonDown(1))
            ShootProyectile();
    }

    private void RotateWeaponsToMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
         
        GameObject bulletLeft = Instantiate(BulletPrefab);                          //-> Crear 
        bulletLeft.transform.position = (Vector2)FirePointLeft.position;         //Coloca la bala en la posición del player
        bulletLeft.transform.up = normalizeShootDirection;                        //Ajusta el ángulo de la bala a la ubicación del mouse
       
        GameObject bulletRight = Instantiate(BulletPrefab);                         //-> Crear 
        bulletRight.transform.position = (Vector2)FirePointRight.position;       //Coloca la bala en la posición del player
        bulletRight.transform.up = normalizeShootDirection;                       //Ajusta el ángulo de la bala a la ubicación del mouse        
    }




}
