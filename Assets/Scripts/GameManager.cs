using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Layers {
    public static int player = LayerMask.NameToLayer("Player");
    public static int playerMask = LayerMask.GetMask("Player");
    public static int foreground = LayerMask.NameToLayer("Foreground");
    public static int foregroundMask = LayerMask.GetMask("Foreground");

    public static int ToMask(params int[] layers)
    {
        var layerNames = layers.Map(l => LayerMask.LayerToName(l));
        return LayerMask.GetMask(layerNames.ToArray());
    }
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    private void Awake()
    {
        if (instance) Debug.LogError("There can only be one GameManager per Scene");
        instance = this;
    }

}
