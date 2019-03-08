///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Exersise1/Exersise1
//	File Name:         Exersise1Driver.cs
//	Description:       A driver to control everthing from the other classes
//	Course:            CSCI 2210 - Data Structures	
//	Author:            Roland Mahe, maher1@etsu.edu, Deptartment of Computing, East Tennessee State University
//	Created:           Wednesday, February 06, 2019
//	Copyright:         Roland Mahe, 2019
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityNamespace;
using System.Windows.Forms;
using System.IO;

namespace Exercise1
{
    /// <summary>
    /// A driver class to control everything from the other classes.
    /// </summary>
    class Exercise1Driver
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        [STAThread]
        static void Main(string[] args)
        {
            #region Window Setup
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Title = "English to Pig Latin Translator";
            #endregion

            #region Variable setup
            String original = "";//a string to hold the original untranslated string
            List<String> originalToken = null;//a string to hold the original untranslated tokenized string
            String strDelims = " ,.?!/;:\n";//delimiters to watch out for

            PigLatin pg = null;//creating a piglatin object named pg
            UtilityNamespace.Menu menu = new UtilityNamespace.Menu("The Menu");
            menu = menu + "Open a file" + "Tokenize" + "Display Tokens" + "Translate to Piglatin" + "Display translation" + "Quit";

            Choices choice = (Choices)menu.GetChoice();
            #endregion

            #region Menu control
            while (choice != Choices.QUIT)
            {
                switch (choice)
                {
                    case Choices.OPEN:
                        Console.WriteLine("You selected Open");
                        original = getFile();//use the getFile() method to retrieve the path to the file
                        Console.WriteLine("Finished");
                        Console.ReadKey();
                        break;

                    case Choices.TOKENIZE:
                        Console.WriteLine("You selected Tokenize");
                        if(original.Equals(""))//if original is empty (a file hasnt been selected in this case)
                        {
                            Console.WriteLine("Please select a file first");
                        }
                        else
                        {
                            originalToken = Tools.Tokenize(original, strDelims);//storing the tokenized version of original in originalToken
                            Console.WriteLine("Finished");
                        }
                        Console.ReadLine();
                        break;
         
                    case Choices.SHOWTOKENS:
                        Console.WriteLine("You chose to display the tokens");
                        if(originalToken == null)//if origininalToken is empty
                        {
                            Console.WriteLine("Please tokenize your file first");
                        }
                        else
                        {
                            foreach (String item in originalToken)//for each item in originalTokens
                            {
                                Console.WriteLine(item);//display that item
                            }
                        }

                        Console.ReadKey();
                        break;

                    case Choices.TRANSLATE:
                        if (originalToken == null)//if the user hasn't tokenized original yet
                        {
                            Console.WriteLine("Please Tokenize first");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("You chose to translate");
                            pg = new PigLatin(original, strDelims);//initilizing object pg with a new PigLatin object based on the info given
                            Console.WriteLine("Finished");
                            Console.ReadLine();
                        }
                        break;

                    case Choices.DISPLAY:
                        if(pg == null)//if the originalToken hasn't been translated (if pg is equal to null)
                        {
                            Console.WriteLine("Please Translate first");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("You chose to display the translated string");
                            Console.ReadLine();
                            Console.WriteLine(pg);//using PigLatins toString method to display the info
                            Console.ReadLine();
                        }
                        break;
                }  // end of switch

                choice = (Choices)menu.GetChoice();
            }  // end of while
            #endregion
        }  // end of main

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">The file '{filePath}</exception>
        public static String getFile()
        {
            OpenFileDialog OpenDlg = new OpenFileDialog();//creating new object OpenDlg and initializing it
            OpenDlg.Filter = "text files|*.txt;*.text|all files|*.*";//giving the instructions on what files to show
            OpenDlg.InitialDirectory = Application.StartupPath;//setting the initial directory to the apps start up path
            OpenDlg.Title = "Select your desired text file";//setting the title text
            String filePath = "";//filePath will store the path to the file

            if(DialogResult.OK == OpenDlg.ShowDialog())//if the dialogs result is equal to OK
            {
                filePath = OpenDlg.FileName;//set the value of filepath to the path to the file chosen
            }

            if(!File.Exists(filePath))//if the file doesnt exist
            {
                throw new FileNotFoundException($"The file '{filePath}' was not found and could not be opened.");//throw an exception
            }

            StreamReader rdr = new StreamReader(filePath);//creating a new streamreader object called rdr
            String strWholeFile = rdr.ReadToEnd();//reading the whole file and storing it in  strWholeFile

            return strWholeFile;//return the whole file
        }
    }
}
