using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour,IDamagable, IShootable
{
    
    [SerializeField] private Spawnedobj SpwTypeR;
    [SerializeField] private int ScoreAdder;
    public int Scoreincreaser { get => ScoreAdder;}
    [SerializeField] private  ParticleSystem DeathPar;
    [SerializeField] private GameObject smallerPrefab;
    [SerializeField] private AudioSource _Source;
    [SerializeField] private int indexDestroy;
    private SwapController _swpcon;
    private Vector2 Randomdir;
    public Spawnedobj SpwType { get; private set; }

    private void Start()
    {
        Randomdir = new(UnityEngine.Random.Range(-10,20), UnityEngine.Random.Range(-10, 20));
        _swpcon = GetComponent<SwapController>();
        SpwType = SpwTypeR;
        GetComponent<Rigidbody2D>().velocity = Time.fixedDeltaTime * UnityEngine.Random.Range(3,6) * Randomdir;
        
    }
    private void Update()
    {
        _swpcon.TestBoundsPassed();
    }
    public Spawnedobj WhenDestroyed()
    {
        DeathPar.Play();
        
        StartCoroutine(nameof(BackTo),0.04f);
        if (smallerPrefab != null)
        {
            Instantiate(smallerPrefab, transform.position, Quaternion.identity);
            Instantiate(smallerPrefab, transform.position, Quaternion.identity);
        }
        
        return SpwType;
    }
    private IEnumerator BackTo(float wait)
    {
        SoundManager.instance.PlayFx(indexDestroy, _Source, 0.7f);
        yield return new WaitForSeconds(wait);
        if (SpwTypeR != Spawnedobj.littleOrShip)
        {
            if (SpawnManager.instance._spawnQueue.Count != SpawnManager.instance.Limitobjs)
                SpawnManager.instance.EnqueueToQueue(this.gameObject);
            else
                Destroy(gameObject);

        }
        else
            Destroy(gameObject);
        

    }
}
public enum Spawnedobj
{
    None,
    AsteroidBig1,
    AsteroidBig2,
    AsteroidBig3,
    AsteroidMid1,
    AsteroidMid2,
    AsteroidMid3,
    littleOrShip
}
