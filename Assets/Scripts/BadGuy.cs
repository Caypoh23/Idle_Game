using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Tag myTag;
    [SerializeField] private Tag enemyTag;
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject hitParticles;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxHealth;

    [SerializeField] private float attackDamage;
    [SerializeField] private float knockbackVeclocity;
    [SerializeField] private Vector2 knockbackAngle;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private GoodGuy goodGuy;

    private bool _isKnockedback;
    private float _knockbackStartTime;


    private Vector2 _currentVelocity;
    private Vector2 _workSpace; // whenever we will set new Velocity, this variable will hold its value


    private float _currentHealth;
    public AttackDetails _attackDetails;


    private void Start()
    {
        _currentHealth = maxHealth;

        _attackDetails.DamageAmount = attackDamage;
        _attackDetails.Position = transform.position;
        _attackDetails.KnockbackVeclocity = knockbackVeclocity;
        _attackDetails.KnockbackAngle = knockbackAngle;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.HasTag(enemyTag))
        {
            collision.gameObject.SendMessage("Damage", _attackDetails);
            gameObject.SendMessage("Damage", goodGuy._attackDetails);
        }
    }


    private void Damage(AttackDetails attackDetails)
    {

        int direction;

        DeacreaseHealth(attackDetails.DamageAmount);

        Instantiate(hitParticles, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails.Position.x < transform.position.x)
        {
            direction = 1; //right 
        }
        else
        {
            direction = -1; //left
        }

        _isKnockedback = true;
        _knockbackStartTime = Time.time;
        SetVelocity(attackDetails.KnockbackVeclocity, attackDetails.KnockbackAngle, direction);

    }

    public void DeacreaseHealth(float amount)
    {
        _currentHealth -= amount;
        //_healthBar.SetHealth(_currentHealth);
        if (_currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction) // 1 is right, -1 is left
    {
        angle.Normalize();
        _workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = _workSpace;
        _currentVelocity = _workSpace;
    }
    //set velocity to 0
    public void SetVelocityZero()
    {
        rb.velocity = Vector2.zero;
        _currentVelocity = Vector2.zero;
    }
    private void Die()
    {
  
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    public void CheckKnockback()
    {
        if (Time.time >= _knockbackStartTime + knockbackDuration && _isKnockedback)
        {
            _isKnockedback = false;
            SetVelocityZero();
        }
    }
}
