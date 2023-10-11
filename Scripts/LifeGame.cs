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
        /// <param name="height">縦の座標</param>
        /// <param name="width">横の座標</param>
        /// <returns>生きているならTrue</returns>
        private bool IsAlive(int height, int width)
        {
            int LifeCount = 0;

            int left = width == 0 ? 0 : width - 1;
            int right = width == widthSize - 1 ? widthSize - 1 : width + 1;

            int under = height == 0 ? 0 : height - 1;
            int up = height == heightSize - 1 ? heightSize - 1 : height + 1;

            // 自分含めた周囲9マスのcellの生存状態を確認する
            for (int x = left; x <= right; x++)
            {
                for (int y = under; y <= up; y++)
                {
                    if (aliveCell[y, x] is false)
                    {
                        continue;
                    }

                    LifeCount++;
                }
            }

            if (aliveCell[height, width])
            {
                // 周囲8マスの生存cell数
                int aroundLifeCount = LifeCount - 1;
                // 周囲の生きているcellが 2<=x<=3 ならば生存
                return aroundLifeCount >= 2 && aroundLifeCount <= 3;
            }
            else
            {
                // 自分が死んでいて周囲の生きているcell数が3つならば誕生
                return LifeCount == 3;
            }
        }

        /// <summary>
        /// 世代を１つ進める
        /// </summary>
        /// <returns>次世代のcellの状態</returns>
        public bool[,] NextGeneration()
        {
            bool[,] newGene = new bool[heightSize, widthSize];
            for (int i = 0; i < heightSize; i++)
            {
                for (int j = 0; j < widthSize; j++)
                {
                    newGene[i, j] = IsAlive(i, j);
                }
            }
            return newGene;
        }

        /// <summary>
        /// 渡されたcellの状態を描画する
        /// 生存:■ 死亡:□
        /// </summary>
        /// <param name="cells"></param>
        public void RenderState()
        {
            for (int i = 0; i < heightSize; i++)
            {
                for (int j = 0; j < widthSize; j++)
                {
                    Console.Write(aliveCell[i, j] ? '■' : '□');
                }
                Console.WriteLine();
            }
        }
    }
}