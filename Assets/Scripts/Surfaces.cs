using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Surfaces : MonoBehaviour
{
    public bool closedWall = true; // Checking it will bounce back  after collision
    public float bounceStrength = 0.75f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            if (closedWall)
            {
                // To speed Up ball movement after every collision
                /*Vector2 normal = collision.GetContact(0).normal;
                ball.rigidbody.AddForce(-normal * bounceStrength, ForceMode2D.Impulse);*/
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Lose);
            }
            
        }
    }
}
