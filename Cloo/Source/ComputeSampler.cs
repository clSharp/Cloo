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

using System;
using OpenTK.Compute.CL10;

namespace Cloo
{
    public class ComputeSampler: ComputeResource
    {
        #region Fields
        
        private readonly ComputeContext context;
        private readonly AddressingMode addressingMode;
        private readonly FilterMode filterMode;
        private readonly bool normalizedCoords;

        #endregion

        #region Properties

        /// <summary>
        /// Return the context specified when the sampler is created.
        /// </summary>
        public ComputeContext Context
        {
            get
            {
                return context;
            }
        }

        /// <summary>
        /// Return the value specified by addressing argument when the sampler is created.
        /// </summary>
        public AddressingMode AddressingMode
        {
            get
            {
                return addressingMode;
            }
        }

        /// <summary>
        /// Return the value specified by filtering argument when the sampler is created.
        /// </summary>
        public FilterMode FilterMode
        {
            get
            {
                return filterMode;
            }
        }

        /// <summary>
        /// Return the value specified by normalizedCoords when the sampler is created.
        /// </summary>
        public bool NormalizedCoords
        {
            get
            {
                return normalizedCoords;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a sampler object.
        /// </summary>
        /// <param name="context">A valid OpenCL context.</param>
        /// <param name="normalizedCoords">Determines if the image coordinates specified are normalized or not.</param>
        /// <param name="addressing">Specifies how out-of-range image coordinates are handled when reading from an image.</param>
        /// <param name="filtering">Specifies the Type of filter that must be applied when reading an image.</param>
        public ComputeSampler( ComputeContext context, bool normalizedCoords, AddressingMode addressing, FilterMode filtering )
        {
            int error = ( int )ErrorCode.Success;
            Handle = CL.CreateSampler( context.Handle, normalizedCoords, addressing, filtering, out error );
            ComputeException.ThrowIfError( error );
            this.addressingMode = addressing;
            this.context = context;
            this.filterMode = filtering;
            this.normalizedCoords = normalizedCoords;
        }

        #endregion

        #region Protected methods

        protected override void Dispose( bool manual )
        {
            if( manual )
            {
                //free managed resources
            }

            // free native resources
            if( Handle != IntPtr.Zero )
            {
                CL.ReleaseSampler( Handle );
                Handle = IntPtr.Zero;
            }
        }

        #endregion
    }
}