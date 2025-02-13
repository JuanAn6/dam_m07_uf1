CREATE TABLE Sala (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    adreca VARCHAR(255) NOT NULL,
    municipi VARCHAR(255) NOT NULL,
    num_columnes INT NOT NULL,
    num_files INT NOT NULL,
    te_mapa BOOLEAN NOT NULL
);
CREATE TABLE Zona (
    id INT AUTO_INCREMENT PRIMARY KEY,
    `desc` TEXT,
    nom VARCHAR(255) NOT NULL,
    numero INT NOT NULL,
    capacitat INT NOT NULL,
    color VARCHAR(50),
    sala_id INT NOT NULL,
    FOREIGN KEY (sala_id) REFERENCES Sala(id)
);
CREATE TABLE Cadira (
    id INT AUTO_INCREMENT PRIMARY KEY,
    zona_id INT NOT NULL,
    x INT NOT NULL,
    y INT NOT NULL,
    FOREIGN KEY (zona_id) REFERENCES Zona(id)
);
CREATE TABLE Event (
    id INT AUTO_INCREMENT PRIMARY KEY,
    tipus VARCHAR(30) NOT NULL,
    nom VARCHAR(255) NOT NULL,
    protagonista VARCHAR(255) NOT NULL,
    descripcio VARCHAR(1024) NOT NULL,
    img_path VARCHAR(255) NOT NULL,
    data_event DATE NOT NULL,
    estat VARCHAR(30) NOT NULL,
    sala_id INT NOT NULL,
    FOREIGN KEY (sala_id) REFERENCES Sala(id)
);