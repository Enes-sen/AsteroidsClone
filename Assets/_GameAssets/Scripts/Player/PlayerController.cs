using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float _Rotspeed,Shotindex,Blowupindex,thurstindex;
    [SerializeField] private AudioSource _Source,_scr2;
    [SerializeField] private ParticleSystem _Deathpar;
    private readonly float _delay = 0.5f;
    private const string _pbullet= "PBullet";
    private bool _canshot = true;
    private bool _isdead = false;
    [Header("Consts")]
    private const string TriggerName = "ThursterActive";
    [Header("Referances")]
    [SerializeField] private Transform shotpoint;
    [SerializeField] private GameObject prefab;
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rb;
    private SwapController _swpcontroller;
    [SerializeField] private Animator _anim;
    private MoveController _moveManager;
    private AnimationController _animator;
    private PlayerInput _playeInput;
    


    private void Awake()
    {
        _canshot = true;
        _scr2.enabled = false;
        SetAwake();
    }

    private void Update()
    {
        
        _playeInput.SetInputs();
        _scr2.enabled = _playeInput.GetThrusterNormalized() != 0;
        _swpcontroller.TestBoundsPassed();
        Invoke(nameof(Canshot),_delay);
        if (_playeInput.Onshoot() && _canshot && !_isdead)
            Shot();
    }

    private void Shot()
    {
        _canshot = false;
        SoundManager.instance.PlayFx((int)Shotindex, _Source);
        Instantiate(prefab,shotpoint.position,shotpoint.rotation);
        
    }
    void Canshot()
    {
        _canshot = true;
    }

    private void FixedUpdate()
    {
        
        if (_playeInput.GetThrusterNormalized() != 0f && !_isdead)
        {
            _moveManager.Move(_playeInput.GetThrusterNormalized(), _rb, Speed);
            _animator.SetBoolAnim(true,_anim, TriggerName);   
        }
        else
        {
            _animator.SetBoolAnim(false, _anim, TriggerName);
        }
        
        _moveManager.Rotate(_playeInput.GetRotateInput(),_Rotspeed,_rb);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.TryGetComponent<AsteroidController>(out var con))
        {
            ScoreManager.instance.IncreaseScore(con.Scoreincreaser);
            UiManager.instance.UpdateScore();
            con.WhenDestroyed();
            _Deathpar.Play();
            SoundManager.instance.PlayFx((int)Blowupindex, _Source);
            Invoke(nameof(Death), 0.02f);
        }
        else if (col.transform.TryGetComponent<EnemyShipController>(out var _))
        {
            _Deathpar.Play();
            SoundManager.instance.PlayFx((int)Blowupindex, _Source);
            Invoke(nameof(Death), 0.02f);
        }
        else if (col.transform.TryGetComponent<Destroyer>(out var dest))
        {
            _Deathpar.Play();
            SoundManager.instance.PlayFx((int)Blowupindex, _Source);
            Invoke(nameof(Death), 0.02f);
        }
    }
    private void Death()
    {
        int left = UiManager.instance.DecreaseShip();
        _isdead = true;
        if (left != 0)
        {
            Invoke(nameof(Revive), 0.01f);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
            gameObject.SetActive(!_isdead);
        }



    }
    private void Revive()
    {
        gameObject.SetActive(!_isdead);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _isdead = false;
        gameObject.SetActive(!_isdead);
        SoundManager.instance.PlayFx(0, _Source, 0.7f);
    }

    private void SetAwake() 
    {
        _rb = GetComponent<Rigidbody2D>();
        _swpcontroller = GetComponent<SwapController>();
        _moveManager = GetComponent<MoveController>();
        _animator = GetComponent<AnimationController>();
        _playeInput = GetComponent<PlayerInput>();
    }
}
