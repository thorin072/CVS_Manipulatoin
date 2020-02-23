# CVS for the KUKA manipulator model
This SOFTWARE solves the problem of planning smooth movement for the KUKA manipulator. To solve the problem of motion planning, a computer vision model is used, which receives an input image containing the desired motion paths, and after processing, the system issues an xls file with coordinates for the movement of the manipulator.
## Basic function
+ Realizes:
  + Selection of control points of objects in the image using the Harris detector method.
  + Solves the classification problem for each individual object, i.e. a set of control points relate only to one image object.
  + Bypassing the control points of the object along a specific route.
  + From the resulting route, the desired trajectory for moving along a linear section, or moving along a curved section, is plotted. Smooth motion is defined by the trapezoidal law of motion.
  + An xls file containing coordinates for the movement of the manipulator's working body will be submitted to the output.
## Interfaces
The MVC pattern is implemented.
The software model consists of:
  + Imageproccesingservice - is an interface for processing images using CVS methods. This module solves the problem of finding control points using the Harris detector and obtaining an array of control points.
  + IStateStorageService - is an interface for storing intermediate processing results.
  + IClusterServise - provides an interface for processing the received control points and classifying them for each image object using the clustering method.
  + IMessageServise-provides an interface for error handling
  + Igraphservice - is an interface for solving the problem of traversing control points in the desired sequence. Implements a method for creating an adjacency matrix for control points, a method for creating a priority for traversing an object by control points. 
  + IInterpritateServise - is an interface for basic commands for the manipulator, such as raising and lowering the pen at the beginning and end points of the manipulator, calculating the path of interleaving on Bezier curves.
  + IExcelServise - is an interface for displaying the final coordinates of the manipulator's position in an Excel file.
  + ISplineServise - is an interface for setting Bezier (2nd) order curves and setting the trapezoidal law of motion .
## Example of work 
<a href="http://www.youtube.com/watch?feature=player_embedded&v=YYzeGKxgiII" target="_blank"><img src="http://img.youtube.com/vi/YYzeGKxgiII/0.jpg" 
alt="Модель использующая выходной xls файл данного ПО:" width="240" height="180" border="1"/></a>
