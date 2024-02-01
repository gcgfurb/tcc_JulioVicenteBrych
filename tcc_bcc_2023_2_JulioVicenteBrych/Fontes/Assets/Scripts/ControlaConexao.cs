using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlaConexao : MonoBehaviour
{
    private NetworkManager manager;
    public GameObject canvas;
    public TMP_InputField entrada;

    void Start()
    {
        manager = GameObject.FindObjectOfType<NetworkManager>();
    }

    void Update()
    {
        if (!NetworkServer.active && !NetworkClient.isConnected)
        {
            canvas.SetActive(true);
        }
    }
    public void conectar()
    {
        manager.networkAddress = entrada.text;
        manager.StartClient();
        canvas.SetActive(false);
        NetworkClient.Ready();
        if (NetworkClient.localPlayer == null)
        {
            NetworkClient.AddPlayer();
        }

    }

}
