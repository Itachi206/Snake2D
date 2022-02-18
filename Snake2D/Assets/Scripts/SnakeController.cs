using System;
using System.Collections.Generic;
using UnityEngine;

public class Snake2Controller : MonoBehaviour
{
    //to store the direction of snake
    private Vector2 snake2Direction;

    //to store all the tails of the snake
    public List<Transform> snake2Body = new List<Transform>();

    //to reference the snakebodyprefab 
    public Transform snake2BodyPrefab;

    //to set the initialsize of snake 
    public int snake2InitialSize = 4;

    //snakespeed
    public int snake2Speed;

    public ScoreController scoreControllerObject;

    private void Awake()
    {
        snake2Direction = Vector2.left;
    }
    private void Start()
    {
        GameStart();
    }

    //to check the input for direction of snake
    private void Update()
    {
        SnakeMovement();
    }

    private void SnakeMovement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && snake2Direction != Vector2.down)
        {
            snake2Direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && snake2Direction != Vector2.up)
        {
            snake2Direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && snake2Direction != Vector2.right)
        {
            snake2Direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && snake2Direction != Vector2.left)
        {
            snake2Direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        for (int i = snake2Body.Count - 1; i > 0; i--)                   //for loop running backwords to change position from back to front
        {
            snake2Body[i].position = snake2Body[i - 1].position;          // to change the last tail of the change position with 
        }                                                               //the next tails as the snake moves furthur

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x ) + snake2Direction.x,
            Mathf.Round(this.transform.position.y ) + snake2Direction.y,
            0.0f
            ); 
    }

    //to increase the size of snake after eating food 
    private void GrowSnake()
    {
        Transform segment = Instantiate(this.snake2BodyPrefab);
        segment.position = snake2Body[snake2Body.Count - 1].position;           //get the position of the last element    

        snake2Body.Add(segment);                                              //Add the element in the last position of list
    }

    private void GameStart()
    {
        scoreControllerObject.ResetScore();
       for(int i = 1; i < snake2Body.Count; i++)
        {
            Destroy(snake2Body[i].gameObject);                   //to reload the game as destroying all the snake body
        }
        snake2Body.Clear();                                      // clear the list 
        snake2Body.Add(this.transform);                          // add snakehead to the list 

        for(int i = 1; i < snake2InitialSize; i++)
        {
            snake2Body.Add(Instantiate(this.snake2BodyPrefab));
        }
        this.transform.position = Vector3.zero;                 //setting position of the snakehead to zeros
     }
    
    private void ScreenWrap(Collider2D other)
    {
        if (snake2Direction == Vector2.right)
        {
            this.transform.position = new Vector3((-1 * other.transform.position.x) + 1 , this.transform.position.y, 0.0f);
        }else if (snake2Direction == Vector2.left)
        {
            this.transform.position = new Vector3((-1 * other.transform.position.x) - 1, this.transform.position.y, 0.0f);
        }else if (snake2Direction == Vector2.up)
        {
            this.transform.position = new Vector3( this.transform.position.x, (- 1 * other.transform.position.y) + 1, 0.0f);
        }else if (snake2Direction == Vector2.down)
        {
            this.transform.position = new Vector3(this.transform.position.x, (- 1 * other.transform.position.y) - 1, 0.0f);
        }
    }

    private void ReduceSnakeSize()
    {
        if(snake2Body.Count == 1)
        {
            Debug.Log("Game Ended");
            GameStart();            
        }
        else
        {
            Destroy(snake2Body[snake2Body.Count - 1].gameObject);
            snake2Body.RemoveAt(snake2Body.Count - 1);
        }   
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //when snake collide with the food Snake should increase
        if (other.tag == "Food")
        {
            GrowSnake();
            scoreControllerObject.IncreaseScore(10);
        } else if(other.tag == "Obstacle")              //when snake collid with itself
        {
            GameStart();
        } else if(other.tag == "Wall")
        {
           ScreenWrap(other);
        } else if (other.tag == "PoisonFood")
        {
            ReduceSnakeSize();
            scoreControllerObject.IncreaseScore(-10);
        }
    }    
}
