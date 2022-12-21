
using System;
using UnityEngine;

public class PM : MonoBehaviour
{
    bool wasJustClicked = true;
    bool canMove;
    Vector2 playerSize;

    Rigidbody2D rb;//il rigidbody dell'oggetto


    // Start is called before the first frame update
    void Start()
    {
        playerSize = gameObject.GetComponent<SpriteRenderer>().bounds.extents;
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //bisogna trasformare le coordinate dello schermo(che sono differenti a seconda della risoluzione)
            //nelle coordinate dell'ambiente di unity(trasform)
            //la classe camera possiede un metodo che automaticamente trasforma la mouse position ovvero le coordinate del mouse sullo schermo
            //le trasforma in coordinate del world ovvero l'ambiente di unity
            Vector2 newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (wasJustClicked)
            {
                wasJustClicked = false;
                float x=transform.position.x;   
                float y=transform.position.y;
                 

                if ((newMousePosition.x>=x-playerSize.x && newMousePosition.x<=x+playerSize.x)||(newMousePosition.x<=x+playerSize.x && newMousePosition.x>=x-playerSize.x)&& 
                    (newMousePosition.y>=y-playerSize.y && newMousePosition.y<=y+playerSize.y)|| (newMousePosition.y<=y+playerSize.y && newMousePosition.y>=y-playerSize.y))
                {
                    //il click del mouse si trova sopra la texture del player
                    canMove = true;
                }
                else
                {
                    canMove = false;
                }
                if (canMove)
                {
                    rb.MovePosition(newMousePosition);
                }

            }
            else
            {
                wasJustClicked = true;
            }
        }
    }
}
