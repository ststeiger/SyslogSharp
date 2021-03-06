/*
 * Copyright 2010 Andrew Draut
 * 
 * This file is part of Syslog Sharp.
 * 
 * Syslog Sharp is free software: you can redistribute it and/or modify it under the terms of the GNU General 
 * Public License as published by the Free Software Foundation, either version 3 of the License, or (at 
 * your option) any later version.
 * 
 * Syslog Sharp is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even 
 * the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with Syslog Sharp. If not, see http://www.gnu.org/licenses/.
*/

using System;
using System.Collections.Generic;
using System.Text;
using Syslog.Server;

namespace Syslog.Test
{
    class Program
    {
        static Syslog.BarracudaWebFilter.Parser p = new Syslog.BarracudaWebFilter.Parser();

        static void Main(string[] args)
        {
            Listener l = Listener.CreateInstance(System.Configuration.ConfigurationManager.AppSettings["listenIPAddress"],
                    Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["listenPort"]),
                    Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["bufferFlushFrequency"]));

            Syslog.Server.Console.Server consoleServer = new Syslog.Server.Console.Server();

            if (l.Start())
            {
                Listener.MessageReceived += new Listener.MessageReceivedEventHandler(l_MessageReceived);
                Console.WriteLine("Listener Started.  Press any key to stop listener");

                consoleServer.Start();
                Console.WriteLine("Console server started.");
            }

            Console.ReadLine();
            l.Stop();
            consoleServer.Stop();
        }

        static void l_MessageReceived(MessageReceivedEventArgs e)
        {
            //Console.WriteLine(e.SyslogMessage.ToString());
            Console.WriteLine((((IParser)p).Parse(e.SyslogMessage)));
        }
    }
}
