// See https://aka.ms/new-console-template for more information

namespace LifeGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int height;
            int width;
            int generationCount = 0;

            (width, height) = InitSetting();
            int[,] init = new int[height, width];

            Random random = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // ランダムに0,1を代入する
                    init[i, j] = random.Next(2);
                }
            }

            LifeGame lifeGame = new LifeGame(height, width);
            lifeGame.isAliveCell = Int2Bool(init);

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{generationCount}世代目");
                lifeGame.RenderState();
                Thread.Sleep(700);
                lifeGame.isAliveCell = lifeGame.NextGeneration();
                generationCount++;
            }
        }

        /// <summary>
        /// 初期設定
        /// </summary>
        /// <returns>(width,height)</returns>
        static (int, int) InitSetting()
        {
            int width;
            int height;
            bool parseResult;

            Console.Write("横の長さは？：");
            parseResult = int.TryParse(Console.ReadLine(), out width);
            if (!parseResult)
            {
                Console.WriteLine("数字を入力してね！");
                return InitSetting();
            }

            Console.Write("縦の高さは？：");
            parseResult = int.TryParse(Console.ReadLine(), out height);
            if (!parseResult)
            {
                Console.WriteLine("数字を入力してね！");
                return InitSetting();
            }

            return (width, height);
        }

        static bool[,] Int2Bool(int[,] ints)
        {
            int heightLength = ints.GetLength(0);
            int widthLength = ints.GetLength(1);
            bool[,] cellStates = new bool[heightLength, widthLength];

            for (int i = 0; i < heightLength; i++)
            {
                for (int j = 0; j < widthLength; j++)
                {
                    // 0,1以外が入っていたらエラーを吐く
                    if (!(ints[i, j] == 0 || ints[i, j] == 1)) throw new ArgumentException();

                    cellStates[i, j] = ints[i, j] == 1;
                }
            }
            return cellStates;
        }
    }
}