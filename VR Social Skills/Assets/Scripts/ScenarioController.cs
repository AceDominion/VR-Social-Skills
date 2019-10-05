using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Linq;
using Valve.VR;

public class ScenarioController : MonoBehaviour
{
    public bool aware; // check for if the conversation has started
    public bool response; // check for if the user is talking
    public float time = 0; // time until black out
    public string words; // what the user said
    public int step = 0; // what part of the script the conversation is up to
    public bool InRange;
    public int Scenario = MenuButtons.Scenario;
    public GameObject Player;
    public GameObject Mia;
    public GameObject Tom;
    public AudioClip hello; // Audii clip that is referenced by the audiosource.
    public AudioSource helloref; // Audio source reference.
    public AudioClip hru; // Audii clip that is referenced by the audiosource.
    public AudioSource hruref; // Audio source reference.
    public AudioClip fine; // "im fine thanks"
    public AudioSource fineref;
    public bool x; // this is used to control timeMenu's activation


    public Image black;
    public Animator anim;

    public Collider Conbox;


    KeywordRecognizer KeywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Start is called before the first frame update
    void Start()
    {
        aware = false;
        response = false;
        InRange = false;
        x = false;
        step = 0;

        helloref.clip = hello; // Sets the reference to refer to the clip.
        hruref.clip = hru;
        fineref.clip = fine;

        keywords.Add("Hello", () =>
        {
            response = true;
            words = "Hello";
        });

        keywords.Add("Hi", () =>
        {
            response = true;
            words = "Hi";
        });

        keywords.Add("Hey", () =>
        {
            response = true;
            words = "Hey";
        });

        keywords.Add("How are you", () =>
        {
            response = true;
            words = "How are you";
        });

        keywords.Add("How are you going", () =>
        {
            response = true;
            words = "How are you going";
        });

        keywords.Add("How you going", () =>
        {
            response = true;
            words = "How you going";
        });

        keywords.Add("How's things", () =>
        {
            response = true;
            words = "How's things";
        });

        keywords.Add("Good", () =>
        {
            response = true;
            words = "Good";
        });

        keywords.Add("Fine", () =>
        {
            response = true;
            words = "Fine";
        });

        keywords.Add("Not bad", () =>
        {
            response = true;
            words = "Not bad";
        });

        keywords.Add("You", () =>
        {
            response = true;
            words = "You";
        });

        keywords.Add("Yourself", () =>
        {
            response = true;
            words = "Yourself";
        });

        KeywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        KeywordRecognizer.OnPhraseRecognized += KeywordRecongnizerOnPhraseRecongnized;
        KeywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime; // fade to black time counting down

        if (aware == true)
        {
            RotateTowards(Player.transform, Mia.transform, 1.0f); //turns characters to face player
            RotateTowards(Player.transform, Tom.transform, 1.0f); //turns characters to face player
        }

        if (Scenario == 1)
        {

            if(step == 0)
            {
                Player.transform.position = new Vector3(0, 6, -50); // first scenario
                time = 35;
                step++;
            }

            if(step == 1)
            {
                if ((words == "Hello" || words == "Hi" || words == "Hey") && InRange == true)
                {
                    aware = true;
                    helloref.Play();
                    time = 3;
                    response = false;
                    step++;
                }
            }
        }

        if (Scenario == 2)
        {
            if (step == 0)
            {
                Player.transform.position = new Vector3(-9, 6, -9); //second scenario
                time = 20;
                step++;
            }

            if (step == 1)
            {
                if(InRange == true)
                {
                    aware = true;
                    helloref.Play();
                    time = 15;
                    step++;
                }
            }

            if (step == 2)
            {
                if (words == "Hello" || words == "Hi" || words == "Hey")
                {
                    hruref.Play();
                    time = 15;
                    step++;
                }
            }

            if (step == 3)
            {
                if (words == "Good" || words == "Fine" || words == "Not bad" || words == "You" || words == "Yourself")
                {
                    fineref.Play();
                    time = 3;
                    step++;
                }
            }
        }

        
        if (time <= 0) // if count down time reachs 0
        {
            if (x)
            {
                SceneManager.LoadScene(0);
            }

            SteamVR_Fade.Start(Color.black, 1);
            time = 4;
            x = true;
        }

        response = false; // sets it so player has to speak again in order to be considered talking
    }

    private void OnTriggerEnter(Collider Conbox)
    {
        if (Conbox.tag == "Conbox")
        {
            InRange = true;
        }
    }

    public static void RotateTowards(Transform player, Transform npc, float speed = 0.75f)
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

    void KeywordRecongnizerOnPhraseRecongnized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
