using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour {

    public float timer = 5;
    public GameObject playerBox;

	[SerializeField]
	private Canvas canvas;

    private Image img;
	private Text txt;

	private GameManager gameManager;

    public void Start()
    {
        img = canvas.GetComponentInChildren<Image>();
        txt = canvas.GetComponentInChildren<Text>();
		img.sprite = Resources.Load<Sprite>("UI_image/Please");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == playerBox.name)
        {
            if (gameManager.State == GameManager.GameState.START)
    			gameManager.StartWaves();
            Destroy(gameObject);
        }
    }
}
