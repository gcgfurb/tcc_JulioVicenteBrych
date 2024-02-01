using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private Servidor servidor;

    private GameObject cam;
    private Vector3 posCamera;
    private Quaternion rotCamera;

    private GameObject marcador;
    private Vector3 posMarcador;
    private Quaternion rotMarcador;

    private GameObject painel;
    private Vector3 posPainel;
    private Quaternion rotPainel;

    private GameObject botao1;
    private GameObject botao2;

    float sensibilidade = 0.005f;
    void Awake()
    {
        servidor = GameObject.FindObjectOfType<Servidor>();
        BotoesCallBack bts = GameObject.FindObjectOfType<BotoesCallBack>();
        if (bts != null)
            if(bts.jogador == null)
                bts.jogador = this;
        painel = GameObject.FindWithTag("Painel");
        cam = GameObject.FindGameObjectWithTag("CameraObjeto");
        marcador = GameObject.FindWithTag("Marcador");
        botao1 = GameObject.FindGameObjectWithTag("Botao1");
        botao2 = GameObject.FindGameObjectWithTag("Botao2");

        posCamera = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        rotCamera = new Quaternion(cam.transform.rotation.x, cam.transform.rotation.y, cam.transform.rotation.z, cam.transform.rotation.w);
        
        posMarcador = new Vector3(marcador.transform.position.x, marcador.transform.position.y, marcador.transform.position.z);
        rotMarcador = new Quaternion(marcador.transform.rotation.x, marcador.transform.rotation.y, marcador.transform.rotation.z, marcador.transform.rotation.w);

    }
    public override void OnStartLocalPlayer()
    {
        servidor.jogador = this;
    }
    private void Update()
    {
        if(painel == null)
        {
            painel = GameObject.FindWithTag("Painel");
        }
        if(botao1 == null || botao2 == null)
        {
            botao1 = GameObject.FindGameObjectWithTag("Botao1");
            botao2 = GameObject.FindGameObjectWithTag("Botao2");
        }
        if (!servidor.Windows)
        {
            PegaTrasformCamera();
            PegaTrasformMarcador();
            PegaTrasformBotoes();
        }
        if(servidor.Windows)
        {
            PegaTrasformPainel();
        }
        
    }
    #region PegaTransformações
    private void PegaTrasformCamera()
    {
        Vector3 novaPosicao = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        Quaternion novaRotacao = new Quaternion(cam.transform.rotation.x, cam.transform.rotation.y, cam.transform.rotation.z, cam.transform.rotation.w);
        if (verificaAlteracaoCamera(novaPosicao,novaRotacao))
        {
            CmdSincCamera(novaPosicao, novaRotacao);
        }
        posCamera = novaPosicao;
        rotCamera = novaRotacao;
        
    }
    private void PegaTrasformMarcador()
    {
        Vector3 novaPosicao = new Vector3(marcador.transform.position.x, marcador.transform.position.y, marcador.transform.position.z);
        Quaternion novaRotacao = new Quaternion(marcador.transform.rotation.x, marcador.transform.rotation.y, marcador.transform.rotation.z, marcador.transform.rotation.w);
        if (verificaAlteracaoMarcador(novaPosicao, novaRotacao))
        {
            CmdSincMarcador(novaPosicao, novaRotacao);
        }
        posMarcador = novaPosicao;
        rotMarcador = novaRotacao;
        
    }
    public void PegaTrasformPainel()
    {
        posPainel = new Vector3(painel.transform.localPosition.x, painel.transform.localPosition.y, painel.transform.localPosition.z);
        rotPainel = new Quaternion(painel.transform.localRotation.x, painel.transform.localRotation.y, painel.transform.localRotation.z, painel.transform.localRotation.w);
        CmdSincPainel(posPainel, rotPainel);
    }

    public void PegaTrasformBotoes()
    {
        Vector3 posBotoa1 = new Vector3(botao1.transform.localPosition.x, botao1.transform.localPosition.y, botao1.transform.localPosition.z) ;
        Quaternion rotBotoa1 = new Quaternion(botao1.transform.localRotation.x, botao1.transform.localRotation.y, botao1.transform.localRotation.z, botao1.transform.localRotation.w);

        Vector3 posBotoa2 = new Vector3(botao2.transform.localPosition.x, botao2.transform.localPosition.y, botao2.transform.localPosition.z);
        Quaternion rotBotoa2 = new Quaternion(botao2.transform.localRotation.x, botao2.transform.localRotation.y, botao2.transform.localRotation.z, botao2.transform.localRotation.w);

        CmdSincBotoes(posBotoa1, rotBotoa1, posBotoa2, rotBotoa2);
    }
    #endregion
    #region Comandos Sincronização
    [Command]
    public void CmdSincCamera(Vector3 pos, Quaternion rot)
    {
        servidor.posicaoCamera = pos;
        servidor.rotacaoCamera = rot;
    }
    [Command]
    public void CmdSincMarcador(Vector3 pos, Quaternion rot)
    {
        servidor.posicaoMarcador = pos;
        servidor.rotacaoMarcador = rot;
    }
    [Command]
    public void CmdSincPainel(Vector3 pos, Quaternion rot)
    {
        servidor.posicaoPainelBotoes = pos;
        servidor.rotacaoPainelBotoes = rot;
    }
    [Command]
    public void CmdSincBotoes(Vector3 pos1, Quaternion rot1, Vector3 pos2, Quaternion rot2)
    {
        servidor.posicaoBotao1 = pos1;
        servidor.rotacaoBotao1 = rot1;
        servidor.posicaoBotao2 = pos2;
        servidor.rotacaoBotao2 = rot2;
    }
    #endregion
    #region Verifica Alterações
    private bool verificaAlteracaoMarcador(Vector3 pos, Quaternion rot)
    {
        float distancia = calculaDistaciaVector3(posMarcador, pos);
        if (distancia > sensibilidade || distancia < -sensibilidade)
        {
            return true;
        }
        if (mudouRocacao(rotMarcador, rot))
        {
            return true;
        }
        return false;
    }
    private bool verificaAlteracaoCamera(Vector3 pos, Quaternion rot)
    {
        float distancia = calculaDistaciaVector3(posCamera, pos);
        if (distancia > sensibilidade || distancia < -sensibilidade)
        {
            return true;
        }
        if (mudouRocacao(rotCamera, rot))
        {
            return true;
        }
        return false;
    }

    private float calculaDistaciaVector3(Vector3 vec1,Vector3 vec2)
    {
        return Vector3.Distance(vec1, vec2);
    }
    private bool mudouRocacao(Quaternion q1 , Quaternion q2)
    {
        float x = q1.x - q2.x;
        if (x > sensibilidade || x < -sensibilidade)
        {
            return true;
        }
        float y = q1.y - q2.y;
        if (y > sensibilidade || y < -sensibilidade)
        {
            return true;
        }
        float z = q1.z - q2.z;
        if (z > sensibilidade || z < -sensibilidade)
        {
            return true;
        }
        float w = q1.w - q2.w;
        if (w > sensibilidade || w < -sensibilidade)
        {
            return true;
        }
        return false;
    }
    #endregion  
    #region Botoes
    public void acionaBotaoPainel(int indice)
    {
        CmdAcionaBotaoPainel(indice);
    }
    public void acionaBotaoMarcador(int indice)
    {
        CmdAcionaBotaoMarcador(indice);
    }
    [Command]
    public void CmdAcionaBotaoPainel(int indice)
    {
        servidor.IndiceBotaoPainel = indice;
    }
    [Command]
    public void CmdAcionaBotaoMarcador(int indice)
    {
        servidor.IndiceBotaoMarcador = indice;
    }
    public void BotoesMaoVisivel(bool visivel)
    {
        CmdDesativaBotoesMao(visivel);
    }
    [Command]
    public void CmdDesativaBotoesMao(bool visivel)
    {
        servidor.BotoesMaoVisivel = visivel;
    }
    #endregion
}


