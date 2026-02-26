namespace project_1;

internal class Program
{
    private static void Main(string[] args)
    {
        Random random = new Random();
        // Счётчики для итоговой статистики по всем сыгранным играм
        int totalGames = 0;
        int totalAttempts = 0;
        int minAttempts = int.MaxValue;
        int maxAttempts = 0;

        Console.WriteLine("добро пожаловать в игру!");
        Console.WriteLine("Я придумал число от 0 до 100");

        // Основной игровой цикл — продолжается, пока игрок не решит выйти
        while (true)
        {
            // Генерируем новое загаданное число для каждой игры
            int secretNumber = random.Next(0, 101);
            int guess = -1;
            int attempts = 0;
            int invalidInputCount = 0; // Счётчик подряд идущих некорректных вводов
            bool gameWon = false;
            bool playedIncorrectly = false; // Флаг: игрок превысил лимит ошибок ввода

            Console.WriteLine($"Игра {++totalGames}");

            // Цикл угадывания — продолжается до правильного ответа или нарушения правил
            while (guess != secretNumber)
            {
                Console.WriteLine("напиши свое число: ");
                string? input = Console.ReadLine();

                // Проверяем корректность ввода: должно быть целое число в диапазоне 0..100
                if (!int.TryParse(input, out guess) || guess is > 100 or < 0)
                {
                    invalidInputCount++;
                    // Более 3 некорректных вводов подряд — игра прерывается
                    if (invalidInputCount > 3)
                    {
                        playedIncorrectly = true;
                        break;
                    }
                    Console.WriteLine("введи число корректнее (0..100): ");
                    continue;
                }

                // Сброс счётчика ошибок после успешного ввода
                invalidInputCount = 0;
                attempts++;

                // Подсказка: сравниваем догадку с загаданным числом
                if (guess < secretNumber)
                {
                    Console.WriteLine("слишком мало");
                }
                else if (guess > secretNumber)
                {
                    Console.WriteLine("число меньше");
                }
                else
                {
                    Console.WriteLine("you win");
                    gameWon = true;
                    break;
                }
            }

            // Если игрок исчерпал попытки на корректный ввод — завершаем программу
            if (playedIncorrectly)
            {
                Console.WriteLine("Простите, вы играли некорректно.");
                return;
            }

            // Обновляем статистику только при победе (не при некорректной игре)
            if (gameWon)
            {
                totalAttempts += attempts;
                minAttempts = Math.Min(minAttempts, attempts);
                maxAttempts = Math.Max(maxAttempts, attempts);
            }

            ShowStats(totalGames, totalAttempts, minAttempts, maxAttempts);

            // Предлагаем сыграть ещё раз
            Console.WriteLine("\nХочешь еще? (y/n): ");
            string? playAgain = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (playAgain is not ("y" or "да" or "yes"))
            {
                Console.WriteLine("gg ty game");
                break;
            }

            Console.WriteLine();
        }
    }

    // Выводит накопленную статистику: количество игр, попыток, среднее, минимум и максимум
    private static void ShowStats(int games, int attempts, int min, int max)
    {
        if (games == 0)
            return;

        double avg = (double)attempts / games;
        Console.WriteLine(
            $"--- Статистика: игр — {games}, попыток — {attempts}, среднее — {avg:F1}, мин — {min}, макс — {max} ---");
    }
}
