using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealerReciver : DameReciver, IDespawnable
{
    public void DeSpawn()
    {
        EnemySchemeSpawner.Instance.DeSpawnToPool(this.transform);
    }
}
