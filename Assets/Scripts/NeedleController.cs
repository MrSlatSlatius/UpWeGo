﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NeedleController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 endPoint = Vector3.zero;
    [SerializeField] private float speed = 1.4f;
    [SerializeField] private float minPushDistance = 1.2f;
    [SerializeField] private float maxReachDistance = 2.3f;
    private Platform currentPlatform = null;

    public PlayerMovement Player { get; set; }
    public Vector3 Direction { get; set; } = Vector3.zero;
    public Platform IgnorePlatform { get; set; } = null;
    public bool Active { get; set; } = true;

    private Vector3 InitPos { get; set; }

    private void Awake()
    {
        InitPos = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        endPoint = transform.position;
    }

    private void Update()
    {
        if (currentPlatform == null)
        {
            endPoint += Direction.normalized * speed * Time.deltaTime;
            if (Vector3.Distance(InitPos, endPoint) > maxReachDistance)
                Destroy(gameObject);
        }
        else
        {
            if (currentPlatform.Fixed)
            {
                Vector3 dir = (currentPlatform.transform.position + Vector3.up * 1.2f) - Player.transform.position;

                Player.Move(dir.normalized * speed);
                endPoint = currentPlatform.transform.position;

                if (Vector3.Distance(currentPlatform.transform.position + Vector3.up * 1.2f, Player.transform.position) < 0.3f)
                {
                    Player.ApplyGravity = true;
                    Player.Active = true;
                    Destroy(gameObject);
                    return;
                }
            }
            else
            {
                endPoint = Vector3.Lerp(endPoint, transform.position, Time.deltaTime * 10f);// currentPlatform.transform.position;
                if (Vector3.Distance(transform.position, endPoint) < 0.1f)
                    Destroy(gameObject);
            }
        }

        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(new Vector3[] { transform.position, endPoint });

        if (currentPlatform != null)
            return;

        Collider[] colliders = Physics.OverlapSphere(endPoint, 0.2f, (1 << 8));
        foreach (Collider collider in colliders)
        {
            Platform p = collider.GetComponent<Platform>();
            if (p != null && p != IgnorePlatform)
            {
                currentPlatform = p;

                if (p.Fixed)
                {
                    Player.Active = false;
                    Player.ApplyGravity = false;
                    break;
                }

                Vector3 dir = (transform.position - currentPlatform.transform.position);
                currentPlatform.Move(
                    dir.normalized +
                    dir * 2f);

                break;
            }

            Item i = collider.GetComponent<Item>();
            if (i != null)
            {
                Destroy(i.gameObject);
                break;
            }
        }
    }
}
