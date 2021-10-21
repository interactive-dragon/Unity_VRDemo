using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour {

    private Vector3 m_Target;
    public GameObject collisionExplosion;
    public float Speed;


    // Update is called once per frame
    void Update()
    {
        float step = Speed * Time.deltaTime;

        if (m_Target != null)
        {
            if (transform.position == m_Target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_Target, step);
        }
    }

    public void setTarget(Vector3 target)
    {
        m_Target = target;
    }

    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }


    }
}
