namespace LifeGame
{
    internal class LifeGame
    {
        private int heightSize;
        private int widthSize;

        /// <summary>
        /// 座標のcellの生存状態
        /// </summary>
        public bool[,] aliveCell;

        /// <summary>
        /// 全体の大きさを決めるコンストラクタ
        /// </summary>
        /// <param name="heightSize">縦</param>
        /// <param name="widthSize">横</param>
        public LifeGame(int heightSize, int widthSize)
        {
            this.heightSize = heightSize;
            this.widthSize = widthSize;

            aliveCell = new bool[heightSize, widthSize];
        }

        /// <summary>
        /// cellの生死を判定する
        /// </summary>
        /// <param name="y">縦の座標</param>
        /// <param name="x">横の座標</param>
        /// <returns>生きているならTrue</returns>
        private bool IsAlive(int y, int x)
        {
            int lifeCount = 0;

            // 自分含めた周囲9マスのcellの生存状態を確認する
            for (int i = y - 1; i < y + 1; i++)
            {
                // 0 <= i < heightSize
                if ((uint)i > heightSize) continue;
                for (int j = x - 1; j < x + 1; j++)
                {
                    // 0 <= j < widthSize
                    if ((uint)j > widthSize) continue;

                    if (!aliveCell[i, j]) continue;
                    lifeCount++;
                }
            }

            if (aliveCell[y, x])
            {
                // 周囲8マスの生存cell数
                int aroundLifeCount = lifeCount - 1;
                // 周囲の生きているcellが 2<=x<=3 ならば生存
                return aroundLifeCount >= 2 && aroundLifeCount <= 3;
            }
            else
            {
                // 自分が死んでいて周囲の生きているcell数が3つならば誕生
                return lifeCount == 3;
            }
        }

        /// <summary>
        /// 世代を１つ進める
        /// </summary>
        /// <returns>次世代のcellの状態</returns>
        public bool[,] NextGeneration()
        {
            bool[,] newGene = new bool[heightSize, widthSize];
            for (int y = 0; y < heightSize; y++)
            {
                for (int x = 0; x < widthSize; x++)
                {
                    newGene[y, x] = IsAlive(y, x);
                }
            }
            return newGene;
        }

        /// <summary>
        /// cellの状態を描画する
        /// 生存:■ 死亡:□
        /// </summary>
        /// <param name="cells"></param>
        public void RenderState()
        {
            for (int y = 0; y < heightSize; y++)
            {
                for (int x = 0; x < widthSize; x++)
                {
                    Console.Write(aliveCell[y, x] ? '■' : '□');
                }
                Console.WriteLine();
            }
        }
    }
}