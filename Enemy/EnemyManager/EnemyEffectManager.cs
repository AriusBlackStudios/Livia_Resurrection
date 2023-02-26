using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Livia{
public class EnemyEffectManager : CharacterEffectsManager
{
    EnemyManager enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<EnemyManager>();

    }

}
}
