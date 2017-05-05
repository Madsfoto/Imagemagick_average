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

        // make external integer as starting point for the second loop, that is outside the loop. Get set something?
        public int numOfFilesNumber = 0;

            public int SetNumOfFilesNumber(int newnr)
            {

            numOfFilesNumber = newnr;
            return newnr;
            }

        public int GetNumOfFilesNumber()
        {
            return numOfFilesNumber;
        }


        static void Main(string[] args)
        {
            Program SNT = new Program();
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
             * convert syntax:  coonvert NAME.jpg NAME2.jpg NAME3.jpg -average OUTPUT
             * 
             */

            // Create list of all the jpgs in current folder

            int fCountall = Directory.GetFiles(currentdir, "*", SearchOption.TopDirectoryOnly).Length; // count number of files in current dir
            //int fCount = fCountall - (avgimg +2); // ffmpeg, batfile, (TODO: How many shuld I remove?)
            int fCount = 5; // as test

            int howFar = 1;

            string convertString = "convert " ; // The convert string that should be output of this application. 

            //Console.WriteLine(convertString);
            string padding = "000000"; // padding for the integers so they fit the numbering scheme.
                                       //Console.Write(fCount.ToString(padding) + ".jpg"); // gives 000010.jpg with fCount = 10. 

            
            for (int counterOfFiles=1; counterOfFiles <= fCount; counterOfFiles++)
            {
                SNT.SetNumOfFilesNumber(counterOfFiles);
                int num = SNT.GetNumOfFilesNumber();

                // doing things with all files in the directory
                //
                // The files are named 000001.jpg, 000002.jpg and so on.

                SNT.SetNumOfFilesNumber(howFar);
                int startnumber = SNT.GetNumOfFilesNumber(); 

                for (int counterOfAvgImg=1; counterOfAvgImg <= avgimg; counterOfAvgImg++)
                {
                    // This will run the amount of times specified by the integer set by the command line.
                    // This function should write "000001.jpg 000002.jpg ... avgnr.jpg" as written above. 

                    string howFarStr = ""; // string to write to

                    // If I get the startnumber in here, I will do the same thing every time the loop runs. 
                    // I want startnumber as the lowest number in the string, then adding on to the string from the startnumber. 
                    // 


                    string numstring = num.ToString(padding); // number with padding
                    string comandLine = "";

                    
                    howFarStr = " " + num.ToString(padding) + ".jpg";
                    num = num + 1;
                    
                    

                    if(counterOfAvgImg == avgimg)
                    {
                        
                    Console.WriteLine("Counter == avg img: " + comandLine);
                    }
                    
                }
                    howFar = howFar + 1;
                

            }
         
        }
    }
}
