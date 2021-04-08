using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public float range;

    public float vida = 100;

    public Transform target;

    public bool detected = false;

    public Vector2 direcao;

    public GameObject Alarm;

    public GameObject Arma;

    public GameObject Bullet;

    public Transform Shootpoint;

    public float force;

    public float firerate;

    public float nextTimeShoot = 0;

    public AudioSource tiro;


    void Start()
    {
        vida = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = target.position;

        direcao = targetPos - (Vector2)transform.position;

        RaycastHit2D rayinfo = Physics2D.Raycast(transform.position, direcao, range);

        if (rayinfo)
        {
            if (rayinfo.collider.gameObject.tag == "Player")
            {
                if (detected==false)
                {
                    detected = true;
                    Alarm.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else
            {
                if (detected==true)
                {
                    detected = false;
                    Alarm.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
        if (detected)
        {
            Arma.transform.up = direcao;
            if (Time.time > nextTimeShoot)
            {
                nextTimeShoot = Time.time + 1 / firerate;
                Shoot();
                tiro.Play();
            }
        }

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        GameObject bulletins = Instantiate(Bullet, Shootpoint.position, Quaternion.identity);
        bulletins.GetComponent<Rigidbody2D>().AddForce(direcao * force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="tiro aliada")
        {
            vida -= 10;
        }
    }
}
