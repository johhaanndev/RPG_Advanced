﻿using RPG.Combat;
using RPG.Movement;
using System;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (InteractWithCombat())
                return;

            if (InteractWithMovement())
                return;

            print("nothing to do.");

        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)
                    continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                    continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                    GetComponent<Mover>().StartMoveAction(hit.point);
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}