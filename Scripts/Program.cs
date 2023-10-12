// See https://aka.ms/new-console-template for more information

namespace LifeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] init = InitSetting();
            int height = init.GetLength(0);
            int width = init.GetLength(1);
            int generationCount = 0;

            Random random = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // ランダムに0,1を代入する
                    init[y, x] = random.Next(2);
                }
            }

            LifeGame lifeGame = new LifeGame(height, width);
            lifeGame.aliveCell = Int2Bool(init);

            // 自動で世代を進める
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{generationCount}世代目");
                lifeGame.RenderState();
                Thread.Sleep(700);
                lifeGame.aliveCell = lifeGame.NextGeneration();
                generationCount++;
            }
        }

        /// <summary>
        /// 初期設定
        /// </summary>
        /// <returns>[height, width]</returns>
        static int[,] InitSetting()
        {
            int width;
            int height;
            bool parseResult;

            do
            {
                Console.Write("横の長さは？：");
                parseResult = int.TryParse(Console.ReadLine(), out width);
                if (!parseResult) Console.WriteLine("数字を入力してね！");
            } while (!parseResult);

            do
            {
                Console.Write("縦の高さは？：");
                parseResult = int.TryParse(Console.ReadLine(), out height);
                if (!parseResult) Console.WriteLine("数字を入力してね！");
            } while (!parseResult);

            return new int[height, width];
        }

        static bool[,] Int2Bool(int[,] ints)
        {
            int heightLength = ints.GetLength(0);
            int widthLength = ints.GetLength(1);
            bool[,] cellStates = new bool[heightLength, widthLength];

            for (int y = 0; y < heightLength; y++)
            {
                for (int x = 0; x < widthLength; x++)
                {
                    // 0,1以外が入っていたらエラーを吐く
                    if (!(ints[y, x] == 0 || ints[y, x] == 1)) throw new ArgumentException();

                    cellStates[y, x] = ints[y, x] == 1;
                }
            }
            return cellStates;
        }
    }
}