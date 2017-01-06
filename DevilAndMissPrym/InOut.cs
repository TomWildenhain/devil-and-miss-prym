/*
 * Created by SharpDevelop.
 * User: 16wildenhaint
 * Date: 5/13/2015
 * Time: 1:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace DevilAndMissPrym
{
    /// <summary>
    /// Description of InOut.
    /// </summary>
    public class InOut
    {
        const int LETTER_PRINT_DELAY = 0;//20
        const int LINE_PRINT_DELAY = 50;
        const int LONG_PAUSE = 500;
        private static string charFirstName = "[first name]";
        private static string charLastName = "[last name]";
        private static string charTitle = "[title]";
        private static string charVillage = "[village]";
        private static bool fastPrint = false;
        private InOut()
        {
        }
        public static void setNameData(string firstName, string lastName, string title)
        {
            charFirstName = firstName;
            charLastName = lastName;
            charTitle = title;
        }
        public static void setVillage(string village)
        {
            charVillage = village;
        }
        private static string addDynamicText(string text)
        {
            string rVal = text.Replace("@fname", charFirstName);
            rVal = rVal.Replace("@lname", charLastName);
            rVal = rVal.Replace("@title", charTitle);
            rVal = rVal.Replace("@village", charVillage);
            return rVal;
        }
        private static string wrapText(string text)
        {
            string dText = addDynamicText(text);
            string[] lines = dText.Split('\n');
            string rVal = "";
            for (int j = 0; j < lines.Length; j++)
            {
                string[] words = lines[j].Split(' ');
                string fixedLine = "";
                int count = 0;
                int buffer = Console.BufferWidth - 1;
                if (buffer < 3)
                {
                    buffer = 3;
                }
                for (int i = 0; i < words.Length; i++)
                {
                    if (count + 1 + words[i].Length <= buffer)
                    {
                        if (i > 0)
                        {
                            fixedLine += " ";
                            count++;
                        }
                        fixedLine += words[i];
                        count += words[i].Length;
                    }
                    else if (count == buffer&&false)
                    {
                        fixedLine += words[i];
                        count = words[i].Length;
                    }
                    else
                    {
                        fixedLine += "\n" + words[i];
                        count = words[i].Length;
                    }
                }
                rVal += fixedLine;
                if (j != lines.Length - 1)
                {
                    rVal += "\n";
                }
            }
            return rVal;
        }
        public static void printLnSlow(string line)
        {
            printLnSlow(line, LETTER_PRINT_DELAY);
        }
        public static void printSlow(string line){
        	printSlow(line,LETTER_PRINT_DELAY);
        }
        public static void printSlow(string line, int delay){
        	//line = wrapText(line);
            if (fastPrint)
            {
                Console.Write(line);
            }
            else
            {
                int i;
                for (i = 0; i < line.Length; i++)
                {
                    if (line[i] + "" == " ")
                    {
                        Console.Write(line[i] + "");
                    }
                    else
                    {
                        break;
                    }
                }
                for (; i < line.Length; i++)
                {
                    Console.Write(line[i] + "");
                    sleep(delay);
                }
            }
        }//not wrapped
        public static void printLnSlow(string line, int delay)
        {
        	printSlow(wrapText(line), delay);
            Console.WriteLine();
        }
        public static void printLn(string line)
        {
            line = wrapText(line);
            Console.WriteLine(line);
        }
        public static void print(string line){
        	Console.Write(line);
        }
        public static void printLnsSlow(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                printLn(lines[i]);
                sleep(LINE_PRINT_DELAY);
            }
        }
        public static void printStartLogo()
        {
            string name = charTitle + " " + charLastName;
            string titleText = "The Devil and " + name;
            printFullScreen(titleText);
            printLn("");
        }
        public static void printFullScreen(string line){
            int buffer = Console.BufferWidth;
            int numOfStars = buffer - line.Length;
            int numToLeft = numOfStars / 2;
            int numToRight = numOfStars - numToLeft;
            string starsLeft = "";
            for (int i = 0; i < numToLeft; i++)
            {
                starsLeft += "*";
            }
            string starsRight = "";
            for (int i = 0; i < numToRight; i++)
            {
                starsRight += "*";
            }
            printSlow(starsLeft + line + starsRight);
        }
        private static void sleep(int time)
        {
            System.Threading.Thread.Sleep(time);
        }
        public static int askForNum(string errMsg, int maxVal)
        {
            string input = Console.In.ReadLine();
            while (true)
            {
                if (!checkFastPrint(input))
                {
                    try
                    {
                        int rVal = Convert.ToInt32(input);
                        if (rVal + "" == input && rVal <= maxVal)
                        {
                            return rVal - 1;
                        }
                    }
                    catch { }
                    printLnSlow(errMsg);
                }
                input = Console.In.ReadLine();
            }
        }
        public static string askForText(string errMsg, int minLength)
        {
            string input = Console.In.ReadLine().Trim();
            while (true)
            {
                if (!checkFastPrint(input))
                {
                    if (input.Length >= minLength)
                    {
                        return input;
                    }
                    printLnSlow(errMsg);
                }
                input = Console.In.ReadLine().Trim();
            }
        }
        public static bool checkFastPrint(string line)
        {
            if (line.IndexOf("#fast") > -1)
            {
                fastPrint = true;
            }
            else if (line.IndexOf("#slow") > -1)
            {
                fastPrint = false;
            }
            else
            {
                return false;
            }
            return true;
        }
        public static void pause(string message)
        {
        	print(message);
            string typed = Console.In.ReadLine();
            checkFastPrint(typed);
            int yPos=Console.CursorTop;
            Console.SetCursorPosition(0,yPos-1);
            string blank="";
            for (int i = 0; i < message.Length + typed.Length; i++)
            {
            	blank+=" ";
            }
            printLn(blank);
        }
    }
}
