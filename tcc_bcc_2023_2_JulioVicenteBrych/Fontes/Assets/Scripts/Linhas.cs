using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linhas : MonoBehaviour
{
    public Transform[] inicio;
    public Transform[] fim;
    public LineRenderer[] linhasDesenhadas;
    public Transform troca;
    private int opcao = 1;
    private void Update()
    {
        if (inicio.Length > 0)
        {
            if (inicio.Length == fim.Length)
            {
                for (int i = 0; i < inicio.Length; i++)
                {
                    desenhaLinha(i, inicio[i].position, fim[i].position);
                }
            }
            else
            {
                int diferenca = Mathf.Abs(inicio.Length - fim.Length);
                for (int i = 0; i < inicio.Length - diferenca; i++)
                {
                    desenhaLinha(i, inicio[i].position, fim[i].position);
                }
            }
        }
    }

    private void desenhaLinha(int posicao,Vector3 inicio,Vector3 fim)
    {
        linhasDesenhadas[posicao].SetPosition(0, inicio);
        linhasDesenhadas[posicao].SetPosition(1, fim);
    }
    public void MudarContexto(int valor)
    {
        if(valor != opcao)
        {
            if (valor == 1)
            {
                Transform aux = inicio[1];
                inicio[1] = inicio[2];
                inicio[2] = troca;
                troca = aux;
                opcao = 1;
            }
            else
            {
                Transform aux = troca;
                troca = inicio[2];
                inicio[2] = inicio[1];
                inicio[1] = aux;
                opcao = 2;
            }
        }
    }
}
