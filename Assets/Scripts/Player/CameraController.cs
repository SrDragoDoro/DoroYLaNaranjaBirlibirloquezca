using UnityEngine;

public class C_SeguirCamara : MonoBehaviour
{
    public Transform Player; //Ubicación del player en la escena
    public float suavizadoDeMovimiento = 5f; 

    void Update()
    {
        if (Player == null) return;
        Vector2 nuevaPosicion = new Vector2(Player.position.x, Player.position.y);
        transform.position = Vector2.Lerp(transform.position, nuevaPosicion, suavizadoDeMovimiento * Time.deltaTime);
    }
}