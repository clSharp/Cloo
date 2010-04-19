#region License

/*

Copyright (c) 2009 - 2010 Fatjon Sakiqi

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

*/

#endregion

namespace Cloo
{
    using System;

    public class ComputeContextProperty
    {
        #region Fields

        private readonly ComputeContextPropertyName name;
        private readonly IntPtr value;

        #endregion

        #region Properties

        /// <summary>
        /// The name of this property.
        /// </summary>
        public ComputeContextPropertyName Name
        {
            get { return name; }
        }

        /// <summary>
        /// The value of this property.
        /// </summary>
        public IntPtr Value
        {
            get { return value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new context property.
        /// </summary>
        /// <param name="name">The name of the created property.</param>
        /// <param name="value">The value of the created property.</param>
        public ComputeContextProperty(ComputeContextPropertyName name, IntPtr value)
        {
            this.name = name;
            this.value = value;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets a string representation of this ComputeContextProperty.
        /// </summary>
        public override string ToString()
        {
            return "ComputeContextProperty(" + name + ", " + value + ")";
        }

        #endregion
    }
}