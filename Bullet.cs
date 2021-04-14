using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;            //Geschwindigkeit der Bullet
    public float damage;                //Schaden der Bullet
    public GameObject target;           //Ziel der Bullet
    public Vector3 startPosition;       //Startposition
    public Vector3 targetPosition;      //Zielposition

    private float distance;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        float timeInterval = Time.time - startTime;

        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");  //sucht HealthBar Script
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();  //^
                healthBar.currentHealth -= Mathf.Max(damage, 0);    //zieht entweder Leben ab, oder 0, Bug fix dass Gegner nicht Leben dazu bekommt

                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);

                    GameManager.singleton.SetCoins(GameManager.singleton.GetCoins() + Slimey.slimey.killWorth);
                    AudioManager.instance.slimeyDestroy.Play();     
                }
            }

            Destroy(gameObject);
        }
    }
}
