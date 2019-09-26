using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenarioScript : MonoBehaviour
{
    public int option = 0; // for determining which option of the script is running
    public bool aware; // check for if the conversation has started
    public bool response; // check for if the user is talking
    public float time = 0; // time until black out
    public float talking = 0; // duration of user talking
    public float leeway = 0; // saftey time given so user doesnt run out of time from breaks in continuous talking
    public string words; // what the user said
    public int step = 0; // what part of the script the conversation is up to
    public GameObject Player;
    public GameObject Mia;
    public GameObject Tom;

    public Image black;
    public Animator anim;

    public Collider Conbox;

    // Start is called before the first frame update
    void Start()
    {
        aware = false;
        response = false;
        step = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (leeway < 0)
        {
            time -= Time.deltaTime;
        }

        leeway -= Time.deltaTime;


       /* if (user.talking)
        {
            response = true;
        } */


        if (aware == true)
        {
            RotateTowards(Player.transform, Mia.transform, 1.0f);
            RotateTowards(Player.transform, Tom.transform, 1.0f);


            if (option == 0)
            {

                if (step == 0)
                {
                    //say.Hello
                    time = 15;
                    step++;
                }

                if (response == true)
                {
                    if (step == 1)
                    {
                        if (words == "Hello" || words == "Hi" || words == "G Day" || words == "Hey")
                        {
                            //say.HowAreYou
                            response = false;
                            time = 15;
                            step++;
                        }
                    }

                    if (step == 2)
                    {
                        talking += Time.deltaTime;
                        leeway = 1.5f;
                    }

                }

                if (time <=0 || talking >= 20)
                {
                    Fading();
                }

                response = false;
            }


            if (option == 1)
            {

                if (step == 0)
                {
                    time = 15;
                    step++;
                }

                if (step == 1)
                {
                    if (words == "Hello" || words == "Hi" || words == "G Day" || words == "Hey")
                    {
                        //say.HeyGoodToSeeYoo
                        response = false;
                        time = 15;
                        step++;
                    }
                }

                if (step == 2)
                {
                    if (words == "How are you" || words == "How are you going" || words == "How you going" || words == "how's things")
                    {
                        //say.PrettyGoodHowAreYou
                        time = 3;
                    }
                }

                if (time <= 0)
                {
                    Fading();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider Conbox)
    {
        if (Conbox.tag == "Conbox")
        {
            aware = true;
        }
    }

    public static void RotateTowards(Transform player, Transform npc, float speed = 1.0f)
    {
        Vector3 direction = new Vector3(player.position.x - npc.position.x, 0.0f, player.position.z - npc.position.z).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        npc.rotation = Quaternion.Slerp(npc.rotation, lookRotation, Time.deltaTime * speed);
    }

    IEnumerator Fading()
    {
        anim.SetBool("FadeOut", true);
        return null;
    }
}
