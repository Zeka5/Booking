using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class OwnerDto
    {
        private bool isSuper;
        public bool IsSuper
        {
            get { return isSuper; }
            set
            {
                if (isSuper != value)
                {
                    isSuper = value;
                }
            }
        }

        private int totalRatings;
        public int TotalRatings
        {
            get { return totalRatings; }
            set
            {
                if (totalRatings != value)
                {
                    totalRatings = value;
                }
            }
        }
    }
}
