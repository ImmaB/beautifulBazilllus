using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.gameObject.tag == Tag.player)
            GameManager.player.SetSave(true);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == Tag.player)
            GameManager.player.SetSave(false);
    }
}
