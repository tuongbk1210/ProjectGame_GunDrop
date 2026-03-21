using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnData
    {
        public Vector3 position;
        public WeaponType weaponType;
        public MapLevel mapLevel;
    }

    public GameObject enemyPrefab;

    public List<EnemySpawnData> wave1 = new List<EnemySpawnData>();

    private void Start()
    {
        CreateWave1();
        SpawnWave(wave1);
    }

    void CreateWave1()
    {
        wave1.Clear();

        Vector3[] positions = new Vector3[]
        {
            new Vector3(0,15,0),
            new Vector3(-5,12,0),
            new Vector3(11,5,0),
            new Vector3(5,-8,0),

            new Vector3(2,10,0),
            new Vector3(-7,0.35f,0),
            new Vector3(5,15,0),
            new Vector3(13,5,0),
        };

        AddEnemy(positions[0], WeaponType.AK, MapLevel.Level1);
        AddEnemy(positions[1], WeaponType.AK, MapLevel.Level3);

        AddEnemy(positions[2], WeaponType.Knife, MapLevel.Level2);
        AddEnemy(positions[3], WeaponType.Knife, MapLevel.Level1);

        AddEnemy(positions[4], WeaponType.Handgun, MapLevel.Level2);
        AddEnemy(positions[5], WeaponType.Handgun, MapLevel.Level2);

        AddEnemy(positions[6], WeaponType.Shotgun, MapLevel.Level1);
        AddEnemy(positions[7], WeaponType.Shotgun, MapLevel.Level2);
    }

    void AddEnemy(Vector3 pos, WeaponType weapon, MapLevel level)
    {
        EnemySpawnData data = new EnemySpawnData();
        data.position = pos;
        data.weaponType = weapon;
        data.mapLevel = level;

        wave1.Add(data);
    }

    void SpawnWave(List<EnemySpawnData> wave)
    {
        foreach (var data in wave) {
            GameObject enemy = Instantiate(enemyPrefab, data.position, Quaternion.identity);
            EnemyLevel1 scirpt = enemy.GetComponent<EnemyLevel1>();
            scirpt.weaponType = data.weaponType;
            scirpt.mapLevel = data.mapLevel;
        }
    }
}