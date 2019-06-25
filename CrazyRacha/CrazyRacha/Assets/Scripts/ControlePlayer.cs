using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlePlayer : MonoBehaviour
{
    private Rigidbody rb;
    public float aceleracao;
    private bool noChao = true, emPe = true;
    Vector3 ultimaPosicao = new Vector3();
    Quaternion ultimaRotacao = new Quaternion();
    public float velocidadeMax;
    private List<GameObject> pontoRetorno = new List<GameObject>();
    private ControleJogo controleJogo;
   
    // Start is called before the first frame update
    void Start()
    {
        controleJogo = GameObject.FindGameObjectWithTag("ControleGame").GetComponent<ControleJogo>();
        rb = GetComponent<Rigidbody>();
        ultimaPosicao = transform.localPosition;
        ultimaRotacao = transform.localRotation;

        pontoRetorno.AddRange(GameObject.FindGameObjectsWithTag("PontoRetorno"));
        print("qnt:" + pontoRetorno.Count);
        if (aceleracao == 0)
            aceleracao = 1;

        StartCoroutine(UltimaPosicao());
        
    }

    void Update()
    {
        verificar_angulo();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical < 0)
            Mover_Re(vertical);
        else if (vertical > 0)
            Mover_Frente(vertical);
        else
            Desacelera();

        if (horizontal > 0)
            Mover_Direita(horizontal);
        else if (horizontal < 0)
            Mover_Esquerda(horizontal);

    }

    void Desacelera()
    {
        print("Velocidade: " + rb.velocity.magnitude);
        if (rb.velocity.magnitude > 1)
            rb.AddForce(transform.forward * -10);
    }
    void Mover_Re(float acc)
    {
       // print("Velocidade: " + rb.velocity.magnitude);
        if (emPe && (rb.velocity.magnitude < velocidadeMax))
            rb.AddForce(transform.forward * acc * 30);

    }

    void Mover_Frente(float acc)
    {
       // print("Velocidade: " + rb.velocity.magnitude + "em pe" + emPe);
        if (emPe && (rb.velocity.magnitude > velocidadeMax * -1))
            rb.AddForce(transform.forward * acc * 30);
    }

    void Mover_Direita(float rot)
    {
        if (noChao && emPe)
            transform.Rotate(0, 1.5f, 0);

    }

    void Mover_Esquerda(float rot)
    {
        if (noChao && emPe)
            transform.Rotate(0, -1.5f, 0);
    }


    void verificar_angulo()
    {
        float x = transform.localRotation.eulerAngles.x;
        float z = transform.localRotation.eulerAngles.z;
        float limiteMin = 20;
        float limiteMax = 355;

       // print("Angulo x: " + x + "Angulo z:" + z);
        emPe = true;

        if ((x > limiteMin) && (x < limiteMax))
            emPe = false;

        if ((z > limiteMin) && (z < limiteMax))
            emPe = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Caixa"))
            controleJogo.ColisaoCaixa();

        if (collision.gameObject.CompareTag("Roda"))
        {
            controleJogo.coletarCaixa();
            Destroy(collision.gameObject);
        }

    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Pista"))
            noChao = true;
        else
            noChao = false;
    }

    IEnumerator UltimaPosicao()
    {
        while(true)
        {
            if (!noChao || !emPe)
            {
                yield return new WaitForSeconds(3);
                if (!noChao || !emPe)
                {
                    float menorPos = Vector3.Distance(pontoRetorno[0].transform.position, transform.position);
                    Vector3 pos = new Vector3();
                    Quaternion ang = new Quaternion();
                    float dist;
                    int index = 0;
                    for (int i = 0; i <= pontoRetorno.Count - 1; i++)
                    {
                        dist = Vector3.Distance(pontoRetorno[i].transform.position, transform.position);
                        //print("Distancia: " + dist);
                        if (dist < menorPos)
                        {
                            menorPos = dist;
                            index = i;
                        }
                    }
                    transform.position = pontoRetorno[index].transform.position;
                    transform.rotation = pontoRetorno[index].transform.rotation;
                }

            }
            yield return new WaitForSeconds(3);
        }
    }

}
