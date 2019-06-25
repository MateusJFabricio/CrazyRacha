using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControleJogo : MonoBehaviour
{
    private int vidaPlayer, vidaBoss;
    private int fase, itens;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        vidaPlayer = 3;
        vidaBoss = 10;
        fase = 1;
        itens = 0;
        atualizaText();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProximaFase()
    {
        fase = 2;
        vidaPlayer = 10;
        SceneManager.LoadScene("CenaPista2");
    }

    void atualizaText()
    {
        text.text = "Fase: " + fase + "\n" + "Vidas: " + vidaPlayer + " - Itens: " + itens;
    }

    public void coletarCaixa()
    {
        itens++;
        atualizaText();
    }

    public void ColisaoCaixa()
    {
        vidaPlayer -= 1;
        atualizaText();
        if (vidaPlayer == 0)
            reiniciarFase();

    }

    private void reiniciarFase()
    {
        if (fase == 1)
        {
            SceneManager.LoadScene("CenaPista1");
            vidaPlayer = 3;
            atualizaText();
        }
        else
        {
            SceneManager.LoadScene("CenaPista2");
            vidaPlayer = 10;
            vidaBoss = 10;
            atualizaText();
        }
    }
}
