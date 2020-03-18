using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private HingeJoint2D jointLeft;
    [SerializeField] private HingeJoint2D jointRight;

    public float moveStrength = 1;
    private PlayerInput input;
    private Vector2 moveInput;
    private Rigidbody2D rigBod;

    // Start is called before the first frame update
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        jointLeft.enabled = false;
        jointRight.enabled = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (moveInput.magnitude > Mathf.Epsilon)
            rigBod.AddForce(moveInput * moveStrength);   
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput.x = ctx.ReadValue<Vector2>().x;
    }

    public void OnHoldLeft(InputAction.CallbackContext ctx) { OnHold(ctx, jointLeft); }
    public void OnHoldRight(InputAction.CallbackContext ctx) { OnHold(ctx, jointRight); }

    private void OnHold(InputAction.CallbackContext ctx, HingeJoint2D joint)
    {
        if (ctx.IsInputStart()) // start Holding
        {
            Collider2D col = Physics2D.OverlapCircle(joint.transform.position.To2D(), Mathf.Epsilon, Layers.foreGroundMask);
        }
        else if (ctx.IsInputStop()) // stop holding
        {

        }
    }
}
