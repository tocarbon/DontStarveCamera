using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject image;
    public float x;
    public float up;
    public float xofy;
    Vector3 before;
    bool rota;

    private void Start()
    {
        x = -5;
        up = 120;
        xofy = 0.42f;
    }
    void Update()
    {
        image.transform.position = new Vector3(x, Mathf.Sin(x) * xofy, 0) * up + new Vector3(200,42,0);
        Vector3 dir = image.transform.position - before;
        if (dir != Vector3.zero && rota)
        {
            image.transform.eulerAngles = new Vector3(0, 0, 90 + Mathf.Atan2(dir.x, dir.y) * -180 / Mathf.PI);
        }
        before = image.transform.position;
        rota = true;
    }
}
