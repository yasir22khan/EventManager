using System.Reflection;

namespace EventManagement.Infrastructure;

public static class AssemblyReference {
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
