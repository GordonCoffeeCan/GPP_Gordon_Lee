using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowResize : MonoBehaviour {

    public static WindowResize Instance;

    [HideInInspector] public Vector2 screenLimitation;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        screenLimitation = ScreenBoundary();
    }
	
	// Update is called once per frame
	void Update () {
        screenLimitation = ScreenBoundary();
    }

    private Vector2 ScreenBoundary() {
        Vector2 _limitedPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        return _limitedPos;
    }
}
