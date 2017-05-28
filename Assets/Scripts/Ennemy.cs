using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour {
    
    [SerializeField]
    private GameObject dieEffect;

    public GameObject target;

    [SerializeField]
    private long hp = 100;

    private long maxHp;

    [SerializeField]
    private long damage = 10;

    private AudioSource audio;
    private Animation animation;
    private Reactions reaction;


    [SerializeField]
    private float walkSpeed = 4.0f;

    [SerializeField]
    private float minDistance = 2.3f;

    [SerializeField]
    private float toleranceDist = 0.4f;

    private float oldDistance = 0;

    private void Start ()
    {
        audio = GetComponent<AudioSource>();
        animation = GetComponent<Animation>();
        reaction = GetComponent<Reactions>();
        target = GameObject.Find("Player");
        maxHp = hp;
    }

    private void Update()
    {
        if (audio.isPlaying || hp <= 0)
            return;
        var dis = Vector3.Distance(transform.position, target.transform.position);
        if (dis >= minDistance)
            runInto(dis);
        else
            hitPlayer();
    }

    public void react(string name)
    {
        reaction.PlayReaction(name);
    }

    public void die()
    {
        reaction.PlayAnimation("die");
        dieEffect.SetActive(true);
        Destroy(gameObject, 2);
    }

    public void setLife(long nb)
    {
        hp = nb;
        if (hp <= 0)
            die();
    }

    public long getLife()
    {
        return hp;
    }

    public long getMaxLife()
    {
        return maxHp;
    }


    public long getDamage()
    {
        return damage;
    }
    
    private void runInto(float dis)
    {
        var y = transform.position.y;
        var x = transform.rotation.x;
        var z = transform.rotation.z;
        transform.LookAt(target.transform.position);
        var r = transform.rotation;
        r.x = x;
        r.z = z;
        transform.rotation = r;
        animation.Play("walk");
        var t = transform.position + transform.forward * walkSpeed * Time.deltaTime;
        t.y = y;
        transform.position = t;
        oldDistance = dis;
    }

    private void hitPlayer()
    {
        if (!animation.isPlaying)
        {
            animation.PlayQueued("hit");
            reaction.PlayReaction("rage", true);
        }
    }

}
