using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    int cur = 0;
    [SerializeField] float speed = 0.3f;

    void FixedUpdate()
    {
        // Waypoint not reached yet? Then move closer
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else cur = (cur + 1) % waypoints.Length;
        // Note: we used the Vector2.MoveTowards function to calculate a point that is a bit closer to the waypoint.
        // Afterwards we set the ghost's position with rigidbody2D.MovePosition. If the waypoint is reached then we
        // increase the cur variable by one. We also want to reset the cur to 0 if it exceeds the list length.
        // We could use something like if (cur == waypoints.Length) cur = 0, but using the modulo (%) operator makes this look a bit more elegant.

        // Animation
        Vector2 dir = waypoints[cur].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "pacman")
            Destroy(collision.gameObject);
    }
}
