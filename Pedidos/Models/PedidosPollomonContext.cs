using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pedidos.Models
{
    public partial class PedidosPollomonContext : DbContext
    {
        public PedidosPollomonContext()
        {
        }

        public PedidosPollomonContext(DbContextOptions<PedidosPollomonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Calificacion> Calificacion { get; set; }
        public virtual DbSet<CategoriaProducto> CategoriaProducto { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFactura { get; set; }
        public virtual DbSet<DetallePedido> DetallePedido { get; set; }
        public virtual DbSet<Dosificacion> Dosificacion { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Fotos> Fotos { get; set; }
        public virtual DbSet<Mensajes> Mensajes { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Pagina> Pagina { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<Transportador> Transportador { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("workstation id=PedidosPollomon.mssql.somee.com;packet size=4096;user id=pollomon_SQLLogin_1;pwd=akoluaza31;data source=PedidosPollomon.mssql.somee.com;persist security info=False;initial catalog=PedidosPollomon");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.HasIndex(e => e.IdPedido)
                    .HasName("fkIdx_pedido_calificacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDestino).HasColumnName("id_destino");

                entity.Property(e => e.IdOrigen).HasColumnName("id_origen");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasColumnName("observaciones")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Puntaje).HasColumnName("puntaje");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Calificacion)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pedido_calificacion");
            });

            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Lugar)
                    .IsRequired()
                    .HasColumnName("lugar")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdDestino).HasColumnName("id_destino");

                entity.Property(e => e.IdOrigen).HasColumnName("id_origen");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasColumnName("celular")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("direccion")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Latitud)
                    .HasColumnName("latitud")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Longitud)
                    .HasColumnName("longitud")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.NombresApellidos)
                    .IsRequired()
                    .HasColumnName("nombres_apellidos")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Referencia)
                    .IsRequired()
                    .HasColumnName("referencia")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasColumnName("telefono")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasIndex(e => e.IdDetallePedido)
                    .HasName("fkIdx_detallepedido_factura");

                entity.HasIndex(e => e.IdFactura)
                    .HasName("fkIdx_factura_detallefactura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.IdDetallePedido).HasColumnName("id_detalle_pedido");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.Monto)
                    .HasColumnName("monto")
                    .HasColumnType("decimal(18, 1)");

                entity.HasOne(d => d.IdDetallePedidoNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.IdDetallePedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detallepedido_factura");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.DetalleFactura)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_detallefactura");
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasIndex(e => e.IdPedido)
                    .HasName("fkIdx_pedido_detallepedido");

                entity.HasIndex(e => e.IdProducto)
                    .HasName("fkIdx_producto_detallepedido");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("decimal(18, 1)");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.SubMonto)
                    .HasColumnName("sub_monto")
                    .HasColumnType("decimal(18, 1)");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.DetallePedido)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pedido_detallepedido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetallePedido)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_detallepedido");
            });

            modelBuilder.Entity<Dosificacion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activa).HasColumnName("activa");

                entity.Property(e => e.FechaActivacion)
                    .HasColumnName("fecha_activacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaLimiteEmision)
                    .HasColumnName("fecha_limite_emision")
                    .HasColumnType("datetime");

                entity.Property(e => e.Leyenda)
                    .IsRequired()
                    .HasColumnName("leyenda")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Llave)
                    .IsRequired()
                    .HasColumnName("llave")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.NroAutorizacion).HasColumnName("nro_autorizacion");

                entity.Property(e => e.NroFacturaActual).HasColumnName("nro_factura_actual");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasIndex(e => e.IdDosificacion)
                    .HasName("fkIdx_dosificacion_factura");

                entity.HasIndex(e => e.IdPedido)
                    .HasName("fkIdx_pedido_factura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoControl)
                    .IsRequired()
                    .HasColumnName("codigo_control")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEmision)
                    .HasColumnName("fecha_emision")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdDosificacion).HasColumnName("id_dosificacion");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.NroFactura).HasColumnName("nro_factura");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasColumnName("observaciones")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDosificacionNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdDosificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dosificacion_factura");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pedido_factura");
            });

            modelBuilder.Entity<Fotos>(entity =>
            {
                entity.HasIndex(e => e.IdProducto)
                    .HasName("fkIdx_producto_fotos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.PathImg)
                    .IsRequired()
                    .HasColumnName("path_img")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Fotos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_fotos");
            });

            modelBuilder.Entity<Mensajes>(entity =>
            {
                entity.HasIndex(e => e.IdChat)
                    .HasName("fkIdx_chat_mensajes");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaHora)
                    .HasColumnName("fecha_hora")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdChat).HasColumnName("id_chat");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdChatNavigation)
                    .WithMany(p => p.Mensajes)
                    .HasForeignKey(d => d.IdChat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_chat_mensajes");
            });

            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.HasIndex(e => e.IdProducto)
                    .HasName("fkIdx_producto_oferta");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaFin)
                    .HasColumnName("fecha_fin")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("fecha_inicio")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.PrecioOferta)
                    .HasColumnName("precio_oferta")
                    .HasColumnType("decimal(18, 1)");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_oferta");
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasColumnName("contenido")
                    .HasColumnType("text");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Publicado).HasColumnName("publicado");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasIndex(e => e.IdCliente)
                    .HasName("fkIdx_cliente_pedido");

                entity.HasIndex(e => e.IdTransporte)
                    .HasName("fkIdx_transporte_pedido");

                entity.HasIndex(e => e.IdVendedor)
                    .HasName("fkIdx_vendedor_pedido");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoQrFactura)
                    .IsRequired()
                    .HasColumnName("codigo_qr_factura")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaAtencion)
                    .HasColumnName("fecha_atencion")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaEntrega)
                    .HasColumnName("fecha_entrega")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnName("fecha_ingreso")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaSalida)
                    .HasColumnName("fecha_salida")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdTransporte).HasColumnName("id_transporte");

                entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");

                entity.Property(e => e.MontoCliente)
                    .HasColumnName("monto_cliente")
                    .HasColumnType("decimal(18, 1)");

                entity.Property(e => e.MontoEnvio)
                    .HasColumnName("monto_envio")
                    .HasColumnType("decimal(18, 1)");

                entity.Property(e => e.TipoPago)
                    .IsRequired()
                    .HasColumnName("tipo_pago")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_pedido");

                entity.HasOne(d => d.IdTransporteNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdTransporte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transporte_pedido");

                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.IdVendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vendedor_pedido");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasIndex(e => e.IdCategoria)
                    .HasName("fkIdx_categoria_producto");

                entity.HasIndex(e => e.IdVendedor)
                    .HasName("fkIdx_vendedor_producto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnName("cantidad")
                    .HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Especificaciones)
                    .IsRequired()
                    .HasColumnName("especificaciones")
                    .HasColumnType("text");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasColumnName("marca")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasColumnName("modelo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioMayor)
                    .HasColumnName("precio_mayor")
                    .HasColumnType("decimal(18, 1)");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnName("precio_unitario")
                    .HasColumnType("decimal(18, 1)");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_categoria_producto");

                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.IdVendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vendedor_producto");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Transportador>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasColumnName("celular")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DescripcionVehiculo)
                    .IsRequired()
                    .HasColumnName("descripcion_vehiculo")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Latitud)
                    .HasColumnName("latitud")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Longitud)
                    .HasColumnName("longitud")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasColumnName("nombre_completo")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TipoVehiculo)
                    .IsRequired()
                    .HasColumnName("tipo_vehiculo")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.IdRol)
                    .HasName("fkIdx_rol_usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("clave")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_usuario");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasIndex(e => e.IdRubro)
                    .HasName("fkIdx_rubro_vendedor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasColumnName("celular")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("correo")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("direccion")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IdRubro).HasColumnName("id_rubro");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasColumnName("nombre_empresa")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PathLogo)
                    .IsRequired()
                    .HasColumnName("path_logo")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PersonaContacto)
                    .IsRequired()
                    .HasColumnName("persona_contacto")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasColumnName("telefono")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRubroNavigation)
                    .WithMany(p => p.Vendedor)
                    .HasForeignKey(d => d.IdRubro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rubro_vendedor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
