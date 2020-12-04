DECLARE @Sql NVARCHAR(500) DECLARE @Cursor CURSOR

SET @Cursor = CURSOR FAST_FORWARD FOR
SELECT DISTINCT sql = 'ALTER TABLE [' + tc2.TABLE_SCHEMA + '].[' +  tc2.TABLE_NAME + '] DROP [' + rc1.CONSTRAINT_NAME + '];'
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc1
LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc2 ON tc2.CONSTRAINT_NAME =rc1.CONSTRAINT_NAME

OPEN @Cursor FETCH NEXT FROM @Cursor INTO @Sql

WHILE (@@FETCH_STATUS = 0)
BEGIN
Exec sp_executesql @Sql
FETCH NEXT FROM @Cursor INTO @Sql
END

CLOSE @Cursor DEALLOCATE @Cursor
GO

EXEC sp_MSforeachtable 'DROP TABLE ?'
GO

-- ************** [CategoriaProducto]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='CategoriaProducto')
CREATE TABLE [CategoriaProducto]
(
 [id]          bigint IDENTITY (1, 1) NOT NULL ,
 [nombre]      varchar(50) NOT NULL ,
 [descripcion] varchar(500) NOT NULL ,
 [lugar]       varchar(150) NOT NULL ,


 CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Rubro]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Rubro')
CREATE TABLE [Rubro]
(
 [id]          bigint IDENTITY (1, 1) NOT NULL ,
 [nombre]      varchar(50) NOT NULL ,
 [descripcion] varchar(250) NOT NULL ,
 [id_padre]    bigint, 

 CONSTRAINT [PK_rubro] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Transportador]
/*
IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Transportador')
CREATE TABLE [Transportador]
(
 [id]                   bigint IDENTITY (1, 1) NOT NULL ,
 [nombre_completo]      varchar(250) NOT NULL ,
 [celular]              varchar(10) NOT NULL ,
 [descripcion_vehiculo] varchar(250) NOT NULL ,
 [tipo_vehiculo]        varchar(50) NOT NULL ,
 [estado]               varchar(50) NOT NULL ,
 [latitud]              decimal(18,6) NOT NULL ,
 [longitud]             decimal(18,6) NOT NULL ,
 [email]                VARCHAR(50) NOT NULL,


 CONSTRAINT [PK_tranportador] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Cliente]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Cliente')
CREATE TABLE [Cliente]
(
 [id]                bigint IDENTITY (1, 1) NOT NULL ,
 [nombres_apellidos] varchar(500) NOT NULL ,
 [celular]           varchar(10) NOT NULL ,
 [telefono]          varchar(10) NOT NULL ,
 [correo]			 varchar(100) NULL,


 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO*/





-- ************** [Transportador]
IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Transportador')
CREATE TABLE [Transportador]
(
 [id]                   bigint IDENTITY (1, 1) NOT NULL ,
 [nombre_completo]      varchar(250) NOT NULL ,
 [celular]              varchar(10) NOT NULL ,
 [descripcion_vehiculo] varchar(250) NOT NULL ,
 [tipo_vehiculo]        varchar(50) NOT NULL ,
 [estado]               varchar(50) NOT NULL ,
 [latitud]              decimal(18,6) NOT NULL ,
 [longitud]             decimal(18,6) NOT NULL ,


 CONSTRAINT [PK_tranportador] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Cliente]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Cliente')
CREATE TABLE [Cliente]
(
 [id]                bigint IDENTITY (1, 1) NOT NULL ,
 [nombres_apellidos] varchar(500) NOT NULL ,
 [celular]           varchar(10) NOT NULL ,
 [telefono]          varchar(10) NOT NULL ,


 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Direccion]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Direccion')
CREATE TABLE [Direccion]
(
 [id]                bigint IDENTITY (1, 1) NOT NULL ,
 [descripcion]       varchar(500) NOT NULL ,
 [latitud]           decimal(18,6) NOT NULL ,
 [longitud]          decimal(18,6) NOT NULL ,
 [referencia]        varchar(500) NOT NULL ,
 [predeterminada]    bit NOT NULL default 0 ,
 [id_cliente]        bigint NOT NULL ,


 CONSTRAINT [PK_direccion] PRIMARY KEY CLUSTERED ([id] ASC), 
 CONSTRAINT [FK_cliente_direccion] FOREIGN KEY ([id_cliente])  REFERENCES [Cliente]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_cliente_direccion] ON [Direccion] 
 (
  [id_cliente] ASC
 )

GO

-- ************** [Vendedor]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Vendedor')
CREATE TABLE [Vendedor]
(
 [id]               bigint IDENTITY (1, 1) NOT NULL ,
 [persona_contacto] varchar(250) NOT NULL ,
 [celular]          varchar(10) NOT NULL ,
 [telefono]         varchar(10) NOT NULL ,
 [correo]           varchar(150) NOT NULL ,
 [nombre_empresa]   varchar(250) NOT NULL ,
 [direccion]        varchar(250) NOT NULL ,
 [path_logo]        varchar(250) NOT NULL ,
 [id_rubro]         bigint NOT NULL ,


 CONSTRAINT [PK_vendedor] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_rubro_vendedor] FOREIGN KEY ([id_rubro])  REFERENCES [Rubro]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_rubro_vendedor] ON [Vendedor] 
 (
  [id_rubro] ASC
 )

GO

-- ************** [Producto]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Producto')
CREATE TABLE [Producto]
(
 [id]               bigint IDENTITY (1, 1) NOT NULL ,
 [nombre]           varchar(500) NOT NULL ,
 [precio_unitario]  decimal(18,1) NOT NULL ,
 [cantidad]         decimal(18,1) NOT NULL ,
 [precio_mayor]     decimal(18,1) NOT NULL ,
 [marca]            varchar(50) NOT NULL ,
 [modelo]           varchar(50) NOT NULL ,
 [especificaciones] text NOT NULL ,
 [id_categoria]     bigint NOT NULL ,
 [id_vendedor]      bigint NOT NULL ,


 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_categoria_producto] FOREIGN KEY ([id_categoria])  REFERENCES [CategoriaProducto]([id]),
 CONSTRAINT [FK_vendedor_producto] FOREIGN KEY ([id_vendedor])  REFERENCES [Vendedor]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_categoria_producto] ON [Producto] 
 (
  [id_categoria] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_vendedor_producto] ON [Producto] 
 (
  [id_vendedor] ASC
 )

GO

-- ************** [Fotos]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Fotos')
CREATE TABLE [Fotos]
(
 [id]          bigint IDENTITY (1, 1) NOT NULL ,
 [descripcion] varchar(150) NOT NULL ,
 [path_img]    varchar(max) NOT NULL ,
 [id_producto] bigint NOT NULL ,


 CONSTRAINT [PK_fotos] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_producto_fotos] FOREIGN KEY ([id_producto])  REFERENCES [Producto]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_producto_fotos] ON [Fotos] 
 (
  [id_producto] ASC
 )

GO

-- ************** [Oferta]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Oferta')
CREATE TABLE [Oferta]
(
 [id]            bigint IDENTITY (1, 1) NOT NULL ,
 [fecha_inicio]  datetime NOT NULL ,
 [fecha_fin]     datetime NOT NULL ,
 [precio_oferta] decimal(18,1) NOT NULL ,
 [id_producto]   bigint NOT NULL ,
 fecha_creacion datetime not null default GETDATE(), 
 fecha_actualizacion datetime, 
 id_usuario bigint, 


 CONSTRAINT [PK_oferta] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_producto_oferta] FOREIGN KEY ([id_producto])  REFERENCES [Producto]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_producto_oferta] ON [Oferta] 
 (
  [id_producto] ASC
 )

GO

-- ************** [Pedido]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Pedido')
CREATE TABLE [Pedido]
(
 [id]                bigint IDENTITY (1, 1) NOT NULL ,
 [fecha_ingreso]     datetime NULL ,
 [fecha_atencion]    datetime NULL ,
 [fecha_salida]      datetime NULL ,
 [fecha_entrega]     datetime NULL ,
 [estado]            varchar(50) NULL ,
 [codigo_qr_factura] varchar(500) NULL ,
 [monto_envio]       decimal(18,1) NULL ,
 [tipo_pago]         varchar(50) NULL ,
 [monto_cliente]     decimal(18,1) NULL ,
 [id_cliente]        bigint NOT NULL ,
 [id_vendedor]       bigint NULL ,
 [id_transporte]     bigint NULL ,


 CONSTRAINT [PK_pedido] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_cliente_pedido] FOREIGN KEY ([id_cliente])  REFERENCES [Cliente]([id]),
 CONSTRAINT [FK_vendedor_pedido] FOREIGN KEY ([id_vendedor])  REFERENCES [Vendedor]([id]),
 CONSTRAINT [FK_transporte_pedido] FOREIGN KEY ([id_transporte])  REFERENCES [Transportador]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_cliente_pedido] ON [Pedido] 
 (
  [id_cliente] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_vendedor_pedido] ON [Pedido] 
 (
  [id_vendedor] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_transporte_pedido] ON [Pedido] 
 (
  [id_transporte] ASC
 )

GO

-- ************** [DetallePedido]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='DetallePedido')
CREATE TABLE [DetallePedido]
(
 [id]          bigint IDENTITY (1, 1) NOT NULL ,
 [id_pedido]   bigint NOT NULL ,
 [id_producto] bigint NOT NULL ,
 [cantidad]    decimal(18,1) NOT NULL ,
 [sub_monto]   decimal(18,1) NOT NULL ,


 CONSTRAINT [PK_detallepedido] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_pedido_detallepedido] FOREIGN KEY ([id_pedido])  REFERENCES [Pedido]([id]),
 CONSTRAINT [FK_producto_detallepedido] FOREIGN KEY ([id_producto])  REFERENCES [Producto]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_pedido_detallepedido] ON [DetallePedido] 
 (
  [id_pedido] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_producto_detallepedido] ON [DetallePedido] 
 (
  [id_producto] ASC
 )

GO

-- ************** [Calificacion]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Calificacion')
CREATE TABLE [Calificacion]
(
 [id]            bigint IDENTITY (1, 1) NOT NULL ,
 [id_origen]     bigint NOT NULL ,
 [id_destino]    bigint NOT NULL ,
 [puntaje]       int NOT NULL ,
 [observaciones] varchar(500) NOT NULL ,
 [tipo]          varchar(50) NOT NULL ,
 [id_pedido]     bigint NOT NULL ,


 CONSTRAINT [PK_calificacion] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_pedido_calificacion] FOREIGN KEY ([id_pedido])  REFERENCES [Pedido]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_pedido_calificacion] ON [Calificacion] 
 (
  [id_pedido] ASC
 )

GO

-- ************** [Rol]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Rol')
CREATE TABLE [Rol]
(
 [id]          bigint IDENTITY (1, 1) NOT NULL ,
 [nombre]      varchar(50) NOT NULL ,


 CONSTRAINT [PK_rold] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Usuario]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Usuario')
CREATE TABLE [Usuario]
(
 [id]      bigint IDENTITY (1, 1) NOT NULL ,
 [nombre]  varchar(50) NOT NULL ,
 [clave]   varchar(50) NOT NULL ,
 [estado]  varchar(50) NOT NULL ,
 [entidad] varchar(50) ,
 [id_ref]  bigint ,
 [id_rol]  bigint NOT NULL ,


 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_rol_usuario] FOREIGN KEY ([id_rol])  REFERENCES [Rol]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_rol_usuario] ON [Usuario] 
 (
  [id_rol] ASC
 )

GO

-- ************** [Chat]
/*
IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Chat')
CREATE TABLE [Chat]
(
 [id]         bigint IDENTITY (1, 1) NOT NULL ,
 [id_origen]  bigint NOT NULL ,
 [id_destino] bigint NOT NULL ,
 [estado]     varchar(50) NOT NULL ,


 CONSTRAINT [PK_chat] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO*/

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Chat')
CREATE TABLE [Chat]
(
 [id]         bigint IDENTITY (1, 1) NOT NULL ,
 [id_origen]  bigint NOT NULL ,
 [id_destino] bigint NOT NULL ,
 [fecha_hora] datetime NOT NULL ,
 [text]       varchar(500) NOT NULL ,
 [estado]     varchar(50) NOT NULL ,


 CONSTRAINT [PK_chat] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Mensajes]
/*
IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Mensajes')
CREATE TABLE [Mensajes]
(
 [id]         bigint IDENTITY (1, 1) NOT NULL ,
 [fecha_hora] datetime NOT NULL ,
 [text]       varchar(500) NOT NULL ,
 [id_chat]    bigint NOT NULL ,


 CONSTRAINT [PK_mensajes] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_chat_mensajes] FOREIGN KEY ([id_chat])  REFERENCES [Chat]([id])
);
GO

*/
CREATE NONCLUSTERED INDEX [fkIdx_chat_mensajes] ON [Mensajes] 
 (
  [id_chat] ASC
 )

GO

-- ************** [Pagina]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Pagina')
CREATE TABLE [Pagina]
(
 [id]        bigint IDENTITY (1, 1) NOT NULL ,
 [nombre]    varchar(50) NOT NULL ,
 [contenido] text NOT NULL ,
 [publicado] bit NOT NULL ,
 [tipo]      bigint NOT NULL ,
 fecha_creacion datetime not null default GETDATE(), 
 fecha_actualizacion datetime, 
 id_usuario bigint, 

 CONSTRAINT [PK_pagina] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Dosificacion]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Dosificacion')
CREATE TABLE [Dosificacion]
(
 [id]                   bigint IDENTITY (1, 1) NOT NULL ,
 [nro_autorizacion]     bigint NOT NULL ,
 [nro_factura_actual]   bigint NOT NULL ,
 [llave]                varchar(2000) NOT NULL ,
 [fecha_limite_emision] datetime NOT NULL ,
 [leyenda]              varchar(2000) NOT NULL ,
 [fecha_activacion]     datetime NOT NULL ,
 [activa]               bigint NOT NULL ,
 [actividad_principal]  varchar(2000) NOT NULL ,
 [actividad_secundaria] varchar(2000),

 CONSTRAINT [PK_dosificacion] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ************** [Factura]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Factura')
CREATE TABLE [Factura]
(
 [id]              bigint IDENTITY (1, 1) NOT NULL ,
 [nro_factura]     bigint NOT NULL ,
 [fecha_emision]   datetime NOT NULL ,
 [estado]          varchar(50) NOT NULL ,
 [codigo_control]  varchar(100) NOT NULL ,
 [observaciones]   varchar(2000) NOT NULL ,
 [id_dosificacion] bigint NOT NULL ,
 [id_pedido]       bigint NOT NULL ,


 CONSTRAINT [PK_factura] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_dosificacion_factura] FOREIGN KEY ([id_dosificacion])  REFERENCES [Dosificacion]([id]),
 CONSTRAINT [FK_pedido_factura] FOREIGN KEY ([id_pedido])  REFERENCES [Pedido]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_dosificacion_factura] ON [Factura] 
 (
  [id_dosificacion] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_pedido_factura] ON [Factura] 
 (
  [id_pedido] ASC
 )

GO

-- ************** [DetalleFactura]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='DetalleFactura')
CREATE TABLE [DetalleFactura]
(
 [id]                bigint IDENTITY (1, 1) NOT NULL ,
 [cantidad]          decimal(18,1) NOT NULL ,
 [monto]             decimal(18,1) NOT NULL ,
 [descripcion]       varchar(2000) NOT NULL ,
 [id_detalle_pedido] bigint NOT NULL ,
 [id_factura]        bigint NOT NULL ,


 CONSTRAINT [PK_detallefactura] PRIMARY KEY CLUSTERED ([id] ASC),
 CONSTRAINT [FK_detallepedido_factura] FOREIGN KEY ([id_detalle_pedido])  REFERENCES [DetallePedido]([id]),
 CONSTRAINT [FK_factura_detallefactura] FOREIGN KEY ([id_factura])  REFERENCES [Factura]([id])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_detallepedido_factura] ON [DetalleFactura] 
 (
  [id_detalle_pedido] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_factura_detallefactura] ON [DetalleFactura] 
 (
  [id_factura] ASC
 )

GO

-- ************** [Parametro]

IF NOT EXISTS (SELECT * FROM sys.tables t WHERE t.name='Parametro')
CREATE TABLE [Parametro]
(
 [id]        bigint IDENTITY (1, 1) NOT NULL ,
 [nombre]    varchar(50) NOT NULL ,
 [valor]     text NOT NULL ,


 CONSTRAINT [PK_Parametro] PRIMARY KEY CLUSTERED ([id] ASC)
);
GO

-- ** DATOS **
insert into rol (nombre) values ('Administrador');
insert into rol (nombre) values ('Cliente');
insert into rol (nombre) values ('Vendedor');
insert into rol (nombre) values ('Transportador');

insert into usuario (nombre, clave, estado, id_rol) values ('admin', 'admin', 'activo', 1);

insert into parametro (nombre, valor) values ('tipo_pagina', 'Página');
insert into parametro (nombre, valor) values ('tipo_pagina', 'Noticia');
insert into parametro (nombre, valor) values ('tipo_pagina', 'Soporte');
insert into parametro (nombre, valor) values ('tipo_pagina', 'Principal');