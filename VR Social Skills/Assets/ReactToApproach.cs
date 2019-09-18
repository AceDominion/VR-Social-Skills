using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactToApproach : MonoBehaviour
{
    
    public GameObject Player;
    public GameObject Mia;
    public GameObject Tom;

    public bool Aware; // Once NPC is aware, they constantly follow the player

    public Collider Conbox;

    // Start is called before the first frame update
    void Start()
    {
        Aware = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Aware)
        {
            //Mia.transform.LookAt(Player.transform);
            RotateTowards(Player.transform, Mia.transform, 2.0f);
            RotateTowards(Player.transform, Tom.transform, 2.0f);
        }
    }


    private void OnTriggerEnter(Collider Conbox)
    {
        if (Conbox.tag == "Conbox")
        {
            Aware = true;
        }
    }

    public static void RotateTowards(Transform player, Transform npc, float speed = 1.0f) {
        Vector3 direction = new Vector3(player.position.x - npc.position.x, 0.0f, player.position.z - npc.position.z).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        npc.rotation = Quaternion.Slerp(npc.rotation, lookRotation, Time.deltaTime * speed);
    }


}
