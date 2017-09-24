using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// Future work, aka TODO: 
// 1: Error handling. The program currently breaks horribly when not given the expected parameters


namespace AverageTest_IM_console
{

    

    class Program
    {

        // External variables 
        // make external integer as starting point for the second loop, that is outside the loop. 

        public int startNumber = 0;
        public string consoleString = "magick"; // The convert string that should be output of this application. 
        public string howFarStr = "";
        public string outputFilename = "";
        public int numberOfRounds = 1;
        public string finalstring ="";
        public int paddingInt = 000000;
        public string autoAdj = "";
        public int avgImgPublic = 0;
        public int howFarPublic = 0;

        // Get/set of the outer loop. The input newnr is actually the int howFar, incremented with the second argument
        // In reality it is the starting number for the loops
        // the StartNumber functions deals with the first number in the averaging sequence, it is the output filename of the finished file,
        // and the output name of the bat files for consistency. 

        public int setStartNumber(int newnr)
        {
            startNumber = newnr;
            return newnr;
        }

        public int getStartNumber()
        {
            return startNumber;
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

        // The PFinalString functions deals with the output into the bat files. It is 'final' in the sense that,
        // it will not be changed more when it is used as output.

        public string setPFinalstring(string str)
        {
            finalstring = finalstring + str;

            return finalstring;
        }
        public string getPFinalString()
        {
            return finalstring;
        }

        public void clearPFinalString()
        {
            finalstring = "";
        }

        public void writePFinalstring()
        {
            Console.WriteLine(finalstring); 
        }

        public string setAutoAdj(string autoAdjStr)
        {
            autoAdj = autoAdjStr;
            return autoAdj;
        }

        public void setAvgImg(int avgimg)
        {
            avgImgPublic = avgimg;
        }
        public int getAvgImg()
        {
            return avgImgPublic;
        }

        public void setHowFar(int howFar)
        {
            howFarPublic = howFar;
        }


        // create the function to create a bat file with the 'starting file name that is averaged'(aka 000001).bat
        // Contents: output string like normal
        public void createBatchfile()
        {
            // string with the current output
            string currentStr = finalstring;


            // create file with the correct name
            // getStartNumber() returns the correct number.
            // Filename
            int fileNameInt = getStartNumber();
            string fileName = String.Format("{0:000000}", fileNameInt);
            
            TextWriter tw = new StreamWriter(fileName+".bat"); 

            // Put the currentStr into the file
            // body
            tw.WriteLine(currentStr); // Write the normal currentStr to the file

            
                tw.WriteLine(autoAdj); // This will write the auto adjust command to the file, making sure that the 'averaged' files will have more contrast
            

            // close the file
            tw.Close();

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
            // string outputString = String.Concat(" -evaluate-sequence mean ", "average" + currentStartNumber, ".jpg"); // org
            string outputString = String.Concat(" -evaluate-sequence mean ", "average" + getAvgImg() + "_" + currentStartNumber, ".jpg");

            return outputString;
            // tested 8 may 2017: Works
        }

        static void Main(string[] args)
        {
            Program p = new Program(); // to get access to the functions above


            // Input number of pictures to average together
            string str = args[0];
            int avgimg = Convert.ToInt32(str); // I am currently unable to take an int directly from the argument list, hence the conversion here
            p.setAvgImg(avgimg);

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

            int fCountAll = Directory.GetFiles(currentdir, "*.jpg", SearchOption.TopDirectoryOnly).Length; // count number of files in current dir
            
            

            int howFar = 1;
            

            
            //Console.WriteLine(convertString);
            string padding = "000000"; // padding for the integers so they fit the numbering scheme.
                                       

            
            for (int counterOfFiles=1; counterOfFiles <= fCountAll; counterOfFiles++)
            {
                
                p.setStartNumber(howFar); //Set num of files to 1, then to 2 after avgimg has been run through.
                

                int num = p.getStartNumber();

                // doing things with all files in the directory
                //
                // The files are named 000001.jpg, 000002.jpg and so on.
                // 

                p.setStartNumber(counterOfFiles);
                

                for (int counterOfAvgImg=1; counterOfAvgImg <= avgimg+1; counterOfAvgImg++)
                {
                    // This will run the amount of times specified by the integer set by the command line.
                    // This function should write "000001.jpg 000002.jpg ... avgnr.jpg" as written above. 

                    // If I get the startnumber in here, I will do the same thing every time the loop runs. 
                    // I want startnumber as the lowest number in the string, then adding on to the string from the startnumber. 
                    // 
                    
                    string numstring = num.ToString(padding)+".jpg"; // number with padding. Is incremented properly when running through the inner and outer loop.
                    p.setConsoleString(numstring);

                    
                    num = num + 1;
                    p.setAutoAdj("magick " + "average" + p.getAvgImg() + "_" + p.numberOfRounds.ToString(padding) + ".jpg" + " -auto-level "+ "average" + p.getAvgImg() + "_" + p.numberOfRounds.ToString(padding) + ".jpg");


                    if (counterOfAvgImg == avgimg)
                    {
                        //setConsoleString(NEW STRING);
                        // p.getConsoleString() gives the correct "magick 000001.jpg 000002.jpg 000003.jpg ... avgimg.jpg", 
                        // 

                        // The problem of incrementing with skipInt in howFar is that the output filename is incremented as well. 
                        // The obvious solution is to create a different counter that is incremented independently of howFar.
                        // Not sure if it is the right solution though.
                        // 

                        // String combination into new string, as preperation for making each comand a single .bat file.
                        // The idea is instead of having one bat file with all the items to average and going through them sequentially and serially,
                        // the output is one bat file per average.
                        // Combined with a "watcher" program, in the current idea/implementation, this will allow for parallel execution,
                        // which in turn will make the averaging process much faster

                        
                        String finalstring = "";
                        finalstring = p.getConsoleString() + p.setOutputFilenameString(p.numberOfRounds.ToString(padding));
                        p.setPFinalstring(finalstring);
                        //p.writePFinalstring();

                        
                        p.createBatchfile();
                        
                        p.clearPFinalString();

                        //Console.WriteLine(p.getConsoleString() + p.setOutputFilenameString(p.numberOfRounds.ToString(padding))); // Writes a line with the current consolestring,
                        // and the rest of the requred things.


                    }

                }
                
                p.clearConsoleString(); //we need to reset the consoleString after it has been written to have meaningful results
                    howFar = howFar + 1; // The starting point for the averaging sequence. 
                p.setHowFar(howFar);
                
                
                p.numberOfRounds = p.numberOfRounds + 1; // number of rounds is incremented by 1, as it is independent of the skipping. 
                // 

            }
         
        }

        
    }
}
