using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float moveDelay;
    GameObject player;
    Vector3 vel;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 trackPlayerPos = new Vector3(player.GetComponent<Rigidbody2D>().position.x, player.GetComponent<Rigidbody2D>().position.y, -10);

        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, trackPlayerPos, ref vel, moveDelay);
        //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, trackPlayerPos, Time.deltaTime * moveDelay);
    }
}
