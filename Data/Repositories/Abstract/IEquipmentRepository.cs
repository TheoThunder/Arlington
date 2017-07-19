using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IEquipmentRepository
    {
        IQueryable<Equipment> Equipments { get; }
        void SaveEquipment(Equipment equipment);
        void DeleteEquipment(Equipment equipment);
        IEnumerable<Equipment> GetEquipmentByType(string equipmenttype);
       
    }
}
