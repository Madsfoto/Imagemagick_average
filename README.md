# ImageMagick_average_program
Average a number of images via command line argument, going through all files in the current folder (Warning: Very basic)
## Syntax
**AverageTest-IM-console.exe Number_Of_Images_To_Average Number_of_images_to_advance **

_Number_of_images_To_advance_ should be larger an integer larger than 0, as the increment is this integer, so if 0 is the argument, no incrementation is done. 
The output will be a series of bat files, curently named 1.bat, 2.bat, 3.bat etc with the imagemagick comand inside them. The files can be executed with the comand 
_for %%a in ("*.bat") do "%%a"_  . (currently, as of june 19 2017 untested).
The plan is to create another program to execute the bat files in parallel to decrease execution time.


This program is written to solve a very specific problem in a very specific manner, it has sharp corners (for instance, will crash horribly if no arguments are given) and not very helpful. 

Assumptions: All input files have the numbering scheme 000001.jpg, 000002.jpg, 000003.jpg and so forth. For ffmpeg it is assumed that the files are the same pixel size (as in 1920x1080). 


To get the full use, install _imagemagick_, name the images _%06d_ (in ffmpeg terms, meaning that there's always 6 digits and an ever increasing number at the end, run this program with output captured in a bat file ( _> conv.bat_ being the prefered way), run the captured output and see your images being converted to average images. 
ffmpeg can then be used to compress the averaged images into a movie going through your image library. 

To rename the input files, a possiblility is to use [Rena](https://github.com/Madsfoto/Rena).

#Improvement ideas: 
- Make it not crash when not giving right input
- Use [the real imagemagick](https://www.imagemagick.org/script/index.php) [.net](https://github.com/dlemstra/Magick.NET) for the actual averaging
