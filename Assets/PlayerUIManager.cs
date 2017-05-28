using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour {
 
    [SerializeField]
    private Text hp_text;

    [SerializeField]
    private Image hp_bar;

    private Player p;
    private Sprite[] barSprites;

    void Start()
    {
        p = gameObject.GetComponentInParent<Player>();
        barSprites = Resources.LoadAll<Sprite>("lifebar/");
        if (barSprites.Length != 11)
            Debug.LogError("FAIL ON SPRITES");
    }

    // Update is called once per frame
    void Update()
    {
        var life = p.getLife();
        if (life < 0)
            life = 0;
        hp_text.text = life + "/" + p.getMaxLife();
        int a = (int)((life * 100 / p.getMaxLife()));
        a = a - (a % 10);
        foreach (Sprite s in barSprites)
        {
            if (s.name == "lifebar" + a)
            {
                hp_bar.sprite = s;
                break;
            }
        }
        transform.LookAt(Camera.main.transform);
    }
}


