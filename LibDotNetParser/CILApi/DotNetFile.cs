﻿using System.Collections.Generic;

namespace LibDotNetParser.CILApi
{
    public class DotNetFile
    {
        PEParaser peFile;
        public PEParaser Backend
        {
            get { return peFile; }
        }
        public List<DotNetType> Types
        {
            get
            {
                List<DotNetType> t = new List<DotNetType>();
                int i = 0;
                foreach (var item in peFile.Tabels.TypeDefTabel)
                {
                    t.Add(new DotNetType(this, item, i + 1));
                    i++;
                }

                return t;
            }
        }
        /// <summary>
        /// Entry point of EXE/DLL. Will be null if EXE/DLL does not have entry point.
        /// </summary>
        public DotNetMethod EntryPoint
        {
            get
            {
                DotNetMethod m = null;
                foreach (var item in Types)
                {
                    foreach (var m2 in item.Methods)
                    {
                        if (m2.IsStatic && m2.Name == "Main")
                        {
                            m = m2;
                            break;
                        }
                    }
                }

                return m;
            }
        }
        public DotNetFile(string Path)
        {
            peFile = new PEParaser(Path);
        }

        public DotNetFile(byte[] file)
        {
            peFile = new PEParaser(file);
        }
    }
}
