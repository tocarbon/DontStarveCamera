using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    GameObject player;
    GameObject[] images = new GameObject[4];//w,s,a,d
    GameObject cameramain;
    List<GameObject> objects = new List<GameObject>();
    float movespeed = 1;

    float setRotateAngle = 45;
    float rotatingAngle;
    bool onRotating;
    float rotateSpeed = 0.4f;

    float cameraToPlayerDistance;

    void Start()
    {
        cameramain = Camera.main.gameObject;
        player = GameObject.Find("Player").gameObject;

        for (int i = 0; i < 4; i++)
        {
            images[i] = player.transform.Find(i.ToString()).gameObject;
        }
        var obj = GameObject.Find("Object").transform;
        foreach (Transform item in obj)
        {
            objects.Add(item.gameObject);
        }
        Vector3 vPlayer = new Vector3(player.transform.position.x,0,player.transform.position.z);
        Vector3 vCamera = new Vector3(cameramain.transform.position.x,0,cameramain.transform.position.z);
        Vector3 dir = vPlayer - vCamera;
        cameraToPlayerDistance = dir.magnitude;
        //Debug.Log(cameraToPlayerDistance);
    }

    void Update()
    {
        float vx = Input.GetAxisRaw("Horizontal");
        float vy = Input.GetAxisRaw("Vertical");

        player.transform.Translate(new Vector3(vx, 0, vy).normalized * Time.deltaTime * movespeed);

        if (Mathf.Abs(vy) > 0)
        {
            foreach (var item in images)
            {
                item.SetActive(false);
            }
            if (vy < 0)
            {
                images[0].SetActive(true);
            }
            else
            {
                images[1].SetActive(true);
            }
        }
        else
        {
            if (Mathf.Abs(vx) > 0)
            {
                foreach (var item in images)
                {
                    item.SetActive(false);
                }
                if (vx < 0)
                {
                    images[2].SetActive(true);
                }
                else
                {
                    images[3].SetActive(true);
                }
            }
        }

        if (!onRotating)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rotatingAngle = setRotateAngle;
                onRotating = true;
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotatingAngle = - setRotateAngle;
                onRotating = true;
            }
        }
        else
        {
            float ang = Mathf.Abs(rotatingAngle);
            if (ang > 0)
            {
                float angle_;
                if (ang <= 0.01f)
                {
                    angle_ = rotatingAngle;
                    rotatingAngle = 0;
                    onRotating = false;
                }
                else
                {
                    angle_ = rotatingAngle * rotateSpeed;
                    rotatingAngle -= rotatingAngle * rotateSpeed;
                }
                player.transform.eulerAngles += new Vector3(0, angle_, 0);
                cameramain.transform.RotateAround(player.transform.position, Vector3.up, angle_);
                foreach (var item in objects)
                {
                    item.transform.eulerAngles += new Vector3(0, angle_, 0);
                }
            }
        }
        
        Vector3 vPlayer = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        Vector3 vCamera = new Vector3(cameramain.transform.position.x, 0, cameramain.transform.position.z);
        Vector3 target = vPlayer + -player.transform.forward * cameraToPlayerDistance;
        float dis = (vCamera - target).magnitude;
        //Debug.Log(dis);

        cameramain.transform.position += (target - vCamera) * 0.05f;
    }
}
