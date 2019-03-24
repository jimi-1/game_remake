using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float smoothing = 10;
    public float restTime = 0.5f;
    public float restTimer = 0;
    public GameObject panel;
    [HideInInspector]public Vector2 targetPos = new Vector2(1, 1);
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        


    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager._instance.food <= 0||GameManager._instance.isEnd==true)
            return;

        rb.MovePosition(Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime));

        restTimer += Time.deltaTime;

        if (restTimer < restTime)
            return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h > 0)
        {
            v = 0;
        }
        if (h != 0 || v != 0)
        {
            //GameManager._instance.ReduceFood(1);
            collider.enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
            collider.enabled = true;

            if (hit.transform == null)
            {
                targetPos += new Vector2(h, v);
                
               
            }
            else
            {
                switch (hit.collider.tag)
                {
                    // case "OutWall":
                    //     break;
                    // case "Wall":
                    //     animator.SetTrigger("Attack");
                    //     hit.collider.SendMessage("TakeDamage");
                    //     break;
                    case "Food":
                        //GameManager.Instance.AddFood(10);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "Soda":
                        //GameManager.Instance.AddFood(20);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "Exit":
                        panel.SetActive(true);
                        Debug.Log("3d1sa3d3");
                        break;

                }
            }

           // GameManager.Instance.OnplayerMove();

            restTimer = 0;
        }
        
    }
    public void TakeDamage(int lossFood)
    {
        //GameManager.Instance.ReduceFood(lossFood);
        animator.SetTrigger("Damage");

    }
}
