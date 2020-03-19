using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    private SpriteRenderer sprite;
    private HingeJoint2D joint;

    [SerializeField] private Color holdColor;
    private Color idleColor;
    private bool holding = false;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        idleColor = sprite.color;
    }

    private void FixedUpdate()
    {
        if (holding && !joint.enabled) Hold();
    }

    internal void SetJoint(HingeJoint2D joint)
    {
        this.joint = joint;
        joint.anchor = transform.localPosition;
        joint.enabled = false;
    }

    internal void Hold(bool hold = true)
    {
        holding = hold;
        sprite.color = hold ? holdColor : idleColor;
        if (hold)
        {
            if (joint.enabled) return;
            Collider2D col = Physics2D.OverlapCircle(transform.position.To2D(), Mathf.Epsilon, Layers.foregroundMask);
            if (!col) return;
            joint.enabled = true;
            joint.connectedBody = col.attachedRigidbody;
            joint.connectedAnchor = new Vector2(0, 0);
        }
        else
        {
            joint.enabled = false;
        }
    }
}
