using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Quaternion angulo;
    public float grado;


    public GameObject target;
    public bool atacando;


    private bool atackEnemy;
    public float atackEnemySpeed;

    [Range(0, 1)]
    public float enemyAtackTime;

    [Range(0, 2)]
    public float enemyAtackCD;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 15)
        {
            cronometro += 1 * Time.deltaTime;

            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 1);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 1:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 2f * Time.deltaTime);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 10 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 5);
                transform.Translate(Vector3.forward * 5f * Time.deltaTime);
            }
            else
            {
                StartCoroutine(EnemyCourtainAtack());
                atacando = true;
            }
            atacando = false;
        }
    }

    private IEnumerator EnemyCourtainAtack()
    {
        float startTime = Time.time; // need to remember this to know how long to dash

        while (Time.time < startTime + enemyAtackTime)
        {
           transform.Translate(Vector3.forward * atackEnemySpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }
        while (Time.time < startTime + enemyAtackCD)
        {
            atackEnemy = false;
            yield return null;
        }
    }
}
