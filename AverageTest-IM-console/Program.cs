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
        public string consoleString = "magick"; // The convert string that should be output of this application. 
        public string howFarStr = "";
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
            consoleString = String.Concat(consoleString, " ", appendix); // Original string SPACE AppendedString
            
            // 
            return consoleString;
        }

        public void clearConsoleString()
        {
            consoleString = "convert";
        }

        public string getOutputFilename()
        {
            return outputFilename;
        }

        public string setOutputFilenameString(string currentStartNumber)
        {
            // This function is moddelled from setConsoleString, same functionality. 
            // The consoleString is now looking like "convert 000001.jpg 000002.jpg ... 123456.jpg -average ", 
            // the output filename is "average(currentStartNumber).jpg, where (currentStartNumber) is a 6 digit number.
            string outputString = String.Concat(" -evaluate_sequence mean ", "average" + currentStartNumber, ".jpg");

            return outputString;
            // tested 8 may 2017: Works
        }

        static void Main(string[] args)
        {
            Program p = new Program(); // to get access to the functions above
            

            // Input number of pictures to average together
            string str = args[0];
            int avgimg = 0;
            avgimg = Convert.ToInt32(str); // I am currently unable to take an int directly from thr argument list, hence the conversion here

            string currentdir = Directory.GetCurrentDirectory();


            string ignorefile = args[1]; // How many files to ignore at the end, the int ignoreFiles (after being set) is used as a 'max number of images - ignoreFiles'/
            // limit, but imagemagick errors when you feed it files that does not exist, meaning that the variable itself is useless.
            int ignoreFiles = 0;
            ignoreFiles = Convert.ToInt32(ignorefile);
            
            
            
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

            int fCountAll = Directory.GetFiles(currentdir, "*", SearchOption.TopDirectoryOnly).Length; // count number of files in current dir
            int fCount = fCountAll - (avgimg + ignoreFiles); // ffmpeg, batfile, program itself
            //int fCount = 5; // as test

            int howFar = 1;

            
            //Console.WriteLine(convertString);
            string padding = "000000"; // padding for the integers so they fit the numbering scheme.
                                       //Console.Write(fCount.ToString(padding) + ".jpg"); // gives 000010.jpg with fCount = 10. 

            
            for (int counterOfFiles=1; counterOfFiles <= fCount; counterOfFiles++)
            {
                p.SetNumOfFilesNumber(howFar); //Set num of files to 1, then to 2 after avgimg has been run through.
                

                int num = p.GetNumOfFilesNumber();

                // doing things with all files in the directory
                //
                // The files are named 000001.jpg, 000002.jpg and so on.
                // 
                
                
                //int startnumber = p.GetNumOfFilesNumber();
                

                for (int counterOfAvgImg=1; counterOfAvgImg <= avgimg; counterOfAvgImg++)
                {
                    // This will run the amount of times specified by the integer set by the command line.
                    // This function should write "000001.jpg 000002.jpg ... avgnr.jpg" as written above. 

                    // If I get the startnumber in here, I will do the same thing every time the loop runs. 
                    // I want startnumber as the lowest number in the string, then adding on to the string from the startnumber. 
                    // 
                    
                    string numstring = num.ToString(padding)+".jpg"; // number with padding. Is incremented properly when running through the inner and outer loop.
                    p.setConsoleString(numstring);
                    // Which means it is in the string addition the problems are. 
                    
                   
                    num = num + 1;
                    
                    
                    

                    if(counterOfAvgImg == avgimg)
                    {
                        //setConsoleString(NEW STRING);
                        // p.getConsoleString() gives the correct "convert 000001.jpg 000002.jpg 000003.jpg ... avgimg.jpg", 
                        // 


                        //Console.WriteLine(p.getConsoleString() + p.setOutputFilenameInt(howFar));

                        Console.WriteLine(p.getConsoleString() + p.setOutputFilenameString(howFar.ToString(padding)));

                    }

                }
                p.clearConsoleString();
                    howFar = howFar + 1; // is incremented after a run through avgimg.
                

            }
         
        }
    }
}
