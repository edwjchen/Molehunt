using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    private bool attackMove = false;
    private bool attacking = false;

    private GameObject nextAttack;
    private Vector3 nextMove;

    private Camera cam;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Awake()
    {
        if (cam == null) cam = this.GetComponentInChildren<Camera>();
        if (agent == null) agent = this.GetComponentInChildren<NavMeshAgent>();
    }

    void Update()
    {
        //All movement scripts
        if (!attacking)
        {
            if (Input.GetMouseButtonDown(1))
            {
                attackMove = false;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                    nextMove = hit.point;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (attackMove)
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform != null
                            && hit.transform.gameObject.name != "Ground")
                        {
                            if (Vector3.Distance(hit.transform.gameObject.transform.position, this.transform.position) < 2)
                            {
                                agent.SetDestination(this.transform.position);
                                attackMove = false;
                                StartCoroutine(Attack(hit.transform.gameObject));
                            }
                            else
                            {
                                agent.SetDestination(hit.point);
                                nextAttack = hit.transform.gameObject;
                                nextMove = hit.point;
                            }
                        }
                    }
                }
            }

            if (attackMove && nextAttack)
            {
                if (Vector3.Distance(nextAttack.transform.position, this.transform.position) < 2)
                {
                    agent.SetDestination(this.transform.position);
                    attackMove = false;
                    StartCoroutine(Attack(nextAttack));
                    nextAttack = null;
                }
            }

            if (this.transform.position == nextMove)
            {

            }
        }

        //key inputs
        if (!attackMove && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A key was pressed.");
            attackMove = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            attackMove = false;
        }
    }

    private IEnumerator Attack(GameObject obj)
    {
        attacking = true;
        // process pre-yield
        yield return new WaitForSeconds(0.5f);
        obj.transform.position += new Vector3(0, -100, 0);
        // process post-yield
        attacking = false;
    }
}
