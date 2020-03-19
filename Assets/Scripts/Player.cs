using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Holder leftHolder;
    [SerializeField] private Holder rightHolder;
    [SerializeField] private HingeJoint2D leftJoint;
    [SerializeField] private HingeJoint2D rightJoint;

    public float moveStrength = 1;
    private PlayerInput input;
    private Vector2 moveInput;
    private Rigidbody2D rigBod;

    // Start is called before the first frame update
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rigBod = GetComponent<Rigidbody2D>();
        leftHolder.SetJoint(leftJoint);
        rightHolder.SetJoint(rightJoint);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (moveInput.magnitude > Mathf.Epsilon)
            rigBod.AddForce(moveInput * moveStrength);   
    }

    internal void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    internal void OnHoldLeft(InputAction.CallbackContext ctx) { OnHold(ctx, leftHolder); }
    internal void OnHoldRight(InputAction.CallbackContext ctx) { OnHold(ctx, rightHolder); }

    private void OnHold(InputAction.CallbackContext ctx, Holder holder)
    {
        if (ctx.IsInputStart())
            holder.Hold();
        else if (ctx.IsInputStop())
            holder.Hold(false);
    }
}
