using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts
{
    public class SectorAutomatedBasic : StateMachineBehaviour
    {
        BasicSectorScript mySector;
        Dictionary<string,BasicAggressorScript> raiders, defenders;
        System.Random rnd;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            mySector = animator.gameObject.GetComponent<BasicSectorScript>();
            rnd = new System.Random();

            raiders = mySector.GetRaiders();
            defenders = mySector.GetDefenders();
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(raiders.Count > 0 && defenders.Count > 0)
            {
                int result = rnd.Next(raiders.Count + defenders.Count) - defenders.Count;
                if(result <= 0)
                {
                    raiders.ElementAt(rnd.Next(raiders.Count - 1)).Value.TakeDamage(BasicAggressorScript.basicDamage);
                }
                else
                {
                    defenders.ElementAt(rnd.Next(defenders.Count - 1)).Value.TakeDamage(BasicAggressorScript.basicDamage);
                }
            }
            else
            {
                //NIE "activate", bo aktywuje agresorów! musi być trzeci stan
                animator.SetTrigger("Deactivate");
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}