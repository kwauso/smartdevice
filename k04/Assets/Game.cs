#nullable enable
using GameCanvas;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Game : GameBase
{
    const int BALL_NUM = 30;
    int[] ball_x = new int [BALL_NUM];
    int[] ball_y = new int [BALL_NUM];
    int[] ball_col = new int [BALL_NUM];
    int[] ball_speed = new int [BALL_NUM];
    GcImage[] ball_img = new GcImage [BALL_NUM];
    int ball_w = 24;
    int ball_h = 24;

    int player_x = 304;
    int player_y = 448;
    int player_speed = 3;
    int player_w = 32;
    int player_h = 32;
    GcImage player_img = GcImage.GR0;

    int score = 0;
    int time = 1800;

    public override void InitGame()
    {
        gc.SetResolution(640, 480);
        for(int i = 0; i < BALL_NUM; i++)
        {
            resetBall(i);
        }
    }

    public override void UpdateGame()
    {
        if (time > 0)
        {
            time--;
        }

        for(int i = 0; i < BALL_NUM; i++)
        {
            ball_y[i] += ball_speed[i];
            if(ball_y[i] > 480)
            {
                resetBall(i);
            }

            if (gc.CheckHitRect(ball_x[i], ball_y[i], ball_w, ball_h, player_x, player_y, player_w, player_h))
            {
                if (time > 0)
                {
                    score += ball_col[i];
                }
                resetBall(i);
            }
        }

        if(gc.GetPointerFrameCount(0) > 0)
        {
            var px = gc.GetPointerX(0);
            if(px > 320)
            {
                player_x += player_speed;
                player_img = GcImage.GR0;
            }
            else
            {
                player_x -= player_speed;
                player_img = GcImage.GL0;
            }
        }
    }

    public override void DrawGame()
    {
        gc.ClearScreen();

        for(int i = 0; i < BALL_NUM; i++)
        {
            gc.DrawImage(ball_img[i], ball_x[i], ball_y[i]);
        }

        gc.DrawImage(player_img, player_x, player_y);

        gc.SetColor(0, 0, 0);
        if (time > 0)
        {
            gc.DrawString("time:" + time, 0, 0);
        }
        else
        {
            gc.DrawString("finished!!", 0, 0);
        }
        gc.DrawString("score:" + score, 0, 24);
    }

    void resetBall(int id)
    {
        ball_x[id] = gc.Random(0, 616);
        ball_y[id] = -gc.Random(24, 480);
        ball_speed[id] = gc.Random(3, 6);
        ball_col[id] = gc.Random(1, 3);

        if(ball_col[id] == 1)
        {
            ball_img[id] = GcImage.BallYellow;
        }
        else if(ball_col[id] == 2)
        {
            ball_img[id] = GcImage.BallRed;
        }
        else
        {
            ball_img[id] = GcImage.BallBlue;
        }
    }
}
