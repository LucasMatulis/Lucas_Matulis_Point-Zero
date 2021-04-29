using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arma : MonoBehaviour
{
    public Controle controle;




    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            controle.Arma = true;
            controle.municao += 45;
            Destroy(gameObject);
        }
    }
}
