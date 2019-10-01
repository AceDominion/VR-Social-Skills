using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Linq;

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
    public bool InRange;
    public GameObject Player;
    public GameObject Mia;
    public GameObject Tom;
    public AudioClip hello; // Audii clip that is referenced by the audiosource.
    public AudioSource helloref; // Audio source reference.
    public AudioClip hru; // Audii clip that is referenced by the audiosource.
    public AudioSource hruref; // Audio source reference.
    public AudioClip fine; // "im fine thanks"
    public AudioSource fineref;

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
        step = 0;

        helloref.clip = hello; // Sets the reference to refer to the clip.
        hruref.clip = hru;
        fineref.clip = fine;

        keywords.Add("Hello", () =>
        {
            /*if (InRange == true)
            {
                aware = true;
            }*/
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

        if (leeway < 0) // while leeway is greater then 0, fade to black count down time wont tick
        {
            time -= Time.deltaTime; // fade to black time counting down
        }

        if (leeway > 0) // while leeway is greater then 0, talking time will increase until 20 at which point screen fades to black
        {
            talking += Time.deltaTime; // talking time counting up
        }

        leeway -= Time.deltaTime; // amount of pause between talking counting down

        if (aware == true)
        {
            RotateTowards(Player.transform, Mia.transform, 1.0f); //turns characters to face player
            RotateTowards(Player.transform, Tom.transform, 1.0f); //turns characters to face player


            if (option == 0) // first potential way things play out according to script
            {

                if (step == 0) // first thing that happens in the first script option
                {
                    helloref.Play(); // Makes the audio source play, which refers to the hello clip.
                    time = 15; // 15 seconds until fade to black
                    step++; // move on to the next part of this option
                }

                if (response == true) // if player is speaking
                {
                    if (step == 1) // second thing that happens in first script option
                    {
                        if (words == "Hello" || words == "Hi" || words == "Hey") // player needs to say one of these words progress
                        {
                            hruref.Play(); // actor response: "how are you"
                            response = false; // sets it so player has to speak again to begin next part
                            time = 15; // 15 seconds until fade to black
                            step++; // move on to the next part of this option
                        }
                    }

                    if (step == 2) // third thing that happens in the first script option
                    {
                        leeway = 1.5f; // amount of pause allowed for when the player has to continusly talk for 20 seconds
                        if (words == "Good" || words == "Fine" || words == "Not bad" || words == "You" || words == "Yourself") // player needs to say one of these words progress
                        {
                            fineref.Play(); //actor response: "im fine thanks"
                            response = false;
                            time = 15;
                            step++;
                            // here we could also set a boolean "complete = true" and check for it in update(), then when true just end the scene.

                        }
                    }

                }

                if (time <=0 || talking >= 20) // if count down time reachs 0 or if the player has talked for 20 seconds
                {
                    Fading(); // fade to black
                }

                response = false; // sets it so player has to speak again in order to be considered talking
            }


            if (option == 1) // second potential way things play out according to script
            {

                if (step > 1) // as long as the player has spoken once then the actors will turn towards them
                {
                    RotateTowards(Player.transform, Mia.transform, 1.0f); //turns characters to face player
                    RotateTowards(Player.transform, Tom.transform, 1.0f); //turns characters to face player
                }

                if (step == 0) // first part of the second script option 
                {
                    time = 15; // time given to say hello before fade to black
                    step++; // move on to the next step
                }

                if (step == 1) // second part of the second script option
                {
                    if (words == "Hello" || words == "Hi" || words == "Hey") // player needs to say one of these words to progress
                    {
                        helloref.Play(); 
                        //say.HeyGoodToSeeYoo
                        response = false; // sets it so player has to speak again in order to be considered talking
                        time = 15; // time until fade to black
                        step++; // move on to the next step
                    }
                }

                if (step == 2) // third part of the second script option
                {
                    if (words == "How are you" || words == "How are you going" || words == "How you going" || words == "How's things") // player needs to say one of these to progress
                    {
                        //say.PrettyGoodHowAreYou
                        time = 3; // time until fade to black
                    }
                }

                if (time <= 0) // if time has run out
                {
                    Fading(); // fade to black
                }
            }
        }
    }

    private void OnTriggerEnter(Collider Conbox)
    {
        if (Conbox.tag == "Conbox")
        {
            InRange = true;
            aware = true;
            //moved this to when the player speaks, so that NPCs become "aware" only upon the player speaking.
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
