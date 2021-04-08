using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoInimigo : MonoBehaviour
{
    public Controle controle;
    [SerializeField]
    private Transform posicaoDetiro;
    [SerializeField]
    private GameObject tiroprefab;
    public bool Atirar = false;
    public float FireRate;
    public float nextTimeToFIre = 0;
    public AudioSource tiro;



    void Update()
    {
        if (!Atirar)
        {
            nextTimeToFIre += Time.deltaTime;
        }

        if (Atirar)
        {
            if (Time.time > nextTimeToFIre)
            {
                nextTimeToFIre += 1;
                Atira();
                tiro.Play();
            }
        }
    }
      
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Atirar = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Atirar = false;
        }
    }
    private void Atira()
    {
        int X = 1;
        GameObject tiro = Instantiate(tiroprefab, transform.position, posicaoDetiro.rotation);
        Vector3 direita = new Vector3(X, 1, 1);
        tiro.transform.localScale = direita;
        tiro.GetComponent<velocidadeDisparoInimigo>().Flip(X);
    }
}

