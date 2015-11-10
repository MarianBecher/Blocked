using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;

    public Vector2 margin;
    public Vector2 smoothing;

    public BoxCollider2D Bounds;
    private Vector3 _min;
    private Vector3 _max;
    
    public bool IsFollowing { get; set; }

	void Start () {
        _min = Bounds.bounds.min;
        _max = Bounds.bounds.max;
        IsFollowing = true;
	}
	
	void Update () {
        var x = transform.position.x;
        var y = transform.position.y;

        if (IsFollowing)
        {
            if (Mathf.Abs(x - target.position.x) > margin.x)
                x = Mathf.Lerp(x, target.position.x, smoothing.x * Time.deltaTime);

            if (Mathf.Abs(y - target.position.y) > margin.y)
                y = Mathf.Lerp(y, target.position.y, smoothing.y * Time.deltaTime);
        }

        var cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, _min.y + GetComponent<Camera>().orthographicSize, _max.y - GetComponent<Camera>().orthographicSize);
        transform.position = new Vector3(x, y, transform.position.z);
	}

}
