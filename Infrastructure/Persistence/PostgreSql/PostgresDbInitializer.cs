using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Postgres;

public class PostgresDbInitializer : DapperBase, IDbInitializer
{
    private string _masterConStr;

    public PostgresDbInitializer(IConfiguration config) : base(config)
    {
        _masterConStr = config["POSTGRES_MASTER_CON_STR"];
    }

    public async Task InitializeDbAsync()
    {
        if (!await CheckAppDbExists())
        {
            await CreateAppDbAsync();
            await CreateBasketTableAsync();
            await CreateProductTableAsync();
            await CreateBasketItemsTableAsync();
            await InsertToBasketTable();
            await InsertToProductTable();
            await InsertToBasketItemsTable();
        }
    }

    private async Task<bool> CheckAppDbExists()
    {
        var query = "SELECT 1 FROM pg_database WHERE datname = 'basket_db';";
        var queryResult = await ExecuteQueryAsync<string?>(query, null, _masterConStr);
        return queryResult.Count() < 1 ? false : true;
    }

    private async Task CreateAppDbAsync()
    {
        var command = "CREATE DATABASE basket_db;";
        await ExecuteCommandAsync(command, null, _masterConStr);
    }

    private async Task CreateBasketTableAsync()
    {
        var command = @"
            CREATE TABLE IF NOT EXISTS public.""baskets""
            (
                ""id"" character varying COLLATE pg_catalog.""default"" NOT NULL,
                ""ip_address"" character varying COLLATE pg_catalog.""default"",
                ""user_id"" character varying COLLATE pg_catalog.""default"",
                ""date_created"" date,
                ""date_updated"" date,
                ""date_deleted"" date,
                CONSTRAINT ""baskets_pkey"" PRIMARY KEY(""id"")
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.""baskets"" OWNER to postgres;
        ";

        await ExecuteCommandAsync(command);
    }

    private async Task CreateProductTableAsync()
    {
        var command = @"
            CREATE TABLE IF NOT EXISTS public.""products""
            (
                ""id"" character varying COLLATE pg_catalog.""default"" NOT NULL,
                ""category_id"" character varying COLLATE pg_catalog.""default"",
                ""name"" character varying COLLATE pg_catalog.""default"",
                ""price"" money,
                ""stock_count"" bigint,
                ""date_added"" date,
                ""date_updated"" date,
                ""date_deleted"" date,
                CONSTRAINT ""products_pkey"" PRIMARY KEY (""id"")
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.""products""
                OWNER to postgres;
        ";

        await ExecuteCommandAsync(command);
    }

    private async Task CreateBasketItemsTableAsync()
    {
        var command = @"
            CREATE TABLE IF NOT EXISTS public.""basket_items""
            (
                ""id"" character varying COLLATE pg_catalog.""default"" NOT NULL,
                ""basket_id"" character varying COLLATE pg_catalog.""default"",
                ""product_id"" character varying COLLATE pg_catalog.""default"",
                ""count"" bigint,
                ""price_in_basket"" money,
                ""date_added"" date,
                ""date_updated"" date,
                ""date_deleted"" date,
                CONSTRAINT ""basket_items_pkey"" PRIMARY KEY (""id""),
                CONSTRAINT ""fk_basket_id"" FOREIGN KEY (""basket_id"")
                    REFERENCES public.""baskets"" (""id"") MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION,
                CONSTRAINT ""fk_product_id"" FOREIGN KEY (""product_id"")
                    REFERENCES public.""products"" (""id"") MATCH SIMPLE
                    ON UPDATE NO ACTION
                    ON DELETE NO ACTION
                    NOT VALID
            )

            TABLESPACE pg_default;

            ALTER TABLE IF EXISTS public.""basket_items""
                OWNER to postgres;
        ";

        await ExecuteCommandAsync(command);
    }

    private async Task InsertToBasketTable()
    {
        var command = @"
        INSERT INTO baskets(""id"", ip_address, user_id, date_created, date_updated, date_deleted)
	                VALUES('basketid1', 'ipaddress1', 'userid1', '2022-10-25', '2022-10-25','2022-10-25'),
		                  ('basketid2', 'ipaddress2', 'userid2', '2022-10-25', '2022-10-25', '2022-10-25')
        ";

        await ExecuteCommandAsync(command);
    }

    private async Task InsertToProductTable()
    {
        var command = @"
    INSERT INTO products(""id"", category_id, ""name"", price, stock_count, date_added, date_deleted)
            VALUES('productid1', 'categoryid1', 'fare',     100.00, 1, '2022-10-25', '2022-10-25'),
            	  ('productid2', 'categoryid2', 'klavye',   200.00, 2, '2022-10-25', '2022-10-25'),
            	  ('productid3', 'categoryid3', 'laptop',   300.00, 3, '2022-10-25', '2022-10-25'),
            	  ('productid4', 'categoryid4', 'defter',   400.00, 4, '2022-10-25', '2022-10-25'),
            	  ('productid5', 'categoryid5', 'kalem',    500.00, 5, '2022-10-25', '2022-10-25'),
            	  ('productid6', 'categoryid6', 'kitap',    600.00, 6, '2022-10-25', '2022-10-25'),
            	  ('productid7', 'categoryid7', 'koltuk',   700.00, 7, '2022-10-25', '2022-10-25'),
            	  ('productid8', 'categoryid8', 'masa',     800.00, 8, '2022-10-25', '2022-10-25')
        ";

        await ExecuteCommandAsync(command);
    }

    private async Task InsertToBasketItemsTable()
    {
        var command = @"
            INSERT INTO basket_items(""id"", basket_id, product_id, ""count"", price_in_basket, date_added, date_deleted)
		    VALUES('itemid1', 'basketid1', 'productid1', 1, 300.00, '2022-10-25', '2022-10-25'),
			      ('itemid2', 'basketid2', 'productid2', 2, 600.00, '2022-10-25', '2022-10-25'),
			      ('itemid3', 'basketid2', 'productid1', 3, 900.00, '2022-10-25', '2022-10-25')
        ";

        await ExecuteCommandAsync(command);
    }
}