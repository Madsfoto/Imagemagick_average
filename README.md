# ImageMagick_average_program
Average a number of images via command line argument, going through all files in the current folder (Warning: Very basic)
## Syntax
**AverageTest-IM-console.exe Number_Of_Images_To_Average Number_of_images_To_Ignore > conv.bat**


This program is written to solve a very specific problem in a very specific manner, it has sharp corners (for instance, will crash horribly if no arguments are given) and not very helpful. 
Assumptions: All files have the numbering scheme 000001.jpg, 000002.jpg, 000005 and so forth. For ffmpeg it is assumed that the files are the same size. 


To get the full use, install _imagemagick_, name the images _%06d_ (in ffmpeg terms, meaning that there's always 6 digits and an ever increasing number at the end, run this program with output captured in a bat file ( _> conv.bat_ being the prefered way), run the captured output and see your images being converted to average images. 
ffmpeg can then be used to compress the averaged images into a movie going through your image library. 

#Improvement ideas: 
- Make it not crash when not giving right input
- Make the output tell how many files are averaged together (and change the ffmpeg command)
- Use [the real imagemagick](https://www.imagemagick.org/script/index.php) [.net](https://github.com/dlemstra/Magick.NET) for the actual averaging
