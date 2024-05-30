using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSpreader : MonoBehaviour
{
    [SerializeField] ParticleSystem lightning;
    //randomly shoots a lighting bolt in a square area
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        StartCoroutine(ShootLightning());
    }


    IEnumerator ShootLightning()
    {
        while (true)
        {
            //randomly shoot a lighting bolt
            Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10));
            //instantiate the lighting bolt at the random position, roated downwards
            Instantiate(lightning, randomPosition, Quaternion.Euler(-90, 0, 0));
            yield return new WaitForSeconds(1.0f);
        }
    }

}
