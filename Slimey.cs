using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimey : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    public float startSpeed = 1.0f;
    [HideInInspector]
    public float speed;
    public float slowAmount = 0.5f;

    public int killWorth = 50;
    public int LifeDamage = 1;

    public static Slimey slimey;

    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        lastWaypointSwitchTime = Time.time;     //Time = Zeit ?XD
        slimey = this;
    }

    // Update is called once per frame
    void Update()
    {
        slimey = this;

        //Macht dass sich der Slimey von Punkt a zu Punkt b und zu Punkt c usw. bewegen kann
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        float pathLength = Vector3.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed;
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        if (gameObject.transform.position.Equals(endPosition))      //kontrolliert ob der Slimey schon am letzten Waypoint ist
        {
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
            }
            else    //zerstört den Slimey und setzt die Leben auf eines weniger
            {
                CameraShake.instance.StartShake();
                Destroy(gameObject);
                GameManager.singleton.SetHealth(GameManager.singleton.GetHealth() - LifeDamage);
            }
        }
    }

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(gameObject.transform.position, waypoints[currentWaypoint + 1].transform.position);

        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance = distance + Vector2.Distance(startPosition, endPosition);
        }

        return distance;
    }

    //fucking hell shit doesnt work pls help
    public void Slow (float amount)
    {
        speed = startSpeed * (1f - amount);
    }
}
