-- Criação da tabela de Transportes
CREATE TABLE Transportes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    CustoPorMetroCubico DECIMAL(10, 2) NOT NULL
);

-- Criação da tabela de Serviços
CREATE TABLE Servicos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Origem NVARCHAR(255) NOT NULL,
    Destino NVARCHAR(255) NOT NULL,
    DataSaida DATE NOT NULL,
    Altura DECIMAL(10, 2) NOT NULL,
    Largura DECIMAL(10, 2) NOT NULL,
    Comprimento DECIMAL(10, 2) NOT NULL,
    TransporteId INT NOT NULL,
    Responsavel NVARCHAR(255) NOT NULL,
    Status NVARCHAR(50) NOT NULL, -- Pendente, Finalizado, Cancelado
    CustoTotal DECIMAL(18, 2) NULL,
    FOREIGN KEY (TransporteId) REFERENCES Transportes(Id)
);

-- Inserção dos tipos de transporte básicos
INSERT INTO Transportes (Nome, CustoPorMetroCubico) 
VALUES ('Caminhão', 35.00), ('Navio', 22.00);