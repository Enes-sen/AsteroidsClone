using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapController : MonoBehaviour
{
    float _width,_height;
    Camera _cam;
    void Start()
    {
        _cam = Camera.main;
        _height = _cam.orthographicSize * 2;
        _width = _height * _cam.aspect;
    }

    

    public void TestBoundsPassed()
    {
        var pos = transform.position;

        if (pos.x > _width/2)
            pos.x = -_width/2;
        else if(pos.x <-_width/2)
            pos.x = _width/2;
        else if (pos.y > _height / 2)
            pos.y = -_height / 2;
        else if (pos.y < -_height / 2)
            pos.y = _height / 2;
        transform.position = pos;
    }

}
