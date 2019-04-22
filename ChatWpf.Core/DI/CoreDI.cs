using ChatWpf.Core.DI.Interfaces;
using Dna;

namespace ChatWpf.Core.DI
{
    public static class CoreDi
    {
        public static IFileManager FileManager => Framework.Service<IFileManager>();

        public static ITaskManager TaskManager => Framework.Service<ITaskManager>();
    }
}
