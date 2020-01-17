using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Tex_Adventure_Game
{
    class MainSpace
    {
        //ints
        public static int index = 0;
        public static int correct = 0;
        public static int itemID = 0;
        public static int playerSword = 0;
        public static int playerRanged = 0;
        public static int playerMagic = 0;
        public static int playerMagicDmg = 2;
        public static int playerSwordDmg = 2;
        public static int playerRangedDmg = 2;
        public static int playerHP = 20;
        public static int playerMaxHP = 20;
        public static int playerMana = 12;
        public static int playerMaxMana = 12;
        public static int randDamage = 0;

        //strings
        public static string input;
        public static string item = null;
        public static string mob = null;
        //enemy stats for beastiary and combat
        public static readonly List<string> enemyName = new List<string> { "Wolf", "Sheno", "Acmer", "Pipheon", "Woedel", "Xenome" };
        public static readonly List<int> enemyHP = new List<int> { 10, 13, 18, 24, 40, 26 };
        public static readonly List<int> DefaultEnemyHP = new List<int> { 10, 13, 18, 24, 40, 26 };
        public static readonly List<int> enemyMaxDmg = new List<int> { 2, 4, 6, 10, 3, 15 };
        public static readonly List<string> enemyDesc = new List<string> { "Good at biting", "Has a powerful axe", "Incredible warriors", "Flying beasts that hurt", "Strong defenses but weak attacks", "Able to hit the player with dangerous potions" };

        //Items available for inventory
        public static readonly List<string> items = new List<string> { "Egg", "Shovel", "Machete", "Staff", "Flower", "Bow" };
        public static readonly List<string> itemsDesc = new List<string> { "Good for cooking breakfast, wouldn't eat raw...", "Good for digging", "Good for stabbing", "Good for magic", "Pretty things are nice", "Good for archery" };
       
        public static List<string> inventory = new List<string> { "Exit" };
        public static List<string> beastiary = new List<string> { "Exit" };

        //bools
        public static bool foundItem = false;

        //runs menu selector (not mine)
        private static string drawMenu(List<string> menus)
        {
            Console.Clear();
            for (int i = 0; i < menus.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(menus[i]);
                }
                else
                {
                    Console.WriteLine(menus[i]);
                }
                Console.ResetColor();
            }
            ConsoleKeyInfo ckey = Console.ReadKey();
            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == menus.Count - 1)
                {
                    index = 0; //Remove the comment to return to the topmost item in the list
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = menus.Count - 1; //Remove the comment to return to the item in the bottom of the list
                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return menus[index];
            }
            else if (ckey.Key == ConsoleKey.Q && menus[index] != "Exit" && !beastiary.Contains(menus[index]))
            {
                menus.Remove(menus[index]);
                index--;
            }
            else
            {
                return "";
            }
            return "";
        }
        //combat system
        public static void Combat(bool tutorial)
        {
            bool finished = false;
            do
            {
                Console.Title = ("-=-COMBAT : " + enemyName[0].ToUpper() + "-=-");
                correct = 0;
                Console.Clear();
                if (!beastiary.Contains(enemyName[0]))
                {
                    beastiary.Add(enemyName[0]);
                }
                Console.WriteLine("Your Health is at {0} Points, Your Mana is at {1} Points", playerHP, playerMana);
                Console.WriteLine("The {0}s Health is at {1} Points", enemyName[0], enemyHP[0]);
                Console.WriteLine("Enter the number for the type of attack that you wish to perform:");
                Console.WriteLine("1. Sword Attack");
                Console.WriteLine("2. Ranged Weapon Attack");
                Console.WriteLine("3. Magical Attack");
                Console.Write(">> ");
                int playerAttackOption = int.Parse(Console.ReadLine());
                if (playerAttackOption == 1)
                {
                    Random randDmg = new Random();
                    int randDamage = randDmg.Next(0, playerSwordDmg);
                    Console.WriteLine("Damage inflicted: {0}", randDamage);
                    Random randCritChance = new Random();
                    int skillPointResult = randCritChance.Next(0, 100);
                    if (skillPointResult >= 80 && randDamage > 0)
                    {
                        randDamage += playerSword;
                        Console.WriteLine("You did a critical hit, +{0} damage!", playerSword);
                    }
                    Console.WriteLine("You did {0} Damage to the {1}", randDamage, enemyName[0]);
                    enemyHP[0] -= randDamage;
                }
                if (playerAttackOption == 2)
                {
                    Random randDmg = new Random();
                    int randDamage = randDmg.Next(0, playerRanged);
                    Console.WriteLine("Damage inflicted: {0}", randDamage);
                    Random randCritChance = new Random();
                    int skillPointResult = randCritChance.Next(0, 100);
                    if (skillPointResult >= 80 && randDamage > 0)
                    {
                        randDamage += playerRanged;
                        Console.WriteLine("You did a critical hit, +{0} damage!", playerRanged);
                    }
                    Console.WriteLine("You did {0} Damage to the {1}", randDamage, enemyName[0]);
                    enemyHP[0] -= randDamage;
                }
                if (playerAttackOption == 3)
                {
                    if (playerMana == 0)
                    {
                        Console.WriteLine("Not enough mana!");
                        Console.ReadKey();
                        Combat(tutorial);
                    }
                    Random randDmg = new Random();
                    int randDamage = randDmg.Next(0, playerMagic);
                    Console.WriteLine("Damage inflicted: {0}", randDamage);
                    Random randCritChance = new Random();
                    int skillPointResult = randCritChance.Next(0, 100);
                    if (skillPointResult >= 60 && randDamage > 0)
                    {
                        randDamage += playerMagic;
                        Console.WriteLine("You did a critical hit, +{0} damage!", playerMagic);
                    }
                    Console.WriteLine("You did {0} Damage to the {1}", randDamage, enemyName[0]);
                    playerMana -= 3;
                    enemyHP[0] -= randDamage;
                }
                if (enemyHP[0] > 0)
                {
                    Random randEnemyDmg = new Random();
                    int enemyDmg = randEnemyDmg.Next(0, enemyMaxDmg[0]);
                    Console.WriteLine("The {0} hits back, and deal {1} Points of Damage", enemyName[0], enemyDmg);
                    playerHP -= enemyDmg;
                    Console.ReadLine();
                }
                if (enemyHP[0] < 0)
                {
                    enemyHP[0] = 0;
                }
                if (playerHP < 0)
                {
                    playerHP = 0;
                }
                Console.WriteLine("Your Health is at {0} Points, Your Mana is at {1} Points", playerHP, playerMana);
                Console.WriteLine("The {0}s Health is at {1} Points", enemyName[0], enemyHP[0]);
                if (enemyHP[0] <= 0 || playerHP <= 0)
                {
                    if (enemyHP[0] > 0 && playerHP == 0)
                    {
                        Console.WriteLine("");
                        Thread.Sleep(1000);
                        Console.WriteLine(" - You died in battle -");
                        Console.ReadKey();
                        finished = true;
                    }
                    if (playerHP > 0 && enemyHP[0] == 0)
                    {
                        Console.WriteLine("");
                        Console.ReadKey();
                        Console.WriteLine(" - You won the battle -");
                        Console.ReadKey();
                        enemyHP[0] = DefaultEnemyHP[0];
                        Console.Title = "The Ember Isle";
                        finished = true;
                    }
                }
                else
                {
                    Console.WriteLine("Enter the number for the type of attack that you wish to perform:");
                    Console.WriteLine("1. Sword Attack");
                    Console.WriteLine("2. Ranged Weapon Attack");
                    Console.WriteLine("3. Magical Attack");
                    Console.Write(">> ");
                }

            } while (finished == false);
            Console.Clear();
            if (tutorial)
            {
                playerMana = 5;
                playerHP = 20;
            }
            //if player dies then it is permanent 
            if (playerHP <= 0)
            {
                Console.WriteLine("Do you want to start again? (yes/no)");
                string startOver = Console.ReadLine().ToLower();
                if (startOver == "yes"||startOver == "y")
                {
                    Main(null);
                }
                else
                {
                    Console.WriteLine("Thanks for playing!");
                    Thread.Sleep(1200);
                    Environment.Exit(0);
                }
            }
        }
        public static void CharacterCreation()
        {
            //Strings:
            string playerGender;
            string playerRace;
            string playerClass;
            string playerName;

            //Character Creation:
            do
            {
                Console.Clear();
                Console.WriteLine("Please choose a sex:");
                Console.Write("Male / Female\n>> ");
                playerGender = Console.ReadLine().ToLower();
                if (playerGender == "male" || playerGender == "female")
                {
                    correct = 1;
                }
                if (playerGender == "jack")
                {
                    playerSword += 10000;
                    playerRanged += 10000;
                    playerMagic += 10000;
                    correct = 1;
                    Start();
                }
            } while (correct == 0);
            correct = 0;

            //playerRace Creation:
            do
            {
                Console.Clear();
                Console.WriteLine("Please choose a race:");
                Console.WriteLine("> Human");
                Console.WriteLine("> Dwarf");
                Console.WriteLine("> Elf");
                Console.Write(">> ");
                playerRace = Console.ReadLine().ToLower();
                switch (playerRace)
                {
                    case "human":
                        Console.WriteLine("This race gives a bonus to the following stats:");
                        Console.WriteLine("Sword. 1 Point");
                        Console.WriteLine("Ranged : 1 Point");
                        Console.WriteLine("Magic : 1 Point");
                        Console.Write("Is this the race you wish to play? (yes/no)\n>> ");
                        input = Console.ReadLine().ToLower();
                        if (input == "yes" || input == "y")
                        {
                            //race bonuses
                            playerSword++;
                            playerRanged++;
                            playerMagic++;
                            correct = 1;
                        }
                        if (input == "no" || input == "n")
                        {
                            correct = 0;
                        }
                        break;
                    case "dwarf":
                        Console.WriteLine("This race gives a bonus to the following stats:");
                        Console.WriteLine("Sword : 2 Point");
                        Console.WriteLine("Ranged : 1 Point");
                        Console.WriteLine("Magic : 0 Points");
                        Console.Write("Is this the race you wish to play? (yes/no)\n>> ");
                        input = Console.ReadLine().ToLower();
                        if (input == "yes" || input == "y")
                        {
                            //race bonuses
                            playerSword += 2;
                            playerRanged++;
                            correct = 1;
                        }
                        if (input == "no" || input == "n")
                        {
                            correct = 0;
                        }
                        break;
                    case "elf":
                        Console.WriteLine("This race gives a bonus to the following stats:");
                        Console.WriteLine("Sword : 0 Points");
                        Console.WriteLine("Ranged : 1 Points");
                        Console.WriteLine("Magic : 2 Point");
                        Console.Write("Is this the race you wish to play? (yes/no)\n>> ");
                        input = Console.ReadLine().ToLower();
                        if (input == "yes" || input == "y")
                        {
                            //race bonuses
                            playerRanged++;
                            playerMagic += 2;
                            correct = 1;
                        }
                        if (input == "no" || input == "n")
                        {
                            correct = 0;
                        }
                        break;
                }
            } while (correct == 0);
            correct = 0;

            //playerClass Creation:
            do
            {
                Console.Clear();
                Console.WriteLine("Please choose a class:");
                Console.WriteLine("> Warrior");
                Console.WriteLine("> Mage");
                Console.WriteLine("> Thief");
                Console.Write(">> ");
                playerClass = Console.ReadLine().ToLower();
                switch (playerClass)
                {
                    case "warrior":
                        Console.WriteLine("This class gives a bonus to the following stats:");
                        Console.WriteLine("Sword : 3 Point");
                        Console.WriteLine("Ranged : 0 Point");
                        Console.WriteLine("Magic : 0 Point");
                        Console.Write("Is this the class you wish to play? (yes/no)\n>> ");
                        input = Console.ReadLine().ToLower();
                        if (input == "yes" || input == "y")
                        {
                            //class bonuses
                            playerSword += 3;
                            correct = 1;
                        }
                        if (input == "no" || input == "n")
                        {
                            correct = 0;
                        }
                        break;
                    case "mage":
                        Console.WriteLine("This class gives a bonus to the following stats:");
                        Console.WriteLine("Sword : 0 Point");
                        Console.WriteLine("Ranged : 1 Point");
                        Console.WriteLine("Magic : 2 Point");
                        Console.Write("Is this the class you wish to play? (yes/no)\n>> ");
                        input = Console.ReadLine().ToLower();
                        if (input == "yes" || input == "y")
                        {
                            //class bonuses
                            playerRanged++;
                            playerMagic += 2;
                            correct = 1;
                        }
                        if (input == "no" || input == "n")
                        {
                            correct = 0;
                        }
                        break;
                    case "thief":
                        Console.WriteLine("This class gives a bonus to the following stats:");
                        Console.WriteLine("Sword : 1 Point");
                        Console.WriteLine("Ranged : 2 Point");
                        Console.WriteLine("Magic : 0 Point");
                        Console.Write("Is this the class you wish to play? (yes/no)\n>> ");
                        input = Console.ReadLine().ToLower();
                        if (input == "yes" || input == "y")
                        {
                            //class bonuses
                            playerSword++;
                            playerRanged += 2;
                            correct = 1;
                        }
                        if (input == "no" || input == "n")
                        {
                            correct = 0;
                        }
                        break;
                }
            } while (correct == 0);
            correct = 0;
            do
            {
                Console.Clear();
                Console.Write("Name your character\n>> ");
                playerName = Console.ReadLine();
                if (playerName == null|| playerName == "")
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Name");
                    Console.ReadKey();
                }
                else
                {
                    correct = 1;
                }

            } while (correct == 0);
            correct = 0;

            //Player Description
            Console.Clear();
            Console.WriteLine("Your full character description, is:");
            Console.WriteLine("A {0} {1} {2} called {3}", playerGender, playerRace, playerClass, playerName);
            Console.WriteLine("Sword skill points: {0}", playerSword);
            Console.WriteLine("Ranged skill points: {0}", playerRanged);
            Console.WriteLine("Magic skill points: {0}", playerMagic);
            Console.ReadKey();
            Console.Clear();
            Console.Write("Would you like to run through the tutorial?\n(For first time players this is strongly recommended)\n>> ");
            input = Console.ReadLine().ToLower();
            if (input == "yes" || input == "y")
            {
                Console.Clear();
                Tutorial();
            }
            else
            {
                Start();
            }
        }
        //runs tutorial
        public static void Tutorial() 
        {
            bool tutorial = true;
            Console.Title = "Tutorial";
            Console.WriteLine("Welcome to the tutorial!\nTo begin, press any key...");
            Console.ReadKey();
            Console.Clear();
            do
            {
                //beastiary tut
                Console.Write("You can input 'B' to check the beastiary containing all monsters you have fought\n>> ");
                input = Console.ReadLine();
                if (input == "b")
                {
                    correct = 1;
                    Beastiary();
                    Console.Title = "Tutorial";
                    Console.WriteLine("You haven't fought any monsters on your way to the tutorial screen, so it appears empty\nFight more monsters to fill this page up");
                    Console.ReadKey();
                }
                else
                {
                    correct = 0;
                    Console.Clear();
                }
            } while (correct == 0);
            correct = 0;
            do
            {
                //inventory tut
                Console.Clear();
                Console.Write("You can input 'I' to check your inventory of items\n>> ");
                input = Console.ReadLine();
                if (input == "i")
                {
                    correct = 1;
                    item = items[itemID];
                    foundItem = true;
                    Inventory(item);
                    Console.Title = "Tutorial";
                    Console.WriteLine("This is where certain quest items and tools will appear\nYou only have 20 spaces - use them well!\nYou can also hover over an item and press 'Q' to drop it\n(WARNING: Once you have done this you cannot pick it back up)");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Inventory(item);
                    Console.Title = "Tutorial";
                }
                else
                {
                    correct = 0;
                    Console.Clear();
                }
            } while (correct == 0);
            correct = 0;
            do
            {
                correct = 1;
                //movement tut
                Console.WriteLine("You can move in the directions North, East, South and West by typing 'Go [DIRECTION]'");
                Console.ReadKey();
            } while (correct == 0);
            correct = 0;
            do
            {
                //combat system tutorial
                Console.Clear();
                Console.WriteLine("Now Im going to drop you into a fight\nIf you die - don't worry! It's just a test");
                Console.ReadKey();
                Combat(tutorial);
                Console.Title = "Tutorial";
                Console.WriteLine("That wasnt so bad was it?");
                Console.ReadKey();
                Console.Clear();
                do
                {
                    Console.Write("Now you can check your beastiary for your new encounter\n>> ");
                    input = Console.ReadLine();
                    if (input == "b")
                    {
                        Beastiary();
                        correct = 1;
                    }
                    else
                    {
                        Console.Clear();
                        correct = 0;
                    }
                    correct = 1;
                } while (correct == 0);
                Console.WriteLine("I think you're ready! Be careful...");
                Console.ReadKey();
                //!!dont forget to add health regen!!//
                Parallel.Invoke(() => ManaRegen(), () => Start());


            } while (correct == 0);
            correct = 0;
        }
        //beastiary containing all known monsters and demons
        public static void Beastiary()
        {
            Console.Title = "-=-BEASTIARY-=-";
            Console.Clear();
            while (true)
            {
                string selectedMenuItem = drawMenu(beastiary);
                if (selectedMenuItem == beastiary[0])
                {
                    Console.Clear();
                    break;
                }
                else if (selectedMenuItem == beastiary[index])
                {
                    Console.Clear();
                    Console.WriteLine("Maximum damage/hit: " + enemyMaxDmg[index-1]);
                    Console.WriteLine("Health: " + DefaultEnemyHP[index-1]);
                    Console.WriteLine("Description: " + enemyDesc[index-1]);
                    Console.ReadKey();

                }
            }
            Console.Title = "The Ember Isle";
        }
        //inventory of player
        public static void Inventory(string item)
        {
            Console.Title = "-=-INVENTORY-=-";
            if (foundItem)
            {
                inventory.Add(item);
            }
            foundItem = false;
            Console.Clear();
            while (true)
            {
                string selectedMenuItem = drawMenu(inventory);
                if (selectedMenuItem == inventory[0])
                {
                    Console.Clear();
                    break;
                }
                else if (selectedMenuItem == inventory[index])
                {
                    Console.Clear();
                    Console.WriteLine(itemsDesc[itemID]);
                    Console.ReadKey();

                }
            }
            Console.Title = "The Ember Isle";
        }
        //mana regen over time
        public static void ManaRegen()
        {
            do
            {
                if (playerMana < 12)
                {
                    Thread.Sleep(30000);
                    playerMana += 2;
                }
                if (playerMana > 12)
                {
                    playerMana = 12;
                }

            } while (playerMana != 12);
        }
        public static void Start()
        {
            Random rnd = new Random();
            //player = 1
            //paths = 2
            //enemies = 3-9
            //anything else = ???
            int[,] map = new int[15, 15];
            Console.Title = "The Ember Isle";
            Console.WriteLine("Woohoo!");
            Console.Clear();
            //declares 1s position
            int xof1 = rnd.Next(0, 15);
            int yof1 = rnd.Next(0, 15);
            map[yof1, xof1] = 1;
            Console.Write("Pos of 1: {0}, {1}\n", xof1, yof1);
            do
            {
                Console.Write(">> ");
                input = Console.ReadLine().ToLower();
                Match movement = new Regex(@"(go |head |move )").Match(input);
                if (movement.Success)
                {
                    string secondWord = input.Split(' ').Skip(1).FirstOrDefault().ToLower();
                    switch (secondWord)
                    {
                        case "north":
                            if (yof1 == 0)
                            {
                                Console.WriteLine("Can't go");
                            }
                            else
                            {
                                yof1++;
                                map[yof1--, xof1] = 0;
                            }
                            break;
                        case "south":
                            if (yof1 == 14)
                            {
                                Console.WriteLine("Can't go");
                            }
                            else
                            {
                                yof1--;
                                map[yof1++, xof1] = 0;
                            }
                            break;
                        case "east":
                            if (xof1 == 0)
                            {
                                Console.WriteLine("Can't go");
                            }
                            else
                            {
                                xof1++;
                                map[yof1, xof1--] = 0;
                            }
                            break;
                        case "west":
                            if (xof1 == 14)
                            {
                                Console.WriteLine("Can't go");
                            }
                            else
                            {
                                xof1--;
                                map[yof1, xof1++] = 0;
                            }
                            break;
                        default:
                            Console.WriteLine("Direction not recognised");
                            break;
                    }
                }
                else
                {
                    if (input == "quit")
                    {
                        Console.WriteLine("Exiting");
                    }
                    else
                    {
                        Console.WriteLine("I don't understand " + input.Split(' ')[0]);
                    }

                }
                map[yof1, xof1] = 1;
            } while (input != "quit");

            //prints grid
            int rowLength = map.GetLength(0);
            int colLength = map.GetLength(1);
            for (int x = 0; x < rowLength; x++)
            {
                for (int y = 0; y < colLength; y++)
                {
                    Console.Write(string.Format("{0}\t", map[x, y]));
                }
                Console.Write("\n\n");
            }

            Console.ReadKey();
        }
        public static void Main(string[] args)
        {
            Console.Title = "The Ember Isle";
            Console.WindowHeight = 40;
            Console.WindowWidth = 125;
            Console.SetBufferSize(125, 40);
            Console.CursorVisible = false;
            CharacterCreation();
        }
    }
}
