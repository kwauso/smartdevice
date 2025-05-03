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
    int ball_x;
    int ball_y;
    int ball_speed_x;
    int ball_speed_y;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        // キャンバスの大きさを設定します
        gc.SetResolution(640, 480);
        ball_x = 0;
        ball_y = 0;
        ball_speed_x = 3;
        ball_speed_y = 3;
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
        // 起動からの経過時間を取得します
        ball_x = ball_x + ball_speed_x;
        ball_y = ball_y + ball_speed_y;

        if( ball_x < 0 ) {
        ball_x = 0;
        ball_speed_x = -ball_speed_x;
        }

        if( ball_y < 0 ) {
        ball_y = 0;
        ball_speed_y = -ball_speed_y;
        }

        if( ball_x > 616 ) {
        ball_x = 616;
        ball_speed_x = -ball_speed_x;
        }

        if( ball_y > 456 ) {
        ball_y = 456;
        ball_speed_y = -ball_speed_y;
        }
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        // 画面を白で塗りつぶします
        gc.ClearScreen();

        // 青空の画像を描画します
        gc.ClearScreen();

        // 0番の画像を描画します
        gc.DrawImage(GcImage.BlueSky, 0, 0);

	    gc.DrawImage(GcImage.BallYellow,ball_x,ball_y);
    }
}
