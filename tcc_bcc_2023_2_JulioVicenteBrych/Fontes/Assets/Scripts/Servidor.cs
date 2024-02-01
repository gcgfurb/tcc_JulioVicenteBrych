using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Servidor : NetworkBehaviour
{
    // Variaveis para sincronizar a camera
    [SyncVar(hook = nameof(AtualizaCameraPosicao))]
    public Vector3 posicaoCamera;
    [SyncVar(hook = nameof(AtualizaCameraRotacao))]
    public Quaternion rotacaoCamera;

    // Variaveis pra sincronizar o marcador
    [SyncVar(hook = nameof(AtualizaMarcadorPosicao))]
    public Vector3 posicaoMarcador;
    [SyncVar(hook = nameof(AtualizaMarcadorRotacao))]
    public Quaternion rotacaoMarcador;

    // Variaveis para Sincronizar o painel de Botoes na Mão
    [SyncVar(hook = nameof(AtualizaBotaoPosicao))]
    public Vector3 posicaoPainelBotoes;
    [SyncVar(hook = nameof(AtualizaBotaoRotacao))]
    public Quaternion rotacaoPainelBotoes;

    // Variaveis para Sincronizar o Botao1
    [SyncVar(hook = nameof(AtualizaBotao1Posicao))]
    public Vector3 posicaoBotao1;
    [SyncVar(hook = nameof(AtualizaBotao1Rotacao))]
    public Quaternion rotacaoBotao1;

    // Variaveis para Sincronizar o Botao2
    [SyncVar(hook = nameof(AtualizaBotao2Posicao))]
    public Vector3 posicaoBotao2;
    [SyncVar(hook = nameof(AtualizaBotao2Rotacao))]
    public Quaternion rotacaoBotao2;

    // Variaveis para Sincronizar o precionar dos botoes
    [SyncVar(hook = nameof(AcionaBotoesPainel))]
    public int IndiceBotaoPainel;
    [SyncVar(hook = nameof(AcionaBotoesMarcador))]
    public int IndiceBotaoMarcador;
    [SyncVar(hook = nameof(MostraBotoesMao))]
    public bool BotoesMaoVisivel;

    public bool Windows = false;
    private GameObject cameraPrin;
    private GameObject marcador;
    public Player jogador;
    private GameObject painelBotoes;
    private BotoesControler btControler;
    private GameObject botao1;
    private GameObject botao2;

    private void Awake()
    {
        if (Windows)
            cameraPrin = GameObject.FindWithTag("CameraObjeto");
        marcador = GameObject.FindWithTag("Marcador");
        btControler = GameObject.FindObjectOfType<BotoesControler>();
        painelBotoes = GameObject.FindWithTag("Painel");
        botao1 = GameObject.FindGameObjectWithTag("Botao1");
        botao2 = GameObject.FindGameObjectWithTag("Botao2");
    }

    #region Camera
    void AtualizaCameraPosicao(Vector3 velho, Vector3 novo)
    {
        if (Windows)
        {
            cameraPrin.transform.position = posicaoCamera;
        }
    }
    void AtualizaCameraRotacao(Quaternion velho, Quaternion novo)
    {
        if (Windows)
        {
            cameraPrin.transform.rotation = rotacaoCamera;
        }
    }
    #endregion

    #region Marcador
    void AtualizaMarcadorPosicao(Vector3 velho, Vector3 novo)
    {
        if (Windows)
        {
            marcador.transform.position = posicaoMarcador;
        }
    }
    void AtualizaMarcadorRotacao(Quaternion velho, Quaternion novo)
    {
        if (Windows)
        {
            marcador.transform.rotation = rotacaoMarcador;
        }
    }
    #endregion

    #region Painel Botoes
    void AtualizaBotaoPosicao(Vector3 velho, Vector3 novo)
    {
        if (!Windows)
        {
            painelBotoes.transform.localPosition = posicaoPainelBotoes;
        }
    }
    void AtualizaBotaoRotacao(Quaternion velho, Quaternion novo)
    {
        if (!Windows)
        {
            painelBotoes.transform.localRotation = rotacaoPainelBotoes;
        }
    }
    #endregion

    #region Botoes
    void AcionaBotoesMarcador(int novo, int velho)
    {
        if (!Windows)
        {
            btControler.AcionaBotaoMarcador(IndiceBotaoMarcador);
        }
    }

    void AcionaBotoesPainel(int novo, int velho)
    {
        if (!Windows)
        {
            btControler.AcionaBotaoPainel(IndiceBotaoPainel);
        }
    }
    void MostraBotoesMao(bool novo, bool velho)
    {
        if (!Windows)
        {
            btControler.DesativaPainel(BotoesMaoVisivel);
        }
    }
    void AtualizaBotao1Posicao(Vector3 velho, Vector3 novo)
    {
        if (Windows)
        {
            botao1.transform.position = posicaoBotao1;
        }
    }
    void AtualizaBotao1Rotacao(Quaternion velho, Quaternion novo)
    {
        if (Windows)
        {
            botao1.transform.rotation = rotacaoBotao1;
        }
    }
    void AtualizaBotao2Posicao(Vector3 velho, Vector3 novo)
    {
        if (Windows)
        {
            botao2.transform.position = posicaoBotao2;
        }
    }
    void AtualizaBotao2Rotacao(Quaternion velho, Quaternion novo)
    {
        if (Windows)
        {
            botao2.transform.rotation = rotacaoBotao2;
        }
    }
    #endregion

}
