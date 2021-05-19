using System;
using System.Collections.Generic;
using UnityEngine;

//https://202psj.tistory.com/1261

[Serializable]
public class EnemyTest
{
    [SerializeField] public string name;
    [SerializeField] public List<string> skills;

    public EnemyTest(string name, List<string> skills)
    {
        this.name = name;
        this.skills = skills;
    }
}


[Serializable]
public class Enemy : MonoBehaviour
{
    private void Start()
    {
        //Test
        string jsonStr = JsonUtility.ToJson(new EnemyTest("오거", new List<string>() { "물리공격", "마법" }));

        EnemyTest enemy0 = JsonUtility.FromJson<EnemyTest>(jsonStr);

        EnemyTest enemy1 = new EnemyTest("", new List<string>() { });

        JsonUtility.FromJsonOverwrite(jsonStr, enemy1);

        string jsonEnemy1 = JsonUtility.ToJson(enemy1);

        Debug.Log(jsonEnemy1);
    }
    
}
