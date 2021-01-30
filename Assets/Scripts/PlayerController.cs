using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D _rigidbody2D;
    private Camera _viewCamera;
    private Vector2 _velocity;

    private void Start()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _viewCamera = Camera.main;
    }

    void Update()
    {

        Vector3 mousePos = _viewCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        transform.up = direction;
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _velocity = new Vector2(horizontal, vertical).normalized * speed;
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = _rigidbody2D.position;
        _rigidbody2D.MovePosition(currentPos + _velocity * Time.deltaTime);
        Vector2 newPos = _rigidbody2D.position;
        _viewCamera.transform.position = new Vector3(newPos.x, newPos.y, -1);
    }
}
