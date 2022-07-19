// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Examples
{
    public class Program
    {
        private StreamReader SRGlobal;  
        // Instantiate a StreamWriter instance.
        StreamWriter SWGlobal = new StreamWriter("everwhat.txt");
        
        /// <summary>
        /// Constructor resource usage example, no leaks expected.
        /// </summary>
        public Program(string filename){
            SRGlobal = new StreamReader(filename);  
        }

        /// <summary>
        /// An example with no null dereference expected.
        /// </summary>
        public string NullDeReferenceOK(){
            return "abc";
        }
        
        /// <summary>
        /// Intraprocedural resource usage example, no leaks expected.
        /// </summary>
        public void ResourceLeakIntraproceduralOK(){
            string data;
            StreamReader sr = new StreamReader("whatever.txt");            
            data = sr.ReadToEnd();
            sr.Close();
            Console.WriteLine(data);
        }

        /// <summary>
        /// Intraprocedural resource usage example, leaks expected.
        /// </summary>
        public void ResourceLeakIntraproceduralBad(){
            StreamWriter sw = new StreamWriter("everwhat.txt");
            sw.WriteLine("Guru99 - ASP.Net");
            // FIXME: should close the stream intraprocedurally by calling sw.Close()
        }

        /// <summary>
        /// Interprocedural resource usage example, leaks expected.
        /// </summary>
         public void ResourceLeakInterproceduralBad(){
            ResourceLeak rl = new ResourceLeak();
            StreamWriter stream = rl.AllocateStreamWriter();
            if (stream == null)
                return;

            try 
            {
                stream.WriteLine(12);
            } 
            finally 
            {
                // FIXME: should close the stream by calling stream.Close().
            }
        }

        /// <summary>
        /// Interprocedural resource usage example, no leaks expected.
        /// </summary>
        public void ResourceLeakInterproceduralOK(){
            ResourceLeak rl = new ResourceLeak();
            StreamWriter stream = rl.AllocateStreamWriter();
            if (stream == null)
                return;

            try 
            {
                stream.WriteLine(12);
            } 
            finally 
            {
                stream.Close();
            }
        }

        /// <summary>
        /// Interprocedural close resource function.
        /// </summary>
        public void CleanUp(){
            SRGlobal.Close();
            SWGlobal.Close();
            Console.WriteLine("Close is called");
        }

    }

    public class MainClass {
        public static void Main(string[] args)
        {
            Program p = new Program("whatever.txt");
            NullDeref nd = new NullDeref();
            // FIXME: should close the global streams by calling p.Cleanup()
            // Null dereference error report expected.
            nd.NullDeReferenceBad().GetHashCode();
            // No null dereference error report expected.
            p.NullDeReferenceOK().GetHashCode();
        }
    }
}
