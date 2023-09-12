using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Ball speed setting
    [Header("Ball Settings")]
    [Tooltip("The speed at which the ball moves.")]
    [SerializeField]
    private float _speed = 20;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();     

        if (_rb == null)
        {
            throw new MissingComponentException("Rigidbody2D component not found on the Ball object.");
        }   
    }

    public Rigidbody2D ReturnBallRigidbody()
    {
        return _rb;
    }

    private void Start()
    {
        EventManager.OnBallSpawned.Invoke(this);
    }

    public void AddForce(Vector2 force)
    {
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void CheckVelocity()
    {
        if(_rb.velocity.x == 0)
        {
            _rb.velocity = new Vector2(Random.Range(1, 3), _rb.velocity.y);
        }
        else if(_rb.velocity.y == 0)
        {
           _rb.velocity = new Vector2(_rb.velocity.x, Random.Range(1, 3));
        }
    }

    public void Serve(Vector3 targetPosition)
    {
        transform.SetParent(null);

        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector2 force = new Vector2(direction.x * _speed, direction.y * _speed);

        // Apply the calculated force to the ball
        AddForce(force);
    }
}
