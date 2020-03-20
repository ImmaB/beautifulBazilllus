using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public Vector2 maximumShake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Shake()
    {
        Vector2 randomShake = maximumShake * new Vector2(Rand.Float(-1, 1), Rand.Float(-1, 1));
    }
}
