namespace MessengerDataAnalyzer.Analyzers.Generics;

public static class AnalyzerExtensions
{
    public static IEnumerable<IAnalyzer> GetAllAnalyzers() =>
        typeof(IAnalyzer).Assembly
            .GetTypes()
            .Where(p => p.IsClass && p.IsAssignableTo(typeof(IAnalyzer)))
            .Select(Activator.CreateInstance)
            .Cast<IAnalyzer>();
}