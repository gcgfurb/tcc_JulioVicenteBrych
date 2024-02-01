using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOlharCamera : MonoBehaviour
{
    public GameObject[] ListaDeObjetos;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        foreach(GameObject obj in ListaDeObjetos)
        {
            obj.transform.LookAt(cam.transform, Vector3.up);
        }
    }
}
