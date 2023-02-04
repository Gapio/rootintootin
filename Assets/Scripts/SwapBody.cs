using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBody : MonoBehaviour
{

    public Transform character;
    public Transform wantedCharacter;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint contact in other.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.white);
            }
            character.GetComponent<PlayerController>().enabled = false;
            character.GetComponent<MeshRenderer>().enabled = false;
            wantedCharacter.GetComponent<MeshRenderer>().enabled = true;
            wantedCharacter.GetComponent<SpiderController>().enabled = true;

        }
    }
}
