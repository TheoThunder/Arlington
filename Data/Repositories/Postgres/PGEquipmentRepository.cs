using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Data.Repositories.Abstract;
using Data.Domain;

namespace Data.Repositories.Postgres
{
    public class PGEquipmentRepository : IEquipmentRepository
    {
        #region Query Strings
        private string EquipmentSelectQuery = @"SELECT equipmentid, equipmentname, equipmenttype, active FROM equipment";
        private string EquipmentInsertQuery = @"INSERT INTO equipment (equipmentname, equipmenttype, active) VALUES (:name, :type, :active)";
        private string EquipmentUpdateQuery = @"UPDATE equipment SET equipmentname = :name, equipmenttype = :type, active = :active WHERE equipmentid = :equipmentid";
        private string EquipmentDeleteQuery = @"DELETE FROM equipment WHERE equipmentid = :equipmentid";
        private string EquipmentTypeQuery = @"SELECT equipmentid, equipmentname, equipmenttype, active FROM equipment AS a Where equipmenttype = :equiptype";
        //private string EquipmentTypeCheck = @"SELECT equipmentname FROM equipment WHERE equipmenttype = 'Check'";
        //private string EquipmentTypeTerminal = @"SELECT equipmentname FROM equipment WHERE equipmenttype = 'Termninal'";
        //private string EquipmentTypePinpad = @"SELECT equipmentname FROM equipment WHERE equipmenttype = 'Pinpad'";
        #endregion

        //  private static int counter = 1;
        public static IList<Domain.Equipment> fakeEquipments = new List<Domain.Equipment>();
        public IQueryable<Domain.Equipment> Equipments
        {

            get
            {
                fakeEquipments.Clear();
                using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
                {
                    conn.Open();
                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(EquipmentSelectQuery, conn))
                    {
                        using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Equipment newEquipment = populateEquipmentFromDB(dr);
                                fakeEquipments.Add(newEquipment);
                            }
                        }
                    }
                }
                return fakeEquipments.AsQueryable();

            }

        }

        public void SaveEquipment(Domain.Equipment equipment)
        {
            string query;
            bool isUpdate = false;
            // Want to know right off the bat if we're doing a insert or update
            if (equipment.EquipmentId > 0)
            {
                query = EquipmentUpdateQuery;
                isUpdate = true;
            }
            else
            {
                query = EquipmentInsertQuery;
            }

            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(query, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("type", NpgsqlTypes.NpgsqlDbType.Text));
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("active", NpgsqlTypes.NpgsqlDbType.Boolean));
                    
                    if (isUpdate)
                        command.Parameters.Add(new Npgsql.NpgsqlParameter("equipmentid", NpgsqlTypes.NpgsqlDbType.Integer));

                    command.Prepare();

                    command.Parameters[0].Value = equipment.Name;
                    command.Parameters[1].Value = equipment.Type;
                    command.Parameters[2].Value = equipment.Active;
                    
                    if (isUpdate)
                    {
                        command.Parameters[3].Value = equipment.EquipmentId;
                    }

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }

            
        }

        public void DeleteEquipment(Domain.Equipment equipment)
        {
            using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(EquipmentDeleteQuery, conn))
                {
                    command.Parameters.Add(new Npgsql.NpgsqlParameter("equipmentid", NpgsqlTypes.NpgsqlDbType.Integer));
                    command.Prepare();
                    command.Parameters[0].Value = equipment.EquipmentId;
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Equipment> GetEquipmentByType(string equipmenttype)
        {
            IList<Domain.Equipment> equiptype = new List<Domain.Equipment>();
            using(Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(Infrastructure.ConfigReader.ConnectionString.ToString()))
            {
                conn.Open();
                using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(EquipmentTypeQuery, conn))
                {
                   command.Parameters.Add(new Npgsql.NpgsqlParameter("equiptype", NpgsqlTypes.NpgsqlDbType.Text));
                   command.Prepare();
                   command.Parameters[0].Value = equipmenttype;
                   using (Npgsql.NpgsqlDataReader dr = command.ExecuteReader())
                   {
                       while ( dr.Read())
                       {
                           Equipment newequip = populateEquipmentFromDB(dr);
                           equiptype.Add(newequip);
                       }
                   }
                }
            }
            return equiptype;
        }


        

        #region Helper Methods
        private static Equipment populateEquipmentFromDB(Npgsql.NpgsqlDataReader dr)
        {
            Equipment newEquipment = new Equipment();
            newEquipment.EquipmentId = Helper.ConvertFromDBVal<int>(dr[0]);
            newEquipment.Name= dr[1].ToString();
            newEquipment.Type = dr[2].ToString();
            newEquipment.Active = Helper.ConvertFromDBVal<Boolean>(dr[3]);
            
            return newEquipment;
        }

        #endregion
    }
}
