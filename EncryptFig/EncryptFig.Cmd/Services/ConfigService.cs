using System;
using System.Configuration;
using System.Diagnostics;
using System.Text;

namespace EncryptFig.Cmd.Services
{
    public class ConfigService
    {
        private static readonly string[] _nodeArray = ConfigurationManager.AppSettings["Nodes"].Split(';');
        private static readonly string _webConfigFolder = ConfigurationManager.AppSettings["WebConfigFolder"];

        public string OutputConfig()
        {
            return buildMessage(_webConfigFolder, _nodeArray);
        }

        private string buildMessage(string folderPath, string[] nodes)
        {
            var myString = new StringBuilder();
            myString.AppendLine("WebConfigFolder: " + folderPath);
            myString.AppendLine("Nodes to be encrypted/decrypted:");
            var count = 1;
            foreach (var node in nodes)
            {
                myString.AppendLine(count + "). " + node);
                count++;
            }
            return myString.ToString();
        }

        public string EnCrypt()
        {
            var myProcessInfo = new System.Diagnostics.ProcessStartInfo(); //Initializes a new ProcessStartInfo of name myProcessInfo
            myProcessInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe"; //Sets the FileName property of myProcessInfo to %SystemRoot%\System32\cmd.exe where %SystemRoot% is a system variable which is expanded using Environment.ExpandEnvironmentVariables
            //myProcessInfo.Arguments = "&"; //Sets the arguments to cd..
            myProcessInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //Sets the WindowStyle of myProcessInfo which indicates the window state to use when the process is started to Hidden
            myProcessInfo.Verb = "runas"; //The process should start with elevated permissions

            myProcessInfo.RedirectStandardInput = true;
            myProcessInfo.RedirectStandardOutput = true;
            myProcessInfo.UseShellExecute = false;

            var process = Process.Start(myProcessInfo);

            if (process != null)
            {
                process.StandardInput.WriteLine("cd C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319");
                
                foreach (var node in _nodeArray)
                {
                    string commandString= "@ASPNET_REGIIS -pef " + ""+ node +"" +" "+  ""+_webConfigFolder +"";
                    process.StandardInput.WriteLine(commandString);
                }
         
                //process.StandardInput.WriteLine("yourCommand.exe arg1 arg2");

                process.StandardInput.Close(); // line added to stop process from hanging on ReadToEnd()

                var outputString = process.StandardOutput.ReadToEnd();
                return outputString;
            }
            return string.Empty;
        }

        public string DeCrypt()
        {
            var myProcessInfo = new System.Diagnostics.ProcessStartInfo(); //Initializes a new ProcessStartInfo of name myProcessInfo
            myProcessInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe"; //Sets the FileName property of myProcessInfo to %SystemRoot%\System32\cmd.exe where %SystemRoot% is a system variable which is expanded using Environment.ExpandEnvironmentVariables
            //myProcessInfo.Arguments = "&"; //Sets the arguments to cd..
            myProcessInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //Sets the WindowStyle of myProcessInfo which indicates the window state to use when the process is started to Hidden
            myProcessInfo.Verb = "runas"; //The process should start with elevated permissions

            myProcessInfo.RedirectStandardInput = true;
            myProcessInfo.RedirectStandardOutput = true;
            myProcessInfo.UseShellExecute = false;
            
            var process = Process.Start(myProcessInfo);

            if (process != null)
            {
                process.StandardInput.WriteLine("cd C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319");

                foreach (var node in _nodeArray)
                {
                    string commandString = "@ASPNET_REGIIS -pdf " + "" + node + "" + " " + "" + _webConfigFolder + "";
                    process.StandardInput.WriteLine(commandString);
                    
                }

                //process.StandardInput.WriteLine("yourCommand.exe arg1 arg2");

                process.StandardInput.Close(); // line added to stop process from hanging on ReadToEnd()

                var outputString = process.StandardOutput.ReadToEnd();
                return outputString;
            }
            return string.Empty;
        }
    }
}