using UnityEngine;

public class EnemyCheck : MyBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected bool check;
    [SerializeField] protected LayerMask EnemyLayer;
    protected float minDistance = 10f;
    protected Vector2 minnoticebox;
    public bool Check { get {  return check;} } 
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }
    protected void LoadPlayer()
    {
        this.player = GetComponentInParent<Player>();
    }
    protected void OnEnable() {
        this.minnoticebox = player.NoticeBox;
    }
    protected void FixedUpdate()
    {
        RaycastHit2D boxcheck = Physics2D.BoxCast(this.transform.position,player.NoticeBox,0f,Vector2.up,0,EnemyLayer);
        if(boxcheck) {
            if((boxcheck.transform.position - this.transform.position).x <= minnoticebox.x && (boxcheck.transform.position - this.transform.position).y <= minnoticebox.y ) {
                this.minnoticebox = boxcheck.transform.position - this.transform.position;
                this.player.Target = boxcheck.transform;
            }
        }
        else 
        {
            player.Target = null;
        this.minnoticebox = player.NoticeBox;
        }
        if(player.Target) check = true;
        else check = false; 
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, player.NoticeBox);
    }

}
