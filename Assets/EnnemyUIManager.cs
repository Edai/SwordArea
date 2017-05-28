using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnnemyUIManager : MonoBehaviour
{
    [SerializeField]
    private Text name;
    
    [SerializeField]
    private Text hp_text;

    [SerializeField]
    private Image hp_bar;

    private Ennemy e;
    private Sprite[] barSprites;

    // Use this for initialization
    void Start()
    {
        e = gameObject.GetComponentInParent<Ennemy>();
        barSprites = Resources.LoadAll<Sprite>("lifebar/");
        if (barSprites.Length != 11)
            Debug.LogError("FAIL ON SPRITES");
    }

    // Update is called once per frame
    void Update()
    {
        name.text = e.name;
        var life = e.getLife();
        if (life < 0)
            life = 0;
        hp_text.text = life + "/" + e.getMaxLife() + "	 LVL:" + 5;
        int a = (int)((life * 100 / e.getMaxLife()));
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
