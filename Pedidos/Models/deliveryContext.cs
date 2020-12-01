using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Pedidos.Models
{
    public partial class deliveryContext : DbContext
    {
        public deliveryContext()
        {
        }

        public deliveryContext(DbContextOptions<deliveryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Calificacion> Calificacions { get; set; }
        public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public virtual DbSet<DetallePedido> DetallePedidos { get; set; }
        public virtual DbSet<Direccion> Direccions { get; set; }
        public virtual DbSet<Dosificacion> Dosificacions { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<Foto> Fotos { get; set; }
        public virtual DbSet<Ofertum> Oferta { get; set; }
        public virtual DbSet<Pagina> Paginas { get; set; }
        public virtual DbSet<Parametro> Parametros { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Rubro> Rubros { get; set; }
        public virtual DbSet<Transportador> Transportadors { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Vendedor> Vendedors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS02;Database=delivery;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calificacion>(entity =>
            {
                entity.ToTable("Calificacion");

                entity.HasIndex(e => e.IdPedido, "fkIdx_pedido_calificacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDestino).HasColumnName("id_destino");

                entity.Property(e => e.IdOrigen).HasColumnName("id_origen");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("observaciones");

                entity.Property(e => e.Puntaje).HasColumnName("puntaje");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Calificacions)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pedido_calificacion");
            });

            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.ToTable("CategoriaProducto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Lugar)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("lugar");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaHora)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_hora")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.IdDestino).HasColumnName("id_destino");

                entity.Property(e => e.IdOrigen).HasColumnName("id_origen");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("text");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.NombresApellidos)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombres_apellidos");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.ToTable("DetalleFactura");

                entity.HasIndex(e => e.IdDetallePedido, "fkIdx_detallepedido_factura");

                entity.HasIndex(e => e.IdFactura, "fkIdx_factura_detallefactura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("cantidad")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdDetallePedido).HasColumnName("id_detalle_pedido");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("monto")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.HasOne(d => d.IdDetallePedidoNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdDetallePedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_detallepedido_factura");

                entity.HasOne(d => d.IdFacturaNavigation)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.IdFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factura_detallefactura");
            });

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.ToTable("DetallePedido");

                entity.HasIndex(e => e.IdPedido, "fkIdx_pedido_detallepedido");

                entity.HasIndex(e => e.IdProducto, "fkIdx_producto_detallepedido");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("cantidad")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.SubMonto)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("sub_monto")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pedido_detallepedido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_detallepedido");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.ToTable("Direccion");

                entity.HasIndex(e => e.IdCliente, "fkIdx_cliente_direccion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Latitud)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("latitud")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 6)");

                entity.Property(e => e.Longitud)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("longitud")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 6)");

                entity.Property(e => e.Predeterminada).HasColumnName("predeterminada");

                entity.Property(e => e.Referencia)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("referencia");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_direccion");
            });

            modelBuilder.Entity<Dosificacion>(entity =>
            {
                entity.ToTable("Dosificacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activa).HasColumnName("activa");

                entity.Property(e => e.ActividadPrincipal)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("actividad_principal");

                entity.Property(e => e.ActividadSecundaria)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("actividad_secundaria");

                entity.Property(e => e.FechaActivacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_activacion")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FechaLimiteEmision)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_limite_emision")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.Leyenda)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("leyenda");

                entity.Property(e => e.Llave)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("llave");

                entity.Property(e => e.NroAutorizacion).HasColumnName("nro_autorizacion");

                entity.Property(e => e.NroFacturaActual).HasColumnName("nro_factura_actual");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");

                entity.HasIndex(e => e.IdDosificacion, "fkIdx_dosificacion_factura");

                entity.HasIndex(e => e.IdPedido, "fkIdx_pedido_factura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoControl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("codigo_control");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaEmision)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_emision")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.IdDosificacion).HasColumnName("id_dosificacion");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");

                entity.Property(e => e.NroFactura).HasColumnName("nro_factura");

                entity.Property(e => e.Observaciones)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("observaciones");

                entity.HasOne(d => d.IdDosificacionNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdDosificacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dosificacion_factura");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pedido_factura");
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.HasIndex(e => e.IdProducto, "fkIdx_producto_fotos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.PathImg)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("path_img");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Fotos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_fotos");
            });

            modelBuilder.Entity<Ofertum>(entity =>
            {
                entity.HasIndex(e => e.IdProducto, "fkIdx_producto_oferta");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_fin")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.PrecioOferta)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("precio_oferta")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_producto_oferta");
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.ToTable("Pagina");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("contenido")
                    .HasAnnotation("Relational:ColumnType", "text");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_actualizacion")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_creacion")
                    .HasDefaultValueSql("(getdate())")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Publicado).HasColumnName("publicado");

                entity.Property(e => e.Tipo).HasColumnName("tipo");
            });

            modelBuilder.Entity<Parametro>(entity =>
            {
                entity.ToTable("Parametro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Valor)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("valor")
                    .HasAnnotation("Relational:ColumnType", "text");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedido");

                entity.HasIndex(e => e.IdCliente, "fkIdx_cliente_pedido");

                entity.HasIndex(e => e.IdTransporte, "fkIdx_transporte_pedido");

                entity.HasIndex(e => e.IdVendedor, "fkIdx_vendedor_pedido");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoQrFactura)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("codigo_qr_factura");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaAtencion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_atencion")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FechaEntrega)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_entrega")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_ingreso")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_salida")
                    .HasAnnotation("Relational:ColumnType", "datetime");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdTransporte).HasColumnName("id_transporte");

                entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");

                entity.Property(e => e.MontoCliente)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("monto_cliente")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.Property(e => e.MontoEnvio)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("monto_envio")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.Property(e => e.TipoPago)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo_pago");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cliente_pedido");

                entity.HasOne(d => d.IdTransporteNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdTransporte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_transporte_pedido");

                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdVendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vendedor_pedido");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.HasIndex(e => e.IdCategoria, "fkIdx_categoria_producto");

                entity.HasIndex(e => e.IdVendedor, "fkIdx_vendedor_producto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("cantidad")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.Property(e => e.Especificaciones)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("especificaciones")
                    .HasAnnotation("Relational:ColumnType", "text");

                entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");

                entity.Property(e => e.IdVendedor).HasColumnName("id_vendedor");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modelo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.PrecioMayor)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("precio_mayor")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("precio_unitario")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 1)");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_categoria_producto");

                entity.HasOne(d => d.IdVendedorNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdVendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_vendedor_producto");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Rubro>(entity =>
            {
                entity.ToTable("Rubro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdPadre).HasColumnName("id_padre");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Transportador>(entity =>
            {
                entity.ToTable("Transportador");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.DescripcionVehiculo)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_vehiculo");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Latitud)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("latitud")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 6)");

                entity.Property(e => e.Longitud)
                    .HasColumnType("decimal(18, 6)")
                    .HasColumnName("longitud")
                    .HasAnnotation("Relational:ColumnType", "decimal(18, 6)");

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nombre_completo");

                entity.Property(e => e.TipoVehiculo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo_vehiculo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.IdRol, "fkIdx_rol_usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Entidad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("entidad");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.IdRef).HasColumnName("id_ref");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rol_usuario");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.ToTable("Vendedor");

                entity.HasIndex(e => e.IdRubro, "fkIdx_rubro_vendedor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.IdRubro).HasColumnName("id_rubro");

                entity.Property(e => e.NombreEmpresa)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nombre_empresa");

                entity.Property(e => e.PathLogo)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("path_logo");

                entity.Property(e => e.PersonaContacto)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("persona_contacto");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdRubroNavigation)
                    .WithMany(p => p.Vendedors)
                    .HasForeignKey(d => d.IdRubro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rubro_vendedor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
