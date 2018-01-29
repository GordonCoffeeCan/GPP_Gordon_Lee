using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] private float damage = 10;
    [SerializeField] private float speed = 12f;

    [SerializeField] private ParticleSystem spark;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        OutBoundaryDestory();
    }

    private void OutBoundaryDestory() {
        if (this.transform.position.x > WindowResize.Instance.screenLimitation.x || this.transform.position.x < -WindowResize.Instance.screenLimitation.x) {
            Destroy(this.gameObject);
        }

        if (this.transform.position.y > WindowResize.Instance.screenLimitation.y || this.transform.position.y < -WindowResize.Instance.screenLimitation.y) {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy() {
        if(spark != null) {
            Instantiate(spark, this.transform.position, Quaternion.identity);
        }
    }
}
