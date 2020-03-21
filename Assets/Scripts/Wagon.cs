using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public float shakeInterval;
    public float shakeIntervalTolerance;
    public Vector2 minShakeForce;
    public Vector2 maxShakeForce;

    private Rigidbody2D rigBod;

    private void Start()
    {
        rigBod = GetComponent<Rigidbody2D>();
        StartCoroutine("Shake");
    }

    private IEnumerator Shake()
    {
        while (true)
        {
            Vector2 randomShake = minShakeForce + (maxShakeForce - minShakeForce) * new Vector2(Rand.Float(-1, 1), Rand.Float(-1, 1));
            rigBod.AddForce(randomShake);
            SoundManager.PlayTrainShake();
            yield return new WaitForSeconds(shakeInterval + Rand.Float(-1, 1) * shakeIntervalTolerance);
        }
    }
}
