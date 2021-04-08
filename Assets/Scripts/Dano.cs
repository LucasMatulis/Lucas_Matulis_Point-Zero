using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    public Controle controle;
    public float dano;
    public float tempo;
    public bool ativada;
    private BoxCollider2D bc;


    public void Start()
    {
        controle = FindObjectOfType<Controle>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ativada = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ativada = false;
        }
    }

    void Update()
    {
        if (ativada)
        {
            controle.vida -= dano * Time.deltaTime * tempo;
            controle.velocidade = 4;
            controle.forcapulo = 1400f * controle.gravidade;
        }
        else
        {
            controle.forcapulo = 2000f * controle.gravidade;
        }
    }
}
