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

    // ラケット用の変数を追加
    int player_x;
    int player_y;
    int player_w;
    int player_h;

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

        // ラケットの初期化
        player_x = 270;
        player_y = 460;
        player_w = 100;
        player_h = 20;
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
        // 起動からの経過時間を取得します
        ball_x = ball_x + ball_speed_x;
        ball_y = ball_y + ball_speed_y;

        // 左端で跳ね返る
        if (ball_x < 0)
        {
            ball_x = 0;
            ball_speed_x = -ball_speed_x;
        }

        // 上端で跳ね返る
        if (ball_y < 0)
        {
            ball_y = 0;
            ball_speed_y = -ball_speed_y;
        }

        // 右端で跳ね返る
        if (ball_x > 616)
        {
            ball_x = 616;
            ball_speed_x = -ball_speed_x;
        }

        // ラケットとの当たり判定（ボールのサイズは24x24）
        if (gc.CheckHitRect(ball_x, ball_y, 24, 24, player_x, player_y, player_w, player_h))
        {
            // ボールが下に動いているときのみ跳ね返す
            if (ball_speed_y > 0)
            {
                ball_speed_y = -ball_speed_y;
            }
        }

        // 画面下に落ちたらボールを消す（動かなくする）
        if (ball_y > 480)
        {
            ball_speed_x = 0;
            ball_speed_y = 0;
            ball_x = -100; // 画面外に移動
            ball_y = -100;
        }

        // タップ位置に応じてラケットを移動
        if (gc.GetPointerFrameCount(0) > 0)
        {
            player_x = (int)gc.GetPointerX(0) - player_w / 2;
        }
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        // 画面を白で塗りつぶす
        gc.ClearScreen();

        // 青空の画像を描画
        gc.DrawImage(GcImage.BlueSky, 0, 0);

        // ボールの描画
        gc.DrawImage(GcImage.BallYellow, ball_x, ball_y);

        // ラケットの描画
        gc.SetColor(0, 0, 255);
        gc.FillRect(player_x, player_y, player_w, player_h);
    }
}
