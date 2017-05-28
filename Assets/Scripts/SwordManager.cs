using UnityEngine;

public class SwordManager : MonoBehaviour
{
    private GameObject collidingObject;

    [SerializeField]
    private int damage = 5;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other && (other.name.Contains("Hitbox") || other.name.Contains("hitbox")))
        {
            if ((collidingObject && other.name == collidingObject.name))
                return;
            SetCollidingObject(other);
            Ennemy e = other.gameObject.GetComponentInParent<Ennemy>();
            if (e && e.getLife() > 0)
            {
                e.react("damage");
                e.setLife(e.getLife() - damage);
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
            return;
        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
            return;
        collidingObject = col.gameObject;
    }
}
