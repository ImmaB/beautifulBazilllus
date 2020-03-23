using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Holder leftHolder;
    [SerializeField] private Holder rightHolder;
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
    private int inSaveZones = 0;
    public ParticleSystem saveParticles;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rigBod = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (moveInput.magnitude > Mathf.Epsilon)
        {
            Vector2 force = moveInput * (holding ? moveStrengthWhenHolding : moveStrength);
            leftHolder.AddForce(force);
            rightHolder.AddForce(force);
            rigBod.AddForce(force);
        }

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

    internal void SetSave(bool save = true)
    {
        if (save)
        {
            inSaveZones++;
            if (inSaveZones > 0)
                saveParticles.Play();
        }
        else
        {
            inSaveZones--;
            if (inSaveZones == 0)
                saveParticles.Stop();
        }
    }
}
