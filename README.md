# MessengerDataAnalyzer

Allows you to extract data from your messanger covnersations.

## How to run

With CMD 
```
> .\MessengerDataAnalyzer.exe "C:\Optional Custom Directory\With Json Files"
```

## Analyzers available

- Per Person Counters:
  - Messages sent
  - Messages sent
  - Pictures sent
  - Reactions given
  - Top reaction used
  - Messages containing specified words
  - Average message size
  
- Overall counters:
  - Reactions given
  
- Other counters:
  - Grouped reactions for others from specified person
  
 ## How to add your own analyzers?
 
 Just add a class implementing `IAnalyzer` interface.
  
```
public class MyNewAnalyzer : IAnalyzer
{
    public bool IsDisabled { get; } = false;
    
    public string AnalyzeData(Conversation conversation)
    {
        
        return "This will be returned to console and written to file";
    }
}
