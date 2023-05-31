using System.ComponentModel;
using UnityEngine;

public class BlackTarDrooler : IPlantAttack
{
    public Transform droolStart;
    public GameObject blackTarPrefab;
    public Vector3 blackTarSize;
    public float blackTarIncrement;
    public float blackTarDecrement;

    public float blackTarStayTime;
    public float blackTarCooldown;

    public float blackTarDuration;
    public float blackTarTicDamage;
    public float timeBetweenBlackTarTics;
    public float blackTarSlowDebuf;

    bool countdown = false;
    BlackTar blackTar = null;
    float tarCooldown = 0f;
    float tarStayTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!planted)
        {
            return;
        }

        if(tarCooldown <= 0f && blackTar == null)
        {
            blackTar = Instantiate(blackTarPrefab, droolStart).GetComponent<BlackTar>();
            blackTar.blackTarDuration = blackTarDuration;
            blackTar.blackTarTicDamage = blackTarTicDamage;
            blackTar.timeBetweenBlackTarTics = timeBetweenBlackTarTics;
            blackTar.blackTarSlowDebuf = blackTarSlowDebuf;
        }
        else
        {
            tarCooldown -= Time.deltaTime;
        }

        if(blackTar != null && !countdown)
        {
            bool scaling = false;
            if(blackTar.transform.localScale.x < blackTarSize.x)
            {
                blackTar.transform.localScale += new Vector3(blackTarIncrement, 0, 0);
                scaling = true;
            }

            if (blackTar.transform.localScale.z < blackTarSize.z)
            {
                blackTar.transform.localScale += new Vector3(0, 0, blackTarIncrement);
                scaling = true;
            }

            if (!scaling)
            {
                tarStayTime = blackTarStayTime;
                countdown = true;
            }
        }

        if(tarStayTime > 0f)
        {
            tarStayTime -= Time.deltaTime;
        }else if (tarStayTime <= 0f && countdown)
        {
            bool scaling = false;

            if (blackTar.transform.localScale.x > 0)
            {
                blackTar.transform.localScale += new Vector3(-blackTarDecrement, 0, 0);
                scaling = true;
            }

            if (blackTar.transform.localScale.z > 0)
            {
                blackTar.transform.localScale += new Vector3(0, 0, -blackTarDecrement);
                scaling = true;
            }

            if (!scaling)
            {
                countdown = false;
                Destroy(blackTar.gameObject);
                blackTar = null;
                tarCooldown = blackTarCooldown;
                tarStayTime = 0f;
            }
        }
    }
}
