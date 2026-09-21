using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyShipController : MonoBehaviour,IShootable
{
    private Rigidbody2D _Enemyrb;
    [SerializeField] private float _speed;
    [SerializeField] private int DieScore=350;
    [SerializeField] private float _Shotrate=0.75f;
    [SerializeField] private ParticleSystem _exploding;
    [SerializeField] private AudioSource _Source;
    [SerializeField] private int Shotindex,Shipindex,ExplodeIndex;
    [Header("Target Settings")]
    private  const string  _targetTag = "Player";
    private Transform _target;
    private Vector2 _movedir;
    [SerializeField]private Vector2 _offset;
    [SerializeField] private GameObject BulletP;

    void Start()
    {
        _Enemyrb = GetComponent<Rigidbody2D>();
        GameObject obj = GameObject.FindGameObjectWithTag(_targetTag);
        _target = obj.transform;
        InvokeRepeating(nameof(Shotp), 0f, _Shotrate);
    }
    private void Update()
    {
        _movedir = (_target.position + (Vector3)_offset) - transform.position;
    }
    private void FixedUpdate()
    {
        if (_target.gameObject.activeSelf)
        {
            Invoke(nameof(MoveSound), 2f);
            _Enemyrb.velocity = _speed * Time.fixedDeltaTime * _movedir;
        }
    }

    private void MoveSound()
    {
        SoundManager.instance.PlayFx(Shipindex, _Source,0.25f);
    }

    private void OnCollisionEnter2D(Collision2D cls)
    {
        if (cls.transform.TryGetComponent<AsteroidController>(out var con))
        {
            con.WhenDestroyed();
            Die();
        }else if (cls.transform.TryGetComponent<PlayerController>(out var _))
        {
            Die();
        }
        else if (cls.transform.TryGetComponent<Bullet>(out var _))
        {
            Die();
        }

    }
    public void Die()
    {
        _exploding.Play();
        SoundManager.instance.PlayFx(ExplodeIndex, _Source,1f);
        ScoreManager.instance.IncreaseScore(DieScore);
        UiManager.instance.UpdateScore();
        Destroy(gameObject,0.1f);
    }

    private void Shotp()
    {
        if (_target.gameObject.activeSelf)
        {
            Vector2 pos = (Vector2)transform.position;
            Vector2 dir = _target.position - transform.position;
            var bullet = Instantiate<GameObject>(BulletP, pos, Quaternion.identity);
            SoundManager.instance.PlayFx(Shotindex, _Source,1f);
            bullet.GetComponent<Rigidbody2D>().velocity = (_speed * 3f) * Time.fixedDeltaTime * dir;
        }
    }
}
