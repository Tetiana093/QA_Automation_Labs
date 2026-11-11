-- Таблиця для тестування методу IsOperator
CREATE TABLE IsOperatorTests (
    ID int IDENTITY(1,1) PRIMARY KEY,
    Symbol nvarchar(5) NOT NULL,
    Expected bit NOT NULL
);

-- Таблиця для тестування методу InsertSymbol
CREATE TABLE InsertSymbolTests (
    ID int IDENTITY(1,1) PRIMARY KEY,
    InputString nvarchar(50) NOT NULL,
    SymbolToInsert nvarchar(1) NOT NULL,
    InsertPosition int NOT NULL,
    ExpectedString nvarchar(50) NOT NULL
);