using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapBody : MonoBehaviour
{

    public GameObject currentCharacter;
    public GameObject wantedCharacter;
    public Camera pcam;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentCharacter.gameObject.SetActive(false);
            wantedCharacter.gameObject.SetActive(true);
            pcam.GetComponent<CameraController>().target = wantedCharacter;
        }
    }
}
