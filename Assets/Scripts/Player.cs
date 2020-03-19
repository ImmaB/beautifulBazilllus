using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Holder leftHolder;
    [SerializeField] private Holder rightHolder;
    [SerializeField] private HingeJoint2D leftJoint;
    [SerializeField] private HingeJoint2D rightJoint;
    [Space]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private float lostHealthPerSecond;
    [Space]
    public float moveStrength = 1;
    public float moveStrengthWhenHolding = 1;

    private PlayerInput input;
    private Vector2 moveInput;
    private Rigidbody2D rigBod;
    private float health = 1;
    public bool holding { get { return leftHolder.joint.enabled || rightHolder.joint.enabled; } }
    [HideInInspector] public int inSaveZones = 0;

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
            rigBod.AddForce(moveInput * (holding ? moveStrengthWhenHolding : moveStrength));

        if (inSaveZones == 0)
            LooseHealth(lostHealthPerSecond * Time.deltaTime);
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

    private void LooseHealth(float loss)
    {
        health -= loss;
        healthBarImage.fillAmount = health;
        if (health <= 0)
        {
            // GAME OVER
        }
    }
}
