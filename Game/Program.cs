

using OpenTK.Windowing.Common;

namespace Minecraft_Clone_Tutorial_Series_videoproj
{
    public class Program
    {
        // Entry point of the program
        static void Main(string[] args)
        {


            Thread currentThread = Thread.CurrentThread;

            //получаем имя потока
            Console.WriteLine($"Имя потока: {currentThread.Name}");
            currentThread.Name = "Метод Main";
            Console.WriteLine($"Имя потока: {currentThread.Name}");

            Console.WriteLine($"Запущен ли поток: {currentThread.IsAlive}");
            Console.WriteLine($"Id потока: {currentThread.ManagedThreadId}");
            Console.WriteLine($"Приоритет потока: {currentThread.Priority}");
            Console.WriteLine($"Статус потока: {currentThread.ThreadState}");

            // Creates game object and disposes of it after leaving the scope
            using (Game game = new Game(2560, 1440))
            {
                // running the gamesktop;
                 game.Run();
            }
        }
    }
}