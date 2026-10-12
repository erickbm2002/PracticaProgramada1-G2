PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Clientes (
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
	Nombre TEXT NOT NULL,
	Apellido TEXT,
	Email TEXT UNIQUE,
	Telefono TEXT,
	FechaNacimiento DATE,
	Direccion TEXT,
	Ciudad TEXT,
	Pais TEXT,
	FechaCreacion DATETIME DEFAULT (datetime('now')),
	FechaModificacion DATETIME,
	Activo INTEGER NOT NULL DEFAULT 1 CHECK (Activo IN (0,1))
);

CREATE INDEX IF NOT EXISTS idx_clientes_nombre ON Clientes(Nombre, Apellido);
CREATE INDEX IF NOT EXISTS idx_clientes_email ON Clientes(Email);
