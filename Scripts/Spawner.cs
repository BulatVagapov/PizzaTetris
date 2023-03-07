using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;//массив для префабов групп

    [SerializeField] Group group;

    public void spawnNext()
    {
        // Random Index
        int i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        group.CurrentGroupAppoint(Instantiate(groups[i], transform.position, Quaternion.identity).transform);
        //проверка есть ли пространство для падения группы
        //group.NoMorePlaysToGroup();
    }
}
