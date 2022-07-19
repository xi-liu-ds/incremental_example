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
    public class ResourceLeak
    {
        /// <summary>
        /// Returns a StreamWriter resource unless returns null with exception, no leaks expected.
        /// </summary>
        public StreamWriter AllocateStreamWriter() 
        {
            try
            {
                FileStream fs = File.Create("everwhat.txt");
                return new StreamWriter(fs);
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}