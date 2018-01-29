using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSpeed = 8;
    [SerializeField] private Bullet bullet;

    private Rigidbody2D rig;

    private Vector2 velocity = Vector2.zero;

    private float angle = 0;

    private float shootTimer = 0.2f;
    private float currentShootTimer = 0;

	// Use this for initialization
	void Start () {
        rig = this.GetComponent<Rigidbody2D>();

        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {
        OnRotation();
        LimiteMovement();

        currentShootTimer -= Time.deltaTime;

        if ((Input.GetButton("Fire") || Input.GetAxis("GamePad_Fire") > 0.5f) && currentShootTimer <= 0) {
            currentShootTimer = shootTimer;
            if(bullet != null) {
                Bullet _bullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
            }
        }
    }

    private void FixedUpdate() {
        OnMove();
    }

    private void OnMove() {
        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        velocity.Normalize();
        velocity *= Time.deltaTime;
        rig.MovePosition(new Vector2(this.transform.position.x + velocity.x * speed, this.transform.position.y + velocity.y * speed));
    }

    private void OnRotation() {
        Vector2 _vec2 = new Vector2(Input.GetAxis("Right_X"), Input.GetAxis("Right_Y"));
        _vec2.Normalize();
        if (_vec2.magnitude > 0.85f) {
            angle = Mathf.Atan2(_vec2.x, _vec2.y) * Mathf.Rad2Deg;
            //this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)), rotationSpeed * Time.deltaTime);
        }
    }

    private void LimiteMovement() {
        if (this.transform.position.x > WindowResize.Instance.screenLimitation.x) {
            this.transform.position = new Vector2(WindowResize.Instance.screenLimitation.x, this.transform.position.y);
        }

        if (this.transform.position.x < -WindowResize.Instance.screenLimitation.x) {
            this.transform.position = new Vector2(-WindowResize.Instance.screenLimitation.x, this.transform.position.y);
        }

        if (this.transform.position.y > WindowResize.Instance.screenLimitation.y) {
            this.transform.position = new Vector2(this.transform.position.x, WindowResize.Instance.screenLimitation.y);
        }

        if (this.transform.position.y < -WindowResize.Instance.screenLimitation.y) {
            this.transform.position = new Vector2(this.transform.position.x, -WindowResize.Instance.screenLimitation.y);
        }
    }
}
