using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Consts")]
    private const string InputX = "Horizontal";
    private const string InputY = "Vertical";

    [Header("Variables")]
    private float ThrustInput;
    private float rotateInput;
    [SerializeField]private KeyCode ShotKey;

    public void SetInputs()
    {
        SetThurstInput();
        SetRoateInput();
    }
    private void SetThurstInput() 
    {

        ThrustInput = Input.GetKey(KeyCode.W)?1f:0f;
    }
    private void SetRoateInput(){ rotateInput = Input.GetAxisRaw(InputX); }
    public float GetRotateInput() {  return rotateInput; }
    public float GetThrusterNormalized()
    {
        return Mathf.Abs(ThrustInput);
    }
    public bool Onshoot()
    {
        return Input.GetKeyDown(ShotKey);
    }
}
