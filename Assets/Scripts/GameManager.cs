using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Layer {
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

public static class Tag
{
    public static string player = "Player";
    public static string saveZone = "Save Zone";
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public static Player player { get; private set; }

    public GameObject GameOverScreen;

    private void Awake()
    {
        if (instance) Debug.LogError("There can only be one GameManager per Scene");
        instance = this;
        player = FindObjectOfType<Player>();
        instance.GameOverScreen.SetActive(false);
    }

    internal void OnGameOver()
    {
        SoundManager.StopAll();
        GameOverScreen.SetActive(true);
    }

    internal static void Reload()
    {
        if (!instance.GameOverScreen.activeSelf) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
