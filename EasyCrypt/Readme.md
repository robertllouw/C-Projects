# EasyCrypt

This is an easy and dirty way for AES encryption to show some concepts

## Notes


## Commandline use
Options:
  -i | --input <value>    Input for Encryption/Decryption
  -e | --encrypt <value>  Encrypt Information
  -d | --decrypt <value>  Decrypt Information
  -f | --file <value>     Mark that file is being Encrypted/Decrypted
  -o | --output <value>   Output to this FullPath, Default is output to screen
  -? | -h | --help        Show help information
  
## Example use
EasyCrypt.exe -i "hello world" -e : Encrypt the string "hello world" and display on screen
EasyCrypt.exe -i "hello world" -e -o "c:\temp\filename.txt" : Encrypt the string "hello world" and output to file

EasyCrypt.exe -i "XXXXX" -d : Decrypt the encrypted string "XXXXXX"
EasyCrypt.exe -i "XXXXX" -d -o "c:\temp\filename.txt" : Decrypt the encrypted string "XXXXXX" and output to file

EasyCrypt.exe -i "c:\temp\encryptMe" -e  : Encrypt the file on screen
EasyCrypt.exe -i "c:\temp\encryptMe" -e -f -o "c:\temp\encrypted" : Encrypt the file and output to file

EasyCrypt.exe -i "c:\temp\encryptMe" -d  : Decrypt the file and display on screen
EasyCrypt.exe -i "c:\temp\encryptMe" -d -f -o "c:\temp\encrypted" : Decrypt the file and output to file