using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotoesCallBack: MonoBehaviour
{
    public Player jogador;
    public void AcionaBotaoPainel(int indice)
    {
        jogador.acionaBotaoPainel(indice);
    }
    public void AcionaBotaoMarcador(int indice)
    {
        jogador.acionaBotaoMarcador(indice);
    }
    public void BotoesMaoVisivel(bool visivel)
    {
        jogador.BotoesMaoVisivel(visivel);
    }
}
