using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Roomgate : MyBehaviour,Iinteractable 
{
    [SerializeField] protected HashSet<Vector2Int> positions;
    [SerializeField] protected Tilemap tilemap;
    [SerializeField] protected List<BoxCollider2D> listbox;
    public bool CanOpen;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.Loadtilemap();
    }
    protected void Loadtilemap() {
        this.tilemap = GetComponentInChildren<Grid>().transform.GetComponentInChildren<Tilemap>();
    }
    public void Initinite(HashSet<Vector2Int> position) {
        this.listbox.Clear();
        this.CanOpen = true;
        this.positions = position;
        TilemapVisualizer.Instance.PaintRoomGateTiles(positions,this.tilemap);
        foreach(var ele in positions) {
            Transform box =  MapSpawner.Instance.Spawn("WallBox",new Vector3(ele.x,ele.y,0),Quaternion.identity);
            box.transform.parent = this.transform;
            this.listbox.Add(box.GetComponent<BoxCollider2D>());
        }  
    }
    public void Open() {
        if(!CanOpen) return;
        this.tilemap.gameObject.SetActive(false);
        foreach(var ele in listbox) {
            ele.isTrigger  = true;
        }        
    }
    public void Close() {
        this.tilemap.gameObject.SetActive(true);
        foreach(var ele in listbox) {
            ele.isTrigger = false;
        }
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
    public void OnInteract(Player player)
    {
        this.Open();
    }
}