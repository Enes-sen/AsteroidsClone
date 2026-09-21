using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D _rb;

    SwapController _swapcon;
    private void Awake()
    {
        _swapcon = GetComponent<SwapController>();
        _rb.velocity = speed * transform.up;
        Destroy(gameObject, 1.5f);
    }
    private void Update()
    {
        _swapcon.TestBoundsPassed();
    }

    private void OnCollisionEnter2D(Collision2D cls)
    {
        if (cls.transform.TryGetComponent<AsteroidController>(out var con))
        {
            ScoreManager.instance.IncreaseScore(con.Scoreincreaser);
            UiManager.instance.UpdateScore();
            con.WhenDestroyed();
        }
        else if (cls.transform.TryGetComponent<EnemyShipController>(out var coni))
        {
            coni.Die();
        }

        Destroy(gameObject, 0.05f);



    }
}
