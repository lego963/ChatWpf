using System;
using System.Collections.Generic;
using System.Text;
using ChatWpf.Core.IoC.Interfaces;
using Dna;

namespace ChatWpf.Core.DI
{
    public static class CoreDI
    {
        /// <summary>
        /// A shortcut to access the <see cref="IFileManager"/>
        /// </summary>
        public static IFileManager FileManager => Framework.Service<IFileManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ITaskManager"/>
        /// </summary>
        public static ITaskManager TaskManager => Framework.Service<ITaskManager>();
    }
}
