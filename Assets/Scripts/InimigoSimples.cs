using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoSimples : MonoBehaviour
{
    private float vida;
    private float vidaBase = 50;
    public Image healthBar;
    public Image healthBar2;
    public Controle controle;

    void Start()
    {
        vida = vidaBase;
        healthBar.enabled = false;
        healthBar2.enabled = false;
    }

    
    void Update()
    {
        healthBar.fillAmount = vida / vidaBase;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
        if (vida < vidaBase)
        {
            healthBar.enabled = true;
            healthBar2.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "tiro aliada")
        {
            vida -= 10;
        }
    }
}
