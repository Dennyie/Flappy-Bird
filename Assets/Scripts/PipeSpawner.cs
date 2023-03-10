using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject prefab;

    [SerializeField]    private float spawnRate = 1f;
    [SerializeField]    private float minHeight = -1f;
    [SerializeField] private float maxHeight = 1f;
    public bool control;


    private void OnDisable() // Função chamada quando o objeto é desativado
    {
        CancelInvoke(nameof(Spawn));    // Cancela todas as invocações 
    }
    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
    void Update() // Update é chamado 1 vez por frame 
    {

        //  Debug.Log("velocity " + rigidbody2d.velocity.y);    // Debug log para ver a velocidade que o flappy está caindo
        if (IsPressingJump()&& control == false)
        {
            control = true;
            InvokeRepeating(nameof(Spawn), spawnRate, spawnRate); // invoca repetidamente em uma certa quantidade de tempo

        }



    }
    private static bool IsPressingJump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public void stopInvoke()
    {
        CancelInvoke();
    }

}
