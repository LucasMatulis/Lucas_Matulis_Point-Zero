using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediKit : MonoBehaviour
{
    public Controle controle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && controle.vida< controle.starthealth)
        {
            controle.vida += 20;
            Destroy(gameObject);
        }
    }
}
