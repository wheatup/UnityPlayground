using UnityEngine;

public class Main : MonoBehaviour {

	public Camera mainCamera;
	public GameObject cube;
	public GameObject ground;

	private Ray ray;
	private RaycastHit hit;

	void Start () {
		
	}

	void FixedUpdate () {
		ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit)) {
			//Debug.Log(hit);
			//if(hit.collider.gameObject == ground) {
			cube.transform.position = hit.point;
			//}
		}
	}
}
