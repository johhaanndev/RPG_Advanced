using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2.0f;
        [SerializeField] private float timeBetweenAttacks = 1.0f;
        [SerializeField] private float weaponDamage = 5.0f;

        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null)
                return;
            if (target.IsDead())
                return;

            if (target == null)
                return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform.position);

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                timeSinceLastAttack = 0;
                TriggerAttack();
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        /// <summary>
        /// Animation Even when attacking
        /// </summary>
        private void Hit()
        {
            if (target == null)
                return;

            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
                return false;

            Health targetToCheck = combatTarget.GetComponent<Health>();
            return targetToCheck != null && !targetToCheck.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

    }
}
