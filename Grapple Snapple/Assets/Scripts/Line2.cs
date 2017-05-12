﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Line2 : MonoBehaviour
{

    private GameObject player;
    private GameObject lineObj;
    private GameObject crossX;
    private GameObject crossY;
    private Vector3 playerPos;
    private Vector3 lineHit;
    public static LineRenderer line;
    public static RaycastHit hit;

    void Start()
    {
        player = GameObject.Find("Player");
        lineObj = GameObject.Find("Line");
        crossX = GameObject.Find("CrosshairX");
        crossY = GameObject.Find("CrosshairY");
        playerPos = player.transform.position;
    }

    void Update()
    {
        playerPos = player.transform.position;

        if (line != null && Grapple2.hook != null)
        {
            line.SetPosition(0, playerPos);
            line.SetPosition(1, Grapple2.hook.transform.position);
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150) && hit.collider.name != "No")
        {
            //Debug.Log("In Range!");
            crossX.GetComponent<Image>().color = new Color(0, 255, 0);
            crossY.GetComponent<Image>().color = new Color(0, 255, 0);
        }

        else
        {
            //Debug.Log("Out of Range!");
            crossX.GetComponent<Image>().color = new Color(255, 0, 0);
            crossY.GetComponent<Image>().color = new Color(255, 0, 0);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150) && hit.collider.name != "No")
            {
                if (!lineObj.GetComponent<LineRenderer>() && Grapple2.hook != null)
                {
                    lineHit = hit.point;
                    lineObj.AddComponent<LineRenderer>();
                    line = lineObj.GetComponent<LineRenderer>();
                    line.SetPosition(0, playerPos);
                    line.SetPosition(1, Grapple2.hook.transform.position);
                    line.useWorldSpace = true;
                    line.receiveShadows = false;
                    line.startWidth = 0.1f;
                    line.endWidth = 0.1f;
                    line.material = new Material(Shader.Find("Unlit/Color"));
                    line.material.color = Color.black;
                }
            }
        }

        if (Grapple2.hookDestroyed)
        {
            Destroy(line);
        }
    }
}