using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChanger : MonoBehaviour
{

    public Text text = null;
    public float time = 0f;
    public float timeTwo = 0f;
    public int path;



    // Start is called before the first frame update
    void Start()
    {
        time = 90f;
        timeTwo = -1f;
        path = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeTwo -= Time.deltaTime;

        if (path == 0 && timeTwo < 0)
        {
            if (time < 87)
            {
                text.text = "To Arrive at the classroom please enter the second door on the left";
            }

            if (time < 45)
            {
                text.text = "Time is running out, please enter the second door on the left";
            }

            if (time < 0)
            {
                text.text = "Time's up, game over";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RD")
        {
            path = 1;
        }

        if (other.gameObject.tag != "RD")
        {
            if (path == 0)
            {
                text.text = "Wrong door please try again";
                timeTwo = 1.5f;
            }
        }
    }
}
