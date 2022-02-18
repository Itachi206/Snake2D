using System.Collections;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    {
        StartCoroutine(RandomizeFoodPosition());
    }

    IEnumerator RandomizeFoodPosition()
    {
        //box collider property Bound to get the size of the area
        Bounds boundGridArea = this.gridArea.bounds;

        //random number genrator for food using unity inbuilt function Random()
        float x = Random.Range(boundGridArea.min.x, boundGridArea.max.x);
        float y = Random.Range(boundGridArea.min.y, boundGridArea.max.y);

        //assigm food position to this coordinates and round those values to get whole numbers
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        yield return new WaitForSecondsRealtime(3f);
        // Destroy(gameObject);
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //when snake collide with the food it should again regenerate at new position
        if (other.tag == "Snake1") 
        {
            StartCoroutine(RandomizeFoodPosition());
        }else if(other.tag == "Snake2")
        {
            StartCoroutine(RandomizeFoodPosition());
        }
    }
}
