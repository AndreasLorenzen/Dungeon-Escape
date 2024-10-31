using System;

class DungeonEscape
{
    // 2D array representing the dungeon layout.
    // '-' and '|' represent the boundaries, ' ' represents open space,
    // 'K' represents keys, 'T' represents traps, 'E' is the exit, and 'S' is the player's starting position.
    static char[,] labyrint = {
        { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' },
        { '|', ' ', ' ', ' ', 'K', ' ', ' ', 'T', ' ', '|' },
        { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
        { '|', ' ', 'S', ' ', ' ', ' ', ' ', ' ', 'K', '|' },
        { '|', ' ', ' ', ' ', ' ', 'T', ' ', ' ', ' ', '|' },
        { '|', ' ', 'T', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
        { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'E', '|' },
        { '|', ' ', ' ', ' ', ' ', ' ', 'T', ' ', ' ', '|' },
        { '|', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '|' },
        { '-', '-', '-', '-', '-', '-', '-', '-', '-', '-' }
    };

    // Variables declared in a fild so i can use them accross all my methods
    static int spillerX = 3, spillerY = 2;


    static bool harNøgle = false;


    static bool spilKørende = true;

    static void Main()
    {
        Console.WriteLine("Velkommen til Dungeon Escape! Find nøglerne og undgå fælderne.");


        while (spilKørende)
        {
            VisLabyrint();
            OpdaterPosition(); 
        }
    }

    // This method shows the dungion and prints og the dungoin. 
    static void VisLabyrint()
    {
        Console.Clear();
        Console.WriteLine("Dungeon Kort:");

        // Loop through the dungeon rows and columns to print the layout.
        for (int i = 0; i < labyrint.GetLength(0); i++)
        {
            // Hvis makes sure that the symbols for traps etc. is hidden, and display p at playerposition.
            for (int j = 0; j < labyrint.GetLength(1); j++)
            {
                if (i == spillerX && j == spillerY)
                {
                    Console.Write("P ");
                }
                else if (labyrint[i, j] == 'T' || labyrint[i, j] == 'K' || labyrint[i, j] == 'E')
                {
                    Console.Write(". ");
                }
                else
                {
                    Console.Write(labyrint[i, j] + " ");
                }
            }
            Console.WriteLine(); // Move to the next line after each row is printed.
        }

        // Shows if the player have found the keys or not.
        Console.WriteLine("\nStatus: " + (harNøgle ? "Du har nøglen!" : "Ingen nøgle endnu."));
        Console.WriteLine("Indtast din retning ('op', 'ned', 'venstre', 'højre'): ");
    }

    // Method to update the player's position based on their input. The nyX and nyY variables are storing the potensial new position.
    static void OpdaterPosition()
    {
        string retning = Console.ReadLine().ToLower();

        int nyX = spillerX;
        int nyY = spillerY;

        switch (retning)
        {
            case "op":
                nyX--; // Move up (decrease x-coordinate).
                break;
            case "ned":
                nyX++; // Move down (increase x-coordinate).
                break;
            case "venstre":
                nyY--; // Move left (decrease y-coordinate).
                break;
            case "højre":
                nyY++; // Move right (increase y-coordinate).
                break;
            default:
                Console.WriteLine("Ugyldig retning. Prøv igen.");
                return; 
        }

        // This checks if the new position is within bounds and not blocked by walls.
        if (nyX >= 0 && nyX < labyrint.GetLength(0) && nyY >= 0 && nyY < labyrint.GetLength(1) &&
            labyrint[nyX, nyY] != '-' && labyrint[nyX, nyY] != '|')
        {
            spillerX = nyX;
            spillerY = nyY;
            TjekHændelser();
        }
        else
        {
            Console.WriteLine("Du kan ikke gå udenfor labyrinten.");
        }
    }

    // Method to check for events or items at the player's current location.
    static void TjekHændelser()
    {
        char rum = labyrint[spillerX, spillerY];

        if (rum == 'K')
        {
            Console.WriteLine("Du har fundet en nøgle!"); 
            harNøgle = true; 
            labyrint[spillerX, spillerY] = ' '; 
        }
        else if (rum == 'T')
        {
            Console.WriteLine("Du faldt i en fælde! Spillet er slut."); 
            spilKørende = false; 
            return; 
        }
        else if (rum == 'E')
        {
            // Allow exit only if the player has the key.
            if (harNøgle)
            {
                Console.WriteLine("Du har fundet udgangen og vundet spillet!"); 
                spilKørende = false; 
            }
            else
            {
                Console.WriteLine("Du skal finde nøglen først!"); 
            }
        }
    }
}
