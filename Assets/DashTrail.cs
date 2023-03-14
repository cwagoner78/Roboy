using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrail : MonoBehaviour
{
    public float trailTime;
    public bool isTrailing;

    public GameObject fader;
    public SpriteRenderer mySprite;

    public float timeCounter;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrailing)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= trailTime)
            {
                timeCounter = 0;
                GameObject f = Instantiate(fader, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity) as GameObject;
                SpriteRenderer faderSprite = f.GetComponent<SpriteRenderer>();
                faderSprite.sprite = mySprite.sprite;
            }
        }
    }




}
