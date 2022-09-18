using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationRental.Infrastructure.Entities
{
    public class RentalEntity : BaseEntity<int>
    {
        public RentalEntity() : base(0)
        {

        }
        public RentalEntity(int key) : base(key)
        {
        }

        public int Units { get; set; }
    }
}
