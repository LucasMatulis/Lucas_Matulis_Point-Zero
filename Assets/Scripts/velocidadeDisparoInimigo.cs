using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocidadeDisparoInimigo : MonoBehaviour
{
    private float velocidade = -500;
    private int x = 1;
    private Animator anim;
    public Disparo disparo;
    float tempo = 2.0f;
   
 
    void Start()
    {
        anim = GetComponent<Animator>();
        velocidade *= x;
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * velocidade);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "parede")
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * velocidade);
            Destroy(gameObject);
        }
    }
    public void Flip(int _x)
    {
        x = _x;
    }
}
