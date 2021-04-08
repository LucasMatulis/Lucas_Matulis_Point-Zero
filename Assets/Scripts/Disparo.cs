using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    private Animator anim;
    public Controle controle;
    [SerializeField]
    private Transform posicaoDetiro;
[SerializeField]
        private GameObject tiroprefab;
    // Update is called once per frame
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && controle.Arma && controle.municao > 0)
        {         
            Atira();
            controle.municao -= 1;
        }
    }
    private void Atira()
    {
        int X = 0;
        X = controle.direcaoJogador;
        GameObject tiro = Instantiate(tiroprefab, transform.position, posicaoDetiro.rotation);
        Vector3 direita = new Vector3(X, 1, 1);
        tiro.transform.localScale=direita;
        tiro.GetComponent<velocidadedisparo>().Flip(X);
    }
}




































