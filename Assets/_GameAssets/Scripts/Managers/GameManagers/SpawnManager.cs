using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> prefabs;
    public Queue<GameObject> _spawnQueue = new();
    public readonly float Limitobjs=12;
    public static SpawnManager instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (_spawnQueue.Count == 0)
        {
            foreach (GameObject prefab in prefabs)
            {
                var yaxis = transform.position.y;
                var obj = Instantiate(prefab, new(Random.Range(yaxis, -yaxis), Random.Range(yaxis,-yaxis)+0.05f,0f), Quaternion.identity);
                EnqueueToQueue(obj);
            }
        }
        if (_spawnQueue.Count != 0)
        {
            InvokeRepeating(nameof(SpawnOne), 0f, Random.Range(2f, 6f));
        }
    }

    private void SpawnOne()
    {
        if (_spawnQueue.Count != 0)
        {
            float _Rnum = 9.2f;
            GameObject obj = DequeueFromQueue();
            Vector3 pos = new(Random.Range(-_Rnum, _Rnum), Random.Range(-_Rnum, _Rnum), transform.position.z);
            obj.transform.position = pos;
            obj.SetActive(true);

        }
    }
    public void EnqueueToQueue( GameObject obj)
    {
        if (_spawnQueue.Count != Limitobjs)
        {
            obj.SetActive(false);
            _spawnQueue.Enqueue(obj);
        }
    }
    public GameObject DequeueFromQueue()
    {
        return _spawnQueue.Dequeue();
    }

}
