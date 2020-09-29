# web and serial port scanner
Simple console program for scanning web port and serial port status.

**Commands:**

[webPort] 

Description: Scans web ports and their status.

[serialPort] 

Description: Scans serial ports and their status.

[help] 

Description: Displays program options.

**Command options**

[-f --from]  

Description: Program scans ports that are equal or greater than this value (0 - 65535), default value is 0.

[-t --to] 

Description: Program scans ports that are equal or less than this value (0 - 65535), default value is 65535.

[-s --status] 

Description: Finds for ports that have that status. (any, free, in_use), default value is "in_use";


**Sample args**

1) webPort -f 100 -t 3000 -s in_use
2) serialPort -t 6
3) webPort -s free
2) serialPort -s any
