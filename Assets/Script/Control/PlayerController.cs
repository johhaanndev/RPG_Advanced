using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(1))
                MoveToCursor();
        }

        private void MoveToCursor()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }
}