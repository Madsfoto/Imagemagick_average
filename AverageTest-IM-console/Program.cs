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
            consoleString = "magick";
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
            int avgimg = Convert.ToInt32(str); // I am currently unable to take an int directly from the argument list, hence the conversion here


            string currentdir = Directory.GetCurrentDirectory();

            string skipStr = args[1]; // the string for how many images to skip when averaging
                                   // The idea is that when you record a video at 30 fps, the resulting averaging "stream" of images, when combined together again
                                   // is still played at 30 fps. 
                                   // Then if you want to do timelapse images, you can either record at a lower fps (for instance 10 fps) or ignore some of the resulting averaged images
                                   // Example 1: Record at 10 fps, average, assemble at 30fps and the result is 1/3rd the time of the original
                                   // Example 2: Record at 30 fps, average only every n frames, assemble at 30 fps and the result is 1/n'th time of the original. 
                                   // As of this commit the idea is untested.

            // Future work, aka TODO: 
            // 1: Error handling. The program currently breaks horribly when not given the expected parameters
            // 2: Averaging a different amount than 1/n, where n is an integer. 

            int skipInt = Convert.ToInt32(skipStr);
            
            
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
            
            

            int howFar = 1;

            
            //Console.WriteLine(convertString);
            string padding = "000000"; // padding for the integers so they fit the numbering scheme.
                                       //Console.Write(fCount.ToString(padding) + ".jpg"); // gives 000010.jpg with fCount = 10. 

            
            for (int counterOfFiles=1; counterOfFiles <= fCountAll; counterOfFiles++)
            {
                counterOfFiles = counterOfFiles + skipInt;
                p.SetNumOfFilesNumber(howFar); //Set num of files to 1, then to 2 after avgimg has been run through.
                

                int num = p.GetNumOfFilesNumber();

                // doing things with all files in the directory
                //
                // The files are named 000001.jpg, 000002.jpg and so on.
                // 
                
                

                for (int counterOfAvgImg=1; counterOfAvgImg <= avgimg; counterOfAvgImg++)
                {
                    // This will run the amount of times specified by the integer set by the command line.
                    // This function should write "000001.jpg 000002.jpg ... avgnr.jpg" as written above. 

                    // If I get the startnumber in here, I will do the same thing every time the loop runs. 
                    // I want startnumber as the lowest number in the string, then adding on to the string from the startnumber. 
                    // 
                    
                    string numstring = num.ToString(padding)+".jpg"; // number with padding. Is incremented properly when running through the inner and outer loop.
                    p.setConsoleString(numstring);
                    
                    
                   
                    num = num + 1;
                    
                    
                    

                    if(counterOfAvgImg == avgimg)
                    {
                        //setConsoleString(NEW STRING);
                        // p.getConsoleString() gives the correct "convert 000001.jpg 000002.jpg 000003.jpg ... avgimg.jpg", 
                        // 

                        // The problem of incrementing with skipInt in howFar is that the output filename is incremented as well. 
                        // The obvious solution is to create a different counter that is incremented independently of howFar.
                        // Not sure if it is the right solution though.
                        // 
                        Console.WriteLine(p.getConsoleString() + p.setOutputFilenameString(howFar.ToString(padding)));

                    }

                }
                p.clearConsoleString();
                    howFar = howFar + 1; // is incremented after a run through avgimg.
                

            }
         
        }
    }
}
