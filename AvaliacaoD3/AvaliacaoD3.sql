CREATE DATABASE AvaliacaoD3;
USE AvaliacaoD3;

CREATE TABLE users (
	Id VARCHAR(255) PRIMARY KEY NOT NULL,
	Nome VARCHAR(255) NOT NULL,
	Email VARCHAR(255) NOT NULL UNIQUE,
	Senha VARCHAR(255) NOT NULL
);

INSERT INTO users (Id, Nome, Email, Senha) VALUES ('a808ba4e-95c5-4da3-9b20-d18847a54afb', 'Admin', 'admin@email.com', 'admin123');