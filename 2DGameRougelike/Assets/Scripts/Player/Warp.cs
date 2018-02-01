using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

    public Transform warpTarget;

    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {

        ScreenFading screenFader = GameObject.FindGameObjectWithTag("ScreenFading").GetComponent<ScreenFading>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        InputScript input = GameObject.FindGameObjectWithTag("Player").GetComponent<InputScript>();

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        input.enabled = false;

        // If i wanna make the player pop
        //input.gameObject.SetActive(false);

        yield return StartCoroutine(screenFader.FadeToBlack());
        Debug.Log("Collio");
        collision.gameObject.transform.position = warpTarget.position;
        //Camera.main.transform.position = warpTarget.position;

        yield return StartCoroutine(screenFader.FadeToClear());
        input.enabled = true;

    }
}
