using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products (Name, Description, Stock, ImageURL, Price, CategoryId) VALUES ('Caderno Espiral', 'Caderno espiral universitário com 200 folhas.', 10, 'https://example.com/images/caderno_espiral.jpg', 15.90, 1)");
            mb.Sql("INSERT INTO Products (Name, Description, Stock, ImageURL, Price, CategoryId) VALUES ('Caneta Esferográfica', 'Caneta esferográfica azul com tinta de secagem rápida.', 50, 'https://example.com/images/caneta_esferografica.jpg', 2.50, 1)");

            mb.Sql("INSERT INTO Products (Name, Description, Stock, ImageURL, Price, CategoryId) VALUES ('Smartphone XYZ', 'Smartphone XYZ com tela de 6.5 polegadas e câmera de 48MP.', 5, 'https://example.com/images/smartphone_xyz.jpg', 1999.99, 2)");
            mb.Sql("INSERT INTO Products (Name, Description, Stock, ImageURL, Price, CategoryId) VALUES ('Fone de Ouvido Bluetooth', 'Fone de ouvido Bluetooth com cancelamento de ruído.', 15, 'https://example.com/images/fone_ouvido_bluetooth.jpg', 299.90, 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
