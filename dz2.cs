namespace GameFindNamber
{
    internal class Program
    {
        int min = 0;
        int max = 0;
        int count = 0;
        int countGame = 0;
        int left = 0;
        int right = 100;
        int counter = 0;

        public int?  GetNumber()
        {
           int attempt = 0;
            for (int i = 0; i < 3; i++)
            {

                if (!int.TryParse(Console.ReadLine(), out attempt)
                    || attempt > 100 || attempt < 0)
                    Console.WriteLine($"Input number from [{left};{right}]");
                else break;
                if (i == 2)
                {
                    Console.WriteLine("You are stupid");
                    return null;
                }
            }
            return attempt;
        }

        public bool CompareNumber(int attempt, int rndnum)
        {
            if (rndnum < attempt)
                Console.WriteLine("Secret number is smaller — try a lower number");
            else if (rndnum > attempt)
                Console.WriteLine("Secret number is larger — try a higher number");
            else
            {
                Console.WriteLine("You win!");
                return true;
            }
            return false;
        }

        public void PlayGame(int rndnum)
        {
            while (true)
            {
                Console.WriteLine($"Input number from [{left};{right}]");

                int? result = GetNumber();
                if (result == null)
                    return;
                int attempt = result.Value;
                bool flag = CompareNumber(attempt, rndnum);
                if (flag == true)
                    break;
            }
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            Program program = new Program();
            char answer = 'Y';
            do
            {
                int rndnum = rnd.Next(program.left, program.right + 1);
                int attempt = 0;
                int counter = 0;
                program.PlayGame(rndnum);
                Console.WriteLine("Do you want play again");
                answer = Convert.ToChar(Console.Read());
            } while (answer == 'Y');
            //Console.WriteLine($"min = {min} max = {max} avg ={(double)count / countGame}");
        }
    }
}
