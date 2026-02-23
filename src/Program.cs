class Program
{
    const int Size = 16;
    static bool[,] canvas = new bool[Size, Size];
    static int cursorX = 0;
    static int cursorY = 0;

    static void Main()
    {
        checkTerminalSize();
        Console.CursorVisible = false;
        Console.Clear();
        DrawCanvas();

        while (true)
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow: cursorY = Math.Max(0, cursorY - 1); break;
                case ConsoleKey.DownArrow: cursorY = Math.Min(Size - 1, cursorY + 1); break;
                case ConsoleKey.LeftArrow: cursorX = Math.Max(0, cursorX - 1); break;
                case ConsoleKey.RightArrow: cursorX = Math.Min(Size - 1, cursorX + 1); break;
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    canvas[cursorY, cursorX] = true; // paint
                    break;
                case ConsoleKey.Backspace:
                    canvas[cursorY, cursorX] = false; // erase
                    break;
                case ConsoleKey.Escape:
                    Console.ResetColor();
                    Console.Clear();
                    return;
            }
            DrawCanvas();
        }
    }

    static void DrawCanvas()
    {
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (x == cursorX && y == cursorY)
                    Console.BackgroundColor = ConsoleColor.DarkGray; // cursor
                else if (canvas[y, x])
                    Console.BackgroundColor = ConsoleColor.Red;
                else
                    Console.BackgroundColor = ConsoleColor.Black;

                Console.Write("  "); // 2 spaces = 1 pixel
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }

    // helpers
    static void checkTerminalSize()
    {
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;

        // not sure if this is enough
        if (width < 32 || height < 16)
        {
            Console.WriteLine("Terminal size too small! Please resize to at least 32x16 and restart.");
            Environment.Exit(0);
        }
    }
}