namespace HaloOnline.Research.Core.Imports.Structs
{
    /// <summary>
    /// The high-order portion of the descriptor.
    /// </summary>
    public struct LdtHighBits
    {
        private int _value;

        /// <summary>
        /// Max set value is 255 (11111111b)
        /// </summary>
        public int BaseMid
        {
            get
            {
                return (_value & 0xFF);
            }
            set
            {
                _value = (_value & unchecked((int)0xFFFFFF00)) | (value & 0xFF);
            }
        }

        /// <summary>
        /// Max set value is 31 (11111b)
        /// </summary>
        public int Type
        {
            get
            {
                return (_value & 0x1F00) >> 8;
            }
            set
            {
                _value = (_value & unchecked((int)0xFFFFE0FF)) | ((value & 0x1F) << 8);
            }
        }

        /// <summary>
        /// Max set value is 3 (11b)
        /// </summary>
        public int Dpl
        {
            get
            {
                return (_value & 0x6000) >> 13;
            }
            set
            {
                _value = (_value & unchecked((int)0xFFFF9FFF)) | ((value & 0x3) << 13);
            }
        }

        /// <summary>
        /// Max set value is 1 (1b)
        /// </summary>
        public int Pres
        {
            get
            {
                return (_value & 0x4000) >> 15;
            }
            set
            {
                _value = (_value & unchecked((int)0xFFFFBFFF)) | ((value & 0x1) << 15);
            }
        }

        /// <summary>
        /// Max set value is 15 (1111b)
        /// </summary>
        public int LimitHi
        {
            get
            {
                return (_value & 0xF0000) >> 16;
            }
            set
            {
                _value = (_value & unchecked((int)0xFFF0FFFF)) | ((value & 0xF) << 16);
            }
        }

        /// <summary>
        /// Max set value is 1 (1b)
        /// </summary>
        public int Sys
        {
            get
            {
                return (_value & 0x100000) >> 20;
            }
            set
            {
                _value = (_value & unchecked((int)0xFFEFFFFF)) | ((value & 0x1) << 20);
            }
        }

        /// <summary>
        /// Max set value is 1 (1b)
        /// </summary>
        public int Reserved0
        {
            get
            {
                return (_value & 0x200000) >> 21;
            }
            set
            {
                _value = (_value & unchecked((int)0xFFDFFFFF)) | ((value & 0x1) << 21);
            }
        }

        /// <summary>
        /// Max set value is 1 (1b)
        /// </summary>
        public int DefaultBig
        {
            get
            {
                return (_value & 0x400000) >> 22;
            }
            set
            {
                _value = (_value & unchecked((int)0xFFBFFFFF)) | ((value & 0x1) << 22);
            }
        }

        /// <summary>
        /// Max set value is 1 (1b)
        /// </summary>
        public int Granularity
        {
            get
            {
                return (_value & 0x800000) >> 23;
            }
            set
            {
                _value = (_value & unchecked((int)0xFF7FFFFF)) | ((value & 0x1) << 23);
            }
        }

        /// <summary>
        /// Max set value is 255 (11111111b)
        /// </summary>
        public int BaseHi
        {
            get
            {
                return (_value & unchecked((int)0xFF000000)) >> 24;
            }
            set
            {
                _value = (_value & unchecked(0xFFFFFF)) | ((value & 0xFF) << 24);
            }
        }
    }
}
