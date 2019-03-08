//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:			Exercise 1 - PigLatin
//	File Name:		    LinearProbing.cs
//	Description:		Working with strings and translating English language text into Pig Latin.
//	Course:			    CSCI 2210-001 - Data Structures
//	Author:			    Roland Patick Mahe, maher1@etsu.edu, Department of Computing, East Tennessee State University
//	Created:			Tuesday, February 05, 2019
//	Copyright:		    Roland Patrick Mahe, 2019
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise1
{
    /// <summary>
    /// Tools to use in the process of converting Enlglish to PigLatin
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// This method will parse strIn and return a List<String> containing 
        /// all the “words” and non-whitespace delimiters from strDelims
        /// </summary>
        /// <param name="strIn">An existing string</param>
        /// <param name="strDelims">Delimiters that will be used to parse strIn</param>
        /// <return>Returns a tokenized list of strIn</return>
        public static List<String> Tokenize(String strIn, String strDelims)
        {
            List<String> parsedStrIn = new List<String> { };//a List<String> that will store the tokenized or parsed version of strIn

            char[] delims = strDelims.ToCharArray();//converting strDelims to an array and storing that in the char array delims
            int delimIndex = -1;//stores the current index
            int startIndex = 0;//stores the previous index and/or starting index
            string subStr;//used to store a subStr
            
            do
            {
                delimIndex = strIn.IndexOfAny(delims, startIndex);//gets index of first character found from delims starting from the startIndex
                
                if (delimIndex > -1)//if a delimiter was found in strIn
                {
                    subStr = strIn.Substring(startIndex, delimIndex - startIndex);//assign subStr the substring from index startIndex to delimIndex
                                                                                      //(delimIndex - startIndex) converts it to appropriate length
                    if (!subStr.Equals(" ") && !subStr.Equals("") && !subStr.Equals("\n"))//if subStr isnt empty or equal to a space
                    {
                        parsedStrIn.Add(subStr);//add subStr to parsedStrIn
                    }
                    
                    if (startIndex == delimIndex)//if the startIndex is equal to the delimIndex (in other words, if the delim found was anything but a space we want to include that also)
                    {
                        subStr = strIn.Substring(startIndex, 1);//so we assign that delim to subStr

                        if (!subStr.Equals(" "))//and if that delim isn't a space
                        {
                            parsedStrIn.Add(subStr);//add it to parsedStrIn
                        }

                        delimIndex += 1;//then add one to the delimIndex so we don't get stuck in a infinte loop finding the same delim over and over
                    }
                }
                startIndex = delimIndex;//startIndex is assigned the value of delimIndex
            } while (delimIndex > -1);//continue this loop while delimIndex is greater that -1 (in other words, stop when you dont find a delimiter)

            return parsedStrIn;//return the parsed version of strIn
        }

        /// <summary>
        /// This method should format the strings in the list for display as one
        /// or more “sentences” that may wrap from one line to the next if it
        /// stretches more than 80 columns.
        /// </summary>
        /// <param name="parsedStr">An existing string that has been put in a list.</param>
        /// <returns>Returns the formatted string</returns>
        public static String Format(List<String> parsedStr)
        {
            int colCount = 0; //the current amount of columns in the current line
            String formattedStr = ""; //a string to hold the formatted string that will be returned at the end of this method
            List<String> capsules = new List<String> { "\"", "(", ")", "{", "}", "[", "]", "'", "`"};//a list of capsules ( (), [], "", '', etc..)
            List<String> strPunct = new List<String>{",", ".", "?", "!", ";", ":" };//a list of delimiters or punctuation marks
            String lastItem = ""; //stores the lastItem that was used in the foreach loop below
            bool inCap = false; //are the items being processed in a capsule ( (), [], "", '', etc..)
            String subStr = "";  //a string that will hold a substring of formattedStr while the program is adding a new line
            int indexToChange; //a integer that will hold the location of the index the program will be changing to a new line

            foreach (String item in parsedStr)//for each item in parsedStr
            {
                if (colCount >= 80)
                {
                    indexToChange = formattedStr.LastIndexOf(" ", (formattedStr.Length - 1)); //the index that will be changed is stored. I substract 1 from the length to get the index of the last character instead of the length
                    subStr = formattedStr.Substring(indexToChange + 1);//subStr is assigned everything after the 80th
                    formattedStr = formattedStr.Remove(indexToChange);//we remove everything from formatted str after the 80th column. (We have saved this part in subStr)
                    formattedStr += "\n" + subStr; //add a new line, then add subStr after the new line
                    colCount = 0; //set colCount to 0 again, because we are starting a new line
                    colCount += subStr.Length;//add the length of subStr to colCount, since we added subStr on the new line
                }

                if (strPunct.Contains<String>(item) || capsules.Contains<String>(lastItem) && inCap)//if the str variable is equal to anything in the strDelims
                {
                    formattedStr += item;//add str to formatted string
                    colCount += item.Length;//add the length of str to colCount
                }
                else if (capsules.Contains<String>(item))//if the current item is contained in the capusules list
                {
                    if(!inCap)//if we are not currently in a capsule e.g. this is our open bracket
                    {
                        formattedStr += " ";//add a space
                        colCount += 1;//add one to the column count because of the space
                        inCap = true;//we are now inside a capsule
                    }
                    else//if we ARE inside a capsule, e.g. this is our closing brake
                    {
                        inCap = false;//we are no longer in a capsule, because we are about to add the closing capsule
                    }
                    formattedStr += item;//add capsule
                }
                else//if anything else e.g. our item is a word
                {
                    formattedStr += " " + item;//add a space, then the item
                    colCount += item.Length + 1;//add the length of the item plus 1 (accounting for the space added) to the column count
                }


                lastItem = item;//assign the current item to lastItem
            }

            return formattedStr.Trim();//return the formatted string, trimming any leading spaces
        }
    }
}

