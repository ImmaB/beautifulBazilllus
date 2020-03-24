using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Holder leftHolder;
    [SerializeField] private Holder rightHolder;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Color saveColor;
    [SerializeField] private Color hurtColor;
    [Space]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private float lostHealthPerSecond;
    [SerializeField] private float hurtMultiplier;
    [Space]
    public float moveStrength = 1;
    public float moveStrengthWhenHolding = 1;

    private PlayerInput input;
    private Vector2 moveInput;
    private Rigidbody2D rigBod;
    private float health = 1;
    public bool holding { get { return leftHolder.joint.enabled || rightHolder.joint.enabled; } }
    private int inSaveZones = 0, inHurtZones = 0;
    public ParticleSystem saveParticles;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        rigBod = GetComponent<Rigidbody2D>();
        input.SwitchCurrentActionMap("InGame");
    }

    private void FixedUpdate()
    {
        if (GameManager.state != GameState.running) return;
        if (moveInput.magnitude > Mathf.Epsilon)
        {
            Vector2 force = moveInput * (holding ? moveStrengthWhenHolding : moveStrength);
            leftHolder.AddForce(force);
            rightHolder.AddForce(force);
            rigBod.AddForce(force);
        }

        if (inHurtZones > 0)
            LooseHealth(lostHealthPerSecond * Time.deltaTime * hurtMultiplier);
        else if (inSaveZones == 0)
            LooseHealth(lostHealthPerSecond * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnHoldLeft(InputAction.CallbackContext ctx) { OnHold(ctx, leftHolder); }
    public void OnHoldRight(InputAction.CallbackContext ctx) { OnHold(ctx, rightHolder); }
    
    public void OnRetry(InputAction.CallbackContext ctx) { if (ctx.IsInputStop()) GameManager.Reload(); }

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
        if (health <= 0) SetDead();
    }

    internal void SetSave(bool save = true)
    {
        OnZoneEnterOrLeave(ref inSaveZones, save, () => saveParticles.Play(), () => saveParticles.Stop());
        UpdateSpriteColor();
    }

    internal void SetHurt(bool hurt = true)
    {
        OnZoneEnterOrLeave(ref inHurtZones, hurt, () => {}, () => {});
        UpdateSpriteColor();
    }

    private void OnZoneEnterOrLeave(ref int zoneCounter, bool entered, Action onEnter, Action onLeave)
    {
        if (entered)
        {
            if (++zoneCounter == 1) onEnter();
        }
        else
        {
            if (--zoneCounter == 0) onLeave();
        }
    }

    private void UpdateSpriteColor()
    {
        sprite.color = inHurtZones > 0 ? hurtColor : (inSaveZones > 0 ? saveColor : Color.white);
    }

    private void SetDead()
    {
        DisableInput();
        GameManager.instance.Invoke("OnGameOver", 3);
    }

    internal void DisableInput()
    {
        input.SwitchCurrentActionMap("GameOver");
    }

}
