using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomAwake : MonoBehaviour
{
    public GameObject Boom1;
    public GameObject Boom2;
    public GameObject Boom3;
    public float BoomTime;
    private void Awake()
    {
        StartCoroutine(SpawnBooms());
    }

    private IEnumerator SpawnBooms()
    {
        yield return new WaitForSeconds(BoomTime);

        Boom1.SetActive(true);

        yield return new WaitForSeconds(BoomTime);

        Boom2.SetActive(true);

        yield return new WaitForSeconds(BoomTime);

        Boom3.SetActive(true);
    }
}
