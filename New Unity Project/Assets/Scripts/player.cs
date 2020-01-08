using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    #region 欄位區域
    [Header("移動速度")][Range(1, 1000)]
    public int speed = 10;
    [Tooltip("旋轉速度"), Range(1.5f , 200f)]
    public float turn = 20.5f;
    [Header("是否完成任務")]
    public bool mission;
    [Header("玩家名稱")]
    public string name = "redrole";
    #endregion

    [Header("撿東西的地方")]
    public Rigidbody rigCatch;

    public Transform tran;
    public Rigidbody rig;
    public Animator ani;

    private void Update()
    {
        Turn();
        Run();
        Catch();
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.name);

        if (other.name == "UMP-45" && ani.GetCurrentAnimatorStateInfo(0).IsName("撿東西"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;
        }
    }


    #region 方法區域
    /// <summary>
    /// 跑步
    /// </summary>
    private void Run()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("撿東西")) return ;
        float v = Input.GetAxis("Vertical");
        //rig.AddForce(0, 0, speed * v);
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);

        ani.SetBool("開關", v != 0);
    }
    /// <summary>
    /// 旋轉
    /// </summary>
   private void Turn()
    {
        float h = Input.GetAxis("Horizontal");
        tran.Rotate(0, turn * h * Time.deltaTime, 0);
    }

    ///<summary>
    ///撿東西
    ///<summary>

    private void Catch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        ani.SetTrigger("撿東西");
    }

    #endregion
    
}
