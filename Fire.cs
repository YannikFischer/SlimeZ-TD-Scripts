 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public List<GameObject> slimeysInRange;

    private float lastShotTime;
    private TankData tankData;

    // Start is called before the first frame update
    void Start()
    {
        slimeysInRange = new List<GameObject>();
        lastShotTime = Time.time;
        tankData = gameObject.GetComponentInChildren<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;
        float minimalSlimeyDistance = float.MaxValue;

        foreach (GameObject slimey in slimeysInRange)
        {
            float distanceToGoal = slimey.GetComponent<Slimey>().DistanceToGoal();

            if (distanceToGoal < minimalSlimeyDistance)
            {
                target = slimey;
                minimalSlimeyDistance = distanceToGoal;
            }
        }

        if (target != null)
        {
            if (Time.time - lastShotTime > tankData.currentLevel.fireRate)
            {
                Shoot(target.GetComponent<CircleCollider2D>());
                lastShotTime = Time.time;               //setzen den Letzten Schuss auf die jetzige Zeit sodass er nicht direkt nochmal schießt, sondern die FireRate abwartet
            }

            //Rotiert den Tank richtung Gegner
            Vector3 direction = gameObject.transform.position - target.transform.position;

            gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI, new Vector3(0, 0, 1));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)     //checkt ob ein Slimey in range ist
    {
        if (other.gameObject.tag.Equals("Slimey"))      //falls ein Slimey in range ist soll es was ausführen, falls was anderes in rnage ist wie zb eine Bullet dann solls nicht ausführen
        {
            slimeysInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Slimey"))
        {
            slimeysInRange.Remove(other.gameObject);
        }
    }

    void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = tankData.currentLevel.bullet;     //hier holen wir den Prefab aus dem Tank raus (die Bullet die wir pro Tank Level ausgewählt haben)

        Vector3 startPosition = gameObject.transform.position;      //Start und Zielposition wird ermittelt
        Vector3 targetPosition = target.transform.position;

        startPosition.z = bulletPrefab.transform.position.z;        //Z Werte sollen identisch sein (bug fix)
        targetPosition.z = bulletPrefab.transform.position.z;

        GameObject newBullet = Instantiate(bulletPrefab);           //neue Bullet wird erzeugt
        newBullet.transform.position = startPosition;               //diese Bullet wird auf die Startposition gesetzt

        Bullet bulletComp = newBullet.GetComponent<Bullet>();       //Holen uns das Script "Bullet"

        bulletComp.target = target.gameObject;                      //Setzen das Ziel
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;
    }
}
