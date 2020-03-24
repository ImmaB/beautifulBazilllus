using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {   
        if (col.gameObject.tag == Tag.player)
            GameManager.instance.Invoke("OnWin", 2);
    }
}
