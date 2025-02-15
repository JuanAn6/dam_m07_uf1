using _20241128_PracticaEntrades.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
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

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {

                        // query SQL
                        consulta.CommandText = @"select * from Sala where id = @sala_id";
                        DBUtils.createParam(consulta, sala_id, "sala_id", System.Data.DbType.Int32);

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

                            sala = new Sala(id, nom, adreca, municipi, new List<Zona>(), numColumnes, numFiles, teMapa);
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
                        consulta.CommandText = @"select * from Sala limit " + items_per_page + " offset " + ((currentPage-1)*items_per_page) ;
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
                        
                        DBUtils.createParam(consulta, "%"+name_filter+"%", "name_filter", System.Data.DbType.String);

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
                                                (@nom, @adreca, @municipi, @num_columnes, @num_files, @te_mapa)";

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
                            filesAfectades = consulta.ExecuteNonQuery();

                            insertZonesOfSala(d.Zones, d.Id)

                            if (filesAfectades > 1)
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

        private static void insertZonesOfSala(List<Zona> zones, int id)
        {
            throw new NotImplementedException();
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

                        DBUtils.createParam(consulta, d.Nom, "nom", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.Adreca, "adreca", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.Municipi, "municipi", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.NumColumnes, "num_columnes", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.NumFiles, "num_files", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.TeMapa, "te_mapa", System.Data.DbType.Boolean);

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
                        catch (Exception)
                        {
                            trans.Rollback();
                        }


                        return false;
                    }

                }
            }


        }
        


    }
}
