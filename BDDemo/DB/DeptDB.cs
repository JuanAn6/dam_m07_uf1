using Microsoft.EntityFrameworkCore;
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
    public class DeptDB
    {

        public static long CountDepts()
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
                        consulta.CommandText = @"select count(dept_no) from dept";
                        count = (long) consulta.ExecuteScalar();   
                    }

                }
            }
            return count;

        }

        public static bool DeleteDept(int dept_no)
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
                        consulta.CommandText = @"delete from dept where dept_no = @dept_no";

                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans; 

                        DBUtils.createParam(consulta, dept_no, "dept_no", System.Data.DbType.Int32);

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

        public static List<Dept> GetDepts()
        {
            List<Dept> departaments = new List<Dept>();
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
                        consulta.CommandText = @"select * from dept";
                        var reader = consulta.ExecuteReader();
                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int dept_no = reader.GetInt32(reader.GetOrdinal("DEPT_NO"));
                            string dnom = reader.GetString(reader.GetOrdinal("DNOM"));
                            string loc = reader.GetString(reader.GetOrdinal("LOC"));

                            departaments.Add(new Dept(dept_no, dnom, loc));
                        }
                    }

                }
            }
            return departaments;

        }

        public static bool insertDept(Dept d)
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

                        //recojer el lastId
                        int last_id = DeptDB.lastIdDepts(consulta,trans);

                        //INCREMENTAR EL LAST_ID NS CUANDO!!!!!!!

                        // query SQL
                        consulta.CommandText = @"insert into dept  (dept_no, dnom, loc) values (@dept_no, @dnom, @loc)";

                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans;

                        DBUtils.createParam(consulta, last_id, "dept_no", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.DNom, "dnom", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.Loc, "loc", System.Data.DbType.String);

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

        public static bool updateDept(Dept d)
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
                        consulta.CommandText = @"update dept set dnom = @dnom , loc = @loc where dept_no = @dept_no";

                        //Necessari per que la consulta estigui en la transacció
                        consulta.Transaction = trans;

                        DBUtils.createParam(consulta, d.Dept_no, "dept_no", System.Data.DbType.Int32);
                        DBUtils.createParam(consulta, d.DNom, "dnom", System.Data.DbType.String);
                        DBUtils.createParam(consulta, d.Loc, "loc", System.Data.DbType.String);

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

        private static int lastIdDepts(DbCommand consulta, DbTransaction trans)
        {
            // query SQL
            consulta.CommandText = @"update last_id where table_name='dept' for update";

            //Necessari per que la consulta estigui en la transacció
            consulta.Transaction = trans;

            int last_id = (int)consulta.ExecuteScalar();

            return last_id;


        }






    }

}
