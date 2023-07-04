using System.Reflection;

namespace EventManagement.Persistence;

public static class AssemblyReference {
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
