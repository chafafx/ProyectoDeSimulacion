using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class FPController : MonoBehaviour {
	
	//public float speed = 5f;
	private Transform cam;
	private Rigidbody rb;
	private Vector3 velocity = Vector3.zero;
	private float mouseSensitivity = 250f;
	private float verticalLookRotation;
	private int count;
	private float timer;
	public Text countText;
	public Text winText;
	public Text timeRemaining;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		cam = Camera.main.transform;		
		count = 0;
		countText.text = "Count: " + count.ToString();
		winText.text = "";
		timer = 500.0f;
		timeRemaining.text = "TIME REMAINING: " + timer.ToString("F");
	}
	
	// Update is called once per frame
	void Update () {
		if(timer > 0.0f && count < 5){
			timer -= Time.deltaTime;
			timeRemaining.text = "TIME REMAINING: " + timer.ToString("F");
		} else if (timer <= 0.0f){
			if (count < 5) {
				winText.text = "LOOOOOOSER!!!!";
				SceneManager.LoadScene(2);
			}
			timeRemaining.text = "TIME REMAINING: " + 0.00f;
			timer = 0.0f;
		}
		float xMov = Input.GetAxisRaw("Horizontal");
		float yMov = Input.GetAxisRaw("Vertical");
		float yMov = -10.0f;
		float zMov = Input.GetAxisRaw("Jump");
		float zMov = 0;
		float xMov = 0;
	
		Vector3 movHor = transform.right * xMov;
		Vector3 movVer = transform.forward * yMov;
		Vector3 movUp = transform.up * zMov;	
		velocity = (movHor + movVer + movUp).normalized * speed;
		
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity);
		verticalLookRotation += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation,-60,60);
		cam.localEulerAngles = Vector3.left * verticalLookRotation;
		
	}

	private void FixedUpdate(){
		if(velocity != Vector3.zero){
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive(false);
			count = count + 1;
			countText.text = "Count: " + count.ToString();
			if(count >= 5){
				winText.text = "You have found all the elements!!";
				SceneManager.LoadScene("FinalW");
				
			}
		}
        	Destroy(other.gameObject);
    	}
}
