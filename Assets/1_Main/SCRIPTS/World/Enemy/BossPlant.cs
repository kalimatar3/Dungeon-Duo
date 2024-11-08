
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BossPlant : Enemy
{
    public override void Attack()
    {
        int rd = Random.Range(0,2);
        switch (rd) {
            case 0 : 
            StartCoroutine(this.ForcusSeeds());
            break; 
            case 1 :
            StartCoroutine(this.SpeadSeeds());
            break;
        }
    }
    protected IEnumerator SpeadSeeds() {
        int seedNumbers = Random.Range(40,51);
        this.canAttack = false;
        for(int i = 1; i < seedNumbers ; i++) {
            float degreeoffset = 360f/(float)seedNumbers;
            float zrotation = Random.Range(0,seedNumbers) * degreeoffset;
            Quaternion newrotaion = Quaternion.Euler(0,0,zrotation);
            Transform Seed = EnemySchemeSpawner.Instance.Spawn("StraightSeed",this.transform.position,newrotaion);
            yield return new WaitForSeconds(0.2f);            
        this.canAttack = true;
        }
    }
    protected IEnumerator ForcusSeeds() {
        int seednumbers = 10;   
        this.canAttack = false;
        for(int i = 0; i < seednumbers;i++) {
            EnemySchemeSpawner.Instance.Spawn("ForcusSeed",this.transform.position,Quaternion.identity);        
        }
        yield return new WaitForSeconds(0.5f);
        this.canAttack = true;
    }
    protected override void UpdateState()
    {
        this.stateMachine.CurState.FrameUpdate();
        if(target == null) this.stateMachine.ChangeSate(wanderingState);
        else this.stateMachine.ChangeSate(attackState);
    }
    protected override void DropItem()
    { 
        int Goldnumber = Random.Range(5,15);
        int EnergyNumber = Random.Range (5,15);
        for(int i = 0 ; i < Goldnumber  ;i++) {
            Transform gold = ItemSpawner.Instance.Spawn("Gold",this.transform.position,Quaternion.identity);
        }
        for (int i =0 ; i < EnergyNumber; i++) {
            Transform energyball = ItemSpawner.Instance.Spawn("EnergyBall",this.transform.position,Quaternion.identity);         
        }
        Transform portal = MapSpawner.Instance.Spawn("Portal",new Vector3(roomholder.Center.x,roomholder.Center.y ),Quaternion.identity);
    }
}
