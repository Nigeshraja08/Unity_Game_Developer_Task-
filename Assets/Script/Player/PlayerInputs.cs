using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public Vector2 move;
    public Vector2 changeGravity;
    public Vector2 look;
    public bool Isconfirm;
    

    public void OnMove(InputValue value)
    {
        MoveInputs(value.Get<Vector2>());
    }

    public void OnGravity(InputValue value)
    {
        GravityInput(value.Get<Vector2>());
    }

    public void OnConfirmGravity(InputValue value)
    {
        SetConfirm(value.isPressed);
    }

    public void OnLook(InputValue value)
    {
        LookInput(value.Get<Vector2>());
    }
    public void MoveInputs(Vector2 newValue)
    {
        move = newValue;
    }


    public void GravityInput(Vector2 newValue)
    {
        changeGravity = newValue;
        //Debug.Log(changeGravity);
    }

    public void SetConfirm(bool newValue)
    {
        Isconfirm = newValue;
        //Debug.Log(Isconfirm);
    }

    public void LookInput(Vector2 newValue)
    {
        look = newValue;
       // Debug.Log(look);
    }
}
