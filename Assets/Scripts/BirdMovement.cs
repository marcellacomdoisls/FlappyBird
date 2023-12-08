using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    BirdPhysics birdPhysics;
    private bool physicsEnabled = false;
    private bool isPaused = true;

    private InputCommand commands;

    void Start()
    {
        birdPhysics = new BirdPhysics();

        commands = new InputCommand();
        commands.SetAction(InputCommandAction.JUMP, JumpCommand);
        commands.SetAction(InputCommandAction.PAUSE, PauseCommand);
        commands.SetAction(InputCommandAction.RESUME, ResumeCommand);
        commands.SetAction(InputCommandAction.CLOSE, CloseCommand);
        commands.SetAction(InputCommandAction.KILL, KillCommand);
    }

    void Update()
    {
        commands.ExecuteCommand(InputCommandAction.PAUSE, isPaused);
        commands.ExecuteCommand(InputCommandAction.RESUME, !isPaused);

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }

        float birdY = birdPhysics.yPos;

        if(birdY < -4.6f || birdY > 4.6f)
        {
            birdPhysics.yPos = Mathf.Clamp(birdPhysics.yPos, -4.6f, 4.6f);
            birdPhysics.verticalVelocity = 0f;
        }

        if (physicsEnabled)
        {
            commands.ExecuteCommand(InputCommandAction.JUMP, Input.GetKeyDown(KeyCode.Space));
            UpdatePhysics();
            transform.position = new Vector3(birdPhysics.xPos, birdPhysics.yPos);
            transform.eulerAngles = new Vector3(0f, 0f, Mathf.Max(birdPhysics.angle * 5f, -35f));
        }
    }

    public bool JumpCommand()
    {
        birdPhysics.verticalVelocity = birdPhysics.jumpStrength;
        return true;
    }

    public bool PauseCommand()
    {
        physicsEnabled = false;
        return true;
    }

    public bool ResumeCommand()
    {
        physicsEnabled = true;
        return true;
    }

    public bool CloseCommand()
    {
        return true;
    }

    public bool KillCommand()
    {
        return true;
    }

    public bool InvertGravity()
    {
        birdPhysics.gravity = -birdPhysics.gravity;
        birdPhysics.jumpStrength = -birdPhysics.jumpStrength;

        return true;
    }

    public void UpdatePhysics()
    {
        if (birdPhysics.verticalVelocity > -birdPhysics.maxFallSpeed)
        {
            birdPhysics.verticalVelocity -= birdPhysics.gravity * Time.deltaTime;
        }

        birdPhysics.yPos += birdPhysics.verticalVelocity * Time.deltaTime;
        birdPhysics.angle = birdPhysics.verticalVelocity;
    }

    enum Action
    {
        JUMP, CHANGE_GRAVITY
    }
}
