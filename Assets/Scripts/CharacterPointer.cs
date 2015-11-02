using UnityEngine;
using System.Collections;

public class CharacterPointer : MonoBehaviour {

    public float timeToLive = 2.0f;
    public float hoverDistance = 0;
    public Transform objectToFollow;

    private float createdTime = 0;

	// Use this for initialization
	void Start () {
        createdTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 position = objectToFollow.position;
        position.y += objectToFollow.GetComponent<BoxCollider2D>().size.y / 2 + hoverDistance;
        this.transform.position = position;

        float currentTime = Time.time;
        if (currentTime > createdTime + timeToLive)
        {
            Destroy(this.gameObject);
        }
	}
}
