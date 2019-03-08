///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  Exersise1/Exersise1
//	File Name:         PigLatin.cs
//	Description:       This class translates any given text to piglatin
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

namespace Exercise1
{
    /// <summary>
    /// The class will translate English to Pig Latin
    /// </summary>
    class PigLatin
    {
        /// <summary>
        /// This propert represents the original piece of English text that was given
        /// </summary>
        /// <value>The original string in english</value>
        public String Original { get; set; }

        /// <summary>
        /// This propert represents the desired delimiters that will be used to split up 
        ///     the original string
        /// </summary>
        /// <value>The delimiters used to split up the string</value>
        public String strDelims { get; set; }

        /// <summary>
        /// Gets or sets the results. This property will store the translated version of Original
        ///     after it has been put through the Tools.Tokenize method
        /// </summary>
        /// <value>
        /// The results of translating using the Translate class
        /// </value>
        public List<String> Results { get; set; }

        /// <summary>
        /// Default constructor: Initializes a new instance
        ///     of the PigLatin class using no values
        /// </summary>
        public PigLatin()
        {
            this.Original = "This is a default string";
            this.strDelims = " ,.?!/;:";
        }

        /// <summary>
        /// Parameterized constructor: Initializes a new instance
        ///     of the PigLatin class using values specified in the parameters
        /// </summary>
        /// <param name="Original">The original english string, to be translated</param>
        public PigLatin(String Original, String strDelims)
        {
            this.Original = Original; //string given in contstructor is assigned to the 'Original' property
            this.strDelims = strDelims;//
            this.Results = Translate(Tools.Tokenize(Original, strDelims));
        }

        /// <summary>
        /// Translates the specified tokens to pig latin.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>Returns a list of translated words</returns>
        public List<String> Translate(List<String> tokens)
        {
            List<String> translatedStr = new List<string>();
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y'};//char list of vowels
            int index = 0;
            String modItem = "";//used to store a modified version of item 
            String transItem = "";//used to store a translated version of item 
            foreach(String item in tokens)//for each item in the list tokens
            {
                index = item.IndexOfAny(vowels);
                if (index > -1)//if the item has a vowel in it
                {
                    if(index == 0)
                    {
                        translatedStr.Add(item + "lay"); 
                    }
                    else
                    {
                        modItem = item.Substring(0, index);
                        transItem = item.Replace(modItem, "");
                        transItem += modItem.ToLower() + "ay";
                        translatedStr.Add(transItem);
                    }
                }
                else
                {
                    translatedStr.Add(item);
                }
            }

            return translatedStr;//**CHANGE**
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
           return Tools.Format(Results);
        }

    }
}
