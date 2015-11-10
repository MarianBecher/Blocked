using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController2D))]
public class SplitController : MonoBehaviour {

    public Transform childPrefab;
    public Vector2 absOffset;
    private bool canSplit;

    private PlayerController2D _controller;
	
    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<PlayerController2D>();
        this.canSplit = !_controller.active && Input.GetAxis("Split") > 0;
    }

	// Update is called once per frame
	void Update () {
	    if(_controller.active && Input.GetAxis("Split") > 0)
        {
            if(canSplit)
            {
                Transform child2 = ((Transform)Instantiate(childPrefab));
                Transform child1 = ((Transform)Instantiate(childPrefab));
                child1.position = this.transform.position + new Vector3(absOffset.x, absOffset.y + 0.01f, 0);
                child2.position = this.transform.position - new Vector3(absOffset.x, absOffset.y, 0);
                _controller.getContainer().handleSplitCharacter(GetComponent<PlayerController2D>(), child1.GetComponent<PlayerController2D>(), child2.GetComponent<PlayerController2D>());
            }
        }
        else
        {
            this.canSplit = true;
        }
	}
}
