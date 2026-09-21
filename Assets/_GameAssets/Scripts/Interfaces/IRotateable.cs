using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotateable
{
    void Rotate(float input, float _force, Rigidbody2D _rb);
}
