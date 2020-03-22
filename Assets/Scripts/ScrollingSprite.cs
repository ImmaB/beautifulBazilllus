using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingSprite : MonoBehaviour
{
    public Vector2 speed = new Vector2( 1.0f, 0.0f );

    private SpriteRenderer spriteRenderer;
    private Vector2 uvOffset = Vector2.zero;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate() 
    {
        if (spriteRenderer.enabled)
            spriteRenderer.material.SetTextureOffset("_MainTex", uvOffset += speed * Time.deltaTime);
    } 
}
