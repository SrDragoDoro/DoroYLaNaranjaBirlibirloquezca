using UnityEngine;

public class C_Personaje : MonoBehaviour
{
    [SerializeField]  private GameObject Doro;

    [SerializeField] private float velocidad = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        Controles();
    }

    public void Controles()
    {
        float ejeX = Input.GetAxis("Horizontal");
        float ejey = Input.GetAxis("Vertical");
        transform.Translate(ejeX * velocidad * Time.deltaTime, 0, ejey * velocidad * Time.deltaTime);
    }
}
