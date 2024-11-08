using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : Room
{
    [SerializeField] protected List<Weapon> ListWeapons;
    public override void CreateRoom(HashSet<Vector2Int> floorpositions)
    {
        base.CreateRoom(floorpositions);
        this.ListWeapons.Clear();
        Transform Staff = ItemSpawner.Instance.Spawn("FireStaff",new Vector3(Center.x + 3, Center.y + 2,0),Quaternion.identity);
        Transform Bow = ItemSpawner.Instance.Spawn("NormalBow",new Vector3(Center.x, Center.y + 2,0),Quaternion.identity);
        Transform Sword = ItemSpawner.Instance.Spawn("NormalSword",new Vector3(Center.x  - 3, Center.y + 2,0),Quaternion.identity);
        ListWeapons.Add(Staff.GetComponent<Weapon>());
        ListWeapons.Add(Sword.GetComponent<Weapon>());
        ListWeapons.Add(Bow.GetComponent<Weapon>());
    }
    protected override void TriggerState()
    {
        foreach(var ele in ListWeapons) {
            if(ele.weaponStateEnum != WeaponStateEnum.OnDrop) {
                this.statemachine.ChangeSate(openState);
                return;
            }
        }
        this.statemachine.ChangeSate(closeState);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.openState.OnEnter += HideItems;
    }
    protected virtual void OnDisable() {
        this.openState.OnEnter -= HideItems;
    }
    protected void HideItems() {
        foreach(var ele in ListWeapons) {
            if(ele.weaponStateEnum == WeaponStateEnum.OnDrop) {
                ItemSpawner.Instance.DeSpawnToPool(ele.transform);
            }
        }
    }
}
