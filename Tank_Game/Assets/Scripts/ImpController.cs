using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpController : MonoBehaviour
{
    public static ImpController controller;
    public Tank_Data pawn;
    public Tank_Move tankMover;
    public Shoot shoot;
    public Enemy_Shoot enemyShoot;
    public new CameraFallow camera;
    public bool canRapidFire;

    public enum MAINBODYCONTROL
    {
        WASD,
        Arrows,
        Other
    };

    public enum TURRETCONTROL
    {
        WASD,
        Arrows,
        Mouse,
        Other
    };

    public MAINBODYCONTROL mainControl;
    public TURRETCONTROL turretControl;

    Transform pawnTf;


    void Start()
    {
        controller = this;
        pawnTf = pawn.GetComponent<Transform>(); //Get the transform component
        tankMover = pawn.GetComponent<Tank_Move>(); //Get the TankMover component
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Start with the assumption that I'm not moving
        Vector3 directionToMove = Vector3.zero;
        Vector3 bulletVelocity = Vector3.zero;


        switch (mainControl)
        {
            #region WASD_CONTROLS
            case MAINBODYCONTROL.WASD:
                // If thw E key is down -- add "forward" to the direction I'm moving
                if (Input.GetKey(KeyCode.W))
                {
                    directionToMove += Vector3.forward * pawn.moveSpeed * Time.deltaTime;
                }

                // If S key is down -- Add "reverse" to the direction I am moving
                if (Input.GetKey(KeyCode.S))
                {
                    directionToMove += -Vector3.forward * pawn.reverseMoveSpeed * Time.deltaTime;
                }

                //If A is down -- Rotate to the left
                if (Input.GetKey(KeyCode.A))
                {
                    pawn.mover.Rotate(-pawn.rotateSpeed * Time.deltaTime);
                }

                //If D is down -- Rotate to the right
                if (Input.GetKey(KeyCode.D))
                {
                    pawn.mover.Rotate(pawn.rotateSpeed * Time.deltaTime);
                }

                //Camera Controls with arrow keys
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    pawn.turretMover.Rotate(pawn.turretRotateSpeed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    pawn.turretMover.Rotate(-pawn.turretRotateSpeed * Time.deltaTime);
                }

                //If space is down -- shoot!
                if (Input.GetKey(KeyCode.Space))
                {
                    shoot.ShootOutObject(shoot.bulletPrefab.gameObject);
                }
                break;
                #endregion
                #region ARROW_CONTROLS
                #endregion
                #region OTHER
                #endregion
        }


        // After I've checked all my imputs, tell my mover to move in the final direction;s
        pawn.mover.Move(directionToMove);
        //gameObject.SendMessage("Move", (Vector3)directionToMove, SendMessageOptions.RequireReceiver); // Go through all the components with this function. If that component has a Move function, tell that component to run it.
        //                                                                    //If all components do not have this function, the second parameter will say that it's okay that you don't have one.
    }
}
