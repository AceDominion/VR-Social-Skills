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
                text.text = "Head on up through the school, entering the first door on your left.";
            }

            if (time < 45)
            {
                text.text = "You're looking for the classroom. It's in through the doors on the left.";
            }

            if (time < 0)
            {
                text.text = "Screen fades to black...";
            }
        }

        if (path == 1 && timeTwo < 0)
        {
            if (time < 90)
            {
                text.text = "Great! Head on down through the hall, you're looking for room 305.";
            }

            if (time < 45)
            {
                text.text = "Room 305 is in the main hall - there are two people standing outside the room.";
            }

            if (time < 0)
            {
                text.text = "Screen fades to black...";
            }
        }

        if (path == 2)
        {
            text.text = "Great! You found the classroom. Go say hi to your classmates Tom and Mia.";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RD")
        {
            path = 1;
            time = 90f;
        }

        if (other.gameObject.tag == "WD")
        {
            if (path == 0)
            {
                text.text = "Wrong door please try again";
                timeTwo = 1.5f;
            }
        }

        if (other.gameObject.tag == "RD2")
        {
            path = 2;
        }
    }
}
