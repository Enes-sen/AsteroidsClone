using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void Move(float _force, Rigidbody2D _rb,float speed);
}

