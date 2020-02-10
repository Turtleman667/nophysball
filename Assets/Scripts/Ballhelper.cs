using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballhelper : MonoBehaviour
{
    public float Speed;

    public Vector2 Direction = Vector2.up;

    public int CollidePoints;
    
    private CircleCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void LateUpdate()
    {
        var degreesPerPoint = 6.28f / CollidePoints;

        for (int i = 0; i < CollidePoints; i++)
        {
            Vector3 point = transform.position + new Vector3(Mathf.Sin(degreesPerPoint * i), Mathf.Cos(degreesPerPoint * i), 0) * _collider.radius;
            RaycastHit2D hit = Physics2D.Raycast( point, Direction, _collider.radius / 2f, 1 << 8);

            if (hit.collider != null)
            {
                Direction = Vector2.Reflect(Direction, hit.normal);
                break;
            }
        }

        if (Direction.magnitude > 1) Direction = Direction.normalized;

        transform.position = transform.position + (Vector3) Direction * Speed * Time.deltaTime;
    }
}
