using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public float speed = 8f;
    public Vector2 direction { get; private set; }
    private Rigidbody2D _rigidbody;
   
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.zero;
        }
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseDragPostion();
    }
    Vector3 GetMouseDragPostion()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        mousePos.x = transform.position.x;
        return mousePos;
    }
    private void FixedUpdate()
    {
        if (direction.sqrMagnitude != 0)
        {
            GetComponent<Rigidbody2D>().AddForce(direction * speed);
        }
    }
    public void ResetPosition()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.position = new Vector2(_rigidbody.position.x, 0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            GameManager.Instance.UpdateScore();
        }
     }
}
