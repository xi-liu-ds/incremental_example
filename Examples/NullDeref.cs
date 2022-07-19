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
    public class NullDeref
    {
        /// <summary>
        /// An example with null dereference error expected.
        /// </summary>
        public string NullDeReferenceBad(){
            return null;
        }
    }
}