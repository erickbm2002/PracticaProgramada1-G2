PRAGMA foreign_keys = ON;

BEGIN IMMEDIATE;

CREATE TABLE IF NOT EXISTS Telefonos (
	IdTelefono INTEGER PRIMARY KEY AUTOINCREMENT,
	ClienteId INTEGER NOT NULL,
	Numero TEXT NOT NULL CHECK (length(trim(Numero)) > 0),
	CONSTRAINT FK_Telefonos_Clientes_ClienteId
		FOREIGN KEY (ClienteId) REFERENCES Clientes (Id) ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS IX_Telefonos_ClienteId
	ON Telefonos (ClienteId);

INSERT INTO Telefonos (ClienteId, Numero)
SELECT Id, Telefono
FROM Clientes
WHERE Telefono IS NOT NULL
  AND length(trim(Telefono)) > 0
  AND NOT EXISTS (
	  SELECT 1
	  FROM Telefonos
	  WHERE Telefonos.ClienteId = Clientes.Id
		AND Telefonos.Numero = Clientes.Telefono
  );

COMMIT;
