using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmsController : MonoBehaviour
{
    public FilmController top;
    public FilmController mid;
    public FilmController bot;
    public float rewindSpeed = -10;

    private int locked = -1;
    private bool pressed = false;
    void Update()
    {
        if (locked == -1)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                locked = 0;
                pressed = true;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                locked = 1;
                pressed = true;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                locked = 2;
                pressed = true;
            }
        }
        else
        {
            bool res = true;
            switch (locked)
            {
                case 0:
                    {
                        if (Input.GetKeyUp(KeyCode.J))
                        {
                            pressed = false;
                        }
                        if (pressed)
                        {
                            res = top.rewind(Time.deltaTime * rewindSpeed);
                            if (!res)
                            {
                                pressed = false;
                            }
                        }
                        if (!pressed && top.getRevertDistance() == 0f)
                        {
                            locked = -1;
                        }
                        break;
                    }
                case 1:
                    {
                        if (Input.GetKeyUp(KeyCode.K))
                        {
                            pressed = false;
                        }
                        if (pressed)
                        {
                            res = mid.rewind(Time.deltaTime * rewindSpeed);
                            if (!res)
                            {
                                pressed = false;
                            }
                        }
                        if (!pressed && mid.getRevertDistance() == 0f)
                        {
                            locked = -1;
                        }
                        break;
                    }
                case 2:
                    {
                        if (Input.GetKeyUp(KeyCode.L))
                        {
                            pressed = false;
                        }
                        if (pressed)
                        {
                            res = bot.rewind(Time.deltaTime * rewindSpeed);
                            if (!res)
                            {
                                pressed = false;
                            }
                        }
                        if (!pressed && bot.getRevertDistance() == 0f)
                        {
                            locked = -1;
                        }
                        break;
                    }

            }
        }

    }
}
