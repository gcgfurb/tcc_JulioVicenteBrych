using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descanso : MonoBehaviour
{
    private GameObject camera;
    private int timer = 0;
    public int tempo = 300;
    public float sensibilidade = 0.5f;
    
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("CameraObjeto");
    }
    void Update()
    {
        Vector3 aceleracao = Input.acceleration;
        if(aceleracao.x > sensibilidade || aceleracao.y > sensibilidade || aceleracao.z > sensibilidade)
        {
            timer = 0;
            camera.SetActive(true);
        }
        else
        {
            if(timer < tempo)
            {
                timer++;
            }
        }
        if(timer == tempo)
        {
            camera.SetActive(false);
        }
    }
}
