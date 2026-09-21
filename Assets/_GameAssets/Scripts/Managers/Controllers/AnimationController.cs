using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationController : MonoBehaviour
{
    public void SetBoolAnim(bool state,Animator _animator,string TriggerName)
    {
        _animator.SetBool(TriggerName,state);
    }

    
    
}
