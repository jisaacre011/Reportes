CREATE DATABASE Reportes;

USE Reportes;

CREATE TABLE Clientes (
    ClienteId      INT           PRIMARY KEY IDENTITY(1,1),
    NombreCompleto VARCHAR(150)  NOT NULL,
    Correo         VARCHAR(100)  NOT NULL UNIQUE,
    Telefono       VARCHAR(20)   NOT NULL,
    Direccion      VARCHAR(200)  NOT NULL,
    Garantia       VARCHAR(200)  NOT NULL,   -- Objeto ofrecido como garantía (OBLIGATORIO)
    Sueldo         DECIMAL(18,2) NOT NULL,   -- Se usa para validar el límite del préstamo
    EsMoroso       BIT           NOT NULL DEFAULT 0,   -- 0 = Normal, 1 = Moroso
    FechaRegistro  DATETIME      NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Prestamos (
    PrestamoId    INT           PRIMARY KEY IDENTITY(1,1),
    ClienteId     INT           NOT NULL,
    Monto         DECIMAL(18,2) NOT NULL,   -- Capital prestado
    PlazoMeses    INT           NOT NULL,   -- Duración en meses
    TasaInteres   DECIMAL(5,2)  NOT NULL,   -- Ej: 13.25, 15.00, 30.00
    Interes       DECIMAL(18,2) NOT NULL,   -- Interés total generado
    MontoTotal    DECIMAL(18,2) NOT NULL,   -- Monto + Interés
    CuotaMensual  DECIMAL(18,2) NOT NULL,   -- Cuota fija por mes
    CantidadMoras INT           NOT NULL DEFAULT 0,
    FechaInicio   DATETIME      NOT NULL DEFAULT GETDATE(),
    Estado        VARCHAR(20)   NOT NULL DEFAULT 'Activo', -- Activo / Pagado / Moroso

    CONSTRAINT FK_Prestamos_Clientes FOREIGN KEY (ClienteId)
        REFERENCES Clientes(ClienteId)
);

CREATE TABLE TablaAmortizacion (
    AmortizacionId    INT           PRIMARY KEY IDENTITY(1,1),
    PrestamoId        INT           NOT NULL,
    NumeroCuota       INT           NOT NULL,       -- Mes 1, 2, 3...
    Cuota             DECIMAL(18,2) NOT NULL,       -- Cuota fija mensual
    InteresDelMes     DECIMAL(18,2) NOT NULL,       -- Saldo anterior * tasa mensual
    CapitalAmortizado DECIMAL(18,2) NOT NULL,       -- Cuota - Interés del mes
    SaldoRestante     DECIMAL(18,2) NOT NULL,       -- Saldo anterior - Capital amortizado
    FechaPago         DATETIME      NULL,           -- NULL = aún no pagado
    Pagado            BIT           NOT NULL DEFAULT 0,  -- 0 = Pendiente, 1 = Pagado

    CONSTRAINT FK_Amortizacion_Prestamos FOREIGN KEY (PrestamoId)
        REFERENCES Prestamos(PrestamoId)
);

CREATE TABLE Pagos (
    PagoId           INT           PRIMARY KEY IDENTITY(1,1),
    PrestamoId       INT           NOT NULL,
    NumeroCuota      INT           NOT NULL,       -- A qué cuota corresponde este pago
    MontoAnterior    DECIMAL(18,2) NOT NULL,       -- Saldo antes del pago
    InteresPagado    DECIMAL(18,2) NOT NULL,       -- Interés de ese mes
    NuevoMonto       DECIMAL(18,2) NOT NULL,       -- Saldo después del pago
    CuotaPagada      DECIMAL(18,2) NOT NULL,       -- Lo que pagó (puede incluir mora)
    MesesRestantes   INT           NOT NULL,
    TotalInteresAcum DECIMAL(18,2) NOT NULL,       -- Suma acumulada de intereses pagados
    TasaPrestamo     DECIMAL(5,2)  NOT NULL,
    TuveMora         BIT           NOT NULL DEFAULT 0,
    MontoMora        DECIMAL(18,2) NOT NULL DEFAULT 0,   -- 10% de la cuota si aplica
    FechaPago        DATETIME      NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_Pagos_Prestamos FOREIGN KEY (PrestamoId)
        REFERENCES Prestamos(PrestamoId)
);

CREATE TABLE FondoEntidad (
    FondoId             INT           PRIMARY KEY IDENTITY(1,1),
    CapitalDisponible   DECIMAL(18,2) NOT NULL DEFAULT 10000000.00,
    TotalPrestado       DECIMAL(18,2) NOT NULL DEFAULT 0,
    UltimaActualizacion DATETIME      NOT NULL DEFAULT GETDATE()
);

-- Insertar el fondo inicial (10 millones)
INSERT INTO FondoEntidad (CapitalDisponible, TotalPrestado)
VALUES (10000000.00, 0.00);