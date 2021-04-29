using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aumentaVida : MonoBehaviour
{
    public Controle controle;

   
       

   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            controle.starthealth += 25;
            controle.vida += 25;
            Destroy(gameObject);
        }
    }
}
