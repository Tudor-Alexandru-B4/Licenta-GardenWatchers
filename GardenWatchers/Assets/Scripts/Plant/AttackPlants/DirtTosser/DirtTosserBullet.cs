using System;
using System.Collections.Generic;
using UnityEngine;

public class DirtTosserBullet : MonoBehaviour
{
    public float damage;
    public float bulletSpeed;
    public float impactRadius;
    public GameObject target;

    float speed;
    Vector3 startPosition;
    ImpactArea impactArea;

    bool landed = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        impactArea = transform.Find("ImpactArea").GetComponent<ImpactArea>();
        impactArea.gameObject.GetComponent<SphereCollider>().radius = impactRadius;
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime;

        transform.position = Parabola(startPosition, target.transform.position, Vector3.Distance(startPosition, target.transform.position) / 3, speed * bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (landed)
        {
            return;
        }

        var tags = new List<string>() { "Enemy", "Ground" };
        if (tags.Contains(collision.gameObject.tag))
        {
            landed = true;
            var enemyList = new List<IEnemyHealth>(impactArea.enemyHealthList);
            foreach (var enemy in enemyList)
            {
                if (enemy)
                {
                    Debug.Log(enemy.gameObject.name);
                    enemy.TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }

    //https://www.youtube.com/watch?v=ddakS7BgHRI
    public Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}