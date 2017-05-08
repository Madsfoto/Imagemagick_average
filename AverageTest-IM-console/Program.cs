using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AverageTest_IM_console
{

    

    class Program
    {

        // External variables 
        // make external integer as starting point for the second loop, that is outside the loop. Get set something?

        public int numOfFilesNumber = 0;
        public string consoleString = "convert "; // The convert string that should be output of this application. 
        public string howFarStr = "";
        public string average = "-average ";
        public string outputFilename = "";



        public int SetNumOfFilesNumber(int newnr)
            {
            
            numOfFilesNumber = newnr;
            return newnr;
            }

        public int GetNumOfFilesNumber()
        {
            return numOfFilesNumber;
        }

        // create string appending function
        
        public string getConsoleString()
        {
            return consoleString;
        }

        public string setConsoleString(string appendix)
        {
            // consoleString starts as "convert ", then add the filename, so the result is "convert 000001.jpg ". 
            // That way consoleString ends up with the important part (adding the filenames to the string) done. 
            consoleString = String.Concat(consoleString, " ", appendix);
            
            // 
            return consoleString;
        }

        public string getOutputFilename()
        {
            return outputFilename;
        }

        public string setOutputFilename(string currentStartNumber)
        {
            // This function is moddelled from setConsoleString, same functionality. 
            // The consoleString is now looking like "convert 000001.jpg 000002.jpg ... 123456.jpg -average ", 
            // the output filename is "average(currentStartNumber).jpg, where (currentStartNumber) is a 6 digit number.
            string outputString = String.Concat("average", currentStartNumber, ".jpg");

            return outputString;
            // tested 8 may 2017: Works
        }

        static void Main(string[] args)
        {
            Program p = new Program(); // to get access to the functions above
            StringBuilder sb = new StringBuilder(); // to get access to sb.append("");

            // Input number of pictures to average together
            string str = args[0];
            int avgimg = 0;
            avgimg = Convert.ToInt32(str);

            string currentdir = Directory.GetCurrentDirectory();
            
            
            
            /* Output console command: 
             * Context: Image_N is starting image, 
             * The convert command should then average Image_N+1,+2,+3...+avgnr
             * Then Image_N+1,+2+3...
             * Then image_N+2,+3+4...
             * and so on, until the end of images. 
             * 
             * 
             * convert syntax:  convert NAME.jpg NAME2.jpg NAME3.jpg -average OUTPUT
             * 
             */

            // Create list of all the jpgs in current folder

            int fCountall = Directory.GetFiles(currentdir, "*", SearchOption.TopDirectoryOnly).Length; // count number of files in current dir
            //int fCount = fCountall - (avgimg +2); // ffmpeg, batfile, (TODO: How many shuld I remove?)
            int fCount = 5; // as test

            int howFar = 1;

            
            //Console.WriteLine(convertString);
            string padding = "000000"; // padding for the integers so they fit the numbering scheme.
                                       //Console.Write(fCount.ToString(padding) + ".jpg"); // gives 000010.jpg with fCount = 10. 

            
            for (int counterOfFiles=1; counterOfFiles <= fCount; counterOfFiles++)
            {
                p.SetNumOfFilesNumber(howFar);

                int num = p.GetNumOfFilesNumber();

                // doing things with all files in the directory
                //
                // The files are named 000001.jpg, 000002.jpg and so on.
                // 
                //

                
                //int startnumber = p.GetNumOfFilesNumber();
                

                for (int counterOfAvgImg=1; counterOfAvgImg <= avgimg; counterOfAvgImg++)
                {
                    // This will run the amount of times specified by the integer set by the command line.
                    // This function should write "000001.jpg 000002.jpg ... avgnr.jpg" as written above. 

                    // If I get the startnumber in here, I will do the same thing every time the loop runs. 
                    // I want startnumber as the lowest number in the string, then adding on to the string from the startnumber. 
                    // 
                    
                    string numstring = num.ToString(padding); // number with padding
                    
                    string consolestring = p.getConsoleString();


                    p.howFarStr = num.ToString(padding) + ".jpg";
                    p.setConsoleString(p.howFarStr);
                    num = num + 1;
                    
                    Console.WriteLine(consolestring);
                    

                    if(counterOfAvgImg == avgimg)
                    {
                        //setConsoleString(NEW STRING);
                    //Console.WriteLine("Counter == avg img: " + comandLine);
                    //Console.WriteLine("Counter == avg img: " + consolestring);
                        //Console.WriteLine(p.setOutputFilename("123456")); Works 8 may 2017 15.00 Mads Time
                    }
                    
                }
                    howFar = howFar + 1;
                

            }
         
        }
    }
}
