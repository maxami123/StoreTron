using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{

    public float playerSpeed = 1f;
    private Rigidbody2D body;
     private float horizontal;
    private float vertical;
    public float storedHorizontal;
    public float storedVertical;
    public Animation walk;
    
    // Start is called before the first frame update
    void Start()
    {   
        body = GetComponent<Rigidbody2D>();
        walk = GetComponent<Animation>();
        StartCoroutine(Actions());
    }

    // Update is called once per frame
    void Update()
    {

       
    }

     void FixedUpdate()
    {

        body.velocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);   
    }
    private int rangeCap = 1;
    private int randomNum = 0;
    IEnumerator Actions()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (randomNum == 0)
            {
                randomNum = Random.Range(0, rangeCap+1);
                if(randomNum == 0)
                {
                    randomNum = -1;
                }
            }
            else if (randomNum == 1)
            {
                randomNum = Random.Range(-rangeCap, rangeCap);
            }  
            else
                randomNum = Random.Range(0, rangeCap+1);
            horizontal = 0;
            vertical = 0;
            

            if (Random.Range(0,2) == 0)
            {
                vertical = randomNum;
                Debug.Log("vertical: " + vertical);
            }
            else
            {
                horizontal = randomNum;
                Debug.Log("horizontal: " + horizontal);
            }
                
                
        }
    }


}
