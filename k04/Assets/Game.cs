#nullable enable
using GameCanvas;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ゲームクラス。
/// 学生が編集すべきソースコードです。
/// </summary>
public sealed class Game : GameBase
{
    // 変数の宣言
    const int BALL_NUM = 30;
    int[] ball_x = new int [BALL_NUM];
    int[] ball_y = new int [BALL_NUM];
    int[] ball_col = new int [BALL_NUM];
    int[] ball_speed = new int [BALL_NUM];
    GcImage[] ball_img = new GcImage [BALL_NUM];
    int ball_w = 24;
    int ball_h = 24;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        // キャンバスの大きさを設定します
        gc.SetResolution(640, 480);
        for(int i =0 ; i < BALL_NUM ; i ++ )
        {
        resetBall(i);
        }
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
        // 起動からの経過時間を取得します
        for(int i =0 ; i < BALL_NUM ; i ++ )
        {
        ball_y[i] = ball_y[i] + ball_speed[i];
        if(ball_y[i]> 480){
            resetBall(i);
        }
        }
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        gc.ClearScreen();

        for(int i =0 ; i < BALL_NUM ; i ++ ){
            gc.DrawImage(ball_img[i],ball_x[i],ball_y[i]);
        }
    }

    void resetBall(int id){
        ball_x[id] = gc.Random(0,616);
        ball_y[id] = -gc.Random(24,480);
        ball_speed[id] = gc.Random(3,6);
        ball_col[id] = gc.Random(1,3);
        if(ball_col[id]==1){
            ball_img[id] = GcImage.BallYellow;
        }
        else if(ball_col[id]==2){
            ball_img[id] = GcImage.BallRed;
        }
        else {
            ball_img[id] = GcImage.BallBlue; 
        }
    }
}
