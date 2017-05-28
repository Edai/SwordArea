using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


    [SerializeField]
    private string name = "Player";

    [SerializeField]
    private long hp = 200;
    private long maxhp;

    [SerializeField]
    private GameObject eyesCamera;

    [SerializeField]
    private GameObject leftHand;

    [SerializeField]
    private GameObject rightHand;

	[SerializeField]
	private Canvas canvas;

	public enum PlayerState {
		LIVING,
		DEAD
	}

	private PlayerState state;
	public PlayerState State {
		get { return state; }
		set { state = value; }
	}

    public long getLife()
    {
        return hp;
    }

    public long getMaxLife()
    {
        return maxhp;
    }


    // Use this for initialization
    void Start () {
        maxhp = hp;
	}
	
	// Update is called once per frame
	void Update () {
		if (State == PlayerState.DEAD) {
            canvas.enabled = true;
            canvas.GetComponentInChildren<Image>().enabled = false;
            canvas.GetComponentInChildren<Text> ().text = "You are dead!";
			leftHand.active = false;
			rightHand.active = false;
		}
        else {
			leftHand.active = true;
			rightHand.active = true;
		}
        gameObject.transform.position = eyesCamera.transform.position;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!other)
            return;
        Ennemy e = other.GetComponentInParent<Ennemy>();
        if (!e)
            return;
        hp -= e.getDamage();
		if (hp <= 0) {
			hp = 0;
			State = PlayerState.DEAD;
		}
    }
}
