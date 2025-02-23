using _20241128_PracticaEntrades.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace DB
{
    public class SalaDB
    {
        public static long CountSalas()
        {
            long count = 0;
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {
                        // query SQL
                        consulta.CommandText = @"select count(id) from Sala";
                        count = (long)consulta.ExecuteScalar();
                    }

                }
            }
            return count;

        }

        public static bool DeleteSala(int id)
        {
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    var trans = connexio.BeginTransaction();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {
                        // query SQL
                        consulta.CommandText = @"delete from sala where id = @id";

                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans;

                        DBUtils.createParam(consulta, id, "id", System.Data.DbType.Int32);

                        int filesAfectades = -1;

                        try
                        {
                            filesAfectades = consulta.ExecuteNonQuery();

                            if (filesAfectades == 1)
                            {
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                trans.Rollback();
                            }

                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            trans.Rollback();
                        }


                        return false;
                    }

                }
            }

        }

        public static List<Sala> GetSalas()
        {
            List<Sala> sales = new List<Sala>();
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {

                        // query SQL
                        consulta.CommandText = @"select * from Sala";
                        var reader = consulta.ExecuteReader();
                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string nom = reader.GetString(reader.GetOrdinal("nom"));
                            string adreca = reader.GetString(reader.GetOrdinal("adreca"));
                            string municipi = reader.GetString(reader.GetOrdinal("municipi"));
                            int numColumnes = reader.GetInt32(reader.GetOrdinal("num_columnes"));
                            int numFiles = reader.GetInt32(reader.GetOrdinal("num_files"));
                            bool teMapa = reader.GetBoolean(reader.GetOrdinal("te_mapa"));

                            sales.Add(new Sala(id, nom, adreca, municipi, new List<Zona>(), numColumnes, numFiles, teMapa));
                        }
                    }

                }
            }
            return sales;

        }
        public static Sala GetSala(int sala_id)
        {
            Sala sala = null;
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {

                    // Obrir la connexió a la BD
                    connexio.Open();

                    List<Zona> list_zones = new List<Zona>();
                    // Crear una consulta SQL
                    using (var consulta_zones = connexio.CreateCommand())
                    {

                        consulta_zones.CommandText = @"select * from zona where sala_id = @sala_id";
                        DBUtils.createParam(consulta_zones, sala_id, "sala_id", System.Data.DbType.Int32);

                        using (var reader_zona = consulta_zones.ExecuteReader())
                        {
                            while (reader_zona.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                            {

                                int id = reader_zona.GetInt32(reader_zona.GetOrdinal("id"));
                                string desc = reader_zona.GetString(reader_zona.GetOrdinal("desc"));
                                string nom = reader_zona.GetString(reader_zona.GetOrdinal("nom"));
                                int numero = reader_zona.GetInt32(reader_zona.GetOrdinal("numero"));
                                int capacitat = reader_zona.GetInt32(reader_zona.GetOrdinal("capacitat"));
                                string color = reader_zona.GetString(reader_zona.GetOrdinal("color"));

                                List<Cadira> list_cadires = new List<Cadira>();

                                Zona z = new Zona(id, desc, nom, numero, capacitat, color, list_cadires);

                                list_zones.Add(z);

                            }

                        }

                    }


                    foreach (Zona ds in list_zones)
                    {
                        Debug.WriteLine("List_zones: " + ds.ToString());
                        using (var consulta_cadires = connexio.CreateCommand())
                        {
                            consulta_cadires.CommandText = @"select * from cadira where zona_id = @zona_id";
                            DBUtils.createParam(consulta_cadires, ds.Id, "zona_id", System.Data.DbType.Int32);

                            using (var reader_cadires = consulta_cadires.ExecuteReader())
                            {
                                while (reader_cadires.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                                {

                                    int id_cadira = reader_cadires.GetInt32(reader_cadires.GetOrdinal("id"));
                                    int x = reader_cadires.GetInt32(reader_cadires.GetOrdinal("x"));
                                    int y = reader_cadires.GetInt32(reader_cadires.GetOrdinal("y"));

                                    Cadira c = new Cadira(x, y);

                                    ds.Cadires.Add(c);

                                }

                            }
                        }
                    }


                    using (var consulta = connexio.CreateCommand())
                    {
                        // query SQL
                        consulta.CommandText = @"select * from Sala where id = @sala_id";
                        DBUtils.createParam(consulta, sala_id, "sala_id", System.Data.DbType.Int32);

                        using (var reader = consulta.ExecuteReader())
                        {
                            while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                            {

                                int id = reader.GetInt32(reader.GetOrdinal("id"));
                                string nom = reader.GetString(reader.GetOrdinal("nom"));
                                string adreca = reader.GetString(reader.GetOrdinal("adreca"));
                                string municipi = reader.GetString(reader.GetOrdinal("municipi"));
                                int numColumnes = reader.GetInt32(reader.GetOrdinal("num_columnes"));
                                int numFiles = reader.GetInt32(reader.GetOrdinal("num_files"));
                                bool teMapa = reader.GetBoolean(reader.GetOrdinal("te_mapa"));

                                sala = new Sala(id, nom, adreca, municipi, list_zones, numColumnes, numFiles, teMapa);
                            }
                        }


                    }




                }
            }

            return sala;

        }

        public static List<Sala> GetSalasPage(int currentPage, int items_per_page)
        {
            List<Sala> sales = new List<Sala>();
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {

                        // query SQL
                        consulta.CommandText = @"select * from Sala limit " + items_per_page + " offset " + ((currentPage - 1) * items_per_page);
                        var reader = consulta.ExecuteReader();
                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string nom = reader.GetString(reader.GetOrdinal("nom"));
                            string adreca = reader.GetString(reader.GetOrdinal("adreca"));
                            string municipi = reader.GetString(reader.GetOrdinal("municipi"));
                            int numColumnes = reader.GetInt32(reader.GetOrdinal("num_columnes"));
                            int numFiles = reader.GetInt32(reader.GetOrdinal("num_files"));
                            bool teMapa = reader.GetBoolean(reader.GetOrdinal("te_mapa"));

                            sales.Add(new Sala(id, nom, adreca, municipi, new List<Zona>(), numColumnes, numFiles, teMapa));
                        }
                    }

                }
            }
            return sales;
        }

        public static long CountSalasFiltered(string name_filter)
        {
            long count = 0;
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {
                        // query SQL
                        consulta.CommandText = @"select count(id) from Sala where nom LIKE @name_filter";

                        DBUtils.createParam(consulta, "%" + name_filter + "%", "name_filter", System.Data.DbType.String);

                        count = (long)consulta.ExecuteScalar();
                    }

                }
            }
            return count;

        }

        public static List<Sala> GetSalasPageFiltered(int currentPage, int items_per_page, string name_filter)
        {
            List<Sala> sales = new List<Sala>();
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {

                        // query SQL
                        consulta.CommandText = @"select * from sala where nom LIKE @name_filter limit " + items_per_page + " offset " + ((currentPage - 1) * items_per_page);

                        DBUtils.createParam(consulta, "%" + name_filter + "%", "name_filter", System.Data.DbType.String);

                        var reader = consulta.ExecuteReader();

                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string nom = reader.GetString(reader.GetOrdinal("nom"));
                            string adreca = reader.GetString(reader.GetOrdinal("adreca"));
                            string municipi = reader.GetString(reader.GetOrdinal("municipi"));
                            int numColumnes = reader.GetInt32(reader.GetOrdinal("num_columnes"));
                            int numFiles = reader.GetInt32(reader.GetOrdinal("num_files"));
                            bool teMapa = reader.GetBoolean(reader.GetOrdinal("te_mapa"));

                            sales.Add(new Sala(id, nom, adreca, municipi, new List<Zona>(), numColumnes, numFiles, teMapa));
                        }
                    }

                }
            }
            return sales;
        }

        public static bool insertSala(Sala d)
        {
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    var trans = connexio.BeginTransaction();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {

                        // query SQL
                        consulta.CommandText = @"insert into Sala  (nom, adreca, municipi, num_columnes, num_files, te_mapa) values 
                                                (@nom, @adreca, @municipi, @num_columnes, @num_files, @te_mapa);
                                                SELECT LAST_INSERT_ID();";

                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans;


                        DBUtils.createParam(consulta, d.Nom, "nom", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.Adreca, "adreca", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.Municipi, "municipi", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.NumColumnes, "num_columnes", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.NumFiles, "num_files", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.TeMapa, "te_mapa", System.Data.DbType.Boolean);

                        int filesAfectades = -1;
                        try
                        {
                            //RECOJER EL ID DE LA SALA ???
                            //filesAfectades = consulta.ExecuteNonQuery();
                            int salaId = Convert.ToInt32(consulta.ExecuteScalar());
                            d.Id = salaId;
                            Debug.WriteLine("Sala id insertada: " + d.Id);
                            insertZonesOfSala(connexio, trans, d.Zones, d.Id);

                            if (salaId > 0)
                            {
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                trans.Rollback();
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine("ERROR!!! " + e.Message);
                            trans.Rollback();
                        }


                        return false;
                    }

                }
            }
        }

        public static void insertZonesOfSala(DbConnection connexio, DbTransaction transaction, List<Zona> zones, int salaId)
        {
            foreach (var zona in zones)
            {
                using (var consulta = connexio.CreateCommand())
                {
                    // query SQL
                    consulta.CommandText = @"insert into Zona (nom, numero, capacitat, color, `desc`, sala_id) values
                                     (@nom, @numero, @capacitat, @color, @desc, @sala_id);
                                     SELECT LAST_INSERT_ID();";

                    consulta.Transaction = transaction;

                    // Asignar parámetros
                    DBUtils.createParam(consulta, zona.Nom, "nom", System.Data.DbType.String);
                    DBUtils.createParam(consulta, zona.Numero, "numero", System.Data.DbType.Int32);
                    DBUtils.createParam(consulta, zona.Capacitat, "capacitat", System.Data.DbType.Int32);
                    DBUtils.createParam(consulta, zona.Color, "color", System.Data.DbType.String);
                    DBUtils.createParam(consulta, zona.Desc, "desc", System.Data.DbType.String);
                    DBUtils.createParam(consulta, salaId, "sala_id", System.Data.DbType.Int32);

                    try
                    {
                        int zonaId = Convert.ToInt32(consulta.ExecuteScalar());
                        insertCadiresZona(connexio, transaction, zona.Cadires, zonaId);

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("ERROR!!! " + e.Message);
                        throw; //D'aquesta manera la excecpió de la funcio de insert o update sala saltaria
                    }
                }
            }
        }

        private static void insertCadiresZona(DbConnection connexio, DbTransaction transaction, List<Cadira> cadires, int zonaId)
        {
            foreach (var cadira in cadires)
            {
                using (var consulta = connexio.CreateCommand())
                {
                    // query SQL
                    consulta.CommandText = @"insert into cadira (zona_id, x, y ) values
                                     (@zona_id, @x, @y )";

                    consulta.Transaction = transaction;

                    // Asignar parámetros
                    DBUtils.createParam(consulta, zonaId, "zona_id", System.Data.DbType.Int32);
                    DBUtils.createParam(consulta, cadira.X, "x", System.Data.DbType.Int32);
                    DBUtils.createParam(consulta, cadira.Y, "y", System.Data.DbType.Int32);


                    try
                    {
                        consulta.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("ERROR!!! " + e.Message);
                        throw; //D'aquesta manera la excecpió de la funcio de insert o update sala saltaria
                    }
                }
            }
        }

        public static bool updateSala(Sala d)
        {
            using (MysqlEntityContext context = new MysqlEntityContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    var trans = connexio.BeginTransaction();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {
                        // query SQL
                        consulta.CommandText = @"update Sala set nom = @nom, adreca = @adreca, municipi = @municipi, 
                                                num_columnes = @num_columnes, 
                                                num_files = @num_files, te_mapa = @te_mapa where id = @id;";

                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans;

                        DBUtils.createParam(consulta, d.Id, "id", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.Nom, "nom", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.Adreca, "adreca", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.Municipi, "municipi", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.NumColumnes-1, "num_columnes", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.NumFiles-1, "num_files", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.TeMapa, "te_mapa", System.Data.DbType.Boolean);

                        int filesAfectades = -1;
                        try
                        {
                            //Eliminar totes les zones i les cadires de la sala
                            deleteZonesAndCadiresOfSala(connexio, trans, d.Id);

                            //Inserció de les noves zones i cadires
                            filesAfectades = consulta.ExecuteNonQuery();
                            Debug.WriteLine("Sala update ID: " + d.Id + " filesAfectades: " + filesAfectades);

                            insertZonesOfSala(connexio, trans, d.Zones, d.Id);

                            if (filesAfectades > 0)
                            {
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                trans.Rollback();
                            }
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                        }

                        return false;
                    }

                }
            }


        }





        public static bool deleteZonesAndCadiresOfSala(DbConnection connexio, DbTransaction transaction, int id_sala)
        {
            // Crear una consulta SQL
            using (var consulta = connexio.CreateCommand())
            {
                // query SQL
                consulta.CommandText = @"delete from cadira where zona_id in( select id from zona where sala_id = @id);
                                        delete from zona where sala_id = @id;";

                //Necessari per que la consulta estigui en la transacció
                consulta.Transaction = transaction;

                DBUtils.createParam(consulta, id_sala, "id", System.Data.DbType.Int32);

                int filesAfectades = -1;

                try
                {
                    filesAfectades = consulta.ExecuteNonQuery();
                    Debug.WriteLine("Files Delete: " + filesAfectades);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    throw;
                }


                return false;
            }

        }


    }
}
