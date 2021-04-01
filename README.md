# HyperScanningEyeTracking

1. The marker is set up to open local port and send marker whenever we pressed spacebar via that port
2. The way we call a function for sending marker is different between master and client. Just putting in different position where we call a function.
3. To avoid conflict, make sure before we run this project, do the following things :

3.1. Remove EyeInfo_CLIENT.cs from a PC that will be a master
3.2. Copy EyeInfo_CLIENT.cs to a a client PC and change the file name into Eyeinfo.cs
3.3 There should not be in the same project for both EyeInfo.cs and EyeInfo_CLIENT.cs, because it will create a conflict.

See the repository for  HyperscanningEyeGaze_2exp-CLIENT 
for client side
