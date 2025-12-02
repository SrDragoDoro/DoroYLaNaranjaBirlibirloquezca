using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
/*Contenido
Control de movimento (tipo MOBA)
Creación de disparo y acción de disparo
Vida del player
*/
[SerializeField] public struct PlayerStatistics //Sin asignar
{
    public float Speed;
    public float lifePlayer;
    public void Statistics(float _speed, float _lifePlayer)
    {
        Speed = _speed;
        lifePlayer = _lifePlayer;
    }
}

    public class PlayerController : MonoBehaviour
{     
    [SerializeField] private float Speed;
    [SerializeField] private float lifePlayer = 50f;
    private bool move = false; //Simula un swichs de cambio para no hacer un movimiento constante al dar click
    private Vector2 destiny;

    [SerializeField] private Animator PlayerWalk;

    void Update()
    {        
        if (move) //Si es TRUE hay movimiento 
        {
            MoveToDirection();
            PlayerWalk.SetBool("Caminar", true);
        }
        else
        {
            PlayerWalk.SetBool("Caminar", false);
        }

        if (Input.GetMouseButton(0))
        {
            SetDestiny();
        }
    }  

    public void SetDestiny() //Método para obtener las coordenadas del mouse
    {
        destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        move = true;

        if (destiny.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);   // izquierda
        else
            transform.localScale = new Vector3(1, 1, 1);    // derecha
    }

    public void MoveToDirection()
    {      
        Vector3 moveDirection = (destiny - (Vector2)transform.position).normalized; 
        transform.position += moveDirection * Speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, destiny) <= 0.1f)
            move = false;
    }    

    public void PlayerLife(float damage)
    {
        lifePlayer -= damage;
        print("Tu vida actual es: " + lifePlayer);
        if (lifePlayer <= 0)
        {
            print("Has muerto D:");
            print("¡Tu universo ha sido invadido!");
        }
    }

}
