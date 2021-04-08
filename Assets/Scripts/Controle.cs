using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controle : MonoBehaviour
{
    private Animator anim;
    [Header("Vida")]
    public float vida;
    public Image healthBar;
    public float starthealth = 100;
    public float descontodevida;
    public Text vidabarra;

    [Header("Movimento")]
    public Rigidbody2D rb2d;
    public float velocidade;
    public float inputX ;
    public float DistanciaChao;
    private bool PodeAndar = true;
    public bool correndo;
    public float gravidade = 1;
    public AudioSource andar;
    public bool andando;

    [Header ("Pulo")]
    public float wallSlideSpeed;
    public float forcapulo;
    public float DistanciaParede;
    public float wallJumpForce;
    public Vector2 direcaopulo;
    public Vector2 Corridapulo;
    public int direcaoJogador = 1;
    public float runjumpforce;
    public AudioSource pulo;

    [Header("Dano")]
    public float TrapJumpForce;

    [Header("Gravidade")]
    public bool powerUpAntiGravidade = false;
    public float escala;
    public Image iconeGravidade1;
    public Image iconeGravidade2;
    public Image iconeGravidade3;
    public Image iconeGravidade4;

    [Header("Disparo")]
    public bool Arma = false;
    public Image armahud;
    public float municao = 0;
    public Text AmmoInfo;


    [Header ("Checks")]
    public bool groundCheck;
    public bool wallCheck;
    public bool wallSlinding;
    public bool viradoDireita = false;
    public bool jump = false;
    public bool movendo;
    [Header ("Transforms e outros")]
    public Transform pe;
    public Transform mao;

    

    public LayerMask Solido;

   
   


    void Start()
    {
        anim = GetComponent<Animator>();
        vida = starthealth;

    }

    
    void Update()
    {
        groundCheck = Physics2D.OverlapCircle(pe.position, DistanciaChao, Solido);
        wallCheck = Physics2D.Raycast(mao.position, transform.right, DistanciaParede, Solido);

        BarraVida();

        Andar();

        WallSlide();

        Pulo();

        CheckWallSling();
        
        Disparo();

        AntiGravidade();

    }

    private void CheckWallSling()
    {
        if (wallCheck && !groundCheck && rb2d.velocity.y * gravidade < 0 && inputX != 0)
        {
            wallSlinding = true;
            
        }
        else
        {
            wallSlinding = false;
        }
    }

    private void Disparo()
    {
        AmmoInfo.text = "X " + municao;
        if (Arma)
        {
            armahud.enabled = true;
            AmmoInfo.enabled = true;

        }
        else
        {
            armahud.enabled = false;
            AmmoInfo.enabled = false;
        }
    }


    IEnumerator StopMove()
    {
        PodeAndar = false;

        

        yield return new WaitForSeconds(.3f);

        

        PodeAndar = true;
    }

    IEnumerator StopMoveTrap()
    {
        PodeAndar = false;



        yield return new WaitForSeconds(1f);



        PodeAndar = true;
    }

    private void BarraVida()
    {
        descontodevida = vida - starthealth;

        healthBar.fillAmount = vida / starthealth;

        if (vida <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (vida > starthealth)
        {
            vida -= descontodevida;
        }

        string palavra = (vida).ToString("f0");
        vidabarra.text = palavra + "/" + starthealth;
    }
    
    private void Andar()
    {
        if (PodeAndar)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            rb2d.velocity = new Vector2(inputX * velocidade * gravidade, rb2d.velocity.y);
        }

        if (rb2d.velocity.x != 0)
        {
            andando = true;
        }
        else
        {
            andando = false;
        }

        if (Input.GetAxis("Horizontal") != 0 && groundCheck)
        {
            if (!andar.isPlaying)
            {
                if (andar.isPlaying == false)
                {
                    if (andando)
                    {
                        andar.Play();
                    }
                    else
                    {
                        andar.Stop();
                    }
                }
            }

        }

        if (inputX > 0 && !viradoDireita)
        {
            Flip();
            direcaoJogador = 1;
        }
        else if (inputX < 0 && viradoDireita)
        {
            Flip();
            direcaoJogador = -1;
        }

        if (inputX != 0)
        {
            anim.SetTrigger("andar");
        }
        else
        {
            anim.SetTrigger("parado");
        }

        if (Input.GetButton("Fire3"))
        {
            velocidade = 20;
            correndo = true;
        }
        else
        {
            velocidade = 10;
            correndo = false;
        }
    }

    private void WallSlide()
    {
        if (wallSlinding)
        {
            correndo = false;
            if (rb2d.velocity.y * gravidade < -wallSlideSpeed * gravidade)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlideSpeed * gravidade);
            }
        }

        if (wallSlinding && Input.GetKeyDown("space"))
        {
            pulo.Play();

            Vector2 force = new Vector2(wallJumpForce * direcaopulo.x * -direcaoJogador * gravidade, wallJumpForce * direcaopulo.y* gravidade);

            rb2d.velocity = Vector2.zero;

            rb2d.AddForce(force, ForceMode2D.Impulse);

            StartCoroutine("StopMove");
        }
    }

    private void Pulo()
    {
        if (Input.GetKeyDown("space") && groundCheck && !correndo)
        {
            jump = true;
            pulo.Play();
        }

        if (jump)
        {      
            rb2d.AddForce(new Vector2(0f, forcapulo));
            jump = false;            
        }
        if(correndo && groundCheck && Input.GetKeyDown("space"))
        {
            pulo.Play();

            Vector2 force = new Vector2(runjumpforce * Corridapulo.x * direcaoJogador * gravidade, runjumpforce * Corridapulo.y * gravidade);

            rb2d.velocity = Vector2.zero;

            rb2d.AddForce(force, ForceMode2D.Impulse);

            StartCoroutine("StopMove");
        }
    }

    void Flip()
    {
        viradoDireita = !viradoDireita;

        Vector3 escala = transform.localScale;

        escala.x *= -1;

        transform.localScale = escala;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Raio")
        {
            Vector2 force = new Vector2(wallJumpForce * direcaopulo.x * -direcaoJogador* gravidade, TrapJumpForce * direcaopulo.y* gravidade);

            rb2d.velocity = Vector2.zero;

            rb2d.AddForce(force, ForceMode2D.Impulse);

            StartCoroutine("StopMoveTrap");

            vida -= 10;
        }
        
        if (collision.gameObject.tag == "tiro")
        {
            vida -= 10;
        }

        if (collision.gameObject.tag == "AntiGravidade")
        {
            powerUpAntiGravidade = true;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Saida")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void AntiGravidade()
    {
        if (powerUpAntiGravidade)
        {
            iconeGravidade1.enabled = true;
            iconeGravidade2.enabled = true;
            iconeGravidade3.enabled = true;
            iconeGravidade4.enabled = true;
        }
        else
        {
            iconeGravidade1.enabled = false;
            iconeGravidade2.enabled = false;
            iconeGravidade3.enabled = false;
            iconeGravidade4.enabled = false;
        }

        if(powerUpAntiGravidade && Input.GetKeyDown("e"))
        {
            gravidade *= -1;

            escala += 180 * gravidade;

            rb2d.gravityScale *= -1;

            transform.localRotation = Quaternion.Euler(0, 0, escala);
        }
    }

}
