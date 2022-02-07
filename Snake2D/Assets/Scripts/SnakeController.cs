using System;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    //to store the direction of snake
    private Vector2 snakeDirection = Vector2.right;

    //to store all the tails of the snake
    private List<Transform> snakeBody = new List<Transform>();

    //to reference the snakebodyprefab 
    public Transform snakeBodyPrefab;

    //to set the initialsize of snake 
    public int snakeInitialSize = 4;

    //snakespeed
    public int snakeSpeed;

    private void Start()
    {
        GameStart();
    }

    //to check the input for direction of snake
    private void Update()          
    {
        if (Input.GetKeyDown(KeyCode.W) && snakeDirection != Vector2.down)
        {
            snakeDirection = Vector2.up;
        }else if (Input.GetKeyDown(KeyCode.S) && snakeDirection != Vector2.up)
        {
            snakeDirection = Vector2.down;
        }else if (Input.GetKeyDown(KeyCode.A) && snakeDirection != Vector2.right)
        {
            snakeDirection = Vector2.left;
        }else if (Input.GetKeyDown(KeyCode.D) && snakeDirection != Vector2.left)
        {
            snakeDirection = Vector2.right;
        }        
    }

    private void FixedUpdate()
    {
        for (int i = snakeBody.Count - 1; i > 0; i--)                   //for loop running backwords to change position from back to front
        {
            snakeBody[i].position = snakeBody[i - 1].position;          // to change the last tail of the change position with 
        }                                                               //the next tails as the snake moves furthur

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x ) + snakeDirection.x,
            Mathf.Round(this.transform.position.y ) + snakeDirection.y,
            0.0f
            ); 
    }

    //to increase the size of snake after eating food 
    private void GrowSnake()
    {
        Transform segment = Instantiate(this.snakeBodyPrefab);
        segment.position = snakeBody[snakeBody.Count - 1].position;           //get the position of the last element    

        snakeBody.Add(segment);                                              //Add the element in the last position of list

    }

    private void GameStart()
    {
        for(int i = 1; i < snakeBody.Count; i++)
        {
            Destroy(snakeBody[i].gameObject);                   //to reload the game as destroying all the snake body
        }
        snakeBody.Clear();                                      // clear the list 
        snakeBody.Add(this.transform);                          // add snakehead to the list 

        for(int i = 1; i < snakeInitialSize; i++)
        {
            snakeBody.Add(Instantiate(this.snakeBodyPrefab));
        }
        this.transform.position = Vector3.zero;                 //setting position of the snakehead to zeros
     }
    
    private void ScreenWrap(Collider2D other)
    {
        if (snakeDirection == Vector2.right)
        {
            this.transform.position = new Vector3((-1 * other.transform.position.x) + 1 , this.transform.position.y, 0.0f);
        }else if (snakeDirection == Vector2.left)
        {
            this.transform.position = new Vector3((-1 * other.transform.position.x) - 1, this.transform.position.y, 0.0f);
        }else if (snakeDirection == Vector2.up)
        {
            this.transform.position = new Vector3( this.transform.position.x, (- 1 * other.transform.position.y) + 1, 0.0f);
        }else if (snakeDirection == Vector2.down)
        {
            this.transform.position = new Vector3(this.transform.position.x, (- 1 * other.transform.position.y) - 1, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //when snake collide with the food Snake should increase
        if (other.tag == "Food")
        {
            GrowSnake();
        } else if(other.tag == "Obstacle")              //when snake collid with itself
        {
            GameStart();
        } else if(other.tag == "Wall")
        {
           ScreenWrap(other);
        }      
    }    
}
