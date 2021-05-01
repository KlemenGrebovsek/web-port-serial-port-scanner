# web and serial port scanner
Program for scanning web port and serial port status.

**Commands:**

[webPort] 

Description: Scans for web ports and their status.

[serialPort] 

Description: Scans for serial ports and their status.

[help] 

Description: Displays program options.

**Options**

[-f --from]  

Description: Program scans for ports that are equal or greater than this value (0 - 65535), default value is 0.

[-t --to] 

Description: Program scans for ports that are equal or less than this value (0 - 65535), default value is 65535.

[-s --status] 

Description: Program filters ports by this status. (Any, Free, InUse, Unknown), default value is "Any";


**Sample args**

1) webPort -f 100 -t 3000 -s InUse
2) serialPort -t 6
3) webPort -s Free
2) serialPort -s Any
