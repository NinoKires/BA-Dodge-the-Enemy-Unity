using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;
    public bool timerActive = true;
    private float time;

    // Update is called once per frame
    void Update()
    {
        if (timerActive) {
            time += Time.deltaTime;
            var seconds = time % 60;
            timer.text = string.Format ("{0:000}", seconds);
        }
    }
}
