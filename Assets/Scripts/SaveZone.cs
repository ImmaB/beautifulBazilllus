using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == Tag.player)
            col.gameObject.GetComponent<Player>().inSaveZones++;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == Tag.player)
            col.gameObject.GetComponent<Player>().inSaveZones--;
    }
}
