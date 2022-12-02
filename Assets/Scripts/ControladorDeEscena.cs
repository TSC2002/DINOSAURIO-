using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorDeEscena : MonoBehaviour
{
    public GameObject Jugador;
    public Camera CamaraDelJuego;
    public GameObject[] BloquePrefab;
    public float PunteroDeJuego;
    public float LugarSeguroDeGeneracion = 12;
    public TextMeshProUGUI TextoDeJuego;
    public bool Perdiste;
    private AudioSource Sonido;
    public AudioClip Golpe;
    public AudioSource Musica;


    // Start is called before the first frame update
    void Start()
    {
        PunteroDeJuego = -7;
        Perdiste = false;
        Sonido = GetComponent<AudioSource>();
        Musica.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Jugador!= null) { 
        CamaraDelJuego.transform.position = new Vector3(
           Jugador.transform.position.x,
           CamaraDelJuego.transform.position.y,
           CamaraDelJuego.transform.position.z
            );
            TextoDeJuego.text = "Puntaje: " + Mathf.Floor(Jugador.transform.position.x);


        }
        else
        {
            if (!Perdiste)
            {
                Perdiste = true;
                TextoDeJuego.text += "\nSe termino el juego! \nPresione R para reaparecer";
                Sonido.PlayOneShot(Golpe, 1f);
                Musica.Stop();
            }
            if (Perdiste)
            {
                if (Input.GetKeyDown("r"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        while(Jugador!=null && PunteroDeJuego<Jugador.transform.position.x + LugarSeguroDeGeneracion)
        {
            int IndiceBloque = Random.Range(0, BloquePrefab.Length - 1);
            if (PunteroDeJuego < 0)
            {
                IndiceBloque = 5;
            }
            GameObject ObjetoBloque = Instantiate(BloquePrefab[IndiceBloque]);
            ObjetoBloque.transform.SetParent(this.transform);
            Bloque bloque = ObjetoBloque.GetComponent<Bloque>();
            ObjetoBloque.transform.position = new Vector2(
                PunteroDeJuego + bloque.Tamaño / 2,
                0
                );
            PunteroDeJuego += bloque.Tamaño;
        }

    }
}
