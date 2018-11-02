using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Extensions.CommandLineUtils;

namespace EasyCrypt
{
    class Program
    {

        public static void Main(string[] args)
        {

        
            // COMMAND LINE OPTIONS
            CommandLineApplication cmd = new CommandLineApplication();
            CommandOption argInput = cmd.Option("-i | --input <value>", "Input for Encryption/Decryption", CommandOptionType.SingleValue);
            CommandOption argEncrypt = cmd.Option("-e | --encrypt <value>", "Encrypt Information", CommandOptionType.NoValue);
            CommandOption argDecrypt = cmd.Option("-d | --decrypt <value>", "Decrypt Information", CommandOptionType.NoValue);
            CommandOption argFile = cmd.Option("-f | --file <value>", "Mark that file is being Encrypted/Decrypted", CommandOptionType.NoValue);
            CommandOption argOutput = cmd.Option("-o | --output <value>", "Output to this FullPath, Default is output to screen", CommandOptionType.SingleValue);


            cmd.OnExecute(() =>
            {

            try
            {
                string inputData = argInput.Value();
                if (argEncrypt.HasValue() && !argOutput.HasValue() && !argFile.HasValue())
                {
                    string result = EasyCrypt.EncryptString(inputData);
                    Console.WriteLine(result);
                }
                if (argDecrypt.HasValue() && !argOutput.HasValue() && !argFile.HasValue())
                {
                    string result = EasyCrypt.DecryptString(inputData);
                    Console.WriteLine(result);
                }

                //Output to File, String Encryption
                if (argEncrypt.HasValue() && argOutput.HasValue() && !argFile.HasValue())
                {
                    string result = EasyCrypt.EncryptString(inputData);
                    File.WriteAllText(argOutput.Value(), result);
                }
                if (argDecrypt.HasValue() && argOutput.HasValue() && !argFile.HasValue())
                {
                    string result = EasyCrypt.DecryptString(inputData);
                    File.WriteAllText(argOutput.Value(), result);
                    Console.WriteLine(result);
                }

                //Output to Screen, File Encryption
                if (argEncrypt.HasValue() && !argOutput.HasValue() && argFile.HasValue())
                {
                    byte[] fileInformation = File.ReadAllBytes(inputData);
                    string Result = EasyCrypt.EncryptString(Convert.ToBase64String(fileInformation));
                    Console.WriteLine(Result);
                }
                if (argDecrypt.HasValue() && !argOutput.HasValue() && argFile.HasValue())
                {
                    byte[] fileInformation = File.ReadAllBytes(inputData);
                    string Result = EasyCrypt.DecryptString(Convert.ToBase64String(fileInformation));
                    Console.WriteLine(Result);
                }

                //Output to File, File Encryption
                if (argEncrypt.HasValue() && argOutput.HasValue() && argFile.HasValue())
                {
                        EasyCrypt.sendToFile(inputData, argOutput.Value());

                }
                if (argDecrypt.HasValue() && argOutput.HasValue() && argFile.HasValue())
                {
                        EasyCrypt.getFromFile(inputData, argOutput.Value());
                }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
                return 0;
            });

            cmd.HelpOption("-? | -h | --help");
            cmd.Execute(args);       

        }
    }
}
