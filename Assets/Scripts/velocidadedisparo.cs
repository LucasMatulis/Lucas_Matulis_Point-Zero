using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class velocidadedisparo : MonoBehaviour
{
    private float velocidade = 1000;
    private int x = 1;

    void Start()
    {
        velocidade *= x;
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * velocidade);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Inimigo" || other.gameObject.tag == "parede")
        {
            Destroy(gameObject);
        }
    }
    public void Flip(int _x)
    {
         x = _x;
    }   
}
