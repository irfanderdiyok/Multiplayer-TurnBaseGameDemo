using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Animator anim;
   
    public void Askill(){
        anim.Play("Attack1");
    }

     public void Bskill(){
        anim.Play("Attack2");
    }

     public void Cskill(){
        anim.Play("Skill1");
    }

     public void Dskill(){
        anim.Play("Debuff");
    }
}
