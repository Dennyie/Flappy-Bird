using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject prefab;

    [SerializeField]    private float spawnRate = 1f;
    [SerializeField]    private float minHeight = -1f;
    [SerializeField]    private float maxHeight = 1f;

    void Start() // Função chamada quando objeto é ativado
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate); // invoca repetidamente em uma certa quantidade de tempo
    }
    private void OnDisable() // Função chamada quando o objeto é desativado
    {
        CancelInvoke(nameof(Spawn));    // Cancela todas as invocações 
    }
    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
