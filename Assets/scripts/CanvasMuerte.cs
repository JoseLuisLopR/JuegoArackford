using Assets.scripts.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMuerte : MonoBehaviour
{
    public Canvas cvCarga;
    void Start()
    {
        //Time.timeScale = 0;
        StartCoroutine(Utilities.Wait(3, cvCarga));
        StartCoroutine(Utilities.Wait(6, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
