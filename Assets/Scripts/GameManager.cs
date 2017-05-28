using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public enum GameState {
		START,
		PROGRESS,
		STANDBY,
		END
	}

	private GameState state;
	public GameState State {
		get { return state; }
		set { state = value; }
	}

	private int WaveNumber = 0;
	private GameObject ennemies;

    [SerializeField]
    private GameObject start_point;

    private Sprite warning;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Transform[] spawners;

    private Image img;
    private Text txt;

	// Use this for initialization
	void Start () {
		state = GameState.START;
		ennemies = new GameObject ("Ennemies");
        warning = Resources.Load<Sprite>("UI_image/Warning");
        img = canvas.GetComponentInChildren<Image>();
        txt = canvas.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state == GameState.PROGRESS && canvas.enabled == false)
        {
            if (ennemies.transform.childCount == 0)
            {
                State = GameState.STANDBY;
                StartWaves();
            }
        }
	}

    public void StartWaves()
    {
        if (State != GameState.START && State != GameState.STANDBY)
            return;
        State = GameState.PROGRESS;
        canvas.enabled = true;
        img.sprite = warning;
        txt.text = "Look here! Be ready!";
        switch (WaveNumber)
        {
            case (0):
                Invoke("FirstWave", Random.Range(5f, 10f));
                break;
            case (1):
                Invoke("SecondWave", Random.Range(5f, 10f));
                break;
           case (2):
                Invoke("ThirdWave", Random.Range(5f, 10f));
                break;
            default:
                State = GameState.START;
                img.enabled = false;
                txt.text = "Congratulation ! YOU WIN!";
                break;
        }
        WaveNumber++;
    }

    // Spawn one blue golem in the middle
    void FirstWave()
	{
        canvas.enabled = false;
        GameObject g = (GameObject)Instantiate(Resources.Load("BlueGolem"), spawners[1]);
        g.transform.parent = ennemies.transform;
    }

    // Spawn one blue golem on the left and one on the right
	void SecondWave()
	{
        canvas.enabled = false;
        var g = (GameObject)Instantiate(Resources.Load("MiniGolem"), spawners[0]);
        g.transform.parent = ennemies.transform;
        var g1 = (GameObject)Instantiate(Resources.Load("MiniGolem"), spawners[2]);
        g1.transform.parent = ennemies.transform;
    }

    // Spawn one left, one right, after 8s, a big one.
    void ThirdWave()
	{
        canvas.enabled = false;
        var g = (GameObject)Instantiate(Resources.Load("BlueGolem"), spawners[3]);
        g.transform.parent = ennemies.transform;
        var g1 = (GameObject)Instantiate(Resources.Load("MiniGolem"), spawners[5]);
        g1.transform.parent = ennemies.transform;
        Invoke("CreateBigGolem", Random.Range(5f, 8f));
    }

    void CreateBigGolem()
    {
        GameObject go = (GameObject)Instantiate(Resources.Load("Golem"), spawners[1]);
        go.transform.parent = ennemies.transform;
        go.name = "Golem";
    }
    
}
