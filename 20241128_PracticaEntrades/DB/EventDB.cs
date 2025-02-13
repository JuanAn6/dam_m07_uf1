using _20241128_PracticaEntrades.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DB
{
    public class EventDB
    {

        public static long CountEvents()
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
                        consulta.CommandText = @"select count(id) from event";
                        count = (long) consulta.ExecuteScalar();
                        Debug.WriteLine("COUNT EVENTS: " + count);
                    }

                }
            }
            return count;

        }

        public static bool DeleteEvent(int event_id)
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
                        consulta.CommandText = @"delete from event where id = @id";

                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans;

                        DBUtils.createParam(consulta, event_id, "id", System.Data.DbType.Int32);

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

        public static List<Event> GetEvents()
        {
            List<Event> events = new List<Event>();
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
                        consulta.CommandText = @"select * from event";
                        var reader = consulta.ExecuteReader();
                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {

                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string tipus = reader.GetString(reader.GetOrdinal("tipus"));
                            string nom = reader.GetString(reader.GetOrdinal("nom"));
                            string protagonista = reader.GetString(reader.GetOrdinal("protagonista"));
                            string desc = reader.GetString(reader.GetOrdinal("descripcio"));
                            string imgPath = reader.GetString(reader.GetOrdinal("img_path"));
                            DateTime data = reader.GetDateTime(reader.GetOrdinal("data_event"));
                            string estatDB = reader.GetString(reader.GetOrdinal("estat"));


                            TipusEvent p ;
                            switch(tipus)
                            {
                                case "MUSICA":
                                    p = TipusEvent.MUSICA;
                                    break;
                                case "TEATRE":
                                    p = TipusEvent.TEATRE;
                                    break;
                                case "ESPORT":
                                    p = TipusEvent.ESPORT;
                                    break;
                                default:
                                    p = TipusEvent.ALTRES;
                                    break;
                            }

                            Estat estat;

                            switch (estatDB)
                            {
                                case "NOU":
                                    estat = Estat.NOU;
                                    break;
                                case "PUBLICAT":
                                    estat = Estat.PUBLICAT;
                                    break;
                                case "ARXIVAT":
                                    estat = Estat.ARXIVAT;
                                    break;
                                default:
                                    estat = Estat.ARXIVAT;
                                    break;
                            }

                            Sala sala = GetSalaEvent(reader.GetInt32(reader.GetOrdinal("sala_id")));

                            List<Tarifa> tarifes = new List<Tarifa>();

                            events.Add(new Event(p, id, nom, protagonista, desc, imgPath, data, estat, sala, tarifes));

                        }
                    }

                }
            }
            return events;

        }

        public static List<Event> GetEventsPage(int currentPage, float items_per_page)
        {
            List<Event> events = new List<Event>();
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
                        consulta.CommandText = @"select * from event limit "+items_per_page+" offset "+ ((currentPage - 1) * items_per_page);
                        var reader = consulta.ExecuteReader();
                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string tipus = reader.GetString(reader.GetOrdinal("tipus"));
                            string nom = reader.GetString(reader.GetOrdinal("nom"));
                            string protagonista = reader.GetString(reader.GetOrdinal("protagonista"));
                            string desc = reader.GetString(reader.GetOrdinal("descripcio"));
                            string imgPath = reader.GetString(reader.GetOrdinal("img_path"));
                            DateTime data = reader.GetDateTime(reader.GetOrdinal("data_event"));
                            string estatDB = reader.GetString(reader.GetOrdinal("estat"));


                            TipusEvent p;
                            switch (tipus)
                            {
                                case "MUSICA":
                                    p = TipusEvent.MUSICA;
                                    break;
                                case "TEATRE":
                                    p = TipusEvent.TEATRE;
                                    break;
                                case "ESPORT":
                                    p = TipusEvent.ESPORT;
                                    break;
                                default:
                                    p = TipusEvent.ALTRES;
                                    break;
                            }

                            Estat estat;

                            switch (estatDB)
                            {
                                case "NOU":
                                    estat = Estat.NOU;
                                    break;
                                case "PUBLICAT":
                                    estat = Estat.PUBLICAT;
                                    break;
                                case "ARXIVAT":
                                    estat = Estat.ARXIVAT;
                                    break;
                                default:
                                    estat = Estat.ARXIVAT;
                                    break;
                            }

                            Sala sala = SalaDB.GetSala(reader.GetInt32(reader.GetOrdinal("sala_id")));

                            List<Tarifa> tarifes = new List<Tarifa>();

                            events.Add(new Event(p, id, nom, protagonista, desc, imgPath, data, estat, sala, tarifes));
                        }
                    }

                }
            }
            return events;
        }

        public static List<Event> GetEventsPageFiltered(int currentPage, float items_per_page, string name_filter)
        {
            List<Event> events = new List<Event>();
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
                        consulta.CommandText = @"select * from event where nom LIKE @name_filter limit " + items_per_page + " offset " + ((currentPage - 1) * items_per_page);
                        
                        DBUtils.createParam(consulta, "%" + name_filter + "%", "name_filter", System.Data.DbType.String);
                        
                        var reader = consulta.ExecuteReader();
                        
                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string tipus = reader.GetString(reader.GetOrdinal("tipus"));
                            string nom = reader.GetString(reader.GetOrdinal("nom"));
                            string protagonista = reader.GetString(reader.GetOrdinal("protagonista"));
                            string desc = reader.GetString(reader.GetOrdinal("descripcio"));
                            string imgPath = reader.GetString(reader.GetOrdinal("img_path"));
                            DateTime data = reader.GetDateTime(reader.GetOrdinal("data_event"));
                            string estatDB = reader.GetString(reader.GetOrdinal("estat"));


                            TipusEvent p;
                            switch (tipus)
                            {
                                case "MUSICA":
                                    p = TipusEvent.MUSICA;
                                    break;
                                case "TEATRE":
                                    p = TipusEvent.TEATRE;
                                    break;
                                case "ESPORT":
                                    p = TipusEvent.ESPORT;
                                    break;
                                default:
                                    p = TipusEvent.ALTRES;
                                    break;
                            }

                            Estat estat;

                            switch (estatDB)
                            {
                                case "NOU":
                                    estat = Estat.NOU;
                                    break;
                                case "PUBLICAT":
                                    estat = Estat.PUBLICAT;
                                    break;
                                case "ARXIVAT":
                                    estat = Estat.ARXIVAT;
                                    break;
                                default:
                                    estat = Estat.ARXIVAT;
                                    break;
                            }

                            Sala sala = SalaDB.GetSala(reader.GetInt32(reader.GetOrdinal("sala_id")));

                            List<Tarifa> tarifes = new List<Tarifa>();

                            events.Add(new Event(p, id, nom, protagonista, desc, imgPath, data, estat, sala, tarifes));
                        }
                    }

                }
            }
            return events;
        }

        public static long CountEventsFiltered(string name_filter)
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
                        consulta.CommandText = @"select count(id) from event where nom LIKE @name_filter";
                        
                        DBUtils.createParam(consulta, "%" + name_filter + "%", "name_filter", System.Data.DbType.String);

                        count = (long)consulta.ExecuteScalar();
                        Debug.WriteLine("COUNT EVENTS: " + count);
                    }

                }
            }
            return count;

        }

        public static bool insertEvent(Event e)
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
                        consulta.CommandText = @"insert into event (tipus, nom, protagonista, descripcio, img_path, data_event, estat, sala_id) values 
                                                (@tipus, @nom, @protagonista, @descripcio, @img_path, @data_event, @estat, @sala_id)";


                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans;


                        DBUtils.createParam(consulta, e.Tipus, "tipus", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.Nom, "nom", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.Protagonista, "protagonista", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.Desc, "descripcio", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.ImgPath, "img_path", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.Data, "data_event", System.Data.DbType.DateTime);
                        DBUtils.createParam(consulta, e.Estat, "estat", System.Data.DbType.String);

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
                        catch (Exception ex)
                        {
                            Debug.WriteLine("ERROR!!! "+ex.Message);
                            trans.Rollback();
                        }


                        return false;
                    }

                }
            }
        }

        public static bool updateDept(Event e)
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
                        consulta.CommandText = @"update event set tipus = @tipus, nom = @nom, protagonista = @protagonista, descripcio = @descripcio,
                                                    img_path = @img_path, data_event = @data_event, estat = @estat, sala_id = @sala_id where id = @id";

                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans;

                        DBUtils.createParam(consulta, e.Tipus, "tipus", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.Nom, "nom", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.Protagonista, "protagonista", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.Desc, "descripcio", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.ImgPath, "img_path", System.Data.DbType.String);
                        DBUtils.createParam(consulta, e.Data, "data_event", System.Data.DbType.DateTime);
                        DBUtils.createParam(consulta, e.Estat, "estat", System.Data.DbType.String);

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



        private static Sala GetSalaEvent(int sala_id)
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

    }

}
