# Unit Test af DataLayer
Disse test udføres imod en fysisk SQL database, som Data Seedes via context-klassen i DataLayer. Trigges vha. EnsureCreated().

Benytter Utilities/SqlContext.cs, som opretter et objekt af typen DbContextOptions til Contexten.
Tests ligger i klassen UnitTestDataLayer.cs


# Unit Test af ServiceLayer
Disse test udføres imod ServiceLayer og derfor er databasen lavet med InMemory.
Benytter Utilities/InMemoryContext.cs, som opretter et objekt af typen DbContextOptions til Contexten.
Tests ligger i klassen UnitTestServiceLayer.cs