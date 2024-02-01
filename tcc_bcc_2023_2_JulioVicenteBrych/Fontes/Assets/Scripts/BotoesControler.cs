using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotoesControler : MonoBehaviour
{
    public GameObject[] botoesPainel;
    public GameObject[] botoesMarcador;
    public GameObject[] textos;
    public Material MaterialPadrao;
    public Material MaterialAcionado;
    public GameObject PainelMao;
    public GameObject Rocha;
    private GameObject cam;
    private Painel painel;
    private int contexto = 1;
    private Linhas linhas;
    //private GameObject[] botoesPivo;
    private bool blocoAtivado;
    private bool permitirInteracao = false;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("CameraObjeto");
        painel = GameObject.FindObjectOfType<Painel>();
        //botoesPivo = GameObject.FindGameObjectsWithTag("MarcadorPivo");
        linhas = GameObject.FindObjectOfType<Linhas>();
    }
    void Update()
    {
     foreach(GameObject pivo in botoesMarcador)
        {
            pivo.transform.LookAt(cam.transform, Vector3.down);
        }
    }
    private void mudaMaterial(GameObject botao, bool acionado)
    {
        Renderer[] renders = botao.GetComponentsInChildren<Renderer>();
        if (acionado)
        {
            renders[0].material = MaterialAcionado;
        }
        else
        {
            renders[0].material = MaterialPadrao;
        }
    }
    public void apertaBotao(GameObject botao, bool acionado)
    {
        Transform prtMovel = botao.transform.GetChild(0);
        Vector3 novoPos = new Vector3(prtMovel.localPosition.x, prtMovel.localPosition.y, prtMovel.localPosition.z);
        if (acionado)
        {
            novoPos.z = 0.001f;
            prtMovel.localPosition = novoPos;
        }
        else
        {
            novoPos.z = 0.01f;
            prtMovel.localPosition = novoPos;
        }
    }
    public void AcionaBotaoMarcador(int indice)
    {
        if(permitirInteracao)
        {
            if (indice == 0)
            {
                foreach (GameObject obj in botoesMarcador)
                {
                    apertaBotao(obj, false);
                    mudaMaterial(obj, false);
                }
            }
            else
            {
                if (indice <= botoesMarcador.Length && indice > 0)
                {
                    if (!blocoAtivado || indice == 2)
                    {
                        apertaBotao(botoesMarcador[indice - 1], true);
                        mudaMaterial(botoesMarcador[indice - 1], true);
                        painel.MudaTexto(contexto, indice);
                    }
                }
            }
        }
    }
    public void AcionaBotaoPainel(int indice)
    {
        if (indice == 0)
        {
            foreach (GameObject obj in botoesPainel)
            {
                mudaMaterial(obj, false);
            }
        }
        else
        {
            if (indice <= botoesPainel.Length && indice > 0)
            {
                mudaMaterial(botoesPainel[indice - 1], true);
                contexto = indice;
                painel.LimpaPainel();

                if (indice == 2)
                {
                    Rocha.SetActive(true);
                    linhas.MudarContexto(2);
                    ativaBloco(true);
                    blocoAtivado = true;
                }
                else
                {
                    Rocha.SetActive(false);
                    linhas.MudarContexto(1);
                    ativaBloco(false);
                    blocoAtivado = false;
                }
            }
        }
    }
    public void DesativaPainel(bool ativado)
    {
        if (PainelMao != null)
        {
            PainelMao.SetActive(ativado);
        }
    }
    public void ativaBloco(bool ativa)
    {
        if (ativa)
        {
            botoesMarcador[0].SetActive(false);
        }
        else
        {
            botoesMarcador[0].SetActive(true);
        }
        foreach (GameObject texto in textos)
        {
            texto.SetActive(ativa);
        }
    }
    public void alteraInteracao(bool permitir)
    {
        permitirInteracao = permitir;
    }
}
