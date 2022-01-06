using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbar : MonoBehaviour
{

    public Transform[] waypoints;
    int waypointsIndex;



    private void Start()
    {
        StartCoroutine("geridon");


    }

    private void Update()
    {



        transform.localPosition = Vector3.MoveTowards(transform.position, waypoints[waypointsIndex].position, Time.deltaTime * 3);



    }

    IEnumerator geridon()
    {
        yield return new WaitForSeconds(2.5f);
        Vector3 yon = transform.localScale;
        yon.z *= -1;
        transform.localScale = yon;
        iterateWaypointIndex();
        StartCoroutine("geridon");
    }

    void iterateWaypointIndex()
    {
        waypointsIndex++;
        if (waypointsIndex == waypoints.Length)
        {
            waypointsIndex = 0;
        }
    }
}
