#region License

/*

Copyright (c) 2009 Fatjon Sakiqi

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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class ComputeContextPropertyList
    {
        #region Fields
        
        private ComputePlatform platform;
        private IList<ComputeContextProperty> properties;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a collection of all the properties in this list.
        /// </summary>
        public ReadOnlyCollection<ComputeContextProperty> Content
        {
            get { return new ReadOnlyCollection<ComputeContextProperty>( properties ); }
        }

        /// <summary>
        /// Gets the platform of this property list or null if none was specified.
        /// </summary>
        public ComputePlatform Platform
        {
            get { return platform; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new property list.
        /// </summary>
        public ComputeContextPropertyList()
        {
            properties = new List<ComputeContextProperty>();
        }

        /// <summary>
        /// Creates a new property list.
        /// </summary>
        /// <param name="platform">A platform property for this list. Can be null.</param>
        public ComputeContextPropertyList( ComputePlatform platform )
        {            
            this.platform = platform;

            properties = new List<ComputeContextProperty>();
            
            if( platform != null )
                properties.Add( new ComputeContextProperty( ComputeContextPropertyName.Platform, platform.Handle ) );
        }

        #endregion

        #region Internal methods

        internal IntPtr[] ToIntPtrArray()
        {
            IntPtr[] result = new IntPtr[ 2 * properties.Count + 1 ];
            for( int i = 0; i < properties.Count; i++ )
                result[ 2 * i ] = new IntPtr( ( int )properties[ i ].Name );
            result[ result.Length - 1 ] = IntPtr.Zero;
            return result;
        }

        #endregion
    }
}