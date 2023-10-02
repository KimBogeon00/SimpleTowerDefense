using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float bulletAtk;
    public bool rotate = false;
    public float rotateAmount = 45;
    public bool bounce = false;
    public float bounceForce = 10;
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public List<GameObject> trails;

    private Vector3 startPos;
    private float speedRandomness;
    [SerializeField] Vector3 offset;
    private bool collided;
    private Rigidbody rb;
    public Monster rotateToMouse;
    private GameObject target;
    public GameObject tower;


    public List<int> identity = new List<int>();

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();

        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward + offset;
            var ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(muzzleVFX, ps.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }

        Destroy(this.gameObject, 5.0f);
    }

    void FixedUpdate()
    {
        if (target != null)
            rotateToMouse.RotateToMouse(gameObject, target.transform.position);

        if (rotate)
            transform.Rotate(0, 0, rotateAmount, Space.Self);

        if (speed != 0 && rb != null)
            rb.position += (transform.forward + offset) * (speed * Time.deltaTime);
    }
    void update()
    {
        if (rotateToMouse == null || target == null)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider co)
    {
        if (co.gameObject.CompareTag("Monster"))
        {
            co.GetComponent<Monster>().MonsterHit(bulletAtk, identity, tower);
            if (!bounce)
            {
                if (co.gameObject.tag != "Bullet" && !collided)
                {
                    collided = true;

                    if (trails.Count > 0)
                    {
                        for (int i = 0; i < trails.Count; i++)
                        {
                            trails[i].transform.parent = null;
                            var ps = trails[i].GetComponent<ParticleSystem>();
                            if (ps != null)
                            {
                                ps.Stop();
                                Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                            }
                        }
                    }

                    speed = 0;
                    GetComponent<Rigidbody>().isKinematic = true;

                    // ContactPoint contact = co.contacts[0];
                    // Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                    // Vector3 pos = contact.point;

                    if (hitPrefab != null && target != null)
                    {
                        var hitVFX = Instantiate(hitPrefab, target.transform.position, Quaternion.identity) as GameObject;
                        Destroy(hitVFX, 0.5f);
                        // var ps = hitVFX.GetComponent<ParticleSystem>();
                        // if (ps == null)
                        // {
                        //     var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        //     Destroy(hitVFX, psChild.main.duration);
                        // }
                        // else
                        //     Destroy(hitVFX, ps.main.duration);
                    }

                    Destroy(this.gameObject);

                    //StartCoroutine(DestroyParticle(0f));
                }
            }
            // else
            // {
            //     rb.useGravity = true;
            //     rb.drag = 0.5f;
            //     ContactPoint contact = co.contacts[0];
            //     rb.AddForce(Vector3.Reflect((contact.point - startPos).normalized, contact.normal) * bounceForce, ForceMode.Impulse);
            //     Destroy(this);
            // }
        }

    }

    public IEnumerator DestroyParticle(float waitTime)
    {

        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> tList = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                tList.Add(t);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);
                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < tList.Count; i++)
                {
                    tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    public void SetTargets(GameObject trg, Monster rotateTo)
    {
        target = trg;
        rotateToMouse = rotateTo;
    }
}
