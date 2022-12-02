using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public int FuerDeSalto;
    public int VelocidadDeMovimiento;
    bool EnElPiso = false;
    private AudioSource Sonido;
    public AudioClip Salto;
    public float SegIncremento;
    float TiempoActual;

    // Start is called before the first frame update
    void Start()
    {
        Sonido = GetComponent<AudioSource>();
        TiempoActual = SegIncremento * 60;
    }

    // Update is called once per frame
    void Update()
    {
        TiempoActual -= 1f;
        if (Input.GetKeyDown("space") && EnElPiso)
        {
            Sonido.PlayOneShot(Salto, 1f);
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, FuerDeSalto));
        }

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(VelocidadDeMovimiento,
            this.GetComponent<Rigidbody2D>().velocity.y
            );
        if (TiempoActual < 0)
        {
            VelocidadDeMovimiento += 1;
            TiempoActual = SegIncremento * 60;
        }
    }

    private void OnTriggerEnter2D(Collider2D c1)
    {
        EnElPiso = true;
        if(c1.tag == "Obstaculo")
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnElPiso = false;
    }
}
