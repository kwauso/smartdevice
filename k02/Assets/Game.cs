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
    int money;
    const int CARD_TYPE = 10;
    int[] card_count = new int[CARD_TYPE];
    string[] card_name = 
        {"A","B","C","D","E","F","G","H","I","J"};
    bool isComplete;
    int new_card;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public override void InitGame()
    {
        resetValue(); // 初期化処理をまとめた関数を呼ぶ
    }

    /// <summary>
    /// 動きなどの更新処理
    /// </summary>
    public override void UpdateGame()
    {
        if (gc.GetPointerFrameCount(0) == 1 && !isComplete) {
            money -= 100;

            // 出現確率の調整
            if (gc.Random(0, 99) < 25) { // 25%の確率
                new_card = gc.Random(0, 4); // A〜Eは5％
            } else {
                new_card = gc.Random(5, 9); // F〜Jは15％
            }

            card_count[new_card]++;

            isComplete = true;
            for (int i = 0; i < CARD_TYPE; i++) {
                if (card_count[i] == 0) {
                    isComplete = false;
                }
            }
        }

        // 長押しでリセット
        if (gc.GetPointerFrameCount(0) >= 120) {
            resetValue();
        }
    }

    /// <summary>
    /// 描画の処理
    /// </summary>
    public override void DrawGame()
    {
        gc.ClearScreen();
        gc.SetColor(0, 0, 0);
        gc.SetFontSize(36);
        gc.DrawString("money:" + money, 60, 40);

        if (new_card >= 0) {
            gc.DrawString("new:" + card_name[new_card], 60, 80);
        }

        for (int i = 0; i < CARD_TYPE; i++) {
            gc.DrawString(card_name[i] + ":" + card_count[i], 60, 120 + i * 40);
        }

        if (isComplete) {
            gc.DrawString("complete!!", 60, 520);
        }
    }

    // 初期化処理をまとめたメソッド
    void resetValue()
    {
        gc.ChangeCanvasSize(720, 1280);
        gc.SetRandomSeed((uint)gc.CurrentTimestamp * 1024);
        money = 10000;
        for (int i = 0; i < CARD_TYPE; i++) {
            card_count[i] = 0;
        }
        isComplete = false;
        new_card = -1;
    }
}
