using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class Area : MonoBehaviour
{
    private PolygonCollider2D AreaColl;

    CinemachineConfiner cinemaCon;
    private void Start()
    {
        AreaColl = transform.GetComponent<PolygonCollider2D>();
        cinemaCon = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineConfiner>();


        SetConfiner();
    }

    void SetConfiner()
    {
        cinemaCon.m_BoundingShape2D = AreaColl;
    }
}

