using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Repositories.Abstract;
using Data.Domain;
using System.Data.Linq;

namespace Data.Repositories.Static
{
    public class StaticEquipmentRepository:IEquipmentRepository
    {
        private static IList<Equipment> fakeEquipment= new List<Equipment>();
        private static int counter = 1;

        public IQueryable<Domain.Equipment> Equipments
        {
            get { return fakeEquipment.AsQueryable(); }
        }

        public void SaveEquipment(Equipment equipment)
        {
            // If it's a new equipment, just add it to the list
            if (equipment.EquipmentId == 0)
            {
                equipment.EquipmentId = counter;
                counter += 1;
                fakeEquipment.Add(equipment);
            }
            else if (fakeEquipment.Count(row => row.EquipmentId == equipment.EquipmentId) == 1)
            {
                //This is an update. Remove old one, insert new one
                DeleteEquipment(equipment);
                fakeEquipment.Add(equipment);
            }
        }

        public void DeleteEquipment(Domain.Equipment equipment)
        {
            var temp = fakeEquipment.ToList();
            temp.RemoveAll(row => row.EquipmentId == equipment.EquipmentId);
            fakeEquipment = temp;
        }

        public IEnumerable<Equipment> GetEquipmentByType(string equipmenttype)
        {

            return fakeEquipment;
        }

        /// <summary>
        /// Only for Unit Testing. Clears repo of all data
        /// </summary>
        public void ClearRepo()
        {
            fakeEquipment.Clear();
        }
    }
}
