using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveController : MonoBehaviour, IMoveable,IRotateable
{
    public void Move(float _input,Rigidbody2D _rb, float speed)
    {
        Vector2 movedir = transform.up * _input;
        movedir = movedir.normalized * speed;
        _rb.velocity = movedir;
    }

    public void Rotate(float input,float _force,Rigidbody2D _rb)
    {
        var _angle = -input * _force * Time.fixedDeltaTime;
        
        _rb.MoveRotation(_rb.rotation+_angle);
    }
    
}
